namespace Yu3zx.RegTools
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
            this.btnRegTool = new System.Windows.Forms.Button();
            this.btnZxing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRegTool
            // 
            this.btnRegTool.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegTool.Location = new System.Drawing.Point(158, 47);
            this.btnRegTool.Name = "btnRegTool";
            this.btnRegTool.Size = new System.Drawing.Size(153, 55);
            this.btnRegTool.TabIndex = 0;
            this.btnRegTool.Text = "打开注册工具";
            this.btnRegTool.UseVisualStyleBackColor = true;
            this.btnRegTool.Click += new System.EventHandler(this.btnRegTool_Click);
            // 
            // btnZxing
            // 
            this.btnZxing.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnZxing.Location = new System.Drawing.Point(158, 119);
            this.btnZxing.Name = "btnZxing";
            this.btnZxing.Size = new System.Drawing.Size(153, 55);
            this.btnZxing.TabIndex = 1;
            this.btnZxing.Text = "打开条码工具";
            this.btnZxing.UseVisualStyleBackColor = true;
            this.btnZxing.Click += new System.EventHandler(this.btnZxing_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 291);
            this.Controls.Add(this.btnZxing);
            this.Controls.Add(this.btnRegTool);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册工具";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegTool;
        private System.Windows.Forms.Button btnZxing;
    }
}

