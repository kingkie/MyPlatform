namespace MyPlatForms
{
    partial class mainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cob_Parity = new System.Windows.Forms.ComboBox();
            this.lbl_Tips = new System.Windows.Forms.Label();
            this.txt_LogicAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOpenClose = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_DataBits = new System.Windows.Forms.TextBox();
            this.cob_StopBits = new System.Windows.Forms.ComboBox();
            this.comboBaudrate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboPortName = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.btnVerificate = new System.Windows.Forms.Button();
            this.picVerificate = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDapper = new System.Windows.Forms.Button();
            this.btnMuiltiIn = new System.Windows.Forms.Button();
            this.btnDapperDel = new System.Windows.Forms.Button();
            this.btnUpdateDa = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnPages = new System.Windows.Forms.Button();
            this.btnTran = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVerificate)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(76, 32);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(883, 528);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.txtShow);
            this.tabPage1.Controls.Add(this.btnVerificate);
            this.tabPage1.Controls.Add(this.picVerificate);
            this.tabPage1.Location = new System.Drawing.Point(4, 36);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(875, 488);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(564, 185);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 35);
            this.button3.TabIndex = 6;
            this.button3.Text = "接收";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(422, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 35);
            this.button2.TabIndex = 5;
            this.button2.Text = "发送";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cob_Parity);
            this.groupBox1.Controls.Add(this.lbl_Tips);
            this.groupBox1.Controls.Add(this.txt_LogicAddress);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonOpenClose);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_DataBits);
            this.groupBox1.Controls.Add(this.cob_StopBits);
            this.groupBox1.Controls.Add(this.comboBaudrate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboPortName);
            this.groupBox1.Location = new System.Drawing.Point(35, 242);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(636, 150);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            // 
            // cob_Parity
            // 
            this.cob_Parity.FormattingEnabled = true;
            this.cob_Parity.Items.AddRange(new object[] {
            "Even",
            "Mark",
            "None",
            "Odd",
            "Space"});
            this.cob_Parity.Location = new System.Drawing.Point(87, 70);
            this.cob_Parity.Margin = new System.Windows.Forms.Padding(4);
            this.cob_Parity.Name = "cob_Parity";
            this.cob_Parity.Size = new System.Drawing.Size(88, 23);
            this.cob_Parity.TabIndex = 50;
            this.cob_Parity.Text = "Even";
            // 
            // lbl_Tips
            // 
            this.lbl_Tips.AutoSize = true;
            this.lbl_Tips.Location = new System.Drawing.Point(207, 119);
            this.lbl_Tips.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Tips.Name = "lbl_Tips";
            this.lbl_Tips.Size = new System.Drawing.Size(0, 15);
            this.lbl_Tips.TabIndex = 59;
            // 
            // txt_LogicAddress
            // 
            this.txt_LogicAddress.Location = new System.Drawing.Point(460, 75);
            this.txt_LogicAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txt_LogicAddress.Name = "txt_LogicAddress";
            this.txt_LogicAddress.Size = new System.Drawing.Size(163, 25);
            this.txt_LogicAddress.TabIndex = 58;
            this.txt_LogicAddress.Text = "AAAAAAAAAA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(375, 79);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 57;
            this.label3.Text = "逻辑码：";
            // 
            // buttonOpenClose
            // 
            this.buttonOpenClose.Location = new System.Drawing.Point(511, 109);
            this.buttonOpenClose.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpenClose.Name = "buttonOpenClose";
            this.buttonOpenClose.Size = new System.Drawing.Size(113, 34);
            this.buttonOpenClose.TabIndex = 56;
            this.buttonOpenClose.Text = "Open";
            this.buttonOpenClose.UseVisualStyleBackColor = true;
            this.buttonOpenClose.Click += new System.EventHandler(this.buttonOpenClose_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 75);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 53;
            this.label5.Text = "停止位：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 54;
            this.label6.Text = "校验位：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(375, 35);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 55;
            this.label7.Text = "数据位：";
            // 
            // txt_DataBits
            // 
            this.txt_DataBits.Location = new System.Drawing.Point(460, 30);
            this.txt_DataBits.Margin = new System.Windows.Forms.Padding(4);
            this.txt_DataBits.Name = "txt_DataBits";
            this.txt_DataBits.Size = new System.Drawing.Size(163, 25);
            this.txt_DataBits.TabIndex = 52;
            this.txt_DataBits.Text = "8";
            // 
            // cob_StopBits
            // 
            this.cob_StopBits.FormattingEnabled = true;
            this.cob_StopBits.Items.AddRange(new object[] {
            "None",
            "One",
            "OnePointFive",
            "Two"});
            this.cob_StopBits.Location = new System.Drawing.Point(268, 70);
            this.cob_StopBits.Margin = new System.Windows.Forms.Padding(4);
            this.cob_StopBits.Name = "cob_StopBits";
            this.cob_StopBits.Size = new System.Drawing.Size(92, 23);
            this.cob_StopBits.TabIndex = 51;
            this.cob_StopBits.Text = "One";
            // 
            // comboBaudrate
            // 
            this.comboBaudrate.BackColor = System.Drawing.Color.White;
            this.comboBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBaudrate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBaudrate.FormattingEnabled = true;
            this.comboBaudrate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBaudrate.Location = new System.Drawing.Point(269, 29);
            this.comboBaudrate.Margin = new System.Windows.Forms.Padding(4);
            this.comboBaudrate.Name = "comboBaudrate";
            this.comboBaudrate.Size = new System.Drawing.Size(91, 23);
            this.comboBaudrate.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 47;
            this.label2.Text = "波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 46;
            this.label1.Text = "串口号：";
            // 
            // comboPortName
            // 
            this.comboPortName.BackColor = System.Drawing.Color.White;
            this.comboPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPortName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboPortName.FormattingEnabled = true;
            this.comboPortName.Location = new System.Drawing.Point(87, 29);
            this.comboPortName.Margin = new System.Windows.Forms.Padding(4);
            this.comboPortName.Name = "comboPortName";
            this.comboPortName.Size = new System.Drawing.Size(88, 23);
            this.comboPortName.TabIndex = 48;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(422, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 43);
            this.button1.TabIndex = 3;
            this.button1.Text = "初始化设备";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtShow
            // 
            this.txtShow.Location = new System.Drawing.Point(35, 124);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.Size = new System.Drawing.Size(340, 96);
            this.txtShow.TabIndex = 2;
            // 
            // btnVerificate
            // 
            this.btnVerificate.Location = new System.Drawing.Point(422, 30);
            this.btnVerificate.Name = "btnVerificate";
            this.btnVerificate.Size = new System.Drawing.Size(122, 49);
            this.btnVerificate.TabIndex = 1;
            this.btnVerificate.Text = "验证码测试";
            this.btnVerificate.UseVisualStyleBackColor = true;
            this.btnVerificate.Click += new System.EventHandler(this.btnVerificate_Click);
            // 
            // picVerificate
            // 
            this.picVerificate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVerificate.Location = new System.Drawing.Point(35, 30);
            this.picVerificate.Name = "picVerificate";
            this.picVerificate.Size = new System.Drawing.Size(340, 68);
            this.picVerificate.TabIndex = 0;
            this.picVerificate.TabStop = false;
            this.picVerificate.Click += new System.EventHandler(this.picVerificate_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnTran);
            this.tabPage2.Controls.Add(this.btnPages);
            this.tabPage2.Controls.Add(this.btnSelect);
            this.tabPage2.Controls.Add(this.btnUpdateDa);
            this.tabPage2.Controls.Add(this.btnDapperDel);
            this.tabPage2.Controls.Add(this.btnMuiltiIn);
            this.tabPage2.Controls.Add(this.btnDapper);
            this.tabPage2.Location = new System.Drawing.Point(4, 36);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(875, 488);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDapper
            // 
            this.btnDapper.Location = new System.Drawing.Point(30, 26);
            this.btnDapper.Name = "btnDapper";
            this.btnDapper.Size = new System.Drawing.Size(129, 49);
            this.btnDapper.TabIndex = 8;
            this.btnDapper.Text = "插入Modal";
            this.btnDapper.UseVisualStyleBackColor = true;
            this.btnDapper.Click += new System.EventHandler(this.btnDapper_Click);
            // 
            // btnMuiltiIn
            // 
            this.btnMuiltiIn.Location = new System.Drawing.Point(192, 26);
            this.btnMuiltiIn.Name = "btnMuiltiIn";
            this.btnMuiltiIn.Size = new System.Drawing.Size(129, 49);
            this.btnMuiltiIn.TabIndex = 9;
            this.btnMuiltiIn.Text = "插入多条数据\n";
            this.btnMuiltiIn.UseVisualStyleBackColor = true;
            this.btnMuiltiIn.Click += new System.EventHandler(this.btnMuiltiIn_Click);
            // 
            // btnDapperDel
            // 
            this.btnDapperDel.Location = new System.Drawing.Point(358, 26);
            this.btnDapperDel.Name = "btnDapperDel";
            this.btnDapperDel.Size = new System.Drawing.Size(129, 49);
            this.btnDapperDel.TabIndex = 10;
            this.btnDapperDel.Text = "删除数据";
            this.btnDapperDel.UseVisualStyleBackColor = true;
            this.btnDapperDel.Click += new System.EventHandler(this.btnDapperDel_Click);
            // 
            // btnUpdateDa
            // 
            this.btnUpdateDa.Location = new System.Drawing.Point(528, 26);
            this.btnUpdateDa.Name = "btnUpdateDa";
            this.btnUpdateDa.Size = new System.Drawing.Size(129, 49);
            this.btnUpdateDa.TabIndex = 11;
            this.btnUpdateDa.Text = "更新数据";
            this.btnUpdateDa.UseVisualStyleBackColor = true;
            this.btnUpdateDa.Click += new System.EventHandler(this.btnUpdateDa_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(30, 106);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(129, 50);
            this.btnSelect.TabIndex = 12;
            this.btnSelect.Text = "查询数据";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnPages
            // 
            this.btnPages.Location = new System.Drawing.Point(192, 106);
            this.btnPages.Name = "btnPages";
            this.btnPages.Size = new System.Drawing.Size(129, 50);
            this.btnPages.TabIndex = 13;
            this.btnPages.Text = "分页查询";
            this.btnPages.UseVisualStyleBackColor = true;
            this.btnPages.Click += new System.EventHandler(this.btnPages_Click);
            // 
            // btnTran
            // 
            this.btnTran.Location = new System.Drawing.Point(358, 106);
            this.btnTran.Name = "btnTran";
            this.btnTran.Size = new System.Drawing.Size(129, 50);
            this.btnTran.TabIndex = 14;
            this.btnTran.Text = "事务处理";
            this.btnTran.UseVisualStyleBackColor = true;
            this.btnTran.Click += new System.EventHandler(this.btnTran_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 528);
            this.Controls.Add(this.tabControl1);
            this.Name = "mainFrm";
            this.Text = "平台测试";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVerificate)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnVerificate;
        private System.Windows.Forms.PictureBox picVerificate;
        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cob_Parity;
        private System.Windows.Forms.Label lbl_Tips;
        private System.Windows.Forms.TextBox txt_LogicAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOpenClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_DataBits;
        private System.Windows.Forms.ComboBox cob_StopBits;
        private System.Windows.Forms.ComboBox comboBaudrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboPortName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDapper;
        private System.Windows.Forms.Button btnMuiltiIn;
        private System.Windows.Forms.Button btnDapperDel;
        private System.Windows.Forms.Button btnUpdateDa;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnPages;
        private System.Windows.Forms.Button btnTran;
    }
}