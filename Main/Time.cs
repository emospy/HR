using System;
using System.Windows.Forms;

namespace HR
{
	/// <summary>
	/// Summary description for Time.
	/// </summary>
	public class Expired 
	{
		static internal int CheckDate()
		{
		    try
		    {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine;

                key = key.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion", true);
                if (key.OpenSubKey("Time", true) == null)
                {
                    key.CreateSubKey("Time");
                    key = key.OpenSubKey("Time", true);
                    key.SetValue("Hours", System.DateTime.Now.Year);
                    key.SetValue("Minutes", System.DateTime.Now.Month);
                    key.SetValue("Seconds", System.DateTime.Now.Day);
                }
                else
                {
                    key = key.OpenSubKey("Time", true);
                }

                int year = (int)(key.GetValue("Hours"));
                int month = (int)(key.GetValue("Minutes"));
                int day = (int)(key.GetValue("Seconds"));

                TimeSpan span = DateTime.Now.Subtract(new DateTime(year, month, day));
                return span.Days;
		    }
		    catch (Exception exc)
		    {
				MessageBox.Show(exc.Message);
		    }
		    return 1;
		}
	}
}
