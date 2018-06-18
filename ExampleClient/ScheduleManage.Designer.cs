namespace ExampleClient
{
    partial class ScheduleManage
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
			this.button_update = new System.Windows.Forms.Button();
			this.dgv = new System.Windows.Forms.DataGridView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.add_date = new System.Windows.Forms.DateTimePicker();
			this.add_rasp = new System.Windows.Forms.ComboBox();
			this.add_worker = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button_add = new System.Windows.Forms.Button();
			this.button_edit = new System.Windows.Forms.Button();
			this.button_delete = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_update
			// 
			this.button_update.Location = new System.Drawing.Point(348, 81);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(220, 33);
			this.button_update.TabIndex = 23;
			this.button_update.Text = "Показать/обновить расписание";
			this.button_update.UseVisualStyleBackColor = true;
			this.button_update.Click += new System.EventHandler(this.button1_Click);
			// 
			// dgv
			// 
			this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv.Location = new System.Drawing.Point(12, 120);
			this.dgv.Name = "dgv";
			this.dgv.Size = new System.Drawing.Size(757, 327);
			this.dgv.TabIndex = 22;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.add_date);
			this.groupBox1.Controls.Add(this.add_rasp);
			this.groupBox1.Controls.Add(this.add_worker);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.button_add);
			this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 102);
			this.groupBox1.TabIndex = 24;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Информация о смене";
			// 
			// add_date
			// 
			this.add_date.Location = new System.Drawing.Point(50, 46);
			this.add_date.Name = "add_date";
			this.add_date.Size = new System.Drawing.Size(132, 20);
			this.add_date.TabIndex = 26;
			// 
			// add_rasp
			// 
			this.add_rasp.FormattingEnabled = true;
			this.add_rasp.Location = new System.Drawing.Point(50, 72);
			this.add_rasp.Name = "add_rasp";
			this.add_rasp.Size = new System.Drawing.Size(132, 21);
			this.add_rasp.TabIndex = 15;
			// 
			// add_worker
			// 
			this.add_worker.FormattingEnabled = true;
			this.add_worker.Location = new System.Drawing.Point(50, 19);
			this.add_worker.Name = "add_worker";
			this.add_worker.Size = new System.Drawing.Size(132, 21);
			this.add_worker.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.SystemColors.Control;
			this.label5.Location = new System.Drawing.Point(8, 49);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Дата ";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.SystemColors.Control;
			this.label6.Location = new System.Drawing.Point(6, 75);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "Смена";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.SystemColors.Control;
			this.label2.Location = new System.Drawing.Point(14, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "ФИО";
			// 
			// button_add
			// 
			this.button_add.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button_add.Location = new System.Drawing.Point(188, 19);
			this.button_add.Name = "button_add";
			this.button_add.Size = new System.Drawing.Size(113, 72);
			this.button_add.TabIndex = 2;
			this.button_add.Text = "Добавить смену";
			this.button_add.UseVisualStyleBackColor = true;
			this.button_add.Click += new System.EventHandler(this.button2_Click);
			// 
			// button_edit
			// 
			this.button_edit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.button_edit.Location = new System.Drawing.Point(338, 12);
			this.button_edit.Name = "button_edit";
			this.button_edit.Size = new System.Drawing.Size(74, 25);
			this.button_edit.TabIndex = 17;
			this.button_edit.Text = "Изменить";
			this.button_edit.UseVisualStyleBackColor = true;
			this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
			// 
			// button_delete
			// 
			this.button_delete.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button_delete.Location = new System.Drawing.Point(574, 81);
			this.button_delete.Name = "button_delete";
			this.button_delete.Size = new System.Drawing.Size(193, 33);
			this.button_delete.TabIndex = 3;
			this.button_delete.Text = "Удалить";
			this.button_delete.UseVisualStyleBackColor = true;
			this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
			// 
			// ScheduleManage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.MenuHighlight;
			this.ClientSize = new System.Drawing.Size(779, 457);
			this.Controls.Add(this.button_update);
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button_edit);
			this.Controls.Add(this.button_delete);
			this.Name = "ScheduleManage";
			this.Text = "Form2";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScheduleManage_FormClosed);
			this.Load += new System.EventHandler(this.Form2_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_delete;
		private System.Windows.Forms.ComboBox add_worker;
		private System.Windows.Forms.ComboBox add_rasp;
		private System.Windows.Forms.DateTimePicker add_date;
	}
}