﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Yu3zx.DapperExtend;
using Yu3zx.Logs;

namespace Yu3zx.TaggingSevice
{
    public partial class mainFrm : Form
    {
        private Thread thPlc = null;//设备通信轮询
        TcpServer tcpServer = null;//接收客户端
        private Thread thWorkFlow = null; //流程引擎
        private List<FabricClothItem> lUnSave = new List<FabricClothItem>();

        PlcConnector PlcConn = new PlcConnector();

        Random rd = new Random();
        /// <summary>
        /// 
        /// </summary>
        private ConcurrentQueue<PlcCmd> PlcReceive = new ConcurrentQueue<PlcCmd>();

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

            // 192.168.0.201 502
            //IPAddress iPAddress = IPAddress.Parse("192.168.0.201");

            //TcpClient tcpClient = new TcpClient();
            //tcpClient.Connect(iPAddress, 502);
            //if(tcpClient.Connected)
            //{
            //    PlcConn.Client = tcpClient;
            //    PlcConn.ClientKey = "192.168.0.201";
            //    PlcConn.BeginReceive();
            //}

            //-=-=-=-=-=-=-=-获取配置文件-=-=-=-=-=-=-=-
            //InitPlc();

        }

        private void InitPlc()
        {
            PlcConn.Rack = 0;
            PlcConn.Slot = 1;
            PlcConn.ServerIp = "192.168.0.201";
            PlcConn.Port = 502;
            try
            {
                PlcConn.S7Connet();
            }
            catch
            { }
            swtPlc.Checked = PlcConn.S7Connected;
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

                if(thWorkFlow != null)
                {
                    thWorkFlow.Abort();
                    thWorkFlow = null;
                }

                if(thPlc != null)
                {
                    thPlc.Abort();
                    thPlc = null;
                }

                tcpServer = new TcpServer(IPAddress.Parse(cboServerIP.Text), int.Parse(txtPort.Text));
                tcpServer.OnClothDataHandle += TcpServer_OnClothDataHandle;
                tcpServer.StartServer();
                //工作流程
                thWorkFlow = new Thread(WorkFlowGoing);
                thWorkFlow.IsBackground = true;
                thWorkFlow.Name = "thWork";
                thWorkFlow.Start();
                //PLC状态查询读取
                thPlc = new Thread(SearchPlcState);
                thPlc.IsBackground = true;
                thPlc.Name = "thPlc";
                thPlc.Start();

                if (tcpServer.IsServer)
                {
                    btnService.Text = "停止服务";
                }
            }
            else
            {
                try
                {
                    if (thWorkFlow != null)
                    {
                        thWorkFlow.Abort();
                        thWorkFlow = null;
                    }

                    if (thPlc != null)
                    {
                        thPlc.Abort();
                        thPlc = null;
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
            try
            {
                if (item != null)
                {
                    string infoStr = DateTime.Now.ToString("[HH:mm:ss]") + "接收到对象：" + Json.JSONUtil.SerializeJSON(item);
                    Console.WriteLine(infoStr);
                    this.Invoke((EventHandler)delegate {
                        this.txtInfo.Text = infoStr;
                    });
                    string lNum = item.LineNum;
                    string strBatchNo = item.BatchNo;
                    //生产线没有或者批次没有就是错误的
                    if (string.IsNullOrEmpty(lNum) || string.IsNullOrEmpty(strBatchNo))
                    {
                        return;
                    }
                    //批次号
                    if(!ProductStateManager.GetInstance().DictOnLine.ContainsKey(strBatchNo))
                    {
                        OnLineCloth onLine = new OnLineCloth();
                        onLine.BatchNo = strBatchNo;
                        ProductStateManager.GetInstance().DictOnLine.Add(strBatchNo, onLine);
                    }
                    //增加已经上线的
                    lock (ProductStateManager.GetInstance().DictOnLine)
                    {
                        ProductStateManager.GetInstance().DictOnLine[strBatchNo].ClothItems.Add(item);
                        if (item.QualityName == "A") //需要包装的
                        {
                            ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum = ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum + 1;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Instance.LogWrite(ex);
            }
        }

        private WorkState CurrentState = WorkState.None;
        private void WorkFlowGoing()
        {
            while(true)
            {
                try
                {
                    PlcCmd plcCmd;
                    if(PlcReceive.Count > 0)
                    {
                        for(int i = 0;i < PlcReceive.Count;i++)
                        {
                            if(PlcReceive.TryDequeue(out plcCmd))
                            {
                                switch (plcCmd.CmdCode)
                                {
                                    case 0x02:
                                        //获取当前需要打印的
                                        FabricClothItem item = null;
                                        PrintFabricLabel(item);
                                        break;
                                    case 0x03:
                                        //获取打印列表
                                        int iPfl = 0;
                                        if(plcCmd.DataSegment.Count >=2)
                                        {
                                            iPfl = plcCmd.DataSegment[0] * 256 + plcCmd.DataSegment[1];
                                        }
                                        else if (plcCmd.DataSegment.Count == 1)
                                        {
                                            iPfl = plcCmd.DataSegment[0];
                                        }
                                        if(iPfl > 0)
                                        {
                                            PrintFabricList(iPfl); //打印
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    if (!ProductStateManager.GetInstance().CurrentDoing)//是否是当前上线
                    {
                        string strBatchNum = ProductStateManager.GetInstance().GetOnLineList();
                        if (string.IsNullOrEmpty(strBatchNum))
                        {
                            //未能达到上线的条件
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            //开始上线
                            WorkFlowManager.CreateInstance().CurrentLine = ProductStateManager.GetInstance().DictOnLine[strBatchNum].ClothItems[0].LineNum;
                            WorkFlowManager.CreateInstance().CurrentBatchNo = strBatchNum;
                            int iSumNeed = 0;//累计需要

                            int iAClass = 0;
                            //计算出需要移出多少个
                            foreach (var iCloth in ProductStateManager.GetInstance().DictOnLine[strBatchNum].ClothItems)
                            {
                                if (iAClass >= AppManager.CreateInstance().PackingNum)
                                {
                                    break;
                                }
                                if (iCloth.QualityName == "A")
                                {
                                    iAClass++;
                                }
                                iSumNeed++;
                            }

                            CartonBox newBox = new CartonBox();
                            newBox.BatchNo = strBatchNum;

                            lock (ProductStateManager.GetInstance().DictOnLine)
                            {
                                while (iSumNeed > 0)
                                {
                                    var iRemove = ProductStateManager.GetInstance().DictOnLine[strBatchNum].ClothItems[0];

                                    newBox.OnLaunchItems.Add(iRemove);
                                    ProductStateManager.GetInstance().DictOnLine[strBatchNum].ClothItems.RemoveAt(0);
                                    iSumNeed--;
                                }
                                ProductStateManager.GetInstance().CurrentDoing = true;
                                ProductStateManager.GetInstance().CurrentBatchNo = strBatchNum;
                                ProductStateManager.GetInstance().CurrentBox = newBox;//当前装箱
                                ProductStateManager.GetInstance().CartonBoxItems.Add(newBox);


                                if (ProductStateManager.GetInstance().CurrentBox != null && ProductStateManager.GetInstance().CurrentBox.OnLaunchItems.Count > 0)
                                {
                                    ProductStateManager.GetInstance().CurrentLine = ProductStateManager.GetInstance().CurrentBox.OnLaunchItems[0].LineNum;//当前线
                                }

                                ProductStateManager.GetInstance().Save();
                            }

                            //通知PLC上线
                            if (ProductStateManager.GetInstance().CurrentDoing)
                            {
                                try
                                {
                                    byte iLNum = byte.Parse(ProductStateManager.GetInstance().CurrentLine);
                                    int fWidth = newBox.OnLaunchItems[0].FabricWidth;
                                    int iRoll = newBox.OnLaunchItems[0].RollDiam;

                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                    else
                    {
                        //空闲就休息2秒
                        Thread.Sleep(2000);
                    }
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// 监控PLC状态
        /// </summary>
        private void SearchPlcState()
        {
            while(true)
            {
                try
                {
                    if(!PlcConn.S7Connected)
                    {
                        PlcConn.S7Connet();
                        Thread.Sleep(500);
                    }

                    bool needRead = PlcConn.ReadFlag(1, 1);//读取是否已经有标志
                    if(needRead)
                    {
                        PlcConn.WriteFlag(1, 1, false);//设置读取完成标志

                        byte[] cmdInput = PlcConn.ReadDataBlock(1, 2, 6);//读取并解析

                        if(cmdInput != null && cmdInput.Length > 1)
                        {
                            int lineNum = cmdInput[1];//反馈的线序
                            switch(cmdInput[0])
                            {
                                case 0x02:
                                    //请求打印标签,根据当前线号打印
                                    PlcCmd cmd = new PlcCmd();
                                    cmd.CmdCode = 0x02;
                                    cmd.MachineId = cmdInput[1];
                                    
                                    PlcReceive.Enqueue(cmd);
                                    break;
                                case 0x03:
                                    //请求打印进仓单
                                    if(cmdInput.Length > 3)
                                    {
                                        int sumBoxs = cmdInput[3];
                                        PlcCmd cmd1 = new PlcCmd();
                                        cmd1.CmdCode = 0x03;
                                        cmd1.MachineId = cmdInput[1];
                                        cmd1.DataSegment.Add(cmdInput[3]);
                                        PlcReceive.Enqueue(cmd1);//
                                    }
                                    break;
                                default:

                                    break;
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
                catch
                { }
            }
        }

        #region 打印小票
        /// <summary>
        /// 面料标签
        /// </summary>
        private void PrintFabricLabel(FabricClothItem item)
        {
            Console.WriteLine("打印面料标签标签成功！");
            string strQC = item.QualityName.ToUpper();
            switch (strQC)
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
                    if (pCfg != null)
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
        /// <param name="packNum">总包数装成垛</param>
        private void PrintFabricList(int packNum)
        {
            //从PLC 获取完成的 线号
            string iComplete = "1";
        }

        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="lPack">装箱个数</param>
        private void PrintPackingList(List<FabricClothItem> lPack)
        {
            Console.WriteLine(lPack.Count + " " + DateTime.Now.ToString("yyyyMMddHHmmss") + "已经打印装箱单！");
            foreach (var item in lPack)
            {
                Console.Write(" " + item.ReelNum.ToString());
            }
            //Console.WriteLine(lPack.Count + "" + DateTime.Now.ToString("yyyyMMddHHmmss") + "已经打印装箱单！");
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

        #endregion End

        #region PLC通信相关

        private void NoticePlc(byte iLNum, Int16 fabricWidth, Int16 iRollDiam)
        {
            List<byte> lCmd = new List<byte>();
            lCmd.Add(0x01);
            lCmd.Add(iLNum);

        }

        #endregion End

        private void btnTest_Click(object sender, EventArgs e)
        {

        }
    }
}
