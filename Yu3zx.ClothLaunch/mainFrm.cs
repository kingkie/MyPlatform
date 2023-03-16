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
using System.Xml;
using Yu3zx.DapperExtend;
using Yu3zx.Json;

namespace Yu3zx.ClothLaunch
{
    public partial class mainFrm : Form
    {
        Thread thData = null;
        Random rd = new Random();
        private int SNum = 13;//

        private List<OnLaunchItem> ProduceList = new List<OnLaunchItem>();
        /// <summary>
        /// 当前计划
        /// </summary>
        private ProductPlan CurrentPlan
        {
            get;
            set;
        }
        /// <summary>
        /// 当前上线面料
        /// </summary>
        private FabricClothItem CurrentFabric
        {
            get;
            set;
        }

        private string QualityName
        {
            get;
            set;
        } = "A";

        public mainFrm()
        {
            InitializeComponent();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            //Debug模式
            {
                //txtBatchNo.Text ="BN" + DateTime.Now.ToString("yyyyMMddfff");
                //txtProduceNum.Text = (49 + rd.Next(1, 20) / 10f).ToString();
            }

            string strBatchNo = txtBatchNo.Text.Trim();// DateTime.Now.ToString("yyyyMMddfff");
            string strColorNum = txtColorNum.Text;
            float fProduceNum = float.Parse(txtProduceNum.Text);
            string strQualityName = QualityName;
            string strSpecs = txtSpecs.Text;//
            string strQString = txtQuatilyString.Text.Trim();
            int iWidth = int.Parse(txtCWidth.Text);
            int iRoll = int.Parse(txtRollDiam.Text);

            if (DeviceManager.CreateInstance().ClothClient != null && DeviceManager.CreateInstance().ClothClient.Connected)
            {
            }
            else
            {
                MessageBox.Show("未连接，请连接服务端后上线产品！");
                return;
            }

            FabricClothItem item = new FabricClothItem();
            item.BatchNo = strBatchNo;
            item.ColorNum = strColorNum;
            item.LineNum = AppManager.CreateInstance().LineNum.ToString();
            item.ProduceNum = fProduceNum;
            item.QualityName = strQualityName;
            item.QualityString = strQString;
            item.Specs = strSpecs;
            item.FabricWidth = iWidth;
            item.RollDiam = iRoll;
            
            item.ReelNum = AppManager.CreateInstance().GetSerialNoAndUpdate(strBatchNo);
            SNum++;
            item.RndString = "RN" + DateTime.Now.ToString("yyMMddHHmmssfff") + rd.Next(100, 999).ToString();

            CurrentFabric = item;

            Dictionary<string, string> dictData = GetEntityPropertyToDict(item);

            if (!SaveFabricCloth(item))
            {
                MessageBox.Show("保存失败，请检查后重新保存！");
                return;
            }
            else
            {
            }
            OnLaunchItem item1 = new OnLaunchItem();
            item1.Id = item.ReelNum;
            item1.BatchNo = strBatchNo;
            item1.ColorNum = strColorNum;
            item1.ProduceNum = fProduceNum;
            item1.QualityName = strQualityName;
            item1.QualityString = strQString;
            item1.Specs = strSpecs;
            ProduceList.Insert(0, item1);
            dgvShow.DataSource = new BindingList<OnLaunchItem>(ProduceList);
            dgvShow.Refresh();

            string lblFile = Application.StartupPath + "\\Templates\\" + AppManager.CreateInstance().LabelName;
            if(File.Exists(lblFile))
            {
                PrintHelper.CreateInstance().BarPrintInit(lblFile,AppManager.CreateInstance().PrinterName,dictData,AppManager.CreateInstance().PrintCopies);
            }

            try
            {
                NetworkStream ntwStream = DeviceManager.CreateInstance().ClothClient.GetStream();
                if(ntwStream == null || !ntwStream.CanWrite)
                {
                    DataManager.CreateInstance().NeedSend.Enqueue(item);
                }
                if (ntwStream.CanWrite)
                {
                    string strData = JSONUtil.SerializeJSON(item);
                    byte[] buff = Encoding.UTF8.GetBytes(strData);
                    if (buff != null)
                    {
                        ntwStream.Write(buff, 0, buff.Length);
                    }
                }
            }
            catch(Exception ex)
            {
                DataManager.CreateInstance().NeedSend.Enqueue(item);

                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool SaveFabricCloth(FabricClothItem item)
        {
            //保存
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var result = db.InsertRow(item);
                    if (result > 0)
                    {
                        Console.WriteLine("添加成功");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("添加失败");
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        private ProductPlan GetPlan(string strBatchno)
        {
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var lPlan = db.Select<ProductPlan>(u => u.BatchNo == strBatchno && u.IsFinish != 1);
                    if (lPlan != null && lPlan.Count > 0)
                    {
                        Console.WriteLine("添加成功");
                        return lPlan[0];
                    }
                    else
                    {
                        Console.WriteLine("添加失败");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        private SetConfig GetSetConfig(string strKey)
        {
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var lConfigs = db.Select<SetConfig>(u => u.KeyName == strKey);
                    if (lConfigs != null && lConfigs.Count > 0)
                    {
                        Console.WriteLine("添加成功");
                        return lConfigs[0];
                    }
                    else
                    {
                        Console.WriteLine("添加失败");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        private void SetConfigSave(SetConfig config)
        {
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var rtnBool = db.Insert(config);
                    if (rtnBool)
                    {
                        Console.WriteLine("添加成功");
                    }
                    else
                    {
                        Console.WriteLine("添加失败");
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void UpdateConfigSave(SetConfig config)
        {
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var result1 = db.Update("update setconfigs set KeyValue=@KeyValue where KeyName=@KeyName", config);
                    if (result1)
                    {
                        Console.WriteLine("更新成功");
                    }
                    else
                    {
                        Console.WriteLine("更新失败");
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            AppManager.CreateInstance().Init();

            txtServerIp.Text = AppManager.CreateInstance().ServerIp;
            txtServerPort.Text = AppManager.CreateInstance().Port.ToString();
            try
            {
                DeviceManager.CreateInstance().ClothClient = new TcpClient();
                DeviceManager.CreateInstance().ClothClient.Connect(IPAddress.Parse(AppManager.CreateInstance().ServerIp), AppManager.CreateInstance().Port);
            }
            catch(Exception ex)
            {
                MessageBox.Show("连接服务端失败，请联系管理员！");
                //Application.Exit();
                return;
            }

            thData = new Thread(GetServerData);
            thData.IsBackground = true;
            thData.Name = "thData";
            thData.Start();
        }

        private void GetServerData()
        {
            while(true)
            {
                try
                {
                    if(DeviceManager.CreateInstance().ClothClient != null && DeviceManager.CreateInstance().ClothClient.Connected)
                    {
                        NetworkStream ntwStream = DeviceManager.CreateInstance().ClothClient.GetStream();
                        if(ntwStream.CanRead)
                        {
                            StreamReader strmReader = new StreamReader(ntwStream);
                            string strData = strmReader.ReadToEnd();
                            Console.WriteLine(strData);
                        }
                        Thread.Sleep(100);
                    }
                    else
                    {
                        DeviceManager.CreateInstance().ClothClient.Connect(IPAddress.Parse(AppManager.CreateInstance().ServerIp), AppManager.CreateInstance().Port);
                        Thread.Sleep(2000);
                    }
                }
                catch
                {
                }
            }
        }

        private void btnGetLenth_Click(object sender, EventArgs e)
        {

        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string strBN = txtBatchNo.Text.Trim();
                var planItem = GetPlan(strBN);
                if(planItem == null)
                {
                    MessageBox.Show("未查询到当前生产批次计划！");
                }
                else
                {
                    CurrentPlan = planItem;
                    //设置当前批次
                    txtColorNum.Text = planItem.ColorNum;
                    txtSpecs.Text = planItem.Specs;
                    txtRollDiam.Text = planItem.RollDiam.ToString();
                    txtQuatilyString.Text = planItem.QualityString;
                    txtCWidth.Text = planItem.FabricWidth.ToString();
                }
            }
        }

        private void mainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if(DeviceManager.CreateInstance().ClothClient!= null && DeviceManager.CreateInstance().ClothClient.Connected)
                {
                    DeviceManager.CreateInstance().ClothClient.Close();
                }
            }
            catch
            {
            }
            try
            {
                PrintHelper.CreateInstance().UnInit();
            }
            catch
            { }
        }

        public static Dictionary<string, string> GetEntityPropertyToDict<T>(T tEntity)
        {
            Dictionary<string, string> dictClass = new Dictionary<string, string>();
            Type ty = tEntity.GetType();//获取对象类型
            PropertyInfo[] infos = ty.GetProperties();
            foreach (PropertyInfo item in infos)
            {
                try
                {
                    string pName = item.Name;//获取属性名称
                    string pValue = string.Empty;
                    var itVal = item.GetValue(tEntity, null);
                    if (itVal != null)
                    {
                        pValue = itVal.ToString();
                    }
                    dictClass.Add(pName, pValue);
                }
                catch
                { }
            }
            return dictClass;
        }

        private void btnServerConfig_Click(object sender, EventArgs e)
        {
            IPAddress iPAddress;
            int sPort = 0;
            if(!IPAddress.TryParse(txtServerIp.Text.Trim(),out iPAddress))
            {
                MessageBox.Show("请设置正确的IP地址！");
                return;
            }
            if(!int.TryParse(txtServerPort.Text.Trim(),out sPort))
            {
                MessageBox.Show("请设置正确的服务端口号！");
                return;
            }
            else
            {
                if(sPort < 1 || sPort > 65535)
                {
                    MessageBox.Show("请设置正确的服务端口号！");
                    return;
                }
            }

            AppManager.CreateInstance().ServerIp = txtServerIp.Text.Trim();
            AppManager.CreateInstance().Port = sPort;

            if (File.Exists("Config/ItemConfig.xml"))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load("Config/ItemConfig.xml");

                    XmlNode vNode = xmlDoc.SelectSingleNode("Configuration/Server"); //
                    if(vNode != null)
                    {
                        vNode.Attributes["ip"].Value = txtServerPort.Text.Trim();
                        vNode.Attributes["port"].Value = sPort.ToString();
                    }
                    xmlDoc.Save("Config/ItemConfig.xml");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("文件不存在!");
            }
        }

        private void rdo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdoBtn = (sender as RadioButton);
            if(rdoBtn != null && rdoBtn.Checked)
            {
                QualityName = rdoBtn.Tag.ToString();
                Console.WriteLine("Current QualityName:" + QualityName);
            }
        }

        private void btnCurrent_Click(object sender, EventArgs e)
        {
            if(CurrentFabric != null)
            {
                txtBN.Text = CurrentFabric.BatchNo;
                txtCN.Text = CurrentFabric.ColorNum;
                txtPN.Text = CurrentFabric.ProduceNum.ToString();
                txtSp.Text = CurrentFabric.Specs;
                txtSerial.Text = CurrentFabric.ReelNum.ToString();
            }
            else
            {
                string strBN = txtBN.Text.Trim();
                if (string.IsNullOrEmpty(strBN))
                {
                    MessageBox.Show("请输入批次号查询！");
                    return;
                }
                try
                {
                    var cfg = AppManager.CreateInstance().GetPoductSerial(strBN);
                    if (cfg != null)
                    {
                        txtSerial.Text = cfg.KeyValue;
                    }
                }
                catch
                { }
            }
        }

        private void btnAddPrint_Click(object sender, EventArgs e)
        {
            FabricClothItem item = new FabricClothItem();
            item.BatchNo = txtBN.Text;
            item.ColorNum = txtCN.Text;
            item.LineNum = AppManager.CreateInstance().LineNum.ToString();
            item.ProduceNum = float.Parse(txtPN.Text);
            //item.QualityName = strQualityName;
            item.Specs = txtSpecs.Text;

            item.ReelNum = int.Parse(txtSerial.Text);

            Dictionary<string, string> dictData = GetEntityPropertyToDict(item);

            string lblFile = Application.StartupPath + "\\Templates\\" + AppManager.CreateInstance().LabelName;
            if (File.Exists(lblFile))
            {
                PrintHelper.CreateInstance().BarPrintInit(lblFile, AppManager.CreateInstance().PrinterName, dictData, AppManager.CreateInstance().PrintCopies);
            }
        }

        private void dgvShow_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if(dgvShow.Rows.Count > 0)
            {
                DataGridViewRow row1 = dgvShow.Rows[0];
                row1.DefaultCellStyle.BackColor = Color.Lime;
            }
        }
    }
}
