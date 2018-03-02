using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace HR
{
	/// <summary>
	/// Summary description for LogIn.
	/// </summary>
	public class formLogIn : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonLogIn;
		private System.Windows.Forms.Label labelUserName;
		private System.Windows.Forms.Label labelPassword;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxUserName;
		private System.Windows.Forms.TextBox textBoxPassword;

	    int count = 0;
		bool IsProgramStarted = false;
		bool IsLogin = false;
		private System.ComponentModel.Container components = null;
        private mainForm main;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formLogIn( mainForm main, bool IsStarted )
		{

			IsProgramStarted = IsStarted;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(formLogIn));
			this.textBoxUserName = new System.Windows.Forms.TextBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.buttonLogIn = new System.Windows.Forms.Button();
			this.labelUserName = new System.Windows.Forms.Label();
			this.labelPassword = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxUserName
			// 
			this.textBoxUserName.Location = new System.Drawing.Point(128, 16);
			this.textBoxUserName.Name = "textBoxUserName";
			this.textBoxUserName.Size = new System.Drawing.Size(128, 20);
			this.textBoxUserName.TabIndex = 0;
			this.textBoxUserName.Text = "";
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Location = new System.Drawing.Point(128, 48);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '*';
			this.textBoxPassword.Size = new System.Drawing.Size(128, 20);
			this.textBoxPassword.TabIndex = 1;
			this.textBoxPassword.Text = "";
			// 
			// buttonLogIn
			// 
			this.buttonLogIn.Image = ((System.Drawing.Image)(resources.GetObject("buttonLogIn.Image")));
			this.buttonLogIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonLogIn.Location = new System.Drawing.Point(88, 80);
			this.buttonLogIn.Name = "buttonLogIn";
			this.buttonLogIn.Size = new System.Drawing.Size(90, 23);
			this.buttonLogIn.TabIndex = 2;
			this.buttonLogIn.Text = "Влез";
			this.buttonLogIn.Click += new System.EventHandler(this.buttonLogIn_Click);
			// 
			// labelUserName
			// 
			this.labelUserName.Location = new System.Drawing.Point(8, 16);
			this.labelUserName.Name = "labelUserName";
			this.labelUserName.Size = new System.Drawing.Size(112, 16);
			this.labelUserName.TabIndex = 3;
			this.labelUserName.Text = "Потребителско име:";
			// 
			// labelPassword
			// 
			this.labelPassword.Location = new System.Drawing.Point(72, 48);
			this.labelPassword.Name = "labelPassword";
			this.labelPassword.Size = new System.Drawing.Size(56, 16);
			this.labelPassword.TabIndex = 4;
			this.labelPassword.Text = "Парола:";
			// 
			// formLogIn
			// 
			this.AcceptButton = this.buttonLogIn;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(272, 118);
			this.Controls.Add(this.labelPassword);
			this.Controls.Add(this.labelUserName);
			this.Controls.Add(this.buttonLogIn);
			this.Controls.Add(this.textBoxPassword);
			this.Controls.Add(this.textBoxUserName);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(280, 152);
			this.MinimumSize = new System.Drawing.Size(280, 152);
			this.Name = "formLogIn";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Отключи";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.formLogIn_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonLogIn_Click(object sender, System.EventArgs e)
		{
			string user = this.textBoxUserName.Text; 
			string password = this.textBoxPassword.Text;
		    try
		    {


		        bool found = false;
		        DataRow foundRow = null;
            //DataRow row = this.main.dtUsers.Rows.Find( user );
		        foreach (DataRow row in this.main.dtUsers.Rows)
		        {
		          if(user == row["userName"].ToString() && password == row["password"].ToString() )
		          {
		              found = true;
		              foundRow = row;
		              break;
		          }
		        }
			if( found == false )
			{
				MessageBox.Show( "Грешна парола или потрбителско име!" );
			    
				if( this.count > 2 )
				{
					Application.Exit();
				}
				this.count++;
			}
			else
			{
				this.IsLogin = true;
				this.main.typeUser = foundRow["typeUser"].ToString();
                this.main.User = foundRow["userName"].ToString();
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message);
            }
		}

		private void formLogIn_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if( !this.IsProgramStarted )
			{
				if( !this.IsLogin )
				{
					Application.Exit();
				}
			}
		}
	}
}
