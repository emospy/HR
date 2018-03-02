using Microsoft.Win32;

namespace LichenSystaw2004
{
    /// <summary>
    /// Method for retrieving a Registry Value.
    /// </summary>
    public class RegistryAccess
    {
        private const string SOFTWARE_KEY = "Software";
        //private const string COMPANY_NAME = "MyCompany";
        private const string APPLICATION_NAME = "Човешки Ресурси";

        /// <summary>
        /// Method for retrieving a Registry Value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        static public string GetStringRegistryValue(string key, string defaultValue)
        {
            RegistryKey rkCompany;
            //rkCompany = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
            rkCompany = Registry.LocalMachine.OpenSubKey(@"Software\Човешки Ресурси");
            if (rkCompany != null)
            {
                foreach (string sKey in rkCompany.GetValueNames())
                {
                    if (sKey == key)
                    {
                        return (string)rkCompany.GetValue(sKey);
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Method for storing a Registry Value. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="stringValue"></param>
        static public void SetStringRegistryValue(string key, string stringValue)
        {
            RegistryKey rkSoftware;
            //RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkSoftware = Registry.LocalMachine.OpenSubKey(SOFTWARE_KEY, true);
            //rkCompany = rkSoftware.CreateSubKey(COMPANY_NAME);
            //if( rkCompany != null )
            //{
            rkApplication = rkSoftware.CreateSubKey(APPLICATION_NAME);
            if (rkApplication != null)
            {
                rkApplication.SetValue(key, stringValue);
            }
            //}
        }
    }
}