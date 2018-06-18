namespace ExampleClient
{
    partial class ScheduleWorker
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
			this.button1 = new System.Windows.Forms.Button();
			this.dgv = new System.Windows.Forms.DataGridView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(437, 322);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(220, 33);
			this.button1.TabIndex = 27;
			this.button1.Text = "Показать/обновить расписание";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dgv
			// 
			this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv.Location = new System.Drawing.Point(12, 12);
			this.dgv.Name = "dgv";
			this.dgv.Size = new System.Drawing.Size(645, 264);
			this.dgv.TabIndex = 26;
			this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button4);
			this.groupBox2.Controls.Add(this.textBox7);
			this.groupBox2.ForeColor = System.Drawing.SystemColors.Control;
			this.groupBox2.Location = new System.Drawing.Point(12, 282);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(293, 73);
			this.groupBox2.TabIndex = 29;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Поиск по Id";
			// 
			// button4
			// 
			this.button4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button4.Location = new System.Drawing.Point(149, 29);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(137, 27);
			this.button4.TabIndex = 17;
			this.button4.Text = "Найти";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(6, 33);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(137, 20);
			this.textBox7.TabIndex = 18;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(437, 282);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(220, 34);
			this.button2.TabIndex = 30;
			this.button2.Text = "Показать мое расписание";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// ScheduleWorker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Highlight;
			this.ClientSize = new System.Drawing.Size(669, 366);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.groupBox2);
			this.Name = "ScheduleWorker";
			this.Text = "RaspW";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Load += new System.EventHandler(this.OnLoad);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Button button2;
    }
}