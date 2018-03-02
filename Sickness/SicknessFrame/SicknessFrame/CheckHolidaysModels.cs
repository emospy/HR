using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SicknessFrame
{
	public class CheckHolidayModel
	{
		public int id_person { get; set; }
		public string Name { get; set; }
		public int Leftover { get; set; }
		public int Total { get; set; }
		public int CalculatedLeftover { get; set; }
		public int Used { get; set; }
		public int ActualTotal { get; set; }
		public int CalculatedTotal { get; set; }
		public DateTime AssignedAt { get; set; }
		public string Position { get; set; }
		public string IsMember { get; set; }
	}

	public class CheckHolidayPerson
	{
		public int id_person { get; set; }
		public string Name { get; set; }
		public int? Leftover { get; set; }
		public int? Total { get; set; }
		public int? Contract { get; set; }
		public DateTime? HiredAt { get; set; }
		public int CalculatedContract { get; set; }
		public DateTime AssignedAt { get; set; }
		public string Position { get; set; }
	}
}
