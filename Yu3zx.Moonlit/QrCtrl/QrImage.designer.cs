namespace Yu3zx.Moonlit.QrCtrl
{
    partial class QrImage
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.qrPic = new System.Windows.Forms.PictureBox();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.qrPic)).BeginInit();
            this.SuspendLayout();
            // 
            // qrPic
            // 
            this.qrPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qrPic.Location = new System.Drawing.Point(0, 0);
            this.qrPic.Name = "qrPic";
            this.qrPic.Size = new System.Drawing.Size(168, 179);
            this.qrPic.TabIndex = 0;
            this.qrPic.TabStop = false;
            // 
            // printDoc
            // 
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            // 
            // QrImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.qrPic);
            this.Name = "QrImage";
            this.Size = new System.Drawing.Size(168, 179);
            this.Load += new System.EventHandler(this.QrImage_Load);
            this.Resize += new System.EventHandler(this.QrImage_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.qrPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox qrPic;
        private System.Drawing.Printing.PrintDocument printDoc;

    }
}
