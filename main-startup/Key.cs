using System;
using System.Management;
using System.Diagnostics;

namespace LichenSystaw2004
{
	/// <summary>
	/// Summary description for Key.
	/// </summary>
	public class Key
	{
		static internal decimal GenerateProductKey()
		{
            decimal hash = 1;
            hash = GetMacHash();
			return ( hash*13 )*33;
		}
		static internal int GetMacHash()
		{
//			string macAddress = "0";
//			try
//			{
//				ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
//				ManagementObjectCollection queryCollection = query.Get();
//				foreach(ManagementObject mo in queryCollection)
//				{
//					if(mo["MacAddress"]!=null)
//					{
//						if( (bool)mo["IPEnabled"] == true )
//						{
//							macAddress = mo["MacAddress"].ToString();
//							break;
//						}
//					}
//				}
//			}
//			catch
//			{
//				macAddress = MacAddress.GetMac();
//			}
//			
//			return Math.Abs( macAddress.GetHashCode() );
			
			return Math.Abs( System.DateTime.Now.Day.GetHashCode() * 23451 );
		}
		internal enum ProductOption
		{
			Empty,
			Atestacii,
			Learning,
			All
		};
		static internal bool ActivateProduct( string cdKey, decimal productKey, ProductOption prod )
		{
			int mul = 0;
			switch( prod )
			{
				case ProductOption.Empty: mul = 13;
					break;
				case ProductOption.All: mul = 19;
					break;
				case ProductOption.Atestacii: mul = 17;
					break;
				case ProductOption.Learning: mul = 15;
					break;
			}
			decimal result =  (productKey  * mul)/2;
			string res = result.ToString().Replace( ".", "");
			res = res.Replace( ",", "");
			if( res == cdKey )
			{
				return true;
			}
			
			return false;
		}
	}
	/// <summary>
	/// Required designer variable.
	/// </summary>
	public class MacAddress 
	{ 
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public MacAddress() 
		{ 
		} 
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public static string GetMac() 
		{	
			System.Net.IPHostEntry ip = System.Net.Dns.GetHostEntry( System.Net.Dns.GetHostName() );
			string IP = ip.AddressList[0].ToString();
			string str1=String.Empty; 
			try 
			{ 
				string str2=String.Empty; 
				ProcessStartInfo info1 = new ProcessStartInfo(); 
				Process process1 = new Process(); 
				info1.FileName = "nbtstat"; 
				info1.RedirectStandardInput = false; 
				info1.RedirectStandardOutput = true; 
				info1.Arguments = "-A " + IP; 
				info1.UseShellExecute = false; 
				process1 = Process.Start(info1); 
				int num1 = -1; 
				while (num1 <= -1) 
				{ 
					num1 = str2.Trim().ToLower().IndexOf("mac address", 0); 
					if (num1 > -1) 
					{ 
						break; 
					} 
					str2 = process1.StandardOutput.ReadLine(); 
				} 
				process1.WaitForExit(); 
				str1 = str2.Trim(); 
			} 
			catch (Exception exception2) 
			{ 
				throw exception2; 
			} 
			return str1; 
		} 
	}
}
