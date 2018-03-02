using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HR
{
	public partial class MilitaryRangForm : Form
	{
		formPersonalData ParForm;
		public MilitaryRangForm(formPersonalData form)
		{
			this.ParForm = form;
			InitializeComponent();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;			
			this.Close();
		}

		private void dateTimePickerRangOrderDate_ValueChanged(object sender, EventArgs e)
		{
			//if(this.
		}
	}
}
