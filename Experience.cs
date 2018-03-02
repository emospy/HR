using System;
using System.Data;

namespace HR
{
	/// <summary>
	/// Summary description for ExperienceCalculator.
	/// </summary>
	public class Experience
	{
		private int _years;
		/// <summary>
		/// Years
		/// </summary>
		public int Years
		{
			get
			{
				return _years;
			}
		}

		private int _months;
		/// <summary>
		/// Months
		/// </summary>
		public int Months
		{
			get
			{
				return _months;
			}
		}
		private int _days;
		/// <summary>
		/// Days
		/// </summary>
		public int Days
		{
			get
			{
				return _days;
			}
		}

		/// <summary>
		/// Blank Constructor. Initializes variables with 0.
		/// </summary>
		public Experience()
		{
			this._years = 0;
			this._months = 0;
			this._days = 0;
		}
		/// <summary>
		/// Directly initializes all variables
		/// </summary>
		/// <param name="Years"></param>
		/// <param name="Months"></param>
		/// <param name="Days"></param>
		public Experience(int Years, int Months, int Days)
		{
			this._years = Years;
			this._months = Months;
			this._days = Days;
		}

		/// <summary>
		/// Adds given experience to the current. Currently does nothing.
		/// </summary>
		/// <param name="Years"></param>
		/// <param name="Months"></param>
		/// <param name="Days"></param>
		public void Sum(int Years, int Months, int Days)
		{
		}

		/// <summary>
		/// Adds Years
		/// </summary>
		/// <param name="Years">Integer number of years to be added</param>
		public void AddYears(int Years)
		{
			this._years += Years;
		}

		/// <summary>
		/// Adds Months
		/// </summary>
		/// <param name="Months">Integer number of months to be added</param>
		public void AddMonths(int Months)
		{
			this._months += Months;
			if(this.Months >= 12)
			{
				while(this.Months >= 12)
				{
					this.AddYears(1);
					this._months -= 12;
				}
			}
			else if(this.Months < 0)
			{
				while(this.Months < 0)
				{
					this.AddYears(-1);
					this._months += 12;
				}
			}
		}

		/// <summary>
		/// Adds Days
		/// </summary>
		/// <param name="Days">Integer number of days to be added</param>
		public void AddDays(int Days)
		{
			this._days += Days;
			if(this.Days > 30)
			{
				while(this._days >30)
				{
					this.AddMonths(1);
					this._days -= 30;
				}
			}
			else if(this.Days < 0)
			{
				while(this.Days < 0)
				{
					this.AddMonths(-1);
					this._days += 30;
				}
			}
		}		
		
		/// <summary>
		/// Calculates the difference between the staff date and now date then adds the difference to the staff
		/// </summary>
		/// /// <param name="StaffDate">DateTime object giving the starting date to be used and subtracted from Now.</param>
		/// <returns></returns>
		public void CalculateToNow(DateTime StaffDate)
		{			
			this.AddYears(DateTime.Now.Year - StaffDate.Year);
			this.AddMonths(DateTime.Now.Month - StaffDate.Month);
			this.AddDays(DateTime.Now.Day - StaffDate.Day);
		}

		/// <summary>
		/// Calculates the difference between the start date and end date then adds the difference to the staff
		/// </summary>
		/// <param name="StartDate">DateTime object giving the starting date to be used and subtracted from enddate.</param>
		/// <param name="EndDate">DateTime object giving the end date</param>
		/// <param name="staff">Coefficient showing the weight of the experience</param>
		/// <returns></returns>
		public void AddBetween(DateTime StartDate, DateTime EndDate, float staff)
		{
			this.AddYears((int)(EndDate.Year - StartDate.Year * staff));
			this.AddMonths((int)(EndDate.Month - StartDate.Month * staff));
			this.AddDays((int)(EndDate.Day - StartDate.Day * staff));
		}
		/// <summary>
		/// Converting from one category to another using int coefficient
		/// </summary>
		/// <param name="nom">Nominator</param>
		/// <param name="denom">Denominator</param>
		public Experience ConvertToCategory(int nom, int denom)
		{
			int quotient, remainder;
			Experience Exp = new Experience();

			quotient = this.Years * nom / denom;
			remainder = this.Years * nom % denom;
			Exp.AddYears(quotient);
			if(remainder != 0)
			{
				remainder *= 12;
				quotient = remainder / denom;
				remainder = remainder % denom;
				Exp.AddMonths(quotient);
				if(remainder != 0)
				{
					remainder *= 30;
					quotient = remainder / denom;
					Exp.AddDays(quotient);
				}
			}

			quotient = this.Months * nom / denom;
			remainder = this.Months * nom % denom;
			Exp.AddMonths(quotient);
			if(remainder != 0)
			{
				remainder *= 30;
				quotient = remainder / denom;
				Exp.AddDays(quotient);
			}

			quotient = this.Days * nom / denom;
			Exp.AddDays(quotient);

			return Exp;
		}
		/// <summary>
		/// Calculates total staff based on the data from assignments
		/// </summary>
		/// <param name="dtAssignment">Some DataTable</param>
		public void CalculateTotal(DataTable dtAssignment)
		{
			int i;
			for(i = 0; i < dtAssignment.Rows.Count; i ++)
			{

			}
		}
	}
}