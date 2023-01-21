namespace Yu3zx.Moonlit.QrCtrl
{
    partial class UCDevStatus
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStatus = new System.Windows.Forms.Button();
            this.lblDevName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStatus
            // 
            this.btnStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatus.Location = new System.Drawing.Point(338, 0);
            this.btnStatus.Margin = new System.Windows.Forms.Padding(0);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(40, 35);
            this.btnStatus.TabIndex = 1;
            this.btnStatus.UseVisualStyleBackColor = true;
            // 
            // lblDevName
            // 
            this.lblDevName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDevName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDevName.Location = new System.Drawing.Point(0, 0);
            this.lblDevName.Name = "lblDevName";
            this.lblDevName.Size = new System.Drawing.Size(378, 35);
            this.lblDevName.TabIndex = 2;
            this.lblDevName.Text = "label1";
            this.lblDevName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCDevStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnStatus);
            this.Controls.Add(this.lblDevName);
            this.Name = "UCDevStatus";
            this.Size = new System.Drawing.Size(378, 35);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Label lblDevName;
    }
}
