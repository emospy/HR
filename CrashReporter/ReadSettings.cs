using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CrashReporter
{
    public static class ReadSettings
    {

        public static DataLayer.DataLayer GetDatalayer()
        {
            // Reading release_settings.txt
            
            string line = "";
            string address = "";
            string userName = "";
            string password = "";
            string dataBase = "";
            SQLtypes provider = SQLtypes.MySql;

            address = readConf("data_source");
            dataBase = readConf("database");
            userName = readConf("user_name");
            password = readConf("password");
            if (readConf("provider") == "MsSql")
            {
                provider = SQLtypes.MsSql;
            }
            
            return new DataLayer.DataLayer(address, userName, password, dataBase, provider);
        }

        static string readConf(string key)
        {
            string line = "";
            string result = "";
            try
            {

            
            if (!File.Exists("release_settings.txt"))
            {
                FileStream fileStream = File.Create("release_settings.txt");
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("data_source=releaseserver");
                streamWriter.WriteLine("database=release_management");
                streamWriter.WriteLine("user_name=release");
                streamWriter.WriteLine("password=1234");
                streamWriter.WriteLine("provider=MySql");
                streamWriter.WriteLine("workplace=WPNotSet");
                streamWriter.WriteLine("base_directory=c:\temp");
                streamWriter.Close();
                fileStream.Close();
            }

            TextReader textReader = new StreamReader("release_settings.txt");

            
            while ((line = textReader.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] {'='});
                if (split[0] == key)
                    result = split[1];
            }
            textReader.Close();
            }
            catch
            {

                
            }
            return result;
        }

        public static string GetProject()
        {
            return readConf("project_name");
        }
        public static string GetStartExe()
        {
            return readConf("start_exe");
        }
        public static string GetParam(string param)
        {
            return readConf(param);
        }
        
    }
}
