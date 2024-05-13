
namespace Yu3zx.TaggingSevice
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFrm));
            this.notifyIco = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmBackto = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpgHome = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnReConn = new System.Windows.Forms.Button();
            this.swtPlc = new WinformControlLibraryExtension.SwitchButtonExt();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboServerIP = new System.Windows.Forms.ComboBox();
            this.btnService = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpgCtrl = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtClsBatchNo = new System.Windows.Forms.TextBox();
            this.btClsBatch = new System.Windows.Forms.Button();
            this.txtSysState = new System.Windows.Forms.TextBox();
            this.btnSysState = new System.Windows.Forms.Button();
            this.btnStateClear = new System.Windows.Forms.Button();
            this.btnStateSave = new System.Windows.Forms.Button();
            this.tpgConfig = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdateData = new System.Windows.Forms.Button();
            this.btnOnlineData = new System.Windows.Forms.Button();
            this.txtEReelNum = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cboQualityName = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtERollDiam = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtECWidth = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtEQString = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtESpecs = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtEColorNum = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtELen = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtEBatchNo = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cmsMenu.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tpgHome.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tpgCtrl.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tpgConfig.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIco
            // 
            this.notifyIco.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIco.ContextMenuStrip = this.cmsMenu;
            this.notifyIco.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIco.Icon")));
            this.notifyIco.Text = "notify";
            this.notifyIco.Visible = true;
            // 
            // cmsMenu
            // 
            this.cmsMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmBackto,
            this.tsmQuit});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(137, 48);
            // 
            // tsmBackto
            // 
            this.tsmBackto.Name = "tsmBackto";
            this.tsmBackto.Size = new System.Drawing.Size(136, 22);
            this.tsmBackto.Text = "返回主界面";
            this.tsmBackto.Click += new System.EventHandler(this.tsmBackto_Click);
            // 
            // tsmQuit
            // 
            this.tsmQuit.Name = "tsmQuit";
            this.tsmQuit.Size = new System.Drawing.Size(136, 22);
            this.tsmQuit.Text = "退出";
            this.tsmQuit.Click += new System.EventHandler(this.tsmQuit_Click);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpgHome);
            this.tabMain.Controls.Add(this.tpgCtrl);
            this.tabMain.Controls.Add(this.tpgConfig);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(80, 27);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(754, 474);
            this.tabMain.TabIndex = 1;
            // 
            // tpgHome
            // 
            this.tpgHome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpgHome.Controls.Add(this.tableLayoutPanel1);
            this.tpgHome.Controls.Add(this.panel2);
            this.tpgHome.Location = new System.Drawing.Point(4, 31);
            this.tpgHome.Name = "tpgHome";
            this.tpgHome.Padding = new System.Windows.Forms.Padding(3);
            this.tpgHome.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tpgHome.Size = new System.Drawing.Size(746, 439);
            this.tpgHome.TabIndex = 0;
            this.tpgHome.Text = "首页";
            this.tpgHome.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(738, 344);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(363, 338);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "消息";
            // 
            // txtInfo
            // 
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.Location = new System.Drawing.Point(3, 17);
            this.txtInfo.Margin = new System.Windows.Forms.Padding(2);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(357, 318);
            this.txtInfo.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(372, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(363, 338);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "状态";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnReConn);
            this.groupBox4.Controls.Add(this.swtPlc);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(357, 55);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PLC连接状态";
            // 
            // btnReConn
            // 
            this.btnReConn.Location = new System.Drawing.Point(248, 20);
            this.btnReConn.Margin = new System.Windows.Forms.Padding(2);
            this.btnReConn.Name = "btnReConn";
            this.btnReConn.Size = new System.Drawing.Size(73, 25);
            this.btnReConn.TabIndex = 2;
            this.btnReConn.Text = "重新连接";
            this.btnReConn.UseVisualStyleBackColor = true;
            this.btnReConn.Click += new System.EventHandler(this.btnReConn_Click);
            // 
            // swtPlc
            // 
            this.swtPlc.ActivateColor = System.Drawing.Color.Transparent;
            this.swtPlc.BorderThickness = 1;
            this.swtPlc.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.swtPlc.CheckedSlideBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.swtPlc.CheckedText = "Link";
            this.swtPlc.DisableBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.swtPlc.DisableSlideBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.swtPlc.DisableSlideBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.swtPlc.DisableTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.swtPlc.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.swtPlc.Location = new System.Drawing.Point(15, 21);
            this.swtPlc.Margin = new System.Windows.Forms.Padding(2);
            this.swtPlc.Name = "swtPlc";
            this.swtPlc.Size = new System.Drawing.Size(50, 24);
            this.swtPlc.Style = WinformControlLibraryExtension.SwitchButtonExt.SwitchSlideStyles.Internal;
            this.swtPlc.TabIndex = 1;
            this.swtPlc.UnCheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.swtPlc.UnCheckedBorderColor = System.Drawing.Color.Transparent;
            this.swtPlc.UnCheckedSlideBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.swtPlc.UnCheckedSlideBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.swtPlc.UnCheckedText = "";
            this.swtPlc.UnCheckedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboServerIP);
            this.panel2.Controls.Add(this.btnService);
            this.panel2.Controls.Add(this.txtPort);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 347);
            this.panel2.Name = "panel2";
            this.panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel2.Size = new System.Drawing.Size(738, 87);
            this.panel2.TabIndex = 0;
            // 
            // cboServerIP
            // 
            this.cboServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboServerIP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboServerIP.FormattingEnabled = true;
            this.cboServerIP.Items.AddRange(new object[] {
            "127.0.0.1"});
            this.cboServerIP.Location = new System.Drawing.Point(67, 29);
            this.cboServerIP.Name = "cboServerIP";
            this.cboServerIP.Size = new System.Drawing.Size(151, 24);
            this.cboServerIP.TabIndex = 2;
            // 
            // btnService
            // 
            this.btnService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnService.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnService.Location = new System.Drawing.Point(582, 17);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(137, 53);
            this.btnService.TabIndex = 4;
            this.btnService.Text = "启动服务";
            this.btnService.UseVisualStyleBackColor = true;
            this.btnService.Click += new System.EventHandler(this.btnService_Click);
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPort.Location = new System.Drawing.Point(296, 28);
            this.txtPort.Name = "txtPort";
            this.txtPort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPort.Size = new System.Drawing.Size(56, 26);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "65535";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(218, 32);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务端口:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务IP:";
            // 
            // tpgCtrl
            // 
            this.tpgCtrl.Controls.Add(this.groupBox2);
            this.tpgCtrl.Controls.Add(this.txtSysState);
            this.tpgCtrl.Controls.Add(this.btnSysState);
            this.tpgCtrl.Controls.Add(this.btnStateClear);
            this.tpgCtrl.Controls.Add(this.btnStateSave);
            this.tpgCtrl.Location = new System.Drawing.Point(4, 31);
            this.tpgCtrl.Name = "tpgCtrl";
            this.tpgCtrl.Size = new System.Drawing.Size(746, 439);
            this.tpgCtrl.TabIndex = 2;
            this.tpgCtrl.Text = "操作";
            this.tpgCtrl.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtClsBatchNo);
            this.groupBox2.Controls.Add(this.btClsBatch);
            this.groupBox2.Location = new System.Drawing.Point(458, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 61);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "批次号";
            // 
            // txtClsBatchNo
            // 
            this.txtClsBatchNo.Location = new System.Drawing.Point(6, 21);
            this.txtClsBatchNo.Name = "txtClsBatchNo";
            this.txtClsBatchNo.Size = new System.Drawing.Size(126, 21);
            this.txtClsBatchNo.TabIndex = 3;
            // 
            // btClsBatch
            // 
            this.btClsBatch.Location = new System.Drawing.Point(150, 11);
            this.btClsBatch.Margin = new System.Windows.Forms.Padding(2);
            this.btClsBatch.Name = "btClsBatch";
            this.btClsBatch.Size = new System.Drawing.Size(113, 39);
            this.btClsBatch.TabIndex = 2;
            this.btClsBatch.Text = "清除此批次状态";
            this.btClsBatch.UseVisualStyleBackColor = true;
            this.btClsBatch.Click += new System.EventHandler(this.btClsBatch_Click);
            // 
            // txtSysState
            // 
            this.txtSysState.Location = new System.Drawing.Point(18, 74);
            this.txtSysState.Multiline = true;
            this.txtSysState.Name = "txtSysState";
            this.txtSysState.Size = new System.Drawing.Size(720, 357);
            this.txtSysState.TabIndex = 3;
            // 
            // btnSysState
            // 
            this.btnSysState.Location = new System.Drawing.Point(227, 18);
            this.btnSysState.Margin = new System.Windows.Forms.Padding(2);
            this.btnSysState.Name = "btnSysState";
            this.btnSysState.Size = new System.Drawing.Size(82, 39);
            this.btnSysState.TabIndex = 2;
            this.btnSysState.Text = "查看状态";
            this.btnSysState.UseVisualStyleBackColor = true;
            this.btnSysState.Click += new System.EventHandler(this.btnSysState_Click);
            // 
            // btnStateClear
            // 
            this.btnStateClear.Location = new System.Drawing.Point(119, 18);
            this.btnStateClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnStateClear.Name = "btnStateClear";
            this.btnStateClear.Size = new System.Drawing.Size(82, 39);
            this.btnStateClear.TabIndex = 1;
            this.btnStateClear.Text = "清除状态";
            this.btnStateClear.UseVisualStyleBackColor = true;
            this.btnStateClear.Click += new System.EventHandler(this.btnStateClear_Click);
            // 
            // btnStateSave
            // 
            this.btnStateSave.Location = new System.Drawing.Point(18, 18);
            this.btnStateSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnStateSave.Name = "btnStateSave";
            this.btnStateSave.Size = new System.Drawing.Size(82, 39);
            this.btnStateSave.TabIndex = 0;
            this.btnStateSave.Text = "保存状态";
            this.btnStateSave.UseVisualStyleBackColor = true;
            this.btnStateSave.Click += new System.EventHandler(this.btnStateSave_Click);
            // 
            // tpgConfig
            // 
            this.tpgConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpgConfig.Controls.Add(this.groupBox5);
            this.tpgConfig.Location = new System.Drawing.Point(4, 31);
            this.tpgConfig.Name = "tpgConfig";
            this.tpgConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpgConfig.Size = new System.Drawing.Size(746, 439);
            this.tpgConfig.TabIndex = 1;
            this.tpgConfig.Text = "数据编辑";
            this.tpgConfig.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtLine);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.btnUpdateData);
            this.groupBox5.Controls.Add(this.btnOnlineData);
            this.groupBox5.Controls.Add(this.txtEReelNum);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.groupBox8);
            this.groupBox5.Controls.Add(this.txtEBatchNo);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(738, 431);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            // 
            // txtLine
            // 
            this.txtLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtLine.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLine.Location = new System.Drawing.Point(143, 32);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(73, 29);
            this.txtLine.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(106, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 31;
            this.label3.Text = "线号";
            // 
            // btnUpdateData
            // 
            this.btnUpdateData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpdateData.Location = new System.Drawing.Point(507, 370);
            this.btnUpdateData.Name = "btnUpdateData";
            this.btnUpdateData.Size = new System.Drawing.Size(114, 42);
            this.btnUpdateData.TabIndex = 30;
            this.btnUpdateData.Text = "修改数据";
            this.btnUpdateData.UseVisualStyleBackColor = true;
            this.btnUpdateData.Click += new System.EventHandler(this.btnUpdateData_Click);
            // 
            // btnOnlineData
            // 
            this.btnOnlineData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOnlineData.Location = new System.Drawing.Point(577, 26);
            this.btnOnlineData.Name = "btnOnlineData";
            this.btnOnlineData.Size = new System.Drawing.Size(114, 42);
            this.btnOnlineData.TabIndex = 29;
            this.btnOnlineData.Text = "获取数据";
            this.btnOnlineData.UseVisualStyleBackColor = true;
            this.btnOnlineData.Click += new System.EventHandler(this.btnOnlineData_Click);
            // 
            // txtEReelNum
            // 
            this.txtEReelNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtEReelNum.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEReelNum.Location = new System.Drawing.Point(478, 32);
            this.txtEReelNum.Name = "txtEReelNum";
            this.txtEReelNum.Size = new System.Drawing.Size(73, 29);
            this.txtEReelNum.TabIndex = 28;
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(415, 39);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 15);
            this.label20.TabIndex = 27;
            this.label20.Text = "布料卷号";
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox8.Controls.Add(this.cboQualityName);
            this.groupBox8.Controls.Add(this.label23);
            this.groupBox8.Controls.Add(this.txtERollDiam);
            this.groupBox8.Controls.Add(this.label15);
            this.groupBox8.Controls.Add(this.txtECWidth);
            this.groupBox8.Controls.Add(this.label16);
            this.groupBox8.Controls.Add(this.txtEQString);
            this.groupBox8.Controls.Add(this.label17);
            this.groupBox8.Controls.Add(this.txtESpecs);
            this.groupBox8.Controls.Add(this.label18);
            this.groupBox8.Controls.Add(this.txtEColorNum);
            this.groupBox8.Controls.Add(this.label21);
            this.groupBox8.Controls.Add(this.txtELen);
            this.groupBox8.Controls.Add(this.label22);
            this.groupBox8.Location = new System.Drawing.Point(104, 74);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(516, 274);
            this.groupBox8.TabIndex = 24;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "标签修改";
            // 
            // cboQualityName
            // 
            this.cboQualityName.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboQualityName.FormattingEnabled = true;
            this.cboQualityName.Items.AddRange(new object[] {
            "A",
            "HC",
            "KC",
            "SC"});
            this.cboQualityName.Location = new System.Drawing.Point(158, 230);
            this.cboQualityName.Margin = new System.Windows.Forms.Padding(2);
            this.cboQualityName.Name = "cboQualityName";
            this.cboQualityName.Size = new System.Drawing.Size(92, 26);
            this.cboQualityName.TabIndex = 32;
            this.cboQualityName.Text = "A";
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(85, 235);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(67, 15);
            this.label23.TabIndex = 31;
            this.label23.Text = "检测品质";
            // 
            // txtERollDiam
            // 
            this.txtERollDiam.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtERollDiam.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtERollDiam.Location = new System.Drawing.Point(331, 183);
            this.txtERollDiam.Name = "txtERollDiam";
            this.txtERollDiam.Size = new System.Drawing.Size(84, 29);
            this.txtERollDiam.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(256, 189);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 15);
            this.label15.TabIndex = 29;
            this.label15.Text = "布料卷径";
            // 
            // txtECWidth
            // 
            this.txtECWidth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtECWidth.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtECWidth.Location = new System.Drawing.Point(158, 183);
            this.txtECWidth.Name = "txtECWidth";
            this.txtECWidth.Size = new System.Drawing.Size(92, 29);
            this.txtECWidth.TabIndex = 28;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(85, 189);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 15);
            this.label16.TabIndex = 27;
            this.label16.Text = "布料宽度";
            // 
            // txtEQString
            // 
            this.txtEQString.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtEQString.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEQString.Location = new System.Drawing.Point(158, 86);
            this.txtEQString.Name = "txtEQString";
            this.txtEQString.Size = new System.Drawing.Size(257, 29);
            this.txtEQString.TabIndex = 26;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(82, 91);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 15);
            this.label17.TabIndex = 25;
            this.label17.Text = "品    名";
            // 
            // txtESpecs
            // 
            this.txtESpecs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtESpecs.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtESpecs.Location = new System.Drawing.Point(331, 135);
            this.txtESpecs.Name = "txtESpecs";
            this.txtESpecs.Size = new System.Drawing.Size(84, 29);
            this.txtESpecs.TabIndex = 24;
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(255, 141);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 15);
            this.label18.TabIndex = 23;
            this.label18.Text = "产品规格";
            // 
            // txtEColorNum
            // 
            this.txtEColorNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtEColorNum.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEColorNum.Location = new System.Drawing.Point(158, 135);
            this.txtEColorNum.Name = "txtEColorNum";
            this.txtEColorNum.Size = new System.Drawing.Size(92, 29);
            this.txtEColorNum.TabIndex = 22;
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(85, 141);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(67, 15);
            this.label21.TabIndex = 21;
            this.label21.Text = "布料色号";
            // 
            // txtELen
            // 
            this.txtELen.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtELen.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtELen.Location = new System.Drawing.Point(158, 38);
            this.txtELen.Name = "txtELen";
            this.txtELen.Size = new System.Drawing.Size(257, 32);
            this.txtELen.TabIndex = 20;
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(85, 43);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 15);
            this.label22.TabIndex = 19;
            this.label22.Text = "布料长度";
            // 
            // txtEBatchNo
            // 
            this.txtEBatchNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtEBatchNo.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEBatchNo.Location = new System.Drawing.Point(287, 32);
            this.txtEBatchNo.Name = "txtEBatchNo";
            this.txtEBatchNo.Size = new System.Drawing.Size(122, 29);
            this.txtEBatchNo.TabIndex = 26;
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(220, 39);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 15);
            this.label19.TabIndex = 25;
            this.label19.Text = "布料批次";
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 474);
            this.Controls.Add(this.tabMain);
            this.Name = "mainFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务V1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainFrm_FormClosing);
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.cmsMenu.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tpgHome.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tpgCtrl.ResumeLayout(false);
            this.tpgCtrl.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tpgConfig.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmBackto;
        private System.Windows.Forms.ToolStripMenuItem tsmQuit;
        public System.Windows.Forms.NotifyIcon notifyIco;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpgHome;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cboServerIP;
        private System.Windows.Forms.Button btnService;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpgConfig;
        private System.Windows.Forms.TabPage tpgCtrl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private WinformControlLibraryExtension.SwitchButtonExt swtPlc;
        private System.Windows.Forms.Button btnStateSave;
        private System.Windows.Forms.Button btnReConn;
        private System.Windows.Forms.Button btnStateClear;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnUpdateData;
        private System.Windows.Forms.Button btnOnlineData;
        private System.Windows.Forms.TextBox txtEReelNum;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cboQualityName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtERollDiam;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtECWidth;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtEQString;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtESpecs;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtEColorNum;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtELen;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtEBatchNo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSysState;
        private System.Windows.Forms.TextBox txtSysState;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btClsBatch;
        private System.Windows.Forms.TextBox txtClsBatchNo;
    }
}

