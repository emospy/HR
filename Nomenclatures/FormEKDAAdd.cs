using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HR
{
	/// <summary>
	/// Summary description for FormEKDAView.
	/// </summary>
	public class FormEKDAAdd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox textBoxEKDALevel;
		internal BugBox.NumBox numBoxMinSalary;
		internal BugBox.NumBox numBoxMaxSalary;
		internal BugBox.NumBox numBoxPorNum;
		internal System.Windows.Forms.TextBox textBoxEKDACode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		internal System.Windows.Forms.ComboBox comboBoxEducation;
		internal System.Windows.Forms.ComboBox comboBoxRang;
		internal System.Windows.Forms.ComboBox comboBoxLaw;
		internal System.Windows.Forms.ComboBox comboBoxExperience;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public FormEKDAAdd(mainForm main)
		{
			InitializeComponent();	
			this.comboBoxRang.DataSource = main.nomenclaatureData.arrRang;
			this.comboBoxExperience.DataSource = main.nomenclaatureData.arrExperience;
			this.comboBoxEducation.DataSource = main.nomenclaatureData.dtEducation;
            this.comboBoxEducation.DisplayMember = "level";
			this.comboBoxLaw.DataSource = main.nomenclaatureData.arrLaw;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEKDAAdd));
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxEKDALevel = new System.Windows.Forms.TextBox();
			this.numBoxMinSalary = new BugBox.NumBox();
			this.numBoxMaxSalary = new BugBox.NumBox();
			this.numBoxPorNum = new BugBox.NumBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxEKDACode = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.comboBoxEducation = new System.Windows.Forms.ComboBox();
			this.comboBoxRang = new System.Windows.Forms.ComboBox();
			this.comboBoxLaw = new System.Windows.Forms.ComboBox();
			this.comboBoxExperience = new System.Windows.Forms.ComboBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование на длъжност :";
			// 
			// textBoxEKDALevel
			// 
			this.textBoxEKDALevel.Location = new System.Drawing.Point(8, 24);
			this.textBoxEKDALevel.Name = "textBoxEKDALevel";
			this.textBoxEKDALevel.Size = new System.Drawing.Size(672, 20);
			this.textBoxEKDALevel.TabIndex = 0;
			// 
			// numBoxMinSalary
			// 
			this.numBoxMinSalary.Location = new System.Drawing.Point(160, 64);
			this.numBoxMinSalary.Name = "numBoxMinSalary";
			this.numBoxMinSalary.Size = new System.Drawing.Size(168, 20);
			this.numBoxMinSalary.TabIndex = 2;
			// 
			// numBoxMaxSalary
			// 
			this.numBoxMaxSalary.Location = new System.Drawing.Point(336, 64);
			this.numBoxMaxSalary.Name = "numBoxMaxSalary";
			this.numBoxMaxSalary.Size = new System.Drawing.Size(168, 20);
			this.numBoxMaxSalary.TabIndex = 3;
			// 
			// numBoxPorNum
			// 
			this.numBoxPorNum.Location = new System.Drawing.Point(512, 64);
			this.numBoxPorNum.Name = "numBoxPorNum";
			this.numBoxPorNum.Size = new System.Drawing.Size(168, 20);
			this.numBoxPorNum.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Длъжностно ниво :";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(160, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(168, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Минимална основна заплата :";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(336, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(176, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "Максимална основна заплата :";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(512, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "Пореден номер :";
			// 
			// buttonOK
			// 
			this.buttonOK.Image = ((System.Drawing.Image)(resources.GetObject("buttonOK.Image")));
			this.buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonOK.Location = new System.Drawing.Point(366, 144);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(130, 23);
			this.buttonOK.TabIndex = 9;
			this.buttonOK.Text = "Запис";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(196, 144);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonCancel.TabIndex = 10;
			this.buttonCancel.Text = "Отказ";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// textBoxEKDACode
			// 
			this.textBoxEKDACode.Location = new System.Drawing.Point(8, 64);
			this.textBoxEKDACode.Name = "textBoxEKDACode";
			this.textBoxEKDACode.Size = new System.Drawing.Size(144, 20);
			this.textBoxEKDACode.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 88);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(144, 16);
			this.label6.TabIndex = 11;
			this.label6.Text = "Минимално образование :";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(160, 88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 16);
			this.label7.TabIndex = 13;
			this.label7.Text = "Минимален ранг :";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(336, 88);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 16;
			this.label8.Text = "Минимален опит :";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(512, 88);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(144, 16);
			this.label9.TabIndex = 17;
			this.label9.Text = "Вид правоотношение :";
			// 
			// comboBoxEducation
			// 
			this.comboBoxEducation.Location = new System.Drawing.Point(8, 104);
			this.comboBoxEducation.Name = "comboBoxEducation";
			this.comboBoxEducation.Size = new System.Drawing.Size(144, 21);
			this.comboBoxEducation.TabIndex = 5;
			// 
			// comboBoxRang
			// 
			this.comboBoxRang.Location = new System.Drawing.Point(160, 104);
			this.comboBoxRang.Name = "comboBoxRang";
			this.comboBoxRang.Size = new System.Drawing.Size(168, 21);
			this.comboBoxRang.TabIndex = 6;
			// 
			// comboBoxLaw
			// 
			this.comboBoxLaw.Location = new System.Drawing.Point(512, 104);
			this.comboBoxLaw.Name = "comboBoxLaw";
			this.comboBoxLaw.Size = new System.Drawing.Size(168, 21);
			this.comboBoxLaw.TabIndex = 8;
			// 
			// comboBoxExperience
			// 
			this.comboBoxExperience.Location = new System.Drawing.Point(336, 104);
			this.comboBoxExperience.Name = "comboBoxExperience";
			this.comboBoxExperience.Size = new System.Drawing.Size(168, 21);
			this.comboBoxExperience.TabIndex = 7;
			// 
			// FormEKDAAdd
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(692, 173);
			this.Controls.Add(this.comboBoxExperience);
			this.Controls.Add(this.comboBoxLaw);
			this.Controls.Add(this.comboBoxRang);
			this.Controls.Add(this.comboBoxEducation);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxEKDACode);
			this.Controls.Add(this.numBoxPorNum);
			this.Controls.Add(this.numBoxMaxSalary);
			this.Controls.Add(this.numBoxMinSalary);
			this.Controls.Add(this.textBoxEKDALevel);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEKDAAdd";
			this.ShowInTaskbar = false;
			this.Text = "Единен класификатор на длъжностите в администрацията";
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
