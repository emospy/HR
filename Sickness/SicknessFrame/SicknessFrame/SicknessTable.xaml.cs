using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SicknessFrame
{
	/// <summary>
	/// Interaction logic for SicknessTable.xaml
	/// </summary>
	public partial class SicknessTable : Window
	{
		DataClasses1DataContext context;
		private string connectionString;
		
		public SicknessTable(string connstring)
		{
			this.connectionString = connstring;
			InitializeComponent();
		}

		void FillLstBox()
		{
			try
			{
				List <HR_absence> lstAbsence = new List<HR_absence>();
				lstAbsence = (from ab in this.context.HR_absences where ab.typeAbsence == "Болнични" select ab).ToList();
				
				ListBoxItem firstItem = new ListBoxItem();
				//TagListFirstRowControl first = new TagListFirstRowControl();
				//firstItem.Content = first;
				//lstTags.Items.Add(firstItem);
				foreach (HR_absence ab in lstAbsence)
				{
					SicknessRowControl sControl = new SicknessRowControl();
					
					string selectedValue = null;
					foreach (Port port in fm.PortDescriptorTbl)
					{
						if (port.Name != "MSGID")
						{
							selectedValue = port.VarType.ToString();
							break;
						}
					}
					ComboBoxItem selectedItem = new ComboBoxItem();
					foreach (Var_Type en in Enum.GetValues(typeof(Var_Type)))
					{
						ComboBoxItem cmboxItem = new ComboBoxItem();
						cmboxItem.Content = en.ToString();
						if (selectedValue == en.ToString())
						{
							selectedItem = cmboxItem;
						}
						sControl.cmboxDataType.Items.Add(cmboxItem);
					}
					sControl.cmboxDataType.SelectedItem = selectedItem;
					ListBoxItem lstItem = new ListBoxItem();
					lstItem.Content = sControl;
					lstTags.Items.Add(lstItem);
					
				}
			}
			catch
			{
				System.Windows.Forms.MessageBox.Show("There is problem filling tags list!", "Error!");
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.context = new DataClasses1DataContext(this.connectionString);
		}
	}
}
