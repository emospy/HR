using System;

namespace DataLayer
{
	public class AssignmentPackage
	{
		#region AssignmentPackage

		int _ID;
		public int ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
			}
		}
		int _Parent;
		public int Parent
		{
			get
			{
				return _Parent;
			}
			set
			{
				_Parent = value;
			}
		}		
		bool _IsAditionalAssignment;
		public bool IsAditionalAssignment
		{
			get
			{
				return _IsAditionalAssignment;
			}
			set
			{
				 _IsAditionalAssignment = value;
			}
		}

		string _User;
		public string User
		{
			get
			{
				return _User;
			}
			set
			{
				_User = value;
			}
		}

		string _Level1;
		public string Level1
		{
			get
			{
				return _Level1;
			}

			set
			{
				_Level1 = value;
			}
		}

		string _Level2;
		public string Level2
		{
			get
			{
				return _Level2;
			}

			set
			{
				_Level2 = value;
			}
		}

		string _Level3;
		public string Level3
		{
			get
			{
				return _Level3;
			}

			set
			{
				_Level3 = value;
			}
		}

		string _Position;
		public string Position
		{
			get
			{
				return _Position;
			}

			set
			{
				_Position = value;
			}
		}

		string _Contract;
		public string Contract
		{
			get
			{
				return _Contract;
			}

			set
			{
				_Contract = value;
			}
		}

		string _WorkTime;
		public string WorkTime
		{
			get
			{
				return _WorkTime;
			}

			set
			{
				_WorkTime = value;
			}
		}

		DateTime _AssignedAt;
		public DateTime AssignedAt
		{
			get
			{
				return _AssignedAt;
			}

			set
			{
				_AssignedAt = value;
			}
		}

		string _AssignReason;
		public string AssignReason
		{
			get
			{
				return _AssignReason;
			}

			set
			{
				_AssignReason = value;
			}
		}

		string _Staff;
		public string Staff
		{
			get
			{
				return _Staff;
			}

			set
			{
				_Staff = value;
			}
		}

		string _ContractNumber;
		public string ContractNumber
		{
			get
			{
				return _ContractNumber;
			}

			set
			{
				_ContractNumber = value;
			}
		}

		DateTime _ContractExpiry;
		public DateTime ContractExpiry
		{
			get
			{
				return _ContractExpiry;
			}

			set
			{
				_ContractExpiry = value;
			}
		}

		string _NumberKids;
		public string NumberKids
		{
			get
			{
				return _NumberKids;
			}

			set
			{
				_NumberKids = value;
			}
		}

		string _BaseSalary;
		public string BaseSalary
		{
			get
			{
				return _BaseSalary;
			}

			set
			{
				_BaseSalary = value;
			}
		}

		string _SalaryAddon;
		public string SalaryAddon
		{
			get
			{
				return _SalaryAddon;
			}

			set
			{
				_SalaryAddon = value;
			}
		}

		string _ClassPercent;
		public string ClassPercent
		{
			get
			{
				return _ClassPercent;
			}

			set
			{
				_ClassPercent = value;
			}
		}

		string _NKIDName;
		public string NKIDName
		{
			get
			{
				return _NKIDName;
			}

			set
			{
				_NKIDName = value;
			}
		}

		string _NKIDCode;
		public string NKIDCode
		{
			get
			{
				return _NKIDCode;
			}

			set
			{
				_NKIDCode = value;
			}
		}
		int _years;
		public int Years
		{
			get
			{
				return _years;
			}

			set
			{
				_years = value;
			}
		}
		int _months;
		public int Months
		{
			get
			{
				return _months;
			}

			set
			{
				_months = value;
			}
		}
		int _days;
		public int Days
		{
			get
			{
				return _days;
			}

			set
			{
				_days = value;
			}
		}
		#endregion	
		public AssignmentPackage()
		{
		}
	}
}