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
using HRDataLayer;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;
using System.IO;

namespace HolidayPlan
{
	/// <summary>
	/// Interaction logic for ReviewMessages.xaml
	/// </summary>
	public partial class ReviewMessages : Window
	{
		Entities data;
		int id_user;
		
		public ReviewMessages(string connectionstring, string CurrentUser)
		{
			InitializeComponent();
			this.data = new Entities(connectionstring);
			id_user = this.data.HR_Users.First(a => a.userName == CurrentUser).id;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var Messages = this.data.HR_Messages.Where(m => m.IsConfirmed == false);
			this.dgMessages.ItemsSource = Messages;
			this.cmbMessageTypes.ItemsSource = this.data.HR_MessageTypes.Select(a => a);
			this.dgcmbUsers.ItemsSource = this.data.HR_Users.Select(a => a);
			this.dpDateFrom.IsEnabled = false;
			this.dpDateTo.IsEnabled = false;
			this.cmbMessageTypes.IsEnabled = false;
			this.dpDateFrom.SelectedDate = DateTime.Now.AddMonths(-1);
			this.dpDateTo.SelectedDate = DateTime.Now;
		}

		private void dgMessages_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
		{
			if (e.EditAction == GridViewEditAction.Cancel)
			{
				return;
			}
			if (e.EditOperationType == GridViewEditOperationType.Edit)
			{
				var item = (HR_Messages)e.EditedItem;
				if (item.IsConfirmed == true)
				{
					item.id_user = this.id_user;
				}
				data.SaveChanges();
			}
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			this.cmbMessageTypes.IsEnabled = true;
		}

		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			this.cmbMessageTypes.IsEnabled = false;
		}

		private void cbDateFilter_Checked(object sender, RoutedEventArgs e)
		{
			this.dpDateFrom.IsEnabled = true;
			this.dpDateTo.IsEnabled = true;
		}

		private void cbDateFilter_Unchecked(object sender, RoutedEventArgs e)
		{
			this.dpDateFrom.IsEnabled = false;
			this.dpDateTo.IsEnabled = false;
		}

		private void btnShow_Click(object sender, RoutedEventArgs e)
		{
			var Messages = this.data.HR_Messages.Select(m => m);

			if (this.cbMessageType.IsChecked == true && this.cmbMessageTypes.SelectedItem != null)
			{
				var mt = (HR_MessageTypes)this.cmbMessageTypes.SelectedItem;
				Messages = Messages.Where(m => m.HR_MessageInstances.id_messageType == mt.id_messageType);
			}

			if (this.cbAllMessages.IsChecked == false)
			{
				Messages = Messages.Where(m => m.IsConfirmed == false);
			}

			if (this.cbDateFilter.IsChecked == true)
			{
				Messages = Messages.Where(m => m.DueDate >= this.dpDateFrom.SelectedDate && m.DueDate <= this.dpDateTo.SelectedDate);
			}

			this.dgMessages.ItemsSource = null;
			this.dgMessages.ItemsSource = Messages;
			this.dgMessages.Items.Refresh();
		}

		private void btnPrint_Click(object sender, RoutedEventArgs e)
		{
			using (FileStream stream = new FileStream("messages.xls", FileMode.Create))
			{
				this.dgMessages.Export(stream,
				 new GridViewExportOptions()
				 {
					 Format = ExportFormat.ExcelML,
					 ShowColumnHeaders = true,
					 ShowColumnFooters = true,
					 ShowGroupFooters = false,
				 });
				stream.Close();
			}
			System.Diagnostics.Process.Start("messages.xls");
		}
	}
}
