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
using Yu3zx.Json;

namespace Yu3zx.ClothLaunch
{
    public partial class mainFrm : Form
    {
        Thread thData = null;
        Random rd = new Random();
        private int SNum = 13;//
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

        public mainFrm()
        {
            InitializeComponent();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            string strBatchNo = DateTime.Now.ToString("yyyyMMddfff");
            string strColorNum = "197";
            float fProduceNum = 49 + rd.Next(1, 20) / 10f;
            string strQualityName = "A";
            string strSpecs = "137";//

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
            item.Specs = strSpecs;

            item.ReelNum = AppManager.CreateInstance().GetSerialNoAndUpdate();
            SNum++;
            item.RndString = "RN" + DateTime.Now.ToString("yyMMddHHmmssfff") + rd.Next(100, 999).ToString();

            CurrentFabric = item;

            Dictionary<string, string> dictData = GetEntityPropertyToDict(item);


            if (!SaveFabricCloth(item))
            {
                MessageBox.Show("保存失败，请检查后重新保存！");
                return;
            }

            try
            {
                NetworkStream ntwStream = DeviceManager.CreateInstance().ClothClient.GetStream();
                if (ntwStream.CanWrite)
                {
                    string strData = JSONUtil.SerializeJSON(item);
                    byte[] buff = Encoding.UTF8.GetBytes(strData);
                    if (buff != null)
                    {
                        ntwStream.Write(buff, 0, buff.Length);
                    }
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
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

        private void mainFrm_Load(object sender, EventArgs e)
        {
            //SetConfig config = new SetConfig();
            //config.KeyName = "ProductSerialNo";
            //config.KeyValue = "1";

            //SetConfigSave(config);
            //UpdateConfigSave(config);

            //SetConfig config = GetSetConfig("ProductSerialNo");

            AppManager.CreateInstance().Init();
            DeviceManager.CreateInstance().ClothClient = new TcpClient();
            DeviceManager.CreateInstance().ClothClient.Connect(IPAddress.Parse(AppManager.CreateInstance().ServerIp), AppManager.CreateInstance().Port);

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
        }

        public static Dictionary<string, string> GetEntityPropertyToDict<T>(T tEntity)
        {
            Dictionary<string, string> dictClass = new Dictionary<string, string>();
            Type ty = tEntity.GetType();//获取对象类型
            PropertyInfo[] infos = ty.GetProperties();
            foreach (PropertyInfo item in infos)
            {
                string pName = item.Name;//获取属性名称
                string pValue = item.GetValue(tEntity, null).ToString();
                dictClass.Add(pName, pValue);
            }
            return dictClass;
        }
    }
}
