using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DataLayer;

namespace HR
{
	/// <summary>
	/// Summary description for KartotekaLichenSystaw.
	/// </summary>
	public class KartotekaLichenSystaw : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList FilterColumns = new ArrayList();
		private mainForm mainform;
		private ExcelExpo Ex;
		internal bool IsFired = false;
		DataGridTableStyle ts = new DataGridTableStyle();
		formStatisticTotal stat;
		formStatisticTotal2 stat2;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Button buttonFind;
		private System.Windows.Forms.Button buttonFiles;
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.Button buttonNewEmployee;
		private System.Windows.Forms.Button buttonStatistics;
		private System.Windows.Forms.StatusBar statusBarKartoteka;
		private System.Windows.Forms.StatusBarPanel statusBarPanelHeader;
		private System.Windows.Forms.StatusBarPanel statusBarPanelNumberEmployees;
		private System.Windows.Forms.StatusBarPanel statusBarPanelLabelFilter;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.StatusBarPanel statusBarPanelFilter;
		private DataGridView dataGridView1;
		private Button buttonStatistics2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable GridDataSource
		{
			get
			{
				return (DataTable)this.dataGridView1.DataSource;
			}
			set
			{
				this.dataGridView1.DataSource = value;
			}
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		public KartotekaLichenSystaw(mainForm form, DataTable dt, string caption, bool IsFired)
		{
			mainform = form;
			Ex = new ExcelExpo();
			InitializeComponent();
			this.IsFired = IsFired;
			if (this.IsFired)
			{
				buttonNewEmployee.Enabled = false;
			}
			this.dataGridView1.DataSource = dt;
			this.statusBarPanelNumberEmployees.Text = dt.Rows.Count.ToString() + " ";
			this.statusBarPanelFilter.Text = "Не е наложен филтър";
			this.Text = caption;
			this.stat = new formStatisticTotal(this.mainform, true, this.IsFired);
			this.stat2 = new formStatisticTotal2(this.mainform, true, this.IsFired);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KartotekaLichenSystaw));
			this.buttonExit = new System.Windows.Forms.Button();
			this.buttonFind = new System.Windows.Forms.Button();
			this.buttonFiles = new System.Windows.Forms.Button();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.buttonNewEmployee = new System.Windows.Forms.Button();
			this.buttonStatistics = new System.Windows.Forms.Button();
			this.statusBarKartoteka = new System.Windows.Forms.StatusBar();
			this.statusBarPanelHeader = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelNumberEmployees = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelLabelFilter = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelFilter = new System.Windows.Forms.StatusBarPanel();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.buttonStatistics2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelNumberEmployees)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelFilter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelFilter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
			this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonExit.Location = new System.Drawing.Point(830, 8);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(150, 23);
			this.buttonExit.TabIndex = 1;
			this.buttonExit.Text = " Изход";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// buttonFind
			// 
			this.buttonFind.Image = ((System.Drawing.Image)(resources.GetObject("buttonFind.Image")));
			this.buttonFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFind.Location = new System.Drawing.Point(335, 8);
			this.buttonFind.Name = "buttonFind";
			this.buttonFind.Size = new System.Drawing.Size(150, 23);
			this.buttonFind.TabIndex = 2;
			this.buttonFind.Text = "    Търсене";
			this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
			// 
			// buttonFiles
			// 
			this.buttonFiles.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiles.Image")));
			this.buttonFiles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiles.Location = new System.Drawing.Point(170, 8);
			this.buttonFiles.Name = "buttonFiles";
			this.buttonFiles.Size = new System.Drawing.Size(150, 23);
			this.buttonFiles.TabIndex = 3;
			this.buttonFiles.Text = "  Досие";
			this.buttonFiles.Click += new System.EventHandler(this.buttonFiles_Click);
			// 
			// buttonPrint
			// 
			this.buttonPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrint.Image")));
			this.buttonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPrint.Location = new System.Drawing.Point(500, 8);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(150, 23);
			this.buttonPrint.TabIndex = 4;
			this.buttonPrint.Text = "Печат";
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// buttonNewEmployee
			// 
			this.buttonNewEmployee.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewEmployee.Image")));
			this.buttonNewEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonNewEmployee.Location = new System.Drawing.Point(8, 8);
			this.buttonNewEmployee.Name = "buttonNewEmployee";
			this.buttonNewEmployee.Size = new System.Drawing.Size(150, 23);
			this.buttonNewEmployee.TabIndex = 5;
			this.buttonNewEmployee.Text = "    Нов служител";
			this.buttonNewEmployee.Click += new System.EventHandler(this.buttonNewEmployee_Click);
			// 
			// buttonStatistics
			// 
			this.buttonStatistics.Image = ((System.Drawing.Image)(resources.GetObject("buttonStatistics.Image")));
			this.buttonStatistics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonStatistics.Location = new System.Drawing.Point(665, 8);
			this.buttonStatistics.Name = "buttonStatistics";
			this.buttonStatistics.Size = new System.Drawing.Size(106, 23);
			this.buttonStatistics.TabIndex = 6;
			this.buttonStatistics.Text = "Справки";
			this.buttonStatistics.Click += new System.EventHandler(this.buttonStatistics_Click);
			// 
			// statusBarKartoteka
			// 
			this.statusBarKartoteka.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statusBarKartoteka.Location = new System.Drawing.Point(0, 684);
			this.statusBarKartoteka.Name = "statusBarKartoteka";
			this.statusBarKartoteka.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanelHeader,
            this.statusBarPanelNumberEmployees,
            this.statusBarPanelLabelFilter,
            this.statusBarPanelFilter});
			this.statusBarKartoteka.ShowPanels = true;
			this.statusBarKartoteka.Size = new System.Drawing.Size(992, 22);
			this.statusBarKartoteka.SizingGrip = false;
			this.statusBarKartoteka.TabIndex = 7;
			// 
			// statusBarPanelHeader
			// 
			this.statusBarPanelHeader.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarPanelHeader.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarPanelHeader.Name = "statusBarPanelHeader";
			this.statusBarPanelHeader.Text = "Общо служители:";
			this.statusBarPanelHeader.Width = 111;
			// 
			// statusBarPanelNumberEmployees
			// 
			this.statusBarPanelNumberEmployees.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.statusBarPanelNumberEmployees.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
			this.statusBarPanelNumberEmployees.Name = "statusBarPanelNumberEmployees";
			this.statusBarPanelNumberEmployees.ToolTipText = "Общ брой на служителите показани в картотеката";
			this.statusBarPanelNumberEmployees.Width = 50;
			// 
			// statusBarPanelLabelFilter
			// 
			this.statusBarPanelLabelFilter.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarPanelLabelFilter.Name = "statusBarPanelLabelFilter";
			this.statusBarPanelLabelFilter.Text = "Филтър:";
			this.statusBarPanelLabelFilter.Width = 60;
			// 
			// statusBarPanelFilter
			// 
			this.statusBarPanelFilter.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
			this.statusBarPanelFilter.Name = "statusBarPanelFilter";
			this.statusBarPanelFilter.Width = 1200;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView1.Location = new System.Drawing.Point(8, 38);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(972, 640);
			this.dataGridView1.TabIndex = 8;
			this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
			this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
			this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
			this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
			// 
			// buttonStatistics2
			// 
			this.buttonStatistics2.Image = ((System.Drawing.Image)(resources.GetObject("buttonStatistics2.Image")));
			this.buttonStatistics2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonStatistics2.Location = new System.Drawing.Point(777, 8);
			this.buttonStatistics2.Name = "buttonStatistics2";
			this.buttonStatistics2.Size = new System.Drawing.Size(47, 23);
			this.buttonStatistics2.TabIndex = 9;
			this.buttonStatistics2.Text = "   2";
			this.buttonStatistics2.Click += new System.EventHandler(this.buttonStatistics2_Click);
			// 
			// KartotekaLichenSystaw
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonExit;
			this.ClientSize = new System.Drawing.Size(992, 706);
			this.Controls.Add(this.buttonStatistics2);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.statusBarKartoteka);
			this.Controls.Add(this.buttonStatistics);
			this.Controls.Add(this.buttonNewEmployee);
			this.Controls.Add(this.buttonPrint);
			this.Controls.Add(this.buttonFiles);
			this.Controls.Add(this.buttonFind);
			this.Controls.Add(this.buttonExit);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "KartotekaLichenSystaw";
			this.ShowInTaskbar = false;
			this.Text = "Картотека личен състав";
			this.Load += new System.EventHandler(this.KartotekaLichenSystaw_Load);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelNumberEmployees)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelFilter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelFilter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void KartotekaLichenSystaw_Load(object sender, System.EventArgs e)
		{
			DisplayFilter();
			JustifyGrid(this.dataGridView1);
		}

		private void DisplayFilter()
		{
			string Filter = " ";
			bool filter = false;
			for (int i = 0; i < this.FilterColumns.Count; i++)
			{
				bool found = true;
				string[] spl = this.FilterColumns[i].ToString().Split(new char[] { '.' });
				string str;
				if (spl.Length > 1)
				{
					str = spl[1];
				}
				else
				{
					str = spl[0];
				}
				switch (str.ToLower())
				{
					case "receivedaddon":
						{
							Filter += "Пари за дрехи ";
							break;
						}
					case "militaryrang":
						{
							Filter += "Военно звание ";
							break;
						}
					case "militarydegree":
						{
							Filter += "Степен на военно звание ";
							break;
						}
					case "category":
						{
							Filter += "Категория ";
							break;
						}
					case "country":
						{
							Filter += "Държава ";
							break;
						}
					case "town":
						{
							Filter += "Град ";
							break;
						}
					case "borntown":
						{
							Filter += "Месторождение ";
							break;
						}
					case "region":
						{
							Filter += "Област ";
							break;
						}
					case "kwartal":
						{
							Filter += "Адрес ";
							break;
						}
					//					case "street":
					//					{
					//						Filter += "Улица ";
					//						break;
					//					}
					//					case "numblockhouse":
					//					{
					//						Filter += "Адрес ";
					//						break;
					//					}
					case "pcard":
						{
							Filter += "Номер лична карта ";

							break;
						}
					case "pcardpublish":
						{
							Filter += "Издадена от ";
							break;
						}
					case "publishedby":
						{
							Filter += "Издадена на ";
							break;
						}
					case "familystatus":
						{
							Filter += "Семеен статус ";
							break;
						}
					case "education":
						{
							Filter += "Образование ";
							break;
						}
					case "profession":
						{
							Filter += "Професия ";
							break;
						}
					case "sciencetitle":
						{
							Filter += "Научно звание ";
							break;
						}
					case "language":
						{
							Filter += "Чужд език ";
							break;
						}
					case "name":
						{
							Filter += "Име ";
							break;
						}
					case "egn":
						{
							Filter += "ЕГН ";
							break;
						}
					case "sex":
						{
							Filter += "Пол ";
							break;
						}
					case "level4":
						{
							Filter += "Сектор ";
							break;
						}
					case "level1":
						{
							Filter += "Администрация ";
							break;
						}
					case "level2":
						{
							Filter += "Дирекция ";
							break;
						}
					case "level3":
						{
							Filter += "Отдел ";
							break;
						}
					case "law":
						{
							Filter += "Трудово правоотношение ";
							break;
						}
					case "position":
						{
							Filter += "Длъжност ";
							break;
						}
					case "contract":
						{
							Filter += "Договор ";
							break;
						}
					case "testcontractdate":
						{
							Filter += "Изпитателен срок ";
							break;
						}
					case "worktime":
						{
							Filter += "Работно време ";
							break;
						}
					case "assignedat":
						{
							Filter += "Назначен на ";
							break;
						}
					case "contractexpiry":
						{
							Filter += "Договора изтича на ";
							break;
						}
					case "staff":
						{
							Filter += "Щат ";
							break;
						}
					case "penaltydate":
						{
							Filter += "Дата на наказанието ";
							break;
						}
					case "assignreason":
						{
							Filter += "Основание за назначаване ";
							break;
						}
					case "reason":
						{
							Filter += "Основание ";
							break;
						}
					case "numberorder":
						{
							Filter += "Номер на заповед ";
							break;
						}
					case "fromdate":
						{
							Filter += "Дата на постановлението ";
							break;
						}
					case "todate":
						{
							Filter += "До дата ";
							break;
						}
					case "countdays":
						{
							Filter += "Брой дни ";
							break;
						}
					case "calendardays":
						{
							Filter += "Календарни дни ";
							break;
						}
					case "typepenalty":
						{
							Filter += "Вид наказание ";
							break;
						}
					case "typeabsence":
						{
							Filter += "Вид отсъствие ";
							break;
						}
					case "basesalary":
						{
							Filter += "Основна заплата ";
							break;
						}
					case "years":
						{
							Filter += "Брой години ";
							break;
						}
					case "borndate":
						{
							Filter += "Дата на раждане ";
							break;
						}
					default:
						{
							found = false;
							break;
						}
				}
				if (found)
				{
					filter = true;
				}
			}
			if (filter == false)
			{
				Filter = "Не е зададен филтър";
			}
			else
			{
				this.statusBarPanelFilter.Text = Filter;
			}
			DataTable dt = (DataTable)this.dataGridView1.DataSource;
			this.statusBarPanelNumberEmployees.Text = dt.Rows.Count.ToString() + " ";
		}

		#region Buttons
		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonPrint_Click(object sender, System.EventArgs e)
		{
			if (this.dataGridView1.RowCount > 0)
			{
				DataView vue = new DataView((DataTable)this.dataGridView1.DataSource, "", "", DataViewRowState.CurrentRows);
				Ex.ExportView(this.dataGridView1, vue, "");
			}
		}

		private void buttonNewEmployee_Click(object sender, System.EventArgs e)
		{
			formPersonalData form = new formPersonalData(this.mainform, this.IsFired);
			form.ShowDialog(this);
		}

		private void buttonFiles_Click(object sender, System.EventArgs e)
		{
			this.dataGridView1_DoubleClick(sender, e);
		}

		private void buttonFind_Click(object sender, System.EventArgs e)
		{
			if (dataGridView1.RowCount > 0)
			{
				formFind find = new formFind(this.dataGridView1, this.mainform);
				find.ShowDialog(this);
			}
		}
		#endregion

		#region Event reactions
		private void dataGridView1_DoubleClick(object sender, EventArgs e)
		{
			try
			{

				if (dataGridView1.CurrentRow != null)
				{
					dataGridView1.CurrentRow.Selected = true;
					formPersonalData form = new formPersonalData(dataGridView1.CurrentRow.Cells["id"].Value.ToString(), this.mainform, this.IsFired);
					form.ShowDialog(this);
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}

		private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			this.dataGridView1.CurrentRow.Selected = true;
		}

		private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
		{
			this.dataGridView1.CurrentRow.Selected = false;
		}

		#endregion

		private void buttonStatistics_Click(object sender, System.EventArgs e)
		{
			stat.ShowDialog();
			this.DisplayFilter();
			this.JustifyGrid(this.dataGridView1);
		}

		private void buttonStatistics2_Click(object sender, EventArgs e)
		{
			stat2.ShowDialog();
			this.DisplayFilter();
			this.JustifyGrid(this.dataGridView1);
		}

		private void JustifyGrid(DataGridView dgv)
		{
			try
			{
				foreach (DataGridViewColumn Col in dgv.Columns)
				{
					switch (Col.Name.ToLower())
					{
						case "firstassignment":
							{
								Col.HeaderText = "Първо назначение";
								Col.Visible = true;
							}
							break;
                        case "speciality":
                            {
                                Col.HeaderText = "Специалност";
                                Col.Visible = true;
                            }
                            break;
						case "receivedaddon":
							{
								Col.HeaderText = "Пари за дрехи";
								Col.Visible = true;
							}
							break;
						case "militaryrang":
							{
								Col.HeaderText = "Военнo звание";
								Col.Visible = true;
								break;
							}
						case "militarydegree":
							{
								Col.HeaderText = "Степен на военно звание";
								Col.Visible = true;
								break;
							}
						case "category":
							{
								Col.HeaderText = "Категория";
								Col.Visible = true;
								break;
							}
						case "country":
							{
								Col.HeaderText = "Държава";
								Col.Visible = true;
								break;
							}
						case "town":
							{
								Col.HeaderText = "Град";
								Col.Visible = true;
								break;
							}
						case "borntown":
							{
								Col.HeaderText = "Месторождение";
								Col.Visible = true;
								break;
							}
						case "region":
							{
								Col.HeaderText = "Област";
								Col.Visible = true;
								break;
							}
						case "kwartal":
							{
								Col.HeaderText = "Квартал";
								Col.Visible = true;
								break;
							}
						//						case "street":
						//						{
						//							Col.HeaderText = "Улица";
						//							Col.Visible = true;
						//							break;
						//						}
						//						case "numblockhouse":
						//						{
						//							Col.HeaderText = "Номер";
						//							Col.Visible = true;
						//							break;
						//						}
						case "pcard":
							{
								Col.HeaderText = "Номер лична карта";
								Col.Visible = true;
								break;
							}
						case "pcardpublish":
							{
								Col.HeaderText = "Издадена на";
								Col.Visible = true;
								break;
							}
						case "pcardexpiry":
							{
								Col.HeaderText = "Валидна до";
								Col.Visible = true;
								break;
							}
						case "publishedby":
							{
								Col.HeaderText = "Издадена от";
								Col.Visible = true;
								break;
							}
						case "familystatus":
							{
								Col.HeaderText = "Семеен статус";
								Col.Visible = true;
								break;
							}
						case "education":
							{
								Col.HeaderText = "Образование";
								Col.Visible = true;
								break;
							}
						case "engeducation":
							{
								Col.HeaderText = "Education";
								Col.Visible = true;
								break;
							}
						case "profession":
							{
								Col.HeaderText = "Професия";
								Col.Visible = true;
								break;
							}
						case "sciencetitle":
							{
								Col.HeaderText = "Научно звание";
								Col.Visible = true;
								break;
							}
						case "language":
							{
								Col.HeaderText = "Чужд език";
								Col.Visible = true;
								break;
							}
						case "name":
							{
								Col.HeaderText = "Име";
								Col.Visible = true;
								break;
							}
						case "engname":
							{
								Col.HeaderText = "Name";
								Col.Visible = true;
								break;
							}
						case "egn":
							{
								Col.HeaderText = "ЕГН";
								Col.Visible = true;
								break;
							}
						case "sex":
							{
								Col.HeaderText = "Пол";
								Col.Visible = true;
								break;
							}
						case "level4":
							{
								Col.HeaderText = "Сектор";
								Col.Visible = true;
								break;
							}
						case "level1":
							{
								Col.HeaderText = "Администрация";
								Col.Visible = true;
								break;
							}
						case "level2":
							{
								Col.HeaderText = "Дирекция";
								Col.Visible = true;
								break;
							}
						case "level3":
							{
								Col.HeaderText = "Отдел";
								Col.Visible = true;
								break;
							}
						case "level4eng":
							{
								Col.HeaderText = "Sector";
								Col.Visible = true;
								break;
							}
						case "level1eng":
							{
								Col.HeaderText = "Administration";
								Col.Visible = true;
								break;
							}
						case "level2eng":
							{
								Col.HeaderText = "Direction";
								Col.Visible = true;
								break;
							}
						case "level3eng":
							{
								Col.HeaderText = "Department";
								Col.Visible = true;
								break;
							}
						case "law":
							{
								Col.HeaderText = "Трудово правоотношение";
								Col.Visible = true;
								break;
							}
						case "positioneng":
							{
								Col.HeaderText = "Position";
								Col.Visible = true;
								break;
							}
						case "position":
							{
								Col.HeaderText = "Длъжност";
								Col.Visible = true;
								break;
							}
						case "contract":
							{
								Col.HeaderText = "Договор";
								Col.Visible = true;
								break;
							}
						case "testcontractdate":
							{
								Col.HeaderText = "Изпитателен срок";
								Col.Visible = true;
								break;
							}
						case "worktime":
							{
								Col.HeaderText = "Работно време";
								Col.Visible = true;
								break;
							}
						case "assignedat":
							{
								Col.HeaderText = "Назначен на";
								Col.Visible = true;
								break;
							}
						case "hiredat":
							{
								Col.HeaderText = "Първо назначение";
								Col.Visible = true;
								break;
							}
						case "contractexpiry":
							{
								Col.HeaderText = "Договора изтича на";
								Col.Visible = true;
								break;
							}
						case "staff":
							{
								Col.HeaderText = "Щат";
								Col.Visible = true;
								break;
							}
						case "penaltydate":
							{
								Col.HeaderText = "Дата на наказанието";
								Col.Visible = true;
								break;
							}
						case "assignreason":
							{
								Col.HeaderText = "Основание за назначаване";
								Col.Visible = true;
								break;
							}
						case "reason":
							{
								Col.HeaderText = "Основание";
								Col.Visible = true;
								break;
							}
						case "numberorder":
							{
								Col.HeaderText = "Номер на заповед";
								Col.Visible = true;
								break;
							}
						case "fromdate":
							{
								Col.HeaderText = "От дата";
								Col.Visible = true;
								break;
							}
						case "todate":
							{
								Col.HeaderText = "До дата";
								Col.Visible = true;
								break;
							}
						case "countdays":
							{
								Col.HeaderText = "Брой дни";
								Col.Visible = true;
								break;
							}
						case "calendardays":
							{
								Col.HeaderText = "Календарни дни";
								Col.Visible = true;
								break;
							}
						case "typepenalty":
							{
								Col.HeaderText = "Вид наказание";
								Col.Visible = true;
								break;
							}
						case "typeabsence":
							{
								Col.HeaderText = "Вид отсъствие";
								Col.Visible = true;
								break;
							}
						case "basesalary":
							{
								Col.HeaderText = "Основна заплата";
								Col.Visible = true;
								break;
							}
						case "salaryaddon":
							{
								Col.HeaderText = "Надбавки";
								Col.Visible = true;
								break;
							}
						case "years":
							{
								Col.HeaderText = "Години";
								Col.Visible = true;
								break;
							}
						case "year":
							{
								Col.HeaderText = "Година";
								Col.Visible = true;
								break;
							}
						case "totalmark":
							{
								Col.HeaderText = "Оценка";
								Col.Visible = true;
								break;
							}
						case "hasworkplan":
							{
								Col.HeaderText = "Работен план";
								Col.Visible = true;
								break;
							}
						case "hasmiddlemeeting":
							{
								Col.HeaderText = "Междинна среща";
								Col.Visible = true;
								break;
							}
						case "hastraining":
							{
								Col.HeaderText = "Обучение";
								Col.Visible = true;
								break;
							}
						case "hasrangupdate":
							{
								Col.HeaderText = "Повишение в ранг";
								Col.Visible = true;
								break;
							}
						case "forrangupdate":
							{
								Col.HeaderText = "За повишение";
								Col.Visible = true;
								break;
							}
						case "experience":
							{
								Col.HeaderText = "Стаж в организацията";
								Col.Visible = true;
								break;
							}
						case "days":
							{
								Col.HeaderText = "Дни";
								Col.Visible = true;
								break;
							}
						case "months":
							{
								Col.HeaderText = "Месеци";
								Col.Visible = true;
								break;
							}
						case "parentcontractdate":
							{
								Col.HeaderText = "Постъпил на";
								Col.Visible = true;
								break;
							}
						case "other1":
							{
								Col.HeaderText = "Други 1";
								Col.Visible = true;
								break;
							}
						case "other2":
							{
								Col.HeaderText = "Други 2";
								Col.Visible = true;
								break;
							}
						case "other3":
							{
								Col.HeaderText = "Други 3";
								Col.Visible = true;
								break;
							}
						case "other4":
							{
								Col.HeaderText = "Други 4";
								Col.Visible = true;
								break;
							}
						case "other5":
							{
								Col.HeaderText = "Други 5";
								Col.Visible = true;
								break;
							}
						case "id_sysco":
							Col.HeaderText = "Syscoset номер";
							Col.Visible = true;
							break;
						case "rangordervalidfrom":
							Col.HeaderText = "Звание в сила от";
							Col.Visible = true;
							break;
						case "rangorderdate":
							Col.HeaderText = "Заповед за звание от";
							Col.Visible = true;
							break;
						case "rangordernumber":
							Col.HeaderText = "Заповед за звание от";
							Col.Visible = true;
							break;
						case "borndate":
							Col.HeaderText = "Дата на раждане";
							Col.Visible = true;
							break;
						case "orderdate":
							Col.HeaderText = "Дата на заповед";
							Col.Visible = true;
							break;
						case "fireorder":
							Col.HeaderText = "Номер заповед";
							Col.Visible = true;
							break;
						case "fireorderdate":
							Col.HeaderText = "Дата на заповед";
							Col.Visible = true;
							break;
						case "total":
							Col.HeaderText = "Полагаем отпуск";
							Col.Visible = true;
							break;
						case "leftover":
							Col.HeaderText = "Остатък отпуск";
							Col.Visible = true;
							break;
						case "classpercent":
							Col.HeaderText = "% прослужено време";
							Col.Visible = true;
							break;
						default:
							Col.Visible = false;
							break;
					}

				}
			}
			catch (System.Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			Dictionary<string, object> Dict = new Dictionary<string, object>();
			Dict.Add("fired", "1");
			DataAction da = new DataAction(this.mainform.connString);
			if (da.UniversalUpdateParam(TableNames.Person, "id", Dict, this.dataGridView1.CurrentRow.Cells["id"].ToString(), TransactionComnmand.NO_TRANSACTION) == false)
			{
				MessageBox.Show("Прешка при прехвърляне");
			}
		}


	}
}
