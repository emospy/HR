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
	/// Summary description for FormEKDAAdd.
	/// </summary>
	public class FormEKDAView : System.Windows.Forms.Form
	{
		private FormEKDAAdd form;
		private mainForm formmain;
		private DataTable dt;
		private DataLayer.DataAction da;
		private System.Windows.Forms.DataGridTableStyle ts = new DataGridTableStyle();
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonEdit;
		private DataGridView dataGridView1;
		private System.Windows.Forms.Button buttonAdd;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public FormEKDAView(mainForm main)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.formmain = main;						
			this.da = new DataAction(this.formmain.connString );			
			this.Text = "Единен клaсификатор на длъжностите в администрацията";			
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEKDAView));
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
			this.buttonExit.TabIndex = 3;
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
			this.buttonDelete.TabIndex = 2;
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
			this.buttonEdit.TabIndex = 1;
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
			this.buttonAdd.TabIndex = 0;
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
			this.dataGridView1.TabIndex = 4;
			// 
			// FormEKDAView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(992, 706);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonEdit);
			this.Controls.Add(this.buttonAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEKDAView";
			this.ShowInTaskbar = false;
			this.Text = "Класификатор на длъжностите в администрацията";
			this.Load += new System.EventHandler(this.FormEKDAView_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonAdd_Click(object sender, System.EventArgs e)
		{
			form = new FormEKDAAdd(this.formmain);				
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				int id;
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				this.PopulatePackageFromForm(form, Dict);
				id = this.da.UniversalInsertParam(TableNames.Ekda, Dict, "id", TransactionComnmand.NO_TRANSACTION);
				if (id > 0)
				{
					Dict.Add("ID", id.ToString());
					AddPackageToTable(Dict);
				}
				else
				{
					MessageBox.Show("Грешка при добавяне на номенкклатура", ErrorMessages.NoConnection);
				}
			}
		}		

		private void PopulatePackageFromForm(FormEKDAAdd form, Dictionary <string, object> Dict)
		{			
			Dict.Add("Code", form.textBoxEKDACode.Text);
			Dict.Add("Level", form.textBoxEKDALevel.Text);			
			Dict.Add("MinSalary", form.numBoxMinSalary.Text);
			Dict.Add("MaxSalary", form.numBoxMaxSalary.Text);			
			Dict.Add("PorNum", form. numBoxPorNum.Text);
			Dict.Add("Rang", form.comboBoxRang.Text);
			Dict.Add("Education", form.comboBoxEducation.Text);
			Dict.Add("Law", form.comboBoxLaw.Text);
			Dict.Add("Experience", form.comboBoxExperience.Text);
		}

		private void AddPackageToTable(Dictionary<string, object> Dict)
		{
			DataRow row = this.dt.NewRow();

			foreach (KeyValuePair<string, object> kvp in Dict)
			{
				row[kvp.Key] = Dict[kvp.Key];
			}		
			this.dt.Rows.Add(row);			
		}

		private void buttonEdit_Click(object sender, System.EventArgs e)
		{	
			if( this.dataGridView1.CurrentRow != null )
			{
				form = new FormEKDAAdd(this.formmain);
				DataRow row = this.dt.Rows.Find(this.dataGridView1.CurrentRow.Cells["id"].Value);
            
				form.textBoxEKDACode.Text = row["code"].ToString();
				form.textBoxEKDALevel.Text = row["level"].ToString();
				form.numBoxMaxSalary.Text = row["maxSalary"].ToString();
				form.numBoxMinSalary.Text = row["minSalary"].ToString();
				form.numBoxPorNum.Text = row["porNum"].ToString();
				int index;
				index = form.comboBoxEducation.FindString(row["education"].ToString());
				if(index != -1)
				{
					form.comboBoxEducation.SelectedIndex = index;
				}

				index = form.comboBoxExperience.FindString(row["experience"].ToString());
				if(index != -1)
				{
					form.comboBoxExperience.SelectedIndex = index;
				}

				index = form.comboBoxLaw.FindString(row["law"].ToString());
				if(index != -1)
				{
					form.comboBoxLaw.SelectedIndex = index;
				}
				index = form.comboBoxRang.FindString(row["rang"].ToString());
				if(index != -1)
				{
					form.comboBoxRang.SelectedIndex = index;
				}
				
				form.ShowDialog(this);
				if( form.DialogResult == DialogResult.OK)
				{
					Dictionary<string, object> Dict = new Dictionary<string, object>();
					this.PopulatePackageFromForm(form, Dict);

					if (this.da.UniversalUpdateParam(TableNames.Ekda, "id", Dict, row["id"].ToString(), TransactionComnmand.NO_TRANSACTION))
					{
						try
						{
							Dict.Add("ID", row["id"].ToString());
						}
						catch (System.Exception ex)
						{
							MessageBox.Show(ex.Message, "Грешен идентификатор");
							return;
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

		private void UpdatePackageInTable(Dictionary<string, object> Dict)
		{
			DataRow row = this.dt.Rows.Find(Dict["ID"]);
			if (row != null)
			{
				foreach (KeyValuePair<string, object> kvp in Dict)
				{
					row[kvp.Key] = Dict[kvp.Key];
				}
			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridView1.CurrentRow != null)
			{
				if( DialogResult.OK == MessageBox.Show( this, "Наистина ли искате да изтриете избраната номенклатура", "Въпрос", MessageBoxButtons.OKCancel ))
				{
					string del;
					del = this.dataGridView1.CurrentRow.Cells["id"].Value.ToString();

					if (this.da.UniversalDelete(TableNames.Ekda, del, "id"))
					{
						DataRow Row = this.dt.Rows.Find(del);
						if(Row != null)
							this.dt.Rows.Remove(Row);
					}
					else
					{
						MessageBox.Show("Грешка при изтриване на номенклатура", ErrorMessages.NoConnection);
					}
				}
			}
		}

		private void JustifyGridView(DataGridView dgv)
		{
			try
			{
				foreach (DataGridViewColumn columnStyle in dgv.Columns)
				{

					switch (columnStyle.Name.ToLower())
					{
						case "code":
						{
							columnStyle.HeaderText = "Код";
							columnStyle.Visible = true;
							break;
						}
						case "level":
						{
							columnStyle.HeaderText = "Име";
							columnStyle.Visible = true;
							break;
						}		
						case "minSalary":
						{
							columnStyle.HeaderText = "Минимална заплата";
							columnStyle.Visible = true;
							break;
						}
						case "maxSalary":
						{
							columnStyle.HeaderText = "Максимална заплата";
							columnStyle.Visible = true;
							break;
						}
						case "porNum":
						{
							columnStyle.HeaderText = "Пореден номер";
							columnStyle.Visible = true;
							break;
						}
						case "rang":
						{
							columnStyle.HeaderText = "Ранг";
							columnStyle.Visible = true;
							break;
						}
						case "experience":
						{
							columnStyle.HeaderText = "Опит";
							columnStyle.Visible = true;
							break;
						}
						case "education":
						{
							columnStyle.HeaderText = "Образование";
							columnStyle.Visible = true;
							break;
						}
						case "law":
						{
							columnStyle.HeaderText = "Правоотношение";
							columnStyle.Visible = true;
							break;
						}
						default :
						{
							columnStyle.Visible = false;	
							break;
						}
					}				
				}
			}
			catch(System.Exception e)
			{
				MessageBox.Show("Some Error", e.Message);
			}
		}

		private void FormEKDAView_Load(object sender, EventArgs e)
		{
			this.dt = da.SelectWhere(TableNames.Ekda, "*", "");
			if (this.dt == null)
			{
				MessageBox.Show("Грешка при зареждане на номенклатура ЕКДА", ErrorMessages.NoConnection);
				this.Close();
			}
			this.dt.PrimaryKey = new DataColumn[] { this.dt.Columns["ID"] };
			this.dataGridView1.DataSource = this.dt;
			this.dataGridView1.ClearSelection();
			this.JustifyGridView(this.dataGridView1);
		}		
	}
}
