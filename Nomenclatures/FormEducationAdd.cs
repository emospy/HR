using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HR
{
	/// <summary>
	/// Summary description for FormEducationAdd.
	/// </summary>
	public class FormEducationAdd : System.Windows.Forms.Form
	{
		/// <summary>
		/// Summary description for FormEducationAdd.
		/// </summary>
		public BugBox.NumBox numBoxEducationPrice;
		private System.Windows.Forms.Label label74;
		/// <summary>
		/// Summary description for FormEducationAdd.
		/// </summary>
		public BugBox.NumBox numBoxEducationHours;
		/// <summary>
		/// Summary description for FormEducationAdd.
		/// </summary>
		public BugBox.NumBox numBoxEducationDays;
		private System.Windows.Forms.Label label73;
		private System.Windows.Forms.Label label68;
		private System.Windows.Forms.Label label67;
		/// <summary>
		/// Summary description for FormEducationAdd.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxEducationCode;
		private System.Windows.Forms.Label label66;
		private System.Windows.Forms.Label label63;
		private System.Windows.Forms.Label label78;
		private System.Windows.Forms.Label label79;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		/// <summary>
		/// Summary description for FormEducationAdd.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxEducationArea;
		/// <summary>
		/// Summary description for FormEducationAdd.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxEducationTheme;
		/// <summary>
		/// Summary description for FormEducationAdd.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxEducationOrganizer;
		/// <summary>
		/// Summary description for FormEducationAdd.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxEducationPlace;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// Summary description for FormEducationAdd.
		/// </summary>
		public FormEducationAdd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEducationAdd));
			this.textBoxEducationOrganizer = new System.Windows.Forms.TextBox();
			this.textBoxEducationPlace = new System.Windows.Forms.TextBox();
			this.numBoxEducationPrice = new BugBox.NumBox();
			this.label74 = new System.Windows.Forms.Label();
			this.numBoxEducationHours = new BugBox.NumBox();
			this.numBoxEducationDays = new BugBox.NumBox();
			this.label73 = new System.Windows.Forms.Label();
			this.label68 = new System.Windows.Forms.Label();
			this.label67 = new System.Windows.Forms.Label();
			this.textBoxEducationCode = new System.Windows.Forms.TextBox();
			this.label66 = new System.Windows.Forms.Label();
			this.label63 = new System.Windows.Forms.Label();
			this.label78 = new System.Windows.Forms.Label();
			this.label79 = new System.Windows.Forms.Label();
			this.textBoxEducationArea = new System.Windows.Forms.TextBox();
			this.textBoxEducationTheme = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxEducationOrganizer
			// 
			this.textBoxEducationOrganizer.Location = new System.Drawing.Point(344, 152);
			this.textBoxEducationOrganizer.Name = "textBoxEducationOrganizer";
			this.textBoxEducationOrganizer.Size = new System.Drawing.Size(344, 20);
			this.textBoxEducationOrganizer.TabIndex = 7;
			// 
			// textBoxEducationPlace
			// 
			this.textBoxEducationPlace.Location = new System.Drawing.Point(8, 152);
			this.textBoxEducationPlace.Name = "textBoxEducationPlace";
			this.textBoxEducationPlace.Size = new System.Drawing.Size(328, 20);
			this.textBoxEducationPlace.TabIndex = 6;
			// 
			// numBoxEducationPrice
			// 
			this.numBoxEducationPrice.Location = new System.Drawing.Point(520, 112);
			this.numBoxEducationPrice.Name = "numBoxEducationPrice";
			this.numBoxEducationPrice.Size = new System.Drawing.Size(168, 20);
			this.numBoxEducationPrice.TabIndex = 5;
			// 
			// label74
			// 
			this.label74.Location = new System.Drawing.Point(536, 96);
			this.label74.Name = "label74";
			this.label74.Size = new System.Drawing.Size(80, 16);
			this.label74.TabIndex = 150;
			this.label74.Text = "Цена:";
			// 
			// numBoxEducationHours
			// 
			this.numBoxEducationHours.Location = new System.Drawing.Point(344, 112);
			this.numBoxEducationHours.Name = "numBoxEducationHours";
			this.numBoxEducationHours.Size = new System.Drawing.Size(168, 20);
			this.numBoxEducationHours.TabIndex = 4;
			// 
			// numBoxEducationDays
			// 
			this.numBoxEducationDays.Location = new System.Drawing.Point(176, 112);
			this.numBoxEducationDays.Name = "numBoxEducationDays";
			this.numBoxEducationDays.Size = new System.Drawing.Size(160, 20);
			this.numBoxEducationDays.TabIndex = 3;
			// 
			// label73
			// 
			this.label73.Location = new System.Drawing.Point(344, 96);
			this.label73.Name = "label73";
			this.label73.Size = new System.Drawing.Size(80, 16);
			this.label73.TabIndex = 147;
			this.label73.Text = "Брой часове:";
			// 
			// label68
			// 
			this.label68.Location = new System.Drawing.Point(176, 96);
			this.label68.Name = "label68";
			this.label68.Size = new System.Drawing.Size(68, 16);
			this.label68.TabIndex = 146;
			this.label68.Text = "Брой дни:";
			// 
			// label67
			// 
			this.label67.Location = new System.Drawing.Point(8, 96);
			this.label67.Name = "label67";
			this.label67.Size = new System.Drawing.Size(68, 16);
			this.label67.TabIndex = 145;
			this.label67.Text = "Код:";
			// 
			// textBoxEducationCode
			// 
			this.textBoxEducationCode.Location = new System.Drawing.Point(8, 112);
			this.textBoxEducationCode.Name = "textBoxEducationCode";
			this.textBoxEducationCode.Size = new System.Drawing.Size(160, 20);
			this.textBoxEducationCode.TabIndex = 2;
			// 
			// label66
			// 
			this.label66.Location = new System.Drawing.Point(8, 56);
			this.label66.Name = "label66";
			this.label66.Size = new System.Drawing.Size(128, 16);
			this.label66.TabIndex = 143;
			this.label66.Text = "Тема:";
			// 
			// label63
			// 
			this.label63.Location = new System.Drawing.Point(8, 16);
			this.label63.Name = "label63";
			this.label63.Size = new System.Drawing.Size(128, 16);
			this.label63.TabIndex = 141;
			this.label63.Text = "Област на обучението:";
			// 
			// label78
			// 
			this.label78.Location = new System.Drawing.Point(8, 136);
			this.label78.Name = "label78";
			this.label78.Size = new System.Drawing.Size(168, 16);
			this.label78.TabIndex = 161;
			this.label78.Text = "Място на провеждане:";
			// 
			// label79
			// 
			this.label79.Location = new System.Drawing.Point(344, 136);
			this.label79.Name = "label79";
			this.label79.Size = new System.Drawing.Size(168, 16);
			this.label79.TabIndex = 162;
			this.label79.Text = "Обучаваща организация:";
			// 
			// textBoxEducationArea
			// 
			this.textBoxEducationArea.Location = new System.Drawing.Point(8, 32);
			this.textBoxEducationArea.Name = "textBoxEducationArea";
			this.textBoxEducationArea.Size = new System.Drawing.Size(680, 20);
			this.textBoxEducationArea.TabIndex = 0;
			// 
			// textBoxEducationTheme
			// 
			this.textBoxEducationTheme.Location = new System.Drawing.Point(8, 72);
			this.textBoxEducationTheme.Name = "textBoxEducationTheme";
			this.textBoxEducationTheme.Size = new System.Drawing.Size(680, 20);
			this.textBoxEducationTheme.TabIndex = 1;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(196, 192);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonCancel.TabIndex = 9;
			this.buttonCancel.Text = "Отказ";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Image = ((System.Drawing.Image)(resources.GetObject("buttonOK.Image")));
			this.buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonOK.Location = new System.Drawing.Point(366, 192);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(130, 23);
			this.buttonOK.TabIndex = 8;
			this.buttonOK.Text = "Запис";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// FormEducationAdd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(692, 221);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxEducationTheme);
			this.Controls.Add(this.textBoxEducationArea);
			this.Controls.Add(this.textBoxEducationOrganizer);
			this.Controls.Add(this.textBoxEducationPlace);
			this.Controls.Add(this.numBoxEducationPrice);
			this.Controls.Add(this.numBoxEducationHours);
			this.Controls.Add(this.numBoxEducationDays);
			this.Controls.Add(this.textBoxEducationCode);
			this.Controls.Add(this.label74);
			this.Controls.Add(this.label73);
			this.Controls.Add(this.label68);
			this.Controls.Add(this.label67);
			this.Controls.Add(this.label66);
			this.Controls.Add(this.label63);
			this.Controls.Add(this.label78);
			this.Controls.Add(this.label79);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEducationAdd";
			this.Text = "Добавяне на обучение в каталога";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
