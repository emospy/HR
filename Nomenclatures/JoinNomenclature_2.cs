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
	public class JoinNomenklature2 : System.Windows.Forms.Form
	{
		private mainForm formmain;
		private DataTable dt;
		private string table;
		private DataAction da;
		private string descriptor;
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
		public JoinNomenklature2(string table, string formName, DataTable dtT, mainForm main, string descriptor)
		{
			try
			{
				this.descriptor = descriptor;
				this.formmain = main;				
				this.table = table;
				this.dt = dtT;
				this.da = new DataAction(this.formmain.connString);
				InitializeComponent();
				this.Text = formName;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JoinNomenklature));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView1.Location = new System.Drawing.Point(5, 8);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.Size = new System.Drawing.Size(883, 692);
			this.dataGridView1.TabIndex = 10;
			this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
			this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
			this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
			// 
			// JoinNomenklature
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(992, 706);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonEdit);
			this.Controls.Add(this.buttonAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "JoinNomenklature";
			this.ShowInTaskbar = false;
			this.Text = "CommonNomenclature";
			this.Load += new System.EventHandler(this.JoinNomenklature_Load);
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
					if (Col.Visible == true)
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

					Dict.Add("descriptor", this.descriptor);
					for (int i = 0; i < ray.Count; i++)
					{
						MappingFormData map = (MappingFormData)ray[i];
						Dict.Add(map.MappingName, map.ColumnText);						
						row[map.MappingName] = map.ColumnText;						
					}

					id = this.da.UniversalInsertParam(this.table, Dict, "id", TransactionComnmand.NO_TRANSACTION);
					if (id > 0)
					{
						row["id"] = id;
						this.dt.Rows.Add(row);
					}
					else
					{
						MessageBox.Show("Грешка при добавяне на номенклатура", ErrorMessages.NoConnection);
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Грешка при добавяне на номенклатура");
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
				if (this.dataGridView1.CurrentRow != null)
				{
					if (DialogResult.OK == MessageBox.Show(this, "Наистина ли искате да изтриете избраната номенклатура", "Въпрос", MessageBoxButtons.OKCancel))
					{
						if (this.da.UniversalDelete(this.table, this.dataGridView1.CurrentRow.Cells["id"].Value.ToString(), "id"))
						{
							DataRow row = dt.Rows.Find(this.dataGridView1.CurrentRow.Cells["id"].Value);
							if (row != null)
							{								
								dt.Rows.Remove(row);
							}
							else
								MessageBox.Show("Грешка при изтриване на номенклатура", "Грешка");
						}
						else
						{
							MessageBox.Show("Грешкал при изтриване на номенклатура", ErrorMessages.NoConnection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, "Грешка при изтриване на номенклатура");
				MessageBox.Show(ex.Message, "Грешка при изтриване на номенклатура");
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

				foreach (DataGridViewColumn Col in dataGridView1.Columns)
				{
					if (Col.Visible == true)
					{
						MappingFormData map = new MappingFormData();
						map.HeaderText = Col.HeaderText;
						map.MappingName = Col.Name;
						map.ColumnText = this.dataGridView1.CurrentRow.Cells[map.MappingName].Value.ToString();
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
						this.dataGridView1.CurrentRow.Cells[map.MappingName].Value = map.ColumnText;
					}

					if (this.da.UniversalUpdateParam(this.table, "id", Dict, this.dataGridView1.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.NO_TRANSACTION) == false)
					{
						MessageBox.Show("Грешка при редакция на номенклатура", ErrorMessages.NoConnection);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, "Грешка при редакция на номенклатура");
				MessageBox.Show(ex.Message, "Грешка при редакция на номенклатура");
			}
		}

		private void dataGridView1_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.CurrentRow == null)
				return;
			this.dataGridView1.CurrentRow.Selected = true;
		}

		private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			this.dataGridView1.CurrentRow.Selected = true;
		}

		private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
		{
			this.dataGridView1.CurrentRow.Selected = false;
		}

		private void JustifyGrid()
		{
			try
			{
				foreach (DataGridViewColumn columnStyle in this.dataGridView1.Columns)
				{
					switch (columnStyle.Name)
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
							columnStyle.HeaderText = "Коефициент";
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
			catch (System.Exception e)
			{
				MessageBox.Show("Грешка при оразмеряване", e.Message);
			}
		}	

		private void JoinNomenklature_Load(object sender, EventArgs e)
		{
			try
			{
				string where = "";
				if (descriptor != null && descriptor != "")
				{
					where = "where descriptor = '" + descriptor + "'";
				}
				this.dt = this.da.SelectWhere(this.table, "*", where);
				if (dt == null)
				{
					MessageBox.Show("Грешка при зареждаме на номенклатура " + this.descriptor, ErrorMessages.NoConnection);
					this.Close();
				}
				this.dt.PrimaryKey = new DataColumn[] { this.dt.Columns["id"] };
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
