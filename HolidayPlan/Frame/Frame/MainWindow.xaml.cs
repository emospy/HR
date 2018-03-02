using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.EntityClient;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HolidayPlan;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Frame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString;
        private string xmlFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\Config.xml");
        private string server;
        private string userID;
        private string pass;
        private string database;
        private string DBType;
  
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.ParseXML() && this.GenerateConnectionString())
            {
                HolidayPlanWindow win = new HolidayPlanWindow(connectionString);
                win.ShowDialog();
            }
        }

        private bool ParseXML()
        {
            try
            {
                DataSet ds = new DataSet();

                FileStream fsReadXML = new FileStream(xmlFilePath, FileMode.Open);

                ds.ReadXml(fsReadXML);

                this.server =   ds.Tables[0].Rows[0].ItemArray[(int)XMLPosition.Server].ToString();
                this.userID =   ds.Tables[0].Rows[0].ItemArray[(int)XMLPosition.UserID].ToString();
                this.pass =     ds.Tables[0].Rows[0].ItemArray[(int)XMLPosition.Password].ToString();
                this.database = ds.Tables[0].Rows[0].ItemArray[(int)XMLPosition.Database].ToString();
                this.DBType =   ds.Tables[0].Rows[0].ItemArray[(int)XMLPosition.DBType].ToString();

                ds.Dispose();
                fsReadXML.Close();

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
        }

        private bool GenerateConnectionString()
        {

            switch (this.DBType.ToLower())
            {
                case "mysql":
                    {
                        return this.GenereteMySQLConnectionString();
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        private bool GenereteMySQLConnectionString()
        {
            try
            {
                MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder
                {
                    Server = this.server,
                    UserID = this.userID,
                    Password = this.pass,
                    Database = this.database,
                    Port = 3306,
                    CharacterSet = "utf8"
                };

                EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
                entityBuilder.Provider = "MySql.Data.MySqlClient";
                entityBuilder.ProviderConnectionString = stringBuilder.ToString();
                entityBuilder.Metadata = @"res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl";

                this.connectionString = entityBuilder.ToString();

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                return false;
            }
        }

    }
}
