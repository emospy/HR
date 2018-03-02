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
	/// Interaction logic for NKPDCheck.xaml
	/// </summary>
	public partial class NKPDCheck : Window
	{
		HRDataLayer.Entities data;
		public NKPDCheck(string connstring)
		{
			InitializeComponent();
			this.data = new HRDataLayer.Entities(connstring);
		}

		private void btnCheck_Click(object sender, RoutedEventArgs e)
		{

			 //var query = from person in people
			 //		   join pet in pets on person equals pet.Owner into gj
			 //		   from subpet in gj.DefaultIfEmpty()
			 //		   select new { person.FirstName, PetName = (subpet == null ? String.Empty : subpet.Name) };

			var ProblemPositions = (from p in this.data.HR_GlobalPositions
								   join n in this.data.HR_NKP on p.NKPCode equals n.code into nkp
								   from nk in nkp.DefaultIfEmpty()
								   where nk == null
								   //&& p.NKPCode != ""
								   select p).ToList();

			this.dgREsults.ItemsSource = ProblemPositions;
		}

		private void btnExport_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
