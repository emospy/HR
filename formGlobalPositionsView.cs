using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DataLayer;
using System.Data;

namespace HR
{
	/// <summary>
	/// Summary description for formGlobalPositionsView.
	/// </summary>
	public class formGlobalPositionsView : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonEdit;
		private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public formGlobalPositionsView(mainForm main)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			
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
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonEdit = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(16, 16);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.Size = new System.Drawing.Size(576, 248);
			this.dataGrid1.TabIndex = 0;
			this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(56, 272);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.TabIndex = 1;
			this.buttonAdd.Text = "Добави";
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(192, 272);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.TabIndex = 2;
			this.buttonDelete.Text = "Изтрии";
			// 
			// buttonEdit
			// 
			this.buttonEdit.Location = new System.Drawing.Point(336, 272);
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.TabIndex = 3;
			this.buttonEdit.Text = "Коригирай";
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(488, 272);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Изход";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// formGlobalPositionsView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 309);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonEdit);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.dataGrid1);
			this.Name = "formGlobalPositionsView";
			this.Text = "formGlobalPositionsView";
			this.Load += new System.EventHandler(this.formGlobalPositionsView_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void formGlobalPositionsView_Load(object sender, System.EventArgs e)
		{
			
		}

		private void dataGrid1_Click(object sender, System.EventArgs e)
		{
			this.dataGrid1.Select( this.dataGrid1.CurrentRowIndex);
		}		
	}
}
