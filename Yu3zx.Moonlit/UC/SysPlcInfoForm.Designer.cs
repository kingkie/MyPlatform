namespace Yu3zx.Moonlit.UC
{
    partial class SysPlcInfoForm
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
            this.sysPlcInfoCannel = new System.Windows.Forms.Button();
            this.sysPlcInfoSave = new System.Windows.Forms.Button();
            this.sysPlcInfo_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sysPlcInfo_Port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sysPlcInfo_delAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sysPlcInfo_partsAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sysPlcInfo_writeAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sysPlcInfo_Info = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.sysPlcInfo_validAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.sysPlcInfo_Ip = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.sysPlcInfo_Station = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.sysPlcInfo_totalAddress = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sysPlcInfoCannel
            // 
            this.sysPlcInfoCannel.Location = new System.Drawing.Point(501, 315);
            this.sysPlcInfoCannel.Name = "sysPlcInfoCannel";
            this.sysPlcInfoCannel.Size = new System.Drawing.Size(75, 40);
            this.sysPlcInfoCannel.TabIndex = 12;
            this.sysPlcInfoCannel.Text = "取消";
            this.sysPlcInfoCannel.UseVisualStyleBackColor = true;
            this.sysPlcInfoCannel.Click += new System.EventHandler(this.sysPlcInfoCannel_Click);
            // 
            // sysPlcInfoSave
            // 
            this.sysPlcInfoSave.Location = new System.Drawing.Point(396, 315);
            this.sysPlcInfoSave.Name = "sysPlcInfoSave";
            this.sysPlcInfoSave.Size = new System.Drawing.Size(75, 40);
            this.sysPlcInfoSave.TabIndex = 11;
            this.sysPlcInfoSave.Text = "保存";
            this.sysPlcInfoSave.UseVisualStyleBackColor = true;
            this.sysPlcInfoSave.Click += new System.EventHandler(this.sysPlcInfoSave_Click);
            // 
            // sysPlcInfo_Name
            // 
            this.sysPlcInfo_Name.Location = new System.Drawing.Point(120, 24);
            this.sysPlcInfo_Name.Name = "sysPlcInfo_Name";
            this.sysPlcInfo_Name.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_Name.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "设备名称";
            // 
            // sysPlcInfo_Port
            // 
            this.sysPlcInfo_Port.Location = new System.Drawing.Point(120, 69);
            this.sysPlcInfo_Port.Name = "sysPlcInfo_Port";
            this.sysPlcInfo_Port.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_Port.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "端口";
            // 
            // sysPlcInfo_delAddress
            // 
            this.sysPlcInfo_delAddress.Location = new System.Drawing.Point(120, 115);
            this.sysPlcInfo_delAddress.Name = "sysPlcInfo_delAddress";
            this.sysPlcInfo_delAddress.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_delAddress.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 16;
            this.label3.Text = "删除地址";
            // 
            // sysPlcInfo_partsAddress
            // 
            this.sysPlcInfo_partsAddress.Location = new System.Drawing.Point(120, 161);
            this.sysPlcInfo_partsAddress.Name = "sysPlcInfo_partsAddress";
            this.sysPlcInfo_partsAddress.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_partsAddress.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 18;
            this.label4.Text = "部件码地址";
            // 
            // sysPlcInfo_writeAddress
            // 
            this.sysPlcInfo_writeAddress.Location = new System.Drawing.Point(120, 206);
            this.sysPlcInfo_writeAddress.Name = "sysPlcInfo_writeAddress";
            this.sysPlcInfo_writeAddress.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_writeAddress.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 20;
            this.label5.Text = "回写地址";
            // 
            // sysPlcInfo_Info
            // 
            this.sysPlcInfo_Info.Location = new System.Drawing.Point(523, 206);
            this.sysPlcInfo_Info.Name = "sysPlcInfo_Info";
            this.sysPlcInfo_Info.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_Info.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(415, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 22;
            this.label6.Text = "描述";
            // 
            // sysPlcInfo_validAddress
            // 
            this.sysPlcInfo_validAddress.Location = new System.Drawing.Point(523, 161);
            this.sysPlcInfo_validAddress.Name = "sysPlcInfo_validAddress";
            this.sysPlcInfo_validAddress.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_validAddress.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(415, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 24;
            this.label7.Text = "可读地址";
            // 
            // sysPlcInfo_Ip
            // 
            this.sysPlcInfo_Ip.Location = new System.Drawing.Point(523, 24);
            this.sysPlcInfo_Ip.Name = "sysPlcInfo_Ip";
            this.sysPlcInfo_Ip.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_Ip.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(415, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 26;
            this.label8.Text = "IP地址";
            // 
            // sysPlcInfo_Station
            // 
            this.sysPlcInfo_Station.Location = new System.Drawing.Point(523, 69);
            this.sysPlcInfo_Station.Name = "sysPlcInfo_Station";
            this.sysPlcInfo_Station.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_Station.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(415, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 18);
            this.label9.TabIndex = 28;
            this.label9.Text = "所在工位";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // sysPlcInfo_totalAddress
            // 
            this.sysPlcInfo_totalAddress.Location = new System.Drawing.Point(523, 115);
            this.sysPlcInfo_totalAddress.Name = "sysPlcInfo_totalAddress";
            this.sysPlcInfo_totalAddress.Size = new System.Drawing.Size(233, 28);
            this.sysPlcInfo_totalAddress.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(415, 125);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 18);
            this.label10.TabIndex = 30;
            this.label10.Text = "总成码地址";
            // 
            // SysPlcInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sysPlcInfo_totalAddress);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.sysPlcInfo_Station);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.sysPlcInfo_Ip);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.sysPlcInfo_validAddress);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.sysPlcInfo_Info);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sysPlcInfo_writeAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sysPlcInfo_partsAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sysPlcInfo_delAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sysPlcInfo_Port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sysPlcInfoCannel);
            this.Controls.Add(this.sysPlcInfoSave);
            this.Controls.Add(this.sysPlcInfo_Name);
            this.Controls.Add(this.label1);
            this.Name = "SysPlcInfoForm";
            this.Text = "设备管理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sysPlcInfoCannel;
        private System.Windows.Forms.Button sysPlcInfoSave;
        private System.Windows.Forms.TextBox sysPlcInfo_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sysPlcInfo_Port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sysPlcInfo_delAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox sysPlcInfo_partsAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox sysPlcInfo_writeAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sysPlcInfo_Info;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox sysPlcInfo_validAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox sysPlcInfo_Ip;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox sysPlcInfo_Station;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox sysPlcInfo_totalAddress;
        private System.Windows.Forms.Label label10;
    }
}