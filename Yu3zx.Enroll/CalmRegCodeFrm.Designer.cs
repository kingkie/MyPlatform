namespace Yu3zx.Enroll
{
    partial class CalmRegCodeFrm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGetCPU = new System.Windows.Forms.Button();
            this.btnGetMac = new System.Windows.Forms.Button();
            this.btnCalm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRegister = new System.Windows.Forms.TextBox();
            this.txtMacCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGetCPU);
            this.panel1.Controls.Add(this.btnGetMac);
            this.panel1.Controls.Add(this.btnCalm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 75);
            this.panel1.TabIndex = 0;
            // 
            // btnGetCPU
            // 
            this.btnGetCPU.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGetCPU.Location = new System.Drawing.Point(17, 15);
            this.btnGetCPU.Name = "btnGetCPU";
            this.btnGetCPU.Size = new System.Drawing.Size(130, 47);
            this.btnGetCPU.TabIndex = 2;
            this.btnGetCPU.Text = "获取CPU码";
            this.btnGetCPU.UseVisualStyleBackColor = true;
            this.btnGetCPU.Click += new System.EventHandler(this.btnGetCPU_Click);
            // 
            // btnGetMac
            // 
            this.btnGetMac.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGetMac.Location = new System.Drawing.Point(170, 15);
            this.btnGetMac.Name = "btnGetMac";
            this.btnGetMac.Size = new System.Drawing.Size(130, 47);
            this.btnGetMac.TabIndex = 1;
            this.btnGetMac.Text = "获取机器码";
            this.btnGetMac.UseVisualStyleBackColor = true;
            this.btnGetMac.Click += new System.EventHandler(this.btnGetMac_Click);
            // 
            // btnCalm
            // 
            this.btnCalm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCalm.Location = new System.Drawing.Point(314, 15);
            this.btnCalm.Name = "btnCalm";
            this.btnCalm.Size = new System.Drawing.Size(130, 47);
            this.btnCalm.TabIndex = 0;
            this.btnCalm.Text = "注册码计算";
            this.btnCalm.UseVisualStyleBackColor = true;
            this.btnCalm.Click += new System.EventHandler(this.btnCalm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRegister);
            this.groupBox1.Controls.Add(this.txtMacCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 177);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtRegister
            // 
            this.txtRegister.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRegister.Location = new System.Drawing.Point(99, 84);
            this.txtRegister.Multiline = true;
            this.txtRegister.Name = "txtRegister";
            this.txtRegister.Size = new System.Drawing.Size(345, 69);
            this.txtRegister.TabIndex = 7;
            // 
            // txtMacCode
            // 
            this.txtMacCode.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMacCode.Location = new System.Drawing.Point(99, 34);
            this.txtMacCode.Name = "txtMacCode";
            this.txtMacCode.Size = new System.Drawing.Size(345, 28);
            this.txtMacCode.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "注册码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "机器码:";
            // 
            // CalmRegCodeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 252);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalmRegCodeFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册码计算工具";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRegister;
        private System.Windows.Forms.TextBox txtMacCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCalm;
        private System.Windows.Forms.Button btnGetMac;
        private System.Windows.Forms.Button btnGetCPU;
    }
}