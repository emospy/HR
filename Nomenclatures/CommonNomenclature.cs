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
	/// Форма за универсална номенклатура
	/// </summary>
	///<remarks>
	///Идеята е да се направи формата така, че да е максимално гъвкава и да позволява раобта с призволни по вид номенклатури.
	///</remarks>
	public class CommonNomenclature : System.Windows.Forms.Form
	{
		private mainForm formmain;
		private DataTable dt;
		private string table;
		private DataAction da;
		private int parent;
		private DataGridTableStyle ts = new DataGridTableStyle();
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
		/// Конструктор на класа
		/// </summary> 
		public CommonNomenclature(string table, string formName, DataTable dt, mainForm main, int par)
		{
			this.parent = par;
			this.dt = dt;
			this.formmain = main;
			this.table = table;
			this.da = new DataAction(this.formmain.connString);
			InitializeComponent();
			this.Text = formName;
		}

		/// <summary>
		/// Конструктор на класа
		/// </summary> 
		public CommonNomenclature(string table, string formName, DataTable dt, mainForm main)
		{
			this.parent = -1;
			this.dt = dt;
			this.formmain = main;
			this.table = table;
			this.da = new DataAction(this.formmain.connString);
			InitializeComponent();
			this.Text = formName;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonNomenclature));
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
			this.buttonExit.TabIndex = 9;
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
			this.buttonDelete.TabIndex = 8;
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
			this.buttonEdit.TabIndex = 7;
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
			this.buttonAdd.TabIndex = 6;
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
			this.dataGridView1.TabIndex = 10;
			// 
			// CommonNomenclature
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(992, 706);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonEdit);
			this.Controls.Add(this.buttonAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CommonNomenclature";
			this.ShowInTaskbar = false;
			this.Text = "CommonNomenclature";
			this.Load += new System.EventHandler(this.CommonNomenclature_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				ArrayList Columns = new ArrayList();
				foreach (DataGridViewColumn Col in dataGridView1.Columns)
				{
					if (Col.Visible)
					{
						MappingFormData map = new MappingFormData();
						map.HeaderText = Col.HeaderText;
						map.MappingName = Col.Name;
						Columns.Add(map);
					}
				}
				CommonNomenclatureAdd form = new CommonNomenclatureAdd(Columns);
				form.Text = "Добавяне на " + this.Text.ToLower();

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					int id;
					DataRow row = dt.NewRow();
					ArrayList ray = form.GetVariables();
					Dictionary<string, object> Dict = new Dictionary<string, object>();

					for (int i = 0; i < ray.Count; i++)
					{
						MappingFormData map = (MappingFormData)ray[i];
						Dict.Add(map.MappingName, map.ColumnText);
						row[map.MappingName] = map.ColumnText;
					}

					if (this.parent > 0)
					{
						Dict.Add("parent", this.parent.ToString());
					}

					id = this.da.UniversalInsertParam(this.table, Dict, "id", TransactionComnmand.NO_TRANSACTION);
					if (id > 0)
					{
						row["id"] = id;
						this.dt.Rows.Add(row);
					}
					else
					{
						MessageBox.Show("Грешка при добавяне на мноменклатура", ErrorMessages.NoConnection);
					}
				}
			}
			catch(ArgumentException)
			{
				MessageBox.Show("Въвели сте невалидни данни", "Грешка");
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (dataGridView1.CurrentRow != null)
				{
					if (DialogResult.OK == MessageBox.Show(this, "Наистина ли искате да изтриете избраната номенклатура", "Въпрос", MessageBoxButtons.OKCancel))
					{
						if (this.da.UniversalDelete(this.table, this.dataGridView1.CurrentRow.Cells["id"].Value.ToString(), "id"))
						{
							DataRow row = dt.Rows.Find(this.dataGridView1.CurrentRow.Cells["id"].Value);
							dt.Rows.Remove(row);
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

		private void buttonEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridView1.CurrentRow == null)
				{
					return;
				}

				ArrayList Columns = new ArrayList();
				foreach (DataGridViewColumn Col in this.dataGridView1.Columns)
				{
					if (Col.Visible)
					{                        
						MappingFormData map = new MappingFormData();
						map.HeaderText = Col.HeaderText;
						map.MappingName = Col.Name;
                        if (this.dataGridView1.CurrentRow.Cells[Col.Name].ValueType.Name == "Int32")
                        {
                            if (this.dataGridView1.CurrentRow.Cells[Col.Name].Value is System.DBNull)
                            {
                                this.dataGridView1.CurrentRow.Cells[Col.Name].Value = 0;
                            }
                        }
						map.ColumnText = this.dataGridView1.CurrentRow.Cells[Col.Name].Value.ToString();
						Columns.Add(map);
					}
				}
				CommonNomenclatureAdd form = new CommonNomenclatureAdd(Columns);
				form.Text = "Редакция на " + this.Text.ToLower();

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					Dictionary<string, object> Dict = new Dictionary<string, object>();
					ArrayList ray = form.GetVariables();
					for (int i = 0; i < ray.Count; i++)
					{
						MappingFormData map = (MappingFormData)ray[i];
						Dict.Add(map.MappingName, map.ColumnText);
					}
					if (this.da.UniversalUpdateParam(this.table, "id", Dict, this.dataGridView1.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.NO_TRANSACTION) == false)
					{
						MessageBox.Show("Грешка при редакция на номенклатура", ErrorMessages.NoConnection);
						return;
					}
					for (int i = 0; i < ray.Count; i++)
					{
						MappingFormData map = (MappingFormData)ray[i];
						this.dataGridView1.CurrentRow.Cells[map.MappingName].Value = map.ColumnText;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		//private void dataGrid1_Click(object sender, System.EventArgs e)
		//{
		//    if(this.dataGrid1.VisibleRowCount > 0)
		//    {
		//        this.dataGrid1.Select(this.dataGrid1.CurrentRowIndex);
		//    }
		//}

		//private void dataGrid1_CurrentCellChanged(object sender, System.EventArgs e)
		//{
		//    dataGrid1.UnSelect(dataGrid1.CurrentRowIndex);
		//    dataGrid1.Select(dataGrid1.CurrentRowIndex);
		//}

		private void JustifyGrid()
		{
			try
			{
				// Now create the column styles within the table style.
				foreach (DataGridViewColumn columnStyle in this.dataGridView1.Columns)
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
                        case "englevel":
                        {
                            columnStyle.HeaderText = "Name";
							columnStyle.Visible = true;
                            break;
                        }
                        case "changefrom":
                        {
                            columnStyle.HeaderText = "Стара стойност";
							columnStyle.Visible = true;
                            break;
                        }
                        case "changeto":
                        {
                            columnStyle.HeaderText = "Нова стойност";
							columnStyle.Visible = true;
                            break;
                        }
                        case "changeoperation":
                        {
                            columnStyle.HeaderText = "Операция";
							columnStyle.Visible = true;
                            break;
                        }
                        case "oldlevel":
                        {
                            columnStyle.HeaderText = "Старо име";
							columnStyle.Visible = true;
                            break;
                        }
                        case "oldcode":
                        {
                            columnStyle.HeaderText = "Стар код";
							columnStyle.Visible = true;
                            break;
                        }
						case "staff":
						{
							columnStyle.HeaderText = "Щат";
							columnStyle.Visible = true;
							break;
						}
						case "year":
						{
							columnStyle.HeaderText = "Година";
							columnStyle.Visible = true;
							break;
						}
						case "leftover":
						{
							columnStyle.HeaderText = "Остатък";
							columnStyle.Visible = true;
							break;
						}
						case "total":
						{
							columnStyle.HeaderText = "Полагаем";
							columnStyle.Visible = true;
							break;
						}
                        case "telk":
                        {
                            columnStyle.HeaderText = "ТЕЛК";
                            columnStyle.Visible = true;
                            break;
                        }
                        case "unpayed":
                        {
                            columnStyle.HeaderText = "Неплатен отпуск";
                            columnStyle.Visible = true;
                            break;
                        }
                        case "education":
                        {
                            columnStyle.HeaderText = "Полагаем отпуск обучение";
                            columnStyle.Visible = true;
                            break;
                        }
                        case "additional":
                        {
                            columnStyle.HeaderText = "Полагаем отпуск друг";
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
			catch(System.Exception ex)
			{
				MessageBox.Show("Some Error", ex.Message);
			}
		}

		private void CommonNomenclature_Load(object sender, EventArgs e)
		{
			try
			{
				this.dataGridView1.DataSource = this.dt;
				this.JustifyGrid();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
	}
}
