using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DataLayer;
using System.Collections.Generic;

namespace HR
{
	/// <summary>
	/// Summary description for formFinishYear.
	/// </summary>
	public class formFinishYear : System.Windows.Forms.Form
	{
		DataAction fa;
		int currentYear = 2000;
		mainForm mainCopy;
		//DataTable dtYear;
		private System.Windows.Forms.Button buttonFinish;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formFinishYear(mainForm main)
		{

			this.fa = new DataAction(main.connString);
			InitializeComponent();
			mainCopy = main;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(formFinishYear));
			this.buttonFinish = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonFinish
			// 
			this.buttonFinish.Image = ((System.Drawing.Image)(resources.GetObject("buttonFinish.Image")));
			this.buttonFinish.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFinish.Location = new System.Drawing.Point(112, 136);
			this.buttonFinish.Name = "buttonFinish";
			this.buttonFinish.Size = new System.Drawing.Size(128, 24);
			this.buttonFinish.TabIndex = 0;
			this.buttonFinish.Text = "Нова година";
			this.toolTip1.SetToolTip(this.buttonFinish, "Освен че ще се стартира нова година то ще се приключи старата");
			this.buttonFinish.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.Location = new System.Drawing.Point(64, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(240, 40);
			this.label1.TabIndex = 3;
			this.label1.Text = "Текущата година е:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label2.Location = new System.Drawing.Point(40, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(312, 48);
			this.label2.TabIndex = 4;
			this.label2.Text = "При приключване на годината ще се подновят отпуските на всички служители и ще се " +
				"освободят щатните бройки за сезонните служители";
			// 
			// formFinishYear
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(376, 182);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonFinish);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(384, 216);
			this.MinimumSize = new System.Drawing.Size(384, 216);
			this.Name = "formFinishYear";
			this.ShowInTaskbar = false;
			this.Text = "Приклчюване на година";
			this.toolTip1.SetToolTip(this, "Преди да приключите годината проверете дали датата на компютъра е коректна ");
			this.Load += new System.EventHandler(this.formFinishYear_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Сигурни ли сте че искате да приключите година " + currentYear.ToString() + " ? Промените ще бъдат необратими!", "Въпрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				fa.CalculateExperienceCorrection(this.currentYear); // Изчисляваме трудовия стаж за корекция (намалява се с броя дни неплатен отпуск над 30)
				
				if (this.mainCopy.connString.Contains("hrnso"))
				{
					currentYear++; // Нова година, нов късмет :)
					fa.UpdateYear(this.currentYear);
					this.UpdateHolidayNewYearNSO(this.currentYear);
				}
				else if (this.mainCopy.connString.Contains("hrshumen"))
				{
					string connectionString;
					if (mainForm.GetConnString(out connectionString) == false)
						return;
					fa.UpdateHolidayNewYearShumen(connectionString);
				}
				else
				{
					currentYear++; // Нова година, нов късмет :)
					fa.UpdateYear(this.currentYear);
					fa.UpdateHolidayNewYear(this.currentYear);
				}

				fa.ReloadSeasonWorkers();

				this.label1.Text = "Текущата година е: " + currentYear.ToString();
			}
		}

		private void formFinishYear_Load(object sender, System.EventArgs e)
		{
			DataTable dty = new DataTable();
			bool read = false;
			dty = this.fa.SelectWhere(TableNames.Year, "*", "");
			if (dty == null)
			{
				MessageBox.Show("Грешка при зареждане на данните за година", ErrorMessages.NoConnection);
				this.Close();
			}
			read = int.TryParse(dty.Rows[0]["year"].ToString(), out this.currentYear);
			if (read == false)
			{
				MessageBox.Show("Невалидни данни за година");
				this.Close(); //do not allow attempt to finish year if there is a problem with the database
			}
			this.label1.Text = "Текущата година е: " + currentYear.ToString();
		}

		public void UpdateHolidayNewYearNSO(int year)
		{
			DataTable dt = new DataTable();

			dt = fa.SelectWhere(TableNames.PersonAssignment, "id, parent, numHoliday, additionalHoliday, law, years, months, days, assignedat", "WHERE IsActive = 1");

			try
			{
				foreach (DataRow row in dt.Rows)
				{
					try
					{
						int total = 0;
						try
						{
							total += int.Parse(row["numholiday"].ToString());
						}
						catch (FormatException)
						{
						}
						try
						{
							total += int.Parse(row["additionalholiday"].ToString());
						}
						catch (FormatException)
						{
						}

						if (total >= 40)
						{
							Dictionary<string, object> hDict = new Dictionary<string, object>();

							hDict.Add("parent", row["parent"]);
							hDict.Add("year", year);
							hDict.Add("leftover", total);
							hDict.Add("total", total);

							fa.UniversalInsertParam(TableNames.YearHoliday, hDict, "id", TransactionComnmand.NO_TRANSACTION);
						}
						else if (row["law"].ToString().ToLower() == "служебно")
						{
							Dictionary<string, object> hDict = new Dictionary<string, object>();
							hDict.Add("parent", row["parent"]);
							hDict.Add("year", year);
							hDict.Add("leftover", total + 1);
							hDict.Add("total", total + 1);
							fa.UniversalInsertParam(TableNames.YearHoliday, hDict, "id", TransactionComnmand.NO_TRANSACTION);

							Dictionary<string, object> assDict = new Dictionary<string, object>();
							int add = int.Parse(row["numholiday"].ToString());
							add++;
							assDict.Add("numholiday", add);
							fa.UniversalUpdateParam(TableNames.PersonAssignment, "id", assDict, row["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
						}
						else
						{
							Dictionary<string, object> hDict = new Dictionary<string, object>();
							hDict.Add("parent", row["parent"]);
							hDict.Add("year", year);
							hDict.Add("leftover", total);
							hDict.Add("total", total);
							fa.UniversalInsertParam(TableNames.YearHoliday, hDict, "id", TransactionComnmand.NO_TRANSACTION);
						}
					}
					catch (Exception )
					{
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}