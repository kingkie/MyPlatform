
namespace Yu3zx.PrintCenter
{
    partial class frmPrinter
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
            this.tabContent = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbl = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboInitprinter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudCopys = new System.Windows.Forms.NumericUpDown();
            this.tabContent.SuspendLayout();
            this.tbl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopys)).BeginInit();
            this.SuspendLayout();
            // 
            // tabContent
            // 
            this.tabContent.Controls.Add(this.tabPage1);
            this.tabContent.Controls.Add(this.tabPage2);
            this.tabContent.Controls.Add(this.tabPage3);
            this.tabContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContent.ItemSize = new System.Drawing.Size(96, 32);
            this.tabContent.Location = new System.Drawing.Point(3, 63);
            this.tabContent.Name = "tabContent";
            this.tabContent.SelectedIndex = 0;
            this.tabContent.Size = new System.Drawing.Size(577, 354);
            this.tabContent.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 36);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(569, 314);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "小 标 签";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 36);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(549, 350);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "布卷标签";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 36);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(549, 350);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "箱外标签";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbl
            // 
            this.tbl.ColumnCount = 1;
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl.Controls.Add(this.tabContent, 0, 1);
            this.tbl.Controls.Add(this.groupBox1, 0, 0);
            this.tbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl.Location = new System.Drawing.Point(0, 0);
            this.tbl.Name = "tbl";
            this.tbl.RowCount = 2;
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl.Size = new System.Drawing.Size(583, 420);
            this.tbl.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudCopys);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboInitprinter);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(577, 54);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "选择打印机:";
            // 
            // cboInitprinter
            // 
            this.cboInitprinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInitprinter.FormattingEnabled = true;
            this.cboInitprinter.Location = new System.Drawing.Point(121, 20);
            this.cboInitprinter.Name = "cboInitprinter";
            this.cboInitprinter.Size = new System.Drawing.Size(205, 20);
            this.cboInitprinter.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(333, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "设置打印份数:";
            // 
            // nudCopys
            // 
            this.nudCopys.Location = new System.Drawing.Point(446, 20);
            this.nudCopys.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCopys.Name = "nudCopys";
            this.nudCopys.Size = new System.Drawing.Size(58, 21);
            this.nudCopys.TabIndex = 6;
            this.nudCopys.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // frmPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 420);
            this.Controls.Add(this.tbl);
            this.Name = "frmPrinter";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印中心";
            this.Load += new System.EventHandler(this.frmPrinter_Load);
            this.tabContent.ResumeLayout(false);
            this.tbl.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopys)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabContent;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboInitprinter;
        private System.Windows.Forms.NumericUpDown nudCopys;
        private System.Windows.Forms.Label label2;
    }
}

