
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboServerIP = new System.Windows.Forms.ComboBox();
            this.btnService = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpgConfig = new System.Windows.Forms.TabPage();
            this.tblConfig = new System.Windows.Forms.TableLayoutPanel();
            this.cmsMenu.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tpgHome.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tpgConfig.SuspendLayout();
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
            this.tabMain.Controls.Add(this.tpgConfig);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(60, 24);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(571, 378);
            this.tabMain.TabIndex = 1;
            // 
            // tpgHome
            // 
            this.tpgHome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpgHome.Controls.Add(this.groupBox1);
            this.tpgHome.Controls.Add(this.panel2);
            this.tpgHome.Location = new System.Drawing.Point(4, 28);
            this.tpgHome.Name = "tpgHome";
            this.tpgHome.Padding = new System.Windows.Forms.Padding(3);
            this.tpgHome.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tpgHome.Size = new System.Drawing.Size(563, 346);
            this.tpgHome.TabIndex = 0;
            this.tpgHome.Text = "首页";
            this.tpgHome.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(555, 245);
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
            this.txtInfo.Size = new System.Drawing.Size(549, 225);
            this.txtInfo.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboServerIP);
            this.panel2.Controls.Add(this.btnService);
            this.panel2.Controls.Add(this.txtPort);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 248);
            this.panel2.Name = "panel2";
            this.panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel2.Size = new System.Drawing.Size(555, 93);
            this.panel2.TabIndex = 0;
            // 
            // cboServerIP
            // 
            this.cboServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboServerIP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboServerIP.FormattingEnabled = true;
            this.cboServerIP.Items.AddRange(new object[] {
            "127.0.0.1"});
            this.cboServerIP.Location = new System.Drawing.Point(67, 17);
            this.cboServerIP.Name = "cboServerIP";
            this.cboServerIP.Size = new System.Drawing.Size(151, 24);
            this.cboServerIP.TabIndex = 2;
            // 
            // btnService
            // 
            this.btnService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnService.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnService.Location = new System.Drawing.Point(398, 20);
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
            this.txtPort.Location = new System.Drawing.Point(296, 15);
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
            this.label2.Location = new System.Drawing.Point(218, 20);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务端口:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务IP:";
            // 
            // tpgConfig
            // 
            this.tpgConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpgConfig.Controls.Add(this.tblConfig);
            this.tpgConfig.Location = new System.Drawing.Point(4, 28);
            this.tpgConfig.Name = "tpgConfig";
            this.tpgConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpgConfig.Size = new System.Drawing.Size(563, 346);
            this.tpgConfig.TabIndex = 1;
            this.tpgConfig.Text = "配置";
            this.tpgConfig.UseVisualStyleBackColor = true;
            // 
            // tblConfig
            // 
            this.tblConfig.ColumnCount = 2;
            this.tblConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblConfig.Location = new System.Drawing.Point(3, 3);
            this.tblConfig.Name = "tblConfig";
            this.tblConfig.RowCount = 1;
            this.tblConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblConfig.Size = new System.Drawing.Size(555, 338);
            this.tblConfig.TabIndex = 0;
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 378);
            this.Controls.Add(this.tabMain);
            this.Name = "mainFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务V1.0";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.cmsMenu.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tpgHome.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tpgConfig.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tblConfig;
    }
}

