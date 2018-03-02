using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataLayer;
using System.Diagnostics;

namespace HR
{
	/// <summary>
	/// Form for attached documents
	/// </summary>
	public partial class FormAttached : Form
	{
		DataAction da;
		private string TypeDocument;
		private string TableName;
		private DataTable dtDocs;
		private string Par;
		/// <summary>
		/// Form for attached documents ctor
		/// </summary>
		public FormAttached(string table, string type, string parent, string connstring)
		{
			InitializeComponent();
			this.Par = parent;
			this.TableName = table;
			this.TypeDocument = type;
			da = new DataAction(connstring);
		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FormAttached_Load(object sender, EventArgs e)
		{
			try
			{
				string whereStatement = string.Format("WHERE parent = {0}", this.Par);
				if (this.TypeDocument != "")
				{
					whereStatement += string.Format(" and typedocument = {0}", this.TypeDocument);
				}
				this.dtDocs = this.da.SelectWhere(this.TableName, "*", whereStatement);
				if (this.dtDocs == null)
					this.Close();
				this.dataGridView1.DataSource = this.dtDocs;
				this.JustifyGrid();
				this.dataGridView1.ClearSelection();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void JustifyGrid()
		{
			try
			{
				foreach (DataGridViewColumn columnStyle in this.dataGridView1.Columns)
				{
					switch (columnStyle.Name)
					{
						case "link":
							columnStyle.HeaderText = "Път към документа";
							columnStyle.Visible = true;
							break;
						case "typedocument":
							columnStyle.HeaderText = "Група документи";
							columnStyle.Visible = true;
							break;
						case "dateadded":
							columnStyle.HeaderText = "Name";
							columnStyle.Visible = true;
							break;
						default:
							columnStyle.Visible = false;
							break;
					}
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("Грешка при оразмеряване", ex.Message);
				ErrorLog.WriteException(ex, "Грешка при оразмеряване");
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			try
			{
				this.openFileDialog1 = new OpenFileDialog();
				this.openFileDialog1.Multiselect = true;
				if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					Dictionary <string, object> Err = new Dictionary<string,object>();
					int i = 0;
					foreach (string s in this.openFileDialog1.FileNames)
					{
						Dictionary <string, object> Dict = new Dictionary<string,object>();
						Dict.Add("parent", this.Par);
						Dict.Add("link", s);
						Dict.Add("dateadded", DateTime.Now);
						Dict.Add("typedocument", this.TypeDocument);

						if (this.da.UniversalInsertParam(TableNames.AttachedDocuments, Dict, "id", TransactionComnmand.NO_TRANSACTION) == -1)
						{
							Err.Add(i.ToString(), Dict["link"] + " не е добавен успешно.");
							i++;
						}
					}
					if (Err.Count > 0)
					{
						string message = "";
						foreach(KeyValuePair<string, object> kvp in Err)
						{
							message += kvp.Key + "\n";
						}
						MessageBox.Show(message, "Грешка при добавяне на документ");
					}
					this.FormAttached_Load(sender, e);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonExit_Click_1(object sender, EventArgs e)
		{
			this.Close();
		}

		private void buttonEdit_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridView1.CurrentRow != null)
				{
					Process.Start(this.dataGridView1.CurrentRow.Cells["link"].Value.ToString());
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridView1.CurrentRow != null)
				{
					if (DialogResult.OK == MessageBox.Show(this, "Наистина ли искате да изтриете избраната връзка?", "Въпрос", MessageBoxButtons.OKCancel))
					{
						if (this.da.UniversalDelete(this.TableName, this.dataGridView1.CurrentRow.Cells["id"].Value.ToString(), "id"))
						{
							this.FormAttached_Load(sender, e); 
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
	}
}
