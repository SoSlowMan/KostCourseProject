namespace ExampleClient
{
    partial class OrdersManager
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
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
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
			this.button_update.Location = new System.Drawing.Point(338, 12);
			this.button_update.Name = "button_update";
			this.button_update.Size = new System.Drawing.Size(220, 33);
			this.button_update.TabIndex = 35;
			this.button_update.Text = "Показать/обновить список заказов";
			this.button_update.UseVisualStyleBackColor = true;
			this.button_update.Click += new System.EventHandler(this.button_update_Click);
			// 
			// dgv
			// 
			this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv.Location = new System.Drawing.Point(12, 120);
			this.dgv.Name = "dgv";
			this.dgv.Size = new System.Drawing.Size(546, 249);
			this.dgv.TabIndex = 34;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.add_worker);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.button_add);
			this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 102);
			this.groupBox1.TabIndex = 36;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Информация о смене";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(63, 45);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(132, 20);
			this.textBox2.TabIndex = 17;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(63, 20);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(132, 20);
			this.textBox1.TabIndex = 16;
			// 
			// add_worker
			// 
			this.add_worker.FormattingEnabled = true;
			this.add_worker.Location = new System.Drawing.Point(63, 70);
			this.add_worker.Name = "add_worker";
			this.add_worker.Size = new System.Drawing.Size(132, 21);
			this.add_worker.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.SystemColors.Control;
			this.label5.Location = new System.Drawing.Point(21, 47);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Адрес";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.SystemColors.Control;
			this.label6.Location = new System.Drawing.Point(13, 73);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(49, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "Рабочий";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.SystemColors.Control;
			this.label2.Location = new System.Drawing.Point(6, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Описание";
			// 
			// button_add
			// 
			this.button_add.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button_add.Location = new System.Drawing.Point(201, 20);
			this.button_add.Name = "button_add";
			this.button_add.Size = new System.Drawing.Size(113, 71);
			this.button_add.TabIndex = 2;
			this.button_add.Text = "Добавить заказ";
			this.button_add.UseVisualStyleBackColor = true;
			this.button_add.Click += new System.EventHandler(this.button_add_Click);
			// 
			// button_edit
			// 
			this.button_edit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.button_edit.Location = new System.Drawing.Point(338, 49);
			this.button_edit.Name = "button_edit";
			this.button_edit.Size = new System.Drawing.Size(220, 32);
			this.button_edit.TabIndex = 33;
			this.button_edit.Text = "Изменить";
			this.button_edit.UseVisualStyleBackColor = true;
			this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
			// 
			// button_delete
			// 
			this.button_delete.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button_delete.Location = new System.Drawing.Point(338, 82);
			this.button_delete.Name = "button_delete";
			this.button_delete.Size = new System.Drawing.Size(220, 32);
			this.button_delete.TabIndex = 32;
			this.button_delete.Text = "Удалить";
			this.button_delete.UseVisualStyleBackColor = true;
			this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
			// 
			// NewOrders
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Highlight;
			this.ClientSize = new System.Drawing.Size(568, 379);
			this.Controls.Add(this.button_update);
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button_edit);
			this.Controls.Add(this.button_delete);
			this.Name = "NewOrders";
			this.Text = "ExcM";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewOrders_FormClosed);
			this.Load += new System.EventHandler(this.OnLoad);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox add_worker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}