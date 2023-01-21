using GMap.NET;
namespace Yu3zx.Moonlit
{
    partial class mainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFrm));
            CCWin.SkinControl.Animation animation1 = new CCWin.SkinControl.Animation();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tabQrCode = new System.Windows.Forms.TabPage();
            this.panel12 = new System.Windows.Forms.Panel();
            this.yyQrcodeInfoItemList = new System.Windows.Forms.ListView();
            this.columnHeader33 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader34 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader35 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader36 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader37 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader38 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader39 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel11 = new System.Windows.Forms.Panel();
            this.yyQrcodeInfo_search = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.qrsearchName = new System.Windows.Forms.TextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.yyQrcodeInfoDelete = new System.Windows.Forms.Button();
            this.yyQrcodeInfoEdit = new System.Windows.Forms.Button();
            this.yyQrcodeInfoAdd = new System.Windows.Forms.Button();
            this.tabCollectList = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.yyCollectInfoItemList = new System.Windows.Forms.ListView();
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader32 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel8 = new System.Windows.Forms.Panel();
            this.yyCollectInfo_search = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.yyCollectInfo_searchName = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.yyCollectInfoDelete = new System.Windows.Forms.Button();
            this.yyCollectInfoEdit = new System.Windows.Forms.Button();
            this.yyCollectInfoAdd = new System.Windows.Forms.Button();
            this.tpgAbout = new System.Windows.Forms.TabPage();
            this.tpgSearch = new System.Windows.Forms.TabPage();
            this.tabCtrlSearch = new System.Windows.Forms.TabControl();
            this.tpg_Spectrum = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvSpectrum = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlSpem = new System.Windows.Forms.Panel();
            this.yySearchClear = new System.Windows.Forms.Button();
            this.search_source_ip = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.search_pruduct_xh = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.search_pruduct_code = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.search_end_time = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.search_start_time = new System.Windows.Forms.DateTimePicker();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.btnSearchAll = new System.Windows.Forms.Button();
            this.tpgSysCfg = new System.Windows.Forms.TabPage();
            this.tabSysSet = new System.Windows.Forms.TabControl();
            this.tabProduct1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.productList = new System.Windows.Forms.ListView();
            this.column_product_no = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_product_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_product_code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_product_mark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_product_create = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel4 = new System.Windows.Forms.Panel();
            this.productEdit = new System.Windows.Forms.Button();
            this.productDelete = new System.Windows.Forms.Button();
            this.productAdd = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.productNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.product_search = new System.Windows.Forms.Button();
            this.tabPLCInfo = new System.Windows.Forms.TabPage();
            this.sysPlcInfoItemList = new System.Windows.Forms.ListView();
            this.col_index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_station = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_create_time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_op_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel14 = new System.Windows.Forms.Panel();
            this.sysPlcInfo_search = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.searchName = new System.Windows.Forms.TextBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.sysPlcInfoAdd = new System.Windows.Forms.Button();
            this.sysPlcInfoDelete = new System.Windows.Forms.Button();
            this.sysPlcInfoEdit = new System.Windows.Forms.Button();
            this.tpgServer = new System.Windows.Forms.TabPage();
            this.pnlFoot = new System.Windows.Forms.Panel();
            this.lvDevices = new System.Windows.Forms.ListView();
            this.colXH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDevid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDevName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSiteAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIPAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIpPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLinkState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCoilsAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRegAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlScopeEdit = new System.Windows.Forms.Panel();
            this.btnShowDev = new System.Windows.Forms.Button();
            this.btnEditDev = new System.Windows.Forms.Button();
            this.btnDelDev = new System.Windows.Forms.Button();
            this.btnAddDev = new System.Windows.Forms.Button();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.gpbMainServer = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpgMapMode = new System.Windows.Forms.TabPage();
            this.pnlHome = new System.Windows.Forms.Panel();
            this.tlpHome = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.gpbDevStatus = new System.Windows.Forms.GroupBox();
            this.flpDevices = new System.Windows.Forms.FlowLayoutPanel();
            this.btnServer = new System.Windows.Forms.Button();
            this.tblPrint = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtQrCode = new System.Windows.Forms.TextBox();
            this.btnQrSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.nudRight = new System.Windows.Forms.NumericUpDown();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.nudDown = new System.Windows.Forms.NumericUpDown();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.nudUp = new System.Windows.Forms.NumericUpDown();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.qrImage = new Yu3zx.Moonlit.QrCtrl.QrImage();
            this.pnlHeight = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.pnlWidth = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.pnlFont = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtAlarmInfo = new System.Windows.Forms.TextBox();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.skinTabCtrl = new CCWin.SkinControl.SkinTabControl();
            this.imgPrint = new System.Drawing.Printing.PrintDocument();
            this.tabQrCode.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tabCollectList.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tpgSearch.SuspendLayout();
            this.tabCtrlSearch.SuspendLayout();
            this.tpg_Spectrum.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlSpem.SuspendLayout();
            this.tpgSysCfg.SuspendLayout();
            this.tabSysSet.SuspendLayout();
            this.tabProduct1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPLCInfo.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel13.SuspendLayout();
            this.tpgServer.SuspendLayout();
            this.pnlFoot.SuspendLayout();
            this.pnlScopeEdit.SuspendLayout();
            this.pnlHead.SuspendLayout();
            this.gpbMainServer.SuspendLayout();
            this.tpgMapMode.SuspendLayout();
            this.pnlHome.SuspendLayout();
            this.tlpHome.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpbDevStatus.SuspendLayout();
            this.tblPrint.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDown)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUp)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.pnlHeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.pnlWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.pnlFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.skinTabCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "ico_SysRepair.png");
            this.imageList.Images.SetKeyName(1, "aboutus.png");
            this.imageList.Images.SetKeyName(2, "ico_PluginCleaner.png");
            this.imageList.Images.SetKeyName(3, "sysconfig.png");
            this.imageList.Images.SetKeyName(4, "systestauto.png");
            this.imageList.Images.SetKeyName(5, "deviceconfig.png");
            this.imageList.Images.SetKeyName(6, "datasearch.png");
            this.imageList.Images.SetKeyName(7, "sysconnection.png");
            this.imageList.Images.SetKeyName(8, "mapnew.png");
            this.imageList.Images.SetKeyName(9, "startserver.png");
            this.imageList.Images.SetKeyName(10, "product.png");
            this.imageList.Images.SetKeyName(11, "采集图标.png");
            this.imageList.Images.SetKeyName(12, "查询1.png");
            this.imageList.Images.SetKeyName(13, "查询2.png");
            this.imageList.Images.SetKeyName(14, "服务器1.png");
            this.imageList.Images.SetKeyName(15, "关于我们1.png");
            this.imageList.Images.SetKeyName(16, "关于我们2.png");
            this.imageList.Images.SetKeyName(17, "监控设置.png");
            this.imageList.Images.SetKeyName(18, "监控设置1.png");
            this.imageList.Images.SetKeyName(19, "设置.png");
            this.imageList.Images.SetKeyName(20, "设置1.png");
            this.imageList.Images.SetKeyName(21, "设置2.png");
            this.imageList.Images.SetKeyName(22, "设置3.png");
            this.imageList.Images.SetKeyName(23, "简版二维码.png");
            // 
            // tabQrCode
            // 
            this.tabQrCode.Controls.Add(this.panel12);
            this.tabQrCode.Controls.Add(this.panel11);
            this.tabQrCode.Controls.Add(this.panel10);
            this.tabQrCode.ImageKey = "简版二维码.png";
            this.tabQrCode.Location = new System.Drawing.Point(0, 75);
            this.tabQrCode.Name = "tabQrCode";
            this.tabQrCode.Size = new System.Drawing.Size(1016, 613);
            this.tabQrCode.TabIndex = 11;
            this.tabQrCode.Text = "总成二维码";
            this.tabQrCode.UseVisualStyleBackColor = true;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.yyQrcodeInfoItemList);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 67);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1016, 466);
            this.panel12.TabIndex = 16;
            // 
            // yyQrcodeInfoItemList
            // 
            this.yyQrcodeInfoItemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader33,
            this.columnHeader34,
            this.columnHeader35,
            this.columnHeader36,
            this.columnHeader37,
            this.columnHeader38,
            this.columnHeader39});
            this.yyQrcodeInfoItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yyQrcodeInfoItemList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.yyQrcodeInfoItemList.FullRowSelect = true;
            this.yyQrcodeInfoItemList.GridLines = true;
            this.yyQrcodeInfoItemList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.yyQrcodeInfoItemList.HideSelection = false;
            this.yyQrcodeInfoItemList.LabelWrap = false;
            this.yyQrcodeInfoItemList.Location = new System.Drawing.Point(0, 0);
            this.yyQrcodeInfoItemList.Name = "yyQrcodeInfoItemList";
            this.yyQrcodeInfoItemList.Size = new System.Drawing.Size(1016, 466);
            this.yyQrcodeInfoItemList.TabIndex = 10;
            this.yyQrcodeInfoItemList.UseCompatibleStateImageBehavior = false;
            this.yyQrcodeInfoItemList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader33
            // 
            this.columnHeader33.Text = "序号";
            this.columnHeader33.Width = 98;
            // 
            // columnHeader34
            // 
            this.columnHeader34.Text = "总二维码";
            this.columnHeader34.Width = 280;
            // 
            // columnHeader35
            // 
            this.columnHeader35.Text = "来源IP";
            this.columnHeader35.Width = 96;
            // 
            // columnHeader36
            // 
            this.columnHeader36.Text = "录入时间";
            this.columnHeader36.Width = 170;
            // 
            // columnHeader37
            // 
            this.columnHeader37.Text = "校验状态";
            this.columnHeader37.Width = 131;
            // 
            // columnHeader38
            // 
            this.columnHeader38.Text = "打印状态";
            this.columnHeader38.Width = 145;
            // 
            // columnHeader39
            // 
            this.columnHeader39.Text = "打印时间";
            this.columnHeader39.Width = 170;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.yyQrcodeInfo_search);
            this.panel11.Controls.Add(this.label19);
            this.panel11.Controls.Add(this.qrsearchName);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1016, 67);
            this.panel11.TabIndex = 15;
            // 
            // yyQrcodeInfo_search
            // 
            this.yyQrcodeInfo_search.Location = new System.Drawing.Point(326, 15);
            this.yyQrcodeInfo_search.Name = "yyQrcodeInfo_search";
            this.yyQrcodeInfo_search.Size = new System.Drawing.Size(127, 37);
            this.yyQrcodeInfo_search.TabIndex = 9;
            this.yyQrcodeInfo_search.Text = "查  询";
            this.yyQrcodeInfo_search.UseVisualStyleBackColor = true;
            this.yyQrcodeInfo_search.Click += new System.EventHandler(this.yyQrcodeInfo_search_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(22, 24);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(82, 15);
            this.label19.TabIndex = 7;
            this.label19.Text = "总成二维码";
            // 
            // qrsearchName
            // 
            this.qrsearchName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qrsearchName.Location = new System.Drawing.Point(131, 18);
            this.qrsearchName.Name = "qrsearchName";
            this.qrsearchName.Size = new System.Drawing.Size(181, 26);
            this.qrsearchName.TabIndex = 8;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.yyQrcodeInfoDelete);
            this.panel10.Controls.Add(this.yyQrcodeInfoEdit);
            this.panel10.Controls.Add(this.yyQrcodeInfoAdd);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(0, 533);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1016, 80);
            this.panel10.TabIndex = 14;
            // 
            // yyQrcodeInfoDelete
            // 
            this.yyQrcodeInfoDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yyQrcodeInfoDelete.Location = new System.Drawing.Point(680, 26);
            this.yyQrcodeInfoDelete.Name = "yyQrcodeInfoDelete";
            this.yyQrcodeInfoDelete.Size = new System.Drawing.Size(93, 32);
            this.yyQrcodeInfoDelete.TabIndex = 13;
            this.yyQrcodeInfoDelete.Text = "删除总成";
            this.yyQrcodeInfoDelete.UseVisualStyleBackColor = true;
            this.yyQrcodeInfoDelete.Click += new System.EventHandler(this.yyQrcodeInfoDelete_Click);
            // 
            // yyQrcodeInfoEdit
            // 
            this.yyQrcodeInfoEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yyQrcodeInfoEdit.Location = new System.Drawing.Point(788, 26);
            this.yyQrcodeInfoEdit.Name = "yyQrcodeInfoEdit";
            this.yyQrcodeInfoEdit.Size = new System.Drawing.Size(93, 32);
            this.yyQrcodeInfoEdit.TabIndex = 11;
            this.yyQrcodeInfoEdit.Text = "编辑总成";
            this.yyQrcodeInfoEdit.UseVisualStyleBackColor = true;
            this.yyQrcodeInfoEdit.Click += new System.EventHandler(this.yyQrcodeInfoEdit_Click);
            // 
            // yyQrcodeInfoAdd
            // 
            this.yyQrcodeInfoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yyQrcodeInfoAdd.Location = new System.Drawing.Point(897, 26);
            this.yyQrcodeInfoAdd.Name = "yyQrcodeInfoAdd";
            this.yyQrcodeInfoAdd.Size = new System.Drawing.Size(93, 32);
            this.yyQrcodeInfoAdd.TabIndex = 12;
            this.yyQrcodeInfoAdd.Text = "添加总成";
            this.yyQrcodeInfoAdd.UseVisualStyleBackColor = true;
            this.yyQrcodeInfoAdd.Click += new System.EventHandler(this.yyQrcodeInfoAdd_Click);
            // 
            // tabCollectList
            // 
            this.tabCollectList.Controls.Add(this.panel9);
            this.tabCollectList.Controls.Add(this.panel8);
            this.tabCollectList.Controls.Add(this.panel7);
            this.tabCollectList.ImageKey = "采集图标.png";
            this.tabCollectList.Location = new System.Drawing.Point(0, 75);
            this.tabCollectList.Name = "tabCollectList";
            this.tabCollectList.Size = new System.Drawing.Size(1016, 613);
            this.tabCollectList.TabIndex = 10;
            this.tabCollectList.Text = "采集明细";
            this.tabCollectList.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.yyCollectInfoItemList);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 82);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1016, 459);
            this.panel9.TabIndex = 16;
            // 
            // yyCollectInfoItemList
            // 
            this.yyCollectInfoItemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader27,
            this.columnHeader28,
            this.columnHeader29,
            this.columnHeader30,
            this.columnHeader31,
            this.columnHeader32,
            this.columnHeader3});
            this.yyCollectInfoItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yyCollectInfoItemList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.yyCollectInfoItemList.FullRowSelect = true;
            this.yyCollectInfoItemList.GridLines = true;
            this.yyCollectInfoItemList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.yyCollectInfoItemList.HideSelection = false;
            this.yyCollectInfoItemList.LabelWrap = false;
            this.yyCollectInfoItemList.Location = new System.Drawing.Point(0, 0);
            this.yyCollectInfoItemList.Name = "yyCollectInfoItemList";
            this.yyCollectInfoItemList.Size = new System.Drawing.Size(1016, 459);
            this.yyCollectInfoItemList.TabIndex = 10;
            this.yyCollectInfoItemList.UseCompatibleStateImageBehavior = false;
            this.yyCollectInfoItemList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "序号";
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "总成二维码";
            this.columnHeader28.Width = 280;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "部件二维码";
            this.columnHeader29.Width = 280;
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "产品型号";
            this.columnHeader30.Width = 111;
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "工位";
            this.columnHeader31.Width = 100;
            // 
            // columnHeader32
            // 
            this.columnHeader32.Text = "状态";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "扫描时间";
            this.columnHeader3.Width = 166;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.yyCollectInfo_search);
            this.panel8.Controls.Add(this.label18);
            this.panel8.Controls.Add(this.yyCollectInfo_searchName);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1016, 82);
            this.panel8.TabIndex = 15;
            // 
            // yyCollectInfo_search
            // 
            this.yyCollectInfo_search.Location = new System.Drawing.Point(411, 26);
            this.yyCollectInfo_search.Name = "yyCollectInfo_search";
            this.yyCollectInfo_search.Size = new System.Drawing.Size(127, 37);
            this.yyCollectInfo_search.TabIndex = 9;
            this.yyCollectInfo_search.Text = "查  询";
            this.yyCollectInfo_search.UseVisualStyleBackColor = true;
            this.yyCollectInfo_search.Click += new System.EventHandler(this.yyCollectInfo_search_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(29, 35);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 16);
            this.label18.TabIndex = 7;
            this.label18.Text = "总成二维码";
            // 
            // yyCollectInfo_searchName
            // 
            this.yyCollectInfo_searchName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.yyCollectInfo_searchName.Location = new System.Drawing.Point(154, 30);
            this.yyCollectInfo_searchName.Name = "yyCollectInfo_searchName";
            this.yyCollectInfo_searchName.Size = new System.Drawing.Size(237, 26);
            this.yyCollectInfo_searchName.TabIndex = 8;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.yyCollectInfoDelete);
            this.panel7.Controls.Add(this.yyCollectInfoEdit);
            this.panel7.Controls.Add(this.yyCollectInfoAdd);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 541);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1016, 72);
            this.panel7.TabIndex = 14;
            // 
            // yyCollectInfoDelete
            // 
            this.yyCollectInfoDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yyCollectInfoDelete.Location = new System.Drawing.Point(710, 21);
            this.yyCollectInfoDelete.Name = "yyCollectInfoDelete";
            this.yyCollectInfoDelete.Size = new System.Drawing.Size(93, 32);
            this.yyCollectInfoDelete.TabIndex = 13;
            this.yyCollectInfoDelete.Text = "删除采集";
            this.yyCollectInfoDelete.UseVisualStyleBackColor = true;
            this.yyCollectInfoDelete.Click += new System.EventHandler(this.yyCollectInfoDelete_Click);
            // 
            // yyCollectInfoEdit
            // 
            this.yyCollectInfoEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yyCollectInfoEdit.Location = new System.Drawing.Point(809, 21);
            this.yyCollectInfoEdit.Name = "yyCollectInfoEdit";
            this.yyCollectInfoEdit.Size = new System.Drawing.Size(93, 32);
            this.yyCollectInfoEdit.TabIndex = 11;
            this.yyCollectInfoEdit.Text = "编辑采集";
            this.yyCollectInfoEdit.UseVisualStyleBackColor = true;
            this.yyCollectInfoEdit.Click += new System.EventHandler(this.yyCollectInfoEdit_Click);
            // 
            // yyCollectInfoAdd
            // 
            this.yyCollectInfoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yyCollectInfoAdd.Location = new System.Drawing.Point(908, 21);
            this.yyCollectInfoAdd.Name = "yyCollectInfoAdd";
            this.yyCollectInfoAdd.Size = new System.Drawing.Size(93, 32);
            this.yyCollectInfoAdd.TabIndex = 12;
            this.yyCollectInfoAdd.Text = "添加采集";
            this.yyCollectInfoAdd.UseVisualStyleBackColor = true;
            this.yyCollectInfoAdd.Click += new System.EventHandler(this.yyCollectInfoAdd_Click);
            // 
            // tpgAbout
            // 
            this.tpgAbout.ForeColor = System.Drawing.Color.Black;
            this.tpgAbout.ImageKey = "关于我们1.png";
            this.tpgAbout.Location = new System.Drawing.Point(0, 75);
            this.tpgAbout.Name = "tpgAbout";
            this.tpgAbout.Size = new System.Drawing.Size(1016, 613);
            this.tpgAbout.TabIndex = 3;
            this.tpgAbout.Text = "关于我们";
            this.tpgAbout.UseVisualStyleBackColor = true;
            // 
            // tpgSearch
            // 
            this.tpgSearch.Controls.Add(this.tabCtrlSearch);
            this.tpgSearch.ForeColor = System.Drawing.Color.Black;
            this.tpgSearch.ImageKey = "查询1.png";
            this.tpgSearch.Location = new System.Drawing.Point(0, 75);
            this.tpgSearch.Name = "tpgSearch";
            this.tpgSearch.Size = new System.Drawing.Size(1016, 613);
            this.tpgSearch.TabIndex = 5;
            this.tpgSearch.Text = "数据查询";
            this.tpgSearch.UseVisualStyleBackColor = true;
            // 
            // tabCtrlSearch
            // 
            this.tabCtrlSearch.Controls.Add(this.tpg_Spectrum);
            this.tabCtrlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlSearch.ItemSize = new System.Drawing.Size(120, 32);
            this.tabCtrlSearch.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlSearch.Name = "tabCtrlSearch";
            this.tabCtrlSearch.SelectedIndex = 0;
            this.tabCtrlSearch.Size = new System.Drawing.Size(1016, 613);
            this.tabCtrlSearch.TabIndex = 0;
            this.tabCtrlSearch.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabCtrlSearch_Selected);
            // 
            // tpg_Spectrum
            // 
            this.tpg_Spectrum.Controls.Add(this.panel1);
            this.tpg_Spectrum.Controls.Add(this.pnlSpem);
            this.tpg_Spectrum.Location = new System.Drawing.Point(4, 36);
            this.tpg_Spectrum.Name = "tpg_Spectrum";
            this.tpg_Spectrum.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_Spectrum.Size = new System.Drawing.Size(1008, 573);
            this.tpg_Spectrum.TabIndex = 0;
            this.tpg_Spectrum.Text = "生产记录查询";
            this.tpg_Spectrum.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvSpectrum);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 461);
            this.panel1.TabIndex = 2;
            // 
            // lvSpectrum
            // 
            this.lvSpectrum.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader16});
            this.lvSpectrum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSpectrum.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvSpectrum.FullRowSelect = true;
            this.lvSpectrum.GridLines = true;
            this.lvSpectrum.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSpectrum.HideSelection = false;
            this.lvSpectrum.LabelWrap = false;
            this.lvSpectrum.Location = new System.Drawing.Point(0, 0);
            this.lvSpectrum.Name = "lvSpectrum";
            this.lvSpectrum.Size = new System.Drawing.Size(1002, 461);
            this.lvSpectrum.TabIndex = 4;
            this.lvSpectrum.UseCompatibleStateImageBehavior = false;
            this.lvSpectrum.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "序号";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "总成二维码";
            this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader14.Width = 280;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "部件二维码";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 250;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "产品编号";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "工位";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "采集时间";
            this.columnHeader16.Width = 140;
            // 
            // pnlSpem
            // 
            this.pnlSpem.Controls.Add(this.yySearchClear);
            this.pnlSpem.Controls.Add(this.search_source_ip);
            this.pnlSpem.Controls.Add(this.label7);
            this.pnlSpem.Controls.Add(this.search_pruduct_xh);
            this.pnlSpem.Controls.Add(this.label6);
            this.pnlSpem.Controls.Add(this.search_pruduct_code);
            this.pnlSpem.Controls.Add(this.label5);
            this.pnlSpem.Controls.Add(this.label9);
            this.pnlSpem.Controls.Add(this.search_end_time);
            this.pnlSpem.Controls.Add(this.label10);
            this.pnlSpem.Controls.Add(this.search_start_time);
            this.pnlSpem.Controls.Add(this.btnExportAll);
            this.pnlSpem.Controls.Add(this.btnSearchAll);
            this.pnlSpem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpem.Location = new System.Drawing.Point(3, 3);
            this.pnlSpem.Name = "pnlSpem";
            this.pnlSpem.Size = new System.Drawing.Size(1002, 106);
            this.pnlSpem.TabIndex = 1;
            // 
            // yySearchClear
            // 
            this.yySearchClear.Location = new System.Drawing.Point(657, 60);
            this.yySearchClear.Name = "yySearchClear";
            this.yySearchClear.Size = new System.Drawing.Size(91, 37);
            this.yySearchClear.TabIndex = 8;
            this.yySearchClear.Text = "清空";
            this.yySearchClear.UseVisualStyleBackColor = true;
            this.yySearchClear.Click += new System.EventHandler(this.yySearchClear_Click);
            // 
            // search_source_ip
            // 
            this.search_source_ip.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.search_source_ip.Location = new System.Drawing.Point(677, 16);
            this.search_source_ip.Name = "search_source_ip";
            this.search_source_ip.Size = new System.Drawing.Size(180, 24);
            this.search_source_ip.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(624, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "工位";
            // 
            // search_pruduct_xh
            // 
            this.search_pruduct_xh.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.search_pruduct_xh.Location = new System.Drawing.Point(420, 16);
            this.search_pruduct_xh.Name = "search_pruduct_xh";
            this.search_pruduct_xh.Size = new System.Drawing.Size(180, 24);
            this.search_pruduct_xh.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(332, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "产品编号";
            // 
            // search_pruduct_code
            // 
            this.search_pruduct_code.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.search_pruduct_code.Location = new System.Drawing.Point(117, 16);
            this.search_pruduct_code.Name = "search_pruduct_code";
            this.search_pruduct_code.Size = new System.Drawing.Size(180, 24);
            this.search_pruduct_code.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(17, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "总成二维码";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(303, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "～";
            // 
            // search_end_time
            // 
            this.search_end_time.CustomFormat = "yyyy-MM-dd";
            this.search_end_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.search_end_time.Location = new System.Drawing.Point(335, 66);
            this.search_end_time.Name = "search_end_time";
            this.search_end_time.Size = new System.Drawing.Size(179, 21);
            this.search_end_time.TabIndex = 6;
            this.search_end_time.Value = new System.DateTime(2029, 12, 31, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(17, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 9;
            this.label10.Text = "采集时间";
            // 
            // search_start_time
            // 
            this.search_start_time.CustomFormat = "yyyy-MM-dd";
            this.search_start_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.search_start_time.Location = new System.Drawing.Point(117, 66);
            this.search_start_time.Name = "search_start_time";
            this.search_start_time.Size = new System.Drawing.Size(180, 21);
            this.search_start_time.TabIndex = 4;
            this.search_start_time.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // btnExportAll
            // 
            this.btnExportAll.Location = new System.Drawing.Point(768, 62);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(105, 37);
            this.btnExportAll.TabIndex = 7;
            this.btnExportAll.Text = "导出数据";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnOutSpectrum_Click);
            // 
            // btnSearchAll
            // 
            this.btnSearchAll.Location = new System.Drawing.Point(556, 60);
            this.btnSearchAll.Name = "btnSearchAll";
            this.btnSearchAll.Size = new System.Drawing.Size(87, 37);
            this.btnSearchAll.TabIndex = 7;
            this.btnSearchAll.Text = "查  询";
            this.btnSearchAll.UseVisualStyleBackColor = true;
            this.btnSearchAll.Click += new System.EventHandler(this.btnSpectruSh_Click);
            // 
            // tpgSysCfg
            // 
            this.tpgSysCfg.Controls.Add(this.tabSysSet);
            this.tpgSysCfg.ForeColor = System.Drawing.Color.Black;
            this.tpgSysCfg.ImageKey = "设置3.png";
            this.tpgSysCfg.Location = new System.Drawing.Point(0, 75);
            this.tpgSysCfg.Name = "tpgSysCfg";
            this.tpgSysCfg.Size = new System.Drawing.Size(1016, 613);
            this.tpgSysCfg.TabIndex = 2;
            this.tpgSysCfg.Text = "设备设置";
            this.tpgSysCfg.UseVisualStyleBackColor = true;
            // 
            // tabSysSet
            // 
            this.tabSysSet.Controls.Add(this.tabProduct1);
            this.tabSysSet.Controls.Add(this.tabPLCInfo);
            this.tabSysSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSysSet.ItemSize = new System.Drawing.Size(96, 36);
            this.tabSysSet.Location = new System.Drawing.Point(0, 0);
            this.tabSysSet.Name = "tabSysSet";
            this.tabSysSet.SelectedIndex = 0;
            this.tabSysSet.Size = new System.Drawing.Size(1016, 613);
            this.tabSysSet.TabIndex = 0;
            this.tabSysSet.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabSysSet_Selected);
            // 
            // tabProduct1
            // 
            this.tabProduct1.Controls.Add(this.panel3);
            this.tabProduct1.Controls.Add(this.panel4);
            this.tabProduct1.Controls.Add(this.panel5);
            this.tabProduct1.Location = new System.Drawing.Point(4, 40);
            this.tabProduct1.Name = "tabProduct1";
            this.tabProduct1.Padding = new System.Windows.Forms.Padding(3);
            this.tabProduct1.Size = new System.Drawing.Size(1008, 569);
            this.tabProduct1.TabIndex = 11;
            this.tabProduct1.Text = "产品设置";
            this.tabProduct1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.productList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 73);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1002, 430);
            this.panel3.TabIndex = 2;
            // 
            // productList
            // 
            this.productList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_product_no,
            this.column_product_name,
            this.column_product_code,
            this.column_product_mark,
            this.column_product_create});
            this.productList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productList.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.productList.FullRowSelect = true;
            this.productList.GridLines = true;
            this.productList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.productList.HideSelection = false;
            this.productList.LabelWrap = false;
            this.productList.Location = new System.Drawing.Point(0, 0);
            this.productList.MultiSelect = false;
            this.productList.Name = "productList";
            this.productList.Size = new System.Drawing.Size(1002, 430);
            this.productList.TabIndex = 5;
            this.productList.UseCompatibleStateImageBehavior = false;
            this.productList.View = System.Windows.Forms.View.Details;
            // 
            // column_product_no
            // 
            this.column_product_no.Text = "序号";
            // 
            // column_product_name
            // 
            this.column_product_name.Text = "产品名称";
            this.column_product_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_product_name.Width = 150;
            // 
            // column_product_code
            // 
            this.column_product_code.Text = "产品编码";
            this.column_product_code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_product_code.Width = 150;
            // 
            // column_product_mark
            // 
            this.column_product_mark.Text = "产品识别码";
            this.column_product_mark.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_product_mark.Width = 150;
            // 
            // column_product_create
            // 
            this.column_product_create.Text = "创建时间";
            this.column_product_create.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_product_create.Width = 150;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.productEdit);
            this.panel4.Controls.Add(this.productDelete);
            this.panel4.Controls.Add(this.productAdd);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 503);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1002, 63);
            this.panel4.TabIndex = 1;
            // 
            // productEdit
            // 
            this.productEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.productEdit.ForeColor = System.Drawing.Color.Black;
            this.productEdit.Location = new System.Drawing.Point(765, 17);
            this.productEdit.Name = "productEdit";
            this.productEdit.Size = new System.Drawing.Size(96, 32);
            this.productEdit.TabIndex = 14;
            this.productEdit.Text = "编辑产品";
            this.productEdit.UseVisualStyleBackColor = true;
            this.productEdit.Click += new System.EventHandler(this.productEdit_Click);
            // 
            // productDelete
            // 
            this.productDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.productDelete.ForeColor = System.Drawing.Color.Black;
            this.productDelete.Location = new System.Drawing.Point(652, 17);
            this.productDelete.Name = "productDelete";
            this.productDelete.Size = new System.Drawing.Size(96, 32);
            this.productDelete.TabIndex = 13;
            this.productDelete.Text = "删除产品";
            this.productDelete.UseVisualStyleBackColor = true;
            this.productDelete.Click += new System.EventHandler(this.productDelete_Click);
            // 
            // productAdd
            // 
            this.productAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.productAdd.ForeColor = System.Drawing.Color.Black;
            this.productAdd.Location = new System.Drawing.Point(875, 17);
            this.productAdd.Name = "productAdd";
            this.productAdd.Size = new System.Drawing.Size(96, 32);
            this.productAdd.TabIndex = 12;
            this.productAdd.Text = "添加产品";
            this.productAdd.UseVisualStyleBackColor = true;
            this.productAdd.Click += new System.EventHandler(this.productAdd_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.productNo);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.product_search);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1002, 70);
            this.panel5.TabIndex = 0;
            // 
            // productNo
            // 
            this.productNo.Location = new System.Drawing.Point(115, 25);
            this.productNo.Name = "productNo";
            this.productNo.Size = new System.Drawing.Size(146, 21);
            this.productNo.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(27, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "产品名称";
            // 
            // product_search
            // 
            this.product_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.product_search.Location = new System.Drawing.Point(857, 17);
            this.product_search.Name = "product_search";
            this.product_search.Size = new System.Drawing.Size(123, 37);
            this.product_search.TabIndex = 7;
            this.product_search.Text = "查  询";
            this.product_search.UseVisualStyleBackColor = true;
            this.product_search.Click += new System.EventHandler(this.product_search_Click);
            // 
            // tabPLCInfo
            // 
            this.tabPLCInfo.Controls.Add(this.sysPlcInfoItemList);
            this.tabPLCInfo.Controls.Add(this.panel14);
            this.tabPLCInfo.Controls.Add(this.panel13);
            this.tabPLCInfo.ImageKey = "product.png";
            this.tabPLCInfo.Location = new System.Drawing.Point(4, 40);
            this.tabPLCInfo.Name = "tabPLCInfo";
            this.tabPLCInfo.Size = new System.Drawing.Size(192, 0);
            this.tabPLCInfo.TabIndex = 9;
            this.tabPLCInfo.Text = "监控设备";
            this.tabPLCInfo.UseVisualStyleBackColor = true;
            // 
            // sysPlcInfoItemList
            // 
            this.sysPlcInfoItemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_index,
            this.col_name,
            this.col_ip,
            this.col_port,
            this.col_station,
            this.col_create_time,
            this.col_op_name});
            this.sysPlcInfoItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sysPlcInfoItemList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sysPlcInfoItemList.FullRowSelect = true;
            this.sysPlcInfoItemList.GridLines = true;
            this.sysPlcInfoItemList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.sysPlcInfoItemList.HideSelection = false;
            this.sysPlcInfoItemList.LabelWrap = false;
            this.sysPlcInfoItemList.Location = new System.Drawing.Point(0, 87);
            this.sysPlcInfoItemList.Name = "sysPlcInfoItemList";
            this.sysPlcInfoItemList.Size = new System.Drawing.Size(192, 0);
            this.sysPlcInfoItemList.TabIndex = 9;
            this.sysPlcInfoItemList.UseCompatibleStateImageBehavior = false;
            this.sysPlcInfoItemList.View = System.Windows.Forms.View.Details;
            // 
            // col_index
            // 
            this.col_index.Text = "序号";
            // 
            // col_name
            // 
            this.col_name.Text = "名称";
            this.col_name.Width = 98;
            // 
            // col_ip
            // 
            this.col_ip.Text = "IP地址";
            this.col_ip.Width = 162;
            // 
            // col_port
            // 
            this.col_port.Text = "端口";
            this.col_port.Width = 96;
            // 
            // col_station
            // 
            this.col_station.Text = "工位";
            this.col_station.Width = 111;
            // 
            // col_create_time
            // 
            this.col_create_time.Text = "录入时间";
            this.col_create_time.Width = 166;
            // 
            // col_op_name
            // 
            this.col_op_name.Text = "录入人";
            this.col_op_name.Width = 166;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.sysPlcInfo_search);
            this.panel14.Controls.Add(this.label2);
            this.panel14.Controls.Add(this.searchName);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(192, 87);
            this.panel14.TabIndex = 8;
            // 
            // sysPlcInfo_search
            // 
            this.sysPlcInfo_search.Location = new System.Drawing.Point(326, 27);
            this.sysPlcInfo_search.Name = "sysPlcInfo_search";
            this.sysPlcInfo_search.Size = new System.Drawing.Size(127, 37);
            this.sysPlcInfo_search.TabIndex = 2;
            this.sysPlcInfo_search.Text = "查  询";
            this.sysPlcInfo_search.UseVisualStyleBackColor = true;
            this.sysPlcInfo_search.Click += new System.EventHandler(this.sysPlcInfo_search_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(18, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "设备名称";
            // 
            // searchName
            // 
            this.searchName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.searchName.Location = new System.Drawing.Point(115, 30);
            this.searchName.Name = "searchName";
            this.searchName.Size = new System.Drawing.Size(181, 26);
            this.searchName.TabIndex = 1;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.sysPlcInfoAdd);
            this.panel13.Controls.Add(this.sysPlcInfoDelete);
            this.panel13.Controls.Add(this.sysPlcInfoEdit);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel13.Location = new System.Drawing.Point(0, -74);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(192, 74);
            this.panel13.TabIndex = 7;
            // 
            // sysPlcInfoAdd
            // 
            this.sysPlcInfoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sysPlcInfoAdd.Location = new System.Drawing.Point(67, 23);
            this.sysPlcInfoAdd.Name = "sysPlcInfoAdd";
            this.sysPlcInfoAdd.Size = new System.Drawing.Size(93, 32);
            this.sysPlcInfoAdd.TabIndex = 5;
            this.sysPlcInfoAdd.Text = "添加设备";
            this.sysPlcInfoAdd.UseVisualStyleBackColor = true;
            this.sysPlcInfoAdd.Click += new System.EventHandler(this.sysPlcInfoAdd_Click);
            // 
            // sysPlcInfoDelete
            // 
            this.sysPlcInfoDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sysPlcInfoDelete.Location = new System.Drawing.Point(-143, 23);
            this.sysPlcInfoDelete.Name = "sysPlcInfoDelete";
            this.sysPlcInfoDelete.Size = new System.Drawing.Size(93, 32);
            this.sysPlcInfoDelete.TabIndex = 6;
            this.sysPlcInfoDelete.Text = "删除设备";
            this.sysPlcInfoDelete.UseVisualStyleBackColor = true;
            this.sysPlcInfoDelete.Click += new System.EventHandler(this.sysPlcInfoDelete_Click);
            // 
            // sysPlcInfoEdit
            // 
            this.sysPlcInfoEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sysPlcInfoEdit.Location = new System.Drawing.Point(-37, 23);
            this.sysPlcInfoEdit.Name = "sysPlcInfoEdit";
            this.sysPlcInfoEdit.Size = new System.Drawing.Size(93, 32);
            this.sysPlcInfoEdit.TabIndex = 4;
            this.sysPlcInfoEdit.Text = "编辑设备";
            this.sysPlcInfoEdit.UseVisualStyleBackColor = true;
            this.sysPlcInfoEdit.Click += new System.EventHandler(this.sysPlcInfoEdit_Click);
            // 
            // tpgServer
            // 
            this.tpgServer.Controls.Add(this.pnlFoot);
            this.tpgServer.Controls.Add(this.pnlHead);
            this.tpgServer.ForeColor = System.Drawing.Color.Black;
            this.tpgServer.ImageKey = "服务器1.png";
            this.tpgServer.Location = new System.Drawing.Point(0, 75);
            this.tpgServer.Name = "tpgServer";
            this.tpgServer.Size = new System.Drawing.Size(1016, 613);
            this.tpgServer.TabIndex = 6;
            this.tpgServer.Text = "服务配置";
            this.tpgServer.UseVisualStyleBackColor = true;
            // 
            // pnlFoot
            // 
            this.pnlFoot.Controls.Add(this.lvDevices);
            this.pnlFoot.Controls.Add(this.pnlScopeEdit);
            this.pnlFoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFoot.Location = new System.Drawing.Point(0, 77);
            this.pnlFoot.Name = "pnlFoot";
            this.pnlFoot.Size = new System.Drawing.Size(1016, 536);
            this.pnlFoot.TabIndex = 1;
            // 
            // lvDevices
            // 
            this.lvDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colXH,
            this.colDevid,
            this.colDevName,
            this.colSiteAddr,
            this.colIPAddr,
            this.colIpPort,
            this.colOrderNum,
            this.colLinkState,
            this.colCoilsAddr,
            this.colRegAddr});
            this.lvDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDevices.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvDevices.FullRowSelect = true;
            this.lvDevices.GridLines = true;
            this.lvDevices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDevices.HideSelection = false;
            this.lvDevices.LabelWrap = false;
            this.lvDevices.Location = new System.Drawing.Point(0, 0);
            this.lvDevices.MultiSelect = false;
            this.lvDevices.Name = "lvDevices";
            this.lvDevices.Size = new System.Drawing.Size(1016, 473);
            this.lvDevices.TabIndex = 6;
            this.lvDevices.UseCompatibleStateImageBehavior = false;
            this.lvDevices.View = System.Windows.Forms.View.Details;
            // 
            // colXH
            // 
            this.colXH.Text = "序号";
            // 
            // colDevid
            // 
            this.colDevid.Text = "设备编码";
            this.colDevid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDevid.Width = 100;
            // 
            // colDevName
            // 
            this.colDevName.Text = "设备名称";
            this.colDevName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDevName.Width = 120;
            // 
            // colSiteAddr
            // 
            this.colSiteAddr.Text = "PLC站号";
            this.colSiteAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colSiteAddr.Width = 90;
            // 
            // colIPAddr
            // 
            this.colIPAddr.Text = "服务端IP";
            this.colIPAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colIPAddr.Width = 120;
            // 
            // colIpPort
            // 
            this.colIpPort.Text = "服务端口";
            this.colIpPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colIpPort.Width = 90;
            // 
            // colOrderNum
            // 
            this.colOrderNum.Text = "工位序号";
            this.colOrderNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colOrderNum.Width = 90;
            // 
            // colLinkState
            // 
            this.colLinkState.Text = "连接状态";
            this.colLinkState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colLinkState.Width = 90;
            // 
            // colCoilsAddr
            // 
            this.colCoilsAddr.Text = "开关地址";
            this.colCoilsAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colCoilsAddr.Width = 100;
            // 
            // colRegAddr
            // 
            this.colRegAddr.Text = "条码地址";
            this.colRegAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colRegAddr.Width = 100;
            // 
            // pnlScopeEdit
            // 
            this.pnlScopeEdit.Controls.Add(this.btnShowDev);
            this.pnlScopeEdit.Controls.Add(this.btnEditDev);
            this.pnlScopeEdit.Controls.Add(this.btnDelDev);
            this.pnlScopeEdit.Controls.Add(this.btnAddDev);
            this.pnlScopeEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlScopeEdit.Location = new System.Drawing.Point(0, 473);
            this.pnlScopeEdit.Name = "pnlScopeEdit";
            this.pnlScopeEdit.Size = new System.Drawing.Size(1016, 63);
            this.pnlScopeEdit.TabIndex = 2;
            // 
            // btnShowDev
            // 
            this.btnShowDev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowDev.Location = new System.Drawing.Point(22, 17);
            this.btnShowDev.Name = "btnShowDev";
            this.btnShowDev.Size = new System.Drawing.Size(113, 32);
            this.btnShowDev.TabIndex = 15;
            this.btnShowDev.Text = "查  询";
            this.btnShowDev.UseVisualStyleBackColor = true;
            this.btnShowDev.Click += new System.EventHandler(this.btnShowDev_Click);
            // 
            // btnEditDev
            // 
            this.btnEditDev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditDev.ForeColor = System.Drawing.Color.Black;
            this.btnEditDev.Location = new System.Drawing.Point(779, 17);
            this.btnEditDev.Name = "btnEditDev";
            this.btnEditDev.Size = new System.Drawing.Size(96, 32);
            this.btnEditDev.TabIndex = 14;
            this.btnEditDev.Text = "编辑设备";
            this.btnEditDev.UseVisualStyleBackColor = true;
            this.btnEditDev.Click += new System.EventHandler(this.btnEditDev_Click);
            // 
            // btnDelDev
            // 
            this.btnDelDev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelDev.ForeColor = System.Drawing.Color.Black;
            this.btnDelDev.Location = new System.Drawing.Point(666, 17);
            this.btnDelDev.Name = "btnDelDev";
            this.btnDelDev.Size = new System.Drawing.Size(96, 32);
            this.btnDelDev.TabIndex = 13;
            this.btnDelDev.Text = "删除设备";
            this.btnDelDev.UseVisualStyleBackColor = true;
            this.btnDelDev.Click += new System.EventHandler(this.btnDelDev_Click);
            // 
            // btnAddDev
            // 
            this.btnAddDev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDev.ForeColor = System.Drawing.Color.Black;
            this.btnAddDev.Location = new System.Drawing.Point(889, 17);
            this.btnAddDev.Name = "btnAddDev";
            this.btnAddDev.Size = new System.Drawing.Size(96, 32);
            this.btnAddDev.TabIndex = 12;
            this.btnAddDev.Text = "增加设备";
            this.btnAddDev.UseVisualStyleBackColor = true;
            this.btnAddDev.Click += new System.EventHandler(this.btnAddDev_Click);
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.gpbMainServer);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1016, 77);
            this.pnlHead.TabIndex = 0;
            // 
            // gpbMainServer
            // 
            this.gpbMainServer.Controls.Add(this.label3);
            this.gpbMainServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbMainServer.Location = new System.Drawing.Point(0, 0);
            this.gpbMainServer.Name = "gpbMainServer";
            this.gpbMainServer.Size = new System.Drawing.Size(1016, 77);
            this.gpbMainServer.TabIndex = 0;
            this.gpbMainServer.TabStop = false;
            this.gpbMainServer.Text = "监控服务开关";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(423, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "设备设置";
            // 
            // tpgMapMode
            // 
            this.tpgMapMode.Controls.Add(this.pnlHome);
            this.tpgMapMode.ForeColor = System.Drawing.Color.Black;
            this.tpgMapMode.ImageKey = "监控设置.png";
            this.tpgMapMode.Location = new System.Drawing.Point(0, 75);
            this.tpgMapMode.Margin = new System.Windows.Forms.Padding(0);
            this.tpgMapMode.Name = "tpgMapMode";
            this.tpgMapMode.Padding = new System.Windows.Forms.Padding(3);
            this.tpgMapMode.Size = new System.Drawing.Size(1016, 613);
            this.tpgMapMode.TabIndex = 0;
            this.tpgMapMode.Text = "监控首页";
            this.tpgMapMode.UseVisualStyleBackColor = true;
            // 
            // pnlHome
            // 
            this.pnlHome.Controls.Add(this.tlpHome);
            this.pnlHome.Controls.Add(this.splitter1);
            this.pnlHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHome.Location = new System.Drawing.Point(3, 3);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(1010, 607);
            this.pnlHome.TabIndex = 1;
            // 
            // tlpHome
            // 
            this.tlpHome.ColumnCount = 2;
            this.tlpHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHome.Controls.Add(this.groupBox2, 0, 0);
            this.tlpHome.Controls.Add(this.btnServer, 1, 1);
            this.tlpHome.Controls.Add(this.tblPrint, 1, 0);
            this.tlpHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHome.Location = new System.Drawing.Point(0, 0);
            this.tlpHome.Name = "tlpHome";
            this.tlpHome.RowCount = 2;
            this.tlpHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tlpHome.Size = new System.Drawing.Size(1007, 607);
            this.tlpHome.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMsg);
            this.groupBox2.Controls.Add(this.gpbDevStatus);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.tlpHome.SetRowSpan(this.groupBox2, 2);
            this.groupBox2.Size = new System.Drawing.Size(497, 601);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收消息";
            // 
            // txtMsg
            // 
            this.txtMsg.BackColor = System.Drawing.Color.White;
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.Location = new System.Drawing.Point(3, 17);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(491, 285);
            this.txtMsg.TabIndex = 0;
            // 
            // gpbDevStatus
            // 
            this.gpbDevStatus.Controls.Add(this.flpDevices);
            this.gpbDevStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpbDevStatus.Location = new System.Drawing.Point(3, 302);
            this.gpbDevStatus.Name = "gpbDevStatus";
            this.gpbDevStatus.Size = new System.Drawing.Size(491, 296);
            this.gpbDevStatus.TabIndex = 0;
            this.gpbDevStatus.TabStop = false;
            this.gpbDevStatus.Text = "设备状态";
            // 
            // flpDevices
            // 
            this.flpDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDevices.Location = new System.Drawing.Point(3, 17);
            this.flpDevices.Margin = new System.Windows.Forms.Padding(12);
            this.flpDevices.Name = "flpDevices";
            this.flpDevices.Size = new System.Drawing.Size(485, 276);
            this.flpDevices.TabIndex = 0;
            // 
            // btnServer
            // 
            this.btnServer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnServer.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnServer.Location = new System.Drawing.Point(555, 532);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(400, 53);
            this.btnServer.TabIndex = 18;
            this.btnServer.Text = "启动服务";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.startServer_Click);
            // 
            // tblPrint
            // 
            this.tblPrint.ColumnCount = 1;
            this.tblPrint.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPrint.Controls.Add(this.groupBox3, 0, 0);
            this.tblPrint.Controls.Add(this.panel2, 0, 1);
            this.tblPrint.Controls.Add(this.groupBox5, 0, 2);
            this.tblPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPrint.Location = new System.Drawing.Point(506, 3);
            this.tblPrint.Name = "tblPrint";
            this.tblPrint.RowCount = 3;
            this.tblPrint.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tblPrint.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblPrint.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPrint.Size = new System.Drawing.Size(498, 505);
            this.tblPrint.TabIndex = 19;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtQrCode);
            this.groupBox3.Controls.Add(this.btnQrSearch);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(492, 66);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "打印总成二维码";
            // 
            // txtQrCode
            // 
            this.txtQrCode.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtQrCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQrCode.Location = new System.Drawing.Point(117, 22);
            this.txtQrCode.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.txtQrCode.Name = "txtQrCode";
            this.txtQrCode.Size = new System.Drawing.Size(261, 26);
            this.txtQrCode.TabIndex = 1;
            this.txtQrCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQrCode_KeyUp);
            // 
            // btnQrSearch
            // 
            this.btnQrSearch.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnQrSearch.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQrSearch.Location = new System.Drawing.Point(384, 20);
            this.btnQrSearch.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.btnQrSearch.Name = "btnQrSearch";
            this.btnQrSearch.Size = new System.Drawing.Size(100, 32);
            this.btnQrSearch.TabIndex = 2;
            this.btnQrSearch.Text = "查  询";
            this.btnQrSearch.UseVisualStyleBackColor = true;
            this.btnQrSearch.Click += new System.EventHandler(this.btnQrSearch_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(7, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "总成二维码";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(492, 294);
            this.panel2.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(492, 294);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "生成二维码";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlLeft, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlRight, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlBottom, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlTop, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlContent, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlHeight, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlWidth, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlFont, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(486, 274);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(51, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "二维码打印边距";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.label11);
            this.pnlLeft.Controls.Add(this.nudLeft);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(3, 40);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(137, 194);
            this.pnlLeft.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(395, -102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "二维码高";
            // 
            // nudLeft
            // 
            this.nudLeft.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.nudLeft.Location = new System.Drawing.Point(82, 85);
            this.nudLeft.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new System.Drawing.Size(54, 21);
            this.nudLeft.TabIndex = 16;
            this.nudLeft.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLeft.ValueChanged += new System.EventHandler(this.nudLeft_ValueChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.nudRight);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(346, 40);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(137, 194);
            this.pnlRight.TabIndex = 4;
            // 
            // nudRight
            // 
            this.nudRight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudRight.Location = new System.Drawing.Point(3, 85);
            this.nudRight.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudRight.Name = "nudRight";
            this.nudRight.Size = new System.Drawing.Size(54, 21);
            this.nudRight.TabIndex = 17;
            this.nudRight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRight.ValueChanged += new System.EventHandler(this.nudRight_ValueChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.nudDown);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(146, 240);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(194, 31);
            this.pnlBottom.TabIndex = 5;
            // 
            // nudDown
            // 
            this.nudDown.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudDown.Location = new System.Drawing.Point(71, 2);
            this.nudDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudDown.Name = "nudDown";
            this.nudDown.Size = new System.Drawing.Size(52, 21);
            this.nudDown.TabIndex = 15;
            this.nudDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDown.ValueChanged += new System.EventHandler(this.nudDown_ValueChanged);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.nudUp);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(146, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(194, 31);
            this.pnlTop.TabIndex = 6;
            // 
            // nudUp
            // 
            this.nudUp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudUp.Location = new System.Drawing.Point(72, 2);
            this.nudUp.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudUp.Name = "nudUp";
            this.nudUp.Size = new System.Drawing.Size(52, 21);
            this.nudUp.TabIndex = 14;
            this.nudUp.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudUp.ValueChanged += new System.EventHandler(this.nudUp_ValueChanged);
            // 
            // pnlContent
            // 
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.qrImage);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(146, 40);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(194, 194);
            this.pnlContent.TabIndex = 19;
            // 
            // qrImage
            // 
            this.qrImage.BackColor = System.Drawing.Color.White;
            this.qrImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.qrImage.FontSize = 9F;
            this.qrImage.Location = new System.Drawing.Point(-1, 2);
            this.qrImage.Margin = new System.Windows.Forms.Padding(4);
            this.qrImage.Name = "qrImage";
            this.qrImage.PrintMarginBottom = 0;
            this.qrImage.PrintMarginLeft = 0;
            this.qrImage.PrintMarginRight = 0;
            this.qrImage.PrintMarginTop = 0;
            this.qrImage.QrContent = "yu3zx.com";
            this.qrImage.QrFont = new System.Drawing.Font("黑体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.qrImage.Size = new System.Drawing.Size(196, 192);
            this.qrImage.TabIndex = 0;
            // 
            // pnlHeight
            // 
            this.pnlHeight.Controls.Add(this.label12);
            this.pnlHeight.Controls.Add(this.nudHeight);
            this.pnlHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeight.Location = new System.Drawing.Point(3, 240);
            this.pnlHeight.Name = "pnlHeight";
            this.pnlHeight.Size = new System.Drawing.Size(137, 31);
            this.pnlHeight.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(-20, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "二维码高(mm)";
            // 
            // nudHeight
            // 
            this.nudHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHeight.Location = new System.Drawing.Point(82, 2);
            this.nudHeight.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(54, 21);
            this.nudHeight.TabIndex = 20;
            this.nudHeight.Value = new decimal(new int[] {
            103,
            0,
            0,
            0});
            this.nudHeight.ValueChanged += new System.EventHandler(this.nudHeight_ValueChanged);
            // 
            // pnlWidth
            // 
            this.pnlWidth.Controls.Add(this.label13);
            this.pnlWidth.Controls.Add(this.nudWidth);
            this.pnlWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWidth.Location = new System.Drawing.Point(346, 240);
            this.pnlWidth.Name = "pnlWidth";
            this.pnlWidth.Size = new System.Drawing.Size(137, 31);
            this.pnlWidth.TabIndex = 21;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(60, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 8;
            this.label13.Text = "二维码宽(mm)";
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(3, 2);
            this.nudWidth.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(54, 21);
            this.nudWidth.TabIndex = 6;
            this.nudWidth.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // pnlFont
            // 
            this.pnlFont.Controls.Add(this.label14);
            this.pnlFont.Controls.Add(this.nudFontSize);
            this.pnlFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFont.Location = new System.Drawing.Point(346, 3);
            this.pnlFont.Name = "pnlFont";
            this.pnlFont.Size = new System.Drawing.Size(137, 31);
            this.pnlFont.TabIndex = 22;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(69, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 22;
            this.label14.Text = "字体大小";
            // 
            // nudFontSize
            // 
            this.nudFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudFontSize.DecimalPlaces = 1;
            this.nudFontSize.Location = new System.Drawing.Point(2, 5);
            this.nudFontSize.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.nudFontSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(56, 21);
            this.nudFontSize.TabIndex = 7;
            this.nudFontSize.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudFontSize.ValueChanged += new System.EventHandler(this.nudFontSize_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtAlarmInfo);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 375);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(492, 127);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "提示消息";
            // 
            // txtAlarmInfo
            // 
            this.txtAlarmInfo.BackColor = System.Drawing.Color.White;
            this.txtAlarmInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAlarmInfo.Location = new System.Drawing.Point(3, 17);
            this.txtAlarmInfo.Multiline = true;
            this.txtAlarmInfo.Name = "txtAlarmInfo";
            this.txtAlarmInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAlarmInfo.Size = new System.Drawing.Size(486, 107);
            this.txtAlarmInfo.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Transparent;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(1007, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 607);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // skinTabCtrl
            // 
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 1F;
            this.skinTabCtrl.Animation = animation1;
            this.skinTabCtrl.AnimatorType = CCWin.SkinControl.AnimationType.Transparent;
            this.skinTabCtrl.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabCtrl.Controls.Add(this.tpgMapMode);
            this.skinTabCtrl.Controls.Add(this.tpgServer);
            this.skinTabCtrl.Controls.Add(this.tabCollectList);
            this.skinTabCtrl.Controls.Add(this.tabQrCode);
            this.skinTabCtrl.Controls.Add(this.tpgSysCfg);
            this.skinTabCtrl.Controls.Add(this.tpgSearch);
            this.skinTabCtrl.Controls.Add(this.tpgAbout);
            this.skinTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabCtrl.ImageList = this.imageList;
            this.skinTabCtrl.ImgSize = new System.Drawing.Size(48, 48);
            this.skinTabCtrl.ItemSize = new System.Drawing.Size(80, 75);
            this.skinTabCtrl.Location = new System.Drawing.Point(4, 28);
            this.skinTabCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.skinTabCtrl.Name = "skinTabCtrl";
            this.skinTabCtrl.Padding = new System.Drawing.Point(0, 0);
            this.skinTabCtrl.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabCtrl.PageArrowDown")));
            this.skinTabCtrl.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabCtrl.PageArrowHover")));
            this.skinTabCtrl.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabCtrl.PageCloseHover")));
            this.skinTabCtrl.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabCtrl.PageCloseNormal")));
            this.skinTabCtrl.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabCtrl.PageDown")));
            this.skinTabCtrl.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabCtrl.PageHover")));
            this.skinTabCtrl.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Top;
            this.skinTabCtrl.PageNorml = null;
            this.skinTabCtrl.PageTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.skinTabCtrl.SelectedIndex = 0;
            this.skinTabCtrl.Size = new System.Drawing.Size(1016, 688);
            this.skinTabCtrl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabCtrl.TabIndex = 1;
            this.skinTabCtrl.Selected += new System.Windows.Forms.TabControlEventHandler(this.skinTabCtrl_Selected);
            // 
            // imgPrint
            // 
            this.imgPrint.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.imgPrint_PrintPage);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackShade = false;
            this.ClientSize = new System.Drawing.Size(1024, 720);
            this.CloseBoxSize = new System.Drawing.Size(36, 22);
            this.Controls.Add(this.skinTabCtrl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaxSize = new System.Drawing.Size(36, 22);
            this.MiniSize = new System.Drawing.Size(36, 22);
            this.Name = "mainFrm";
            this.Radius = 10;
            this.ShowDrawIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生产线数据采集系统V1.0";
            this.TitleOffset = new System.Drawing.Point(5, 1);
            this.TitleSuitColor = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainFrm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainFrm_FormClosed);
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.tabQrCode.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.tabCollectList.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.tpgSearch.ResumeLayout(false);
            this.tabCtrlSearch.ResumeLayout(false);
            this.tpg_Spectrum.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlSpem.ResumeLayout(false);
            this.pnlSpem.PerformLayout();
            this.tpgSysCfg.ResumeLayout(false);
            this.tabSysSet.ResumeLayout(false);
            this.tabProduct1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPLCInfo.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.tpgServer.ResumeLayout(false);
            this.pnlFoot.ResumeLayout(false);
            this.pnlScopeEdit.ResumeLayout(false);
            this.pnlHead.ResumeLayout(false);
            this.gpbMainServer.ResumeLayout(false);
            this.gpbMainServer.PerformLayout();
            this.tpgMapMode.ResumeLayout(false);
            this.pnlHome.ResumeLayout(false);
            this.tlpHome.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gpbDevStatus.ResumeLayout(false);
            this.tblPrint.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDown)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudUp)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlHeight.ResumeLayout(false);
            this.pnlHeight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.pnlWidth.ResumeLayout(false);
            this.pnlWidth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.pnlFont.ResumeLayout(false);
            this.pnlFont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.skinTabCtrl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TabPage tabQrCode;
        private System.Windows.Forms.Button yyQrcodeInfoDelete;
        private System.Windows.Forms.Button yyQrcodeInfoAdd;
        private System.Windows.Forms.Button yyQrcodeInfoEdit;
        private System.Windows.Forms.ListView yyQrcodeInfoItemList;
        private System.Windows.Forms.ColumnHeader columnHeader33;
        private System.Windows.Forms.ColumnHeader columnHeader34;
        private System.Windows.Forms.ColumnHeader columnHeader35;
        private System.Windows.Forms.ColumnHeader columnHeader36;
        private System.Windows.Forms.ColumnHeader columnHeader37;
        private System.Windows.Forms.ColumnHeader columnHeader38;
        private System.Windows.Forms.ColumnHeader columnHeader39;
        private System.Windows.Forms.Button yyQrcodeInfo_search;
        private System.Windows.Forms.TextBox qrsearchName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabPage tabCollectList;
        private System.Windows.Forms.Button yyCollectInfoDelete;
        private System.Windows.Forms.Button yyCollectInfoAdd;
        private System.Windows.Forms.Button yyCollectInfoEdit;
        private System.Windows.Forms.ListView yyCollectInfoItemList;
        private System.Windows.Forms.ColumnHeader columnHeader27;
        private System.Windows.Forms.ColumnHeader columnHeader28;
        private System.Windows.Forms.ColumnHeader columnHeader29;
        private System.Windows.Forms.ColumnHeader columnHeader30;
        private System.Windows.Forms.ColumnHeader columnHeader31;
        private System.Windows.Forms.ColumnHeader columnHeader32;
        private System.Windows.Forms.Button yyCollectInfo_search;
        private System.Windows.Forms.TextBox yyCollectInfo_searchName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TabPage tpgAbout;
        private System.Windows.Forms.TabPage tpgSearch;
        private System.Windows.Forms.TabPage tpgSysCfg;
        private System.Windows.Forms.TabControl tabSysSet;
        private System.Windows.Forms.TabPage tabPLCInfo;
        private System.Windows.Forms.Button sysPlcInfoDelete;
        private System.Windows.Forms.Button sysPlcInfoAdd;
        private System.Windows.Forms.Button sysPlcInfoEdit;
        private System.Windows.Forms.Button sysPlcInfo_search;
        private System.Windows.Forms.TextBox searchName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabProduct1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView productList;
        private System.Windows.Forms.ColumnHeader column_product_no;
        private System.Windows.Forms.ColumnHeader column_product_name;
        private System.Windows.Forms.ColumnHeader column_product_code;
        private System.Windows.Forms.ColumnHeader column_product_mark;
        private System.Windows.Forms.ColumnHeader column_product_create;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button productEdit;
        private System.Windows.Forms.Button productDelete;
        private System.Windows.Forms.Button productAdd;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox productNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button product_search;
        private System.Windows.Forms.TabPage tpgServer;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.GroupBox gpbMainServer;
        private System.Windows.Forms.TabPage tpgMapMode;
        private System.Windows.Forms.Panel pnlHome;
        private BSE.Windows.Forms.Splitter splitter1;
        private CCWin.SkinControl.SkinTabControl skinTabCtrl;
        private System.Windows.Forms.Panel pnlFoot;
        private System.Windows.Forms.TableLayoutPanel tlpHome;
        private System.Windows.Forms.ListView lvDevices;
        private System.Windows.Forms.ColumnHeader colXH;
        private System.Windows.Forms.ColumnHeader colDevid;
        private System.Windows.Forms.ColumnHeader colDevName;
        private System.Windows.Forms.ColumnHeader colSiteAddr;
        private System.Windows.Forms.ColumnHeader colIPAddr;
        private System.Windows.Forms.ColumnHeader colIpPort;
        private System.Windows.Forms.ColumnHeader colOrderNum;
        private System.Windows.Forms.ColumnHeader colLinkState;
        private System.Windows.Forms.Panel pnlScopeEdit;
        private System.Windows.Forms.Button btnEditDev;
        private System.Windows.Forms.Button btnDelDev;
        private System.Windows.Forms.Button btnAddDev;
        private System.Windows.Forms.Button btnShowDev;
        private System.Windows.Forms.ColumnHeader colCoilsAddr;
        private System.Windows.Forms.ColumnHeader colRegAddr;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.ListView sysPlcInfoItemList;
        private System.Windows.Forms.ColumnHeader col_index;
        private System.Windows.Forms.ColumnHeader col_name;
        private System.Windows.Forms.ColumnHeader col_ip;
        private System.Windows.Forms.ColumnHeader col_port;
        private System.Windows.Forms.ColumnHeader col_station;
        private System.Windows.Forms.ColumnHeader col_create_time;
        private System.Windows.Forms.ColumnHeader col_op_name;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TextBox txtAlarmInfo;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tblPrint;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtQrCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnQrSearch;
        private System.Windows.Forms.GroupBox gpbDevStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flpDevices;
        private System.Drawing.Printing.PrintDocument imgPrint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudRight;
        private System.Windows.Forms.NumericUpDown nudLeft;
        private System.Windows.Forms.NumericUpDown nudDown;
        private System.Windows.Forms.NumericUpDown nudUp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TabControl tabCtrlSearch;
        private System.Windows.Forms.TabPage tpg_Spectrum;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvSpectrum;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.Panel pnlSpem;
        private System.Windows.Forms.TextBox search_source_ip;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox search_pruduct_xh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox search_pruduct_code;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker search_end_time;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker search_start_time;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.Button btnSearchAll;
        private System.Windows.Forms.Button yySearchClear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Panel pnlHeight;
        private System.Windows.Forms.Panel pnlWidth;
        private System.Windows.Forms.Panel pnlFont;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.Label label14;
        private QrCtrl.QrImage qrImage;
    }
}

