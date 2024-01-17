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
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using Yu3zx.ClothLaunch.Models;
using Yu3zx.DapperExtend;
using Yu3zx.Json;
using Yu3zx.Logs;

namespace Yu3zx.ClothLaunch
{
    public partial class mainFrm : Form
    {
        Thread thData = null;
        Random rd = new Random();
        private int SNum = 13;//
        Thread thHeart = null;//心跳包

        System.Windows.Forms.Timer thMesTimer = null;// new System.Windows.Forms.Timer();

        private List<OnLaunchItem> ProduceList = new List<OnLaunchItem>();
        private List<FabricClothItem> OnlineClothItems = new List<FabricClothItem>();
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
            thMesTimer = new System.Windows.Forms.Timer();
            thMesTimer.Interval = 5000;
            thMesTimer.Tick += ThMesTimer_Tick;
        }

        private void ThMesTimer_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("获取MES数据：" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            //return;
            try
            {
                var fabric = SqlDataHelper.GetNoPackFabric("JY0" + AppManager.CreateInstance().LineNum);//
                if (fabric != null)
                {
                    string strGrade = fabric.SGrade.Trim();
                    if (strGrade == "A" || strGrade == "HC" || strGrade == "KC" || strGrade == "SC")
                    {
                        CurrentFabirc = fabric;
                    }
                    else
                    {
                        try
                        {
                            SqlDataHelper.HSFabricUpdate(fabric.SFabricNo);//更新
                        }
                        catch(Exception ex)
                        {
                            Log.Instance.LogWrite("L89:" + ex.Message);
                        }
                        return;
                    }

                    string strBatchNo = fabric.SCardNo;// txtBatchNo.Text.Trim();// DateTime.Now.ToString("yyyyMMddfff");
                    string strColorNum = fabric.SColorNo;// txtColorNum.Text;
                    float fProduceNum = (float)fabric.NLength;// float.Parse(txtProduceNum.Text);
                    string strQualityName = strGrade;
                    string strSpecs = fabric.SProductWidthOrder;// txtSpecs.Text;//SYarnInfo
                    string strQString = fabric.SMaterialName;

                    float iWidth = 0;
                        
                    if (!float.TryParse(fabric.SFabricWidth,out iWidth))
                    {
                        return;
                    }
                    int iRoll = fabric.NClothRollDiameter;// int.Parse(fabric.);///txtRollDiam.Text

                    if (DeviceManager.CreateInstance().ClothClient != null && DeviceManager.CreateInstance().ClothClient.Connected)
                    {
                    }
                    else
                    {
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
                    item.FabricWidth = (int)(iWidth * 10);
                    item.RollDiam = iRoll;
                    item.ReelNum = fabric.IManualOrderNo;//卷号

                    item.RndString = "RN" + DateTime.Now.ToString("yyMMddHHmmssfff") + rd.Next(100, 999).ToString();

                    OnlineClothItems.Add(item);//临时

                    Dictionary<string, string> dictData = GetEntityPropertyToDict(item);

                    if (!SaveFabricCloth(item))
                    {
                        Log.Instance.LogWrite("保存失败，请检查后重新保存！");
                        Log.Instance.LogWrite("批次：" + strBatchNo);
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

                    Task.Run(() => {
                        this.Invoke((EventHandler)delegate {

                            ProduceList.Insert(0, item1);
                            dgvShow.DataSource = new BindingList<OnLaunchItem>(ProduceList);
                            dgvShow.Refresh();

                            string lblFile = Application.StartupPath + "\\Templates\\" + AppManager.CreateInstance().LabelName;
                            if (File.Exists(lblFile))
                            {
                                PrintHelper.CreateInstance().BarPrintInit(lblFile, AppManager.CreateInstance().PrinterName, dictData, AppManager.CreateInstance().PrintCopies);
                            }
                        });
                    });

                    try
                    {
                        NetworkStream ntwStream = DeviceManager.CreateInstance().ClothClient.GetStream();
                        if (ntwStream == null || !ntwStream.CanWrite)
                        {
                            DataManager.CreateInstance().NeedSend.Add(item);
                            return;
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

                        SqlDataHelper.HSFabricUpdate(fabric.SFabricNo);//更新
                    }
                    catch (Exception ex)
                    {
                        Logs.Log.Instance.LogWrite("网络异常：" + ex.Message);
                        DataManager.CreateInstance().NeedSend.Add(item);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Instance.LogWrite(ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProduceNum.Text))
            {
                txtProduceNum.Focus();
                return;
            }

            string strBatchNo = txtBatchNo.Text.Trim();// DateTime.Now.ToString("yyyyMMddfff");
            string strColorNum = txtColorNum.Text;
            if(string.IsNullOrEmpty(strColorNum))
            {
                strColorNum = "0";
            }
            float fProduceNum = float.Parse(txtProduceNum.Text);
            string strQualityName = QualityName;
            string strSpecs = txtSpecs.Text;//
            string strQString = txtQuatilyString.Text.Trim();

            int iWidth = 0;
            int iRoll = 0;

            if (!int.TryParse(txtCWidth.Text,out iWidth))
            {
                MessageBox.Show("宽度错误！");
                return;
            }
            if (!int.TryParse(txtRollDiam.Text,out iRoll))
            {
                MessageBox.Show("卷径错误！");
                return;
            }

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
            
            if (CurrentFabirc != null)
            {
                item.ReelNum = CurrentFabirc.IManualOrderNo;
            }
            else
            {
                item.ReelNum = AppManager.CreateInstance().GetSerialNoAndUpdate(strBatchNo);
                SNum++;
            }

            item.RndString = "RN" + DateTime.Now.ToString("yyMMddHHmmssfff") + rd.Next(100, 999).ToString();

            OnlineClothItems.Add(item);//临时

            if (CurrentFabric != null)
            {
                if (CurrentFabric.ReelNum == item.ReelNum && CurrentFabric.BatchNo == item.BatchNo)
                {
                    item.RndString = CurrentFabric.RndString;
                }
            }

            CurrentFabric = item;

            Dictionary<string, string> dictData = GetEntityPropertyToDict(item);

            if (!SaveFabricCloth(item))
            {
                MessageBox.Show("保存失败，请检查后重新保存！");
                Logs.Log.Instance.LogWrite("保存失败，请检查后重新保存！");
                Logs.Log.Instance.LogWrite("批次：" + strBatchNo);
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
                    DataManager.CreateInstance().NeedSend.Add(item);
                    return;
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
                if (CurrentFabirc != null)
                {
                    SqlDataHelper.HSFabricUpdate(CurrentFabirc.SFabricNo);//更新
                }
            }
            catch(Exception ex)
            {
                Logs.Log.Instance.LogWrite("网络异常：" + ex.Message);
                DataManager.CreateInstance().NeedSend.Add(item);
                Console.WriteLine(ex.Message);
            }

            txtProduceNum.Text = "50";
            rdoA.Checked = true;
        }

        private void FabricUpdate()
        {
            try 
            {
                if (CurrentFabirc != null)
                {
                    SqlDataHelper.HSFabricUpdate(CurrentFabirc.SFabricNo);
                }
            }
            catch(Exception ex) 
            {
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
                        Logs.Log.Instance.LogWrite("L199,添加失败！");
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    Logs.Log.Instance.LogWrite("L199:" + ex.Message + ex.StackTrace);
                    return false;
                }
            }
        }

        private bool UpdateFabricCloth(FabricClothItem item)
        {
            //updateFabricCloth.ProduceNum = float.Parse(txtELen.Text);
            //updateFabricCloth.QualityString = txtEQString.Text;
            //updateFabricCloth.ColorNum = txtEColorNum.Text;
            //updateFabricCloth.Specs = txtESpecs.Text;
            //updateFabricCloth.FabricWidth = int.Parse(txtECWidth.Text);
            //updateFabricCloth.RollDiam = int.Parse(txtERollDiam.Text);
            //updateFabricCloth.QualityName = cboQualityName.SelectedItem.ToString();
            //保存
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var result = db.Update("update fabric_cloths set QualityName=@QualityName,QualityString=@QualityString,ColorNum=@ColorNum,Specs=@Specs,ProduceNum=@ProduceNum,FabricWidth=@FabricWidth,RollDiam=@RollDiam,AddTime=@AddTime where RndString=@RndString", new { QualityName = item.QualityName, QualityString = item.QualityString, ColorNum = item.ColorNum, Specs = item.Specs, ProduceNum = item.ProduceNum, FabricWidth = item.FabricWidth, RollDiam = item.RollDiam, AddTime = DateTime.Now, RndString = item.RndString });
                    if (result)
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
                catch (Exception ex)
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
                        Logs.Log.Instance.LogWrite("ProductPlan查询成功：" + lPlan.Count.ToString());
                        return lPlan[0];
                    }
                    else
                    {
                        Logs.Log.Instance.LogWrite("ProductPlan查询失败：");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Logs.Log.Instance.LogWrite("ProductPlan查询异常" + ex.Message);
                    Logs.Log.Instance.LogWrite("ProductPlan查询异常" + ex.StackTrace);
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
                //MessageBox.Show("连接服务端失败，请联系管理员！");
                //Application.Exit();
                return;
            }

            thData = new Thread(GetServerData);
            thData.IsBackground = true;
            thData.Name = "thData";
            thData.Start();

            thHeart = new Thread(HeartSend);
            thHeart.IsBackground = true;
            thHeart.Name = "thHeart";
            thHeart.Start();
        }

        private void GetServerData()
        {
            while (true)
            {
                try
                {
                    if (DeviceManager.CreateInstance().ClothClient != null && DeviceManager.CreateInstance().ClothClient.Connected)
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
                        //有心跳包去连接
                        //DeviceManager.CreateInstance().ClothClient.Connect(IPAddress.Parse(AppManager.CreateInstance().ServerIp), AppManager.CreateInstance().Port);
                        Thread.Sleep(2000);
                    }
                }
                catch
                {
                }
            }
        }

        private void HeartSend()
        {
            Thread.Sleep(5000);
            while (true)
            {
                Thread.Sleep(5000);
                try
                {
                    DeviceManager.CreateInstance().CheckConnected();

                    if (DeviceManager.CreateInstance().ClothClient != null && !DeviceManager.CreateInstance().ClothClient.Connected)
                    {
                        DeviceManager.CreateInstance().ClothClient.Connect(IPAddress.Parse(AppManager.CreateInstance().ServerIp), AppManager.CreateInstance().Port);//重新连接
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
            if(CurrentFabirc != null)
            {
                SqlDataHelper.HSFabricUpdate(CurrentFabirc.SFabricNo);
            }
        }

        private void btnBatchInfo_Click(object sender, EventArgs e)
        {
            string strBN = txtBatchNo.Text.Trim();
            var planItem = GetPlan(strBN);
            if (planItem == null)
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
                DataManager.CreateInstance().Save();
            }
            catch
            { }
            try
            {
                PrintHelper.CreateInstance().UnInit();
            }
            catch
            { }

            if (thData != null)
            {
                thData.Abort();
                thData = null;
            }

            if (thHeart != null)
            {
                thHeart.Abort();
                thHeart = null;
            }
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                NetworkStream ntwStream = DeviceManager.CreateInstance().ClothClient.GetStream();
                if (ntwStream.CanWrite)
                {
                    byte[] buff = new byte[] { };
                    ntwStream.Write(buff, 0, buff.Length);
                }

                using (var s = DeviceManager.CreateInstance().ClothClient?.GetStream())
                {
                    var buff1 = new byte[512];
                    if (s.DataAvailable)
                    { 
                        //判断有数据再读，否则Read会阻塞线程。后面的业务逻辑无法处理
                        var len = s.Read(buff1, 0, buff1.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                if(!DeviceManager.CreateInstance().ClothClient.Connected)
                {
                    try
                    {
                        DeviceManager.CreateInstance().ClothClient = new TcpClient();
                        DeviceManager.CreateInstance().ClothClient.Connect(IPAddress.Parse(AppManager.CreateInstance().ServerIp), AppManager.CreateInstance().Port);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("连接服务端失败，重连或者请联系管理员！");
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("连接服务端失败，重连或者请联系管理员！");
            }
        }

        private void btnOnLine_Click(object sender, EventArgs e)
        {
            try
            {
                NetworkStream ntwStream = DeviceManager.CreateInstance().ClothClient.GetStream();
                if (ntwStream == null || !ntwStream.CanWrite)
                {
                    MessageBox.Show("连接服务端失败，重连或者请联系管理员！");
                    return;
                }
                while (DataManager.CreateInstance().NeedSend.Count > 0)
                {
                    lock(DataManager.CreateInstance())
                    {
                        try
                        {
                            var nItem = DataManager.CreateInstance().NeedSend[0];
                            if (ntwStream.CanWrite)
                            {
                                string strData = JSONUtil.SerializeJSON(nItem);
                                byte[] buff = Encoding.UTF8.GetBytes(strData);
                                if (buff != null)
                                {
                                    ntwStream.Write(buff, 0, buff.Length);
                                }
                                DataManager.CreateInstance().NeedSend.RemoveAt(0);
                            }
                            Thread.Sleep(50);
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
            }
            catch
            { }
        }

        private void btnGetOnlineData_Click(object sender, EventArgs e)
        {
            try
            {
                string strBatchNo = txtEBatchNo.Text.Trim();
                int iReelNum = int.Parse(txtEReelNum.Text.Trim());
                var item = OnlineClothItems.Find(x => x.BatchNo == strBatchNo && iReelNum == x.ReelNum);
                if (item != null)
                {
                    txtELen.Text = item.ProduceNum.ToString();
                    txtEQString.Text = item.QualityString.ToString();//品名
                    txtEColorNum.Text = item.ColorNum.ToString();
                    txtESpecs.Text = item.Specs.ToString();
                    txtECWidth.Text = item.FabricWidth.ToString();//
                    txtERollDiam.Text = item.RollDiam.ToString();
                    cboQualityName.SelectedItem = item.QualityName;
                    updateFabricCloth = item;
                }
                else
                {
                    txtELen.Text = "";// item.ProduceNum.ToString();
                    txtEQString.Text = "";// item.QualityString.ToString();
                    txtEColorNum.Text = "";// item.ColorNum.ToString();
                    txtESpecs.Text = "";//item.Specs.ToString();
                    txtECWidth.Text = "";//item.FabricWidth.ToString();//
                    txtERollDiam.Text = "";//item.RollDiam.ToString();
                    updateFabricCloth = null;
                }
            }
            catch
            {

            }
        }

        private FabricClothItem updateFabricCloth = null;

        private void btnEditData_Click(object sender, EventArgs e)
        {
            if(updateFabricCloth == null)
            {
                //未查到本地上线服务，请到服务器平台更新
                MessageBox.Show("本地未查到此卷布料，请在服务器上查找！");
                return;            
            }
            else
            {
                try
                {
                    updateFabricCloth.ProduceNum = float.Parse(txtELen.Text);
                    updateFabricCloth.QualityString = txtEQString.Text;
                    updateFabricCloth.ColorNum = txtEColorNum.Text;
                    updateFabricCloth.Specs = txtESpecs.Text;
                    updateFabricCloth.FabricWidth = int.Parse(txtECWidth.Text);
                    updateFabricCloth.RollDiam = int.Parse(txtERollDiam.Text);
                    updateFabricCloth.QualityName = cboQualityName.SelectedItem.ToString();

                    Dictionary<string, string> dictData = GetEntityPropertyToDict(updateFabricCloth);

                    if (UpdateFabricCloth(updateFabricCloth))
                    {
                        Logs.Log.Instance.LogWrite("L736:修改更新数据成功！");
                    }
                    else
                    {
                        Logs.Log.Instance.LogWrite("L740:修改更新数据失败！");
                    }

                    try
                    {
                        NetworkStream ntwStream = DeviceManager.CreateInstance().ClothClient.GetStream();
                        if (ntwStream == null || !ntwStream.CanWrite)
                        {
                            DataManager.CreateInstance().NeedSend.Add(updateFabricCloth);
                            return;
                        }

                        if (ntwStream.CanWrite)
                        {
                            string strData = JSONUtil.SerializeJSON(updateFabricCloth);
                            byte[] buff = Encoding.UTF8.GetBytes(strData);
                            if (buff != null)
                            {
                                ntwStream.Write(buff, 0, buff.Length);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DataManager.CreateInstance().NeedSend.Add(updateFabricCloth);
                        Console.WriteLine(ex.Message);
                        Logs.Log.Instance.LogWrite("L766:" + ex.Message);
                    }

                    string lblFile = Application.StartupPath + "\\Templates\\" + AppManager.CreateInstance().LabelName;
                    if (File.Exists(lblFile))
                    {
                        PrintHelper.CreateInstance().BarPrintInit(lblFile, AppManager.CreateInstance().PrinterName, dictData, AppManager.CreateInstance().PrintCopies);
                    }
                    //更新显示
                    var pItem = ProduceList.Find(x => x.BatchNo == updateFabricCloth.BatchNo && x.Id == updateFabricCloth.ReelNum);
                    if (pItem != null)
                    {
                        pItem.Specs = updateFabricCloth.Specs;
                        pItem.ProduceNum = updateFabricCloth.ProduceNum;
                        pItem.ColorNum = updateFabricCloth.ColorNum;
                        pItem.QualityName = updateFabricCloth.QualityName;
                        pItem.QualityString = updateFabricCloth.QualityString;
                    }

                    updateFabricCloth = null;

                    MessageBox.Show("已经修改，请检查是否已经包装，已经包装的需要拆开重新包装！");
                }
                catch
                {
                    MessageBox.Show("修改失败，请重试！");
                }
            }
        }

        private void btnSetBegin_Click(object sender, EventArgs e)
        {
            string strBatchNum = txtEditBatchNo.Text.Trim();
            int iReel = int.Parse(txtBeginReelNum.Text.Trim());
            if (string.IsNullOrEmpty(strBatchNum) || iReel < 0)
            {
                MessageBox.Show("请输入正确的批次和卷号！");
            }
            else
            {
                if(AppManager.CreateInstance().SetBeginSerialNo(strBatchNum, iReel))
                {
                    MessageBox.Show(string.Format("批号: {0}设置开始卷号{1}成功！",strBatchNum,iReel));
                }
            }
        }

        private HSFabric CurrentFabirc
        {
            get;
            set;
        }

        private void btnMesData_Click(object sender, EventArgs e)
        {
            try
            {
                var item = SqlDataHelper.GetNoPackFabric("JY0" + AppManager.CreateInstance().LineNum);
                if (item != null)
                {
                    txtBatchNo.Text = item.SCardNo;
                    txtProduceNum.Text = string.Format("{0}", item.NLength);
                    txtQuatilyString.Text = item.SMaterialName;
                    txtColorNum.Text = item.SColorNo;
                    txtSpecs.Text = item.SProductWidthOrder;
                    float fFabLen = 0;
                    try
                    {
                        float.TryParse(item.SFabricWidth, out fFabLen);
                    }
                    catch
                    { }

                    txtCWidth.Text = ((int)(fFabLen * 10)).ToString();
                    txtRollDiam.Text = item.NClothRollDiameter.ToString();

                    string strGrade = item.SGrade;
                    txtGrade.Text = strGrade;
                    if (strGrade.StartsWith("A"))
                    {
                        rdoA.Checked = true;
                    }
                    else if (strGrade.StartsWith("HC"))
                    {
                        rdoHC.Checked = true;
                    }
                    else if (strGrade.StartsWith("KC"))
                    {
                        rdoKC.Checked = true;
                    }
                    else if (strGrade.StartsWith("SC"))
                    {
                        rdoSC.Checked = true;
                    }

                    CurrentFabirc = item;
                }
            }
            catch (Exception ex)
            {

            }

            //try
            //{
            //    var itemList = SqlDataHelper.GetFabricList("JY0" + AppManager.CreateInstance().LineNum);
            //    if (itemList != null && itemList.Count > 0)
            //    {
            //        foreach(var item in itemList) 
            //        {
            //            txtSql.Text += string.Format("Fabric:{0},{1},{2},{3}\r\n", item.SCardNo,item.SFabricNo,item.IManualOrderNo,item.NLength);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    txtSql.Text += ex.StackTrace;
            //}

        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            if(btnServer.Text == "启动服务")
            {
                if(thMesTimer != null)
                {
                    thMesTimer.Stop();
                }
                thMesTimer.Start();
                btnServer.Text = "停止服务";
            }
            else
            {
                thMesTimer.Stop();
                btnServer.Text = "启动服务";
            }
        }

        private void btnAllOnline_Click(object sender, EventArgs e)
        {
            SqlDataHelper.HSFabricAllUpdate("JY0" + AppManager.CreateInstance().LineNum);
        }
    }
}
