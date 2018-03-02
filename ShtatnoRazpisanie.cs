using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DataLayer;

namespace HR
{
	/// <summary>
	/// Summary description for ShtatnoRazpisanie.
	/// </summary>
	public class ShtatnoRazpisanie : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RadioButton radioButtonMain;
		private System.Windows.Forms.RadioButton radioButtonNamed;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.Button buttonExit;
         mainForm main;
		private System.ComponentModel.Container components = null;
		ExcelExpo Ex;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ShtatnoRazpisanie( mainForm main)
		{
            this.main = main;
			Ex = new ExcelExpo();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShtatnoRazpisanie));
			this.radioButtonMain = new System.Windows.Forms.RadioButton();
			this.radioButtonNamed = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// radioButtonMain
			// 
			this.radioButtonMain.Checked = true;
			this.radioButtonMain.Location = new System.Drawing.Point(16, 16);
			this.radioButtonMain.Name = "radioButtonMain";
			this.radioButtonMain.Size = new System.Drawing.Size(184, 24);
			this.radioButtonMain.TabIndex = 0;
			this.radioButtonMain.TabStop = true;
			this.radioButtonMain.Text = "Основно щатно разписание";
			// 
			// radioButtonNamed
			// 
			this.radioButtonNamed.Location = new System.Drawing.Point(16, 48);
			this.radioButtonNamed.Name = "radioButtonNamed";
			this.radioButtonNamed.Size = new System.Drawing.Size(192, 24);
			this.radioButtonNamed.TabIndex = 1;
			this.radioButtonNamed.Text = "Поименно щатно разписание";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonMain);
			this.groupBox1.Controls.Add(this.radioButtonNamed);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(240, 80);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// buttonPrint
			// 
			this.buttonPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrint.Image")));
			this.buttonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPrint.Location = new System.Drawing.Point(256, 16);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(100, 24);
			this.buttonPrint.TabIndex = 1;
			this.buttonPrint.Text = "   Отпечатване";
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
			this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonExit.Location = new System.Drawing.Point(256, 64);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(100, 23);
			this.buttonExit.TabIndex = 2;
			this.buttonExit.Text = "Изход";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// ShtatnoRazpisanie
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 101);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonPrint);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShtatnoRazpisanie";
			this.ShowInTaskbar = false;
			this.Text = "Щатно разписание";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonPrint_Click(object sender, System.EventArgs e)
		{						
			if(this.radioButtonMain.Checked)
			{				
				Ex.ExportOSR(main);			
			}
			else
			{
				Ex.ExportPSR(main);
			}
			GC.Collect();
			/* Трябва да се довърши щатните разписания
			 * За основното щатно разписание има да се селектират всички длъжности 
			 * от таблицата с длъжностите за да получиш таблица която трябва да се разпечата
			 * командния текст ще изглежда така :
			 *  SELECT * FROM FirmPersonal 
			 * Така ще получиш всички длъжности.
			 * За другите две ще е аналогично само че за всяка получена длъзжност 
			 * трябва да видиш кои хора я заемат, наприемер 
			 * SELECT ID FROM PersonAssignment WHERE position = '???'
			 * за позишън може да заместиш направо или от базата да ги прочетеш всички
			 * позиции
			*/
			//MessageBox.Show("Трябва да се довърши Погледни сорса има коментар!");
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
