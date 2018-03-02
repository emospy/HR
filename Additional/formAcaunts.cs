using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using DataLayer;

namespace HR
{
	/// <summary>
	/// Summary description for formAcaunts.
	/// </summary>
	public class formUsers : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxTypeUser;
		private System.Windows.Forms.TextBox textBoxUserName;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.Button buttoAddUser;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.GroupBox groupBoxAddUser;
		private System.Windows.Forms.ComboBox comboBoxDeleteUser;
		private System.Windows.Forms.Button buttonDeleteUser;
		private System.Windows.Forms.GroupBox groupBoxDeleteUser;
		private System.Windows.Forms.Button buttonSaveExit;
		private System.ComponentModel.IContainer components;

		private mainForm main;
		/// <summary>
		/// Summary description for ExcelExpo.
		/// </summary>
		public formUsers( mainForm main)
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(formUsers));
			this.comboBoxTypeUser = new System.Windows.Forms.ComboBox();
			this.textBoxUserName = new System.Windows.Forms.TextBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.label = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.buttoAddUser = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBoxAddUser = new System.Windows.Forms.GroupBox();
			this.comboBoxDeleteUser = new System.Windows.Forms.ComboBox();
			this.buttonDeleteUser = new System.Windows.Forms.Button();
			this.groupBoxDeleteUser = new System.Windows.Forms.GroupBox();
			this.buttonSaveExit = new System.Windows.Forms.Button();
			this.groupBoxAddUser.SuspendLayout();
			this.groupBoxDeleteUser.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxTypeUser
			// 
			this.comboBoxTypeUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTypeUser.Items.AddRange(new object[] {
																  "Администратор",
																  "Допълване",
																  "Разглеждане"});
			this.comboBoxTypeUser.Location = new System.Drawing.Point(16, 88);
			this.comboBoxTypeUser.Name = "comboBoxTypeUser";
			this.comboBoxTypeUser.Size = new System.Drawing.Size(136, 21);
			this.comboBoxTypeUser.TabIndex = 2;
			this.toolTip1.SetToolTip(this.comboBoxTypeUser, "От тук се избира какви права ще има потребителя който добавята. Например ако е от" +
				" тип администратор той ще може да прави всякакви промени");
			// 
			// textBoxUserName
			// 
			this.textBoxUserName.Location = new System.Drawing.Point(16, 40);
			this.textBoxUserName.Name = "textBoxUserName";
			this.textBoxUserName.Size = new System.Drawing.Size(136, 20);
			this.textBoxUserName.TabIndex = 0;
			this.textBoxUserName.Text = "";
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Location = new System.Drawing.Point(168, 40);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '*';
			this.textBoxPassword.Size = new System.Drawing.Size(136, 20);
			this.textBoxPassword.TabIndex = 1;
			this.textBoxPassword.Text = "";
			// 
			// label
			// 
			this.label.Location = new System.Drawing.Point(16, 64);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(152, 23);
			this.label.TabIndex = 4;
			this.label.Text = "Изберете типа потребител";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 6;
			this.label2.Text = "Потребителско име";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(168, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 23);
			this.label3.TabIndex = 1;
			this.label3.Text = "Потребителска парола";
			// 
			// buttoAddUser
			// 
			this.buttoAddUser.Image = ((System.Drawing.Image)(resources.GetObject("buttoAddUser.Image")));
			this.buttoAddUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttoAddUser.Location = new System.Drawing.Point(168, 88);
			this.buttoAddUser.Name = "buttoAddUser";
			this.buttoAddUser.Size = new System.Drawing.Size(136, 23);
			this.buttoAddUser.TabIndex = 3;
			this.buttoAddUser.Text = "   Добави потребител";
			this.buttoAddUser.Click += new System.EventHandler(this.buttoAddUser_Click);
			// 
			// groupBoxAddUser
			// 
			this.groupBoxAddUser.Controls.Add(this.textBoxUserName);
			this.groupBoxAddUser.Controls.Add(this.label2);
			this.groupBoxAddUser.Controls.Add(this.textBoxPassword);
			this.groupBoxAddUser.Controls.Add(this.label3);
			this.groupBoxAddUser.Controls.Add(this.buttoAddUser);
			this.groupBoxAddUser.Controls.Add(this.label);
			this.groupBoxAddUser.Controls.Add(this.comboBoxTypeUser);
			this.groupBoxAddUser.Location = new System.Drawing.Point(16, 8);
			this.groupBoxAddUser.Name = "groupBoxAddUser";
			this.groupBoxAddUser.Size = new System.Drawing.Size(320, 120);
			this.groupBoxAddUser.TabIndex = 1;
			this.groupBoxAddUser.TabStop = false;
			this.groupBoxAddUser.Text = "Добавяне на потребител";
			// 
			// comboBoxDeleteUser
			// 
			this.comboBoxDeleteUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDeleteUser.Location = new System.Drawing.Point(16, 24);
			this.comboBoxDeleteUser.Name = "comboBoxDeleteUser";
			this.comboBoxDeleteUser.Size = new System.Drawing.Size(136, 21);
			this.comboBoxDeleteUser.TabIndex = 0;
			// 
			// buttonDeleteUser
			// 
			this.buttonDeleteUser.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteUser.Image")));
			this.buttonDeleteUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonDeleteUser.Location = new System.Drawing.Point(168, 24);
			this.buttonDeleteUser.Name = "buttonDeleteUser";
			this.buttonDeleteUser.Size = new System.Drawing.Size(136, 23);
			this.buttonDeleteUser.TabIndex = 1;
			this.buttonDeleteUser.Text = "    Изтрий потребител";
			this.buttonDeleteUser.Click += new System.EventHandler(this.buttonDeleteUser_Click);
			// 
			// groupBoxDeleteUser
			// 
			this.groupBoxDeleteUser.Controls.Add(this.buttonDeleteUser);
			this.groupBoxDeleteUser.Controls.Add(this.comboBoxDeleteUser);
			this.groupBoxDeleteUser.Location = new System.Drawing.Point(16, 136);
			this.groupBoxDeleteUser.Name = "groupBoxDeleteUser";
			this.groupBoxDeleteUser.Size = new System.Drawing.Size(320, 56);
			this.groupBoxDeleteUser.TabIndex = 2;
			this.groupBoxDeleteUser.TabStop = false;
			this.groupBoxDeleteUser.Text = "Изтриване на потребители";
			// 
			// buttonSaveExit
			// 
			this.buttonSaveExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveExit.Image")));
			this.buttonSaveExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSaveExit.Location = new System.Drawing.Point(104, 200);
			this.buttonSaveExit.Name = "buttonSaveExit";
			this.buttonSaveExit.Size = new System.Drawing.Size(144, 24);
			this.buttonSaveExit.TabIndex = 11;
			this.buttonSaveExit.Text = "Запомни и затвори";
			this.buttonSaveExit.Click += new System.EventHandler(this.buttonSaveExit_Click);
			// 
			// formUsers
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 230);
			this.Controls.Add(this.buttonSaveExit);
			this.Controls.Add(this.groupBoxDeleteUser);
			this.Controls.Add(this.groupBoxAddUser);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(360, 264);
			this.MinimumSize = new System.Drawing.Size(360, 264);
			this.Name = "formUsers";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Потребители";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.formUsers_Closing);
			this.Load += new System.EventHandler(this.formUsers_Load);
			this.groupBoxAddUser.ResumeLayout(false);
			this.groupBoxDeleteUser.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSaveExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void formUsers_Load(object sender, System.EventArgs e)
		{
			foreach( DataRow row in this.main.dtUsers.Rows )
			{
				this.comboBoxDeleteUser.Items.Add( row[0].ToString() );
			}
		}

		private void buttonDeleteUser_Click(object sender, System.EventArgs e)
		{
			if( this.comboBoxDeleteUser.SelectedIndex != -1 )
			{
				if( DialogResult.Yes == MessageBox.Show( this, "Сигурни ли сте че искате да изтриете избрания потребител?", "Въпрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 ) )
				{
					if (this.main.userAction.UniversalDelete("users", this.comboBoxDeleteUser.SelectedItem.ToString(), "Username"))
					{
						foreach (DataRow row in this.main.dtUsers.Rows)
						{
							if (row[0].ToString() == this.comboBoxDeleteUser.SelectedItem.ToString())
							{
								this.main.dtUsers.Rows.Remove(row);
								break;
							}
						}
						this.comboBoxDeleteUser.Items.Remove(this.comboBoxDeleteUser.SelectedItem);
					}
					else
					{
						MessageBox.Show("Грешка при изтриване на потребител", ErrorMessages.NoConnection);
					}
				}
			}
		}

		private void buttoAddUser_Click(object sender, System.EventArgs e)
		{
			int insres = -1;
			if( this.comboBoxTypeUser.SelectedIndex != -1 &&
				this.textBoxPassword.Text != "" && this.textBoxUserName.Text != "" )
			{
				foreach( string str in this.comboBoxDeleteUser.Items )
				{
					if( this.textBoxUserName.Text == str )
					{
						MessageBox.Show( "Има потребител с такова име. Изберете друго!" );
						return;
					}
				}
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				Dict.Add("UserName", this.textBoxUserName.Text);
				Dict.Add("Password", this.textBoxPassword.Text);
				Dict.Add("TypeUser", this.comboBoxTypeUser.SelectedItem.ToString());

				insres = this.main.userAction.UniversalInsertParam(TableNames.Users, Dict, "id", TransactionComnmand.NO_TRANSACTION);

				if( insres == 0)
				{
					this.main.ErrorInDataBase( "Опитвате се да вече съществуващ потребител" );
				}
				else if (insres < 0)
				{
					MessageBox.Show("Грешка при добавяне на потребител", ErrorMessages.NoConnection);
				}
				else
				{
					this.main.dtUsers.Rows.Add( new object[]{this.textBoxUserName.Text, this.textBoxPassword.Text, this.comboBoxTypeUser.SelectedItem.ToString() } );
					this.comboBoxDeleteUser.Items.Add( this.textBoxUserName.Text );
				}
			}
		}

		private void formUsers_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			
			if( this.comboBoxDeleteUser.Items.Count == 0 )
			{
				MessageBox.Show( "Всички потребители са изтрити! Трябва да добавите поне един за да може след това пак да влезнете в програмата!" );
				e.Cancel = true;
			}
		}
	}
}
