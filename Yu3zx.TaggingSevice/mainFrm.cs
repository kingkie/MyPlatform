﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;
using FastReport.Data;
using FastReport.Utils;
using Yu3zx.DapperExtend;
using Yu3zx.Logs;

namespace Yu3zx.TaggingSevice
{
    public partial class mainFrm : Form
    {
        private Thread thPlc = null;//设备通信轮询
        TcpServer tcpServer = null;//接收客户端
        private Thread thWorkFlow = null; //流程引擎

        PlcConnector PlcConn = new PlcConnector();

        Random rd = new Random();
        Thread thConn = null;
        /// <summary>
        /// PLC收到的命令
        /// </summary>
        private ConcurrentQueue<PlcCmd> PlcReceive = new ConcurrentQueue<PlcCmd>();

        public mainFrm()
        {
            InitializeComponent();
            // this.ControlBox = false;//设置不出现关闭按钮
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            AppManager.CreateInstance().Init();
            txtPort.Text = AppManager.CreateInstance().Port.ToString();
            List<string> loaclIps = GetLoacalIp();
            cboServerIP.Items.AddRange(loaclIps.ToArray());
            cboServerIP.SelectedItem = AppManager.CreateInstance().ServerIp;

            ProductStateManager.GetInstance().DictInit();//2024-01-10
            //-=-=-=-=-=-=-=-获取配置文件-=-=-=-=-=-=-=-
            InitPlc();
            if(AppManager.CreateInstance().AutoServer)
            {
                btnService_Click(sender, e);
            }

            thConn = new Thread(CheckConn);
            thConn.IsBackground = true;
            thConn.Name = "thConn";
            thConn.Start();
        }

        #region 窗体操作
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnService_Click(object sender, EventArgs e)
        {
            if (btnService.Text == "启动服务")
            {
                if (tcpServer != null)
                {
                    tcpServer.StopServer();
                    tcpServer = null;
                }

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

                tcpServer = new TcpServer(IPAddress.Any, int.Parse(txtPort.Text));//IPAddress.Parse(cboServerIP.Text)
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
                    if (tcpServer != null)
                    {
                        tcpServer.StopServer();
                        tcpServer = null;
                    }
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
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                btnService.Text = "启动服务";
            }
        }
        /// <summary>
        /// 显示主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmBackto_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }
        /// <summary>
        /// 退出服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmQuit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确认退出服务？","提示",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void mainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //最重要的是保存状态
                ProductStateManager.GetInstance().Save();

                if (thConn != null)
                {
                    thConn.Abort();
                    thConn = null;
                }
            }
            catch
            {
            }
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

            try
            {
                PrintHelper.CreateInstance().UnInit();
            }
            catch
            { }
        }

        private void btnStateSave_Click(object sender, EventArgs e)
        {
            try
            {
                //最重要的是保存状态
                ProductStateManager.GetInstance().Save();
                MessageBox.Show("保存状态成功！");
            }
            catch
            {
            }
        }

        private void btnReConn_Click(object sender, EventArgs e)
        {
            InitPlc();
        }

        private void btnStateClear_Click(object sender, EventArgs e)
        {
            try
            {
                //最重要的是保存状态
                ProductStateManager.GetInstance().CartonBoxItems.Clear();
                ProductStateManager.GetInstance().CartonBoxList.Clear();
                ProductStateManager.GetInstance().DictOnLine.Clear();

                ProductStateManager.GetInstance().CurrentDoing = false;

                ProductStateManager.GetInstance().Save();
                MessageBox.Show("清除状态成功！");
            }
            catch
            {
            }
        }

        #endregion End

        private void InitPlc()
        {
            PlcConn.Rack = 0;
            PlcConn.Slot = 1;
            PlcConn.ServerIp = AppManager.CreateInstance().PlcIp;
            PlcConn.Port = 502;
            Task.Run(async() => {
                try
                {
                    PlcConn.S7Connet();
                }
                catch(Exception ex)
                {
                    Log.Instance.LogWrite(ex);
                }
                await Task.Delay(2000);
                this.Invoke((EventHandler)delegate {
                    swtPlc.Checked = PlcConn.S7Connected;
                    Log.Instance.LogWrite("PLC连接状态:" + PlcConn.S7Connected.ToString());
                });
            });

        }
        /// <summary>
        /// 连接检测
        /// </summary>
        private void CheckConn()
        {
            Thread.Sleep(5000);
            while (true)
            {
                try
                {
                    Thread.Sleep(3000);

                    PlcConn.CheckConnected();
                }
                catch { }
            }
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
                    Log.Instance.LogWrite(string.Format("接收到：{0},{1},{2}", item.BatchNo, item.ReelNum, item.RndString));
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
                        //判断是否已经存在，如果存在，则修改
                        var findItem = ProductStateManager.GetInstance().DictOnLine[strBatchNo].ClothItems.Find(x => x.RndString == item.RndString);
                        if(findItem != null)
                        {
                            if(findItem.QualityName != item.QualityName)
                            {
                                //品质发生变化则需要修改
                                if (item.QualityName == "A") //需要包装的
                                {
                                    ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum = ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum + 1;
                                }
                                else
                                {
                                    ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum = ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum - 1;
                                    if(ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum < 0)
                                    {
                                        ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum = 0;
                                    }
                                }
                            }
                            else
                            {
                                findItem = item;
                            }
                        }
                        else
                        {
                            ProductStateManager.GetInstance().DictOnLine[strBatchNo].ClothItems.Add(item);
                            if (item.QualityName == "A") //需要包装的
                            {
                                ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum = ProductStateManager.GetInstance().DictOnLine[strBatchNo].AClassSum + 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite(ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
        }

        /// <summary>
        /// 还未做完的
        /// </summary>
        private CartonBox ReMainCartonBox
        {
            get;
            set;
        }
        /// <summary>
        /// 是否有遗留
        /// </summary>
        private bool HaveRemain
        {
            get;
            set;
        }

        /// <summary>
        /// 工作监督
        /// </summary>
        private void WorkFlowGoing()
        {
            Log.Instance.LogWrite("工作线程启动:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            bool bForce = false;//强制
            while (true)
            {
                try
                {
                    PlcCmd plcCmd;
                    if (PlcReceive.Count > 0)
                    {
                        //取消循环，通知比较少
                        //for(int i = 0;i < PlcReceive.Count;i++)
                        //{
                        //}
                        if (PlcReceive.TryDequeue(out plcCmd))
                        {
                            Log.Instance.LogWrite(string.Format("处理Plc指令：{0}", plcCmd.CmdCode));
                            this.Invoke((EventHandler)delegate {
                                if(txtInfo.TextLength > 800)
                                {
                                    txtInfo.Text = string.Empty;
                                }
                                txtInfo.Text += string.Format("处理Plc指令：{0}", plcCmd.CmdCode);
                            });
                            switch (plcCmd.CmdCode)
                            {
                                case 0x01:
                                    //上线完成通知

                                    break;
                                case 0x02://套袋请求打印标签
                                          //获取当前需要打印的
                                    if (ProductStateManager.GetInstance().CurrentBox == null)
                                    {
                                        break;
                                    }
                                    if (ProductStateManager.GetInstance().CurrentBox.LaunchIndex >= ProductStateManager.GetInstance().CurrentBox.OnLaunchItems.Count)
                                    {
                                        ProductStateManager.GetInstance().CurrentDoing = false;
                                        Log.Instance.LogWrite(string.Format("L254,当前序号：{0},当前箱内数{1}", ProductStateManager.GetInstance().CurrentBox.LaunchIndex, ProductStateManager.GetInstance().CurrentBox.OnLaunchItems.Count));
                                    }
                                    else
                                    {
                                        Log.Instance.LogWrite(string.Format("L263,当前序号：{0},当前箱内数{1}", ProductStateManager.GetInstance().CurrentBox.LaunchIndex, ProductStateManager.GetInstance().CurrentBox.OnLaunchItems.Count));

                                        FabricClothItem item = ProductStateManager.GetInstance().CurrentBox.OnLaunchItems[ProductStateManager.GetInstance().CurrentBox.LaunchIndex];
                                        this.Invoke((EventHandler)delegate {
                                            PrintFabricLabel(item);//打印当前
                                            Log.Instance.LogWrite(string.Format("打印面料标签：{0}", item.ReelNum));
                                        });
                                        
                                        try
                                        {
                                            //NoticeRollDiam(item);//告知当前布卷卷径

                                            byte lNum = byte.Parse(item.LineNum);
                                            bool isA = true;
                                            int flag = 0;
                                            if (item.QualityName != "A")
                                            {
                                                if(item.QualityName == "HC")
                                                {
                                                    flag = 3;
                                                }
                                                else if(item.QualityName == "KC" || item.QualityName == "SC")
                                                {
                                                    flag = 2;
                                                }
                                                isA = false;
                                            }

                                            NoticePrintedFabric(lNum, (int)(item.ProduceNum * 10), isA, flag);
                                            Log.Instance.LogWrite(string.Format("通知面料标签打印完成,线号:{0},品质：A{1}", item.LineNum, isA));
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Instance.LogWrite(string.Format("通知打印完成异常:{0}", ex.StackTrace));
                                        }
                                        try
                                        {
                                            if (ProductStateManager.GetInstance().CurrentBox.LaunchIndex == ProductStateManager.GetInstance().CurrentBox.OnLaunchItems.Count - 1)
                                            {
                                                //移除
                                                //ProductStateManager.GetInstance().CartonBoxItems
                                            
                                                ProductStateManager.GetInstance().CurrentDoing = false;//说明现在已经做完成了
                                                ProductStateManager.GetInstance().CurrentBox.LaunchIndex = 0;
                                            }
                                            else
                                            {
                                                ProductStateManager.GetInstance().CurrentBox.LaunchIndex = ProductStateManager.GetInstance().CurrentBox.LaunchIndex + 1;//累加
                                            }
                                        }
                                        catch(Exception ex)
                                        {
                                            Log.Instance.LogWrite("L291:" + ex.Message);
                                            Log.Instance.LogWrite("L291:" + ex.StackTrace);
                                        }
                                    }
                                    break;
                                case 0x03://进仓单打印
                                    int iPfl = 0;
                                    if (plcCmd.DataSegment.Count >= 2)
                                    {
                                        iPfl = (plcCmd.DataSegment[0] * 256) + plcCmd.DataSegment[1];
                                    }
                                    else if (plcCmd.DataSegment.Count == 1)
                                    {
                                        iPfl = plcCmd.DataSegment[0];
                                    }
                                    Log.Instance.LogWrite(string.Format("打印入库单,箱数：{0}", iPfl));
                                    if (iPfl > 0)
                                    {
                                        this.Invoke((EventHandler)delegate {
                                            PrintFabricList(iPfl); //打印入库单
                                        });
                                        //剔除已经成垛的
                                        int moveIndex = iPfl;
                                        if(ProductStateManager.GetInstance().CartonBoxItems.Count >= iPfl)
                                        {
                                            lock (ProductStateManager.GetInstance().CartonBoxItems)
                                            {
                                                while (moveIndex > 0)
                                                {
                                                    try
                                                    {
                                                        ProductStateManager.GetInstance().CartonBoxItems.RemoveAt(0);
                                                        moveIndex--;//BUG
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case 0x04:
                                    //------打印整箱的-------
                                    this.Invoke((EventHandler)delegate {
                                        PrintCartonBoxLabel();//
                                    });
                                    Log.Instance.LogWrite(string.Format("打印整箱"));
                                    break;
                                case 0x05:
                                    //强制上线,还是需要判断现在是否忙碌中
                                    if (!ProductStateManager.GetInstance().CurrentDoing)
                                    {
                                        bForce = true;
                                    }
                                    break;
                                case 0x06:
                                    //复位指令
                                    ProductStateManager.GetInstance().CurrentDoing = false;
                                    break;
                            }
                        }
                    }
                    //第一箱上完了上线第二箱
                    if (!ProductStateManager.GetInstance().CurrentDoing)//是否是当前上线
                    {
                        string strBatchNum = ProductStateManager.GetInstance().GetOnLineList();
                        if (string.IsNullOrEmpty(strBatchNum))
                        {
                            //有强制上线的
                            if (bForce)
                            {
                                bForce = false;//这时有强制指令时做
                                string strForceBatchNum = ProductStateManager.GetInstance().GetOnLineLastList();
                                if (!string.IsNullOrEmpty(strForceBatchNum))
                                {
                                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
                                    //开始强制上线
                                    WorkFlowManager.CreateInstance().CurrentLine = ProductStateManager.GetInstance().DictOnLine[strForceBatchNum].ClothItems[0].LineNum;
                                    WorkFlowManager.CreateInstance().CurrentBatchNo = strForceBatchNum;
                                    int iSumNeed1 = 0;//累计需要
                                    int iAClass1 = 0;
                                    //计算出需要移出多少个，剩余的部分全部移出
                                    foreach (var iCloth in ProductStateManager.GetInstance().DictOnLine[strForceBatchNum].ClothItems)
                                    {
                                        if (iCloth.QualityName == "A")
                                        {
                                            iAClass1++;
                                        }
                                        iSumNeed1++;
                                    }
                                    CartonBox newBox = new CartonBox();
                                    newBox.BatchNo = strForceBatchNum;
                                    newBox.BoxNum = AppManager.CreateInstance().GetBoxNoAndUpdate(strForceBatchNum).ToString(); ;
                                    lock (ProductStateManager.GetInstance().DictOnLine)
                                    {
                                        while (iSumNeed1 > 0)
                                        {
                                            var iRemove = ProductStateManager.GetInstance().DictOnLine[strForceBatchNum].ClothItems[0];
                                            newBox.OnLaunchItems.Add(iRemove);
                                            ProductStateManager.GetInstance().DictOnLine[strForceBatchNum].ClothItems.RemoveAt(0);
                                            iSumNeed1--;
                                        }
                                        ProductStateManager.GetInstance().DictOnLine[strForceBatchNum].AClassSum = ProductStateManager.GetInstance().DictOnLine[strForceBatchNum].AClassSum;//减掉上线的数量
                                        ProductStateManager.GetInstance().CurrentDoing = true;
                                        ProductStateManager.GetInstance().CurrentBatchNo = strForceBatchNum;
                                        ProductStateManager.GetInstance().CurrentBox = newBox;//当前装箱
                                        ProductStateManager.GetInstance().CartonBoxItems.Add(newBox);

                                        if (ProductStateManager.GetInstance().CurrentBox != null && ProductStateManager.GetInstance().CurrentBox.OnLaunchItems.Count > 0)
                                        {
                                            ProductStateManager.GetInstance().CurrentLine = ProductStateManager.GetInstance().CurrentBox.OnLaunchItems[0].LineNum;//当前线
                                        }

                                        if (ProductStateManager.GetInstance().DictCartonList.ContainsKey(ProductStateManager.GetInstance().CurrentLine))
                                        {
                                            ProductStateManager.GetInstance().DictCartonList[ProductStateManager.GetInstance().CurrentLine].Add(newBox);//当前线增加一箱
                                        }
                                        else
                                        {
                                            ProductStateManager.GetInstance().DictCartonList.Add(ProductStateManager.GetInstance().CurrentLine, new List<CartonBox>());
                                            ProductStateManager.GetInstance().DictCartonList[ProductStateManager.GetInstance().CurrentLine].Add(newBox);
                                        }

                                        ProductStateManager.GetInstance().Save();
                                    }
                                    //以及打印包装箱标签
                                    if (ProductStateManager.GetInstance().CurrentDoing)
                                    {
                                        //------打印整箱的-------
                                        this.Invoke((EventHandler)delegate {
                                            PrintCartonBoxLabel();//
                                        });
                                        try
                                        {
                                            //通知PLC上线
                                            byte iLNum = byte.Parse(ProductStateManager.GetInstance().CurrentLine);
                                            short fWidth = (short)newBox.OnLaunchItems[0].FabricWidth;
                                            short iRoll = (short)newBox.OnLaunchItems[0].RollDiam;
                                            Thread.Sleep(2400);
                                            NoticePlc(iLNum, fWidth, iRoll, ProductStateManager.GetInstance().CurrentBox);
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Instance.LogWrite(ex);
                                        }
                                    }
                                }
                            }
                            //未能达到上线的条件
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Log.Instance.LogWrite("L412：开始上线另外的");
                            //开始上线  ----要注意超出

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
                            newBox.BoxNum = AppManager.CreateInstance().GetBoxNoAndUpdate(strBatchNum).ToString();

                            lock (ProductStateManager.GetInstance().DictOnLine)
                            {
                                while (iSumNeed > 0)
                                {
                                    var iRemove = ProductStateManager.GetInstance().DictOnLine[strBatchNum].ClothItems[0];
                                    newBox.OnLaunchItems.Add(iRemove);
                                    ProductStateManager.GetInstance().DictOnLine[strBatchNum].ClothItems.RemoveAt(0);
                                    iSumNeed--;
                                }
                                ProductStateManager.GetInstance().DictOnLine[strBatchNum].AClassSum = ProductStateManager.GetInstance().DictOnLine[strBatchNum].AClassSum - AppManager.CreateInstance().PackingNum;//减掉上线的数量
                                ProductStateManager.GetInstance().CurrentDoing = true;
                                ProductStateManager.GetInstance().CurrentBatchNo = strBatchNum;
                                ProductStateManager.GetInstance().CurrentBox = newBox;//当前装箱
                                ProductStateManager.GetInstance().CartonBoxItems.Add(newBox);
                                if (ProductStateManager.GetInstance().CurrentBox != null && ProductStateManager.GetInstance().CurrentBox.OnLaunchItems.Count > 0)
                                {
                                    ProductStateManager.GetInstance().CurrentLine = ProductStateManager.GetInstance().CurrentBox.OnLaunchItems[0].LineNum;//当前线
                                }

                                if (ProductStateManager.GetInstance().DictCartonList.ContainsKey(ProductStateManager.GetInstance().CurrentLine))
                                {
                                    ProductStateManager.GetInstance().DictCartonList[ProductStateManager.GetInstance().CurrentLine].Add(newBox);//当前线增加一箱
                                }
                                else
                                {
                                    ProductStateManager.GetInstance().DictCartonList.Add(ProductStateManager.GetInstance().CurrentLine, new List<CartonBox>());
                                    ProductStateManager.GetInstance().DictCartonList[ProductStateManager.GetInstance().CurrentLine].Add(newBox);
                                }

                                ProductStateManager.GetInstance().Save();
                            }
                            Log.Instance.LogWrite("L455：新上线！");
                            //以及打印包装箱标签
                            if (ProductStateManager.GetInstance().CurrentDoing)
                            {
                                //------打印整箱的-------
                                this.Invoke((EventHandler)delegate {
                                    PrintCartonBoxLabel();//
                                });
                                try
                                {
                                    Thread.Sleep(2400);//延迟1秒
                                    //通知PLC上线
                                    byte iLNum = byte.Parse(ProductStateManager.GetInstance().CurrentLine);
                                    short fWidth = (short)newBox.OnLaunchItems[0].FabricWidth;
                                    short iRoll = (short)newBox.OnLaunchItems[0].RollDiam;
                                    NoticePlc(iLNum, fWidth, iRoll, ProductStateManager.GetInstance().CurrentBox);
                                }
                                catch(Exception ex)
                                {
                                    Log.Instance.LogWrite("L471:" + ex.Message);
                                    Log.Instance.LogWrite(ex.StackTrace);
                                }
                            }
                        }
                    }
                    else
                    {
                        bForce = false;
                        //空闲就休息2秒
                        Thread.Sleep(2000);
                    }
                }
                catch(Exception ex)
                {
                    Log.Instance.LogWrite("L486:" + ex.Message);
                    Log.Instance.LogWrite("L486:" + ex.StackTrace);
                    Console.WriteLine(ex);
                }
            }
        }
        /// <summary>
        /// 监控PLC状态
        /// </summary>
        private void SearchPlcState()
        {
            Log.Instance.LogWrite("PLC状态线程启动:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            while (true)
            {
                try
                {
                    if(!PlcConn.S7Connected)
                    {
                        Thread.Sleep(500);
                        PlcConn.S7Connet();
                        Thread.Sleep(1500);
                    }

                    bool needRead = PlcConn.ReadFlag(20, 0);//读取是否已经有标志
                    if(needRead)
                    {
                        PlcConn.WriteFlag(20, 0, false);//设置读取完成标志

                        byte[] cmdInput = PlcConn.ReadDataBlock(20, 1, 6);//读取并解析,目前6位

                        if(cmdInput != null && cmdInput.Length > 1)
                        {
                            int lineNum = cmdInput[1];//反馈的线序
                            switch(cmdInput[0])
                            {
                                case 0x01:
                                    //下线完成通知
                                    PlcCmd cmd0 = new PlcCmd();
                                    cmd0.CmdCode = 0x01;
                                    cmd0.MachineId = cmdInput[1];
                                    PlcReceive.Enqueue(cmd0);
                                    break;
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
                                case 0x04:
                                    //下线完成通知
                                    PlcCmd cmd4 = new PlcCmd();
                                    cmd4.CmdCode = 0x04;
                                    cmd4.MachineId = cmdInput[1];
                                    cmd4.DataSegment.Add(cmdInput[3]);//一垛完成总箱数

                                    PlcReceive.Enqueue(cmd4);
                                    break;
                                case 0x05:
                                    //强制
                                    PlcCmd cmd5 = new PlcCmd();
                                    cmd5.CmdCode = 0x05;
                                    cmd5.MachineId = cmdInput[1];
                                    //
                                    PlcReceive.Enqueue(cmd5);
                                    break;
                                case 0x06:
                                    //强制
                                    PlcCmd cmd6 = new PlcCmd();
                                    cmd6.CmdCode = 0x06;
                                    cmd6.MachineId = cmdInput[1];
                                    //
                                    PlcReceive.Enqueue(cmd6);
                                    break;
                                default:

                                    break;
                            }
                        }
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
                catch(Exception ex)
                {
                    Log.Instance.LogWrite("L578:" + ex.Message);
                    Log.Instance.LogWrite(ex.StackTrace);
                }
            }
        }

        #region 打印标签
        /// <summary>
        /// 面料标签
        /// </summary>
        private void PrintFabricLabel(FabricClothItem item)
        {
            try
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
                            //string lblFile = Application.StartupPath + "\\Templates\\" + pbCfg.LabelBName;// FabricKCSC.btw
                            string lblFile = Application.StartupPath + "\\Templates\\KcSc" + pbCfg.LabelBName;
                            if (File.Exists(lblFile))
                            {
                                PrintHelper.CreateInstance().BarPrintInit(lblFile, pbCfg.PrinterName, dictData, PrintHelper.FabricTempleteFieldsList, pbCfg.PrintCopies);
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
                                PrintHelper.CreateInstance().BarPrintInit(lblFile, pCfg.PrinterName, dictData, PrintHelper.FabricTempleteFieldsList, pCfg.PrintCopies);
                            }
                        }
                        Console.WriteLine(strQC + " A类：" + DateTime.Now.ToString("yyyyMMddHHmmss") + "已经打印");
                        break;
                }
            }
            catch(Exception ex)
            {
                Log.Instance.LogWrite(ex);
            }
            try
            {
                //更新数据库
                using (var db = new DapperContext("MySqlDbConnection"))
                {
                    try
                    {
                        var rtnB = db.Update("update fabric_cloths set IsFinish=1 where RndString=@RndString", new { RndString = item.RndString});
                        if (rtnB)
                        {
                            Console.WriteLine("更新成功！");
                        }
                        else
                        {
                            Console.WriteLine("更新失败！");
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch
            { }
        }
        /// <summary>
        /// 打印总垛数据，并移除打印的,打印报表
        /// </summary>
        /// <param name="packNum">总包数装成垛</param>
        private void PrintFabricList(int packNum)
        {
            try
            {
                //没有就不打印
                if (packNum < 0)
                {
                    return;
                }
                if (ProductStateManager.GetInstance().CartonBoxItems.Count >= packNum)
                {
                }
                else
                {
                    return;
                }
                byte lineNum = 0;
                int minPack = Math.Min(ProductStateManager.GetInstance().CartonBoxItems.Count, packNum);
                if(minPack < 1)
                {
                    //通知已经打印
                    NoticePrintedReport(lineNum, minPack);
                    return;
                }
                //一垛总包数
                List <BoxDetail> Boxes = new List<BoxDetail>();
                //增加装箱信息
                //BoxInfo info = new BoxInfo();
                //List<BoxInfo> BoxInfos = new List<BoxInfo>();
                string strBatchNo = string.Empty;
                string strColorNum = string.Empty;
                string strQualityString = string.Empty;
                string strSpecs = string.Empty;
                decimal sumRoll = 0;
                try
                {
                    for (int i = 0; i < minPack; i++)
                    {
                        CartonBox item = ProductStateManager.GetInstance().CartonBoxItems[i];
                        BoxDetail detail = new BoxDetail();
                        for (int j = 0; j < item.OnLaunchItems.Count; j++)
                        {
                            if (j == 0 && i == 0)
                            {
                                //info.BatchNo = item.OnLaunchItems[j].BatchNo;
                                //info.ColorNum = item.OnLaunchItems[j].ColorNum;
                                //info.QualityString = item.OnLaunchItems[j].QualityString;
                                //info.Specs = item.OnLaunchItems[j].Specs;
                                strBatchNo = item.OnLaunchItems[j].BatchNo;
                                strColorNum = item.OnLaunchItems[j].ColorNum;
                                strQualityString = item.OnLaunchItems[j].QualityString;
                                strSpecs = item.OnLaunchItems[j].Specs;
                                //BoxInfos.Add(info);
                                try
                                {
                                    lineNum = byte.Parse(item.OnLaunchItems[j].LineNum);
                                }
                                catch
                                { }
                            }
                            switch (j)
                            {
                                case 0:
                                    detail.RollNum1 = decimal.Round((decimal)item.OnLaunchItems[j].ProduceNum, 2);
                                    detail.ReelNum1 = item.OnLaunchItems[j].ReelNum;
                                    break;
                                case 1:
                                    detail.RollNum2 = decimal.Round((decimal)item.OnLaunchItems[j].ProduceNum, 2);
                                    detail.ReelNum2 = item.OnLaunchItems[j].ReelNum;
                                    break;
                                case 2:
                                    detail.RollNum3 = decimal.Round((decimal)item.OnLaunchItems[j].ProduceNum, 2);
                                    detail.ReelNum3 = item.OnLaunchItems[j].ReelNum;
                                    break;
                                case 3:
                                    detail.RollNum4 = decimal.Round((decimal)item.OnLaunchItems[j].ProduceNum, 2);
                                    detail.ReelNum4 = item.OnLaunchItems[j].ReelNum;
                                    break;
                                case 4:
                                    detail.RollNum5 = decimal.Round((decimal)item.OnLaunchItems[j].ProduceNum, 2);
                                    detail.ReelNum5 = item.OnLaunchItems[j].ReelNum;
                                    break;
                                case 5:
                                    detail.RollNum6 = decimal.Round((decimal)item.OnLaunchItems[j].ProduceNum, 2);
                                    detail.ReelNum6 = item.OnLaunchItems[j].ReelNum;
                                    break;
                            }
                        }

                        sumRoll = +detail.RollSum;//总计
                        detail.BoxNum = item.BoxNum;
                        Boxes.Add(detail);
                    }
                }
                catch(Exception ex)
                {
                    Log.Instance.LogWrite("L897:" + ex.Message);
                    Log.Instance.LogWrite(ex.StackTrace);
                }
                //-------------------
                var FDataSet = new DataSet();
                DataTable table = PackHelper.ListToDataTable(Boxes);
                table.TableName = "Products";
                FDataSet.Tables.Add(table);
                string filePath = Application.StartupPath + "\\Report\\cartonreport.frx";
                if(File.Exists(filePath))
                {
                }
                else
                {
                    Log.Instance.LogWrite("L 1.0,文件不存在:" + filePath);
                }
                Report report = new Report();
                report.Load(filePath);

                Config.ReportSettings.ShowProgress = false;//不显示进度

                Parameter BatchNo = new Parameter();
                BatchNo.Name = "BatchNo";
                BatchNo.DataType = typeof(string);
                BatchNo.Value = strBatchNo;

                Parameter colorNum = new Parameter();
                colorNum.Name = "ColorNum";
                colorNum.DataType = typeof(string);
                colorNum.Value = strColorNum;

                Parameter QualityString = new Parameter();
                QualityString.Name = "QualityString";
                QualityString.DataType = typeof(string);
                QualityString.Value = strQualityString;

                Parameter Specs = new Parameter();
                Specs.Name = "Specs";
                Specs.DataType = typeof(string);
                Specs.Value = strSpecs;

                Parameter paraTotal = new Parameter();
                paraTotal.Name = "ParaTotal";
                paraTotal.DataType = typeof(string);
                paraTotal.Value = sumRoll.ToString();
   
                report.Parameters.Add(BatchNo);
                report.Parameters.Add(colorNum);
                report.Parameters.Add(QualityString);
                report.Parameters.Add(Specs);
                report.Parameters.Add(paraTotal);
                report.RegisterData(FDataSet, "NorthWind");//NorthWind
                report.SmoothGraphics = true;

                report.Prepare();
                report.PrintSettings.ShowDialog = false;

                report.Print();
                report.Dispose();

                #region EastReport

                //EastReport.Report report = new EastReport.Report();
                //string filePath = Application.StartupPath + "\\Report\\cartonreport.rpt";
                //try
                //{
                //    DataSet dsInfo = ConvertToDataSet(BoxInfos);
                //    report.AddDataSet(dsInfo);

                //    DataSet pDatset = ConvertToDataSet(Boxes);
                //    report.AddDataSet(pDatset);

                //    report.Variants.Add(new EastReport.Variant("RollDiamSum", EastReport.VariantType.Decimal, ""));

                //    //report.Variants.Add(new EastReport.Variant("BatchNo", EastReport.VariantType.String, "230A321"));
                //    //report.Variants.Add(new EastReport.Variant("QualityString", EastReport.VariantType.String, "yke813017029"));
                //    //report.Variants.Add(new EastReport.Variant("ColorNum", EastReport.VariantType.String, "199"));
                //    //report.Variants.Add(new EastReport.Variant("Specs", EastReport.VariantType.String, "137"));
                //}
                //catch (Exception)
                //{ }

                //System.Xml.XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load(filePath);//载入报表
                //report.Load(xmlDoc);
                //report.Print(true);
                ////报表打印
                //report.Dispose();

                #endregion End

                Console.WriteLine("总包数打印！");
                //通知已经打印
                NoticePrintedReport(lineNum, packNum);
            }
            catch(Exception ex)
            {
                Console.WriteLine("L977:" + ex.Message);
                Console.WriteLine(ex.StackTrace);
                Log.Instance.LogWrite("L977:" + ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
        }

        public DataSet ConvertToDataSet<T>(IList<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                return null;
            }
            DataSet ds = new DataSet();
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;
            System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (T t in list)
            {
                if (t == null)
                {
                    continue;
                }
                row = dt.NewRow();
                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];
                    string name = pi.Name;



                    if (dt.Columns[name] == null)
                    {
                        var type = pi.PropertyType;
                        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            type = type.GetGenericArguments()[0];
                        }
                        column = new DataColumn(name, type);
                        dt.Columns.Add(column);
                    }
                    row[name] = pi.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 打印整箱的标签
        /// </summary>
        private void PrintCartonBoxLabel()
        {
            try
            {
                //打印整箱标签
                CartonBoxLabel cartonBox = new CartonBoxLabel();
                string lStrNum = "";
                int iNum = 0;
                string strRndString = "";
                string strRollNums = "";
                if (ProductStateManager.GetInstance().CurrentBox != null)
                {
                    var fItem = ProductStateManager.GetInstance().CurrentBox.OnLaunchItems[0];
                    cartonBox.BatchNo = fItem.BatchNo;
                    cartonBox.ColorNum = fItem.ColorNum;
                    cartonBox.QualityString = fItem.QualityString;
                    cartonBox.Specs = fItem.Specs;
                    cartonBox.BoxNum = ProductStateManager.GetInstance().CurrentBox.BoxNum;// 
                    lStrNum = fItem.LineNum;
                    iNum = int.Parse(lStrNum);
                    int idx = 0;
                    foreach (var item in ProductStateManager.GetInstance().CurrentBox.OnLaunchItems)
                    {
                        if (item.QualityName == "A")
                        {
                            strRndString += item.RndString + ",";
                            strRollNums += item.ReelNum.ToString() + ",";
                            switch (idx)
                            {
                                case 0:
                                    cartonBox.RollNum1 = (decimal)item.ProduceNum;
                                    cartonBox.ReelNum1 = item.ReelNum;
                                    break;
                                case 1:
                                    cartonBox.RollNum2 = (decimal)item.ProduceNum;
                                    cartonBox.ReelNum2 = item.ReelNum;
                                    break;
                                case 2:
                                    cartonBox.RollNum3 = (decimal)item.ProduceNum;
                                    cartonBox.ReelNum3 = item.ReelNum;
                                    break;
                                case 3:
                                    cartonBox.RollNum4 = (decimal)item.ProduceNum;
                                    cartonBox.ReelNum4 = item.ReelNum;
                                    break;
                                case 4:
                                    cartonBox.RollNum5 = (decimal)item.ProduceNum;
                                    cartonBox.ReelNum5 = item.ReelNum;
                                    break;
                                case 5:
                                    cartonBox.RollNum6 = (decimal)item.ProduceNum;
                                    cartonBox.ReelNum6 = item.ReelNum;
                                    break;
                            }
                            idx++;
                        }
                    }
                }

                //模板不同纸张不同，打印换纸麻烦
                var pbCfg = AppManager.CreateInstance().GetPrintCfg(lStrNum);
                if (pbCfg != null)
                {
                    Dictionary<string, string> dictData = PrintHelper.GetEntityPropertyToDict(cartonBox);
                    string lblFile = Application.StartupPath + "\\Templates\\" + pbCfg.CartonLabel;
                    if (File.Exists(lblFile))
                    {
                        PrintHelper.CreateInstance().BarPrintInit(lblFile, pbCfg.CartonPrinter, dictData,PrintHelper.CartonTempleteFieldsList, pbCfg.PrintCopies);
                    }
                }

                //保存
                CartonBoxInfo boxInfo = new CartonBoxInfo();
                boxInfo.BatchNo = cartonBox.BatchNo;
                boxInfo.BoxNum = cartonBox.BoxNum;
                boxInfo.ColorNum = cartonBox.ColorNum;
                boxInfo.QualityString = cartonBox.QualityString;
                boxInfo.Specs = cartonBox.Specs;
                boxInfo.SumNum = cartonBox.RollSum.ToString();
                boxInfo.RndStrings = strRndString.Trim(',');
                boxInfo.ReelNums = strRollNums.Trim(',');
                SaveFabricCloth(boxInfo);

                //通知LC已经打印
                NoticePrintedCarton((byte)iNum, ProductStateManager.GetInstance().CurrentBox.OnLaunchItems.Count);
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("L895:" + ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
        }

        private void SaveFabricCloth(CartonBoxInfo item)
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

        #endregion End

        #region PLC通信相关
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void NoticeRollDiam(FabricClothItem item)
        {
            //通知卷径
            try
            {
                byte iLNum = byte.Parse(item.LineNum);
                short shtRoll = (short)(item.ProduceNum * 10);//转换

                List<byte> lCmd = new List<byte>();
                lCmd.Add(0x04); //命令码
                lCmd.Add(iLNum);//产线号
                lCmd.AddRange(MathHelper.ShortToBytes(shtRoll));//卷么

                PlcConn.WriteDataBlock(20, 21, lCmd.ToArray());//
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("L941:" + ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }

            //通知为新指令
            try
            {
                //通知PLC有新指令
                PlcConn.WriteFlag(20, 20, true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// 通知PLC上线 1号指令
        /// </summary>
        /// <param name="iLNum"></param>
        /// <param name="fabricWidth"></param>
        /// <param name="iRollDiam">长度</param>
        /// <param name="carton"></param>
        private void NoticePlc(byte iLNum, short fabricWidth, Int16 iRollDiam, CartonBox carton)
        {
            //通知上线
            try
            {
                List<byte> lCmd = new List<byte>();
                lCmd.Add(0x01); //命令码
                lCmd.Add(iLNum);//产线号
                                //=======================方法一--------------------
                #region 方法一，原来方法
                //{
                //    lCmd.AddRange(MathHelper.ShortToBytes(fabricWidth));//宽度
                //    lCmd.AddRange(MathHelper.ShortToBytes(iRollDiam));//直径
                //    List<int> lNaclassls = new List<int>();
                //    for (int i = 0; i < carton.OnLaunchItems.Count; i++)
                //    {
                //        if (carton.OnLaunchItems[i].QualityName != "A")
                //        {
                //            lNaclassls.Add(i);//次品序号
                //        }
                //    }
                //    lCmd.AddRange(PackHelper.BuildBTypeValue(lNaclassls));
                //    lCmd.AddRange(MathHelper.ShortToBytes(Convert.ToInt16(carton.OnLaunchItems.Count)));// 增加一箱总个数
                //}
                #endregion End

                //=======================方法二--------------------
                #region 方法二
                {
                    lCmd.AddRange(MathHelper.ShortToBytes(Convert.ToInt16(carton.OnLaunchItems.Count)));// 增加一箱总个数
                    lCmd.AddRange(MathHelper.ShortToBytes(fabricWidth));//宽度

                    List<int> lNaclasslsnew = new List<int>();
                    for (int i = 0; i < carton.OnLaunchItems.Count; i++)
                    {
                        if (carton.OnLaunchItems[i].QualityName != "A")
                        {
                            lNaclasslsnew.Add(i);//次品序号
                        }
                    }
                    lCmd.AddRange(PackHelper.BuildBTypeValue(lNaclasslsnew));
                }
                #endregion End
                PlcConn.WriteDataBlock(20, 21, lCmd.ToArray());//
            }
            catch(Exception ex)
            {
                Log.Instance.LogWrite("L1010:" + ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
            //通知为新指令
            try
            {
                //通知PLC有新指令
                PlcConn.WriteFlag(20, 20, true);
            }
            catch
            { }
        }
        /// <summary>
        /// 通知薄膜已打印
        /// </summary>
        private void NoticePrintedFabric(byte iLNum,int rolldiam,bool isA = true,int flag = 0)
        {
            //通知上线
            try
            {
                List<byte> lCmd = new List<byte>();
                lCmd.Add(0x02); //命令码
                lCmd.Add(iLNum);//产线号

                lCmd.AddRange(MathHelper.ShortToBytes(Convert.ToInt16(rolldiam)));
                if (flag > 0)
                {
                    lCmd.Add((byte)flag);
                }
                else
                {
                    if (isA)
                    {
                        lCmd.Add(0x01);
                    }
                    else
                    {
                        lCmd.Add(0x02);
                    }
                }

                PlcConn.WriteDataBlock(20, 21, lCmd.ToArray());//
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("L1040:" + ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
            //通知为新指令
            try
            {
                //通知PLC有新指令
                PlcConn.WriteFlag(20, 20, true);
            }
            catch
            { }
        }

        private void NoticePrintedCarton(byte iLNum, int print)
        {
            //通知上线
            try
            {
                List<byte> lCmd = new List<byte>();
                lCmd.Add(0x03); //
                lCmd.Add(iLNum);//产线号

                lCmd.AddRange(MathHelper.ShortToBytes(Convert.ToInt16(print)));

                PlcConn.WriteDataBlock(20, 21, lCmd.ToArray());//
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("L1068:" + ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
            //通知为新指令
            try
            {
                //通知PLC有新指令
                PlcConn.WriteFlag(20, 20, true);
            }
            catch
            { }
        }

        private void NoticePrintedReport(byte iLNum, int cartonNum)
        {
            //通知上线
            try
            {
                List<byte> lCmd = new List<byte>();
                lCmd.Add(0x03); //
                lCmd.Add(iLNum);//产线号

                lCmd.AddRange(MathHelper.ShortToBytes(Convert.ToInt16(cartonNum)));

                PlcConn.WriteDataBlock(20, 21, lCmd.ToArray());//
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("L1096:" + ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
            //通知为新指令
            try
            {
                //通知PLC有新指令
                PlcConn.WriteFlag(20, 20, true);
            }
            catch
            { }
        }
        /// <summary>
        /// 流水线暂停
        /// </summary>
        private void NoticePause()
        {
            //通知上线
            try
            {
                List<byte> lCmd = new List<byte>();
                lCmd.Add(0x09); //
                lCmd.Add(0x00);//产线号

                lCmd.AddRange(MathHelper.ShortToBytes(Convert.ToInt16(0)));

                PlcConn.WriteDataBlock(20, 21, lCmd.ToArray());//
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("L1126:" + ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
            //通知为新指令
            try
            {
                //通知PLC有新指令
                PlcConn.WriteFlag(20, 20, true);
            }
            catch
            { }
        }

        /// <summary>
        /// 重新启动流水线
        /// </summary>
        /// <param name="iLNum"></param>
        /// <param name="cartonNum"></param>
        private void NoticeStart()
        {
            //通知上线
            try
            {
                List<byte> lCmd = new List<byte>();
                lCmd.Add(0x0A); //
                lCmd.Add(0x00);//产线号

                PlcConn.WriteDataBlock(20, 21, lCmd.ToArray());//
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("L1157:" + ex.Message);
                Log.Instance.LogWrite(ex.StackTrace);
            }
            //通知为新指令
            try
            {
                //通知PLC有新指令
                PlcConn.WriteFlag(20, 20, true);
            }
            catch
            { }
        }

        #endregion End

        private void btnTest_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 取消掉右上角关闭按钮
        /// </summary>
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }



        #region 修改数据

        private void btnOnlineData_Click(object sender, EventArgs e)
        {
            string lNum = txtLine.Text;
            string strBatchNo = txtEBatchNo.Text;
            int reelnum = int.Parse(txtEReelNum.Text);
            if (string.IsNullOrEmpty(lNum) || string.IsNullOrEmpty(strBatchNo))
            {
                return;
            }
            //批次号
            if (!ProductStateManager.GetInstance().DictOnLine.ContainsKey(strBatchNo))
            {
                return;
                //OnLineCloth onLine = new OnLineCloth();
                //onLine.BatchNo = strBatchNo;
                //ProductStateManager.GetInstance().DictOnLine.Add(strBatchNo, onLine);
            }

            //判断是否已经存在，如果存在，则修改
            var findItem = ProductStateManager.GetInstance().DictOnLine[strBatchNo].ClothItems.Find(x => x.ReelNum == reelnum);
            if (findItem != null)
            {
                txtELen.Text = findItem.ProduceNum.ToString();
                txtEQString.Text = findItem.QualityString.ToString();
                txtEColorNum.Text = findItem.ColorNum.ToString();
                txtESpecs.Text = findItem.Specs.ToString();
                txtECWidth.Text = findItem.FabricWidth.ToString();
                txtERollDiam.Text = findItem.RollDiam.ToString();
                cboQualityName.SelectedItem = findItem.QualityName.ToString();
                updateFindItem = findItem;
            }
            else
            {
                txtELen.Text = "";
                txtEQString.Text = "";
                txtEColorNum.Text = "";
                txtESpecs.Text = "";
                txtECWidth.Text = "";
                txtERollDiam.Text = "";
                updateFindItem = null;
            }
        }

        private FabricClothItem updateFindItem;

        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            if(updateFindItem == null)
            {
                MessageBox.Show("未找到对应的产品，是否已经包装装箱！");
                return;
            }
            try
            {
                bool isEditQa = false;
                string strBatchNum = txtEBatchNo.Text.Trim();
                updateFindItem.FabricWidth = int.Parse(txtECWidth.Text);
                updateFindItem.RollDiam = int.Parse(txtERollDiam.Text);
                updateFindItem.ProduceNum = float.Parse(txtELen.Text);
                updateFindItem.QualityString = txtEQString.Text;
                updateFindItem.ColorNum = txtEColorNum.Text;
                updateFindItem.Specs = txtESpecs.Text;
                string strQa = cboQualityName.SelectedItem.ToString();
                if (strQa != updateFindItem.QualityName)
                {
                    isEditQa = true;
                    updateFindItem.QualityName = cboQualityName.SelectedItem.ToString();
                }
                //增加已经上线的
                lock (ProductStateManager.GetInstance().DictOnLine)
                {
                    if(isEditQa)
                    {
                        //品质发生变化则需要修改
                        if (updateFindItem.QualityName == "A") //需要包装的
                        {
                            ProductStateManager.GetInstance().DictOnLine[strBatchNum].AClassSum = ProductStateManager.GetInstance().DictOnLine[strBatchNum].AClassSum + 1;
                        }
                        else
                        {
                            ProductStateManager.GetInstance().DictOnLine[strBatchNum].AClassSum = ProductStateManager.GetInstance().DictOnLine[strBatchNum].AClassSum - 1;
                            if (ProductStateManager.GetInstance().DictOnLine[strBatchNum].AClassSum < 0)
                            {
                                ProductStateManager.GetInstance().DictOnLine[strBatchNum].AClassSum = 0;
                            }
                        }
                    }
                }
                MessageBox.Show("已经修改，请检查是否已经包装或者装箱，已装箱的无法更改，需要重新上线！");
            }
            catch
            { }
        }
        #endregion End

        private void btnSysState_Click(object sender, EventArgs e)
        {
            string sysInfo = ProductStateManager.GetInstance().SysInfo();
            txtSysState.Text = sysInfo;
        }

        private void btClsBatch_Click(object sender, EventArgs e)
        {
            //var carton = new CartonBox();
            //carton.BatchNo = "ABC231218";
            //carton.LaunchIndex = 0;
            //carton.BoxNum = "1";
            //carton.OnLaunchItems = new List<FabricClothItem>();
            //int realnum = rd.Next(5);
            //for (int i = 0; i < 6; i++)
            //{
            //    FabricClothItem fabric = new FabricClothItem();
            //    fabric.BatchNo = "ABC231218";
            //    fabric.QualityName = "QN";
            //    fabric.ReelNum = realnum + i;
            //    fabric.LineNum = realnum.ToString();
            //    fabric.ColorNum = realnum.ToString();
            //    fabric.ProduceNum = (float)(45 + realnum + rd.NextDouble());
            //    fabric.Specs = "Sp";
            //    fabric.QualityString = "Qs";
            //    carton.OnLaunchItems.Add(fabric);
            //}

            //ProductStateManager.GetInstance().CartonBoxItems.Add(carton);

            //PrintFabricList(1);

            //return;

            string strBatchNoCls = txtClsBatchNo.Text.Trim();
            if (MessageBox.Show("确定清除此批次上线,请确保线上已经无此批次正在\r\n包装的产品，并停止上线的状态！", "警告",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (!string.IsNullOrEmpty(strBatchNoCls) && ProductStateManager.GetInstance().DictOnLine.ContainsKey(strBatchNoCls))
                    {
                        ProductStateManager.GetInstance().CurrentDoing = false;
                        ProductStateManager.GetInstance().DictOnLine.Remove(strBatchNoCls);
                    }
                }
                catch
                {

                }
            }
        }
    }
}
