namespace Yu3zx.Moonlit
{
    partial class frmAlarm
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
            this.txtAlarmSource = new System.Windows.Forms.TextBox();
            this.txtAlarmName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAlarmDesc = new System.Windows.Forms.TextBox();
            this.txtAddTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtAlarmSource
            // 
            this.txtAlarmSource.Location = new System.Drawing.Point(124, 25);
            this.txtAlarmSource.Margin = new System.Windows.Forms.Padding(4);
            this.txtAlarmSource.Name = "txtAlarmSource";
            this.txtAlarmSource.ReadOnly = true;
            this.txtAlarmSource.Size = new System.Drawing.Size(265, 25);
            this.txtAlarmSource.TabIndex = 8;
            // 
            // txtAlarmName
            // 
            this.txtAlarmName.Location = new System.Drawing.Point(124, 80);
            this.txtAlarmName.Margin = new System.Windows.Forms.Padding(4);
            this.txtAlarmName.Name = "txtAlarmName";
            this.txtAlarmName.ReadOnly = true;
            this.txtAlarmName.Size = new System.Drawing.Size(265, 25);
            this.txtAlarmName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(48, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "报警名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(62, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "报警源 ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(48, 213);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 11;
            this.label9.Text = "报警描述";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAlarmDesc
            // 
            this.txtAlarmDesc.Location = new System.Drawing.Point(123, 184);
            this.txtAlarmDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtAlarmDesc.Multiline = true;
            this.txtAlarmDesc.Name = "txtAlarmDesc";
            this.txtAlarmDesc.ReadOnly = true;
            this.txtAlarmDesc.Size = new System.Drawing.Size(265, 82);
            this.txtAlarmDesc.TabIndex = 10;
            // 
            // txtAddTime
            // 
            this.txtAddTime.Location = new System.Drawing.Point(124, 133);
            this.txtAddTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddTime.Name = "txtAddTime";
            this.txtAddTime.ReadOnly = true;
            this.txtAddTime.Size = new System.Drawing.Size(265, 25);
            this.txtAddTime.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(47, 138);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "产生时间";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 313);
            this.Controls.Add(this.txtAddTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAlarmSource);
            this.Controls.Add(this.txtAlarmName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAlarmDesc);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAlarm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "报警消息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAlarmSource;
        private System.Windows.Forms.TextBox txtAlarmName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAlarmDesc;
        private System.Windows.Forms.TextBox txtAddTime;
        private System.Windows.Forms.Label label1;

    }
}