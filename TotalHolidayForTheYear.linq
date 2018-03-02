<Query Kind="Program">
  <Connection>
    <ID>6b48b976-fdc5-40e6-a53b-3af373e2f65c</ID>
    <Persist>true</Persist>
    <Server>192.168.10.8</Server>
    <SqlSecurity>true</SqlSecurity>
    <Database>syscodb</Database>
    <UserName>root</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAa/j4Ali68UiMgT6INKHZ1gAAAAACAAAAAAAQZgAAAAEAACAAAAC0o3yCNuYI5r0Fu+U1OOWljCQUsxQ0gTf842zsp/lMhAAAAAAOgAAAAAIAACAAAACDMnCZEAT4vS0ukCpNzQ8hxtfR17cF4+rCT3DmtP3T3xAAAAB0idSB6cjwsjeVp0TvN8+TQAAAAJAcToCw91jvS4t0HU+xG65K8Ld7xQ6IyKaBYc/EQV3+6CgTK/eYAJ9atBwRn3wtrZs4TA/9YG+LkH5cWOv1XIM=</Password>
  </Connection>
</Query>

public class Person2012
{
	public int id;
	public int holiday;
	public int? fired;
}

void Main()
{
	int total = 0;
	var pers = (from p in HR_persons
				from y in HR_year_holidays
				where y.Parent == p.Id
				&& y.Year == 2012
				select new Person2012{id = p.Id, holiday = (int)y.Leftover, fired = p.Fired}).ToList();
	pers.Dump();
	
	foreach(var y in pers)
	{
		if(y.fired == 1)
		{
			var fir = (from f in HR_fireds
					where f.Parent == y.id 
					&& f.FromDate > new DateTime(2012, 12, 31)
					select f).FirstOrDefault();
			if(fir == null)
			{
				continue;
			}
		}
		
		var add = (from a in HR_absences
					where a.Parent == y.id
					&& a.Year == "2012"
					&& a.TypeAbsence == "Полагаем годишен отпуск"
					&& a.FromDate > new DateTime(2012, 12, 31)
					select a).ToList();
					
		foreach(var a in add)
		{
			total += (int)a.CountDays;
		}
		total += y.holiday;
	}
	total.Dump();
}

// Define other methods and classes here
