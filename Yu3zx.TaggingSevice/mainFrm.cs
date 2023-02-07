using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yu3zx.DapperExtend;

namespace Yu3zx.TaggingSevice
{
    public partial class mainFrm : Form
    {
        private Thread thTcp = null;
        TcpServer tcpServer = null;
        private List<FabricClothItem> lUnSave = new List<FabricClothItem>();
        Random rd = new Random();
        public mainFrm()
        {
            InitializeComponent();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            AppManager.CreateInstance().Init();
            txtPort.Text = AppManager.CreateInstance().Port.ToString();
            List<string> loaclIps = GetLoacalIp();
            cboServerIP.Items.AddRange(loaclIps.ToArray());
            cboServerIP.SelectedItem = AppManager.CreateInstance().ServerIp;
        }
        private List<string> GetLoacalIp()
        {
            List<string> Ips = new List<string>();
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    Ips.Add(ipa.ToString());
                }
            }
            return Ips;
        }
        private void tsmBackto_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void tsmQuit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确认退出服务？","提示",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            if (btnService.Text == "启动服务")
            {
                if(tcpServer != null)
                {
                    tcpServer.StopServer();
                    tcpServer = null;
                }

                tcpServer = new TcpServer(IPAddress.Parse(cboServerIP.Text), int.Parse(txtPort.Text));
                tcpServer.OnClothDataHandle += TcpServer_OnClothDataHandle;
                tcpServer.StartServer();
                if(tcpServer.IsServer)
                {
                    btnService.Text = "停止服务";
                }
            }
            else
            {
                try
                {
                    if (thTcp != null)
                    {
                        thTcp.Abort();
                        thTcp = null;
                    }
                    if (tcpServer != null)
                    {
                        tcpServer.StopServer();
                        tcpServer = null;
                    }
                }
                catch
                {

                }
                btnService.Text = "启动服务";
            }
        }

        /// <summary>
        /// 接收到数据处理；保存由上线端保存
        /// </summary>
        /// <param name="item"></param>
        private void TcpServer_OnClothDataHandle(FabricClothItem item)
        {
            if(item != null)
            {
                Console.WriteLine("接收到对象：" + Json.JSONUtil.SerializeJSON(item));
                string lNum = item.LineNum;
                if(string.IsNullOrEmpty(lNum)) //
                {
                    return;
                }
                //没有当前生产线时创建生产线
                if(!ProductManager.CreateInstance().DictOnLineCloths.ContainsKey(lNum))
                {
                    OnLineCloth onLine = new OnLineCloth();
                    onLine.LineNum = lNum;
                    ProductManager.CreateInstance().DictOnLineCloths.Add(lNum, onLine);
                }

                if (item.QualityName == "A") //需要包装的
                {
                    //打印管内标签
                    PrintTubeLabel(item);

                    //打印A类标签
                    PrintFabricLabel(item);

                    //保存
                    // SaveFabricCloth(item);

                    //加入到对应线上
                    ProductManager.CreateInstance().DictOnLineCloths[lNum].ClothItems.Add(item);

                    //判断是否可以装箱了
                    if (ProductManager.CreateInstance().DictOnLineCloths[lNum].ClothItems.Count >= AppManager.CreateInstance().PackingNum)
                    {
                        //触发
                        foreach (var citem in ProductManager.CreateInstance().DictOnLineCloths[lNum].ClothItems)
                        {
                            ProductStateManager.GetInstance().ProductSerialNo++;

                            citem.ReelNum = ProductStateManager.GetInstance().ProductSerialNo;
                        }
                        List<FabricClothItem> lPack = new List<FabricClothItem>();
                        int iNeed = AppManager.CreateInstance().PackingNum;
                        lock (ProductManager.CreateInstance().DictOnLineCloths)
                        {
                            while (iNeed >0)
                            {
                                var iRemove = ProductManager.CreateInstance().DictOnLineCloths[lNum].ClothItems[0];
                                lPack.Add(iRemove);
                                ProductManager.CreateInstance().DictOnLineCloths[lNum].ClothItems.RemoveAt(0);
                                iNeed--;
                            }
                        }

                        PrintPackingList(lPack);
                        //更新
                        ProductStateManager.GetInstance().Save();//保存当前序号

                        //通知PLC当前产线要上线
                    }
                }
                else
                {
                    string strQC = item.QualityName.ToUpper();//
                    switch (strQC)
                    {
                        case "KC":
                            //打印小标签
                            PrintTubeLabel(item);

                            PrintFabricLabel(item);

                            //SaveFabricCloth(item);
                            ProductManager.CreateInstance().DictOnLineCloths[lNum].OtherQualityItem.Add(item);
                            break;
                        case "SC":
                            //打印小标签
                            PrintTubeLabel(item);

                            PrintFabricLabel(item);

                            //SaveFabricCloth(item);
                            ProductManager.CreateInstance().DictOnLineCloths[lNum].OtherQualityItem.Add(item);
                            break;
                        case "HC":
                            //只打标签;需要统计
                            PrintTubeLabel(item);

                            //SaveFabricCloth(item);//需要统计就要保存
                            break;
                        default:
                            //其它不上线
                            break;
                    }
                }
            }
        }

        private void SaveFabricCloth(FabricClothItem item)
        {
            //保存
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                var result = db.InsertRow(item);
                if (result > 0)
                {
                    Console.WriteLine("添加成功");
                }
                else
                {
                    //lUnSave.Add(item);//未保存的列表
                    Console.WriteLine("添加失败");
                }
            }
        }

        /// <summary>
        /// 打印纸管内小标签
        /// </summary>
        private void PrintTubeLabel(FabricClothItem item)
        {
            Console.WriteLine("打印纸管内小标签成功！");
        }

        /// <summary>
        /// 面料标签
        /// </summary>
        private void PrintFabricLabel(FabricClothItem item)
        {
            Console.WriteLine("打印面料标签标签成功！");
            string strQC = item.QualityName.ToUpper();
            switch(strQC)
            {
                case "SC":
                case "KC":
                    //模板不同纸张不同，打印换纸麻烦
                    var pbCfg = AppManager.CreateInstance().GetPrintCfg(item.LineNum);
                    if (pbCfg != null)
                    {
                        Dictionary<string, string> dictData = PrintHelper.GetEntityPropertyToDict(item);
                        string lblFile = Application.StartupPath + "\\Templates\\" + pbCfg.LabelBName;
                        if (File.Exists(lblFile))
                        {
                            PrintHelper.CreateInstance().BarPrintInit(lblFile, pbCfg.PrinterName, dictData, pbCfg.PrintCopies);
                        }
                    }
                    //调用C类模板打印
                    Console.WriteLine(strQC + "已经打印");
                    break;
                case "A":
                    //调用A类模板打印
                    var pCfg = AppManager.CreateInstance().GetPrintCfg(item.LineNum);
                    if(pCfg != null)
                    {
                        Dictionary<string, string> dictData = PrintHelper.GetEntityPropertyToDict(item);
                        string lblFile = Application.StartupPath + "\\Templates\\" + pCfg.LabelName;
                        if (File.Exists(lblFile))
                        {
                            PrintHelper.CreateInstance().BarPrintInit(lblFile, pCfg.PrinterName, dictData, pCfg.PrintCopies);
                        }
                    }
                    Console.WriteLine(strQC + " A类：" + DateTime.Now.ToString("yyyyMMddHHmmss") + "已经打印");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PrintFabricList()
        {
            //从PLC 获取完成的 线号
            string iComplete = "1";
            var lComplete = ProductManager.CreateInstance().DictOnLineCloths[iComplete].ClothItems;

        }

        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="lPack">装箱个数</param>
        public void PrintPackingList(List<FabricClothItem> lPack)
        {
            Console.WriteLine(lPack.Count + " " + DateTime.Now.ToString("yyyyMMddHHmmss") + "已经打印装箱单！");
            foreach (var item in lPack)
            {
                Console.Write(" " + item.ReelNum.ToString());
            }

            //Console.WriteLine(lPack.Count + "" + DateTime.Now.ToString("yyyyMMddHHmmss") + "已经打印装箱单！");
        }
    }
}
