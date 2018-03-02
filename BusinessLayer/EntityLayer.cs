using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
	public class EntityLayer:IDisposable
	{
		string ConnectionString = "metadata=res://*/HREntity.csdl|res://*/HREntity.ssdl|res://*/HREntity.msl;provider=MySql.Data.MySqlClient;provider connection string=';server=localhost;user id=root;password=tess;database=hrdb;persist security info=True'";
		public EntityLayer()
		{
		}

		public EntityLayer(string constr)
		{
			this.ConnectionString = constr;
		}

		public List<yearworkdays> GetCurrentMonthWorkdays(DateTime CurrentMonth)
		{
			using(var entity = new Entities(this.ConnectionString))
			{
				var lstWorkdays = (from wd in entity.yearworkdays
								   where wd.Date.Value.Year == CurrentMonth.Year && wd.Date.Value.Month == CurrentMonth.Month
				                   select wd).ToList();
				return lstWorkdays;
			}
		}

		public void Dispose()
		{
		}
	}
}
