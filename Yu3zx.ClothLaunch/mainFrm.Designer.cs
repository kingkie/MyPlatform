
namespace Yu3zx.ClothLaunch
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgHome = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSpecs = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtColorNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.txtProduceNum = new System.Windows.Forms.TextBox();
            this.btnGetLenth = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rdoA = new System.Windows.Forms.RadioButton();
            this.rdoSC = new System.Windows.Forms.RadioButton();
            this.rdoHC = new System.Windows.Forms.RadioButton();
            this.rdoKC = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.tpgConfig = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnServerConfig = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tpgData = new System.Windows.Forms.TabPage();
            this.btnAddPrint = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCN = new System.Windows.Forms.TextBox();
            this.btnCurrent = new System.Windows.Forms.Button();
            this.txtSp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBN = new System.Windows.Forms.TextBox();
            this.txtPN = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tpgHome.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpgConfig.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tpgData.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgHome);
            this.tabControl1.Controls.Add(this.tpgConfig);
            this.tabControl1.Controls.Add(this.tpgData);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(96, 36);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(942, 553);
            this.tabControl1.TabIndex = 3;
            // 
            // tpgHome
            // 
            this.tpgHome.Controls.Add(this.groupBox2);
            this.tpgHome.Controls.Add(this.groupBox1);
            this.tpgHome.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tpgHome.Location = new System.Drawing.Point(4, 40);
            this.tpgHome.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgHome.Name = "tpgHome";
            this.tpgHome.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgHome.Size = new System.Drawing.Size(934, 509);
            this.tpgHome.TabIndex = 0;
            this.tpgHome.Text = "操作首页";
            this.tpgHome.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSpecs);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtColorNum);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtBatchNo);
            this.groupBox2.Controls.Add(this.txtProduceNum);
            this.groupBox2.Controls.Add(this.btnGetLenth);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(926, 379);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "面料批次信息";
            // 
            // txtSpecs
            // 
            this.txtSpecs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSpecs.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSpecs.Location = new System.Drawing.Point(453, 135);
            this.txtSpecs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSpecs.Name = "txtSpecs";
            this.txtSpecs.Size = new System.Drawing.Size(100, 35);
            this.txtSpecs.TabIndex = 12;
            this.txtSpecs.Text = "137";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(362, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "产品规格";
            // 
            // txtColorNum
            // 
            this.txtColorNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtColorNum.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtColorNum.Location = new System.Drawing.Point(212, 135);
            this.txtColorNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtColorNum.Name = "txtColorNum";
            this.txtColorNum.Size = new System.Drawing.Size(100, 35);
            this.txtColorNum.TabIndex = 10;
            this.txtColorNum.Text = "197";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(121, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "布料色号";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBatchNo.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBatchNo.Location = new System.Drawing.Point(314, 22);
            this.txtBatchNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(304, 35);
            this.txtBatchNo.TabIndex = 8;
            this.txtBatchNo.Text = "202301002";
            // 
            // txtProduceNum
            // 
            this.txtProduceNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtProduceNum.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProduceNum.Location = new System.Drawing.Point(314, 74);
            this.txtProduceNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProduceNum.Name = "txtProduceNum";
            this.txtProduceNum.Size = new System.Drawing.Size(304, 35);
            this.txtProduceNum.TabIndex = 7;
            this.txtProduceNum.Text = "50.4";
            // 
            // btnGetLenth
            // 
            this.btnGetLenth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGetLenth.Location = new System.Drawing.Point(639, 69);
            this.btnGetLenth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetLenth.Name = "btnGetLenth";
            this.btnGetLenth.Size = new System.Drawing.Size(129, 45);
            this.btnGetLenth.TabIndex = 6;
            this.btnGetLenth.Text = "获取长度";
            this.btnGetLenth.UseVisualStyleBackColor = true;
            this.btnGetLenth.Click += new System.EventHandler(this.btnGetLenth_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(3, 291);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(920, 86);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "品质";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.rdoA, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rdoSC, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.rdoHC, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rdoKC, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(225, 31);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(475, 49);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // rdoA
            // 
            this.rdoA.AutoSize = true;
            this.rdoA.Checked = true;
            this.rdoA.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoA.Location = new System.Drawing.Point(3, 2);
            this.rdoA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoA.Name = "rdoA";
            this.rdoA.Size = new System.Drawing.Size(60, 24);
            this.rdoA.TabIndex = 0;
            this.rdoA.TabStop = true;
            this.rdoA.Tag = "A";
            this.rdoA.Text = "A品";
            this.rdoA.UseVisualStyleBackColor = true;
            this.rdoA.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
            // 
            // rdoSC
            // 
            this.rdoSC.AutoSize = true;
            this.rdoSC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoSC.Location = new System.Drawing.Point(357, 2);
            this.rdoSC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoSC.Name = "rdoSC";
            this.rdoSC.Size = new System.Drawing.Size(70, 24);
            this.rdoSC.TabIndex = 3;
            this.rdoSC.Tag = "SC";
            this.rdoSC.Text = "SC品";
            this.rdoSC.UseVisualStyleBackColor = true;
            this.rdoSC.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
            // 
            // rdoHC
            // 
            this.rdoHC.AutoSize = true;
            this.rdoHC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoHC.Location = new System.Drawing.Point(121, 2);
            this.rdoHC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoHC.Name = "rdoHC";
            this.rdoHC.Size = new System.Drawing.Size(70, 24);
            this.rdoHC.TabIndex = 1;
            this.rdoHC.Tag = "HC";
            this.rdoHC.Text = "HC品";
            this.rdoHC.UseVisualStyleBackColor = true;
            this.rdoHC.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
            // 
            // rdoKC
            // 
            this.rdoKC.AutoSize = true;
            this.rdoKC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoKC.Location = new System.Drawing.Point(239, 2);
            this.rdoKC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoKC.Name = "rdoKC";
            this.rdoKC.Size = new System.Drawing.Size(70, 24);
            this.rdoKC.TabIndex = 2;
            this.rdoKC.Tag = "KC";
            this.rdoKC.Text = "KC品";
            this.rdoKC.UseVisualStyleBackColor = true;
            this.rdoKC.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(224, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "布料长度";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(224, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "布料批次";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLaunch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(4, 383);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(926, 122);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunch.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLaunch.Location = new System.Drawing.Point(709, 31);
            this.btnLaunch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(201, 69);
            this.btnLaunch.TabIndex = 0;
            this.btnLaunch.Text = "上 线";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // tpgConfig
            // 
            this.tpgConfig.Controls.Add(this.groupBox3);
            this.tpgConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tpgConfig.Location = new System.Drawing.Point(4, 40);
            this.tpgConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgConfig.Name = "tpgConfig";
            this.tpgConfig.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgConfig.Size = new System.Drawing.Size(934, 509);
            this.tpgConfig.TabIndex = 1;
            this.tpgConfig.Text = "系统设置";
            this.tpgConfig.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnServerConfig);
            this.groupBox3.Controls.Add(this.txtServerPort);
            this.groupBox3.Controls.Add(this.txtServerIp);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(4, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(926, 82);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "服务端设置";
            // 
            // btnServerConfig
            // 
            this.btnServerConfig.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnServerConfig.Location = new System.Drawing.Point(806, 26);
            this.btnServerConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnServerConfig.Name = "btnServerConfig";
            this.btnServerConfig.Size = new System.Drawing.Size(100, 43);
            this.btnServerConfig.TabIndex = 4;
            this.btnServerConfig.Text = "确  定";
            this.btnServerConfig.UseVisualStyleBackColor = true;
            this.btnServerConfig.Click += new System.EventHandler(this.btnServerConfig_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtServerPort.Location = new System.Drawing.Point(387, 36);
            this.txtServerPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(83, 27);
            this.txtServerPort.TabIndex = 3;
            this.txtServerPort.Text = "8266";
            // 
            // txtServerIp
            // 
            this.txtServerIp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtServerIp.Location = new System.Drawing.Point(113, 36);
            this.txtServerIp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(169, 27);
            this.txtServerIp.TabIndex = 2;
            this.txtServerIp.Text = "192.168.1.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "服务端口";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "服务端IP";
            // 
            // tpgData
            // 
            this.tpgData.Controls.Add(this.btnAddPrint);
            this.tpgData.Controls.Add(this.groupBox5);
            this.tpgData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tpgData.Location = new System.Drawing.Point(4, 40);
            this.tpgData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgData.Name = "tpgData";
            this.tpgData.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgData.Size = new System.Drawing.Size(934, 509);
            this.tpgData.TabIndex = 2;
            this.tpgData.Text = "数据查询";
            this.tpgData.UseVisualStyleBackColor = true;
            // 
            // btnAddPrint
            // 
            this.btnAddPrint.Location = new System.Drawing.Point(498, 338);
            this.btnAddPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddPrint.Name = "btnAddPrint";
            this.btnAddPrint.Size = new System.Drawing.Size(153, 49);
            this.btnAddPrint.TabIndex = 1;
            this.btnAddPrint.Text = "补打标签";
            this.btnAddPrint.UseVisualStyleBackColor = true;
            this.btnAddPrint.Click += new System.EventHandler(this.btnAddPrint_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox5.Controls.Add(this.txtSerial);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txtCN);
            this.groupBox5.Controls.Add(this.btnCurrent);
            this.groupBox5.Controls.Add(this.txtSp);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtBN);
            this.groupBox5.Controls.Add(this.txtPN);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(172, 41);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(576, 270);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "标签补打";
            // 
            // txtSerial
            // 
            this.txtSerial.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSerial.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSerial.Location = new System.Drawing.Point(172, 205);
            this.txtSerial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(100, 35);
            this.txtSerial.TabIndex = 23;
            this.txtSerial.Text = "197";
            this.txtSerial.TextChanged += new System.EventHandler(this.txtSerial_TextChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(83, 213);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 19);
            this.label11.TabIndex = 22;
            this.label11.Text = "当前编号";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // txtCN
            // 
            this.txtCN.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCN.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCN.Location = new System.Drawing.Point(172, 145);
            this.txtCN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCN.Name = "txtCN";
            this.txtCN.Size = new System.Drawing.Size(100, 35);
            this.txtCN.TabIndex = 18;
            this.txtCN.Text = "197";
            // 
            // btnCurrent
            // 
            this.btnCurrent.Location = new System.Drawing.Point(339, 196);
            this.btnCurrent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCurrent.Name = "btnCurrent";
            this.btnCurrent.Size = new System.Drawing.Size(140, 52);
            this.btnCurrent.TabIndex = 21;
            this.btnCurrent.Text = "获取当前标签";
            this.btnCurrent.UseVisualStyleBackColor = true;
            this.btnCurrent.Click += new System.EventHandler(this.btnCurrent_Click);
            // 
            // txtSp
            // 
            this.txtSp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSp.Location = new System.Drawing.Point(377, 145);
            this.txtSp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSp.Name = "txtSp";
            this.txtSp.Size = new System.Drawing.Size(100, 35);
            this.txtSp.TabIndex = 20;
            this.txtSp.Text = "137";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(287, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 19);
            this.label7.TabIndex = 19;
            this.label7.Text = "产品规格";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(83, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 19);
            this.label8.TabIndex = 17;
            this.label8.Text = "布料色号";
            // 
            // txtBN
            // 
            this.txtBN.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBN.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBN.Location = new System.Drawing.Point(173, 29);
            this.txtBN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBN.Name = "txtBN";
            this.txtBN.Size = new System.Drawing.Size(304, 35);
            this.txtBN.TabIndex = 16;
            this.txtBN.Text = "202301002";
            // 
            // txtPN
            // 
            this.txtPN.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPN.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPN.Location = new System.Drawing.Point(173, 84);
            this.txtPN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPN.Name = "txtPN";
            this.txtPN.Size = new System.Drawing.Size(304, 35);
            this.txtPN.TabIndex = 15;
            this.txtPN.Text = "50.4";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(83, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 19);
            this.label9.TabIndex = 14;
            this.label9.Text = "布料长度";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(83, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 19);
            this.label10.TabIndex = 13;
            this.label10.Text = "布料批次";
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 553);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "mainFrm";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainFrm_FormClosing);
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpgHome.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tpgConfig.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tpgData.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgHome;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.TextBox txtProduceNum;
        private System.Windows.Forms.Button btnGetLenth;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoSC;
        private System.Windows.Forms.RadioButton rdoKC;
        private System.Windows.Forms.RadioButton rdoHC;
        private System.Windows.Forms.RadioButton rdoA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.TabPage tpgConfig;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Button btnServerConfig;
        private System.Windows.Forms.TextBox txtColorNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtSpecs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tpgData;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtSp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBN;
        private System.Windows.Forms.TextBox txtPN;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnCurrent;
        private System.Windows.Forms.Button btnAddPrint;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.Label label11;
    }
}

