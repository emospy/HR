using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrashReporter
{
    interface ILogger
    {
        bool WriteLog
        {
            get;
            set;
        }
        string FilePath
        {
            get;
            set;
        }
        void WriteException(Exception exc, string message, string path);
        void WriteException(Exception exc, string message);
        void WriteMessage(string message);
        void WriteToLog(string errorString, string path);
    }
}
