using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HRDataLayer;
using Microsoft.Win32;
using Novacode;
using Telerik.Windows.Controls;
using Window = System.Windows.Window;

namespace HolidayPlan
{
	/// <summary>
	/// Preparations on site needed
	/// 1. Add column TreeOrder ot hr_newtree2
	/// 2. Initialise order = id
	/// 3. set all nonexistant position ids from hr_personassignment to null (update t1 set positionid = null from hr_personassignment as t1 left join hr_firmpersonal3 as t2 on t1.positionID = t2.id where t2.id is null)
	/// 4. Create DB relationship between HR_personaassignment and hr_firmpersonal3
	/// 5. Afret finishing remove relationship and delete the remaining unused nodes an positions
	/// </summary>
	public partial class OrganisationStructure : Window
	{
		private Entities data;

		public OrganisationStructure(string connectionstring)
		{
			InitializeComponent();
			this.data = new Entities(connectionstring);
			this.dpDate.SelectedDate = DateTime.Now;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.PopulateTreeRoot(this.RadViewSource);
			this.PopulateTreeRoot(this.RadViewDestination);
			//ContextMenu menu = new ContextMenu();
			//this.RadView.ContextMenu = new ContextMenu();
			//MenuItem item = new MenuItem();
			//item.Click += ContextMenuTreeUp_Click;
			//item.Header = "Премести нагоре";

			//this.RadView.ContextMenu.Items.Add(item);
			//MenuItem item2 = new MenuItem();
			//item.Click += ContextMenuTreeDown_Click;
			//item.Header = "Премести надолу";
			//this.RadView.ContextMenu.Items.Add(item2);
			//this.RadView.ContextMenu = menu;
		}

		private void PopulateTreeRoot(RadTreeView Tree)
		{
			var rootItems = this.data.HR_Newtree2.Where(i => i.par == 0).OrderBy(i => i.TreeOrder).ToList();
			foreach (var item in rootItems)
			{
				RadTreeViewItem it = new RadTreeViewItem();
				it.Tag = item;
				it.Header = item.level;
				Tree.Items.Add(it);
				this.PopulateTreeNodes(item.id, it);
			}
		}

		private void PopulateTreeNodes(int p, RadTreeViewItem parent)
		{
			var lstItems = this.data.HR_Newtree2.Where(i => i.par == p).OrderBy(i => i.TreeOrder).ToList();
			foreach (var item in lstItems)
			{
				RadTreeViewItem it = new RadTreeViewItem();
				it.Tag = item;
				it.Header = item.level;
				parent.Items.Add(it);
				this.PopulateTreeNodes(item.id, it);
			}
		}

		private void MenuItemDown_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RadTreeViewItem item = (RadTreeViewItem)this.RadViewSource.SelectedItem;
				if (item == null)
				{
					return;
				}
				var prevItem = item.NextSiblingItem;
				if (prevItem == null)
				{
					return;
				}
				var node1 = (HR_Newtree2)item.Tag;
				var node2 = (HR_Newtree2)prevItem.Tag;

				int tmp = node1.TreeOrder;
				node1.TreeOrder = node2.TreeOrder;
				node2.TreeOrder = tmp;
				this.data.SaveChanges();

				var parentItem = item.ParentItem;

				if (parentItem != null)
				{
					while (parentItem.Items.Count > 0)
					{
						parentItem.Items.RemoveAt(0);
					}
					this.PopulateTreeNodes((int)node1.par, parentItem);
				}
				else
				{
					while (this.RadViewSource.Items.Count > 0)
					{
						this.RadViewSource.Items.RemoveAt(0);
					}
					this.PopulateTreeRoot(this.RadViewSource);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MenuItemUp_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RadTreeViewItem item = (RadTreeViewItem)this.RadViewSource.SelectedItem;
				if (item == null)
				{
					return;
				}
				var prevItem = item.PreviousSiblingItem;
				if (prevItem == null)
				{
					return;
				}
				var node1 = (HR_Newtree2)item.Tag;
				var node2 = (HR_Newtree2)prevItem.Tag;

				int tmp = node1.TreeOrder;
				node1.TreeOrder = node2.TreeOrder;
				node2.TreeOrder = tmp;
				this.data.SaveChanges();

				var parentItem = item.ParentItem;

				if (parentItem != null)
				{
					while (parentItem.Items.Count > 0)
					{
						parentItem.Items.RemoveAt(0);
					}
					this.PopulateTreeNodes((int)node1.par, parentItem);
				}
				else
				{
					while (this.RadViewSource.Items.Count > 0)
					{
						this.RadViewSource.Items.RemoveAt(0);
					}
					this.PopulateTreeRoot(this.RadViewSource);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void RadViewSource_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs eve)
		{
			var SelectedItem = (RadTreeViewItem)this.RadViewSource.SelectedItem;

			if (SelectedItem == null)
			{
				this.dgEmployeesView.ItemsSource = null;
				this.dgEmployeesView.Items.Refresh();
				return;
			}

			var selectedNode = (HR_Newtree2)(SelectedItem.Tag);

			//var query = from person in people
			//			join pet in pets on person equals pet.Owner into gj
			//			from subpet in gj.DefaultIfEmpty()
			//			select new { person.FirstName, PetName = (subpet == null ? String.Empty : subpet.Name) };

			//var lstEmployees = from assignment in this.data.HR_PersonAssignment
			//				   join pos in this.data.HR_FirmPersonal3 on assignment.positionID equals pos.id into asspos
			//				   from subq in asspos.DefaultIfEmpty()
			//				   where assignment.isActive == 1 
			//				   && subq. par == selectedNode.id

			//from maintable in Repo.T_Whatever
			//from xxx in Repo.T_ANY_TABLE.Where(join condition).DefaultIfEmpty()

			var lstEmployees = this.data.HR_PersonAssignment.Join(                      /// Source Collection
				  this.data.HR_FirmPersonal3,                        /// Inner Collection
				  p => p.positionID,                                /// PK
				  a => a.id,                                /// FK
				  (p, a) => new { Assignment = p, Position = a })
				  .Where(q => q.Assignment.isActive == 1 && q.Position.par == selectedNode.id)
				  .Select(e => new EmployeeOnPosition
				  {
					  Name = e.Assignment.HR_Person.name,
					  assignmentID = e.Assignment.id,
					  GlobalPositionID = e.Position.globalpositionid,
					  nodeID = e.Position.par,
					  personID = e.Assignment.HR_Person.id,
					  position = e.Assignment.position,
					  positionID = e.Assignment.positionID
				  }).OrderBy(p => p.Name).ToList();


			//var lstEmployees = this.data.HR_PersonAssignment.Where(a => a.isActive == 1 && a.HR_FirmPersonal3.par == selectedNode.id)
			//	.Select(e => new EmployeeOnPosition
			//				 {
			//					 Name = e.HR_Person.name,
			//					 assignmentID = e.id,
			//					 GlobalPositionID = e.HR_FirmPersonal3.globalpositionid,
			//					 nodeID = e.HR_FirmPersonal3.par,
			//					 personID = e.HR_Person.id,
			//					 position = e.position,
			//					 positionID = e.positionID
			//				 }).ToList();
			this.dgEmployeesView.ItemsSource = lstEmployees;
		}

		private void btnMoveNode_Click(object sender, RoutedEventArgs e)
		{
			var sourceNode = (RadTreeViewItem)this.RadViewSource.SelectedItem;
			var destNode = (RadTreeViewItem)this.RadViewDestination.SelectedItem;

			if (this.dpDate.SelectedDate == null)
			{
				MessageBox.Show("Не сте избрали дата за преназначаването!");
				return;
			}
			if (sourceNode == null)
			{
				MessageBox.Show("Не сте избрали звено източник!");
				return;
			}
			if (destNode == null)
			{
				MessageBox.Show("Не сте избрали звено приемник!");
				return;
			}

			if (MessageBox.Show(string.Format("Ще преместите звено {0}, като подчинено на звено {1}", sourceNode.Header, destNode.Header), "", MessageBoxButton.YesNo) == MessageBoxResult.No)
			{
				return;
			}

			var movedNode = (HR_Newtree2)sourceNode.Tag;
			var acceptorNode = (HR_Newtree2)destNode.Tag;
			movedNode.par = acceptorNode.id;

			if (sourceNode.ParentItem != null)
			{
				sourceNode.ParentItem.Items.Remove(sourceNode);
			}
			else
			{
				this.RadViewSource.Items.Remove(sourceNode);
			}


			//int count = destNode.Items.Count;
			destNode.Items.Add(sourceNode);
			//sourceNode = (RadTreeViewItem)destNode.Items.GetItemAt(count);

			//create new additional assignment for each employye in the node
			//also create new additional assignment for each of the moved node child employees

			string level1 = "", level2 = "", level3 = "", level4 = "";
			if (destNode.ParentItem == null)
			{
				level1 = destNode.Header.ToString();
				level2 = sourceNode.Header.ToString();
			}
			else if (destNode.ParentItem.ParentItem == null)
			{
				level1 = destNode.ParentItem.Header.ToString();
				level2 = destNode.Header.ToString();
				level3 = sourceNode.Header.ToString();
			}
			else
			{
				level1 = destNode.ParentItem.ParentItem.Header.ToString();
				level2 = destNode.ParentItem.Header.ToString();
				level3 = destNode.Header.ToString();
				level4 = sourceNode.Header.ToString();
			}

			this.MoveEmployees(sourceNode, level1, level2, level3, level4);

			this.data.SaveChanges();

			while (this.RadViewDestination.Items.Count > 0)
			{
				this.RadViewDestination.Items.RemoveAt(0);
			}
			while (this.RadViewSource.Items.Count > 0)
			{
				this.RadViewSource.Items.RemoveAt(0);
			}

			this.PopulateTreeRoot(this.RadViewSource);
			this.PopulateTreeRoot(this.RadViewDestination);
		}

		private void MoveEmployees(RadTreeViewItem sourceNode, string level1, string level2, string level3, string level4)
		{
			var movedNode = (HR_Newtree2)sourceNode.Tag;

			foreach (RadTreeViewItem node in sourceNode.Items)
			{
				if (level3 == "")
				{
					level3 = node.Header.ToString();
				}
				else if (level4 == "")
				{
					level4 = node.Header.ToString();
				}
				MoveEmployees(node, level1, level2, level3, level4);
			}

			var lstEmployees = this.data.HR_PersonAssignment.Join(                      /// Source Collection
				  this.data.HR_FirmPersonal3,                        /// Inner Collection
				 p => p.positionID,                                /// PK
				  a => a.id,                                /// FK
				  (p, a) => new { Assignment = p, Position = a })
				  .Where(q => q.Assignment.isActive == 1 && q.Position.par == movedNode.id)
				  .ToList();
			//get all active employee assignments from the current node
			//var lstEmployees = this.data.HR_PersonAssignment.Where(a => a.isActive == 1 && a.HR_FirmPersonal3.par == movedNode.id).ToList();

			foreach (var ea in lstEmployees)
			{
				ea.Assignment.isActive = 0;
				var newAssignment = FillAssignmentData(ea.Assignment, level1, level2, level3, level4);

				this.data.HR_PersonAssignment.AddObject(newAssignment);
			}
		}

		private HR_PersonAssignment FillAssignmentData(HR_PersonAssignment ea, string level1, string level2, string level3, string level4)
		{
			int years, months, days;
			this.CalcExperience(out years, out months, out days, (int)ea.parent);
			var newAssignment = new HR_PersonAssignment();
			newAssignment.AdditionalHoliday = ea.AdditionalHoliday;
			newAssignment.assignedAt = this.dpDate.SelectedDate.Value;
			newAssignment.assignReason = ea.assignReason;
			newAssignment.baseSalary = ea.baseSalary;
			newAssignment.classPercent = ea.classPercent;
			newAssignment.contract = ea.contract;
			newAssignment.contractExpiry = ea.contractExpiry;
			newAssignment.contractNumber = "";
			newAssignment.contractType = ea.contractType;
			newAssignment.days = days;
			newAssignment.EKDACode = ea.EKDACode;
			newAssignment.EKDALevel = ea.EKDALevel;
			newAssignment.ekdaPayDegree = ea.ekdaPayDegree;
			newAssignment.ExperienceCorrection = ea.ExperienceCorrection;
			newAssignment.exported = ea.exported;
			newAssignment.isActive = 1;
			newAssignment.IsAdditionalAssignment = 1;
			newAssignment.law = ea.law;

			newAssignment.level1 = level1;
			newAssignment.level2 = level2;
			newAssignment.level3 = level3;
			newAssignment.level4 = level4;


			newAssignment.MonthlyAddon = ea.MonthlyAddon;
			newAssignment.months = months;
			newAssignment.nkpCode = ea.nkpCode;
			newAssignment.nkpLevel = ea.nkpLevel;
			newAssignment.numberKids = ea.numberKids;
			newAssignment.NumHoliday = ea.NumHoliday;
			newAssignment.parent = ea.parent;
			newAssignment.ParentContractDate = ea.ParentContractDate;
			newAssignment.parentContractId = ea.parentContractId;
			newAssignment.pcontractreasoncode = ea.pcontractreasoncode;
			newAssignment.position = ea.position;
			newAssignment.positioneng = ea.positioneng;
			newAssignment.positionID = ea.positionID;
			newAssignment.PrevAssignmentID = ea.id;
			newAssignment.salaryAddon = ea.salaryAddon;
			newAssignment.staff = ea.staff;
			newAssignment.substitute = ea.substitute;
			newAssignment.TestContractDate = ea.TestContractDate;
			newAssignment.tutorabsencereason = ea.tutorabsencereason;
			newAssignment.tutorname = ea.tutorname;
			newAssignment.worktime = ea.worktime;
			newAssignment.YearlyAddon = ea.YearlyAddon;
			newAssignment.years = years;

			return newAssignment;
		}

		private void CalcExperience(out int years, out int months, out int days, int personid)
		{
			years = 0;
			months = 0;
			days = 0;

			var firstAssignment = this.data.HR_PersonAssignment.FirstOrDefault(p => p.parent == personid && p.IsAdditionalAssignment == 0);
			if (firstAssignment == null)
			{
				//MessageBox.Show("Грешка в назначението на " + )
				return;
			}

			DateTime AssignDate = (DateTime)firstAssignment.assignedAt;

			//int years = (int)this.dtAssignment.Rows[0]["Years"];
			if (DateTime.Compare(this.dpDate.SelectedDate.Value, AssignDate) >= 0)
			{
				int AssY, AssM, AssD, CYear, CDay, CMonth, TY, TM, TD;

				AssY = AssignDate.Year;
				AssM = AssignDate.Month;
				AssD = AssignDate.Day;
				CYear = this.dpDate.SelectedDate.Value.Year - AssY;
				if ((CMonth = this.dpDate.SelectedDate.Value.Month - AssM) < 0)
				{
					CYear--;
					CMonth += 12;
				}
				if ((CDay = this.dpDate.SelectedDate.Value.Day - AssD) <= 0)
				{
					CDay += 30;
					CMonth--;
					if (CMonth < 0)
					{
						CMonth += 12;
						CYear--;
					}
				}
				TY = TM = TD = 0;
				try
				{
					int fy = 0, fm = 0, fd = 0;

					int.TryParse(firstAssignment.years.ToString(), out fy);
					int.TryParse(firstAssignment.months.ToString(), out fm);
					int.TryParse(firstAssignment.days.ToString(), out fd);

					TY = CYear + fy;
					TM = CMonth + fm;
					TD = CDay + fd;
				}
				catch
				{
				}
				if (TD >= 30)
				{
					TM++;
					TD -= 30;
				}
				if (TM >= 12)
				{
					TM -= 12;
					TY++;
				}
				years = TY;
				months = TM;
				days = TD;
			}
		}

		private void btnJoinNodes_Click(object sender, RoutedEventArgs e)
		{
			var sourceNode = (RadTreeViewItem)this.RadViewSource.SelectedItem;
			var destNode = (RadTreeViewItem)this.RadViewDestination.SelectedItem;

			if (this.dpDate.SelectedDate == null)
			{
				MessageBox.Show("Не сте избрали дата за преназначаването!");
				return;
			}
			if (sourceNode == null)
			{
				MessageBox.Show("Не сте избрали звено източник!");
				return;
			}
			if (destNode == null)
			{
				MessageBox.Show("Не сте избрали звено приемник!");
				return;
			}

			if (MessageBox.Show(string.Format("Ще звено {0}, ще се слее със звено {1}", sourceNode.Header, destNode.Header), "", MessageBoxButton.YesNo) == MessageBoxResult.No)
			{
				return;
			}

			var movedNode = (HR_Newtree2)sourceNode.Tag;
			//var acceptorNode = (HR_Newtree2)destNode.Tag;
			var lstSourcePositions = this.data.HR_FirmPersonal3.Where(p => p.par == movedNode.id).ToList();

			this.MovePositions(lstSourcePositions, destNode);

			this.data.SaveChanges();

			var lstEmployeeAssignments = this.data.HR_PersonAssignment.Join(                      /// Source Collection
				  this.data.HR_FirmPersonal3,                        /// Inner Collection
				p => p.positionID,                                /// PK
				a => a.id,                                /// FK
				(p, a) => new { Assignment = p, Position = a })
							  .Where(q => q.Assignment.isActive == 1 && q.Position.par == movedNode.id)
							  .Select(p => p.Assignment).ToList();

			//var lstEmployeeAssignments = this.data.HR_PersonAssignment.Where(p => p.HR_FirmPersonal3.par == movedNode.id && p.isActive == 1).ToList();


			this.ReassignEmployees(sourceNode, destNode, lstEmployeeAssignments);
			//this.DeletePositions(movedNode);
			//this.DeleteNode(movedNode);
			this.data.SaveChanges();

			while (this.RadViewDestination.Items.Count > 0)
			{
				this.RadViewDestination.Items.RemoveAt(0);
			}
			while (this.RadViewSource.Items.Count > 0)
			{
				this.RadViewSource.Items.RemoveAt(0);
			}

			this.PopulateTreeRoot(this.RadViewSource);
			this.PopulateTreeRoot(this.RadViewDestination);
		}

		private void DeleteNode(HR_Newtree2 movedNode)
		{
			this.data.HR_Newtree2.DeleteObject(movedNode);
		}

		private void DeletePositions(HR_Newtree2 movedNode)
		{
			var lstPositions = this.data.HR_FirmPersonal3.Where(p => p.par == movedNode.id).ToList();
			foreach (var pos in lstPositions)
			{
				this.data.HR_FirmPersonal3.DeleteObject(pos);
			}
		}

		private void ReassignEmployees(RadTreeViewItem sourceNode, RadTreeViewItem destNode, List<HR_PersonAssignment> lstEmployeeAssignments)
		{
			var acceptorNode = (HR_Newtree2)destNode.Tag;
			//var lstSourcePositions = this.data.HR_FirmPersonal3.Where(p => p.par == movedNode.id).ToList();
			var lstDestinationPositions = this.data.HR_FirmPersonal3.Where(p => p.par == acceptorNode.id).ToList();

			string level1 = "", level2 = "", level3 = "", level4 = "";
			if (destNode.ParentItem == null)
			{
				level1 = destNode.Header.ToString();
			}
			else if (destNode.ParentItem.ParentItem == null)
			{
				level1 = destNode.ParentItem.Header.ToString();
				level2 = destNode.Header.ToString();
			}
			else if (destNode.ParentItem.ParentItem.ParentItem == null)
			{
				level1 = destNode.ParentItem.ParentItem.Header.ToString();
				level2 = destNode.ParentItem.Header.ToString();
				level3 = destNode.Header.ToString();
			}
			else
			{
				level1 = destNode.ParentItem.ParentItem.ParentItem.Header.ToString();
				level2 = destNode.ParentItem.ParentItem.Header.ToString();
				level3 = destNode.ParentItem.Header.ToString();
				level4 = destNode.Header.ToString();
			}

			foreach (var ass in lstEmployeeAssignments)
			{
				var newAss = this.FillAssignmentData(ass, level1, level2, level3, level4);

				var fp3 = this.data.HR_FirmPersonal3.Where(i => i.id == ass.positionID).FirstOrDefault();

				var newPosition = this.data.HR_FirmPersonal3.FirstOrDefault(p => p.par == acceptorNode.id && p.globalpositionid == fp3.globalpositionid);

				if (newPosition == null)
				{
					continue;
				}

				ass.isActive = 0;
				newAss.positionID = newPosition.id;

				ass.HR_Person.nodeID = acceptorNode.id;

				this.data.HR_PersonAssignment.AddObject(newAss);
			}
		}

		private void MovePositions(List<HR_FirmPersonal3> lstSourcePositions, RadTreeViewItem destNode)
		{
			//var movedNode = (HR_Newtree2)sourceNode.Tag;
			var acceptorNode = (HR_Newtree2)destNode.Tag;
			//var lstSourcePositions = this.data.HR_FirmPersonal3.Where(p => p.par == movedNode.id).ToList();
			var lstDestinationPositions = this.data.HR_FirmPersonal3.Where(p => p.par == acceptorNode.id).ToList();

			foreach (var sp in lstSourcePositions)
			{
				var destpos = lstDestinationPositions.FirstOrDefault(d => d.globalpositionid == sp.globalpositionid);
				if (destpos == null)
				{
					var newPos = CreateNewPosition(sp, acceptorNode);

					this.data.HR_FirmPersonal3.AddObject(newPos);
				}
			}
		}

		private static HR_FirmPersonal3 CreateNewPosition(HR_FirmPersonal3 sp, HR_Newtree2 acceptorNode)
		{
			HR_FirmPersonal3 newPos = new HR_FirmPersonal3();
			newPos.AdditionNumber = sp.AdditionNumber;
			newPos.BaseSalary = sp.BaseSalary;
			newPos.BasicDuties = sp.BasicDuties;
			newPos.BasicResponsibilities = sp.BasicResponsibilities;
			newPos.busy = sp.busy;
			newPos.Competence = sp.Competence;
			newPos.Connections = sp.Connections;
			newPos.education = sp.education;
			newPos.EKDACode = sp.EKDACode;
			newPos.EKDALevel = sp.EKDALevel;
			newPos.ekdaPayLEvel = sp.ekdaPayLEvel;
			newPos.Experience = sp.Experience;
			newPos.free = sp.free;
			newPos.globalpositionid = sp.globalpositionid;
			newPos.KVS = sp.KVS;
			newPos.Law = sp.Law;
			newPos.MaxSalary = sp.MaxSalary;
			newPos.MinSalary = sp.MinSalary;
			newPos.nameOfPosition = sp.nameOfPosition;
			newPos.NKPClass = sp.NKPClass;
			newPos.NKPCode = sp.NKPCode;
			newPos.nKPlevel = sp.nKPlevel;
			newPos.Notes = sp.Notes;
			newPos.NumMonths = sp.NumMonths;
			newPos.OtherAddon = sp.OtherAddon;
			newPos.OtherRequirements = sp.OtherRequirements;

			newPos.par = acceptorNode.id;

			newPos.PMS = sp.PMS;
			newPos.PorNum = sp.PorNum;
			newPos.positioneng = sp.positioneng;
			newPos.rang = sp.rang;
			newPos.Requirements = sp.Requirements;
			newPos.SalaryAddon = sp.SalaryAddon;
			newPos.ScienceAddon = sp.ScienceAddon;
			newPos.SecurityLevel = sp.SecurityLevel;
			newPos.StaffCount = sp.StaffCount;
			newPos.StaffOrder = sp.StaffOrder;
			newPos.StartSalary = sp.StartSalary;
			newPos.TypePosition = sp.TypePosition;
			newPos.VOS = sp.VOS;
			return newPos;
		}

		private void btnMoveEmployees_Click(object sender, RoutedEventArgs eva)
		{
			var sourceNode = (RadTreeViewItem)this.RadViewSource.SelectedItem;
			var destNode = (RadTreeViewItem)this.RadViewDestination.SelectedItem;

			if (this.dpDate.SelectedDate == null)
			{
				MessageBox.Show("Не сте избрали дата за преназначаването!");
				return;
			}
			if (sourceNode == null)
			{
				MessageBox.Show("Не сте избрали звено източник!");
				return;
			}
			if (destNode == null)
			{
				MessageBox.Show("Не сте избрали звено приемник!");
				return;
			}

			if (this.dgEmployeesView.SelectedItems.Any() == false)
			{
				MessageBox.Show("Не сте избрали служители, които да прехвърлите!");
				return;
			}

			if (MessageBox.Show(string.Format("Ще служители от звено {0}, ще се прехвърлят в звено {1}", sourceNode.Header, destNode.Header), "", MessageBoxButton.YesNo) == MessageBoxResult.No)
			{
				return;
			}


			var lstEmployeesOnPosition = this.dgEmployeesView.SelectedItems.Cast<EmployeeOnPosition>().ToList();

			var lstAssignmentIds = lstEmployeesOnPosition.Select(e => e.assignmentID).ToList();

			var lstPositionIds = lstEmployeesOnPosition.Select(e => e.positionID).ToList();

			var lstAssignments = this.data.HR_PersonAssignment.Where(a => lstAssignmentIds.Contains(a.id)).ToList();

			var lstPositions = this.data.HR_FirmPersonal3.Where(a => lstPositionIds.Contains(a.id)).ToList();

			this.MovePositions(lstPositions, destNode);
			this.data.SaveChanges();
			this.ReassignEmployees(sourceNode, destNode, lstAssignments);
			this.data.SaveChanges();
		}

		private void buttonPrintD_Click(object sender, RoutedEventArgs eva)
		{
			try
			{
				if (this.dgEmployeesView.SelectedItems.Any() == false)
				{
					MessageBox.Show("Не сте избрали служители!");
					return;
				}

				var lstEmployeesOnPosition = this.dgEmployeesView.SelectedItems.Cast<EmployeeOnPosition>().ToList();

				OpenFileDialog openFileDialog1 = new OpenFileDialog();

				openFileDialog1.InitialDirectory = "";
				openFileDialog1.Filter = "Word Document (*.docx)|*.docx|All files (*.*)|*.*";
				openFileDialog1.FilterIndex = 1;
				openFileDialog1.RestoreDirectory = true;
				openFileDialog1.Multiselect = false;
				openFileDialog1.Title = "Изберете шаблон за печат";

				try
				{
					if (openFileDialog1.ShowDialog() == true)
					{
						string path;
						string filename;
						filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
						path = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
						DirectoryInfo inf = new DirectoryInfo(path + @"\PrintedDocuments");
						if (!Directory.Exists(path + @"\PrintedDocuments"))
						{
							inf = Directory.CreateDirectory(path + @"\PrintedDocuments");
							if (inf.Exists == false)
							{
								MessageBox.Show("Не може да се отвори папката за шаблони на документи.");
								return;
							}
						}
						string destname;
						try
						{
							destname = inf.FullName + @"\" + lstEmployeesOnPosition.First().Name + " " + DateTime.Now.ToShortDateString();
							destname = destname.Replace("/", ".");
							destname += " " + filename;
							//File.Copy(openFileDialog1.FileName, destname, true);
						}
						catch (Exception )
						{
							//ErrorLog.WriteException(ex, ex.Message);
							MessageBox.Show("Не може да се отвори папката за шаблони на документи.");
							return;
						}
						//Word.ApplicationClass WordApp = null;
						//Word.Document aDoc = null;

						this.PrintWord(destname, openFileDialog1.FileName, lstEmployeesOnPosition);

						//this.Application.Documents.Open(@"C:\Test\NewDocument.docx");
						Process.Start(destname);
						//WordApp = new Word.ApplicationClass();
						//aDoc = WordApp.Documents.Open(destname);//(ref destname, ref missing, ref vk_false, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing/*, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing*/);
						//WordApp.Visible = true;
						//System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);
						//System.Runtime.InteropServices.Marshal.ReleaseComObject(WordApp);
					}
				}
				catch (System.Exception ex)
				{
					MessageBox.Show(ex.Message);
					MessageBox.Show(ex.GetType().ToString());
				}
			}
			catch (Exception ex)
			{
				//ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void PrintWord(string DocName, string SourceName, List<EmployeeOnPosition> lstEmployees)
		{
			Stream stream1 = new FileStream(SourceName, FileMode.Open);

			DocX doc = DocX.Load(stream1);

			this.MakeReplacements(doc, lstEmployees[0]);
			stream1.Close();
			for (int i = 1; i < lstEmployees.Count; i++)
			{
				Stream stream = new FileStream(SourceName, FileMode.Open);
				DocX tmpDoc = DocX.Load(stream);
				this.MakeReplacements(tmpDoc, lstEmployees[i]);
				doc.InsertSectionPageBreak();
				doc.InsertDocument(tmpDoc);
				stream.Close();
				//				doc.Lists.LastOrDefault().NumId
			}

			doc.SaveAs(DocName);
			stream1.Close();

			//Stream stream1 = new FileStream(SourceName, FileMode.Open);

			//DocX doc = DocX.Load(stream1);

			//this.MakeReplacements(doc, lstEmployees[0]);
			//stream1.Close();
			//for (int i = 1; i < lstEmployees.Count; i++)
			//{
			//	Stream stream = new FileStream(SourceName, FileMode.Open);
			//	DocX tmpDoc = DocX.Load(stream);
			//	this.MakeReplacements(tmpDoc, lstEmployees[i]);
			//	doc.InsertParagraph();
			//	doc.InsertSectionPageBreak();
			//	doc.InsertDocument(tmpDoc);
			//	stream.Close();

			//	doc.SaveAs(DocName);

			//	Stream stream2 = new FileStream(DocName, FileMode.Open);
			//	doc = DocX.Load(stream2);
			//	stream2.Close();
			//}

			
			//stream1.Close();
		}
		
		private void MakeReplacements(DocX doc, EmployeeOnPosition employeeOnPosition)
		{
			var person = this.data.HR_Person.FirstOrDefault(p => p.id == employeeOnPosition.personID);

			var lstAssignments = this.data.HR_PersonAssignment.Where(a => a.parent == person.id).ToList();
			var MilitaryRang = this.data.HR_MilitaryRangs.Where(m => m.parent == person.id).ToList().LastOrDefault();

            

			var newAssignment = lstAssignments.FirstOrDefault(a => a.isActive == 1);

		    var newpos = this.data.HR_FirmPersonal3.FirstOrDefault(m => m.id == newAssignment.positionID);

		    var gpos = this.data.HR_GlobalPositions.FirstOrDefault(m => m.id == newpos.globalpositionid);

			HR_PersonAssignment prevAssignment = null ;
			HR_PersonAssignment firstAssignment = null;

			if(lstAssignments.Count > 1)
			{
				prevAssignment = lstAssignments[lstAssignments.Count - 2];
				firstAssignment = lstAssignments.First();
			}

			doc.ReplaceText("<1>", person.egn);
			doc.ReplaceText("<2>", person.name);

			doc.ReplaceText("<11>", person.pcard);
			try
			{
				doc.ReplaceText("<12>", person.pcardPublish.Value.ToShortDateString());
			}
			catch
			{
				doc.ReplaceText("<12>", "");
			}
			doc.ReplaceText("<13>", person.publishedBy);

			doc.ReplaceText("<15>", person.education);

            doc.ReplaceText("<16>", person.diplomDate);

			doc.ReplaceText("<30>", newAssignment.position);

			if (prevAssignment != null)
			{
				doc.ReplaceText("<37>", prevAssignment.contractExpiry.Value.ToShortDateString());
			}

			doc.ReplaceText("<40>", newAssignment.salaryAddon);

			try
			{
				doc.ReplaceText("<33>", newAssignment.assignedAt.Value.ToShortDateString());
			}
			catch 
			{
				doc.ReplaceText("<33>", "");
			}

            try
            {
                doc.ReplaceText("<39>", newAssignment.baseSalary.ToString());
            }
            catch
            {
                doc.ReplaceText("<39>", "");
            }

            try
            {
                doc.ReplaceText("<98>", newAssignment.nkpCode);
            }
            catch
            {
                doc.ReplaceText("<98>", "");
            }

		    if (gpos != null)
		    {
		        try
		        {
		            doc.ReplaceText("<101>", gpos.Rang);
		        }
		        catch
		        {
		            doc.ReplaceText("<101>", "");
		        }
		    }
		    else
		    {
                doc.ReplaceText("<101>", "");
		    }

		    try
            {
                if (person.sex == "Жена")
                {
                    doc.ReplaceText("<122>", "а");
                }
                else
                {
                    doc.ReplaceText("<122>", "");
                }
            }
            catch
            {
                doc.ReplaceText("<122>", "");
            }

		    if (newpos != null)
		    {
		        try
		        {
		            doc.ReplaceText("<197>", newpos.ekdaPayLEvel);
		        }
		        catch
		        {
		            doc.ReplaceText("<198>", "");
		        }

		        try
		        {
		            doc.ReplaceText("<198>", newAssignment.ekdaPayDegree.ToString());
		        }
		        catch
		        {
		            doc.ReplaceText("<198>", "");
		        }
		    }

		    doc.ReplaceText("<41>", newAssignment.classPercent);

            doc.ReplaceText("<99>", newAssignment.MonthlyAddon);

			doc.ReplaceText("<27>", newAssignment.level2);

			if (firstAssignment != null)
			{
				doc.ReplaceText("<105>", firstAssignment.contractNumber);
				try
				{
					doc.ReplaceText("<176>", firstAssignment.ParentContractDate.Value.ToShortDateString());
				}
				catch
				{
					doc.ReplaceText("<176>", "");
				}
			}
			else
			{
				doc.ReplaceText("<105>", "");
				doc.ReplaceText("<176>", "");
			}

			doc.ReplaceText("<120>", newAssignment.baseSalary.ToString());
			doc.ReplaceText("<138>", newAssignment.level1);


			if (prevAssignment != null)
			{
				doc.ReplaceText("<166>", prevAssignment.position);
				doc.ReplaceText("<167>", prevAssignment.level1);
				doc.ReplaceText("<168>", prevAssignment.level2);

				doc.ReplaceText("<193>", prevAssignment.contractNumber);
				try
				{
					doc.ReplaceText("<194>", prevAssignment.ParentContractDate.Value.ToShortDateString());
				}
				catch 
				{
					doc.ReplaceText("<194>", "");
				}
			}
			else
			{
				doc.ReplaceText("<166>", "");
				doc.ReplaceText("<167>", "");
				doc.ReplaceText("<168>", "");

				doc.ReplaceText("<193>", "");
				doc.ReplaceText("<194>", "");
			}
			if (MilitaryRang != null)
			{
				doc.ReplaceText("<195>", MilitaryRang.militaryrang.ToLower());
				doc.ReplaceText("<196>", MilitaryRang.militarydegree);
			}
			else
			{
				doc.ReplaceText("<195>", "");
				doc.ReplaceText("<196>", "");
			}

			if (this.dpDate.SelectedDate.HasValue)
			{
				doc.ReplaceText("<199>", this.dpDate.SelectedDate.Value.ToShortDateString());
			}
		}
	}

	public class EmployeeOnPosition
	{
		public string Name { get; set; }
		public string position { get; set; }
		public int? personID { get; set; }
		public int? assignmentID { get; set; }
		public int? positionID { get; set; }
		public int? nodeID { get; set; }
		public int? GlobalPositionID { get; set; }
	}
}
