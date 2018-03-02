namespace HR
{
	partial class FormLanguage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLanguage));
			this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
			this.labelLanguage = new System.Windows.Forms.Label();
			this.labelLanguageLevel = new System.Windows.Forms.Label();
			this.comboBoxLanguageLevel = new System.Windows.Forms.ComboBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBoxLanguage
			// 
			this.comboBoxLanguage.FormattingEnabled = true;
			this.comboBoxLanguage.Location = new System.Drawing.Point(13, 32);
			this.comboBoxLanguage.Name = "comboBoxLanguage";
			this.comboBoxLanguage.Size = new System.Drawing.Size(467, 21);
			this.comboBoxLanguage.TabIndex = 0;
			// 
			// labelLanguage
			// 
			this.labelLanguage.AutoSize = true;
			this.labelLanguage.Location = new System.Drawing.Point(13, 13);
			this.labelLanguage.Name = "labelLanguage";
			this.labelLanguage.Size = new System.Drawing.Size(38, 13);
			this.labelLanguage.TabIndex = 1;
			this.labelLanguage.Text = "Език :";
			// 
			// labelLanguageLevel
			// 
			this.labelLanguageLevel.AutoSize = true;
			this.labelLanguageLevel.Location = new System.Drawing.Point(12, 59);
			this.labelLanguageLevel.Name = "labelLanguageLevel";
			this.labelLanguageLevel.Size = new System.Drawing.Size(105, 13);
			this.labelLanguageLevel.TabIndex = 3;
			this.labelLanguageLevel.Text = "Ниво на владеене :";
			// 
			// comboBoxLanguageLevel
			// 
			this.comboBoxLanguageLevel.FormattingEnabled = true;
			this.comboBoxLanguageLevel.Location = new System.Drawing.Point(12, 78);
			this.comboBoxLanguageLevel.Name = "comboBoxLanguageLevel";
			this.comboBoxLanguageLevel.Size = new System.Drawing.Size(467, 21);
			this.comboBoxLanguageLevel.TabIndex = 2;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(96, 115);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Откажи";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSave.Location = new System.Drawing.Point(266, 115);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(130, 23);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Запис";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// FormLanguage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 150);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.labelLanguageLevel);
			this.Controls.Add(this.comboBoxLanguageLevel);
			this.Controls.Add(this.labelLanguage);
			this.Controls.Add(this.comboBoxLanguage);
			this.Name = "FormLanguage";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelLanguage;
		private System.Windows.Forms.Label labelLanguageLevel;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
		/// <summary>
		/// Language combo
		/// </summary>
		public System.Windows.Forms.ComboBox comboBoxLanguage;
		/// <summary>
		/// Language knowledge combo
		/// </summary>
		public System.Windows.Forms.ComboBox comboBoxLanguageLevel;
	}
}