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
	/// Summary description for FormEducationClassifier.
	/// </summary>
	public class FormEducationNomenclature : System.Windows.Forms.Form
	{
		private FormEducationAdd form;
		private mainForm formmain;
		private DataTable dt;
		private DataAction da;
		private System.Windows.Forms.DataGridTableStyle ts = new DataGridTableStyle();
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonEdit;
		private System.Windows.Forms.Button buttonAdd;
		private DataGridView dataGridView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Konstruktor
		/// </summary>
		public FormEducationNomenclature(mainForm main)
		{
			InitializeComponent();

			this.formmain = main;
			da = new DataAction(main.connString);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEducationNomenclature));
			this.buttonExit = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonEdit = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonExit
			// 
			this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
			this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonExit.Location = new System.Drawing.Point(896, 125);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(90, 23);
			this.buttonExit.TabIndex = 14;
			this.buttonExit.Text = "Изход";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonDelete.Location = new System.Drawing.Point(896, 86);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(90, 23);
			this.buttonDelete.TabIndex = 13;
			this.buttonDelete.Text = "   Изтриване";
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonEdit
			// 
			this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.Image")));
			this.buttonEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEdit.Location = new System.Drawing.Point(896, 47);
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.Size = new System.Drawing.Size(90, 23);
			this.buttonEdit.TabIndex = 12;
			this.buttonEdit.Text = "   Корекция";
			this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
			this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAdd.Location = new System.Drawing.Point(896, 8);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(90, 23);
			this.buttonAdd.TabIndex = 11;
			this.buttonAdd.Text = "   Добавяне";
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView1.Location = new System.Drawing.Point(5, 8);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(883, 692);
			this.dataGridView1.TabIndex = 15;
			// 
			// FormEducationNomenclature
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(992, 706);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonEdit);
			this.Controls.Add(this.buttonAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEducationNomenclature";
			this.Text = "Номенклатура обучения";
			this.Load += new System.EventHandler(this.FormEducationNomenclature_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				form = new FormEducationAdd();
				if (form.ShowDialog(this) == DialogResult.OK)
				{
					int id;
					Dictionary<string, object> Dict = new Dictionary<string, object>();
					this.PopulatePackageFromForm(form, Dict);
					id = this.da.UniversalInsertParam(TableNames.EducationNomenklature, Dict, "id", TransactionComnmand.NO_TRANSACTION);
					if (id > 0)
					{
						Dict.Add("ID", id.ToString());
						AddPackageToTable(Dict);
					}
					else
					{
						MessageBox.Show("Грешка при добавяне на номенклатура", ErrorMessages.NoConnection);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridView1.CurrentRow != null)
				{
					form = new FormEducationAdd();
					
					DataRow row = this.dt.Rows.Find(this.dataGridView1.CurrentRow.Cells["id"].Value);

					if (row == null)
					{
						MessageBox.Show("Реда не може да бъде намерен.");
						return;
					}
					form.textBoxEducationTheme.Text = row["educationname"].ToString();
					form.textBoxEducationCode.Text = row["educationcode"].ToString();
					form.textBoxEducationArea.Text = row["educationarea"].ToString();
					form.textBoxEducationPlace.Text = row["place"].ToString();
					form.textBoxEducationOrganizer.Text = row["organisation"].ToString();
					form.numBoxEducationPrice.Text = row["price"].ToString();
					form.numBoxEducationDays.Text = row["numdays"].ToString();
					form.numBoxEducationHours.Text = row["numhours"].ToString();

					form.ShowDialog(this);
					if (form.DialogResult == DialogResult.OK)
					{
						Dictionary<string, object> Dict = new Dictionary<string, object>();
						this.PopulatePackageFromForm(form, Dict);


						if (this.da.UniversalUpdateParam(TableNames.EducationNomenklature, "id", Dict, row["id"].ToString(), TransactionComnmand.NO_TRANSACTION))
						{
							try
							{
								Dict.Add("ID", row["id"].ToString());
							}
							catch (System.Exception ex)
							{
								MessageBox.Show(ex.Message, "Грешен идентификатор");
							}
							this.UpdatePackageInTable(Dict);
						}
						else
						{
							MessageBox.Show("Грешка при редакция на номенклатура", ErrorMessages.NoConnection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
		
		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridView1.CurrentRow != null)
				{
					if (DialogResult.OK == MessageBox.Show(this, "Наистина ли искате да изтриете избраната номенклатура", "Въпрос", MessageBoxButtons.OKCancel))
					{
						string del = this.dataGridView1.CurrentRow.Cells["id"].Value.ToString();
						if (this.da.UniversalDelete(TableNames.EducationNomenklature, del, "id"))
						{
							DataRow delr = this.dt.Rows.Find(del);
							this.dt.Rows.Remove(delr);
						}
						else
						{
							MessageBox.Show("Грешка при изтриване на номенклатура", ErrorMessages.NoConnection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void PopulatePackageFromForm(FormEducationAdd form, Dictionary <string, object> Dict)
		{
			try
			{
				Dict.Add("EducationName", form.textBoxEducationTheme.Text);
				Dict.Add("EducationCode", form.textBoxEducationCode.Text);
				Dict.Add("EducationArea", form.textBoxEducationArea.Text);
				Dict.Add("NumDays", form.numBoxEducationDays.Text);
				Dict.Add("NumHours", form.numBoxEducationHours.Text);
				Dict.Add("Price", form.numBoxEducationPrice.Text);
				Dict.Add("Place", form.textBoxEducationPlace.Text);
				Dict.Add("Organisation", form.textBoxEducationOrganizer.Text);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void AddPackageToTable(Dictionary<string, object> Dict)
		{
			try
			{
				DataRow row = this.dt.NewRow();

				row["id"] = Dict["ID"];
				row["educationname"] = Dict["EducationName"];
				row["educationcode"] = Dict["EducationCode"];
				row["educationarea"] = Dict["EducationArea"];
				row["numhours"] = Dict["NumHours"];
				row["numdays"] = Dict["NumDays"];
				row["price"] = Dict["Price"];
				row["place"] = Dict["Place"];
				row["organisation"] = Dict["Organisation"];

				this.dt.Rows.Add(row);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void UpdatePackageInTable(Dictionary<string, object> Dict)
		{
			try
			{
				DataRow row = this.dt.Rows.Find(Dict["ID"]);

				row["educationname"] = Dict["EducationName"];
				row["educationcode"] = Dict["EducationCode"];
				row["id"] = Dict["ID"];
				row["educationarea"] = Dict["EducationArea"];
				row["numhours"] = Dict["NumHours"];
				row["numdays"] = Dict["NumDays"];
				row["place"] = Dict["Place"];
				row["price"] = Dict["Price"];
				row["organisation"] = Dict["Organisation"];
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void JustifyGridView(DataGridView dgv)
		{
			try
			{
				try
				{
					foreach (DataGridViewColumn columnStyle in dgv.Columns)
					{

						switch (columnStyle.Name.ToLower())
						{
							case "educationname":
								{
									columnStyle.HeaderText = "Тема";
									columnStyle.Visible = true;
									break;
								}
							case "educationcode":
								{
									columnStyle.HeaderText = "Код";
									columnStyle.Visible = true;
									break;
								}
							case "educationarea":
								{
									columnStyle.HeaderText = "Област";
									columnStyle.Visible = true;
									break;
								}
							case "numdays":
								{
									columnStyle.HeaderText = "Брой дни";
									columnStyle.Visible = true;
									break;
								}
							case "numhours":
								{
									columnStyle.HeaderText = "Брой часове";
									columnStyle.Visible = true;
									break;
								}
							case "price":
								{
									columnStyle.HeaderText = "Цена";
									columnStyle.Visible = true;
									break;
								}
							case "place":
								{
									columnStyle.HeaderText = "Място на провеждане";
									columnStyle.Visible = true;
									break;
								}
							case "organisation":
								{
									columnStyle.HeaderText = "Провеждаща организация";
									columnStyle.Visible = true;
									break;
								}
							default:
								{
									columnStyle.Visible = false;
									break;
								}
						}

					}
				}
				catch (System.Exception e)
				{
					MessageBox.Show("Some Error", e.Message);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void FormEducationNomenclature_Load(object sender, EventArgs e)
		{
			try
			{
				this.dt = da.SelectWhere(TableNames.EducationNomenklature, "*", "");
				if (this.dt == null)
				{
					MessageBox.Show("Грешка при зареждане на номенклатура обучения", ErrorMessages.NoConnection);
					this.Close();
				}
				this.dt.PrimaryKey = new DataColumn[] { this.dt.Columns["ID"] };
				this.dataGridView1.DataSource = this.dt;
				this.dataGridView1.ClearSelection();
				this.JustifyGridView(this.dataGridView1);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
	}
}
