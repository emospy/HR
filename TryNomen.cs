using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace LichenSystaw2004
{
	/// <summary>
	/// Summary description for TryNomen.
	/// </summary>
	public class TryNomen : System.Windows.Forms.Form
	{
		private DataView vueDirection, vueDepartment, vueSector;
		private mainForm main;
		private DataViewRowState dvrs;
		private ArrayList arrDirection;
		private ArrayList arrDepartment;
		private ArrayList arrSector;
		private DataTable dtTree;
		private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.ComboBox comboBoxDirection;
		private System.Windows.Forms.ComboBox comboBoxDepartment;
		private System.Windows.Forms.ComboBox comboBoxSector;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TryNomen(mainForm main)
		{			
			InitializeComponent();
			this.main = main;
			dtTree = main.nomenclaatureData.TreeTable;
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
			this.comboBoxDirection = new System.Windows.Forms.ComboBox();
			this.comboBoxDepartment = new System.Windows.Forms.ComboBox();
			this.comboBoxSector = new System.Windows.Forms.ComboBox();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// comboBoxDirection
			// 
			this.comboBoxDirection.Location = new System.Drawing.Point(40, 56);
			this.comboBoxDirection.Name = "comboBoxDirection";
			this.comboBoxDirection.Size = new System.Drawing.Size(121, 21);
			this.comboBoxDirection.TabIndex = 0;
			this.comboBoxDirection.SelectedIndexChanged += new System.EventHandler(this.comboBoxDirection_SelectedIndexChanged);
			// 
			// comboBoxDepartment
			// 
			this.comboBoxDepartment.Location = new System.Drawing.Point(56, 120);
			this.comboBoxDepartment.Name = "comboBoxDepartment";
			this.comboBoxDepartment.Size = new System.Drawing.Size(121, 21);
			this.comboBoxDepartment.TabIndex = 1;
			this.comboBoxDepartment.SelectedIndexChanged += new System.EventHandler(this.comboBoxDepartment_SelectedIndexChanged);
			// 
			// comboBoxSector
			// 
			this.comboBoxSector.Location = new System.Drawing.Point(80, 168);
			this.comboBoxSector.Name = "comboBoxSector";
			this.comboBoxSector.Size = new System.Drawing.Size(121, 21);
			this.comboBoxSector.TabIndex = 2;
			// 
			// comboBox4
			// 
			this.comboBox4.Location = new System.Drawing.Point(72, 224);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(121, 21);
			this.comboBox4.TabIndex = 3;
			// 
			// TryNomen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.comboBox4);
			this.Controls.Add(this.comboBoxSector);
			this.Controls.Add(this.comboBoxDepartment);
			this.Controls.Add(this.comboBoxDirection);
			this.Name = "TryNomen";
			this.Text = "TryNomen";
			this.Load += new System.EventHandler(this.TryNomen_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TryNomen_Load(object sender, System.EventArgs e)
		{
			dvrs = DataViewRowState.CurrentRows;
			vueDirection = new DataView(dtTree, "par = 0", "level", dvrs);

			this.arrDirection = new ArrayList();
			this.arrDirection.Add("");
			this.arrDepartment = new ArrayList();
			this.arrDepartment.Add("");
			this.arrSector = new ArrayList();
			this.arrSector.Add("");
			

			for(int i = 0; i < vueDirection.Count; i++)
			{
				arrDirection.Add(vueDirection[i]["level"]);
			}
			this.comboBoxDirection.DataSource = arrDirection;
		}

		private void comboBoxDirection_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.comboBoxDepartment.Items.Clear();
			this.comboBoxDepartment.Text = "";
			this.comboBoxDepartment.Items.Add("");
			this.comboBoxSector.Items.Clear();
			this.comboBoxSector.Text = "";
			this.comboBoxSector.Items.Add("");

			if(this.comboBoxDirection.SelectedIndex > 0)
			{
				string cond = "par = " + this.vueDirection[this.comboBoxDirection.SelectedIndex - 1]["id"].ToString();

				vueDepartment = new DataView(dtTree, cond, "level", dvrs);

				for(int i = 0; i < vueDepartment.Count; i++)
				{
					this.comboBoxDepartment.Items.Add(vueDepartment[i]["level"]);
				}
			}			
		}

		private void comboBoxDepartment_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			this.comboBoxSector.Items.Clear();
			this.comboBoxSector.Text = "";
			this.comboBoxSector.Items.Add("");

			if(this.comboBoxDepartment.SelectedIndex > 0)
			{
				string cond = "par = " + this.vueDepartment[this.comboBoxDepartment.SelectedIndex - 1]["id"].ToString();

				vueSector = new DataView(dtTree, cond, "level", dvrs);

				for(int i = 0; i < vueSector.Count; i++)
				{
					this.comboBoxSector.Items.Add(vueSector[i]["level"]);
				}
			}		
		}		
	}
}
