namespace Yu3zx.PackingMonitor
{
    partial class cameraFrm
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
            this.picViedo = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSnap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picViedo)).BeginInit();
            this.SuspendLayout();
            // 
            // picViedo
            // 
            this.picViedo.Location = new System.Drawing.Point(78, 12);
            this.picViedo.Name = "picViedo";
            this.picViedo.Size = new System.Drawing.Size(563, 378);
            this.picViedo.TabIndex = 0;
            this.picViedo.TabStop = false;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(751, 37);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(128, 44);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "打开播放";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(751, 113);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 44);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭播放";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSnap
            // 
            this.btnSnap.Location = new System.Drawing.Point(751, 191);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.Size = new System.Drawing.Size(128, 44);
            this.btnSnap.TabIndex = 3;
            this.btnSnap.Text = "截图";
            this.btnSnap.UseVisualStyleBackColor = true;
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // cameraFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 531);
            this.Controls.Add(this.btnSnap);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.picViedo);
            this.Name = "cameraFrm";
            this.Text = "cameraFrm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cameraFrm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picViedo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picViedo;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSnap;
    }
}