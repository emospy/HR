using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HR
{
	/// <summary>
	/// Summary description for CommonNomenclatureAdd.
	/// </summary>
	public class CommonNomenclatureAdd : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private ArrayList arrTextBoxes;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private ArrayList arrLabels;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private ArrayList maps;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// Constructor
		/// </summary>
		public CommonNomenclatureAdd(ArrayList Maps)
		{
			try
			{
				int i;
				this.maps = Maps;
				//
				// Required for Windows Form Designer support
				//			
				InitializeComponent();
				this.arrTextBoxes = new ArrayList();
				this.arrLabels = new ArrayList();
				this.Height = 55 * Maps.Count;
				for (i = 0; i < Maps.Count; i ++)
				{
					MappingFormData map = (MappingFormData)Maps[i];
					Label lab = new Label();
					lab.Location = new Point( 8, 8 + i*40);
					lab.Size = new Size(400, 16);
					lab.Text = map.HeaderText + ":";
					this.arrLabels.Add(lab);
					
					TextBox tex = new TextBox();
					tex.Location = new Point( 8, i*40 + 24);
					tex.Size = new Size(476,20);
					tex.Text = map.ColumnText;
					tex.TabIndex = i;
					this.arrTextBoxes.Add(tex);
					this.Controls.Add(lab);
					this.Controls.Add(tex);				
				}
				this.buttonSave.TabIndex = i;
				this.buttonSave.Location = new Point(106, (Maps.Count ) * 40 + 16);
				this.buttonCancel.TabIndex = i + 1;
				this.buttonCancel.Location = new Point(302, (Maps.Count ) * 40 + 16); 
				this.Size = new Size(500, (Maps.Count + 1) * 40 + 45);		
			}
			catch (Exception ex)
			{
				DataLayer.ErrorLog.WriteException(ex, ex.Message);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonNomenclatureAdd));
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonSave
			// 
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSave.Location = new System.Drawing.Point(266, 40);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(130, 23);
			this.buttonSave.TabIndex = 0;
			this.buttonSave.Text = "Запис";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(96, 40);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Откажи";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// CommonNomenclatureAdd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(492, 69);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CommonNomenclatureAdd";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public void SetVariables(string [] vars)
		{
			try
			{
				for(int i = 0; i < vars.Length; i++)
				{
					TextBox tex = (TextBox) this.arrTextBoxes[i];
					tex.Text = vars[i];
				}
			}
			catch (Exception ex)
			{
				DataLayer.ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Required designaer variable.
		/// </summary>
		public ArrayList GetVariables()
		{
			ArrayList retval = new ArrayList();
			try
			{
				for (int i = 0; i < arrTextBoxes.Count; i++)
				{
					MappingFormData map;
					TextBox tex = (TextBox)this.arrTextBoxes[i];
					map = (MappingFormData)this.maps[i];
					map.ColumnText = tex.Text;
					retval.Add(map);
				}
			}
			catch (Exception ex)
			{
				DataLayer.ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
			return retval;
		}
		/// <summary>
		/// Required designaer variable.
		/// </summary>
		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		/// <summary>
		/// Required designaer variable.
		/// </summary>
		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
