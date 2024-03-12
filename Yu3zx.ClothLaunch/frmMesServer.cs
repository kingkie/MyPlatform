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
using Yu3zx.ClothLaunch.Models;
using Yu3zx.DapperExtend;
using Yu3zx.Json;

namespace Yu3zx.ClothLaunch
{
    public partial class frmMesServer : Form
    {
        Random rd = new Random();
        Thread thData = null;
        Thread thHeart = null;//心跳包

        private List<OnLaunchItem> ProduceList = new List<OnLaunchItem>();
        private List<FabricClothItem> OnlineClothItems = new List<FabricClothItem>();
        List<string> NeedGoLives = new List<string>();

        public frmMesServer()
        {
            InitializeComponent();
        }

        private HSFabric CurrentFabirc
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


        private void frmMesServer_Load(object sender, EventArgs e)
        {
            lblIfo.Text = "";
            AppManager.CreateInstance().Init();
            string[] strs = AppManager.CreateInstance().NeedGoLive.Split(new char[]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
            if(strs != null && strs.Length > 0)
            {
                NeedGoLives.AddRange(strs);
            }

            try
            {
                DeviceManager.CreateInstance().ClothClient = new TcpClient();
                DeviceManager.CreateInstance().ClothClient.Connect(IPAddress.Parse(AppManager.CreateInstance().ServerIp), AppManager.CreateInstance().Port);
            }
            catch(Exception ex)
            {
                lblIfo.Text = "连接服务端失败，请联系管理员！";
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

        private void btnMesData_Click(object sender, EventArgs e)
        {
            try
            {
                InitView();
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
                    txtReelNum.Text = item.IManualOrderNo.ToString();

                    string strGrade = item.SGrade;
                    txtGrade.Text = strGrade;

                    CurrentFabirc = item;
                }
                else
                {
                    CurrentFabirc = null;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void InitView()
        {
            txtReelNum.Text = string.Empty;
            txtGrade.Text = string.Empty;
            lblIfo.Text = string.Empty;
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
                        if (ntwStream.CanRead)
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
                catch (Exception ex)
                {
                    Logs.Log.Instance.LogWrite("L199:" + ex.Message + ex.StackTrace);
                    return false;
                }
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


        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProduceNum.Text))
            {
                txtProduceNum.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtReelNum.Text))
            {
                txtProduceNum.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtGrade.Text))
            {
                txtProduceNum.Focus();
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

            string strBatchNo = txtBatchNo.Text.Trim();// DateTime.Now.ToString("yyyyMMddfff");
            string strColorNum = txtColorNum.Text;
            if (string.IsNullOrEmpty(strColorNum))
            {
                strColorNum = "0";
            }
            float fProduceNum = float.Parse(txtProduceNum.Text);
            string strQualityName = txtGrade.Text.Trim();
            string strSpecs = txtSpecs.Text;//
            string strQString = txtQuatilyString.Text.Trim();

            int iWidth = 0;
            int iRoll = 0;

            if (!int.TryParse(txtCWidth.Text, out iWidth))
            {
                MessageBox.Show("宽度错误！");
                return;
            }
            if (!int.TryParse(txtRollDiam.Text, out iRoll))
            {
                MessageBox.Show("卷径错误！");
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
            }

            item.RndString = "RN" + DateTime.Now.ToString("yyMMddHHmmssfff") + rd.Next(100, 999).ToString();

            if (CurrentFabric != null)
            {
                if (CurrentFabric.ReelNum == item.ReelNum && CurrentFabric.BatchNo == item.BatchNo)
                {
                    item.RndString = CurrentFabric.RndString;
                }
            }

            CurrentFabric = item;

            if (!NeedGoLives.Contains(strQualityName.ToUpper()))
            {
                if (CurrentFabirc != null)
                {
                    bool bUp = SqlDataHelper.HSFabricUpdate(CurrentFabirc.SFabricNo);//更新
                    if (bUp)
                    {
                        //btnMesData_Click(sender, e);
                    }
                }
                txtProduceNum.Text = "50";
                return;
            }

            //if (strQualityName.ToUpper() == "HC" || strQualityName.ToUpper().Trim() == "零次" || item.ReelNum == 0)
            //{
            //    if (CurrentFabirc != null)
            //    {
            //        bool bUp = SqlDataHelper.HSFabricUpdate(CurrentFabirc.SFabricNo);//更新
            //        if (bUp)
            //        {
            //            //btnMesData_Click(sender, e);
            //        }
            //    }
            //    txtProduceNum.Text = "50";
            //    return;
            //}

            OnlineClothItems.Add(item);//临时

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

            string lblFile = Application.StartupPath + "\\Templates\\" + AppManager.CreateInstance().LabelName;
            if (File.Exists(lblFile))
            {
                PrintHelper.CreateInstance().BarPrintInit(lblFile, AppManager.CreateInstance().PrinterName, dictData, AppManager.CreateInstance().PrintCopies);
            }

            try
            {
                //上线到服务端
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
                if (CurrentFabirc != null)
                {
                    bool bUpdate = SqlDataHelper.HSFabricUpdate(CurrentFabirc.SFabricNo);//更新
                    if (!bUpdate)
                    {
                        lblIfo.Text = "数据未更新成功，请按更新按钮更新";
                    }
                    else
                    {
                        btnMesData_Click(sender, e);
                    }

                    ProduceList.Insert(0, item1);
                    dgvShow.DataSource = new BindingList<OnLaunchItem>(ProduceList);
                    dgvShow.Refresh();
                }
            }
            catch (Exception ex)
            {
                Logs.Log.Instance.LogWrite("网络异常：" + ex.Message);
                DataManager.CreateInstance().NeedSend.Add(item);
                Console.WriteLine(ex.Message);
            }

            txtProduceNum.Text = "50";
            InitView();
        }

        private void btnGetLenth_Click(object sender, EventArgs e)
        {
            if (CurrentFabirc != null)
            {
                bool bUpdate = SqlDataHelper.HSFabricUpdate(CurrentFabirc.SFabricNo);
                if (!bUpdate)
                {
                    lblIfo.Text = "数据未更新成功，请按更新按钮重试！";
                }
            }
        }
    }
}
