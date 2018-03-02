using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRDataLayer;

namespace SicknessFrame
{
	public class CustomAbsenceModel : HR_Absence
	{
		public int? id_sysco { get;  set; }
		public string name { get;  set; }
		public bool isUpdated { get; set; }
		public bool isNew { get; set; }

        //public string SicknessNum
        //{
        //    get { return SicknessNumber; }
        //}
	}

	public class CustomHolidaysModel : HR_Absence
	{
		public string user_id { get; set; }
		public string name { get; set; }
		public bool isUpdated { get; set; }
		public bool isNew { get; set; }

		//public string SicknessNum
		//{
		//    get { return SicknessNumber; }
		//}
	}
}
