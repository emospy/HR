using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace HR
{
	/// <summary>
	/// Summary description for formFind.
	/// </summary>
	public class formFind : System.Windows.Forms.Form
	{		
		private DataTable dt;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;
        private DataGridView grid;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.TabControl tabControlSearch;
		private System.Windows.Forms.CheckBox checkBox1;
		private mainForm main;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formFind( DataGridView grid, mainForm main)
		{
			this.main = main;
			this.grid = grid;
            try
            {
                this.dt = (DataTable)grid.DataSource;
            }
            catch (System.InvalidCastException)
            {
                this.dt = ((DataView)(grid.DataSource)).Table;
            }
			
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
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonSearch = new System.Windows.Forms.Button();
			this.tabControlSearch = new System.Windows.Forms.TabControl();					
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(210, 100);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.TabIndex = 10;
			this.buttonSearch.Text = "Търси";
			this.buttonSearch.Click += new EventHandler(buttonSearch_Click);
			// 
			// tabControlSearch
			// 
			this.tabControlSearch.Location = new System.Drawing.Point(0, 8);
			this.tabControlSearch.Name = "tabControlSearch";
			this.tabControlSearch.SelectedIndex = 0;
			this.tabControlSearch.Size = new System.Drawing.Size(500, 88);
			this.tabControlSearch.TabIndex = 11;
			//
			// Tab Pages
			//
            //int page = 0;
            for (int i = 0, page = 0; i < this.grid.Columns.Count; i++)
            {
                if (this.grid.Columns[i].Visible)
                {
                    page++;
                    System.Windows.Forms.TabPage tabPage = new TabPage(this.grid.Columns[i].HeaderText);

                    System.Windows.Forms.TextBox textBox = new TextBox();
                    System.Windows.Forms.Label label = new Label();

                    tabPage.Controls.Add(textBox);
                    tabPage.Controls.Add(label);
                    tabPage.Location = new System.Drawing.Point(4, 22);
                    tabPage.Name = "tabPage" + page;
                    tabPage.Tag = i;
                    tabPage.Size = new System.Drawing.Size(496, 86);
                    tabPage.TabIndex = page++;

                    textBox.Location = new System.Drawing.Point(16, 40);
                    textBox.Name = "textBox" + page;
                    textBox.Size = new System.Drawing.Size(460, 20);
                    textBox.TabIndex = 10;
                    textBox.Text = "";
                    //toolTip1.SetToolTip(this.textBoxName, "Моля въведете " + ts.GridColumnStyles[i].HeaderText);

                    label.Location = new System.Drawing.Point(48, 16);
                    label.Name = "label" + page;
                    label.Size = new System.Drawing.Size(400, 16);
                    label.TabIndex = 11;
                    label.Text = "Търесене по " + this.grid.Columns[i].HeaderText;

                    tabControlSearch.TabPages.Add(tabPage);
                }
            }
			
			// 
			// formFind
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(500,130);			
			this.Controls.Add(this.tabControlSearch);
			this.Controls.Add(this.buttonSearch);
			this.Name = "formFind";
			this.Text = "Търсене";
			foreach(TabPage t in tabControlSearch.TabPages)
			{
				t.ResumeLayout(false);
			}
			this.ResumeLayout(false);

		}		
		#endregion


		private void buttonSearch_Click(object sender, System.EventArgs e)
		{

            //DataView VueSort;
            int i, searchCol;
            bool found = false, cycle = false;

            string srch = "";

            if (tabControlSearch.TabCount <= 0)
            {
                return;
            }
            searchCol = (int)tabControlSearch.TabPages[tabControlSearch.SelectedIndex].Tag;
            if (this.grid.CurrentCell != null)
            {
                i = this.grid.CurrentCell.RowIndex + 1;
                if (i >= this.grid.RowCount)
                    i = 0;
            }
            else
            {
                i = 0;
            }
            //VueSort.Sort = this.dt.Columns[searchCol].ToString();
            //this.grid.DataSource = VueSort;

            srch = tabControlSearch.TabPages[tabControlSearch.SelectedIndex].Controls[0].Text.ToLower();
             
            if(i == 0)
                cycle = true;
            while (!found)
            {
                for(; i < this.grid.RowCount; i++)
                {
                    if (this.grid.Rows[i].Cells[searchCol].Value.ToString().ToLower().EndsWith(srch) || this.grid.Rows[i].Cells[searchCol].Value.ToString().ToLower().StartsWith(srch))
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    if(cycle)
                    {
                        break;
                    }
                    else
                    {
                        i = 0;
                        cycle = true;
                    }
                }
                //foundindex = this.dt.Rows[i][searchCol].ToString().ToLower().IndexOf(tabControlSearch.TabPages[tabControlSearch.SelectedIndex].Controls[0].Text.ToLower());
                //if (foundindex >= 0)
                //{
                //    srch[0] = tabControlSearch.TabPages[tabControlSearch.SelectedIndex].Controls[0].Text.ToLower();
                //    srch[1] = this.dt.Rows[foundindex]["id"].ToString();
                //    foundindex = VueSort.Find(srch[1]);
                //}
                //if (foundindex >= 0)
                //{
                //    found = true;
                //    break;
                //}
                //if (++i >= this.dt.Rows.Count)
                //{
                //    if (cycle == true)
                //        break;
                //    i = 0;
                //    cycle = true;
                //}
            }

            if (found)
            {
                this.grid.CurrentCell = this.grid.Rows[i].Cells[searchCol];
                //if (this.grid.CurrentRowIndex != -1)
                //    grid.UnSelect(grid.CurrentRowIndex);
                //grid.CurrentRowIndex = i;
                //grid.Select(grid.CurrentRowIndex);
            }
            else
            {
                MessageBox.Show("Не са намерени такива лица");
            }			
        }
	}
}