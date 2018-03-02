using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

// Copied from TAKT WPF Controls
namespace DataLayer
{
    public class ErrorLog
    {
        public static string filePath = "DataLayerErrorLog.txt";
        private static bool _writeLog = false;
        public ErrorLog(string file)
        {
            filePath = file;
            WriteLog = true;
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
            StringBuilder sb = new StringBuilder();
            while (exc != null)
            {
                sb.AppendFormat("\r\n {0}\t{1}\t{2}\t{3}\t{4}\r\n", DateTime.Now, message, exc.Source, exc.StackTrace, exc.Message);
                exc = exc.InnerException;
            }

            sb.Append("================================================================================"); // Line delitel
            WriteToLog(sb.ToString(), path);
        }
        public static void WriteException(Exception exc, string message)
        {
            if (_writeLog == false)
                return;
            StringBuilder sb = new StringBuilder();
            while (exc != null)
            {
                sb.AppendFormat("\r\n {0}\t{1}\t{2}\t{3}\t{4}\r\n", DateTime.Now, message, exc.Source, exc.StackTrace, exc.Message);
                exc = exc.InnerException;
            }

            sb.Append("================================================================================"); // Line delitel
            WriteToLog(sb.ToString(), filePath);

        }
        public static void WriteMessage(string message)
        {
            if (_writeLog == false)
                return;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\r\n {0}\t{1}\r\n", DateTime.Now, message);
            WriteToLog(sb.ToString(), filePath);
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
