using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalSettings
{
    public static class GlobalSettings
    {
        static int id_product;

        public static int Id_product
        {
            get { return GlobalSettings.id_product; }
            set { GlobalSettings.id_product = value; }
        }
        static int id_department;

        public static int Id_department
        {
            get { return GlobalSettings.id_department; }
            set { GlobalSettings.id_department = value; }
        }
        static int id_user;

        public static int Id_user
        {
            get { return GlobalSettings.id_user; }
            set { GlobalSettings.id_user = value; }
        }
        static int id_cv;

        public static int Id_cv
        {
            get { return GlobalSettings.id_cv; }
            set { GlobalSettings.id_cv = value; }
        }

        public static string Connectionstring { get; set; }
    }
}
