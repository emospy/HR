using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using HRDataLayer;

namespace HolidayPlan
{
	public class AutomaticMessages
	{
		const int DaysToLookBehind = -90;
		public enum MessageTypes
		{
			ContractExpiryMessage = 1,
			ContractTestPeriod = 2,
			FixedDate = 3,
			MotherhoodAbsenceExpiration = 4,
			MotherhoodSickenssExpiration = 5,
			Employeeanniversary = 6,
			EmployeeLongSickness = 7
		}

		public static void CheckForEvents(string connstring)
		{
			using (Entities data = new Entities(connstring))
			{
				var lstMessageInstances = data.HR_MessageInstances.Where(m => m.IsActive == true); //IsActive

				var lstContractExpiryMessages = lstMessageInstances.Where(m => m.id_messageType == (int)MessageTypes.ContractExpiryMessage).ToList();
				if (lstContractExpiryMessages.Count > 0)
				{
					CheckForContractExpiry(data, lstContractExpiryMessages);
				}

				var lstContractTestPeriodMessages = lstMessageInstances.Where(m => m.id_messageType == (int)MessageTypes.ContractTestPeriod).ToList();
				if (lstContractTestPeriodMessages.Count > 0)
				{
					CheckForContractTestPeriod(data, lstContractTestPeriodMessages);
				}

				var lstFixedDateMessages = lstMessageInstances.Where(m => m.id_messageType == (int)MessageTypes.FixedDate).ToList();
				if (lstFixedDateMessages.Count > 0)
				{
					CheckForFixedDate(data, lstFixedDateMessages);
				}

				var lstmotherhoodAbsenceMessages = lstMessageInstances.Where(m => m.id_messageType == (int)MessageTypes.MotherhoodAbsenceExpiration).ToList();
				if (lstContractTestPeriodMessages.Count > 0)
				{
					CheckForMotherhoodAbsence(data, lstmotherhoodAbsenceMessages);
				}

				var lstMotherhoodSicknessMessages = lstMessageInstances.Where(m => m.id_messageType == (int)MessageTypes.MotherhoodSickenssExpiration).ToList();
				if (lstContractTestPeriodMessages.Count > 0)
				{
					CheckForMotherhoodSickness(data, lstMotherhoodSicknessMessages);
				}

				var lstAnniversaryMessages = lstMessageInstances.Where(m => m.id_messageType == (int)MessageTypes.Employeeanniversary).ToList();
				if (lstContractTestPeriodMessages.Count > 0)
				{
					CheckForEmployeeAnniversary(data, lstAnniversaryMessages);
				}

				var lstLongSicknessMessages = lstMessageInstances.Where(m => m.id_messageType == (int)MessageTypes.EmployeeLongSickness).ToList();
				if (lstLongSicknessMessages.Count > 0)
				{
					CheckForEmployeeLongSickness(data, lstLongSicknessMessages);
				}
			}
		}

		private static void CheckForEmployeeLongSickness(Entities data, List<HR_MessageInstances> lstInstances)
		{
			DateTime PastDate = DateTime.Now.AddDays(-365); //look one year back
			var lstCurrentMessages = data.HR_Messages.Where(m => m.HR_MessageInstances.id_messageType == (int)MessageTypes.EmployeeLongSickness
																	&& m.DueDate > PastDate).ToList();

			foreach (var instance in lstInstances)
			{
				var lstEmployees = (from p in data.HR_Person
									join pa in data.HR_PersonAssignment on p.id equals pa.parent
									where pa.isActive == 1
									select p).ToList();

				foreach (var Employee in lstEmployees)
				{
					DateTime BdStart, BDEnd;
					BdStart = DateTime.Now.AddYears(-1);
					BDEnd = DateTime.Now;

					var lstSickness = data.HR_Absence.Where(a => a.typeAbsence == "Болнични" && a.fromDate > BdStart && a.parent == Employee.id).ToList();

					var CountSickness = lstSickness.Sum(s => s.CalendarDays);
					if (CountSickness > instance.WarningDays)
					{
						var idp = Employee.id;
						var name = Employee.name;

						DateTime pdate = lstSickness.OrderBy(s => s.toDate).LastOrDefault().toDate;

						
						var pMessageCount = lstCurrentMessages.Count(m => m.id_person == idp
							                                                && m.DueDate == pdate
							                                                && m.id_messageInstance == instance.id_messageInstance);
						

						if (pMessageCount == 0)
						{
							//Generate a new message
							var newMessage = new HR_Messages();

							newMessage.DueDate = pdate;
							newMessage.id_messageInstance = instance.id_messageInstance;
							newMessage.id_person = idp;
							newMessage.IsConfirmed = false;
							newMessage.IsMailSent = false;

							newMessage.Text = string.Format("Служител {0} е ползвал {1} дни болнични през последната една календарна година.", name, CountSickness);
							newMessage.Timestamp = DateTime.Now;

							data.HR_Messages.AddObject(newMessage);
						}

						data.SaveChanges();
					}					
				}
			}
		}

		private static void CheckForEmployeeAnniversary(Entities data, List<HR_MessageInstances> lstInstances)
		{
			DateTime PastDate = DateTime.Now.AddDays(DaysToLookBehind);
			var lstCurrentMessages = data.HR_Messages.Where(m => m.HR_MessageInstances.id_messageType == (int)MessageTypes.Employeeanniversary
																	&& m.DueDate > PastDate).ToList();

			foreach (var instance in lstInstances)
			{
				var lstEmployees = (from p in data.HR_Person
									join pa in data.HR_PersonAssignment on p.id equals pa.parent
									where pa.isActive == 1
									select p).ToList();

				foreach (var Employee in lstEmployees)
				{
					DateTime BdStart, BDEnd, Bdate50, Bdate55, Bdate60;
					BdStart = DateTime.Now;
					BDEnd = DateTime.Now.AddDays(30);

					if (Employee.bornDate == null)
					{
						continue;
					}

					Bdate50 = Employee.bornDate.Value.AddYears(50);
					Bdate55 = Employee.bornDate.Value.AddYears(55);
					Bdate60 = Employee.bornDate.Value.AddYears(60);

					DateTime pdate = DateTime.Now;
					bool b50 = false, b55 = false, b60 = false;
					string years = "";

					if (Bdate50 >= BdStart && Bdate50 <= BDEnd)
					{
						b50 = true;
						pdate = Bdate50;
						years = "50";
					}
					else if (Bdate55 >= BdStart && Bdate55 <= BDEnd)
					{
						b55 = true;
						pdate = Bdate55;
						years = "55";
					}
					else if (Bdate60 >= BdStart && Bdate60 <= BDEnd)
					{
						b60 = true;
						pdate = Bdate60;
						years = "60";
					}

					if (b50 || b55 || b60)
					{
						var idp = Employee.id;
						var name = Employee.name;
						var pMessageCount = lstCurrentMessages.Count(m => m.id_person == idp
																			&& m.DueDate == pdate
																			&& m.id_messageInstance == instance.id_messageInstance);

						if (pMessageCount == 0)
						{
							//Generate a new message
							var newMessage = new HR_Messages();

							newMessage.DueDate = pdate;
							newMessage.id_messageInstance = instance.id_messageInstance;
							newMessage.id_person = idp;
							newMessage.IsConfirmed = false;
							newMessage.IsMailSent = false;

							newMessage.Text = string.Format("Служител {0} ще навърши {1} на {2}.", name, years, pdate.ToShortDateString());
							newMessage.Timestamp = DateTime.Now;

							data.HR_Messages.AddObject(newMessage);
						}

						data.SaveChanges();
					}
				}
			}
		}

		private static void CheckForContractExpiry(Entities data, List<HR_MessageInstances> lstInstances)
		{
			DateTime PastDate = DateTime.Now.AddDays(DaysToLookBehind);
			var lstCurrentMessages = data.HR_Messages.Where(m => m.HR_MessageInstances.id_messageType == (int)MessageTypes.ContractExpiryMessage
																	&& m.DueDate > PastDate).ToList();

			foreach (var instance in lstInstances)
			{
				int id_instance = instance.id_messageInstance;
				DateTime DueDate = DateTime.Now.AddDays((int)instance.WarningDays);

				var lstPersonsToExpire = (from p in data.HR_Person
										  from pa in data.HR_PersonAssignment
										  where p.fired == 0
										  && pa.isActive == 1
										  && p.id == pa.parent
										  && (pa.contract == "Срочен" || pa.contract == "Срочен със срок на изпитване")
										  && pa.contractExpiry < DueDate
										  && pa.contractExpiry > PastDate
										  select new { p, pa }).ToList();

				foreach (var person in lstPersonsToExpire)
				{
					var idp = person.p.id;
					var name = person.p.name;
					var pdate = (DateTime)person.pa.contractExpiry;
					var pMessageCount = lstCurrentMessages.Count(m => m.id_person == idp
																&& m.DueDate == pdate
																&& m.id_messageInstance == id_instance);
					if (pMessageCount == 0)
					{ //Generate a new message
						var newMessage = new HR_Messages();

						newMessage.DueDate = pdate;
						newMessage.id_messageInstance = id_instance;
						newMessage.id_person = idp;
						newMessage.IsConfirmed = false;
						newMessage.IsMailSent = false;
						newMessage.Text = string.Format("Срочен договор на служител {0} ще изтече на {1}.", name, pdate.ToShortDateString());
						newMessage.Timestamp = DateTime.Now;

						data.HR_Messages.AddObject(newMessage);
					}
				}
				data.SaveChanges();
			}
		}

		private static void CheckForContractTestPeriod(Entities data, List<HR_MessageInstances> lstInstances)
		{
			DateTime PastDate = DateTime.Now.AddDays(DaysToLookBehind);
			var lstCurrentMessages = data.HR_Messages.Where(m => m.HR_MessageInstances.id_messageType == (int)MessageTypes.ContractTestPeriod
																	&& m.DueDate > PastDate).ToList();

			foreach (var instance in lstInstances)
			{
				int id_instance = instance.id_messageInstance;
				DateTime DueDate = DateTime.Now.AddDays((int)instance.WarningDays);

				var lstPersonsWithTestPeriod = (from p in data.HR_Person
												from pa in data.HR_PersonAssignment
												where p.fired == 0
												&& pa.isActive == 1
												&& p.id == pa.parent
												&& (pa.contract == "Безсрочен със срок на изпитване" || pa.contract == "Срочен със срок на изпитване")
												&& pa.TestContractDate < DueDate
												&& pa.TestContractDate > PastDate
												select new { p, pa }).ToList();

				foreach (var person in lstPersonsWithTestPeriod)
				{
					var idp = person.p.id;
					var name = person.p.name;
					DateTime pdate = (DateTime)person.pa.TestContractDate;
					var pMessageCount = lstCurrentMessages.Count(m => m.id_person == idp
																&& m.DueDate == pdate
																&& m.id_messageInstance == id_instance);
					if (pMessageCount == 0)
					{ //Generate a new message
						var newMessage = new HR_Messages();

						newMessage.DueDate = pdate;
						newMessage.id_messageInstance = id_instance;
						newMessage.id_person = idp;
						newMessage.IsConfirmed = false;
						newMessage.IsMailSent = false;
						newMessage.Text = string.Format("Изпитателен срок на служител {0} ще изтече на {1}.", name, pdate.ToShortDateString());
						newMessage.Timestamp = DateTime.Now;
						data.HR_Messages.AddObject(newMessage);
					}
				}
				data.SaveChanges();
			}
		}

		private static void CheckForFixedDate(Entities data, List<HR_MessageInstances> lstInstances)
		{
			DateTime PastDate = DateTime.Now.AddDays(-365); //look one year back
			var lstCurrentMessages = data.HR_Messages.Where(m => m.HR_MessageInstances.id_messageType == (int)MessageTypes.FixedDate
																	&& m.DueDate > PastDate).ToList();

			foreach (var instance in lstInstances)
			{
				int id_instance = instance.id_messageInstance;
				DateTime DueDate = DateTime.Now.AddDays((int)instance.WarningDays);

				if (instance.FixedDate <= DueDate)
				{
					DateTime pdate = (DateTime)instance.FixedDate;
					var pMessageCount = lstCurrentMessages.Count(m => m.DueDate == pdate
																	  && m.id_messageInstance == id_instance);
					if (pMessageCount == 0)
					{
						//Generate a new message
						var newMessage = new HR_Messages();

						newMessage.DueDate = pdate;
						newMessage.id_messageInstance = id_instance;
						newMessage.IsConfirmed = false;
						newMessage.IsMailSent = false;
						newMessage.Text = string.Format("{0} ще настъпи на {1}.", instance.Description, pdate.ToShortDateString());
						newMessage.Timestamp = DateTime.Now;
						data.HR_Messages.AddObject(newMessage);
					}
				}

				data.SaveChanges();
			}
		}

		private static void CheckForMotherhoodAbsence(Entities data, List<HR_MessageInstances> lstInstances)
		{
			DateTime PastDate = DateTime.Now.AddDays(DaysToLookBehind);
			var lstCurrentMessages = data.HR_Messages.Where(m => m.HR_MessageInstances.id_messageType == (int)MessageTypes.MotherhoodAbsenceExpiration
																	&& m.DueDate > PastDate).ToList();

			foreach (var instance in lstInstances)
			{
				int id_instance = instance.id_messageInstance;
				DateTime DueDate = DateTime.Now.AddDays((int)instance.WarningDays);

				var lstPersonsWithTestPeriod = (from p in data.HR_Person
												from pa in data.HR_Absence
												where p.fired == 0
												&& p.id == pa.parent
												&& pa.typeAbsence == "Отглеждане на дете"
												&& pa.toDate < DueDate
												&& pa.toDate > PastDate
												select new { p, pa }).ToList();

				foreach (var person in lstPersonsWithTestPeriod)
				{
					var idp = person.p.id;
					var name = person.p.name;
					var pdate = person.pa.toDate;
					var pMessageCount = lstCurrentMessages.Count(m => m.id_person == idp
																&& m.DueDate == pdate
																&& m.id_messageInstance == id_instance);
					if (pMessageCount == 0)
					{ //Generate a new message
						var newMessage = new HR_Messages();

						newMessage.DueDate = pdate;
						newMessage.id_messageInstance = id_instance;
						newMessage.id_person = idp;
						newMessage.IsConfirmed = false;
						newMessage.IsMailSent = false;
						newMessage.Text = string.Format("Отпуск за отглеждане на дете {0} ще изтече на {1}.", name, pdate.ToShortDateString());
						newMessage.Timestamp = DateTime.Now;
						data.HR_Messages.AddObject(newMessage);
					}
				}
				data.SaveChanges();
			}
		}

		private static void CheckForMotherhoodSickness(Entities data, List<HR_MessageInstances> lstInstances)
		{
			DateTime PastDate = DateTime.Now.AddDays(DaysToLookBehind);
			var lstCurrentMessages = data.HR_Messages.Where(m => m.HR_MessageInstances.id_messageType == (int)MessageTypes.MotherhoodSickenssExpiration
																	&& m.DueDate > PastDate).ToList();

			foreach (var instance in lstInstances)
			{
				int id_instance = instance.id_messageInstance;
				DateTime DueDate = DateTime.Now.AddDays((int)instance.WarningDays);

				var lstPersonsWithMotherhoodSickness = (from p in data.HR_Person
														from pa in data.HR_Absence
														where p.fired == 0
														&& p.id == pa.parent
														&& pa.typeAbsence == "Болнични след раждане"
														&& pa.toDate < DueDate
														&& pa.toDate > PastDate
														select new { p, pa }).ToList();

				foreach (var person in lstPersonsWithMotherhoodSickness)
				{
					var idp = person.p.id;
					var name = person.p.name;
					var pdate = person.pa.toDate;
					var pMessageCount = lstCurrentMessages.Count(m => m.id_person == idp
																&& m.DueDate == pdate
																&& m.id_messageInstance == id_instance);
					if (pMessageCount == 0)
					{ //Generate a new message
						var newMessage = new HR_Messages();

						newMessage.DueDate = pdate;
						newMessage.id_messageInstance = id_instance;
						newMessage.id_person = idp;
						newMessage.IsConfirmed = false;
						newMessage.IsMailSent = false;
						newMessage.Text = string.Format("Болнични след раждане на {0} ще изтече на {1}.", name, pdate.ToShortDateString());
						newMessage.Timestamp = DateTime.Now;
						data.HR_Messages.AddObject(newMessage);
					}
				}
				data.SaveChanges();
			}
		}
	}
}
