using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Net;
using Yu3zx.Moonlit.Modols;
using System.IO;
using Yu3zx.Util;
using Yu3zx.Moonlit.Models;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.service;
using Yu3zx.Moonlit.UC;
using Yu3zx.Acquisition;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Drawing2D;
using Yu3zx.Moonlit.QrCtrl;
using System.Threading;

namespace Yu3zx.Moonlit
{
    public partial class mainFrm : CCWin.CCSkinMain
    {
        #region 私有变量

        Random rd = new Random();
        List<string> lLocalIP = new List<string>();

        private Thread thStatus = null;

        List<DevModel> mydevList = new List<DevModel>();

        #endregion End
        public mainFrm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
            this.UpdateStyles();
        }

        #region 数据处理和更新状态
        private void InitMainUI()
        {
            nudFontSize.Value = (decimal)ProgramManager.GetInstance().QrFontSize;

            nudHeight.Value = ProgramManager.GetInstance().QrHeight;
            nudWidth.Value = ProgramManager.GetInstance().QrWidth;

            qrImage.SetPage(ProgramManager.GetInstance().QrWidth, ProgramManager.GetInstance().QrHeight);
            qrImage.FontSize = ProgramManager.GetInstance().QrFontSize;

            nudLeft.Value = ProgramManager.GetInstance().MarginLeft;
            nudDown.Value = ProgramManager.GetInstance().MarginBottom;
            nudUp.Value = ProgramManager.GetInstance().MarginTop;
            nudRight.Value = ProgramManager.GetInstance().MarginRight;
        }

        private void ShowDevsStatus()
        {
            flpDevices.Controls.Clear();
            mydevList.Clear();
            foreach(AcqPlc plc in MyPlcManager.GetInstance().Devices)
            {
                DevModel dev = new DevModel();
                dev.ConnectStatus = plc.Connected;
                dev.DevName = plc.DeviceName;
                dev.DevId = plc.DeviceId;
                mydevList.Add(dev);
                UCDevStatus ucDev = new UCDevStatus(dev);
                ucDev.Name = "plc" + plc.DeviceId;
                ucDev.Width = 236;
                flpDevices.Controls.Add(ucDev);
            }

            if(thStatus != null)
            {
                thStatus.Abort();
                thStatus = null;
            }
            thStatus = new Thread(ChangeStatus);
            thStatus.Name = "DevStatusMonitor";
            thStatus.IsBackground = true;
            thStatus.Start();
        }

        private DevModel FindDevStatus(string devid)
        {
            if(mydevList == null || mydevList.Count == 0)
            {
                return null;
            }
            return mydevList.Find(x => x.DevId == devid);
        }

        private void ChangeStatus()
        {
            while(true)
            {
                try
                {
                    for (int i = 0; i < MyPlcManager.GetInstance().Devices.Count; i++)
                    {
                        var devstatus = FindDevStatus(MyPlcManager.GetInstance().Devices[i].DeviceId);
                        if(devstatus == null)
                        {
                            this.Invoke((EventHandler)delegate
                            {
                                DevModel dev = new DevModel();
                                dev.ConnectStatus = MyPlcManager.GetInstance().Devices[i].Connected;
                                dev.DevName = MyPlcManager.GetInstance().Devices[i].DeviceName;
                                dev.DevId = MyPlcManager.GetInstance().Devices[i].DeviceId;
                                mydevList.Add(dev);
                                UCDevStatus ucDev = new UCDevStatus(dev);
                                ucDev.Name = "plc" + MyPlcManager.GetInstance().Devices[i].DeviceId;
                                ucDev.Width = 236;
                                flpDevices.Controls.Add(ucDev);
                            });
                        }
                        else
                        {
                            devstatus.ConnectStatus = MyPlcManager.GetInstance().Devices[i].Connected;
                        }
                    }
                    Thread.Sleep(3000);
                }
                catch
                {

                }
            }
        }

        public void DealReadData(object sender, string cmd, string revData)
        {
            string strTmp = revData;
            switch(cmd)
            {
                case "Complete":

                    break;
                case "Del":
                    //string strDelPart = revData.Substring(0, 34);
                    //string strDelSum = revData.Substring(34, 34);
                    YyCollectInfoService ycis = (YyCollectInfoService)BeanUtil.getBean("yyCollectInfoService");
                    bool bDel = ycis.updateLastDel(sender.ToString());
                    //YyCollectInfo yyCI = ycis.get(strDelSum, strDelPart);
                    //if(yyCI != null)
                    //{
                    //    ycis.delete(yyCI.ID);
                    //}
                    break;
                case "Add":
                    if (revData.Length < 68)
                    {
                        return;
                    }
                    string strAddPart = revData.Substring(0, 34);
                    string strAddSum = revData.Substring(34, 34);
                    SysProductInfoService spis = (SysProductInfoService)BeanUtil.getBean("sysProductInfoService");
                    SysProductInfo sProduct = spis.getProduct(strAddSum);
                    string strProdNo = string.Empty;
                    if(sProduct != null)
                    {
                        strProdNo = sProduct.No;

                    }
                    bool bCanAdd = true;// spis.addValidate(strAddSum, strAddPart, sender.ToString());
                    if(bCanAdd)
                    {
                        YyCollectInfoService ycis1 = (YyCollectInfoService)BeanUtil.getBean("yyCollectInfoService");
                        YyCollectInfo yyciAdd = ycis1.get(strAddSum, strAddPart);
                        if (yyciAdd == null)
                        {
                            yyciAdd = new YyCollectInfo();
                            yyciAdd.createTime = DateTime.Now;
                            yyciAdd.partsCode = strAddPart;
                            yyciAdd.ProductNo = strProdNo;
                            yyciAdd.totalCode = strAddSum;
                            yyciAdd.sourcePlcIp = sender.ToString();
                            yyciAdd.Status = "1";
                            yyciAdd.sourceType = "1";
                            yyciAdd.Station = sender.ToString();

                            bool bIns = ycis1.insert(yyciAdd); //没有插入成功需要另外判断
                            if (bIns)
                            {
                                var plc = MyPlcManager.GetInstance().GetDeviceById(sender.ToString());
                                if (plc != null)
                                {
                                    plc.SetCoilStatus(plc.CoilsAddr, false);
                                }
                            }
                        }
                        else
                        {
                            var plc = MyPlcManager.GetInstance().GetDeviceById(sender.ToString());
                            if (plc != null)
                            {
                                plc.SetCoilStatus(plc.CoilsAddr, false);
                            }
                        }
                    }
                    else
                    {
                        //报警？
                    }
                    break;
                default:

                    break;
            }
            
            this.Invoke((EventHandler)delegate
            {
                if(txtMsg.Lines.Length > 20)
                {
                    txtMsg.Text = string.Empty;
                }
                txtMsg.Text += "设备:" + sender.ToString() + cmd + "发来数据:" + strTmp + "\r\n";
            });
        }

        #endregion End

        #region 主窗体事件
        private void mainFrm_Load(object sender, EventArgs e)
        {
            //SplasherManager.GetInstance().Show(typeof(frmSplash));
            //SplasherManager.GetInstance().Status = "模块加载中...";
            this.WindowState = FormWindowState.Maximized;
            InitMainUI();//MAP和TCHART
            //初始化
            MainManager.GetInstance().SetMainForm(this);
            MainManager.GetInstance().InitMain();
            string strErr = string.Empty;
            if(!MainManager.GetInstance().InitDevices(out strErr))
            {
                MessageBox.Show("设备初始化错误，请检查设备配置及连接状况后在启动服务！");
            }
            else
            {
                if(string.IsNullOrEmpty(strErr))
                {
                    startServer_Click(null, null);
                }
                else
                {
                    MessageBox.Show("设备打开错误:" + strErr + "请处理后在启动服务！");
                }
            }

            ShowDevsStatus();

            //LocalIpList = MainManager.GetInstance().getLocalIP();//获取本机IP地址
            //lLocalIP.AddRange(MainManager.GetInstance().getLocalIP());
            //SplasherManager.GetInstance().Close();
        }

        private void mainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("确定要退出程序？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        private void mainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProgramManager.GetInstance().Save();
            for (int i = 0; i < MyPlcManager.GetInstance().Devices.Count; i++)
            {
                try
                {
                    MyPlcManager.GetInstance().Devices[i].UnInit();
                }
                catch
                {
                }
            }
            if(thStatus != null)
            {
                thStatus.Abort();
                thStatus = null;
            }
            Environment.Exit(0);
        }
        #endregion End

        #region 配置操作

        private void skinTabCtrl_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Name)
            {
                case "tabQrCode":
                    if(yyQrcodeInfoItemList.Items.Count < 1)
                    {
                        yyQrcodeInfo_search_Click(null, null);
                    }
                    break;
                case "tabCollectList":
                    if(yyCollectInfoItemList.Items.Count < 1)
                    {
                        yyCollectInfo_search_Click(null, null);
                    }
                    break;
                case "tpgSearch":
                    break;
                case "tpgServer":
                    if(lvDevices.Items.Count < 1)
                    {
                        btnShowDev_Click(null, null);
                    }
                    break;
                case "tpgSysCfg":
                    if (productList.Items.Count < 1)
                    {
                        product_search_Click(null, null);
                    }
                    break;
                case "tpgAbout":

                    break;
                default:
                    break;
            }
        }

        #endregion End

        #region 查找与配置
       /* private void btnLogSearch_Click(object sender, EventArgs e)
        {
            string sqlstrLog = "select * from uselogs where userid like '" + txtLogUserId.Text + "%' and addtime between '" +
                               dptLogBegin.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + dptLogEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            DataSet dsLogs = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrLog, "uselogs");
            lvLogs.Items.Clear();
        }*/

        /*private void btnAlarmExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (this.lvAlarm.Items.Count <= 0)
                {
                    throw new Exception("无数据可导出");
                }
                string queryPath = System.Windows.Forms.Application.StartupPath + "\\query\\";
                if (!Directory.Exists(queryPath))
                    Directory.CreateDirectory(queryPath);
                string queryFilename = queryPath + "报警查询" + this.dtpAlarmBegin.Value.ToString("yyyyMMddHHmmss") + "-" + this.dtpAlarmEnd.Value.ToString("yyyyMMddHHmmss") + ".xls";
                List<ExcelColumnType> list = new List<ExcelColumnType>();

                if (ExcelHelper.SaveListViewToExcelFile(this.lvAlarm, list, queryFilename))
                {
                    FileInfo fileInfo = new FileInfo(queryFilename);
                    if (fileInfo.Length > 1024 * 1024 * 10)//大于10M提示
                    {
                        if (MessageBox.Show("导出文件成功，数据文件较大是否打开?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(queryFilename);
                        }
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(queryFilename);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }*/

        private void btnSiteSearch_Click(object sender, EventArgs e)
        {
            /*string sqlstrSite = "select devid,devaddr,devgps,devipaddr,devmac from siteinfo";
            DataSet dsSiteInfo = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSite, "siteinfo");
            lvSiteInfo.Items.Clear();
            if (dsSiteInfo.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsSiteInfo.Tables[0].Rows.Count; i++)
                {
                    SiteInfo sti = new SiteInfo();
                    sti.DevId = dsSiteInfo.Tables[0].Rows[i]["devid"].ToString();
                    sti.DevAddr = dsSiteInfo.Tables[0].Rows[i]["devaddr"].ToString();
                    sti.GPS = dsSiteInfo.Tables[0].Rows[i]["devgps"].ToString();
                    sti.IPAddrAndPort = dsSiteInfo.Tables[0].Rows[i]["devipaddr"].ToString();
                    sti.DevMac = dsSiteInfo.Tables[0].Rows[i]["devmac"].ToString();

                    List<string> lLvItem = new List<string>();
                    lLvItem.Clear();
                    lLvItem.Add((i + 1).ToString());
                    lLvItem.Add(sti.DevId);
                    lLvItem.Add(sti.DevAddr);
                    lLvItem.Add(sti.GPS);
                    lLvItem.Add(sti.IPAddrAndPort);
                    lLvItem.Add(sti.DevMac);

                    ListViewItem lvItem = new ListViewItem(lLvItem.ToArray());
                    lvItem.Tag = sti;
                    lvSiteInfo.Items.Add(lvItem);
                }
            }
            else
            {
                MessageBox.Show("查无数据！", "提示", MessageBoxButtons.OK);
            }*/
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            /*if (lvSiteInfo.SelectedItems.Count > 0)
            {

            }
            else
            {
                MessageBox.Show("未选中任何站点!");
            }*/
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            /*if (lvSiteInfo.SelectedItems.Count > 0)
            {

            }
            else
            {
                MessageBox.Show("未选中任何项!");
            }*/
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        }

        private void btnSiteExport_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btnSpectruSh_Click(object sender, EventArgs e)
        {
            string sqlstrSpec = "select collect_info.* from (select id, total_code, parts_code, status, create_time, del_time, source_type, source_plc_ip, op_id, product_no, station from yy_collect_info union all select id, total_code, parts_code, status, create_time, del_time, source_type, source_plc_ip, op_id, product_no, station from yy_collect_info_bak ) collect_info ";
            string pruuctCode=search_pruduct_code.Text;//产品编码
            string pruuctXh = search_pruduct_xh.Text;//产品型号
            string station = search_source_ip.Text;//产品工位
            DateTime startTime = search_start_time.Value;//开始时间
            DateTime endTime = search_end_time.Value;//结束时间
            string where = "1=1";
            if (pruuctCode!=null && !"".Equals(pruuctCode)) {
                where += " and collect_info.total_code like '%" + pruuctCode + "%' ";
            }
            if (station != null && !"".Equals(station))
            {
                where += " and collect_info.station like '%" + station + "%' ";
            }
            if (pruuctXh != null && !"".Equals(pruuctXh))
            {
                where += " and collect_info.product_no like '%" + pruuctXh + "%' ";
            }
            if (startTime != null && !"".Equals(startTime))
            {
                where += " and collect_info.create_time >= '" + startTime.ToString("yyyy-MM-dd") + "' ";
            }

            if (endTime != null && !"".Equals(endTime))
            {
                where += " and collect_info.create_time <= '" + endTime.ToString("yyyy-MM-dd") + " 23:59:59' ";
            }
            sqlstrSpec += " where " + where;
            sqlstrSpec += " order by collect_info.id desc limit 0,10000 ";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "search_collect_info");
            lvSpectrum.Items.Clear();
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {
                    List<string> lLvItem = new List<string>();
                    lLvItem.Clear();
                    lLvItem.Add((i + 1).ToString());
                    lLvItem.Add(dsSpectrum.Tables[0].Rows[i]["total_code"].ToString());
                    lLvItem.Add(dsSpectrum.Tables[0].Rows[i]["parts_code"].ToString()); 
                    lLvItem.Add(dsSpectrum.Tables[0].Rows[i]["product_no"].ToString());
                    lLvItem.Add(dsSpectrum.Tables[0].Rows[i]["station"].ToString());
                    lLvItem.Add(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString());

                    ListViewItem lvItem = new ListViewItem(lLvItem.ToArray());
                    lvSpectrum.Items.Add(lvItem);
                }
            }
            else
            {
                MessageBox.Show("查无数据！", "提示", MessageBoxButtons.OK);
            }
        }

        private void btnOutSpectrum_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (this.lvSpectrum.Items.Count <= 0)
                {
                    throw new Exception("无数据可导出");
                }
                string queryPath = System.Windows.Forms.Application.StartupPath + "\\query\\";
                if (!Directory.Exists(queryPath))
                    Directory.CreateDirectory(queryPath);
                string queryFilename = queryPath + "采集查询.xls";
                List<ExcelColumnType> list = new List<ExcelColumnType>();

                if (ExcelHelper.SaveListViewToExcelFile(this.lvSpectrum, list, queryFilename))
                {
                    FileInfo fileInfo = new FileInfo(queryFilename);
                    if (fileInfo.Length > 1024 * 1024 * 10)//大于10M提示
                    {
                        if (MessageBox.Show("导出文件成功，是否打开?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(queryFilename);
                        }
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(queryFilename);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }
        #endregion End

        #region 设备配置

        private void btnShowDev_Click(object sender, EventArgs e)
        {
            lvDevices.Items.Clear();
            if (MyPlcManager.GetInstance().Devices.Count > 0)
            {
                for (int i = 0; i < MyPlcManager.GetInstance().Devices.Count; i++)
                {

                    List<string> lLvItem = new List<string>();
                    lLvItem.Clear();
                    lLvItem.Add((i + 1).ToString());
                    lLvItem.Add(MyPlcManager.GetInstance().Devices[i].DeviceId);//
                    lLvItem.Add(MyPlcManager.GetInstance().Devices[i].DeviceName);//
                    lLvItem.Add(MyPlcManager.GetInstance().Devices[i].Addr.ToString());//
                    lLvItem.Add(MyPlcManager.GetInstance().Devices[i].IPAddr);//
                    lLvItem.Add(MyPlcManager.GetInstance().Devices[i].IpPort.ToString());//
                    lLvItem.Add(MyPlcManager.GetInstance().Devices[i].SiteOrder.ToString());//
                    lLvItem.Add(MyPlcManager.GetInstance().Devices[i].Connected.ToString());//
                    lLvItem.Add(MyPlcManager.GetInstance().Devices[i].CoilsAddr.ToString());//
                    lLvItem.Add(MyPlcManager.GetInstance().Devices[i].RegsAddr.ToString());//

                    ListViewItem lvItem = new ListViewItem(lLvItem.ToArray());
                    lvItem.Tag = MyPlcManager.GetInstance().Devices[i];
                    lvDevices.Items.Add(lvItem);
                }
            }
        }

        private void btnAddDev_Click(object sender, EventArgs e)
        {
            frmDevice fDev = new frmDevice(null);
            if(fDev.ShowDialog() == DialogResult.OK)
            {
                lock(MyPlcManager.GetInstance().Devices)
                {
                    MyPlcManager.GetInstance().Devices.Add(fDev.Device);
                    MyPlcManager.GetInstance().Save();//保存
                                                      //重新加载显示
                    btnShowDev_Click(sender, e);
                }
            }
        }

        private void btnEditDev_Click(object sender, EventArgs e)
        {
            if (lvDevices.SelectedItems.Count > 0)
            {
                frmDevice fDev = new frmDevice(lvDevices.SelectedItems[0].Tag as AcqPlc);
                if (fDev.ShowDialog() == DialogResult.OK)
                {
                    MyPlcManager.GetInstance().Save();//保存更新
                    //重新加载显示
                    btnShowDev_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show("未选中任何项!");
            }
        }

        private void btnDelDev_Click(object sender, EventArgs e)
        {
            if (lvDevices.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("确认删除该设备？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    lock (MyPlcManager.GetInstance().Devices)
                    {
                        AcqPlc sItem = lvDevices.SelectedItems[0].Tag as AcqPlc;
                        MyPlcManager.GetInstance().RemoveDevice(sItem.DeviceId);//保存更新
                        MyPlcManager.GetInstance().Save();
                        //重新加载显示
                        btnShowDev_Click(sender, e);
                    }
                }
            }
            else
            {
                MessageBox.Show("未选中任何站点!");
            }
        }

        #endregion End

        private void tabSysSet_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Name)
            {
                case "tpgset_scopes":
                    break;
                case "tpgset_users":
                    break;
                case "tabProduct1":
                    product_search_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void tabCtrlSearch_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Name)
            {
                case "tpg_Site":
                    break;
                case "":
                    break;
            }
        }

        private void product_search_Click(object sender, EventArgs e)
        {
            SysProductInfoService sysProductInfoService = (SysProductInfoService)BeanUtil.getBean("sysProductInfoService");
            List<SysProductInfo> productInfoList=sysProductInfoService.list(productNo.Text);
            productList.Items.Clear();
            if (productInfoList!=null && productInfoList.Count>0)
            {
                for (int i = 0; i < productInfoList.Count; i++)
                {

                    SysProductInfo spi = productInfoList[i];

                    List<string> lLvItem = new List<string>();
                    lLvItem.Clear();
                    lLvItem.Add((i + 1).ToString());
                    //lLvItem.Add(dsSpectrum.Tables[0].Rows[i]["id"].ToString());
                    lLvItem.Add(spi.Name);
                    lLvItem.Add(spi.No);
                    lLvItem.Add(spi.Code);
                    lLvItem.Add(spi.createTime.ToString());

                    ListViewItem lvItem = new ListViewItem(lLvItem.ToArray());
                    lvItem.Tag = spi;
                    productList.Items.Add(lvItem);
                }
            }
            else
            {
                MessageBox.Show("查无数据！", "提示", MessageBoxButtons.OK);
            }
        }

        private void productAdd_Click(object sender, EventArgs e)
        {
            //先打开窗体
            addProduct aProduct = new addProduct(null);
            if (aProduct.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SysProductInfoService sysProductInfoService = (SysProductInfoService)BeanUtil.getBean("sysProductInfoService");
                bool status=sysProductInfoService.insert(aProduct.GetProductInfo);
                int maxint=sysProductInfoService.maxIndex();
                SysProductPartsService sysProductPartsService = (SysProductPartsService)BeanUtil.getBean("sysProductPartsService");
                List<SysProductParts>  partsList=aProduct.GetSysProductPartsList;
                if (partsList!=null && partsList.Count>0) {
                    for (int i=0,len=partsList.Count;i<len;i++) {
                        SysProductParts part = partsList[i];
                        part.productId = maxint;
                        sysProductPartsService.insert(part);
                    }
                }
                if (status)
                {
                    product_search_Click(sender, e);
                }
                else {
                    MessageBox.Show("保存数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                }

            }
        }

        private void productEdit_Click(object sender, EventArgs e)
        {

            if (productList.SelectedItems.Count > 0)
            {
                addProduct aProduct = new addProduct(productList.SelectedItems[0].Tag as SysProductInfo);
                if (aProduct.ShowDialog() == DialogResult.OK)
                {
                    SysProductInfoService sysProductInfoService = (SysProductInfoService)BeanUtil.getBean("sysProductInfoService");
                    bool status = sysProductInfoService.update(aProduct.GetProductInfo);

                    SysProductPartsService sysProductPartsService = (SysProductPartsService)BeanUtil.getBean("sysProductPartsService");
                    sysProductPartsService.deleteProduct(aProduct.GetProductInfo.ID);
                    List<SysProductParts> partsList = aProduct.GetSysProductPartsList;
                    if (partsList != null && partsList.Count > 0)
                    {
                        for (int i = 0, len = partsList.Count; i < len; i++)
                        {
                            SysProductParts part = partsList[i];
                            part.productId = aProduct.GetProductInfo.ID;
                            sysProductPartsService.insert(part);
                        }
                    }
                    if (status)
                    {
                        product_search_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("修改数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                    }
                }
            }
           
        }

        private void productDelete_Click(object sender, EventArgs e)
        {
            if (productList.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("确认删除该产品？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    SysProductInfo product = productList.SelectedItems[0].Tag as SysProductInfo;
                    SysProductInfoService sysProductInfoService = (SysProductInfoService)BeanUtil.getBean("sysProductInfoService");
                    bool status = sysProductInfoService.delete(product.ID);
                    if (status)
                    {
                        product_search_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("修改数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                    }

                }
            }
            else
            {
                MessageBox.Show("未选中任何产品");
            }
        }

        private void sysPlcInfo_search_Click(object sender, EventArgs e)
        {
            SysPlcInfoService sysPlcInfoService = (SysPlcInfoService)BeanUtil.getBean("sysPlcInfoService");
            List<SysPlcInfo> sysPlcInfoList = sysPlcInfoService.list(searchName.Text);
            sysPlcInfoItemList.Items.Clear();
            if (sysPlcInfoList != null && sysPlcInfoList.Count > 0)
            {
                for (int i = 0; i < sysPlcInfoList.Count; i++)
                {

                    SysPlcInfo spi = sysPlcInfoList[i];

                    List<string> lLvItem = new List<string>();
                    lLvItem.Clear();
                    lLvItem.Add((i + 1).ToString());
                    lLvItem.Add(spi.Name.ToString());
                    lLvItem.Add(spi.Ip.ToString());
                    lLvItem.Add(spi.Port.ToString());
                    lLvItem.Add(spi.Station.ToString());
                    lLvItem.Add(spi.delAddress.ToString());
                    lLvItem.Add(spi.totalAddress.ToString());
                    lLvItem.Add(spi.partsAddress.ToString());
                    lLvItem.Add(spi.validAddress.ToString());
                    lLvItem.Add(spi.writeAddress.ToString());
                    lLvItem.Add(spi.Info.ToString());
                    lLvItem.Add(spi.createTime.ToString());
                    lLvItem.Add(spi.alterTime.ToString());
                    lLvItem.Add(spi.opId.ToString());
                    lLvItem.Add(spi.opName.ToString());

                    ListViewItem lvItem = new ListViewItem(lLvItem.ToArray());
                    lvItem.Tag = spi;
                    sysPlcInfoItemList.Items.Add(lvItem);
                }
            }
            else
            {
                MessageBox.Show("查无数据！", "提示", MessageBoxButtons.OK);
            }
        }

        private void sysPlcInfoAdd_Click(object sender, EventArgs e)
        {
            //先打开窗体
            SysPlcInfoForm sysPlcInfoForm = new SysPlcInfoForm(null);
            if (sysPlcInfoForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SysPlcInfoService sysPlcInfoService = (SysPlcInfoService)BeanUtil.getBean("sysPlcInfoService");
                bool status = sysPlcInfoService.insert(sysPlcInfoForm.GetSysPlcInfo);
                if (status)
                {
                    sysPlcInfo_search_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("保存数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                }

            }
        }

        private void sysPlcInfoEdit_Click(object sender, EventArgs e)
        {

            if (sysPlcInfoItemList.SelectedItems.Count > 0)
            {
                SysPlcInfoForm sysPlcInfoForm = new SysPlcInfoForm(sysPlcInfoItemList.SelectedItems[0].Tag as SysPlcInfo);
                if (sysPlcInfoForm.ShowDialog() == DialogResult.OK)
                {

                    SysPlcInfoService sysPlcInfoService = (SysPlcInfoService)BeanUtil.getBean("sysPlcInfoService");
                    bool status = sysPlcInfoService.update(sysPlcInfoForm.GetSysPlcInfo);
                    if (status)
                    {
                        sysPlcInfo_search_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("修改数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                    }
                }
            }

        }

        private void sysPlcInfoDelete_Click(object sender, EventArgs e)
        {
            if (sysPlcInfoItemList.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("确认删除该产品？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    SysPlcInfo product = sysPlcInfoItemList.SelectedItems[0].Tag as SysPlcInfo;
                    SysPlcInfoService sysPlcInfoService = (SysPlcInfoService)BeanUtil.getBean("sysPlcInfoService");
                    bool status = sysPlcInfoService.delete(product.ID);
                    if (status)
                    {
                        sysPlcInfo_search_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("修改数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                    }

                }
            }
            else
            {
                MessageBox.Show("未选中任何产品");
            }
        }

        private void yyQrcodeInfo_search_Click(object sender, EventArgs e)
        {
            YyQrcodeInfoService yyQrcodeInfoService = (YyQrcodeInfoService)BeanUtil.getBean("yyQrcodeInfoService");
            List<YyQrcodeInfo> yyQrcodeInfoList = yyQrcodeInfoService.list(qrsearchName.Text);
            yyQrcodeInfoItemList.Items.Clear();
            if (yyQrcodeInfoList != null && yyQrcodeInfoList.Count > 0)
            {
                for (int i = 0; i < yyQrcodeInfoList.Count; i++)
                {

                    YyQrcodeInfo spi = yyQrcodeInfoList[i];

                    List<string> lLvItem = new List<string>();
                    lLvItem.Clear();
                    lLvItem.Add((i + 1).ToString());
                    lLvItem.Add(spi.totalCode.ToString());
                    lLvItem.Add(spi.sourceIp.ToString());
                    lLvItem.Add(spi.createTime.ToString());
                    lLvItem.Add(spi.qualifiedStatus.ToString());
                    lLvItem.Add(spi.printStatus.ToString());
                    lLvItem.Add(spi.printTime.ToString());

                    ListViewItem lvItem = new ListViewItem(lLvItem.ToArray());
                    lvItem.Tag = spi;
                    yyQrcodeInfoItemList.Items.Add(lvItem);
                }
            }
            else
            {
                MessageBox.Show("查无数据！", "提示", MessageBoxButtons.OK);
            }
        }

        private void yyQrcodeInfoAdd_Click(object sender, EventArgs e)
        {
            //先打开窗体
            YyQrcodeInfoForm yyQrcodeInfoForm = new YyQrcodeInfoForm(null);
            if (yyQrcodeInfoForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                YyQrcodeInfoService yyQrcodeInfoService = (YyQrcodeInfoService)BeanUtil.getBean("yyQrcodeInfoService");
                bool status = yyQrcodeInfoService.insert(yyQrcodeInfoForm.GetYyQrcodeInfo);
                if (status)
                {
                    yyQrcodeInfo_search_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("保存数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                }

            }
        }

        private void yyQrcodeInfoEdit_Click(object sender, EventArgs e)
        {

            if (yyQrcodeInfoItemList.SelectedItems.Count > 0)
            {
                YyQrcodeInfoForm yyQrcodeInfoForm = new YyQrcodeInfoForm(yyQrcodeInfoItemList.SelectedItems[0].Tag as YyQrcodeInfo);
                if (yyQrcodeInfoForm.ShowDialog() == DialogResult.OK)
                {

                    YyQrcodeInfoService yyQrcodeInfoService = (YyQrcodeInfoService)BeanUtil.getBean("yyQrcodeInfoService");
                    bool status = yyQrcodeInfoService.update(yyQrcodeInfoForm.GetYyQrcodeInfo);
                    if (status)
                    {
                        yyQrcodeInfo_search_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("修改数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                    }
                }
            }

        }

        private void yyQrcodeInfoDelete_Click(object sender, EventArgs e)
        {
            if (yyQrcodeInfoItemList.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("确认删除该产品？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    YyQrcodeInfo product = yyQrcodeInfoItemList.SelectedItems[0].Tag as YyQrcodeInfo;
                    YyQrcodeInfoService yyQrcodeInfoService = (YyQrcodeInfoService)BeanUtil.getBean("yyQrcodeInfoService");
                    bool status = yyQrcodeInfoService.delete(product.ID);
                    if (status)
                    {
                        yyQrcodeInfo_search_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("修改数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                    }

                }
            }
            else
            {
                MessageBox.Show("未选中任何产品");
            }
        }

        private void yyCollectInfo_search_Click(object sender, EventArgs e)
        {
            YyCollectInfoService yyCollectInfoService = (YyCollectInfoService)BeanUtil.getBean("yyCollectInfoService");
            List<YyCollectInfo> yyCollectInfoList = yyCollectInfoService.list(yyCollectInfo_searchName.Text);
            yyCollectInfoItemList.Items.Clear();
            if (yyCollectInfoList != null && yyCollectInfoList.Count > 0)
            {
                for (int i = 0; i < yyCollectInfoList.Count; i++)
                {

                    YyCollectInfo spi = yyCollectInfoList[i];

                    List<string> lLvItem = new List<string>();
                    lLvItem.Clear();
                    lLvItem.Add((i + 1).ToString());
                    lLvItem.Add(ModelsUtil.toString(spi.totalCode));
                    lLvItem.Add(ModelsUtil.toString(spi.partsCode));
                    lLvItem.Add(ModelsUtil.toString(spi.ProductNo));
                    lLvItem.Add(ModelsUtil.toString(spi.sourcePlcIp));
                    string status = ModelsUtil.toString(spi.Status);
                    string statusStr = "";
                    if (status == "0")
                    {
                        statusStr = "已删除";
                    }
                    else {
                        statusStr = "正常";
                    }
                    lLvItem.Add(statusStr);
                    lLvItem.Add(ModelsUtil.toString(spi.createTime));
                    ListViewItem lvItem = new ListViewItem(lLvItem.ToArray());
                    lvItem.Tag = spi;
                    yyCollectInfoItemList.Items.Add(lvItem);
                }
            }
            else
            {
                MessageBox.Show("查无数据！", "提示", MessageBoxButtons.OK);
            }
        }

        private void yyCollectInfoAdd_Click(object sender, EventArgs e)
        {
            //先打开窗体
            YyCollectInfoForm yyCollectInfoForm = new YyCollectInfoForm(null);
            if (yyCollectInfoForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                YyCollectInfoService yyCollectInfoService = (YyCollectInfoService)BeanUtil.getBean("yyCollectInfoService");
                bool status = yyCollectInfoService.insert(yyCollectInfoForm.GetYyCollectInfo);
                if (status)
                {
                    yyCollectInfo_search_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("保存数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                }

            }
        }

        private void yyCollectInfoEdit_Click(object sender, EventArgs e)
        {

            if (yyCollectInfoItemList.SelectedItems.Count > 0)
            {
                YyCollectInfoForm yyCollectInfoForm = new YyCollectInfoForm(yyCollectInfoItemList.SelectedItems[0].Tag as YyCollectInfo);
                if (yyCollectInfoForm.ShowDialog() == DialogResult.OK)
                {

                    YyCollectInfoService yyCollectInfoService = (YyCollectInfoService)BeanUtil.getBean("yyCollectInfoService");
                    bool status = yyCollectInfoService.update(yyCollectInfoForm.GetYyCollectInfo);
                    if (status)
                    {
                        yyCollectInfo_search_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("修改数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                    }
                }
            }

        }

        private void yyCollectInfoDelete_Click(object sender, EventArgs e)
        {
            if (yyCollectInfoItemList.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("确认删除该产品？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    YyCollectInfo product = yyCollectInfoItemList.SelectedItems[0].Tag as YyCollectInfo;
                    YyCollectInfoService yyCollectInfoService = (YyCollectInfoService)BeanUtil.getBean("yyCollectInfoService");
                    bool status = yyCollectInfoService.delete(product.ID);
                    if (status)
                    {
                        yyCollectInfo_search_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("修改数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
                    }

                }
            }
            else
            {
                MessageBox.Show("未选中任何产品");
            }
        }


        private void sysBaseInfoSave_Click(object sender, EventArgs e)
        {
            /*SysBaseInfoService sysBaseInfoService = (SysBaseInfoService)BeanUtil.getBean("sysBaseInfoService");
            SysBaseInfo sysBaseInfo = sysBaseInfoService.getBaseInfo();//获取
            bool isAdd = false;
            if (sysBaseInfo == null)
            {
                sysBaseInfo = new SysBaseInfo();
                sysBaseInfo.createTime = DateTime.Now;
                isAdd = true;
            }
            sysBaseInfo.collectRate = sysBaseInfo_collectRate.Text;
            sysBaseInfo.qrcodeLength = sysBaseInfo_qrcodeLength.Text;
            sysBaseInfo.companyName = sysBaseInfo_companyName.Text;
            sysBaseInfo.workshopName = sysBaseInfo_workshopName.Text;
            sysBaseInfo.lineName = sysBaseInfo_lineName.Text;
            sysBaseInfo.logoPath = sysBaseInfo_logoPath.Text;
            sysBaseInfo.Sign = sysBaseInfo_Sign.Text;
            sysBaseInfo.alterTime = DateTime.Now;
            bool status = false;
            if (isAdd)
            {
                status = sysBaseInfoService.insert(sysBaseInfo);
            }
            else
            {
                status = sysBaseInfoService.update(sysBaseInfo);
            }

            if (status)
            {
                MessageBox.Show("更新基础配置成功！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("修改数据异常，请联系管理员！", "提示", MessageBoxButtons.OK);
            }*/
        }

        private void sysBaseInfoLoad_Click(object sender, EventArgs e)
        {

            /*SysBaseInfoService sysBaseInfoService = (SysBaseInfoService)BeanUtil.getBean("sysBaseInfoService");
            SysBaseInfo sysBaseInfo = sysBaseInfoService.getBaseInfo();//获取

            sysBaseInfo_collectRate.Text = sysBaseInfo.collectRate;
            sysBaseInfo_qrcodeLength.Text = sysBaseInfo.qrcodeLength;
            sysBaseInfo_companyName.Text = sysBaseInfo.companyName;
            sysBaseInfo_workshopName.Text = sysBaseInfo.workshopName;
            sysBaseInfo_lineName.Text = sysBaseInfo.lineName;
            sysBaseInfo_logoPath.Text = sysBaseInfo.logoPath;
            sysBaseInfo_Sign.Text = sysBaseInfo.Sign;*/
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            if(btnServer.Text == "启动服务")
            {
                for (int i = 0; i < MyPlcManager.GetInstance().Devices.Count; i++)
                {
                    MyPlcManager.GetInstance().Devices[i].DataDeal += DealReadData;
                }
                YyOpSwitch yyOpSwitch = new YyOpSwitch();
                yyOpSwitch.createTime = DateTime.Now;
                yyOpSwitch.actionNo = "start";
                yyOpSwitch.actionName = "启动监控";
                yyOpSwitch.opId = UserUtil.OpId;
                yyOpSwitch.opName = UserUtil.OpName;
                YyOpSwitchService yyOpSwitchService = (YyOpSwitchService)BeanUtil.getBean("yyOpSwitchService");
                yyOpSwitchService.insert(yyOpSwitch);

                btnServer.Text = "停止服务";
            }
            else
            {
                for (int i = 0; i < MyPlcManager.GetInstance().Devices.Count; i++)
                {
                    try
                    {
                        MyPlcManager.GetInstance().Devices[i].DataDeal -= DealReadData;
                    }
                    catch
                    {
                    }
                }
                YyOpSwitch yyOpSwitch = new YyOpSwitch();
                yyOpSwitch.createTime = DateTime.Now;
                yyOpSwitch.actionNo = "stop";
                yyOpSwitch.actionName = "停止监控";
                yyOpSwitch.opId = UserUtil.OpId;
                yyOpSwitch.opName = UserUtil.OpName;
                YyOpSwitchService yyOpSwitchService = (YyOpSwitchService)BeanUtil.getBean("yyOpSwitchService");
                yyOpSwitchService.insert(yyOpSwitch);
                btnServer.Text = "启动服务";
            }
            //closeServer.Visible = true;
            //btnServer.Visible = false;
            //根据情况记录
        }

        private void txtQrCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQrSearch_Click(sender, e);
            }
        }

        private void btnQrSearch_Click(object sender, EventArgs e)
        {
            //qrImage.QrContent = "QJ00422242";
            //qrImage.SetQrImage("216000.19122816-0105.J002d.815001a;082000.1911091Z-0962.J024b.8150040;013000.19123044-0062.J0090.8150050;013000.19123044-0047.J0090.8150050");

            //qrImage.PrintQrImg();
            //return;
            string strQrcode = txtQrCode.Text.Trim();
            txtQrCode.Text = string.Empty;
            if (string.IsNullOrEmpty(strQrcode))
            {
                txtAlarmInfo.Text += "查询条码数据不能为空！\r\n";
                MessageBox.Show("查询条码数据不能为空！");
                return;
            }
            else
            {
                SysProductInfoService spIS = (SysProductInfoService)BeanUtil.getBean("sysProductInfoService");
                var productInfo = spIS.getProduct(strQrcode);

                string printName = string.Empty;
                if (productInfo == null)
                {
                    MessageBox.Show("未找到" + strQrcode + "配置！");
                    return;
                }
                else
                {
                    printName = productInfo.printName;
                }

                YyQrcodeInfoService yyQIS = (YyQrcodeInfoService)BeanUtil.getBean("yyQrcodeInfoService");
                List<YyQrcodeInfo> lyyQrInfos = yyQIS.list(strQrcode);
                if (lyyQrInfos != null && lyyQrInfos.Count > 0)
                {
                    qrImage.QrContent = printName;
                    qrImage.SetQrImage(lyyQrInfos[0].totalQrCode);

                    qrImage.PrintQrImg();

                    txtMsg.Text += "打印总二维码：" + lyyQrInfos[0].totalCode;
                }
                else
                {
                    YyCollectInfoService ycis1 = (YyCollectInfoService)BeanUtil.getBean("yyCollectInfoService");
                    List<YyCollectInfo> ycParts = ycis1.list(strQrcode);
                    if (ycParts == null || ycParts.Count < 1)
                    {
                        txtAlarmInfo.Text += "总二维码：" + strQrcode + "还未采集或未采集完成！\r\n";
                        MessageBox.Show("总二维码：" + strQrcode + "还未采集！");
                        return;
                    }
                    else
                    {
                        SysProductInfoService spis = (SysProductInfoService)BeanUtil.getBean("sysProductInfoService");
                        string strErr = string.Empty;
                        bool bCanParcket = spis.validateProduct(strQrcode, ycParts,out strErr);
                        if (bCanParcket)
                        {
                            YyQrcodeInfo yyQrInfo = new YyQrcodeInfo();
                            yyQrInfo.totalCode = strQrcode;
                            string partsCodeJoin= PackQrcode(ycParts);
                            yyQrInfo.totalQrCode = strQrcode+";"+ partsCodeJoin;
                            yyQrInfo.createTime = DateTime.Now;
                            yyQrInfo.printTime = DateTime.Now;
                            yyQrInfo.printStatus = "1";
                            yyQIS.insert(yyQrInfo);

                            qrImage.QrContent = printName;
                            qrImage.SetQrImage(yyQrInfo.totalQrCode);

                            qrImage.PrintQrImg();

                            txtAlarmInfo.Text += "打印总二维码：" + strQrcode;
                        }
                        else
                        {
                            txtAlarmInfo.Text += "总二维码：" + strQrcode + "还未采集完整！\r\n";
                            txtAlarmInfo.Text += strErr;
                            MessageBox.Show("采集未完整:\r\n" + strErr);
                            return;
                        }
                    }
                }
            }
        }

        private string PackQrcode(List<YyCollectInfo> lParts)
        {
            if (lParts == null || lParts.Count == 0)
            {
                return "";
            }
            StringBuilder sbPack = new StringBuilder();
            try
            {
                for (int i = 0; i < lParts.Count; i++)
                {
                    if (lParts[i].Status != "0")
                    {
                        sbPack.Append(lParts[i].partsCode).Append(";");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            return sbPack.ToString().Trim(';');
        }

        private void imgPrint_PrintPage(object sender, PrintPageEventArgs e)
        {
            //int iLeft = (int)nudLeft.Value;
            //int iTop = (int)nudUp.Value;
            //Bitmap newBitmap = new Bitmap(qrImage.Width, qrImage.Height);
            //qrImage.DrawToBitmap(newBitmap, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height));
            //e.Graphics.Clear(Color.White);
            //e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //e.Graphics.DrawImage(newBitmap, iLeft, iTop, newBitmap.Width, newBitmap.Height);
        }

        private void yySearchClear_Click(object sender, EventArgs e)
        {
            search_start_time.Value = DateTime.Parse("2020/01/01");
            search_end_time.Value = DateTime.Parse("2029/12/31");
            search_pruduct_code.Text="";//产品编码
             search_pruduct_xh.Text="";//产品型号
            search_source_ip.Text="";//产品工位
        }

        private void nudUp_ValueChanged(object sender, EventArgs e)
        {
            ProgramManager.GetInstance().MarginTop = (int)nudUp.Value;
            qrImage.PrintMarginTop = ProgramManager.GetInstance().MarginTop;
        }

        private void nudLeft_ValueChanged(object sender, EventArgs e)
        {
            ProgramManager.GetInstance().MarginLeft = (int)nudLeft.Value;
            qrImage.PrintMarginLeft = ProgramManager.GetInstance().MarginLeft;
        }

        private void nudRight_ValueChanged(object sender, EventArgs e)
        {
            ProgramManager.GetInstance().MarginRight = (int)nudRight.Value;
            qrImage.PrintMarginRight = ProgramManager.GetInstance().MarginRight;
        }

        private void nudDown_ValueChanged(object sender, EventArgs e)
        {
            ProgramManager.GetInstance().MarginBottom = (int)nudDown.Value;
            qrImage.PrintMarginBottom = ProgramManager.GetInstance().MarginBottom;
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            ProgramManager.GetInstance().QrHeight = (int)nudHeight.Value;
            qrImage.SetPage(ProgramManager.GetInstance().QrWidth, ProgramManager.GetInstance().QrHeight);
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            ProgramManager.GetInstance().QrWidth = (int)nudWidth.Value;
            qrImage.SetPage(ProgramManager.GetInstance().QrWidth, ProgramManager.GetInstance().QrHeight);
        }

        private void nudFontSize_ValueChanged(object sender, EventArgs e)
        {
            ProgramManager.GetInstance().QrFontSize = (float)nudFontSize.Value;
            qrImage.FontSize = ProgramManager.GetInstance().QrFontSize;
        }
    }
}
