namespace Yu3zx.Moonlit
{
    partial class frmDealAlarm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvShow = new System.Windows.Forms.DataGridView();
            this.AlarmClear = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DevId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmAddTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShow
            // 
            this.dgvShow.AllowUserToAddRows = false;
            this.dgvShow.AllowUserToDeleteRows = false;
            this.dgvShow.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShow.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvShow.ColumnHeadersHeight = 36;
            this.dgvShow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AlarmClear,
            this.DevId,
            this.AlarmName,
            this.AlarmSource,
            this.AlarmMsg,
            this.AlarmAddTime});
            this.dgvShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShow.Location = new System.Drawing.Point(0, 0);
            this.dgvShow.Margin = new System.Windows.Forms.Padding(1);
            this.dgvShow.Name = "dgvShow";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShow.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dgvShow.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvShow.RowTemplate.Height = 23;
            this.dgvShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvShow.Size = new System.Drawing.Size(840, 488);
            this.dgvShow.TabIndex = 2;
            this.dgvShow.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShow_CellContentClick);
            // 
            // AlarmClear
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.AlarmClear.DefaultCellStyle = dataGridViewCellStyle2;
            this.AlarmClear.HeaderText = "消除报警";
            this.AlarmClear.Name = "AlarmClear";
            // 
            // DevId
            // 
            this.DevId.HeaderText = "设备编号";
            this.DevId.Name = "DevId";
            // 
            // AlarmName
            // 
            this.AlarmName.HeaderText = "报警名称";
            this.AlarmName.Name = "AlarmName";
            this.AlarmName.ReadOnly = true;
            this.AlarmName.Width = 140;
            // 
            // AlarmSource
            // 
            this.AlarmSource.HeaderText = "报 警 源";
            this.AlarmSource.Name = "AlarmSource";
            this.AlarmSource.ReadOnly = true;
            this.AlarmSource.Width = 120;
            // 
            // AlarmMsg
            // 
            this.AlarmMsg.HeaderText = "报警信息";
            this.AlarmMsg.Name = "AlarmMsg";
            this.AlarmMsg.ReadOnly = true;
            this.AlarmMsg.Width = 250;
            // 
            // AlarmAddTime
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.AlarmAddTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.AlarmAddTime.HeaderText = "报警产生时间";
            this.AlarmAddTime.Name = "AlarmAddTime";
            this.AlarmAddTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AlarmAddTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AlarmAddTime.Width = 120;
            // 
            // frmDealAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 488);
            this.Controls.Add(this.dgvShow);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDealAlarm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDealAlarm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvShow;
        private System.Windows.Forms.DataGridViewButtonColumn AlarmClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmAddTime;
    }
}