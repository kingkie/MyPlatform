namespace Yu3zx.Moonlit.QrCtrl
{
    partial class UC_QrCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_QrCode));
            this.lblContent = new System.Windows.Forms.Label();
            this.QrImgCtrl = new Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeImgControl();
            ((System.ComponentModel.ISupportInitialize)(this.QrImgCtrl)).BeginInit();
            this.SuspendLayout();
            // 
            // lblContent
            // 
            this.lblContent.BackColor = System.Drawing.Color.Transparent;
            this.lblContent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblContent.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblContent.Location = new System.Drawing.Point(0, 340);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(366, 15);
            this.lblContent.TabIndex = 1;
            this.lblContent.Text = "801000.19090910-0001.50000.TY63002";
            this.lblContent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QrImgCtrl
            // 
            this.QrImgCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QrImgCtrl.ErrorCorrectLevel = Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.L;
            this.QrImgCtrl.Image = ((System.Drawing.Image)(resources.GetObject("QrImgCtrl.Image")));
            this.QrImgCtrl.Location = new System.Drawing.Point(0, 0);
            this.QrImgCtrl.Margin = new System.Windows.Forms.Padding(1);
            this.QrImgCtrl.Name = "QrImgCtrl";
            this.QrImgCtrl.QuietZoneModule = Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules.Zero;
            this.QrImgCtrl.Size = new System.Drawing.Size(366, 340);
            this.QrImgCtrl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.QrImgCtrl.TabIndex = 2;
            this.QrImgCtrl.TabStop = false;
            this.QrImgCtrl.Text = "yu3zx.com";
            // 
            // UC_QrCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.QrImgCtrl);
            this.Controls.Add(this.lblContent);
            this.Name = "UC_QrCode";
            this.Size = new System.Drawing.Size(366, 355);
            ((System.ComponentModel.ISupportInitialize)(this.QrImgCtrl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblContent;
        private Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeImgControl QrImgCtrl;
    }
}
