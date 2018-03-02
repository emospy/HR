using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HR
{
	/// <summary>
	/// Summary description for Calendar.
	/// </summary>
	public class Calendar : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MonthCalendar monthCalendar1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		mainForm main;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public Calendar( mainForm main )
		{
			//
			// Required for Windows Form Designer support
			//
			this.main = main;
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
			this.SuspendLayout();
			// 
			// monthCalendar1
			// 
			this.monthCalendar1.Location = new System.Drawing.Point(0, 0);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.TabIndex = 0;
			// 
			// Calendar
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 158);
			this.Controls.Add(this.monthCalendar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "Calendar";
			this.ShowInTaskbar = false;
			this.Text = "Календар";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Calendar_Closing);
			this.Load += new System.EventHandler(this.Calendar_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Calendar_Load(object sender, System.EventArgs e)
		{
			main.showInTaskBar  = false;
			//((mainForm)this.Parent).showInTaskBar = false;
		}

		private void Calendar_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			main.showInTaskBar  = true;
		}
	}
}
