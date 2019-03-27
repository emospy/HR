using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DataLayer;
using System.Collections.Generic;

namespace HR
{
	/// <summary>
	/// Summary description for FormStructureNew.
	/// </summary>
	public class FormStructureNew : System.Windows.Forms.Form
	{
		int par;

		private mainForm main;	
		private DataAction da;
		private DataTable dtNodes, dtEmployees;
		private DataTable dtPos;
		private string TableName;
		
		#region items
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.TabControl tabControl1;
		
		private System.Windows.Forms.Button buttonAddNode;
		private System.Windows.Forms.Button buttonAddChild;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TabPage tabPagePositions;
		private System.Windows.Forms.TabPage tabPageEmployees;
		private System.Windows.Forms.Button buttonAddPosition;
		private System.Windows.Forms.Button buttonDeletePosition;
		private System.Windows.Forms.Button buttonEditPosition;
		private System.Windows.Forms.Button buttonFile;
		private System.Windows.Forms.Button buttonEdit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.StatusBar statusBarFirmStructure;
		private System.Windows.Forms.StatusBarPanel statusBarPanelHeader;
		private System.Windows.Forms.StatusBarPanel statusBarPanelLabelStaff;
		private System.Windows.Forms.StatusBarPanel statusBarPanelStaffCount;
		private System.Windows.Forms.StatusBarPanel statusBarPanelLabelFree;
		private System.Windows.Forms.StatusBarPanel statusBarPanelFree;
		private System.Windows.Forms.StatusBarPanel statusBarPanelLabelBusy;
		private System.Windows.Forms.StatusBarPanel statusBarPanelBusy;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanelLabelEmployees;
		private System.Windows.Forms.StatusBarPanel statusBarPanelEmployees;
		private System.Windows.Forms.Button buttonNewEmployee;
		private System.Windows.Forms.ContextMenu contextMenuArrange;
		private System.Windows.Forms.Button buttonCharacteristics;
        private Button buttonHistory;
		private DataGridView dataGridViewPositions;
		private DataGridView dataGridViewNames;
	
		private System.ComponentModel.Container components = null;
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public FormStructureNew(mainForm main, string TN, DataTable TreeTable)
		{			
			InitializeComponent();

			this.main = main;
			this.dtNodes = TreeTable;
			this.dtNodes.PrimaryKey = new DataColumn[] { this.dtNodes.Columns["id"]};
			this.da = new DataAction(main.connString);			

			this.TableName = TN;
			this.dataGridViewPositions.ContextMenu = new ContextMenu();
			this.dataGridViewPositions.ContextMenu.MenuItems.Add("Премести нагоре", new EventHandler(ContextMenuUp_Click));
			this.dataGridViewPositions.ContextMenu.MenuItems.Add("Премести надолу", new EventHandler(ContextMenuDown_Click));

			this.treeView1.ContextMenu = new ContextMenu();
			this.treeView1.ContextMenu.MenuItems.Add("Премести нагоре", new EventHandler(ContextMenuTreeUp_Click));
			this.treeView1.ContextMenu.MenuItems.Add("Премести надолу", new EventHandler(ContextMenuTreeDown_Click));

		}
		
		private void FormStructureNew_Load(object sender, System.EventArgs e)
		{
			PopulateTree(0, this.treeView1.Nodes);
			this.dtPos = this.da.SelectWhere(TableNames.FirmPersonal3, "*", " WHERE par = " + 0 + " order by 'stafforder'");
			if (dtPos == null)
			{
				MessageBox.Show("Грешка при зареждане на структурата на организацията", ErrorMessages.NoConnection);
				this.Close();
			}
			this.dtPos.PrimaryKey = new DataColumn[]{this.dtPos.Columns["ID"]};	
		}
	
		/// <summary>
		/// Required designer variable. 
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStructureNew));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPagePositions = new System.Windows.Forms.TabPage();
			this.dataGridViewPositions = new System.Windows.Forms.DataGridView();
			this.buttonCharacteristics = new System.Windows.Forms.Button();
			this.statusBarFirmStructure = new System.Windows.Forms.StatusBar();
			this.statusBarPanelHeader = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelLabelStaff = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelStaffCount = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelLabelFree = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelFree = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelLabelBusy = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelBusy = new System.Windows.Forms.StatusBarPanel();
			this.buttonEditPosition = new System.Windows.Forms.Button();
			this.buttonDeletePosition = new System.Windows.Forms.Button();
			this.buttonAddPosition = new System.Windows.Forms.Button();
			this.tabPageEmployees = new System.Windows.Forms.TabPage();
			this.dataGridViewNames = new System.Windows.Forms.DataGridView();
			this.buttonNewEmployee = new System.Windows.Forms.Button();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusBarPanelLabelEmployees = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelEmployees = new System.Windows.Forms.StatusBarPanel();
			this.buttonFile = new System.Windows.Forms.Button();
			this.contextMenuArrange = new System.Windows.Forms.ContextMenu();
			this.buttonAddNode = new System.Windows.Forms.Button();
			this.buttonAddChild = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonEdit = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonHistory = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPagePositions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPositions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelStaff)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelStaffCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelFree)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelFree)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelBusy)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelBusy)).BeginInit();
			this.tabPageEmployees.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewNames)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelEmployees)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelEmployees)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.treeView1.FullRowSelect = true;
			this.treeView1.HideSelection = false;
			this.treeView1.Location = new System.Drawing.Point(16, 16);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(232, 571);
			this.treeView1.TabIndex = 1;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// tabControl1
			// 
			this.tabControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPagePositions);
			this.tabControl1.Controls.Add(this.tabPageEmployees);
			this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tabControl1.Location = new System.Drawing.Point(264, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(720, 651);
			this.tabControl1.TabIndex = 2;
			this.tabControl1.DoubleClick += new System.EventHandler(this.dataGridNames_DoubleClick);
			// 
			// tabPagePositions
			// 
			this.tabPagePositions.Controls.Add(this.dataGridViewPositions);
			this.tabPagePositions.Controls.Add(this.buttonCharacteristics);
			this.tabPagePositions.Controls.Add(this.statusBarFirmStructure);
			this.tabPagePositions.Controls.Add(this.buttonEditPosition);
			this.tabPagePositions.Controls.Add(this.buttonDeletePosition);
			this.tabPagePositions.Controls.Add(this.buttonAddPosition);
			this.tabPagePositions.Location = new System.Drawing.Point(4, 22);
			this.tabPagePositions.Name = "tabPagePositions";
			this.tabPagePositions.Size = new System.Drawing.Size(712, 625);
			this.tabPagePositions.TabIndex = 0;
			this.tabPagePositions.Text = "Длъжности";
			// 
			// dataGridViewPositions
			// 
			this.dataGridViewPositions.AllowUserToAddRows = false;
			this.dataGridViewPositions.AllowUserToDeleteRows = false;
			this.dataGridViewPositions.AllowUserToResizeRows = false;
			this.dataGridViewPositions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewPositions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewPositions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewPositions.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewPositions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewPositions.Location = new System.Drawing.Point(5, 8);
			this.dataGridViewPositions.MultiSelect = false;
			this.dataGridViewPositions.Name = "dataGridViewPositions";
			this.dataGridViewPositions.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewPositions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewPositions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewPositions.Size = new System.Drawing.Size(700, 556);
			this.dataGridViewPositions.TabIndex = 6;
			this.dataGridViewPositions.TabStop = false;
			this.dataGridViewPositions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewPositions_MouseUp);
			// 
			// buttonCharacteristics
			// 
			this.buttonCharacteristics.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCharacteristics.Image = ((System.Drawing.Image)(resources.GetObject("buttonCharacteristics.Image")));
			this.buttonCharacteristics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCharacteristics.Location = new System.Drawing.Point(353, 595);
			this.buttonCharacteristics.Name = "buttonCharacteristics";
			this.buttonCharacteristics.Size = new System.Drawing.Size(180, 23);
			this.buttonCharacteristics.TabIndex = 5;
			this.buttonCharacteristics.Text = "   Длъжностна характеристика";
			this.buttonCharacteristics.Click += new System.EventHandler(this.buttonCharacteristics_Click);
			// 
			// statusBarFirmStructure
			// 
			this.statusBarFirmStructure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.statusBarFirmStructure.Dock = System.Windows.Forms.DockStyle.None;
			this.statusBarFirmStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statusBarFirmStructure.Location = new System.Drawing.Point(8, 566);
			this.statusBarFirmStructure.Name = "statusBarFirmStructure";
			this.statusBarFirmStructure.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanelHeader,
            this.statusBarPanelLabelStaff,
            this.statusBarPanelStaffCount,
            this.statusBarPanelLabelFree,
            this.statusBarPanelFree,
            this.statusBarPanelLabelBusy,
            this.statusBarPanelBusy});
			this.statusBarFirmStructure.ShowPanels = true;
			this.statusBarFirmStructure.Size = new System.Drawing.Size(697, 22);
			this.statusBarFirmStructure.SizingGrip = false;
			this.statusBarFirmStructure.TabIndex = 4;
			// 
			// statusBarPanelHeader
			// 
			this.statusBarPanelHeader.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarPanelHeader.Name = "statusBarPanelHeader";
			this.statusBarPanelHeader.Text = "Общо за звеното:";
			this.statusBarPanelHeader.Width = 112;
			// 
			// statusBarPanelLabelStaff
			// 
			this.statusBarPanelLabelStaff.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarPanelLabelStaff.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarPanelLabelStaff.Name = "statusBarPanelLabelStaff";
			this.statusBarPanelLabelStaff.Text = "Щатни бройки :";
			this.statusBarPanelLabelStaff.Width = 98;
			// 
			// statusBarPanelStaffCount
			// 
			this.statusBarPanelStaffCount.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.statusBarPanelStaffCount.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanelStaffCount.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
			this.statusBarPanelStaffCount.Name = "statusBarPanelStaffCount";
			this.statusBarPanelStaffCount.Width = 122;
			// 
			// statusBarPanelLabelFree
			// 
			this.statusBarPanelLabelFree.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarPanelLabelFree.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarPanelLabelFree.Name = "statusBarPanelLabelFree";
			this.statusBarPanelLabelFree.Text = "Вакантни:";
			this.statusBarPanelLabelFree.Width = 69;
			// 
			// statusBarPanelFree
			// 
			this.statusBarPanelFree.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.statusBarPanelFree.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanelFree.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
			this.statusBarPanelFree.Name = "statusBarPanelFree";
			this.statusBarPanelFree.Width = 122;
			// 
			// statusBarPanelLabelBusy
			// 
			this.statusBarPanelLabelBusy.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarPanelLabelBusy.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarPanelLabelBusy.Name = "statusBarPanelLabelBusy";
			this.statusBarPanelLabelBusy.Text = "Заети:";
			this.statusBarPanelLabelBusy.Width = 50;
			// 
			// statusBarPanelBusy
			// 
			this.statusBarPanelBusy.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.statusBarPanelBusy.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanelBusy.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
			this.statusBarPanelBusy.Name = "statusBarPanelBusy";
			this.statusBarPanelBusy.Width = 122;
			// 
			// buttonEditPosition
			// 
			this.buttonEditPosition.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonEditPosition.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditPosition.Image")));
			this.buttonEditPosition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEditPosition.Location = new System.Drawing.Point(180, 595);
			this.buttonEditPosition.Name = "buttonEditPosition";
			this.buttonEditPosition.Size = new System.Drawing.Size(170, 24);
			this.buttonEditPosition.TabIndex = 3;
			this.buttonEditPosition.Text = "     Коригирай";
			this.buttonEditPosition.Click += new System.EventHandler(this.buttonEditPosition_Click);
			// 
			// buttonDeletePosition
			// 
			this.buttonDeletePosition.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonDeletePosition.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeletePosition.Image")));
			this.buttonDeletePosition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonDeletePosition.Location = new System.Drawing.Point(536, 595);
			this.buttonDeletePosition.Name = "buttonDeletePosition";
			this.buttonDeletePosition.Size = new System.Drawing.Size(170, 24);
			this.buttonDeletePosition.TabIndex = 2;
			this.buttonDeletePosition.Text = " Изтрий";
			this.buttonDeletePosition.Click += new System.EventHandler(this.buttonDeletePosition_Click);
			// 
			// buttonAddPosition
			// 
			this.buttonAddPosition.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAddPosition.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddPosition.Image")));
			this.buttonAddPosition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAddPosition.Location = new System.Drawing.Point(8, 595);
			this.buttonAddPosition.Name = "buttonAddPosition";
			this.buttonAddPosition.Size = new System.Drawing.Size(170, 24);
			this.buttonAddPosition.TabIndex = 1;
			this.buttonAddPosition.Text = "   Дoбави";
			this.buttonAddPosition.Click += new System.EventHandler(this.buttonAddPosition_Click);
			// 
			// tabPageEmployees
			// 
			this.tabPageEmployees.Controls.Add(this.dataGridViewNames);
			this.tabPageEmployees.Controls.Add(this.buttonNewEmployee);
			this.tabPageEmployees.Controls.Add(this.statusBar1);
			this.tabPageEmployees.Controls.Add(this.buttonFile);
			this.tabPageEmployees.Location = new System.Drawing.Point(4, 22);
			this.tabPageEmployees.Name = "tabPageEmployees";
			this.tabPageEmployees.Size = new System.Drawing.Size(712, 625);
			this.tabPageEmployees.TabIndex = 1;
			this.tabPageEmployees.Text = "Служители";
			// 
			// dataGridViewNames
			// 
			this.dataGridViewNames.AllowUserToAddRows = false;
			this.dataGridViewNames.AllowUserToDeleteRows = false;
			this.dataGridViewNames.AllowUserToResizeRows = false;
			this.dataGridViewNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewNames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewNames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewNames.DefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridViewNames.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewNames.Location = new System.Drawing.Point(5, 8);
			this.dataGridViewNames.MultiSelect = false;
			this.dataGridViewNames.Name = "dataGridViewNames";
			this.dataGridViewNames.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewNames.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridViewNames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewNames.Size = new System.Drawing.Size(704, 556);
			this.dataGridViewNames.TabIndex = 4;
			this.dataGridViewNames.DoubleClick += new System.EventHandler(this.dataGridNames_DoubleClick);
			// 
			// buttonNewEmployee
			// 
			this.buttonNewEmployee.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonNewEmployee.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewEmployee.Image")));
			this.buttonNewEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonNewEmployee.Location = new System.Drawing.Point(160, 595);
			this.buttonNewEmployee.Name = "buttonNewEmployee";
			this.buttonNewEmployee.Size = new System.Drawing.Size(170, 24);
			this.buttonNewEmployee.TabIndex = 3;
			this.buttonNewEmployee.Text = "   Нов служител";
			this.buttonNewEmployee.Click += new System.EventHandler(this.buttonNewEmployee_Click);
			// 
			// statusBar1
			// 
			this.statusBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.statusBar1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statusBar1.Location = new System.Drawing.Point(8, 566);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanelLabelEmployees,
            this.statusBarPanelEmployees});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(721, 22);
			this.statusBar1.SizingGrip = false;
			this.statusBar1.TabIndex = 2;
			this.statusBar1.Text = "statusBar1";
			// 
			// statusBarPanelLabelEmployees
			// 
			this.statusBarPanelLabelEmployees.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarPanelLabelEmployees.Name = "statusBarPanelLabelEmployees";
			this.statusBarPanelLabelEmployees.Text = "Общо служители в звеното:";
			this.statusBarPanelLabelEmployees.Width = 175;
			// 
			// statusBarPanelEmployees
			// 
			this.statusBarPanelEmployees.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.statusBarPanelEmployees.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
			this.statusBarPanelEmployees.Name = "statusBarPanelEmployees";
			this.statusBarPanelEmployees.Width = 50;
			// 
			// buttonFile
			// 
			this.buttonFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonFile.Image")));
			this.buttonFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFile.Location = new System.Drawing.Point(383, 595);
			this.buttonFile.Name = "buttonFile";
			this.buttonFile.Size = new System.Drawing.Size(170, 24);
			this.buttonFile.TabIndex = 1;
			this.buttonFile.Text = "Досие";
			this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
			// 
			// buttonAddNode
			// 
			this.buttonAddNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddNode.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddNode.Image")));
			this.buttonAddNode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAddNode.Location = new System.Drawing.Point(16, 593);
			this.buttonAddNode.Name = "buttonAddNode";
			this.buttonAddNode.Size = new System.Drawing.Size(130, 24);
			this.buttonAddNode.TabIndex = 3;
			this.buttonAddNode.Text = "   Добави звено";
			this.buttonAddNode.Click += new System.EventHandler(this.buttonAddNode_Click);
			// 
			// buttonAddChild
			// 
			this.buttonAddChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddChild.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddChild.Image")));
			this.buttonAddChild.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAddChild.Location = new System.Drawing.Point(8, 625);
			this.buttonAddChild.Name = "buttonAddChild";
			this.buttonAddChild.Size = new System.Drawing.Size(130, 24);
			this.buttonAddChild.TabIndex = 4;
			this.buttonAddChild.Text = "   Добави подзвено";
			this.buttonAddChild.Click += new System.EventHandler(this.buttonAddChild_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonDelete.Location = new System.Drawing.Point(144, 625);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(100, 24);
			this.buttonDelete.TabIndex = 5;
			this.buttonDelete.Text = " Изтрий";
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(538, 674);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(150, 24);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = " Изход";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonEdit
			// 
			this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.Image")));
			this.buttonEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEdit.Location = new System.Drawing.Point(152, 593);
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.Size = new System.Drawing.Size(100, 24);
			this.buttonEdit.TabIndex = 7;
			this.buttonEdit.Text = "    Коригирай";
			this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.buttonAddChild);
			this.groupBox1.Controls.Add(this.buttonDelete);
			this.groupBox1.Location = new System.Drawing.Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(248, 659);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Структура на организацията";
			// 
			// buttonHistory
			// 
			this.buttonHistory.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonHistory.Image = ((System.Drawing.Image)(resources.GetObject("buttonHistory.Image")));
			this.buttonHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonHistory.Location = new System.Drawing.Point(328, 674);
			this.buttonHistory.Name = "buttonHistory";
			this.buttonHistory.Size = new System.Drawing.Size(150, 24);
			this.buttonHistory.TabIndex = 9;
			this.buttonHistory.Text = "История";
			this.buttonHistory.Click += new System.EventHandler(this.buttonHistory_Click);
			// 
			// FormStructureNew
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(992, 706);
			this.Controls.Add(this.buttonHistory);
			this.Controls.Add(this.buttonEdit);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonAddNode);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormStructureNew";
			this.ShowInTaskbar = false;
			this.Text = "Структура на организацията";
			this.Load += new System.EventHandler(this.FormStructureNew_Load);
			this.Resize += new System.EventHandler(this.FormStructureNew_Resize);
			this.tabControl1.ResumeLayout(false);
			this.tabPagePositions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPositions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelStaff)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelStaffCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelFree)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelFree)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelBusy)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelBusy)).EndInit();
			this.tabPageEmployees.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewNames)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabelEmployees)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelEmployees)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region TreeStructure
		private void PopulateTree(int parrot, TreeNodeCollection ParentNode)
		{
			try
			{
				DataView vueTree = new DataView(this.dtNodes, "par = " + parrot, "TreeOrder", DataViewRowState.CurrentRows);
				for (int i = 0; i < vueTree.Count; i++)
				{
					try
					{
						par = int.Parse(vueTree[i]["par"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Има грешка в структурата на организацията");
						par = 0;
					}
					if (par == parrot)
					{
						TreeNode node1 = new TreeNode(vueTree[i]["level"].ToString());
						try
						{
							node1.Tag = int.Parse(vueTree[i]["id"].ToString());
						}
						catch (System.Exception e)
						{
							MessageBox.Show(e.Message, "Има някаква грешка в дървото на организацията");
							node1.Tag = 0;
						}
						ParentNode.Add(node1);
						this.PopulateTree((int)node1.Tag, node1.Nodes);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}	

		private void buttonAddNode_Click(object sender, System.EventArgs e)
		{
			try
			{
				ArrayList Columns = new ArrayList();
				MappingFormData map = new MappingFormData();
				map.ColumnText = "";
				map.HeaderText = "Код";
				map.MappingName = "code";
				Columns.Add(map);
				map.ColumnText = "";
				map.HeaderText = "Наименование на звено";
				map.MappingName = "level";
				Columns.Add(map);
				map.ColumnText = "";
				map.HeaderText = "Name";
				map.MappingName = "leveleng";
				Columns.Add(map);

				CommonNomenclatureAdd AddForm = new CommonNomenclatureAdd(Columns);

				if (AddForm.ShowDialog() == DialogResult.OK/* && AddForm.textBoxAdd2.Text != ""*/)
				{
					ArrayList ray = AddForm.GetVariables();
					DataAction da = new DataAction(main.connString);
					DataRow row = dtNodes.NewRow();
					for (int i = 0; i < ray.Count; i++)
					{
						map = (MappingFormData)ray[i];
						row[map.MappingName] = map.ColumnText;
					}
					if (row["level"].ToString().Trim() != "")
					{
						TreeNode node = new TreeNode(row["level"].ToString());
						int deep = this.GetDeepOfNodes(this.treeView1.SelectedNode);
						if (deep == 0)
						{
							int id;
							Dictionary<string, object> Dict = new Dictionary<string, object>();
							Dict.Add("level", row["level"].ToString());
							Dict.Add("par", "0");
							Dict.Add("code", row["code"].ToString());
							Dict.Add("leveleng", row["leveleng"].ToString());
							
							treeView1.Nodes.Add(node);
							id = da.UniversalInsertParam(TableNames.NewTree2, Dict, "id", TransactionComnmand.BEGIN_TRANSACTION);
							if (id > 0)
							{
								node.Tag = id;
								row["id"] = node.Tag;
								row["par"] = 0;
								dtNodes.Rows.Add(row);
							}
							else
							{
								MessageBox.Show("Грешка при добавяне на звено", ErrorMessages.NoConnection);
								return;
							}
							Dictionary<string, object> uDict = new Dictionary<string, object>();
							uDict.Add("TreeOrder", id);
							da.UniversalUpdateParam(TableNames.NewTree2, "id", uDict, id.ToString(), TransactionComnmand.USE_TRANSACTION);

						}
						else
						{
							int id;
							Dictionary<string, object> Dict = new Dictionary<string, object>();
							Dict.Add("level", row["level"].ToString());
							Dict.Add("par", treeView1.SelectedNode.Parent.Tag.ToString());
							Dict.Add("code", row["code"].ToString());
							Dict.Add("leveleng", row["leveleng"].ToString());
							Dict.Add("TreeOrder", "id");
							treeView1.SelectedNode.Parent.Nodes.Add(node);
							id = da.UniversalInsertParam(TableNames.NewTree2, Dict, "id", TransactionComnmand.BEGIN_TRANSACTION);
							if (id > 0)
							{
								node.Tag = id;
								row["id"] = node.Tag;
								row["par"] = treeView1.SelectedNode.Parent.Tag;
								dtNodes.Rows.Add(row);
							}
							else
							{
								MessageBox.Show("Грешка при добавяне на звено", ErrorMessages.NoConnection);
								return;
							}
							Dictionary<string, object> uDict = new Dictionary<string, object>();
							uDict.Add("TreeOrder", id);
							da.UniversalUpdateParam(TableNames.NewTree2, "id", uDict, id.ToString(), TransactionComnmand.USE_TRANSACTION);
						}
						Dictionary<string, object> hDict = new Dictionary<string, object>();
						hDict.Add("changefrom", "");
						hDict.Add("changeto", string.Format("{0} {1} {2}", row["code"].ToString(), row["level"].ToString(), row["leveleng"].ToString()));
						hDict.Add("changeoperation", "Добавяне на звено");
                        //hDict.Add("changedate", DataAction.ConvertDateToMySqlU(DateTime.Now, mainForm.DataBaseTypes));
                        hDict.Add("changedate", DateTime.Now);
						if (da.UniversalInsertParam(TableNames.StructureHistory, hDict, "id", TransactionComnmand.COMMIT_TRANSACTION) <= 0)
						{
							MessageBox.Show("Грешка при добавяне на звено", ErrorMessages.NoConnection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private int GetDeepOfNodes( TreeNode node )
		{
			if(node != null)
			{
				for( int i = 0; i < 4; i++ )
				{
					if( node.Parent == null )
					{
						return i;
					}
					else
					{
						node = node.Parent;
					}
				}
			}
			return 0;
		}
		
		private void buttonAddChild_Click(object sender, System.EventArgs e)
		{
			try
			{
				ArrayList Columns = new ArrayList();
				MappingFormData map = new MappingFormData();
				map.ColumnText = "";
				map.HeaderText = "Код";
				map.MappingName = "code";
				Columns.Add(map);
				map.ColumnText = "";
				map.HeaderText = "Наименование на звено";
				map.MappingName = "level";
				Columns.Add(map);
				map.ColumnText = "";
				map.HeaderText = "Name";
				map.MappingName = "leveleng";
				Columns.Add(map);

				CommonNomenclatureAdd AddForm = new CommonNomenclatureAdd(Columns);
				int deep = this.GetDeepOfNodes(this.treeView1.SelectedNode);
				if (deep >= 3)
				{
					MessageBox.Show("Няма по-ниски нива в структурата");
				}
				else
				{
					if (AddForm.ShowDialog() == DialogResult.OK && treeView1.SelectedNode != null)
					{

						DataAction da = new DataAction(main.connString);
						DataRow row = dtNodes.NewRow();
						ArrayList ray = AddForm.GetVariables();

						for (int i = 0; i < ray.Count; i++)
						{
							map = (MappingFormData)ray[i];
							row[map.MappingName] = map.ColumnText;
						}

						TreeNode node = new TreeNode(row["level"].ToString());
						treeView1.SelectedNode.Nodes.Add(node);
						Dictionary<string, object> Dict = new Dictionary<string, object>();
						Dict.Add("level", row["level"].ToString());
						Dict.Add("par", treeView1.SelectedNode.Tag.ToString());
						Dict.Add("code", row["code"].ToString());
						Dict.Add("leveleng", row["leveleng"].ToString());
						node.Tag = da.UniversalInsertParam(TableNames.NewTree2, Dict, "id", TransactionComnmand.NO_TRANSACTION);
						row["id"] = node.Tag;
						row["par"] = treeView1.SelectedNode.Tag;
						dtNodes.Rows.Add(row);

						Dictionary<string, object> uDict = new Dictionary<string, object>();
						uDict.Add("TreeOrder", (int)node.Tag);
						da.UniversalUpdateParam(TableNames.NewTree2, "id", uDict, node.Tag.ToString(), TransactionComnmand.NO_TRANSACTION);

						Dictionary<string, object> hDict = new Dictionary<string, object>();
						hDict.Add("changefrom", "");
						hDict.Add("changeto", row["code"].ToString() + " " + row["level"].ToString() + " " + row["leveleng"].ToString());
						hDict.Add("changeoperation", "Добавяне на звено");
                        //hDict.Add("changedate", DataAction.ConvertDateToMySqlU(DateTime.Now, mainForm.DataBaseTypes));
                        hDict.Add("changedate", DateTime.Now);
						da.UniversalInsertParam(TableNames.StructureHistory, hDict, "id", TransactionComnmand.NO_TRANSACTION);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			try
			{
				this.dtPos.Clear();
				this.dtPos = this.da.SelectWhere(TableNames.FirmPersonal3, "*", " WHERE par = " + this.treeView1.SelectedNode.Tag.ToString() + " order by stafforder");
				this.dtEmployees = this.da.SelectWhere(TableNames.Person, "*", "WHERE nodeID = " + this.treeView1.SelectedNode.Tag + " AND fired = 0");
				if (this.dtPos == null || this.dtEmployees == null)
				{
					MessageBox.Show("Грешка при зареждане на структурата на организацията", ErrorMessages.NoConnection);
					return;
				}
				this.dtPos.PrimaryKey = new DataColumn[] { this.dtPos.Columns["ID"] };
				this.dataGridViewPositions.DataSource = dtPos;

				this.dataGridViewNames.DataSource = dtEmployees;
				this.JustifyGrid(this.dataGridViewNames);
				this.JustifyGrid(this.dataGridViewPositions);
				double Busy = 0, Staff = 0;
				double TotalStaff = 0, TotalBusy = 0;
				foreach (DataRow row in dtPos.Rows)
				{
					double.TryParse(row["StaffCount"].ToString(), out Staff);
					TotalStaff += Staff;

					DataTable dtBusy = this.da.SelectWhere(TableNames.PersonAssignment, "*", string.Format("WHERE positionid = {0} AND isactive = 1 and (tutorname = '' or tutorname  is null)", row["id"].ToString()));
					if (dtBusy != null)
					{
						Busy = 0;
						foreach(DataRow ri in dtBusy.Rows)
						{
							double bb = 0;
							double.TryParse(ri["staff"].ToString(), out bb);
							Busy += bb;
						}
						
						TotalBusy += Busy;
					}
					row["Free"] = Staff - Busy;
					row["Busy"] = Busy;
				}

				this.statusBarPanelStaffCount.Text = TotalStaff.ToString() + " ";
				this.statusBarPanelBusy.Text = TotalBusy.ToString() + " ";
				this.statusBarPanelFree.Text = (TotalStaff - TotalBusy).ToString() + " ";
				this.statusBarPanelEmployees.Text = dtEmployees.Rows.Count.ToString() + " ";
				this.Refresh();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (dtPos.Rows.Count > 0)
				{
					MessageBox.Show("Не можете да изтриете звеното, докато има длъжности!");
				}
				else
				{
					if (this.treeView1.SelectedNode.Nodes.Count > 0)
					{
						MessageBox.Show("Не можете да изтриете звеното, докато има подзвена!");
					}
					else // Изтриване 
					{
						DataRow row = this.dtNodes.Rows.Find((int)this.treeView1.SelectedNode.Tag);
						if (da.UniversalDelete(TableNames.NewTree2, this.treeView1.SelectedNode.Tag.ToString(), "id", TransactionComnmand.BEGIN_TRANSACTION))
						{
							int res;
							Dictionary<string, object> hDict = new Dictionary<string, object>();
							hDict.Add("changefrom", row["code"].ToString() + " " + row["level"].ToString() + " " + row["leveleng"].ToString());
							hDict.Add("changeto", "");
							hDict.Add("changeoperation", "Изтриване на звено");
                            hDict.Add("changedate", DateTime.Now);

							res = da.UniversalInsertParam(TableNames.StructureHistory, hDict, "id", TransactionComnmand.COMMIT_TRANSACTION);
							if (res > 0)
							{
								this.treeView1.SelectedNode.Remove();
								this.dtNodes.Rows.Remove(row);
							}
						}
						else
						{
							MessageBox.Show("Грешка при изтриване на звено", ErrorMessages.NoConnection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataAction da = new DataAction(this.main.connString);
				DataRow row = this.dtNodes.Rows.Find((int)this.treeView1.SelectedNode.Tag);
				ArrayList Columns = new ArrayList();
				MappingFormData map = new MappingFormData();
				map.ColumnText = row["code"].ToString();
				map.HeaderText = "Код";
				map.MappingName = "code";
				Columns.Add(map);
				map.ColumnText = row["level"].ToString();
				map.HeaderText = "Наименование на звено";
				map.MappingName = "level";
				Columns.Add(map);
				map.ColumnText = row["leveleng"].ToString();
				map.HeaderText = "Name";
				map.MappingName = "leveleng";
				Columns.Add(map);

				if (this.treeView1.SelectedNode != null)
				{
					if (dtEmployees.Rows.Count > 0)
					{
						if (DialogResult.No == MessageBox.Show("В звеното има назначени служители. Ако преименувате звеното, това ще се отрази автоматично в техните досиета и история. Сигурни ли сте че искате да коригирате записа?", "Въпрос", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
						{
							return;
						}
					}
					CommonNomenclatureAdd AddForm = new CommonNomenclatureAdd(Columns);

					if (AddForm.ShowDialog() == DialogResult.OK)
					{
						ArrayList ray = AddForm.GetVariables();
						string old = row["code"].ToString() + " " + row["level"].ToString() + " " + row["leveleng"].ToString();
						string oldlevel = row["level"].ToString();

						for (int i = 0; i < ray.Count; i++)
						{
							map = (MappingFormData)ray[i];
							row[map.MappingName] = map.ColumnText;
						}

						Dictionary<string, object> hDict = new Dictionary<string, object>();
						hDict.Add("changefrom", old);
						hDict.Add("changeto", row["code"].ToString() + " " + row["level"].ToString() + " " + row["leveleng"].ToString());
						hDict.Add("changeoperation", "Корекция на звено");
                        //hDict.Add("changedate", DataAction.ConvertDateToMySqlU(DateTime.Now, mainForm.DataBaseTypes));
                        hDict.Add("changedate",DateTime.Now);
						da.UniversalInsertParam(TableNames.StructureHistory, hDict, "id", TransactionComnmand.NO_TRANSACTION);

						Dictionary<string, object> Dict = new Dictionary<string, object>();
						Dict.Add("level", row["level"].ToString());
						//Dict.Add("par", treeView1.SelectedNode.Tag.ToString());
						Dict.Add("code", row["code"].ToString());
						Dict.Add("leveleng", row["leveleng"].ToString());
						da.UniversalUpdateParam(TableNames.NewTree2, "id", Dict, this.treeView1.SelectedNode.Tag.ToString(), TransactionComnmand.NO_TRANSACTION);
						int deep = this.GetDeepOfNodes(this.treeView1.SelectedNode);
						deep++;
						string level = "level" + deep;

						da.RenameLevel(level, oldlevel, row["level"].ToString());
						this.treeView1.SelectedNode.Text = row["level"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
		#endregion				

		#region View Data
		private void PopulatePackageFromForm(formPosition form, Dictionary <string, Object> Dict)
		{
			try
			{
				Dict.Add("Par", this.treeView1.SelectedNode.Tag.ToString());
				Dict.Add("AdditionNumber", form.textBoxAdditionNumber.Text);
				Dict.Add("Busy", "0");
				if (form.numBoxShtatCount.Text == "")
				{
					Dict.Add("Free", "0");
				}
				else
				{
					if (form.comboBoxTypePosition.Text == "Сезонна")
					{
						try
						{
							if (form.numBoxNumberMonths.Text != "")
							{
								Dict.Add("Free", form.numBoxNumberMonths.Text);
							}
							else
							{
								Dict.Add("Free", "0");
							}
						}
						catch (System.Exception e)
						{
							MessageBox.Show(e.Message, "Грешни данни за работни места");
							Dict.Add("Free", "0");
						}
					}
					else
					{
						try
						{
							Dict.Add("Free", form.numBoxShtatCount.Text);
						}
						catch (System.Exception e)
						{
							MessageBox.Show(e.Message, "Грешни данни за работни места");
							Dict.Add("Free", "0");
						}
					}
				}
				if (form.numBoxNumberMonths.Text == "")
				{
					Dict.Add("NumMonths", "0");
				}
				else
				{
					Dict.Add("NumMonths", form.numBoxNumberMonths.Text);
				}
				Dict.Add("Education", form.textBoxEducation.Text);
				Dict.Add("EKDACode", form.textBoxEKDACode.Text);
				Dict.Add("EKDALevel", form.textBoxEKDALevel.Text);
				Dict.Add("Experience", form.textBoxExperience.Text);
				Dict.Add("KVS", form.textBoxKVS.Text);
				Dict.Add("Law", form.textBoxLaw.Text);
				Dict.Add("MinSalary", form.textBoxMinSalary.Text);
				Dict.Add("MaxSalary", form.textBoxMaxSalary.Text);
				Dict.Add("NKPCode", form.textBoxNKPCode.Text);
				Dict.Add("NKPLevel", form.textBoxNKPLevel.Text);
				Dict.Add("Notes", form.textBoxNotes.Text);
				Dict.Add("OtherRequirements", form.textBoxOtherRequirements.Text);
				Dict.Add("PMS", form.textBoxPMS.Text);
				Dict.Add("PorNum", form.textBoxPorNum.Text);
				Dict.Add("NameOfPosition", form.comboBoxPosition.Text);
				Dict.Add("ekdapaylevel", form.comboBoxEkdaPayLevel.Text);
				Dict.Add("Rang", form.textBoxRang.Text);
				Dict.Add("SecurityLevel", form.textBoxSecurity.Text);
				Dict.Add("StartSalary", form.numBoxStartPayment.Text);
				Dict.Add("BaseSalary", form.numBoxBasePayment.Text);
				Dict.Add("ScienceAddon", form.numBoxScienceAddon.Text);
				Dict.Add("SalaryAddon", form.numBoxAddon.Text);
				Dict.Add("OtherAddon", form.numBoxOtherAddon.Text);
				Dict.Add("PositionEng", form.textBoxPositionEng.Text);
				var SelectedPosition = form.comboBoxPosition.SelectedItem;
				int GPID = 0;
				int.TryParse((SelectedPosition as DataRowView)["id"].ToString(), out GPID);
				
				Dict.Add("globalpositionid", GPID);

				try
				{
					Dict.Add("StaffCount", form.numBoxShtatCount.Text);
				}
				catch (System.FormatException)
				{
					Dict.Add("StaffCount", "0");
				}
				Dict.Add("VOS", form.textBoxVOS.Text);
				Dict.Add("TypePosition", form.comboBoxTypePosition.Text);
				try
				{
					if (form.numBoxFree.Text != "")
						Dict["Free"] = form.numBoxFree.Text;
				}
				catch (FormatException)
				{
					//Dict.Add("Free", "0");
				}
				catch (ArgumentException)
				{
				}

				try
				{
					if (form.numBoxBusy.Text != "")
						Dict["Busy"] = form.numBoxBusy.Text;
				}
				catch (FormatException)
				{
					//Dict.Add("Busy", "0");
				}
				catch (ArgumentException)
				{
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void AddPackageToTable(Dictionary<string, object> Dict)
		{
			try
			{
				DataRow row = this.dtPos.NewRow();

				foreach (KeyValuePair<string, object> kvp in Dict)
				{
					row[kvp.Key] = Dict[kvp.Key];
				}
				this.dtPos.Rows.Add(row);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAddPosition_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.treeView1.Nodes.Count == 0)
				{
					MessageBox.Show("Не може да се добавя длъжност към празна структура на организацията.");
					return;
				}
				formPosition form = new formPosition(this, this.main, this.treeView1.SelectedNode);
				form.numBoxBusy.ReadOnly = true;
				form.numBoxFree.ReadOnly = true;
				if (form.ShowDialog() == DialogResult.OK)
				{
					bool IsValid;
					Dictionary<string, object> Dict = new Dictionary<string, object>();
					Dictionary<string, object> sDict = new Dictionary<string, object>();
					Dictionary<string, object> hDict = new Dictionary<string, object>();
					this.PopulatePackageFromForm(form, Dict);
					int id = this.da.UniversalInsertParam(TableNames.FirmPersonal3, Dict, "id", TransactionComnmand.BEGIN_TRANSACTION);
					if (id < 0)
					{
						MessageBox.Show("Грешка при добавяне на длъжност", ErrorMessages.NoConnection);
						return;
					}
					
					sDict.Add("stafforder", id.ToString());
					IsValid = this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", sDict, id.ToString(), TransactionComnmand.USE_TRANSACTION);
					if (IsValid == false)
					{
						MessageBox.Show("Грешка при добавяне на длъжност", ErrorMessages.NoConnection);
						return;
					}
					
					hDict.Add("changefrom", "");
					hDict.Add("changeto", Dict["NameOfPosition"] + " " + Dict["StaffCount"]);
					hDict.Add("changeoperation", "Добавяне на длъжност");
                    //hDict.Add("changedate", DataAction.ConvertDateToMySqlU(DateTime.Now, mainForm.DataBaseTypes));
                    hDict.Add("changedate", DateTime.Now);
					if (da.UniversalInsertParam(TableNames.StructureHistory, hDict, "id", TransactionComnmand.COMMIT_TRANSACTION) < 0)
					{
						MessageBox.Show("Грешка при добавяне на длъжност", ErrorMessages.NoConnection);
						return;
					}

					Dict.Add("ID", id.ToString());
					Dict.Add("StaffOrder", Dict["ID"]);
					AddPackageToTable(Dict);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void UpdatePackageInTable(Dictionary<string, object> Dict)
		{
			try
			{
				DataRow row = this.dtPos.Rows.Find(Dict["ID"]);

				foreach (KeyValuePair<string, object> kvp in Dict)
				{
					row[kvp.Key] = Dict[kvp.Key];
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonDeletePosition_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewPositions.CurrentRow != null)
				{
					if (MessageBox.Show("Сигурни ли сте че искате да изтриете длъжността " + this.dataGridViewPositions.CurrentRow.Cells["NameOfPosition"].Value.ToString(), "Изтриване на длъжност", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						DataTable dtPerson = new DataTable();

						int id;
						try
						{
							id = int.Parse(this.dataGridViewPositions.CurrentRow.Cells["id"].Value.ToString());
						}
						catch (System.Exception ex)
						{
							MessageBox.Show(ex.Message, "Ред с такъв идентификатор не може да бъде открит. Изтриването не може да се извърши");
							return;
						}
						dtPerson = da.SelectWhere(TableNames.PersonAssignment, "*", "WHERE positionid = " + id.ToString() + " and isactive = 1");
						if (dtPerson == null)
						{
							MessageBox.Show("Грешка при изтриване на длъжност", ErrorMessages.NoConnection);
							return;
						}
						if (dtPerson.Rows.Count > 0)
						{
							MessageBox.Show("На тази длъжност има назначени лица/лице. Не можете да изтриете длъжността преди да сте освободили лицата, назначени на нея.");
							return;
						}
						else
						{
							//Transaction
							DataRow toDel = this.dtPos.Rows.Find(id);

							if (this.da.UniversalDelete(TableNames.FirmPersonal3, id.ToString(), "id"))
							{
								this.dtPos.Rows.Remove(toDel);
							}
							else
							{
								MessageBox.Show("Грешка при изтриване на длъжност", ErrorMessages.NoConnection);
								Dictionary<string, object> hDict = new Dictionary<string, object>();
								hDict.Add("changefrom", toDel["nameofposition"] + " " + toDel["staffcount"]);
								hDict.Add("changeto", "");
								hDict.Add("changeoperation", "Изтриване на длъжност");
                                hDict.Add("changedate", DateTime.Now);
								da.UniversalInsertParam(TableNames.StructureHistory, hDict, "id", TransactionComnmand.NO_TRANSACTION);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonEditPosition_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewPositions.CurrentRow != null)
				{
					string nameofposition, nkpcode, nkplevel;

					

					nameofposition = this.dataGridViewPositions.CurrentRow.Cells["NameOfPosition"].Value.ToString();
					nkpcode = this.dataGridViewPositions.CurrentRow.Cells["NKPCode"].Value.ToString();
					nkplevel = this.dataGridViewPositions.CurrentRow.Cells["NKPLevel"].Value.ToString();

					formPosition form = new formPosition(this, this.main, this.treeView1.SelectedNode, nameofposition, nkpcode, nkplevel);
					form.numBoxNumberMonths.Text = this.dataGridViewPositions.CurrentRow.Cells["NumMonths"].Value.ToString();
					form.numBoxShtatCount.Text = this.dataGridViewPositions.CurrentRow.Cells["StaffCount"].Value.ToString();
					form.textBoxExperience.Text = this.dataGridViewPositions.CurrentRow.Cells["Experience"].Value.ToString();
					form.textBoxKVS.Text = this.dataGridViewPositions.CurrentRow.Cells["KVS"].Value.ToString();
					form.textBoxNotes.Text = this.dataGridViewPositions.CurrentRow.Cells["Notes"].Value.ToString();
					form.textBoxOtherRequirements.Text = this.dataGridViewPositions.CurrentRow.Cells["OtherRequirements"].Value.ToString();
					form.textBoxSecurity.Text = this.dataGridViewPositions.CurrentRow.Cells["SecurityLevel"].Value.ToString();
					form.textBoxAdditionNumber.Text = this.dataGridViewPositions.CurrentRow.Cells["AdditionNumber"].Value.ToString();
					form.numBoxFree.Text = this.dataGridViewPositions.CurrentRow.Cells["Free"].Value.ToString();
					form.numBoxBusy.Text = this.dataGridViewPositions.CurrentRow.Cells["Busy"].Value.ToString();
					int ind = form.comboBoxTypePosition.FindString(this.dataGridViewPositions.CurrentRow.Cells["TypePosition"].Value.ToString());
					if (ind >= 0)
					{
						form.comboBoxTypePosition.SelectedIndex = ind;
					}
					DataTable EKDAPayLevel = this.da.SelectWhere(TableNames.EkdaPayLevels, "*", "order by Number");
					form.comboBoxEkdaPayLevel.DataSource = EKDAPayLevel;
					form.comboBoxEkdaPayLevel.DisplayMember = "LevelName";
					int idx = form.comboBoxEkdaPayLevel.FindString(this.dataGridViewPositions.CurrentRow.Cells["ekdapaylevel"].Value.ToString());
					if (idx >= 0)
					{
						form.comboBoxEkdaPayLevel.SelectedIndex = idx;
					}

					form.numBoxStartPayment.Text = this.dataGridViewPositions.CurrentRow.Cells["StartSalary"].Value.ToString();
					form.numBoxBasePayment.Text = this.dataGridViewPositions.CurrentRow.Cells["BaseSalary"].Value.ToString();
					form.numBoxAddon.Text = this.dataGridViewPositions.CurrentRow.Cells["SalaryAddon"].Value.ToString();
					form.numBoxScienceAddon.Text = this.dataGridViewPositions.CurrentRow.Cells["ScienceAddon"].Value.ToString();
					form.numBoxOtherAddon.Text = this.dataGridViewPositions.CurrentRow.Cells["OtherAddon"].Value.ToString();

					if (form.ShowDialog() == DialogResult.OK)
					{
						Dictionary<string, object> Dict = new Dictionary<string, object>();
						this.PopulatePackageFromForm(form, Dict);

						try
						{
							Dictionary<string, object> hDict = new Dictionary<string, object>();
							Dictionary<string, object> pDict = new Dictionary<string, object>();
							hDict.Add("changefrom", this.dataGridViewPositions.CurrentRow.Cells["nameofposition"].Value.ToString() + " " + this.dataGridViewPositions.CurrentRow.Cells["staffcount"].Value.ToString());
							hDict.Add("changeto", Dict["NameOfPosition"] + " " + Dict["StaffCount"]);
							hDict.Add("changeoperation", "Корекция на длъжност");
                            //hDict.Add("changedate", DataAction.ConvertDateToMySqlU(DateTime.Now, mainForm.DataBaseTypes));
                            hDict.Add("changedate", DateTime.Now);
							da.UniversalInsertParam(TableNames.StructureHistory, hDict, "id", TransactionComnmand.BEGIN_TRANSACTION);

							this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", Dict, this.dataGridViewPositions.CurrentRow.Cells["ID"].Value.ToString(), TransactionComnmand.COMMIT_TRANSACTION);

							
							Dict.Add("ID", this.dataGridViewPositions.CurrentRow.Cells["ID"].Value.ToString());
							UpdatePackageInTable(Dict); //Updates only if ID is correct

							pDict.Add("position", Dict["NameOfPosition"]);
							pDict.Add("EKDACode", Dict["EKDACode"]);
							pDict.Add("EKDALevel", Dict["EKDALevel"]);
							pDict.Add("NKPCode", Dict["NKPCode"]);
							pDict.Add("NKPLevel", Dict["NKPLevel"]);
							pDict.Add("Rang", Dict["Rang"]);

							string where = " isactive = 1 AND positionid = '" + Dict["ID"] + "'";
							this.da.UniversalUpdateWhere(TableNames.PersonAssignment, where, pDict); 

						}
						catch (System.Exception ex)
						{
							MessageBox.Show(ex.Message, "Грешни данни EditPosition in formStructurteNew");
							Dict.Add("ID", "0");
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonFile_Click(object sender, System.EventArgs e)
		{
			this.dataGridNames_DoubleClick(sender, e);
		}

		private void JustifyGrid(DataGridView dg)
		{
			try
			{
				foreach (DataGridViewColumn columnStyle in dg.Columns)
				{
					switch (columnStyle.Name.ToLower())
					{
						case "typeposition":
							{
								columnStyle.HeaderText = "Вид длъжност";
								columnStyle.Visible = true;
								break;
							}
						case "country":
							{
								columnStyle.HeaderText = "Държава";
								columnStyle.Visible = true;
								break;
							}
						case "town":
							{
								columnStyle.HeaderText = "Град";
								columnStyle.Visible = true;
								break;
							}
						case "borntown":
							{
								columnStyle.HeaderText = "Месторождение";
								columnStyle.Visible = true;
								break;
							}
						case "region":
							{
								columnStyle.HeaderText = "Област";
								columnStyle.Visible = true;
								break;
							}
						case "familystatus":
							{
								columnStyle.HeaderText = "Семеен статус";
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
						case "sciencetitle":
							{
								columnStyle.HeaderText = "Научно звание";
								columnStyle.Visible = true;
								break;
							}
						case "languages":
							{
								columnStyle.HeaderText = "Чужди езици";
								columnStyle.Visible = true;
								break;
							}
						case "name":
							{
								columnStyle.HeaderText = "Име";
								columnStyle.Visible = true;
								break;
							}
						case "egn":
							{
								columnStyle.HeaderText = "ЕГН";
								columnStyle.Visible = true;
								break;
							}
						case "sex":
							{
								columnStyle.HeaderText = "Пол";
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
						case "worktime":
							{
								columnStyle.HeaderText = "Работно време";
								columnStyle.Visible = true;
								break;
							}
						case "assignedat":
							{
								columnStyle.HeaderText = "Назначен на";
								columnStyle.Visible = true;
								break;
							}
						case "penaltydate":
							{
								columnStyle.HeaderText = "Дата на наказанието";
								columnStyle.Visible = true;
								break;
							}
						case "reason":
							{
								columnStyle.HeaderText = "Причина";
								columnStyle.Visible = true;
								break;
							}
						case "numberorder":
							{
								columnStyle.HeaderText = "Номер на заповед";
								columnStyle.Visible = true;
								break;
							}
						case "fromdate":
							{
								columnStyle.HeaderText = "Дата на постановлението";
								columnStyle.Visible = true;
								break;
							}
						case "todate":
							{
								columnStyle.HeaderText = "До дата";
								columnStyle.Visible = true;
								break;
							}
						case "countdays":
							{
								columnStyle.HeaderText = "Брой дни";
								columnStyle.Visible = true;
								break;
							}
						case "typeabsence":
							{
								columnStyle.HeaderText = "Вид отсъствие";
								columnStyle.Visible = true;
								break;
							}

						case "nameofposition":
							{
								columnStyle.HeaderText = "Длъжност";
								columnStyle.Visible = true;
								break;
							}
						case "ekdapaylevel":
							{
								columnStyle.HeaderText = "Ниво на заплата";
								columnStyle.Visible = true;
								break;
							}
						//						case "PorNum":
						//						{
						//							columnStyle.HeaderText = "Пореден номер"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
						//						case "EKDACode":
						//						{
						//							columnStyle.HeaderText = "Длъжностно ниво"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
						//						case "PMS":
						//						{
						//							columnStyle.HeaderText = "ПМС"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
						//						case "VOS":
						//						{
						//							columnStyle.HeaderText = "ВОС"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
						case "nkpcode":
							{
								columnStyle.HeaderText = "Код по НКПД";
								columnStyle.Visible = true;
								break;
							}
						//						case "rang":
						//						{
						//							columnStyle.HeaderText = "Ранг"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
						case "experience":
							{
								columnStyle.HeaderText = "Опит";
								columnStyle.Visible = true;
								break;
							}
						//						case "MinSalary":
						//						{
						//							columnStyle.HeaderText = "Минимална заплата"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
						//						case "MaxSalary":
						//						{
						//							columnStyle.HeaderText = "Максимална заплата"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
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
						//						case "KVS":
						//						{
						//							columnStyle.HeaderText = "КВС"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
						case "nummonths":
							{
								columnStyle.HeaderText = "Брой месеци";
								columnStyle.Visible = true;
								break;
							}
						//						case "SecurityLevel":
						//						{
						//							columnStyle.HeaderText = "Ниво на сигурност"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
						//						case "OtherRequirements":
						//						{
						//							columnStyle.HeaderText = "Други"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
						//						case "Notes":
						//						{
						//							columnStyle.HeaderText = "Забележки"; 
						//							columnStyle.Visible = true; 
						//							break;
						//						}
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
						case "stafforder":
							{
								columnStyle.HeaderText = "Ред";
								columnStyle.Visible = true;
								break;
							}
						case "id":
							{
								columnStyle.HeaderText = "ID";
								columnStyle.Visible = true;
								break;
							}
						default:
							{
								columnStyle.Visible = false;
								break;
							}
					}
				}
			}
			catch(System.Exception e)
			{
				MessageBox.Show(e.Message, "Error");
			}
		}
		#endregion		
		
		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.main.nomenclaatureData.dtTreeTable = this.dtNodes;
			this.Close();
		}

		# region Event Reactions	

		private void dataGridNames_DoubleClick(object sender, System.EventArgs e)
		{
			if( this.dataGridViewNames.CurrentRow != null)
			{
				formPersonalData form = new formPersonalData( this.dataGridViewNames.CurrentRow.Cells["id"].Value.ToString(), this.main, false ); 
				form.ShowDialog( this );
				this.treeView1_AfterSelect(sender, null);
			}
		}
	
		#endregion		

		private void buttonNewEmployee_Click(object sender, System.EventArgs e)
		{
			formPersonalData form = new formPersonalData(this.main, false ); // IsFired = false Тук работим само с назначени
			form.ShowDialog(this);
			this.treeView1_AfterSelect(sender, null);
		}

		private void ContextMenuUp_Click(object sender, System.EventArgs e)
		{
			if (this.dataGridViewPositions.CurrentRow == null)
				return;

			if (this.dataGridViewPositions.CurrentRow.Index > 0)
			{
				int Temp1, Temp2;
				try
				{
					Dictionary<string, object> Dict1, Dict2;
					Dict1 = new Dictionary<string, object>();
					Dict2 = new Dictionary<string, object>();
					Temp1 = int.Parse(this.dataGridViewPositions.CurrentRow.Cells["stafforder"].Value.ToString());
					Temp2 = int.Parse(this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index - 1].Cells["staffOrder"].Value.ToString());

					Dict1.Add("stafforder", Temp1.ToString());
					Dict2.Add("stafforder", Temp2.ToString());

					if (this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", Dict2, this.dataGridViewPositions.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.BEGIN_TRANSACTION))
					{
						if (this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", Dict1, this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index - 1].Cells["id"].Value.ToString(), TransactionComnmand.COMMIT_TRANSACTION) == false)
						{
							MessageBox.Show("Грешка при редактиране на структурата на организацията", ErrorMessages.NoConnection);
							return;
						}
					}
					else
					{
						MessageBox.Show("Грешка при редактиране на структурата на организацията", ErrorMessages.NoConnection);
						return;
					}

					if (Temp1 == Temp2)
					{//strange?
						MessageBox.Show("Грешни дании за структура на организацията", "Грешка");
					}
					else
					{
						this.dataGridViewPositions.CurrentRow.Cells["stafforder"].Value = Temp2;
						this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index - 1].Cells["staffOrder"].Value = Temp1;
					}

					Temp1 = this.dataGridViewPositions.CurrentRow.Index;
					this.treeView1_AfterSelect(sender, null);
					this.dataGridViewPositions.CurrentCell = this.dataGridViewPositions.Rows[Temp1 - 1].Cells["nameofposition"];
				}
				catch (Exception ex)
				{
					ErrorLog.WriteException(ex, ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void ContextMenuDown_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewPositions.CurrentRow == null)
				{
					return;
				}
				if (this.dataGridViewPositions.CurrentRow.Index < this.dataGridViewPositions.Rows.Count)
				{
					int Temp1, Temp2;
					try
					{
						Dictionary<string, object> Dict1, Dict2;
						Dict1 = new Dictionary<string, object>();
						Dict2 = new Dictionary<string, object>();

						Temp1 = int.Parse(this.dataGridViewPositions.CurrentRow.Cells["stafforder"].Value.ToString());
						Temp2 = int.Parse(this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index + 1].Cells["staffOrder"].Value.ToString());

						Dict1.Add("stafforder", Temp1.ToString());
						Dict2.Add("stafforder", Temp2.ToString());

						if (this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", Dict2, this.dataGridViewPositions.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.BEGIN_TRANSACTION))
						{
							if (this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", Dict1, this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index + 1].Cells["id"].Value.ToString(), TransactionComnmand.COMMIT_TRANSACTION) == false)
							{
								MessageBox.Show("Грешка при редактиране на структурата на организацията", ErrorMessages.NoConnection);
								return;
							}
						}
						else
						{
							MessageBox.Show("Грешка при редактиране на структурата на организацията", ErrorMessages.NoConnection);
							return;
						}

						if (Temp1 == Temp2)
						{//strange?
							MessageBox.Show("Грешни дании за структура на организацията", "Грешка");
						}
						else
						{
							this.dataGridViewPositions.CurrentRow.Cells["stafforder"].Value = Temp2;
							this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index + 1].Cells["staffOrder"].Value = Temp1;
						}

						Temp1 = this.dataGridViewPositions.CurrentRow.Index;
						this.treeView1_AfterSelect(sender, null);
						this.dataGridViewPositions.CurrentCell = this.dataGridViewPositions.Rows[Temp1 + 1].Cells["nameofposition"];
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void ContextMenuTreeUp_Click(object sender, System.EventArgs e)
		{
			if (this.treeView1.SelectedNode == null)
				return;

			var prevNode = this.treeView1.SelectedNode.PrevNode;
			if(prevNode == null)
			{
				return;
			}

			int Temp1, Temp2;
			try
			{
				Dictionary<string, object> Dict1, Dict2;
				Dict1 = new Dictionary<string, object>();
				Dict2 = new Dictionary<string, object>();

				Temp1 = int.Parse(this.dataGridViewPositions.CurrentRow.Cells["stafforder"].Value.ToString());
				Temp2 = int.Parse(this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index + 1].Cells["staffOrder"].Value.ToString());

				Dict1.Add("stafforder", Temp1.ToString());
				Dict2.Add("stafforder", Temp2.ToString());

				if (this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", Dict2, this.dataGridViewPositions.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.BEGIN_TRANSACTION))
				{
					if (this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", Dict1, this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index + 1].Cells["id"].Value.ToString(), TransactionComnmand.COMMIT_TRANSACTION) == false)
					{
						MessageBox.Show("Грешка при редактиране на структурата на организацията", ErrorMessages.NoConnection);
						return;
					}
				}
				else
				{
					MessageBox.Show("Грешка при редактиране на структурата на организацията", ErrorMessages.NoConnection);
					return;
				}

				if (Temp1 == Temp2)
				{//strange?
					MessageBox.Show("Грешни дании за структура на организацията", "Грешка");
				}
				else
				{
					this.dataGridViewPositions.CurrentRow.Cells["stafforder"].Value = Temp2;
					this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index + 1].Cells["staffOrder"].Value = Temp1;
				}

				Temp1 = this.dataGridViewPositions.CurrentRow.Index;
				this.treeView1_AfterSelect(sender, null);
				this.dataGridViewPositions.CurrentCell = this.dataGridViewPositions.Rows[Temp1 + 1].Cells["nameofposition"];
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}

		}

		private void ContextMenuTreeDown_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewPositions.CurrentRow == null)
				{
					return;
				}
				if (this.dataGridViewPositions.CurrentRow.Index < this.dataGridViewPositions.Rows.Count)
				{
					int Temp1, Temp2;
					try
					{
						Dictionary<string, object> Dict1, Dict2;
						Dict1 = new Dictionary<string, object>();
						Dict2 = new Dictionary<string, object>();

						Temp1 = int.Parse(this.dataGridViewPositions.CurrentRow.Cells["stafforder"].Value.ToString());
						Temp2 = int.Parse(this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index + 1].Cells["staffOrder"].Value.ToString());

						Dict1.Add("stafforder", Temp1.ToString());
						Dict2.Add("stafforder", Temp2.ToString());

						if (this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", Dict2, this.dataGridViewPositions.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.BEGIN_TRANSACTION))
						{
							if (this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", Dict1, this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index + 1].Cells["id"].Value.ToString(), TransactionComnmand.COMMIT_TRANSACTION) == false)
							{
								MessageBox.Show("Грешка при редактиране на структурата на организацията", ErrorMessages.NoConnection);
								return;
							}
						}
						else
						{
							MessageBox.Show("Грешка при редактиране на структурата на организацията", ErrorMessages.NoConnection);
							return;
						}

						if (Temp1 == Temp2)
						{//strange?
							MessageBox.Show("Грешни дании за структура на организацията", "Грешка");
						}
						else
						{
							this.dataGridViewPositions.CurrentRow.Cells["stafforder"].Value = Temp2;
							this.dataGridViewPositions.Rows[this.dataGridViewPositions.CurrentRow.Index + 1].Cells["staffOrder"].Value = Temp1;
						}

						Temp1 = this.dataGridViewPositions.CurrentRow.Index;
						this.treeView1_AfterSelect(sender, null);
						this.dataGridViewPositions.CurrentCell = this.dataGridViewPositions.Rows[Temp1 + 1].Cells["nameofposition"];
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonCharacteristics_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dtPos.Rows.Count == 0)
				{
					MessageBox.Show("Не сте избрали длъжност");
					return;
				}
				DataRow row = this.dtPos.Rows.Find(this.dataGridViewPositions.CurrentRow.Cells["id"].Value);
				formCharacteristicAdd form = new formCharacteristicAdd(this.main);
				form.SetControlData(row);
				form.ShowDialog();
				if (form.DialogResult == DialogResult.OK)
				{
					form.GetControlData(row);
					Dictionary<string, object> Dict = new Dictionary<string, object>();

					Dict.Add("AdditionNumber", row["AdditionNumber"].ToString());
					try
					{
						Dict.Add("Busy", row["Busy"].ToString());
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message);
					}
					Dict.Add("Education", row["Education"].ToString());
					Dict.Add("EKDACode", row["EKDACode"].ToString());
					Dict.Add("EKDALevel", row["EKDALevel"].ToString());
					Dict.Add("Experience", row["Experience"].ToString());
					try
					{
						Dict.Add("Free", row["Free"].ToString());
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message);
					}
					Dict.Add("KVS", row["KVS"].ToString());
					Dict.Add("Law", row["Law"].ToString());
					Dict.Add("MaxSalary", row["MaxSalary"].ToString());
					Dict.Add("MinSalary", row["MinSalary"].ToString());
					Dict.Add("NKPCode", row["NKPCode"].ToString());
					Dict.Add("NKPLevel", row["NKPLevel"].ToString());
					Dict.Add("Notes", row["Notes"].ToString());
					Dict.Add("NumMonths", row["NumMonths"].ToString());
					Dict.Add("OtherRequirements", row["OtherRequirements"].ToString());
					try
					{
						Dict.Add("Par", row["Par"].ToString());
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message);
					}
					Dict.Add("PMS", row["PMS"].ToString());
					Dict.Add("PorNum", row["PorNum"].ToString());
					Dict.Add("NameOfPosition", row["NameOfPosition"].ToString());
					Dict.Add("Rang", row["Rang"].ToString());
					Dict.Add("SecurityLevel", row["SecurityLevel"].ToString());
					try
					{
						Dict.Add("StaffCount", row["StaffCount"].ToString());
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message);
					}
					Dict.Add("VOS", row["VOS"].ToString());
					Dict.Add("TypePosition", row["TypePosition"].ToString());

					Dict.Add("NKPClass", row["NKPClass"].ToString());
					Dict.Add("BasicDuties", row["BasicDuties"].ToString());
					Dict.Add("BasicResponsibilities", row["BasicResponsibilities"].ToString());
					Dict.Add("Competence", row["Competence"].ToString());
					Dict.Add("Connections", row["Connections"].ToString());
					Dict.Add("Requirements", row["Requirements"].ToString());

					if (this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id",   Dict, row["ID"].ToString(), TransactionComnmand.NO_TRANSACTION) == false)
					{
						MessageBox.Show("Грешка при редакция на длъжностна характеристика");
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

        private void buttonHistory_Click(object sender, EventArgs e)
        {
			try
			{
				DataTable dt = this.da.SelectWhere(TableNames.StructureHistory, "*", "");
				dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
				CommonNomenclature form = new CommonNomenclature(TableNames.StructureHistory, "История на организацията", dt, this.main);
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
        }

        private void FormStructureNew_Resize(object sender, EventArgs e)
        {
            int size = this.Width;
            int location = size / 2 - 60;
            this.buttonHistory.Left = location;
            location = size / 2 + 60;
            this.buttonCancel.Left = location;
        }

		private void dataGridViewPositions_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.dataGridViewPositions.ContextMenu.Show(this.dataGridViewPositions, new Point(e.X, e.Y));
			}
		}
	}
}