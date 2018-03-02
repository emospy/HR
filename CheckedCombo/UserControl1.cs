using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace CheckedCombo
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class CheckedCombo : UserControl
	{
		// Fields
		private CheckBox checkBox1;
		private string column;
		private ComboBox comboBox1;
		private Container components = null;
		private CheckBox ckAll;
		private string data;

		public bool IsAllChecked
		{
			get { return this.ckAll.Checked; }
		}

		public bool IsInverted
		{
			get { return false; }
		}
		// Methods
		public CheckedCombo()
		{
			this.InitializeComponent();
			this.comboBox1.Enabled = this.Checked;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.comboBox1.Enabled = this.checkBox1.Checked;
			this.ckAll.Enabled = this.checkBox1.Checked;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.ckAll = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(0, 1);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.checkBox1.Size = new System.Drawing.Size(176, 21);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownWidth = 160;
			this.comboBox1.Enabled = false;
			this.comboBox1.Location = new System.Drawing.Point(182, 1);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(212, 21);
			this.comboBox1.TabIndex = 1;
			// 
			// ckAll
			// 
			this.ckAll.Enabled = false;
			this.ckAll.Location = new System.Drawing.Point(400, 1);
			this.ckAll.Name = "ckAll";
			this.ckAll.Size = new System.Drawing.Size(49, 21);
			this.ckAll.TabIndex = 2;
			this.ckAll.Text = "Вс.";
			this.ckAll.UseVisualStyleBackColor = true;
			// 
			// CheckedCombo
			// 
			this.Controls.Add(this.ckAll);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.checkBox1);
			this.Name = "CheckedCombo";
			this.Size = new System.Drawing.Size(450, 23);
			this.ResumeLayout(false);

		}

		// Properties
		public bool Checked
		{
			get
			{
				return this.checkBox1.Checked;
			}
			set
			{
				this.checkBox1.Checked = value;
			}
		}

		public string Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		public ComboBox combobox
		{
			get
			{
				return this.comboBox1;
			}
			set
			{
				this.comboBox1 = value;
			}
		}

		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		public string TextCombo
		{
			get
			{
				return this.checkBox1.Text;
			}
			set
			{
				this.checkBox1.Text = value;
			}
		}

		public int DropDownWidth
		{
			get
			{
				return this.comboBox1.DropDownWidth;
			}
			set
			{
				this.comboBox1.DropDownWidth = value;
			}
		}

        public int SelectedIndex
		{
			get
			{
				return this.comboBox1.SelectedIndex;
			}
			set
			{
				this.comboBox1.SelectedIndex = value;
			}
		}
	}
}
