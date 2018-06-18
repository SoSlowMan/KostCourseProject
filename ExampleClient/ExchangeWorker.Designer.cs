namespace ExampleClient
{
    partial class ExchangeWorker
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.variantsCombo = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.mySmenasCombo = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonSubmit = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.variantsCombo);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(448, 52);
			this.groupBox1.TabIndex = 30;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Смена которую меняем";
			// 
			// source_smena
			// 
			this.variantsCombo.FormattingEnabled = true;
			this.variantsCombo.Location = new System.Drawing.Point(50, 19);
			this.variantsCombo.Name = "source_smena";
			this.variantsCombo.Size = new System.Drawing.Size(392, 21);
			this.variantsCombo.TabIndex = 15;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.SystemColors.Control;
			this.label6.Location = new System.Drawing.Point(6, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "Смена";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.mySmenasCombo);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.ForeColor = System.Drawing.SystemColors.Control;
			this.groupBox2.Location = new System.Drawing.Point(12, 70);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(448, 52);
			this.groupBox2.TabIndex = 31;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Смена на которую меняем";
			// 
			// target_smena
			// 
			this.mySmenasCombo.FormattingEnabled = true;
			this.mySmenasCombo.Location = new System.Drawing.Point(52, 19);
			this.mySmenasCombo.Name = "target_smena";
			this.mySmenasCombo.Size = new System.Drawing.Size(390, 21);
			this.mySmenasCombo.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.SystemColors.Control;
			this.label3.Location = new System.Drawing.Point(6, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Смена";
			// 
			// buttonSubmit
			// 
			this.buttonSubmit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonSubmit.Location = new System.Drawing.Point(341, 128);
			this.buttonSubmit.Name = "buttonSubmit";
			this.buttonSubmit.Size = new System.Drawing.Size(113, 38);
			this.buttonSubmit.TabIndex = 2;
			this.buttonSubmit.Text = "Сделать запрос";
			this.buttonSubmit.UseVisualStyleBackColor = true;
			this.buttonSubmit.Click += new System.EventHandler(this.OnFormSubmit);
			// 
			// ExchangeWorker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Highlight;
			this.ClientSize = new System.Drawing.Size(471, 172);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonSubmit);
			this.Name = "ExchangeWorker";
			this.Text = "Обмен сменами";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Load += new System.EventHandler(this.OnLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox variantsCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox mySmenasCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSubmit;
    }
}