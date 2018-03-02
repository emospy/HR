using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace LichenSystaw2004
{
	/// <summary>
	/// Summary description for formStructureFirm.
	/// </summary>
	public class formStructureFirm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeViewFirm;
		private System.Windows.Forms.Button buttonDirection;
		private System.Windows.Forms.TextBox textBoxDirection;
        private mainForm main;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textBox1DIrectionAdd;
		private System.Windows.Forms.TextBox textBoxControl;
		private System.Windows.Forms.TextBox textBoxTeam;
		private System.Windows.Forms.Button buttonChange;
        private string[] structureName;
		System.Random rand = new Random( System.DateTime.Now.Day + 
			System.DateTime.Now.DayOfYear + System.DateTime.Now.Year + System.DateTime.Now.Second/10 );
		private System.Windows.Forms.Button buttonAddChild;
		private System.Windows.Forms.TextBox textBoxAddChild;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.GroupBox groupBoxNames;
		DataLayer.FirmStrucure firm;
	    
		public formStructureFirm( mainForm main )
		{
			InitializeComponent();
          
			this.main = main;

			firm = new DataLayer.FirmStrucure( main.connString );
			structureName = this.main.nomenclaatureData.FirmStructure;

			this.textBox1DIrectionAdd.Text = structureName[0];
			this.textBoxControl.Text = structureName[1];
			this.textBoxTeam.Text = structureName[2];

			this.LoadNode();
            
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
			this.treeViewFirm = new System.Windows.Forms.TreeView();
			this.buttonDirection = new System.Windows.Forms.Button();
			this.textBoxDirection = new System.Windows.Forms.TextBox();
			this.textBox1DIrectionAdd = new System.Windows.Forms.TextBox();
			this.textBoxControl = new System.Windows.Forms.TextBox();
			this.textBoxTeam = new System.Windows.Forms.TextBox();
			this.buttonChange = new System.Windows.Forms.Button();
			this.buttonAddChild = new System.Windows.Forms.Button();
			this.textBoxAddChild = new System.Windows.Forms.TextBox();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.groupBoxNames = new System.Windows.Forms.GroupBox();
			this.groupBoxNames.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeViewFirm
			// 
			this.treeViewFirm.ImageIndex = -1;
			this.treeViewFirm.Location = new System.Drawing.Point(24, 32);
			this.treeViewFirm.Name = "treeViewFirm";
			this.treeViewFirm.SelectedImageIndex = -1;
			this.treeViewFirm.Size = new System.Drawing.Size(208, 200);
			this.treeViewFirm.TabIndex = 0;
			// 
			// buttonDirection
			// 
			this.buttonDirection.Location = new System.Drawing.Point(288, 72);
			this.buttonDirection.Name = "buttonDirection";
			this.buttonDirection.TabIndex = 1;
			this.buttonDirection.Text = "Добави";
			this.buttonDirection.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// textBoxDirection
			// 
			this.textBoxDirection.Location = new System.Drawing.Point(256, 32);
			this.textBoxDirection.Name = "textBoxDirection";
			this.textBoxDirection.Size = new System.Drawing.Size(152, 20);
			this.textBoxDirection.TabIndex = 2;
			this.textBoxDirection.Text = "";
			// 
			// textBox1DIrectionAdd
			// 
			this.textBox1DIrectionAdd.Location = new System.Drawing.Point(16, 16);
			this.textBox1DIrectionAdd.Name = "textBox1DIrectionAdd";
			this.textBox1DIrectionAdd.Size = new System.Drawing.Size(128, 20);
			this.textBox1DIrectionAdd.TabIndex = 3;
			this.textBox1DIrectionAdd.Text = "";
			// 
			// textBoxControl
			// 
			this.textBoxControl.Location = new System.Drawing.Point(16, 48);
			this.textBoxControl.Name = "textBoxControl";
			this.textBoxControl.Size = new System.Drawing.Size(128, 20);
			this.textBoxControl.TabIndex = 4;
			this.textBoxControl.Text = "";
			// 
			// textBoxTeam
			// 
			this.textBoxTeam.Location = new System.Drawing.Point(16, 80);
			this.textBoxTeam.Name = "textBoxTeam";
			this.textBoxTeam.Size = new System.Drawing.Size(128, 20);
			this.textBoxTeam.TabIndex = 5;
			this.textBoxTeam.Text = "";
			// 
			// buttonChange
			// 
			this.buttonChange.Location = new System.Drawing.Point(40, 112);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.TabIndex = 6;
			this.buttonChange.Text = "Промени";
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// buttonAddChild
			// 
			this.buttonAddChild.Location = new System.Drawing.Point(272, 152);
			this.buttonAddChild.Name = "buttonAddChild";
			this.buttonAddChild.Size = new System.Drawing.Size(120, 23);
			this.buttonAddChild.TabIndex = 7;
			this.buttonAddChild.Text = "Добави наследник";
			this.buttonAddChild.Click += new System.EventHandler(this.buttonAddChild_Click);
			// 
			// textBoxAddChild
			// 
			this.textBoxAddChild.Location = new System.Drawing.Point(264, 112);
			this.textBoxAddChild.Name = "textBoxAddChild";
			this.textBoxAddChild.Size = new System.Drawing.Size(152, 20);
			this.textBoxAddChild.TabIndex = 8;
			this.textBoxAddChild.Text = "";
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(296, 192);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.TabIndex = 9;
			this.buttonDelete.Text = "Изтрий";
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// groupBoxNames
			// 
			this.groupBoxNames.Controls.Add(this.buttonChange);
			this.groupBoxNames.Controls.Add(this.textBoxControl);
			this.groupBoxNames.Controls.Add(this.textBoxTeam);
			this.groupBoxNames.Controls.Add(this.textBox1DIrectionAdd);
			this.groupBoxNames.Location = new System.Drawing.Point(464, 56);
			this.groupBoxNames.Name = "groupBoxNames";
			this.groupBoxNames.Size = new System.Drawing.Size(160, 136);
			this.groupBoxNames.TabIndex = 10;
			this.groupBoxNames.TabStop = false;
			this.groupBoxNames.Text = "Наименования на звената";
			// 
			// formStructureFirm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 286);
			this.Controls.Add(this.groupBoxNames);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.textBoxAddChild);
			this.Controls.Add(this.textBoxDirection);
			this.Controls.Add(this.buttonAddChild);
			this.Controls.Add(this.buttonDirection);
			this.Controls.Add(this.treeViewFirm);
			this.Name = "formStructureFirm";
			this.Text = "Структура на фирмата";
			this.groupBoxNames.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		void LoadNode()
		{
//            NodeIDs ids1 = new NodeIDs();
//			NodeIDs ids2 = new NodeIDs();
//			NodeIDs ids3 = new NodeIDs();

//			foreach( Node nodes1 in this.main.nomenclaatureData.arrDirection )
//			{
//				TreeNode Node1 = new TreeNode( structureName[0] +"  "+ nodes1.NodeName);
//				ids1.ID = nodes1.ID;
//				ids1.ID2 = nodes1.ID2;
//				Node1.Tag = ids1;
//				
//				foreach( Nodes nodes2 in this.main.nomenclaatureData.arrControl )
//				{
//					if( nodes2.ID == nodes1.ID )
//					{
//						TreeNode Node2 = new TreeNode( structureName[1] +"  "+ nodes2.NodeName);
//						ids2.ID = nodes2.ID;
//						ids2.ID2 = nodes2.ID2;
//						Node2.Tag = ids2;
//						#region 3
//						foreach( Nodes nodes3 in this.main.nomenclaatureData.arrTeam )
//						{
//							if( nodes3.ID == nodes2.ID2 )
//							{
//								TreeNode Node3 = new TreeNode( structureName[2] +"  "+ nodes3.NodeName);
//								ids3.ID = nodes3.ID;
//								ids3.ID2 = nodes3.ID2;
//								Node3.Tag = ids3;
//								Node2.Nodes.Add( Node3 );
//							}
//						}
//						#endregion
//						Node1.Nodes.Add( Node2 );
//					}
//				}
//				this.treeViewFirm.Nodes.Add( Node1 );
//			}
		}
		internal string GenerateUniqueID()
		{
			// Не е добър алгоритъм, понякога може да се повторят ИД, Trqbwa da se promeni
			for( int i = 0; i < rand.Next( 30 ); i++ )
			{
				rand.Next();
			}
			return rand.Next().ToString();
		}
		private void buttonNew_Click(object sender, System.EventArgs e)
		{

//			 string deep  = this.GetDeepOfNodes( this.treeViewFirm.SelectedNode);
//             string nodeName = this.textBoxDirection.Text;
//			 string parentID = "0";
//			 string parentID2 = "0";
//			if( deep == "1" )
//			{
//				parentID = ((NodeIDs)this.treeViewFirm.SelectedNode.Tag).ID2;
//			}
//			else
//			{
//                parentID = ((NodeIDs)this.treeViewFirm.SelectedNode.Tag).ID;
//			}
//
//			if( (Button) sender == this.buttonAddChild )
//			{
//				// ако сме го извикали от Добави наследник
//				int d = int.Parse( deep );
//				d++;
//				deep = d.ToString();
//                nodeName = this.textBoxAddChild.Text;
//				if( deep == "2" )
//				{
//					parentID2 = parentID = ((NodeIDs)this.treeViewFirm.SelectedNode.Tag).ID;
//					parentID = ((NodeIDs)this.treeViewFirm.SelectedNode.Tag).ID2;
//				}
//
//
//			}
//			AddNodeToArrays( deep, nodeName , parentID, parentID2 );
//			
		}
		public void AddNodeToArrays( string deep, string nodeName, string parentID,string parentID2 )
		{

//			Nodes nodes = new Nodes();
//			TreeNode treeNode = new TreeNode();
//			NodeIDs ids = new NodeIDs();
//			nodes.NodeName = nodeName;
//           	switch( deep )
//			{
//				case "0" : 
//					     nodes.ID = this.GenerateUniqueID();
//				        	nodes.ID2 = "0";
//					     this.firm.InsertNodeInTable( "Direction", nodes.ID ,nodes.ID2 , nodes.NodeName );
//					     this.main.nomenclaatureData.arrDirection.Add( nodes );
////					     treeNode.Text = nodes.NodeName;
////					     ids.ID  = nodes.ID;
////					     ids.ID2 = "0";
////					     treeNode.Tag = ids;
////					      this.treeViewFirm.Nodes.Add( treeNode );
//					this.treeViewFirm.Nodes.Clear();
//					this.LoadNode();
//					break;
//				case "1" :
//					nodes.ID = parentID;
//					nodes.ID2 = this.GenerateUniqueID();
//					this.firm.InsertNodeInTable( "Control", nodes.ID, nodes.ID2, nodes.NodeName );
//					this.main.nomenclaatureData.arrControl.Add( nodes );
//					this.treeViewFirm.Nodes.Clear();
//					this.LoadNode();
////					treeNode.Text = nodes.NodeName;
////					ids.ID  = nodes.ID;
////					ids.ID2 = nodes.ID2;
////					treeNode.Tag = ids;
////					foreach( TreeNode tree in  this.treeViewFirm.Nodes )
////					{
////						if( nodes.ID ==( ( NodeIDs ) tree.Tag).ID )
////						{
////							tree.Nodes.Add( treeNode );
////						}
////					}
//					break;
//				case "2" :
//					nodes.ID = parentID;
//					nodes.ID2 = parentID2;
//					this.firm.InsertNodeInTable( "Team", nodes.ID, nodes.ID2, nodes.NodeName );
//					this.main.nomenclaatureData.arrTeam.Add( nodes );
//					this.treeViewFirm.Nodes.Clear();
//					this.LoadNode();
////					treeNode.Text = nodes.NodeName;
////					ids.ID  = nodes.ID;
////					ids.ID2 = nodes.ID2;
////					treeNode.Tag = ids;
////					foreach( TreeNode tree in  this.treeViewFirm.Nodes )
////					{
////						if( nodes.ID2 == ( ( NodeIDs ) tree.Tag).ID) 
////						{
////							foreach( TreeNode tree2 in tree.Nodes )
////							{
////								if( nodes.ID == ( ( NodeIDs ) tree2.Tag).ID2 )
////								{
////									tree2.Nodes.Add( treeNode );
////								}
////							}
////							
////						}
////					}
//					break;
//			}
		}
		private string GetDeepOfNodes( TreeNode node )
		{
			for( int i = 0; i < this.structureName.Length; i++ )
			{
				if( node.Parent == null )
				{
					return i.ToString();
				}
				else
				{
					node = node.Parent;
				}
			}
           return (2).ToString();
		}
		private void buttonChange_Click(object sender, System.EventArgs e)
		{
			 structureName[0] = this.textBox1DIrectionAdd.Text;
			 structureName[1] = this.textBoxControl.Text;
			 structureName[2] = this.textBoxTeam.Text;
			this.firm.UpdateFirmStructure( "FirmStructure", structureName );
		}

		private void buttonAddChild_Click(object sender, System.EventArgs e)
		{
			int deep  = int.Parse( this.GetDeepOfNodes( this.treeViewFirm.SelectedNode));
			if( deep >= ( this.structureName.Length - 1) )
			{
				MessageBox.Show( "Структурата на Организациято не може да има по долни звена" );
			}
			else
			{
				this.buttonNew_Click( this.buttonAddChild, e );
			}

		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
//			ArrayList arr = new ArrayList();
//			ArrayList arr2 = new ArrayList();
//			string table="Direction";
//			int i = 0;
//			this.GetAllChildNodes( this.treeViewFirm.SelectedNode, arr, arr2 );
//			foreach( Nodes nodes in arr )
//			{
//
//				switch( arr2[i].ToString() )
//				{
//					case "0" : table = "Direction";
//						break;
//					case "1" : table = "Control";
//						break;
//					case "2" : table = "Team";
//						break;
//				}
//				this.firm.DeleteNodeInTable( table, nodes.ID, nodes.ID2, nodes.NodeName );
//				i++;
//			}
//			this.treeViewFirm.Nodes.Remove( this.treeViewFirm.SelectedNode );
//			
			
		}
		private void GetAllChildNodes( TreeNode node, ArrayList arr, ArrayList arr2)
		{
//			Nodes nodes = new Nodes();
//			int deep  = int.Parse( this.GetDeepOfNodes( node ) );
//			foreach( TreeNode node1 in node.Nodes )
//			{
//				nodes.NodeName = node1.Text;
//				nodes.ID = (( NodeIDs )node1.Tag).ID;
//				nodes.ID2 = (( NodeIDs )node1.Tag).ID2;
//				arr.Add( nodes );
//				arr2.Add( Convert.ToString(deep + 1));
//				foreach( TreeNode node2 in node1.Nodes )
//				{
//					nodes.NodeName = node2.Text;
//					nodes.ID = (( NodeIDs )node2.Tag).ID;
//					nodes.ID2 = (( NodeIDs )node2.Tag).ID2;
//					arr.Add( nodes );
//					arr2.Add( Convert.ToString(deep + 2));
//				}
//			}
//			nodes.NodeName = node.Text;
//			nodes.ID = (( NodeIDs )node.Tag).ID;
//			nodes.ID2 = (( NodeIDs )node.Tag).ID2;
//			arr.Add( nodes );
//			arr2.Add( deep.ToString() );
		}
	}
}
