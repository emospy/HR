using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace HR
{
	/// <summary>
	/// Summary description for FormChoose.
	/// </summary>
	public class FormChoose : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonChoose;
		private System.Windows.Forms.Button buttonCancel;
		private string TableName;
		
		/// <summary>
		/// Property that return selected row
		/// </summary>

		public DataGridView dataGridView1;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public FormChoose(DataTable dt, string Caption)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.dataGridView1.DataSource = dt;
			this.TableName = dt.TableName;
			this.dataGridView1.ClearSelection();
			JustifyGridView(this.dataGridView1, this.TableName);
			this.Text += Caption;
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public FormChoose(DataView dv, string Caption)
		{
			InitializeComponent();

			this.dataGridView1.DataSource = dv;
			this.dataGridView1.ClearSelection();
			this.TableName = dv.Table.TableName;
			JustifyGridView(this.dataGridView1, this.TableName);
			this.Text += Caption;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChoose));
			this.buttonChoose = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonChoose
			// 
			this.buttonChoose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonChoose.Image = ((System.Drawing.Image)(resources.GetObject("buttonChoose.Image")));
			this.buttonChoose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonChoose.Location = new System.Drawing.Point(318, 671);
			this.buttonChoose.Name = "buttonChoose";
			this.buttonChoose.Size = new System.Drawing.Size(150, 23);
			this.buttonChoose.TabIndex = 1;
			this.buttonChoose.Text = "   Избери";
			this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(524, 671);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(150, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "   Изход";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
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
			this.dataGridView1.Size = new System.Drawing.Size(981, 650);
			this.dataGridView1.TabIndex = 3;
			// 
			// FormChoose
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(992, 706);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonChoose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormChoose";
			this.ShowInTaskbar = false;
			this.Text = "Избор на ";
			this.Resize += new System.EventHandler(this.FormChoose_Resize);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void buttonChoose_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}


		private void JustifyGridView(DataGridView dgv, string tableName)
		{
			try
			{
				switch (tableName)
				{
					case "GlobalPositions":			
						foreach (DataGridViewColumn columnStyle in dgv.Columns)
						{
							switch (columnStyle.Name.ToLower())
							{
								case "positionname":
								{
									columnStyle.HeaderText = "Длъжност"; 
									columnStyle.Visible = true;
									break;
								}						
								case "ekdacode":
								{
									columnStyle.HeaderText = "Длъжностно ниво";
									columnStyle.Visible = true;
									break;
								}						
								case "nkpcode":
								{
									columnStyle.HeaderText = "Код по НКПД";
									columnStyle.Visible = true;
									break;
								}
								case "nkplevel":
								{
									columnStyle.HeaderText = "Длъжност по НКПД";
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
								case "minsalary":
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
						break;	
					case "Positions":
					{
						foreach (DataGridViewColumn columnStyle in dgv.Columns)
						{
							switch( columnStyle.Name.ToLower())
							{	
								case "typeposition":
								{
									columnStyle.HeaderText = "Вид длъжност";
									columnStyle.Visible = true;
									break;
								}										
								case "education":
								{
									columnStyle.HeaderText = "Образование";
									columnStyle.Visible = true;
									break;
								}
								case "profession":
								{
									columnStyle.HeaderText = "Професия";
									columnStyle.Visible = true;
									break;
								}
								case "name":
								{
									columnStyle.HeaderText = "Име";
									columnStyle.Visible = true;
									break;
								}
								case "position":
								{
									columnStyle.HeaderText = "Длъжност";
									columnStyle.Visible = true;
									break;
								}
								case "contract":
								{
									columnStyle.HeaderText = "Договор"; 
									columnStyle.Visible = true;
									break;
								}
								case "nameofposition":
								{
									columnStyle.HeaderText = "Длъжност";
									columnStyle.Visible = true;
									break;
								}
								case "nkpcode":
								{
									columnStyle.HeaderText = "Код по НКПД";
									columnStyle.Visible = true;
									break;
								}
								case "experience":
								{
									columnStyle.HeaderText = "Опит";
									columnStyle.Visible = true;
									break;
								}
								case "law":
								{
									columnStyle.HeaderText = "Правоотношение";
									columnStyle.Visible = true;
									break;
								}
								case "staffcount":
								{
									columnStyle.HeaderText = "Щатна бройка";
									columnStyle.Visible = true;
									break;
								}
								case "nummonths":
								{
									columnStyle.HeaderText = "Брой месеци";
									columnStyle.Visible = true;
									break;
								}
								case "free":
								{
									columnStyle.HeaderText = "Свободни";
									columnStyle.Visible = true;
									break;
								}
								case "busy":
								{
									columnStyle.HeaderText = "Заети";
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
						break;
					}
				}
			}			
			catch(System.Exception)
			{
				MessageBox.Show("Some Error");
			}
		}

        private void FormChoose_Resize(object sender, EventArgs e)
        {
            int location = this.Size.Width;
            this.buttonChoose.Left = location / 2 - 50;
            this.buttonCancel.Left = location / 2 + 50;
        }
	}
}
