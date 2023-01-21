namespace Yu3zx.Enroll
{
    partial class EnrollFrm
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
            this.pnlCmd = new System.Windows.Forms.Panel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.gpbContent = new System.Windows.Forms.GroupBox();
            this.txtRegister = new System.Windows.Forms.TextBox();
            this.txtMacCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCmd.SuspendLayout();
            this.gpbContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCmd
            // 
            this.pnlCmd.Controls.Add(this.btnRegister);
            this.pnlCmd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCmd.Location = new System.Drawing.Point(0, 179);
            this.pnlCmd.Name = "pnlCmd";
            this.pnlCmd.Size = new System.Drawing.Size(493, 66);
            this.pnlCmd.TabIndex = 0;
            // 
            // btnRegister
            // 
            this.btnRegister.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegister.Location = new System.Drawing.Point(310, 13);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(146, 42);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "注  册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // gpbContent
            // 
            this.gpbContent.Controls.Add(this.txtRegister);
            this.gpbContent.Controls.Add(this.txtMacCode);
            this.gpbContent.Controls.Add(this.label2);
            this.gpbContent.Controls.Add(this.label1);
            this.gpbContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbContent.Location = new System.Drawing.Point(0, 0);
            this.gpbContent.Name = "gpbContent";
            this.gpbContent.Size = new System.Drawing.Size(493, 179);
            this.gpbContent.TabIndex = 1;
            this.gpbContent.TabStop = false;
            // 
            // txtRegister
            // 
            this.txtRegister.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRegister.Location = new System.Drawing.Point(111, 85);
            this.txtRegister.Multiline = true;
            this.txtRegister.Name = "txtRegister";
            this.txtRegister.Size = new System.Drawing.Size(345, 69);
            this.txtRegister.TabIndex = 3;
            // 
            // txtMacCode
            // 
            this.txtMacCode.Enabled = false;
            this.txtMacCode.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMacCode.Location = new System.Drawing.Point(111, 35);
            this.txtMacCode.Name = "txtMacCode";
            this.txtMacCode.Size = new System.Drawing.Size(345, 28);
            this.txtMacCode.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(25, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "注册码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "机器码:";
            // 
            // EnrollFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 245);
            this.Controls.Add(this.gpbContent);
            this.Controls.Add(this.pnlCmd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnrollFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登记注册";
            this.Load += new System.EventHandler(this.EnrollFrm_Load);
            this.pnlCmd.ResumeLayout(false);
            this.gpbContent.ResumeLayout(false);
            this.gpbContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCmd;
        private System.Windows.Forms.GroupBox gpbContent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegister;
        private System.Windows.Forms.TextBox txtMacCode;
        private System.Windows.Forms.Button btnRegister;
    }
}

