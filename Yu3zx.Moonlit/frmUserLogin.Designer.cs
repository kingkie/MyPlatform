namespace Yu3zx.Moonlit
{
    partial class frmUserLogin
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
            this.pnlDbSet = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDbLink = new System.Windows.Forms.TextBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtUserPwd = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.btnDbSet = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlDbSet.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDbSet
            // 
            this.pnlDbSet.Controls.Add(this.groupBox1);
            this.pnlDbSet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDbSet.Location = new System.Drawing.Point(0, 239);
            this.pnlDbSet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlDbSet.Name = "pnlDbSet";
            this.pnlDbSet.Size = new System.Drawing.Size(474, 72);
            this.pnlDbSet.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDbLink);
            this.groupBox1.Controls.Add(this.btnSet);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(474, 72);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // txtDbLink
            // 
            this.txtDbLink.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDbLink.Location = new System.Drawing.Point(126, 24);
            this.txtDbLink.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDbLink.Name = "txtDbLink";
            this.txtDbLink.Size = new System.Drawing.Size(215, 35);
            this.txtDbLink.TabIndex = 4;
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Location = new System.Drawing.Point(357, 23);
            this.btnSet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(93, 38);
            this.btnSet.TabIndex = 10;
            this.btnSet.Text = "设  置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(7, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据库连接";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(207, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 38);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取  消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(340, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 38);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确  定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtUserPwd);
            this.panel2.Controls.Add(this.txtUserId);
            this.panel2.Controls.Add(this.lblPwd);
            this.panel2.Controls.Add(this.lblUserID);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(474, 177);
            this.panel2.TabIndex = 1;
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserPwd.Location = new System.Drawing.Point(130, 97);
            this.txtUserPwd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.PasswordChar = '*';
            this.txtUserPwd.Size = new System.Drawing.Size(285, 35);
            this.txtUserPwd.TabIndex = 3;
            // 
            // txtUserId
            // 
            this.txtUserId.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserId.Location = new System.Drawing.Point(130, 29);
            this.txtUserId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(285, 35);
            this.txtUserId.TabIndex = 2;
            this.txtUserId.Text = "admin";
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPwd.Location = new System.Drawing.Point(48, 104);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(76, 22);
            this.lblPwd.TabIndex = 1;
            this.lblPwd.Text = "密  码";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserID.Location = new System.Drawing.Point(48, 36);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(76, 22);
            this.lblUserID.TabIndex = 0;
            this.lblUserID.Text = "用户名";
            // 
            // btnDbSet
            // 
            this.btnDbSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDbSet.Location = new System.Drawing.Point(20, 0);
            this.btnDbSet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDbSet.Name = "btnDbSet";
            this.btnDbSet.Size = new System.Drawing.Size(110, 38);
            this.btnDbSet.TabIndex = 10;
            this.btnDbSet.Text = "数据库设置";
            this.btnDbSet.UseVisualStyleBackColor = true;
            this.btnDbSet.Click += new System.EventHandler(this.btnDbSet_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDbSet);
            this.panel3.Controls.Add(this.btnOK);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Location = new System.Drawing.Point(0, 176);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(471, 71);
            this.panel3.TabIndex = 2;
            // 
            // frmUserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 311);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlDbSet);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.Load += new System.EventHandler(this.frmUserLogin_Load);
            this.pnlDbSet.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDbSet;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.TextBox txtUserPwd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnDbSet;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.TextBox txtDbLink;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}