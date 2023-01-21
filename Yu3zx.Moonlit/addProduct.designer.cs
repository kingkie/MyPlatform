namespace Yu3zx.Moonlit
{
    partial class addProduct
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
            this.label1 = new System.Windows.Forms.Label();
            this.fdProductName = new System.Windows.Forms.TextBox();
            this.fdProductNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fdProductCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fdInfo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.productSave = new System.Windows.Forms.Button();
            this.productCannel = new System.Windows.Forms.Button();
            this.product_part_list = new System.Windows.Forms.DataGridView();
            this.partSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partNo3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fdProductPrintName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fdProductCode2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.fdProductCode3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.product_part_list)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "产品名称";
            // 
            // fdProductName
            // 
            this.fdProductName.Location = new System.Drawing.Point(125, 35);
            this.fdProductName.Name = "fdProductName";
            this.fdProductName.Size = new System.Drawing.Size(388, 28);
            this.fdProductName.TabIndex = 1;
            // 
            // fdProductNo
            // 
            this.fdProductNo.Location = new System.Drawing.Point(635, 35);
            this.fdProductNo.Name = "fdProductNo";
            this.fdProductNo.Size = new System.Drawing.Size(544, 28);
            this.fdProductNo.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(533, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "产品编号";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // fdProductCode
            // 
            this.fdProductCode.Location = new System.Drawing.Point(125, 123);
            this.fdProductCode.Name = "fdProductCode";
            this.fdProductCode.Size = new System.Drawing.Size(388, 28);
            this.fdProductCode.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "产品识别号1";
            // 
            // fdInfo
            // 
            this.fdInfo.Location = new System.Drawing.Point(203, 515);
            this.fdInfo.Multiline = true;
            this.fdInfo.Name = "fdInfo";
            this.fdInfo.Size = new System.Drawing.Size(976, 92);
            this.fdInfo.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 551);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "描述";
            // 
            // productSave
            // 
            this.productSave.Location = new System.Drawing.Point(569, 616);
            this.productSave.Name = "productSave";
            this.productSave.Size = new System.Drawing.Size(75, 40);
            this.productSave.TabIndex = 8;
            this.productSave.Text = "保存";
            this.productSave.UseVisualStyleBackColor = true;
            this.productSave.Click += new System.EventHandler(this.productSave_Click);
            // 
            // productCannel
            // 
            this.productCannel.Location = new System.Drawing.Point(660, 616);
            this.productCannel.Name = "productCannel";
            this.productCannel.Size = new System.Drawing.Size(75, 40);
            this.productCannel.TabIndex = 9;
            this.productCannel.Text = "取消";
            this.productCannel.UseVisualStyleBackColor = true;
            this.productCannel.Click += new System.EventHandler(this.productCannel_Click);
            // 
            // product_part_list
            // 
            this.product_part_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.product_part_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.partSerial,
            this.partName,
            this.partStage,
            this.partData,
            this.partNo2,
            this.partNo3});
            this.product_part_list.Location = new System.Drawing.Point(26, 224);
            this.product_part_list.Name = "product_part_list";
            this.product_part_list.RowHeadersWidth = 62;
            this.product_part_list.RowTemplate.Height = 30;
            this.product_part_list.Size = new System.Drawing.Size(1153, 285);
            this.product_part_list.TabIndex = 7;
            this.product_part_list.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.product_part_list_CellContentClick);
            // 
            // partSerial
            // 
            this.partSerial.HeaderText = "序号";
            this.partSerial.MinimumWidth = 8;
            this.partSerial.Name = "partSerial";
            this.partSerial.Width = 50;
            // 
            // partName
            // 
            this.partName.HeaderText = "部件名称";
            this.partName.MinimumWidth = 8;
            this.partName.Name = "partName";
            this.partName.Width = 150;
            // 
            // partStage
            // 
            this.partStage.HeaderText = "工位";
            this.partStage.MinimumWidth = 8;
            this.partStage.Name = "partStage";
            this.partStage.Width = 80;
            // 
            // partData
            // 
            this.partData.HeaderText = "识别码1";
            this.partData.MinimumWidth = 8;
            this.partData.Name = "partData";
            this.partData.Width = 150;
            // 
            // partNo2
            // 
            this.partNo2.HeaderText = "识别码2";
            this.partNo2.MinimumWidth = 8;
            this.partNo2.Name = "partNo2";
            this.partNo2.Width = 150;
            // 
            // partNo3
            // 
            this.partNo3.HeaderText = "识别码3";
            this.partNo3.MinimumWidth = 8;
            this.partNo3.Name = "partNo3";
            this.partNo3.Width = 150;
            // 
            // fdProductPrintName
            // 
            this.fdProductPrintName.Location = new System.Drawing.Point(127, 80);
            this.fdProductPrintName.Name = "fdProductPrintName";
            this.fdProductPrintName.Size = new System.Drawing.Size(386, 28);
            this.fdProductPrintName.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "产品打印名";
            // 
            // fdProductCode2
            // 
            this.fdProductCode2.Location = new System.Drawing.Point(635, 123);
            this.fdProductCode2.Name = "fdProductCode2";
            this.fdProductCode2.Size = new System.Drawing.Size(544, 28);
            this.fdProductCode2.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(521, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "产品识别号2";
            // 
            // fdProductCode3
            // 
            this.fdProductCode3.Location = new System.Drawing.Point(125, 174);
            this.fdProductCode3.Name = "fdProductCode3";
            this.fdProductCode3.Size = new System.Drawing.Size(388, 28);
            this.fdProductCode3.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 18);
            this.label7.TabIndex = 15;
            this.label7.Text = "产品识别号3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1052, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 44);
            this.button1.TabIndex = 16;
            this.button1.Text = "删除部件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 695);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fdProductCode3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.fdProductCode2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.fdProductPrintName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.product_part_list);
            this.Controls.Add(this.productCannel);
            this.Controls.Add(this.productSave);
            this.Controls.Add(this.fdInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.fdProductCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fdProductNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fdProductName);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(200, 0);
            this.Name = "addProduct";
            this.Text = "产品管理";
            ((System.ComponentModel.ISupportInitialize)(this.product_part_list)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fdProductName;
        private System.Windows.Forms.TextBox fdProductNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fdProductCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fdInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button productSave;
        private System.Windows.Forms.Button productCannel;
        private System.Windows.Forms.DataGridView product_part_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn partSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn partName;
        private System.Windows.Forms.DataGridViewTextBoxColumn partStage;
        private System.Windows.Forms.DataGridViewTextBoxColumn partData;
        private System.Windows.Forms.DataGridViewTextBoxColumn partNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn partNo3;
        private System.Windows.Forms.TextBox fdProductPrintName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fdProductCode2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox fdProductCode3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
    }
}