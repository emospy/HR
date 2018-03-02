using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HR
{
	/// <summary>
	/// Summary description for MinSalary.
	/// </summary>
//	public class FormMinSalary : System.Windows.Forms.Form
//	{
//		internal DataTable dt;
//		internal DataLayer.SalaryAction salaryAction;
//		#region Items
//		private System.Windows.Forms.DataGrid dataGrid1;
//		private System.Windows.Forms.Button buttonAdd;
//		private System.Windows.Forms.Button buttonEdit;
//		private System.Windows.Forms.Button buttonDelete;
//		private System.Windows.Forms.Button buttonCancel;
//		#endregion
//		/// <summary>
//		/// Required designer variable.
//		/// </summary>
//		private System.ComponentModel.Container components = null;
//
//		public FormMinSalary(mainForm main)
//		{
//			//
//			// Required for Windows Form Designer support
//			//
//			InitializeComponent();
//			salaryAction = new DataLayer.SalaryAction( "minsalary", main.connString);
//			
//		}
//
//		/// <summary>
//		/// Clean up any resources being used.
//		/// </summary>
//		protected override void Dispose( bool disposing )
//		{
//			if( disposing )
//			{
//				if(components != null)
//				{
//					components.Dispose();
//				}
//			}
//			base.Dispose( disposing );
//		}
//
//		#region Windows Form Designer generated code
//		/// <summary>
//		/// Required method for Designer support - do not modify
//		/// the contents of this method with the code editor.
//		/// </summary>
//		private void InitializeComponent()
//		{
//			this.dataGrid1 = new System.Windows.Forms.DataGrid();
//			this.buttonAdd = new System.Windows.Forms.Button();
//			this.buttonEdit = new System.Windows.Forms.Button();
//			this.buttonDelete = new System.Windows.Forms.Button();
//			this.buttonCancel = new System.Windows.Forms.Button();
//			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
//			this.SuspendLayout();
//			// 
//			// dataGrid1
//			// 
//			this.dataGrid1.DataMember = "";
//			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
//			this.dataGrid1.Location = new System.Drawing.Point(16, 8);
//			this.dataGrid1.Name = "dataGrid1";
//			this.dataGrid1.ReadOnly = true;
//			this.dataGrid1.Size = new System.Drawing.Size(488, 344);
//			this.dataGrid1.TabIndex = 0;
//			this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
//			// 
//			// buttonAdd
//			// 
//			this.buttonAdd.Location = new System.Drawing.Point(528, 72);
//			this.buttonAdd.Name = "buttonAdd";
//			this.buttonAdd.TabIndex = 1;
//			this.buttonAdd.Text = "Добавя";
//			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
//			// 
//			// buttonEdit
//			// 
//			this.buttonEdit.Location = new System.Drawing.Point(528, 128);
//			this.buttonEdit.Name = "buttonEdit";
//			this.buttonEdit.TabIndex = 2;
//			this.buttonEdit.Text = "Коригира";
//			this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
//			// 
//			// buttonDelete
//			// 
//			this.buttonDelete.Location = new System.Drawing.Point(528, 176);
//			this.buttonDelete.Name = "buttonDelete";
//			this.buttonDelete.TabIndex = 3;
//			this.buttonDelete.Text = "Изтрива";
//			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
//			// 
//			// buttonCancel
//			// 
//			this.buttonCancel.Location = new System.Drawing.Point(528, 224);
//			this.buttonCancel.Name = "buttonCancel";
//			this.buttonCancel.TabIndex = 4;
//			this.buttonCancel.Text = "Изход";
//			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
//			// 
//			// FormMinSalary
//			// 
//			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
//			this.ClientSize = new System.Drawing.Size(632, 365);
//			this.Controls.Add(this.buttonCancel);
//			this.Controls.Add(this.buttonDelete);
//			this.Controls.Add(this.buttonEdit);
//			this.Controls.Add(this.buttonAdd);
//			this.Controls.Add(this.dataGrid1);
//			this.Name = "FormMinSalary";
//			this.Text = "MinSalary";
//			this.Load += new System.EventHandler(this.FormMinSalary_Load);
//			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
//			this.ResumeLayout(false);
//
//		}
//		#endregion
//
//		private void buttonCancel_Click(object sender, System.EventArgs e)
//		{
//			this.Close();		
//		}		
//
//		private void FormMinSalary_Load(object sender, System.EventArgs e)
//		{
//			this.dt = this.salaryAction.SelectBasicData();
//			this.dt.PrimaryKey = new DataColumn[]{this.dt.Columns["code"]};
//			this.dataGrid1.DataSource = this.dt;
//			this.dt.TableName = "minsalary";
//			JustifyGrid(dataGrid1);		
//		}
//
//		private void buttonAdd_Click(object sender, System.EventArgs e)
//		{
//			FormAddMinSalary form = new FormAddMinSalary(this);
//			form.ShowDialog();		
//		}		
//
//		private void buttonEdit_Click(object sender, System.EventArgs e)
//		{
//			if( this.dataGrid1.VisibleRowCount > 0 )
//			{
//				FormAddMinSalary form = new FormAddMinSalary(this,(int) this.dt.Rows[dataGrid1.CurrentRowIndex]["id"]);
//				form.textBoxDWN.Text = this.dt.Rows[dataGrid1.CurrentRowIndex]["dw"].ToString();
//				form.textBoxPMS.Text = this.dt.Rows[dataGrid1.CurrentRowIndex]["PMS"].ToString();
//				form.dateTimePickerValidFrom.Text = this.dt.Rows[dataGrid1.CurrentRowIndex]["valid_from"].ToString();
//				form.numBoxSalary.Text = this.dt.Rows[dataGrid1.CurrentRowIndex]["minpermonth"].ToString();
//				form.ShowDialog();
//			}
//		}
//
//		
//		//Da se pogledne funkciqta za iztriwaneto
//		private void buttonDelete_Click(object sender, System.EventArgs e)
//		{
//			if( this.dataGrid1.VisibleRowCount >= 1 )
//			{
//				if( MessageBox.Show( this, "Сигурни ли сте че искате да изтриете заплатата " + this.dataGrid1[ this.dataGrid1.CurrentRowIndex, 2 ].ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//				{
//					this.salaryAction.DeleteRow( int.Parse(this.dataGrid1[ this.dataGrid1.CurrentRowIndex, 0 ].ToString()) );
//					
//				}
//			}
//		}	
//
//		private void dataGrid1_Click(object sender, System.EventArgs e)
//		{
//			if(dataGrid1.CurrentRowIndex == -1)
//				return;
//			dataGrid1.Select(dataGrid1.CurrentRowIndex); 			
//		}
//
//		private void JustifyGrid(DataGrid grid)
//		{
//			Graphics Graphics = grid.CreateGraphics();
//			DataGridTableStyle ts = new DataGridTableStyle();
//			try
//			{				
//				DataTable dataTable = (DataTable)grid.DataSource;
//                				
//				int	nRowsToScan = dataTable.Rows.Count;
//				
//				// Clear any existing table styles.
//				grid.TableStyles.Clear();
//
//				// Use mapping name that is defined in the data source.
//				ts.MappingName = dataTable.TableName;
//
//				// Now create the column styles within the table style.
//				DataGridTextBoxColumn columnStyle;
//				int iWidth;
//
//				for (int iCurrCol = 0; iCurrCol < dataTable.Columns.Count; iCurrCol++)
//				{
//					DataColumn dataColumn = dataTable.Columns[iCurrCol];
//
//					columnStyle = new DataGridTextBoxColumn();
//
//					columnStyle.TextBox.Enabled = false;
//
//					switch( dataColumn.ColumnName)
//					{										
//						case "dw":
//						{
//							columnStyle.HeaderText = "ДВ Бр Н";
//							columnStyle.MappingName = dataColumn.ColumnName;
//							break;
//						}
//						case "pms":
//						{
//							columnStyle.HeaderText = "ПМС"; 
//							columnStyle.MappingName = dataColumn.ColumnName;
//							break;
//						}
//						case "valid_from":
//						{
//							columnStyle.HeaderText = "Валидна от"; 
//							columnStyle.MappingName = dataColumn.ColumnName;
//							break;
//						}
//						case "minpermonth":
//						{
//							columnStyle.HeaderText = "Минимална"; 
//							columnStyle.MappingName = dataColumn.ColumnName;
//							break;
//						}						
//						default :
//						{
//							columnStyle.HeaderText = dataColumn.ColumnName; 
//							columnStyle.MappingName = dataColumn.ColumnName;
//							columnStyle.Width = 0; //скрива колоната
//
//							// Add the new column style to the table style.
//							ts.GridColumnStyles.Add(columnStyle);
//							continue;
//						}
//					}
//					
//					// Set width to header text width.
//					iWidth = (int)(Graphics.MeasureString
//						(columnStyle.HeaderText,
//						grid.Font).Width);
//
//					// Change width, if data width is
//					// wider than header text width.
//					// Check the width of the data in the first X rows.
//					DataRow dataRow;
//					for (int iRow = 0; iRow < nRowsToScan; iRow++)
//					{
//						dataRow = dataTable.Rows[iRow];
//
//						if (null != dataRow[dataColumn.ColumnName])
//						{
//							int iColWidth = (int)(Graphics.MeasureString
//								(dataRow.ItemArray[iCurrCol].ToString(),
//								grid.Font).Width);
//							iWidth = (int)System.Math.Max(iWidth, iColWidth);
//						}
//					}
//					columnStyle.Width = iWidth + 4;
//
//					// Add the new column style to the table style.
//					ts.GridColumnStyles.Add(columnStyle);
//				}
//				// Add the new table style to the data grid.
//				grid.TableStyles.Add(ts);
//			}
//			catch(System.Exception)
//			{
//				MessageBox.Show("Some Error");
//			}
//
//			finally
//			{
//				Graphics.Dispose();
//			}
//		}
//	}
}
