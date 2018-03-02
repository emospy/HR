using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Net;

// Copied from TAKT WPF Controls
namespace CrashReporter
{
    public class ErrorLog
    {
        private static string filePath = "ErrorLog.txt";

        public static DataLayer.DataLayer data;
        static string comp_ip = "0.0.0.0";
        private static string workplace = "WPNotSet";
        static bool isDatabase = false;

        public static bool IsDatabase
        {
            get { return ErrorLog.isDatabase; }
            set 
            { 
                ErrorLog.isDatabase = value;
                if (value == true)
                    InitDB();
            }
        }

        public static string Comp_ip
        {
            get { return comp_ip; }
            set { comp_ip = value; }
        }
        public static string Workplace
        {
            get { return workplace; }
            set { workplace = value; }
        }
        public static int Id_proj { get; set; }

        static string GetIPAddress()
        {
            IPHostEntry host;
            string localIP = "0.0.0.0";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        private static void InitDB()
        {
            data = ReadSettings.GetDatalayer();
            workplace = ReadSettings.GetParam("workplace");
            string projName = ReadSettings.GetParam("project_name");

            object id_p = data.GetSingleResultObject(String.Format("select id_p from projects where name = '{0}' LIMIT 1", projName));
             if (id_p != null)
                 Id_proj = (int)(uint)id_p;
            Comp_ip = GetIPAddress();
        }

        public static string FilePath
        {
            get { return ErrorLog.filePath; }
            set { ErrorLog.filePath = value; }
        }
        private static bool _writeLog = false;
        public ErrorLog()
        {
        }

        public static bool WriteLog
        {
            get
            {
                return _writeLog;
            }
            set
            {
                _writeLog = value;
            }
        }

        public static void WriteException(Exception exc, string message, string path)
        {
            if (_writeLog == false)
                return;
            if (IsDatabase == false)
            {
                StringBuilder sb = new StringBuilder();
                while (exc != null)
                {
                    sb.AppendFormat("\r\n {0}\t{1}\t{2}\t{3}\t{4}\r\n", DateTime.Now, message, exc.Source, exc.StackTrace, exc.Message);
                    exc = exc.InnerException;
                }

                sb.Append("================================================================================"); // Line delitel
                WriteToLog(sb.ToString(), path);
            }
            else
            {
                WriteException(exc, message);
            }
        }
        public static void WriteException(Exception exc, string message)
        {
            if (_writeLog == false)
                return;
            if (IsDatabase == false)
            {
                StringBuilder sb = new StringBuilder();
                while (exc != null)
                {
                    sb.AppendFormat("\r\n {0}\t{1}\t{2}\t{3}\t{4}\r\n", DateTime.Now, message, exc.Source, exc.StackTrace, exc.Message);
                    exc = exc.InnerException;
                }

                sb.Append("================================================================================"); // Line delitel
                WriteToLog(sb.ToString(), filePath);
            }
            else
            {
                if (data != null && Comp_ip != null)
                {
                    data.GetSingleResult(string.Format("insert into exception_details (id_proj, tstamp, computer_ip, message, Workplace) values ({0}, {1}, '{2}', '{3}', '{4}')",
                            Id_proj, data.ConvertDateDBDate(DateTime.Now), Comp_ip, message, Workplace));

                    object obj = data.GetSingleResultObject("select max(id_exc) from exception_details");
                    int id_exc;
                    if (obj != null)
                    {
                        if (int.TryParse(obj.ToString(), out id_exc) == true)
                        {
                            while (exc != null)
                            {
                                data.GetSingleResult(string.Format("insert into inner_exception (id_exc, source, stack_trace, message) values ({0}, '{1}', '{2}', '{3}')",
                                    id_exc, exc.Source, exc.StackTrace.Replace("'", "|"), exc.Message).Replace("'", "|"));
                                exc = exc.InnerException;
                            }
                        }
                    }
                }
            }

        }
        public static void WriteMessage(string message)
        {
            if (_writeLog == false)
                return;
            if (IsDatabase == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("\r\n {0}\t{1}\r\n", DateTime.Now, message);
                WriteToLog(sb.ToString(), filePath);
            }
            else
            {
                if (data != null && message != null)
                    data.GetSingleResult(string.Format("insert into message (tstamp, message) values ({0}, '{1}')", data.ConvertDateDBDate(DateTime.Now), message));
            }
        }

        public static void WriteToLog(string errorString, string path)
        {
            FileStream fstream = new FileStream(path, FileMode.OpenOrCreate);
            fstream.Seek(0, SeekOrigin.End);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] bytarr = encoding.GetBytes(errorString.ToCharArray());
            fstream.Write(bytarr, 0, bytarr.Length);
            fstream.Close();
        }
    }
}
