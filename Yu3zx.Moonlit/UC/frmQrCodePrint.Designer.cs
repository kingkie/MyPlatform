namespace Yu3zx.Moonlit.UC
{
    partial class frmQrCodePrint
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
            this.pnlCmdTop = new System.Windows.Forms.Panel();
            this.btnSeach = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQrCode = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.nudRight = new System.Windows.Forms.NumericUpDown();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.nudDown = new System.Windows.Forms.NumericUpDown();
            this.nudUp = new System.Windows.Forms.NumericUpDown();
            this.pnlSetLeft = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.pnlQcSumCode = new System.Windows.Forms.Panel();
            this.qrCode = new Yu3zx.Moonlit.QrCtrl.UC_QrCode();
            this.imgPrint = new System.Drawing.Printing.PrintDocument();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.pnlCmdTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUp)).BeginInit();
            this.pnlSetLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.pnlQcSumCode.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCmdTop
            // 
            this.pnlCmdTop.Controls.Add(this.btnSeach);
            this.pnlCmdTop.Controls.Add(this.label1);
            this.pnlCmdTop.Controls.Add(this.txtQrCode);
            this.pnlCmdTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCmdTop.Location = new System.Drawing.Point(4, 28);
            this.pnlCmdTop.Name = "pnlCmdTop";
            this.pnlCmdTop.Size = new System.Drawing.Size(892, 76);
            this.pnlCmdTop.TabIndex = 0;
            // 
            // btnSeach
            // 
            this.btnSeach.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSeach.Location = new System.Drawing.Point(665, 22);
            this.btnSeach.Name = "btnSeach";
            this.btnSeach.Size = new System.Drawing.Size(117, 35);
            this.btnSeach.TabIndex = 2;
            this.btnSeach.Text = "查  询";
            this.btnSeach.UseVisualStyleBackColor = true;
            this.btnSeach.Click += new System.EventHandler(this.btnSeach_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(73, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "总成二维码";
            // 
            // txtQrCode
            // 
            this.txtQrCode.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQrCode.Location = new System.Drawing.Point(190, 22);
            this.txtQrCode.Name = "txtQrCode";
            this.txtQrCode.Size = new System.Drawing.Size(452, 34);
            this.txtQrCode.TabIndex = 0;
            this.txtQrCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQrCode_KeyUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.panel6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlSetLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlQcSumCode, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 281);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(892, 435);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.nudRight);
            this.panel6.Controls.Add(this.nudLeft);
            this.panel6.Controls.Add(this.nudDown);
            this.panel6.Controls.Add(this.nudUp);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(672, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(217, 429);
            this.panel6.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "二维码打印边距";
            // 
            // nudRight
            // 
            this.nudRight.Location = new System.Drawing.Point(137, 296);
            this.nudRight.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudRight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRight.Name = "nudRight";
            this.nudRight.Size = new System.Drawing.Size(52, 25);
            this.nudRight.TabIndex = 12;
            this.nudRight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudLeft
            // 
            this.nudLeft.Location = new System.Drawing.Point(23, 296);
            this.nudLeft.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudLeft.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new System.Drawing.Size(52, 25);
            this.nudLeft.TabIndex = 11;
            this.nudLeft.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudDown
            // 
            this.nudDown.Location = new System.Drawing.Point(81, 338);
            this.nudDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDown.Name = "nudDown";
            this.nudDown.Size = new System.Drawing.Size(52, 25);
            this.nudDown.TabIndex = 10;
            this.nudDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudUp
            // 
            this.nudUp.Location = new System.Drawing.Point(81, 255);
            this.nudUp.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudUp.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudUp.Name = "nudUp";
            this.nudUp.Size = new System.Drawing.Size(52, 25);
            this.nudUp.TabIndex = 9;
            this.nudUp.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // pnlSetLeft
            // 
            this.pnlSetLeft.Controls.Add(this.label11);
            this.pnlSetLeft.Controls.Add(this.label13);
            this.pnlSetLeft.Controls.Add(this.nudWidth);
            this.pnlSetLeft.Controls.Add(this.nudHeight);
            this.pnlSetLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSetLeft.Location = new System.Drawing.Point(3, 3);
            this.pnlSetLeft.Name = "pnlSetLeft";
            this.pnlSetLeft.Size = new System.Drawing.Size(217, 429);
            this.pnlSetLeft.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(70, 314);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 15);
            this.label11.TabIndex = 11;
            this.label11.Text = "二维码宽";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(70, 249);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 15);
            this.label13.TabIndex = 10;
            this.label13.Text = "二维码高";
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(72, 338);
            this.nudWidth.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(66, 25);
            this.nudWidth.TabIndex = 9;
            this.nudWidth.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(72, 273);
            this.nudHeight.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(66, 25);
            this.nudHeight.TabIndex = 8;
            this.nudHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // pnlQcSumCode
            // 
            this.pnlQcSumCode.Controls.Add(this.qrCode);
            this.pnlQcSumCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQcSumCode.Location = new System.Drawing.Point(226, 3);
            this.pnlQcSumCode.Name = "pnlQcSumCode";
            this.pnlQcSumCode.Size = new System.Drawing.Size(440, 429);
            this.pnlQcSumCode.TabIndex = 2;
            // 
            // qrCode
            // 
            this.qrCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qrCode.BackColor = System.Drawing.Color.Transparent;
            this.qrCode.Location = new System.Drawing.Point(182, 157);
            this.qrCode.Name = "qrCode";
            this.qrCode.QrFontSize = null;
            this.qrCode.SetPicString = "yu3zx.com";
            this.qrCode.Size = new System.Drawing.Size(81, 96);
            this.qrCode.TabIndex = 1;
            // 
            // imgPrint
            // 
            this.imgPrint.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.imgPrint_PrintPage);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtShow);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(892, 177);
            this.panel1.TabIndex = 3;
            // 
            // txtShow
            // 
            this.txtShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtShow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShow.Location = new System.Drawing.Point(0, 0);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.Size = new System.Drawing.Size(892, 177);
            this.txtShow.TabIndex = 0;
            // 
            // frmQrCodePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(900, 720);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pnlCmdTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQrCodePrint";
            this.ShowDrawIcon = false;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "总成二维码打印";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmQrCodePrint_FormClosed);
            this.pnlCmdTop.ResumeLayout(false);
            this.pnlCmdTop.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUp)).EndInit();
            this.pnlSetLeft.ResumeLayout(false);
            this.pnlSetLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.pnlQcSumCode.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCmdTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQrCode;
        private System.Windows.Forms.Button btnSeach;
        private System.Windows.Forms.Panel pnlQcSumCode;
        private QrCtrl.UC_QrCode qrCode;
        private System.Windows.Forms.Panel pnlSetLeft;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudRight;
        private System.Windows.Forms.NumericUpDown nudLeft;
        private System.Windows.Forms.NumericUpDown nudDown;
        private System.Windows.Forms.NumericUpDown nudUp;
        private System.Drawing.Printing.PrintDocument imgPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtShow;
    }
}