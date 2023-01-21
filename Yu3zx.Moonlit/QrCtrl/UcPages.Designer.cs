namespace Yu3zx.Moonlit.QrCtrl
{
    partial class UcPages
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
            this.btnForward = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.pnlOther = new System.Windows.Forms.Panel();
            this.lblMsg4 = new System.Windows.Forms.Label();
            this.lblMsg3 = new System.Windows.Forms.Label();
            this.txtPageSize = new System.Windows.Forms.TextBox();
            this.btnJumpGo = new System.Windows.Forms.Button();
            this.txtPageNum = new System.Windows.Forms.TextBox();
            this.pnlPreAndNext = new System.Windows.Forms.Panel();
            this.lblCnt = new System.Windows.Forms.Label();
            this.pnlOther.SuspendLayout();
            this.pnlPreAndNext.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnForward
            // 
            this.btnForward.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnForward.Location = new System.Drawing.Point(0, 0);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 41);
            this.btnForward.TabIndex = 0;
            this.btnForward.Text = "上一页";
            this.btnForward.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.Location = new System.Drawing.Point(190, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 41);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // pnlOther
            // 
            this.pnlOther.Controls.Add(this.lblMsg4);
            this.pnlOther.Controls.Add(this.lblMsg3);
            this.pnlOther.Controls.Add(this.txtPageSize);
            this.pnlOther.Controls.Add(this.btnJumpGo);
            this.pnlOther.Controls.Add(this.txtPageNum);
            this.pnlOther.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlOther.Location = new System.Drawing.Point(265, 0);
            this.pnlOther.Name = "pnlOther";
            this.pnlOther.Size = new System.Drawing.Size(260, 41);
            this.pnlOther.TabIndex = 2;
            // 
            // lblMsg4
            // 
            this.lblMsg4.AutoSize = true;
            this.lblMsg4.Location = new System.Drawing.Point(98, 14);
            this.lblMsg4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMsg4.Name = "lblMsg4";
            this.lblMsg4.Size = new System.Drawing.Size(22, 15);
            this.lblMsg4.TabIndex = 58;
            this.lblMsg4.Text = "条";
            // 
            // lblMsg3
            // 
            this.lblMsg3.AutoSize = true;
            this.lblMsg3.Location = new System.Drawing.Point(14, 14);
            this.lblMsg3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMsg3.Name = "lblMsg3";
            this.lblMsg3.Size = new System.Drawing.Size(37, 15);
            this.lblMsg3.TabIndex = 57;
            this.lblMsg3.Text = "每页";
            // 
            // txtPageSize
            // 
            this.txtPageSize.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPageSize.Location = new System.Drawing.Point(54, 9);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtPageSize.MaxLength = 7;
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(41, 25);
            this.txtPageSize.TabIndex = 56;
            this.txtPageSize.Text = "100";
            this.txtPageSize.TextChanged += new System.EventHandler(this.txtPageSize_TextChanged);
            // 
            // btnJumpGo
            // 
            this.btnJumpGo.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnJumpGo.ForeColor = System.Drawing.Color.Black;
            this.btnJumpGo.Location = new System.Drawing.Point(193, 7);
            this.btnJumpGo.Margin = new System.Windows.Forms.Padding(4);
            this.btnJumpGo.Name = "btnJumpGo";
            this.btnJumpGo.Size = new System.Drawing.Size(60, 29);
            this.btnJumpGo.TabIndex = 50;
            this.btnJumpGo.Text = "跳转";
            this.btnJumpGo.UseVisualStyleBackColor = false;
            this.btnJumpGo.Click += new System.EventHandler(this.btnJumpGo_Click);
            // 
            // txtPageNum
            // 
            this.txtPageNum.Location = new System.Drawing.Point(142, 9);
            this.txtPageNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtPageNum.Name = "txtPageNum";
            this.txtPageNum.Size = new System.Drawing.Size(43, 25);
            this.txtPageNum.TabIndex = 49;
            // 
            // pnlPreAndNext
            // 
            this.pnlPreAndNext.Controls.Add(this.lblCnt);
            this.pnlPreAndNext.Controls.Add(this.btnNext);
            this.pnlPreAndNext.Controls.Add(this.btnForward);
            this.pnlPreAndNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreAndNext.Location = new System.Drawing.Point(0, 0);
            this.pnlPreAndNext.Name = "pnlPreAndNext";
            this.pnlPreAndNext.Size = new System.Drawing.Size(265, 41);
            this.pnlPreAndNext.TabIndex = 3;
            // 
            // lblCnt
            // 
            this.lblCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblCnt.AutoSize = true;
            this.lblCnt.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCnt.Location = new System.Drawing.Point(115, 12);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(39, 19);
            this.lblCnt.TabIndex = 2;
            this.lblCnt.Text = "1/1";
            // 
            // UcPages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPreAndNext);
            this.Controls.Add(this.pnlOther);
            this.Name = "UcPages";
            this.Size = new System.Drawing.Size(525, 41);
            this.pnlOther.ResumeLayout(false);
            this.pnlOther.PerformLayout();
            this.pnlPreAndNext.ResumeLayout(false);
            this.pnlPreAndNext.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Panel pnlOther;
        private System.Windows.Forms.Button btnJumpGo;
        private System.Windows.Forms.TextBox txtPageNum;
        private System.Windows.Forms.Panel pnlPreAndNext;
        private System.Windows.Forms.Label lblCnt;
        private System.Windows.Forms.Label lblMsg4;
        private System.Windows.Forms.Label lblMsg3;
        private System.Windows.Forms.TextBox txtPageSize;
    }
}
