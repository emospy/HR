namespace HR
{
	partial class MilitaryRangForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MilitaryRangForm));
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.comboBoxMilitaryRang = new System.Windows.Forms.ComboBox();
			this.dateTimePickerRangOrderDate = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerRangValidFrom = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxRangOrderNumber = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBoxIsActive = new System.Windows.Forms.CheckBox();
			this.checkBoxRangUpdate = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// buttonSave
			// 
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSave.Location = new System.Drawing.Point(295, 105);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(130, 23);
			this.buttonSave.TabIndex = 3;
			this.buttonSave.Text = "Запис";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(448, 105);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Изход";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// comboBoxMilitaryRang
			// 
			this.comboBoxMilitaryRang.FormattingEnabled = true;
			this.comboBoxMilitaryRang.Location = new System.Drawing.Point(12, 26);
			this.comboBoxMilitaryRang.Name = "comboBoxMilitaryRang";
			this.comboBoxMilitaryRang.Size = new System.Drawing.Size(200, 21);
			this.comboBoxMilitaryRang.TabIndex = 5;
			// 
			// dateTimePickerRangOrderDate
			// 
			this.dateTimePickerRangOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerRangOrderDate.Location = new System.Drawing.Point(444, 27);
			this.dateTimePickerRangOrderDate.Name = "dateTimePickerRangOrderDate";
			this.dateTimePickerRangOrderDate.Size = new System.Drawing.Size(200, 20);
			this.dateTimePickerRangOrderDate.TabIndex = 6;
			this.dateTimePickerRangOrderDate.ValueChanged += new System.EventHandler(this.dateTimePickerRangOrderDate_ValueChanged);
			// 
			// dateTimePickerRangValidFrom
			// 
			this.dateTimePickerRangValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerRangValidFrom.Location = new System.Drawing.Point(660, 27);
			this.dateTimePickerRangValidFrom.Name = "dateTimePickerRangValidFrom";
			this.dateTimePickerRangValidFrom.Size = new System.Drawing.Size(200, 20);
			this.dateTimePickerRangValidFrom.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Военно звание :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(228, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Номер на заповед";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(444, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(188, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Дата на подписване на заповедта :";
			// 
			// textBoxRangOrderNumber
			// 
			this.textBoxRangOrderNumber.Location = new System.Drawing.Point(228, 27);
			this.textBoxRangOrderNumber.Name = "textBoxRangOrderNumber";
			this.textBoxRangOrderNumber.Size = new System.Drawing.Size(200, 20);
			this.textBoxRangOrderNumber.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(660, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(61, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "В сила от :";
			// 
			// checkBoxIsActive
			// 
			this.checkBoxIsActive.AutoSize = true;
			this.checkBoxIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBoxIsActive.Location = new System.Drawing.Point(444, 71);
			this.checkBoxIsActive.Name = "checkBoxIsActive";
			this.checkBoxIsActive.Size = new System.Drawing.Size(174, 17);
			this.checkBoxIsActive.TabIndex = 14;
			this.checkBoxIsActive.Text = "АКТИВНО НАЗНАЧЕНИЕ";
			this.checkBoxIsActive.UseVisualStyleBackColor = true;
			// 
			// checkBoxRangUpdate
			// 
			this.checkBoxRangUpdate.AutoSize = true;
			this.checkBoxRangUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBoxRangUpdate.Location = new System.Drawing.Point(228, 71);
			this.checkBoxRangUpdate.Name = "checkBoxRangUpdate";
			this.checkBoxRangUpdate.Size = new System.Drawing.Size(113, 17);
			this.checkBoxRangUpdate.TabIndex = 15;
			this.checkBoxRangUpdate.Text = "ВОЕНЕН РАНГ";
			this.checkBoxRangUpdate.UseVisualStyleBackColor = true;
			// 
			// MilitaryRangForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(873, 138);
			this.Controls.Add(this.checkBoxRangUpdate);
			this.Controls.Add(this.checkBoxIsActive);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxRangOrderNumber);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateTimePickerRangValidFrom);
			this.Controls.Add(this.dateTimePickerRangOrderDate);
			this.Controls.Add(this.comboBoxMilitaryRang);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonCancel);
			this.Name = "MilitaryRangForm";
			this.Text = "Военно звание";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		public System.Windows.Forms.ComboBox comboBoxMilitaryRang;
		public System.Windows.Forms.DateTimePicker dateTimePickerRangOrderDate;
		public System.Windows.Forms.DateTimePicker dateTimePickerRangValidFrom;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox textBoxRangOrderNumber;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.CheckBox checkBoxIsActive;
		public System.Windows.Forms.CheckBox checkBoxRangUpdate;
	}
}