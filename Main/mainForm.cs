using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Resources;
using System.Reflection;
using System.Data;
using System.IO;
using DataLayer;
using System.Text;
using System.Collections.Generic;
//using Microsoft.Win32;
using System.Security.Permissions;
using SicknessFrame;
using HolidayPlan;
//using MySql;
using System.Data.EntityClient;
//using MySql.Data.MySqlClient;

namespace HR
{
	/// <summary>
	/// Summary description for mainForm.
	/// </summary>
	public class mainForm : System.Windows.Forms.Form
	{
		ExcelExpo Ex;
		/// <summary>
		/// Summary description for mainForm.
		/// </summary>
		private string tablePrefix;
		internal bool IsAtestaciiActive = false;
		internal bool IsLearningActive = false;
		internal DataAction userAction;
		internal DataTable dtUsers;
		internal string typeUser;
		internal DataSet dsOptions;
		internal bool IsRegistried;
		internal string User = "az";
		internal bool showInTaskBar
		{
			get { return ShowInTaskbar; }
			set { ShowInTaskbar = value; }
		}
		private DataAction action;
		internal string password = "tess";
		internal string database = "HRDB";
		internal string dbUser = "root";
		internal string dbHost = "localhost";
		internal DataTable dtKartoteka;

		internal string connString;
		//internal string connstring2;
		internal string EntityConectionString;
		#region Controls
		private System.Windows.Forms.MenuItem menu5Item3;
		private System.Windows.Forms.MenuItem menuNomenklaturi_Law;
		private System.Windows.Forms.MenuItem menuNomenklaturi_Rang;
		private System.Windows.Forms.MenuItem menuNomenklaturi_Experience;
		private System.Windows.Forms.MenuItem menuNomenklaturiYearlyAddon;
		private System.Windows.Forms.MenuItem menuNomenklaturi_reasonPenalty;
		private System.Windows.Forms.MenuItem menuNomenklaturi_TypePenalty;
		private System.Windows.Forms.StatusBar statusBarMain;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel2;
		private System.Windows.Forms.StatusBarPanel statusBarPanel3;
		private System.Windows.Forms.StatusBarPanel statusBarPanel4;
		MacAddress macAddress = new MacAddress();
		private System.Windows.Forms.Button buttonKartoteka;
		private System.Windows.Forms.Button buttonStructura;
		private System.Windows.Forms.Button buttonDlujnosti;
		private System.Windows.Forms.Button buttonShtatnoRazpisanie;
		private System.Windows.Forms.Button buttonObshtiSprawki;
		private System.Windows.Forms.Button buttonUsers;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.MenuItem menuSprawki_Free;
		private System.Windows.Forms.MenuItem menuSprawki_Holiday;
		private System.Windows.Forms.MenuItem menuItemSprawkiZZBUT;
		private System.Windows.Forms.MenuItem menuItemSprawkiAttestations;
		private System.Windows.Forms.MenuItem menu_Spravki_PSR;
		private System.Windows.Forms.MenuItem menuItem_Nomenklaturi_Educations;
		private IContainer components;
		private MenuItem menuItem1;
		private MenuItem menuItemProgramOptions;
		private MenuItem menuItem4;
		private MenuItem menuItemBackup;
		private MenuItem menuItemRestore;
		private MenuItem menuItemNKPDClass;
		private MenuItem menuItemMilitaryRangs;
		private MenuItem menuItemKartotekaHolidayPlan;
		private MenuItem menuItemSystemWorkDays;
		private MenuItem menuItemStatisticsLeadersHolidays;
		private MenuItem menuItemSyscosetWeekAbsences;
		private MenuItem menuItemOmegaExport;
		private MenuItem menuItemOmegaExportAdditional;
		private MenuItem menuItemCheckHolidays;
		private MenuItem menuItemOSRNSO;
		private MenuItem menuItemPaidHolidays;
		private MenuItem menuItem5;
		private MenuItem menuItemNKPDCheck;
		private MenuItem menuItem6;
		private MenuItem menuSettingsService;
		private MenuItem menuItemStructureEdit;
		private MenuItem menuItemNSOLastPosition;
        private MenuItem menuItemNSOOfficerPromotions;
		private MenuItem menuItem7;
		internal KartotekaLichenSystaw formKartoteka;
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>

		internal bool CheckConfigFile()
		{
			try
			{
				if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Config.xml"))
				{

					this.dsOptions.ReadXml(System.Windows.Forms.Application.StartupPath + "\\Config.xml", XmlReadMode.InferSchema);
					try
					{
						this.password = this.dsOptions.Tables[0].Rows[0]["password"].ToString();
					}
					catch
					{
						this.password = "tess";
					}
					try
					{
						this.database = this.dsOptions.Tables[0].Rows[0]["database"].ToString();
					}
					catch
					{
						this.database = "hrdb";
					}
					
					try
					{
						this.dbHost = this.dsOptions.Tables[0].Rows[0]["host"].ToString();
					}
					catch
					{
						this.dbHost = "localhost";
					}
					try
					{
						this.dbUser = this.dsOptions.Tables[0].Rows[0]["user"].ToString();
					}
					catch
					{
						this.dbUser = "root";
					}
					return true;
				}
				else
				{
					this.dsOptions.Tables[0].Rows.Add(new object[] { System.Windows.Forms.Application.StartupPath + "\\", "localhost", "root", "tess", "HRDB", "ActivationKey", "DBTypes" });
					MessageBox.Show("Първоначална настройка на програмата!");
					formOptions opt = new formOptions(this, false);
					opt.ShowDialog(this);
					//this.dsOptions.WriteXml( System.Windows.Forms.Application.StartupPath +"Config.xml", XmlWriteMode.WriteSchema );
				}
				return false;
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
				System.Diagnostics.Debug.Write("\\n" + exc.Message);
				return false;
			}
		}
		internal NomeclatureData nomenclaatureData;
		#region Items

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menu5Item1;
		private System.Windows.Forms.MenuItem menu5Item2;
		private System.Windows.Forms.MenuItem menuItemSickness;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuSpravki_Item21;
		private System.Windows.Forms.MenuItem menuSpravki_Item22;
		private System.Windows.Forms.MenuItem menuSpravki_Item23;
		private System.Windows.Forms.MenuItem menuSpravki_Item24;
		private System.Windows.Forms.MenuItem menuSpravki_Item25;
		private System.Windows.Forms.MenuItem menuSpravki_Item26;
		private System.Windows.Forms.MenuItem menuSpravki_Item27;
		private System.Windows.Forms.MenuItem menuSpravki_Item28;
		private System.Windows.Forms.MenuItem menuSpravki_Item29;
		private System.Windows.Forms.MenuItem menuSpravki_Item210;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuAdministartion2Item;
		private System.Windows.Forms.MenuItem Administartion2Item1;
		private System.Windows.Forms.MenuItem menuAdministartion2Item2;
		private System.Windows.Forms.MenuItem menuAdministartion2Item3;
		private System.Windows.Forms.MenuItem menuSpravki_Item;
		private System.Windows.Forms.MenuItem menuSpravki_Item1;
		private System.Windows.Forms.MenuItem menuNomenklaturi_Item;
		private System.Windows.Forms.MenuItem menuNomenklaturi_ProfessionClassifier1;
		private System.Windows.Forms.MenuItem menuNomenklaturi_ClassifierID2;
		private System.Windows.Forms.MenuItem menuNomenklaturi_Osnovaniq3;
		private System.Windows.Forms.MenuItem menuNomenklaturi_ZaPredpriqtie;
		private System.Windows.Forms.MenuItem menuNomenklaturi_ZaLice;
		private System.Windows.Forms.MenuItem menuNomenklaturi_Education;
		private System.Windows.Forms.MenuItem menuNomenklaturi_ForeignLanguages;
		private System.Windows.Forms.MenuItem menuNomenklaturi_MilitaryRang;
		private System.Windows.Forms.MenuItem menuNomenklaturi_ScienceTitle;
		private System.Windows.Forms.MenuItem menuNomenklaturi_ScienceDegree;
		private System.Windows.Forms.MenuItem menuReasonAssignment33;
		private System.Windows.Forms.MenuItem menuReasonFired34;
		private System.Windows.Forms.MenuItem menuKartoteka;
		private System.Windows.Forms.MenuItem menuKartoteka_KartotekaLS;
		private System.Windows.Forms.MenuItem menuKartoteka_KartotekaCanceled;
		private System.Windows.Forms.MenuItem menuKartoteka_Exit;
		private System.Windows.Forms.MenuItem menuNomenklaturi_WorkTime;
		private System.Windows.Forms.MenuItem menuSystem;
		private System.Windows.Forms.MenuItem menuNomenklaturi_NKDS;

		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public mainForm()
		{
			//SplashScreen.ShowSplashScreen();
			this.dsOptions = new DataSet();
			DataTable table = new DataTable();
			table.Columns.Add("workingDir", System.Type.GetType("System.String"));
			table.Columns.Add("Host", System.Type.GetType("System.String"));
			table.Columns.Add("User", System.Type.GetType("System.String"));
			table.Columns.Add("Password", System.Type.GetType("System.String"));
			table.Columns.Add("Database", System.Type.GetType("System.String"));
			table.Columns.Add("ActivationKey", System.Type.GetType("System.String"));
			table.Columns.Add(" DBTypes", System.Type.GetType("System.String"));
			this.dsOptions.Tables.Add(table);
			InitializeComponent();
			Ex = new ExcelExpo();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.Controls != null)
				{
					foreach (Control control in this.Controls)
					{
						try
						{
							if (control != null)
							{
								control.Dispose();
							}
						}
						catch (System.NullReferenceException)
						{
						}
					}
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuKartoteka = new System.Windows.Forms.MenuItem();
			this.menuKartoteka_KartotekaLS = new System.Windows.Forms.MenuItem();
			this.menuKartoteka_KartotekaCanceled = new System.Windows.Forms.MenuItem();
			this.menuItemPaidHolidays = new System.Windows.Forms.MenuItem();
			this.menuItemSickness = new System.Windows.Forms.MenuItem();
			this.menuItemKartotekaHolidayPlan = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuKartoteka_Exit = new System.Windows.Forms.MenuItem();
			this.menuAdministartion2Item = new System.Windows.Forms.MenuItem();
			this.Administartion2Item1 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuAdministartion2Item2 = new System.Windows.Forms.MenuItem();
			this.menuAdministartion2Item3 = new System.Windows.Forms.MenuItem();
			this.menuItemStructureEdit = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item1 = new System.Windows.Forms.MenuItem();
			this.menu_Spravki_PSR = new System.Windows.Forms.MenuItem();
			this.menuSprawki_Free = new System.Windows.Forms.MenuItem();
			this.menuSprawki_Holiday = new System.Windows.Forms.MenuItem();
			this.menuItemStatisticsLeadersHolidays = new System.Windows.Forms.MenuItem();
			this.menuItemSprawkiZZBUT = new System.Windows.Forms.MenuItem();
			this.menuItemSprawkiAttestations = new System.Windows.Forms.MenuItem();
			this.menuItemMilitaryRangs = new System.Windows.Forms.MenuItem();
			this.menuItemSyscosetWeekAbsences = new System.Windows.Forms.MenuItem();
			this.menuItemOmegaExport = new System.Windows.Forms.MenuItem();
			this.menuItemOmegaExportAdditional = new System.Windows.Forms.MenuItem();
			this.menuItemOSRNSO = new System.Windows.Forms.MenuItem();
			this.menuItemNSOLastPosition = new System.Windows.Forms.MenuItem();
			this.menuItemNSOOfficerPromotions = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_Item = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_ProfessionClassifier1 = new System.Windows.Forms.MenuItem();
			this.menuItemNKPDClass = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_ClassifierID2 = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_NKDS = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_Osnovaniq3 = new System.Windows.Forms.MenuItem();
			this.menuReasonAssignment33 = new System.Windows.Forms.MenuItem();
			this.menuReasonFired34 = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_reasonPenalty = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_ZaPredpriqtie = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_WorkTime = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_Law = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturiYearlyAddon = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_TypePenalty = new System.Windows.Forms.MenuItem();
			this.menuItem_Nomenklaturi_Educations = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_ZaLice = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_Education = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_ForeignLanguages = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_MilitaryRang = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_ScienceTitle = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_ScienceDegree = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_Rang = new System.Windows.Forms.MenuItem();
			this.menuNomenklaturi_Experience = new System.Windows.Forms.MenuItem();
			this.menuSystem = new System.Windows.Forms.MenuItem();
			this.menu5Item1 = new System.Windows.Forms.MenuItem();
			this.menu5Item2 = new System.Windows.Forms.MenuItem();
			this.menu5Item3 = new System.Windows.Forms.MenuItem();
			this.menuItemSystemWorkDays = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemBackup = new System.Windows.Forms.MenuItem();
			this.menuItemRestore = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItemProgramOptions = new System.Windows.Forms.MenuItem();
			this.menuItemCheckHolidays = new System.Windows.Forms.MenuItem();
			this.menuItemNKPDCheck = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuSettingsService = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item21 = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item22 = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item23 = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item24 = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item25 = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item26 = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item27 = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item28 = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item29 = new System.Windows.Forms.MenuItem();
			this.menuSpravki_Item210 = new System.Windows.Forms.MenuItem();
			this.statusBarMain = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel4 = new System.Windows.Forms.StatusBarPanel();
			this.buttonKartoteka = new System.Windows.Forms.Button();
			this.buttonStructura = new System.Windows.Forms.Button();
			this.buttonDlujnosti = new System.Windows.Forms.Button();
			this.buttonShtatnoRazpisanie = new System.Windows.Forms.Button();
			this.buttonObshtiSprawki = new System.Windows.Forms.Button();
			this.buttonUsers = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel4)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuKartoteka,
            this.menuAdministartion2Item,
            this.menuSpravki_Item,
            this.menuNomenklaturi_Item,
            this.menuSystem});
			// 
			// menuKartoteka
			// 
			this.menuKartoteka.Index = 0;
			this.menuKartoteka.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuKartoteka_KartotekaLS,
            this.menuKartoteka_KartotekaCanceled,
            this.menuItemPaidHolidays,
            this.menuItemSickness,
            this.menuItemKartotekaHolidayPlan,
            this.menuItem5,
            this.menuItem2,
            this.menuKartoteka_Exit});
			this.menuKartoteka.Text = "Работа с картотеката";
			// 
			// menuKartoteka_KartotekaLS
			// 
			this.menuKartoteka_KartotekaLS.Index = 0;
			this.menuKartoteka_KartotekaLS.Shortcut = System.Windows.Forms.Shortcut.F4;
			this.menuKartoteka_KartotekaLS.Text = "Картотека личен състав";
			this.menuKartoteka_KartotekaLS.Click += new System.EventHandler(this.menuKartoteka_KartotekaLS_Click);
			// 
			// menuKartoteka_KartotekaCanceled
			// 
			this.menuKartoteka_KartotekaCanceled.Index = 1;
			this.menuKartoteka_KartotekaCanceled.Text = "Картотека прекратени договори";
			this.menuKartoteka_KartotekaCanceled.Click += new System.EventHandler(this.menuKartoteka_KartotekaCanceled_Click);
			// 
			// menuItemPaidHolidays
			// 
			this.menuItemPaidHolidays.Index = 2;
			this.menuItemPaidHolidays.Text = "Картотека полагаем годишен отпуск";
			this.menuItemPaidHolidays.Click += new System.EventHandler(this.menuItemPaidHolidays_Click);
			// 
			// menuItemSickness
			// 
			this.menuItemSickness.Index = 3;
			this.menuItemSickness.Text = "Картотека болнични";
			this.menuItemSickness.Click += new System.EventHandler(this.menuItemSickness_Click);
			// 
			// menuItemKartotekaHolidayPlan
			// 
			this.menuItemKartotekaHolidayPlan.Index = 4;
			this.menuItemKartotekaHolidayPlan.Text = "График отпуски";
			this.menuItemKartotekaHolidayPlan.Click += new System.EventHandler(this.menuItemKartotekaHolidayPlan_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 5;
			this.menuItem5.Text = "Съобщения";
			this.menuItem5.Click += new System.EventHandler(this.menuItemMessages_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 6;
			this.menuItem2.Text = "-";
			// 
			// menuKartoteka_Exit
			// 
			this.menuKartoteka_Exit.Index = 7;
			this.menuKartoteka_Exit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
			this.menuKartoteka_Exit.Text = "Изход";
			this.menuKartoteka_Exit.Click += new System.EventHandler(this.menuKartoteka_AddNewEmployee_Click_2);
			// 
			// menuAdministartion2Item
			// 
			this.menuAdministartion2Item.Index = 1;
			this.menuAdministartion2Item.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Administartion2Item1,
            this.menuItem3,
            this.menuAdministartion2Item2,
            this.menuAdministartion2Item3,
            this.menuItemStructureEdit});
			this.menuAdministartion2Item.Text = "Организацията";
			// 
			// Administartion2Item1
			// 
			this.Administartion2Item1.Index = 0;
			this.Administartion2Item1.Text = "Регистрация";
			this.Administartion2Item1.Click += new System.EventHandler(this.menuAdministration_Register_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// menuAdministartion2Item2
			// 
			this.menuAdministartion2Item2.Index = 2;
			this.menuAdministartion2Item2.Text = "Структура на организацията";
			this.menuAdministartion2Item2.Click += new System.EventHandler(this.menuAdministartion_Structure_Click);
			// 
			// menuAdministartion2Item3
			// 
			this.menuAdministartion2Item3.Index = 3;
			this.menuAdministartion2Item3.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.menuAdministartion2Item3.Text = "Длъжности в организацията";
			this.menuAdministartion2Item3.Click += new System.EventHandler(this.menuAdministartion_GlobalPositions_Click);
			// 
			// menuItemStructureEdit
			// 
			this.menuItemStructureEdit.Index = 4;
			this.menuItemStructureEdit.Text = "Редакция на структурата";
			this.menuItemStructureEdit.Click += new System.EventHandler(this.menuItemStructureEdit_Click);
			// 
			// menuSpravki_Item
			// 
			this.menuSpravki_Item.Index = 2;
			this.menuSpravki_Item.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuSpravki_Item1,
            this.menu_Spravki_PSR,
            this.menuSprawki_Free,
            this.menuSprawki_Holiday,
            this.menuItemStatisticsLeadersHolidays,
            this.menuItemSprawkiZZBUT,
            this.menuItemSprawkiAttestations,
            this.menuItemMilitaryRangs,
            this.menuItemSyscosetWeekAbsences,
            this.menuItemOmegaExport,
            this.menuItemOmegaExportAdditional,
            this.menuItemOSRNSO,
            this.menuItemNSOLastPosition,
            this.menuItemNSOOfficerPromotions});
			this.menuSpravki_Item.Text = "Справки";
			// 
			// menuSpravki_Item1
			// 
			this.menuSpravki_Item1.Index = 0;
			this.menuSpravki_Item1.Text = "Длъжностно щатно разписание";
			this.menuSpravki_Item1.Click += new System.EventHandler(this.menuSpravki_Staff_Click);
			// 
			// menu_Spravki_PSR
			// 
			this.menu_Spravki_PSR.Index = 1;
			this.menu_Spravki_PSR.Text = "Поименно щатно разписание";
			this.menu_Spravki_PSR.Click += new System.EventHandler(this.menu_Spravki_PSR_Click);
			// 
			// menuSprawki_Free
			// 
			this.menuSprawki_Free.Index = 2;
			this.menuSprawki_Free.Text = "Заети и свободни работни места";
			this.menuSprawki_Free.Click += new System.EventHandler(this.menuSprawki_Free_Click);
			// 
			// menuSprawki_Holiday
			// 
			this.menuSprawki_Holiday.Index = 3;
			this.menuSprawki_Holiday.Text = "Полагаем годишен отпуск";
			this.menuSprawki_Holiday.Click += new System.EventHandler(this.menuSprawki_Holiday_Click);
			// 
			// menuItemStatisticsLeadersHolidays
			// 
			this.menuItemStatisticsLeadersHolidays.Index = 4;
			this.menuItemStatisticsLeadersHolidays.Text = "Полагаем годишен отпуск ръководни длъжности";
			this.menuItemStatisticsLeadersHolidays.Click += new System.EventHandler(this.menuItemStatisticsLeadersHolidays_Click);
			// 
			// menuItemSprawkiZZBUT
			// 
			this.menuItemSprawkiZZBUT.Index = 5;
			this.menuItemSprawkiZZBUT.Text = "Справка по ЗЗБУТ";
			this.menuItemSprawkiZZBUT.Click += new System.EventHandler(this.menuItemSprawkiZZBUT_Click);
			// 
			// menuItemSprawkiAttestations
			// 
			this.menuItemSprawkiAttestations.Index = 6;
			this.menuItemSprawkiAttestations.Text = "Обща история на атестации";
			this.menuItemSprawkiAttestations.Click += new System.EventHandler(this.menuItemSprawkiAttestations_Click);
			// 
			// menuItemMilitaryRangs
			// 
			this.menuItemMilitaryRangs.Index = 7;
			this.menuItemMilitaryRangs.Text = "Повишаване във военно звание";
			this.menuItemMilitaryRangs.Click += new System.EventHandler(this.menuItemMilitaryRangs_Click);
			// 
			// menuItemSyscosetWeekAbsences
			// 
			this.menuItemSyscosetWeekAbsences.Index = 8;
			this.menuItemSyscosetWeekAbsences.Text = "Сискосет седмичен отчет";
			this.menuItemSyscosetWeekAbsences.Click += new System.EventHandler(this.menuItemSyscosetWeekAbsences_Click);
			// 
			// menuItemOmegaExport
			// 
			this.menuItemOmegaExport.Index = 9;
			this.menuItemOmegaExport.Text = "Експорт на данни за назначения към Омега";
			this.menuItemOmegaExport.Click += new System.EventHandler(this.menuItemOmegaExport_Click);
			// 
			// menuItemOmegaExportAdditional
			// 
			this.menuItemOmegaExportAdditional.Index = 10;
			this.menuItemOmegaExportAdditional.Text = "Експорт на данни за допълнителни споразумения към Омега";
			this.menuItemOmegaExportAdditional.Click += new System.EventHandler(this.menuItemOmegaExportAdditional_Click);
			// 
			// menuItemOSRNSO
			// 
			this.menuItemOSRNSO.Index = 11;
			this.menuItemOSRNSO.Text = "Длъжностно разписание НСО";
			this.menuItemOSRNSO.Click += new System.EventHandler(this.menuItemOSRNSO_Click);
			// 
			// menuItemNSOLastPosition
			// 
			this.menuItemNSOLastPosition.Index = 12;
			this.menuItemNSOLastPosition.Text = "Служители по последно заемана длъжност";
			this.menuItemNSOLastPosition.Click += new System.EventHandler(this.menuItemNSOLastPosition_Click);
			// 
			// menuItemNSOOfficerPromotions
			// 
			this.menuItemNSOOfficerPromotions.Index = 13;
			this.menuItemNSOOfficerPromotions.Text = "Служители повишени в офицерско звание";
			this.menuItemNSOOfficerPromotions.Click += new System.EventHandler(this.menuItemNSOOfficerPromotions_Click);
			// 
			// menuNomenklaturi_Item
			// 
			this.menuNomenklaturi_Item.Index = 3;
			this.menuNomenklaturi_Item.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuNomenklaturi_ProfessionClassifier1,
            this.menuItemNKPDClass,
            this.menuNomenklaturi_ClassifierID2,
            this.menuNomenklaturi_NKDS,
            this.menuItem9,
            this.menuNomenklaturi_Osnovaniq3,
            this.menuItem10,
            this.menuNomenklaturi_ZaPredpriqtie,
            this.menuNomenklaturi_ZaLice});
			this.menuNomenklaturi_Item.Text = "Номенклатури";
			// 
			// menuNomenklaturi_ProfessionClassifier1
			// 
			this.menuNomenklaturi_ProfessionClassifier1.Index = 0;
			this.menuNomenklaturi_ProfessionClassifier1.Text = "Класификатор на професии - НКПД 2011";
			this.menuNomenklaturi_ProfessionClassifier1.Click += new System.EventHandler(this.menuNomenklaturi_ProfessionClassifier_Click);
			// 
			// menuItemNKPDClass
			// 
			this.menuItemNKPDClass.Index = 1;
			this.menuItemNKPDClass.Text = "Класове по НКПД";
			this.menuItemNKPDClass.Click += new System.EventHandler(this.menuItemNKPDClass_Click);
			// 
			// menuNomenklaturi_ClassifierID2
			// 
			this.menuNomenklaturi_ClassifierID2.Index = 2;
			this.menuNomenklaturi_ClassifierID2.Text = "Класификатор на икономически дейности";
			this.menuNomenklaturi_ClassifierID2.Click += new System.EventHandler(this.menuNomenklaturi_ClassifierID_Click);
			// 
			// menuNomenklaturi_NKDS
			// 
			this.menuNomenklaturi_NKDS.Index = 3;
			this.menuNomenklaturi_NKDS.Text = "Единен класификатор на длъжностите в администрацията";
			this.menuNomenklaturi_NKDS.Click += new System.EventHandler(this.menuNomenklaturi_KlasifikatorDS_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 4;
			this.menuItem9.Text = "-";
			// 
			// menuNomenklaturi_Osnovaniq3
			// 
			this.menuNomenklaturi_Osnovaniq3.Index = 5;
			this.menuNomenklaturi_Osnovaniq3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuReasonAssignment33,
            this.menuReasonFired34,
            this.menuNomenklaturi_reasonPenalty});
			this.menuNomenklaturi_Osnovaniq3.Text = "Основания";
			// 
			// menuReasonAssignment33
			// 
			this.menuReasonAssignment33.Index = 0;
			this.menuReasonAssignment33.Text = "Основания за назначаване";
			this.menuReasonAssignment33.Click += new System.EventHandler(this.menuNomenklaturi_ReasonAssignment_Click);
			// 
			// menuReasonFired34
			// 
			this.menuReasonFired34.Index = 1;
			this.menuReasonFired34.Text = "Основания за освобождаване";
			this.menuReasonFired34.Click += new System.EventHandler(this.menuNomenklatures_ReasonFired_Click);
			// 
			// menuNomenklaturi_reasonPenalty
			// 
			this.menuNomenklaturi_reasonPenalty.Index = 2;
			this.menuNomenklaturi_reasonPenalty.Text = "Основания за наказание";
			this.menuNomenklaturi_reasonPenalty.Click += new System.EventHandler(this.menuNomenklaturi_reasonPenalty_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 6;
			this.menuItem10.Text = "-";
			// 
			// menuNomenklaturi_ZaPredpriqtie
			// 
			this.menuNomenklaturi_ZaPredpriqtie.Index = 7;
			this.menuNomenklaturi_ZaPredpriqtie.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuNomenklaturi_WorkTime,
            this.menuNomenklaturi_Law,
            this.menuNomenklaturiYearlyAddon,
            this.menuNomenklaturi_TypePenalty,
            this.menuItem_Nomenklaturi_Educations});
			this.menuNomenklaturi_ZaPredpriqtie.Text = "За организацията";
			// 
			// menuNomenklaturi_WorkTime
			// 
			this.menuNomenklaturi_WorkTime.Index = 0;
			this.menuNomenklaturi_WorkTime.Text = "Работно време";
			this.menuNomenklaturi_WorkTime.Click += new System.EventHandler(this.menuNomenklaturi_WorkTime_Click);
			// 
			// menuNomenklaturi_Law
			// 
			this.menuNomenklaturi_Law.Index = 1;
			this.menuNomenklaturi_Law.Text = "Правоотношения";
			this.menuNomenklaturi_Law.Click += new System.EventHandler(this.menuNomenklaturi_Law_Click);
			// 
			// menuNomenklaturiYearlyAddon
			// 
			this.menuNomenklaturiYearlyAddon.Index = 2;
			this.menuNomenklaturiYearlyAddon.Text = "Годишни надбавки";
			this.menuNomenklaturiYearlyAddon.Click += new System.EventHandler(this.menuNomenklaturiYearlyAddon_Click);
			// 
			// menuNomenklaturi_TypePenalty
			// 
			this.menuNomenklaturi_TypePenalty.Index = 3;
			this.menuNomenklaturi_TypePenalty.Text = "Видове наказания";
			this.menuNomenklaturi_TypePenalty.Click += new System.EventHandler(this.menuNomenklaturi_TypePenalty_Click);
			// 
			// menuItem_Nomenklaturi_Educations
			// 
			this.menuItem_Nomenklaturi_Educations.Index = 4;
			this.menuItem_Nomenklaturi_Educations.Text = "Обучения";
			this.menuItem_Nomenklaturi_Educations.Click += new System.EventHandler(this.menuItem_Nomenklaturi_Educations_Click);
			// 
			// menuNomenklaturi_ZaLice
			// 
			this.menuNomenklaturi_ZaLice.Index = 8;
			this.menuNomenklaturi_ZaLice.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuNomenklaturi_Education,
            this.menuNomenklaturi_ForeignLanguages,
            this.menuNomenklaturi_MilitaryRang,
            this.menuNomenklaturi_ScienceTitle,
            this.menuNomenklaturi_ScienceDegree,
            this.menuNomenklaturi_Rang,
            this.menuNomenklaturi_Experience,
            this.menuItem7});
			this.menuNomenklaturi_ZaLice.Text = "За Лицето";
			// 
			// menuNomenklaturi_Education
			// 
			this.menuNomenklaturi_Education.Index = 0;
			this.menuNomenklaturi_Education.Text = "Образование";
			this.menuNomenklaturi_Education.Click += new System.EventHandler(this.menuNomenklaturi_Education_Click);
			// 
			// menuNomenklaturi_ForeignLanguages
			// 
			this.menuNomenklaturi_ForeignLanguages.Index = 1;
			this.menuNomenklaturi_ForeignLanguages.Text = "Чужди езици";
			this.menuNomenklaturi_ForeignLanguages.Click += new System.EventHandler(this.menuNomenklaturi_ForeignLanguages_Click);
			// 
			// menuNomenklaturi_MilitaryRang
			// 
			this.menuNomenklaturi_MilitaryRang.Index = 2;
			this.menuNomenklaturi_MilitaryRang.Text = "Военен ранг";
			this.menuNomenklaturi_MilitaryRang.Click += new System.EventHandler(this.menuNomenklaturi_MilitaryRang_Click);
			// 
			// menuNomenklaturi_ScienceTitle
			// 
			this.menuNomenklaturi_ScienceTitle.Index = 3;
			this.menuNomenklaturi_ScienceTitle.Text = "Научно звание";
			this.menuNomenklaturi_ScienceTitle.Click += new System.EventHandler(this.menuNomenklaturi_ScienceTitle_Click);
			// 
			// menuNomenklaturi_ScienceDegree
			// 
			this.menuNomenklaturi_ScienceDegree.Index = 4;
			this.menuNomenklaturi_ScienceDegree.Text = "Научна степен";
			this.menuNomenklaturi_ScienceDegree.Click += new System.EventHandler(this.menuNomenklaturi_ScienceDegree_Click);
			// 
			// menuNomenklaturi_Rang
			// 
			this.menuNomenklaturi_Rang.Index = 5;
			this.menuNomenklaturi_Rang.Text = "Ранг";
			this.menuNomenklaturi_Rang.Click += new System.EventHandler(this.menuNomenklaturi_Rang_Click);
			// 
			// menuNomenklaturi_Experience
			// 
			this.menuNomenklaturi_Experience.Index = 6;
			this.menuNomenklaturi_Experience.Text = "Професионален опит";
			this.menuNomenklaturi_Experience.Click += new System.EventHandler(this.menuNomenklaturi_Experience_Click);
			// 
			// menuSystem
			// 
			this.menuSystem.Index = 4;
			this.menuSystem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menu5Item1,
            this.menu5Item2,
            this.menu5Item3,
            this.menuItemSystemWorkDays,
            this.menuItem1,
            this.menuItemBackup,
            this.menuItemRestore,
            this.menuItem4,
            this.menuItemProgramOptions,
            this.menuItemCheckHolidays,
            this.menuItemNKPDCheck,
            this.menuItem6,
            this.menuSettingsService});
			this.menuSystem.Text = "Системни";
			// 
			// menu5Item1
			// 
			this.menu5Item1.Index = 0;
			this.menu5Item1.Text = "Потребители";
			this.menu5Item1.Click += new System.EventHandler(this.menuSystem_Users_Click);
			// 
			// menu5Item2
			// 
			this.menu5Item2.Index = 1;
			this.menu5Item2.Shortcut = System.Windows.Forms.Shortcut.F8;
			this.menu5Item2.Text = "Смяна на потребители";
			this.menu5Item2.Click += new System.EventHandler(this.menuSystem_LogIn_Click);
			// 
			// menu5Item3
			// 
			this.menu5Item3.Index = 2;
			this.menu5Item3.Text = "Приключване на година";
			this.menu5Item3.Click += new System.EventHandler(this.menuSystem_FinishYear_Click);
			// 
			// menuItemSystemWorkDays
			// 
			this.menuItemSystemWorkDays.Index = 3;
			this.menuItemSystemWorkDays.Text = "Задаване на работни дни";
			this.menuItemSystemWorkDays.Click += new System.EventHandler(this.menuItemSystemWorkDays_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 4;
			this.menuItem1.Text = "-";
			// 
			// menuItemBackup
			// 
			this.menuItemBackup.Index = 5;
			this.menuItemBackup.Text = "Архивиране";
			this.menuItemBackup.Click += new System.EventHandler(this.menuItemBackup_Click);
			// 
			// menuItemRestore
			// 
			this.menuItemRestore.Index = 6;
			this.menuItemRestore.Text = "Възстановяване";
			this.menuItemRestore.Click += new System.EventHandler(this.menuItemRestore_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 7;
			this.menuItem4.Text = "-";
			// 
			// menuItemProgramOptions
			// 
			this.menuItemProgramOptions.Index = 8;
			this.menuItemProgramOptions.Text = "Настройки";
			this.menuItemProgramOptions.Click += new System.EventHandler(this.menuSystem_ProgramOptions_Click);
			// 
			// menuItemCheckHolidays
			// 
			this.menuItemCheckHolidays.Index = 9;
			this.menuItemCheckHolidays.Text = "Проверка коректност на отпуски";
			this.menuItemCheckHolidays.Click += new System.EventHandler(this.menuItemCheckHolidays_Click);
			// 
			// menuItemNKPDCheck
			// 
			this.menuItemNKPDCheck.Index = 10;
			this.menuItemNKPDCheck.Text = "Проверка НКПД";
			this.menuItemNKPDCheck.Click += new System.EventHandler(this.menuItemNKPDCheck_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 11;
			this.menuItem6.Text = "Проверка структура на организацията";
			// 
			// menuSettingsService
			// 
			this.menuSettingsService.Index = 12;
			this.menuSettingsService.Text = "Сервизни функции";
			this.menuSettingsService.Click += new System.EventHandler(this.menuSettingsService_Click);
			// 
			// menuSpravki_Item21
			// 
			this.menuSpravki_Item21.Index = -1;
			this.menuSpravki_Item21.Text = "Служители на длъжност";
			// 
			// menuSpravki_Item22
			// 
			this.menuSpravki_Item22.Index = -1;
			this.menuSpravki_Item22.Text = "Служители владеещи чужд език";
			// 
			// menuSpravki_Item23
			// 
			this.menuSpravki_Item23.Index = -1;
			this.menuSpravki_Item23.Text = "Отсъствия на служители";
			// 
			// menuSpravki_Item24
			// 
			this.menuSpravki_Item24.Index = -1;
			this.menuSpravki_Item24.Text = "Служители по образование";
			// 
			// menuSpravki_Item25
			// 
			this.menuSpravki_Item25.Index = -1;
			this.menuSpravki_Item25.Text = "Служители на военен отчет";
			// 
			// menuSpravki_Item26
			// 
			this.menuSpravki_Item26.Index = -1;
			this.menuSpravki_Item26.Text = "Служители с (определено) работно време";
			// 
			// menuSpravki_Item27
			// 
			this.menuSpravki_Item27.Index = -1;
			this.menuSpravki_Item27.Text = "Служители с договор";
			// 
			// menuSpravki_Item28
			// 
			this.menuSpravki_Item28.Index = -1;
			this.menuSpravki_Item28.Text = "Отпуски от минали години";
			// 
			// menuSpravki_Item29
			// 
			this.menuSpravki_Item29.Index = -1;
			this.menuSpravki_Item29.Text = "Отпуски за текущата година";
			// 
			// menuSpravki_Item210
			// 
			this.menuSpravki_Item210.Index = -1;
			this.menuSpravki_Item210.Text = "Служители с неплатен отпуск";
			// 
			// statusBarMain
			// 
			this.statusBarMain.Location = new System.Drawing.Point(0, 78);
			this.statusBarMain.Name = "statusBarMain";
			this.statusBarMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanel2,
            this.statusBarPanel3,
            this.statusBarPanel4});
			this.statusBarMain.ShowPanels = true;
			this.statusBarMain.Size = new System.Drawing.Size(764, 23);
			this.statusBarMain.SizingGrip = false;
			this.statusBarMain.TabIndex = 1;
			this.statusBarMain.Text = "statusBar1";
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.Name = "statusBarPanel1";
			this.statusBarPanel1.Text = "User";
			this.statusBarPanel1.Width = 190;
			// 
			// statusBarPanel2
			// 
			this.statusBarPanel2.Name = "statusBarPanel2";
			this.statusBarPanel2.Text = "Computer name";
			this.statusBarPanel2.Width = 210;
			// 
			// statusBarPanel3
			// 
			this.statusBarPanel3.Name = "statusBarPanel3";
			this.statusBarPanel3.Text = "Ip address";
			this.statusBarPanel3.Width = 240;
			// 
			// statusBarPanel4
			// 
			this.statusBarPanel4.Name = "statusBarPanel4";
			this.statusBarPanel4.Text = "Date";
			this.statusBarPanel4.Width = 130;
			// 
			// buttonKartoteka
			// 
			this.buttonKartoteka.Image = ((System.Drawing.Image)(resources.GetObject("buttonKartoteka.Image")));
			this.buttonKartoteka.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.buttonKartoteka.Location = new System.Drawing.Point(8, 3);
			this.buttonKartoteka.Name = "buttonKartoteka";
			this.buttonKartoteka.Size = new System.Drawing.Size(100, 56);
			this.buttonKartoteka.TabIndex = 2;
			this.buttonKartoteka.Text = "Картотека";
			this.buttonKartoteka.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonKartoteka.Click += new System.EventHandler(this.buttonKartoteka_Click);
			// 
			// buttonStructura
			// 
			this.buttonStructura.Image = ((System.Drawing.Image)(resources.GetObject("buttonStructura.Image")));
			this.buttonStructura.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.buttonStructura.Location = new System.Drawing.Point(116, 3);
			this.buttonStructura.Name = "buttonStructura";
			this.buttonStructura.Size = new System.Drawing.Size(100, 56);
			this.buttonStructura.TabIndex = 3;
			this.buttonStructura.Text = "Структура";
			this.buttonStructura.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonStructura.Click += new System.EventHandler(this.buttonStructura_Click);
			// 
			// buttonDlujnosti
			// 
			this.buttonDlujnosti.Image = ((System.Drawing.Image)(resources.GetObject("buttonDlujnosti.Image")));
			this.buttonDlujnosti.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.buttonDlujnosti.Location = new System.Drawing.Point(224, 3);
			this.buttonDlujnosti.Name = "buttonDlujnosti";
			this.buttonDlujnosti.Size = new System.Drawing.Size(100, 56);
			this.buttonDlujnosti.TabIndex = 4;
			this.buttonDlujnosti.Text = "Длъжности";
			this.buttonDlujnosti.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonDlujnosti.Click += new System.EventHandler(this.buttonDlujnosti_Click);
			// 
			// buttonShtatnoRazpisanie
			// 
			this.buttonShtatnoRazpisanie.Image = ((System.Drawing.Image)(resources.GetObject("buttonShtatnoRazpisanie.Image")));
			this.buttonShtatnoRazpisanie.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.buttonShtatnoRazpisanie.Location = new System.Drawing.Point(440, 3);
			this.buttonShtatnoRazpisanie.Name = "buttonShtatnoRazpisanie";
			this.buttonShtatnoRazpisanie.Size = new System.Drawing.Size(100, 56);
			this.buttonShtatnoRazpisanie.TabIndex = 5;
			this.buttonShtatnoRazpisanie.Text = "Щатно разписание";
			this.buttonShtatnoRazpisanie.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonShtatnoRazpisanie.Click += new System.EventHandler(this.buttonShtatnoRazpisanie_Click);
			// 
			// buttonObshtiSprawki
			// 
			this.buttonObshtiSprawki.Image = ((System.Drawing.Image)(resources.GetObject("buttonObshtiSprawki.Image")));
			this.buttonObshtiSprawki.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.buttonObshtiSprawki.Location = new System.Drawing.Point(332, 3);
			this.buttonObshtiSprawki.Name = "buttonObshtiSprawki";
			this.buttonObshtiSprawki.Size = new System.Drawing.Size(100, 56);
			this.buttonObshtiSprawki.TabIndex = 6;
			this.buttonObshtiSprawki.Text = "Регистрация";
			this.buttonObshtiSprawki.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonObshtiSprawki.Click += new System.EventHandler(this.buttonObshtiSprawki_Click);
			// 
			// buttonUsers
			// 
			this.buttonUsers.Image = ((System.Drawing.Image)(resources.GetObject("buttonUsers.Image")));
			this.buttonUsers.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.buttonUsers.Location = new System.Drawing.Point(548, 3);
			this.buttonUsers.Name = "buttonUsers";
			this.buttonUsers.Size = new System.Drawing.Size(100, 56);
			this.buttonUsers.TabIndex = 7;
			this.buttonUsers.Text = "Настройки";
			this.buttonUsers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonUsers.Click += new System.EventHandler(this.buttonUsers_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
			this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.buttonExit.Location = new System.Drawing.Point(656, 3);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(100, 56);
			this.buttonExit.TabIndex = 8;
			this.buttonExit.Text = "Изход";
			this.buttonExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 7;
			this.menuItem7.Text = "Звание НАТО";
			this.menuItem7.Click += new System.EventHandler(this.menuNomenklaturi_NSONato_Click);
			// 
			// mainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(764, 101);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonUsers);
			this.Controls.Add(this.buttonObshtiSprawki);
			this.Controls.Add(this.buttonShtatnoRazpisanie);
			this.Controls.Add(this.buttonDlujnosti);
			this.Controls.Add(this.buttonStructura);
			this.Controls.Add(this.buttonKartoteka);
			this.Controls.Add(this.statusBarMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(780, 140);
			this.Menu = this.mainMenu1;
			this.MinimumSize = new System.Drawing.Size(780, 140);
			this.Name = "mainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Човешки Ресурси 2017 2.58";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.mainForm_Closing);
			this.Load += new System.EventHandler(this.mainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel4)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void LoadNomenklatureInLists()
		{
			DataTable dt = new DataTable();
			ArrayList ErrorLog = new ArrayList();
			DataAction daa = new DataAction(this.connString);
			this.nomenclaatureData = new NomeclatureData();


			this.nomenclaatureData.dtReasonAssignment = daa.SelectWhere(TableNames.ReasonAssignment, "*", "ORDER BY id");
			if (this.nomenclaatureData.dtReasonAssignment == null)
			{
				ErrorLog.Add("Грешка при зареждане на основания за начначаване");
			}
			this.nomenclaatureData.dtReasonAssignment.PrimaryKey = new DataColumn[] { this.nomenclaatureData.dtReasonAssignment.Columns["id"] };

			this.nomenclaatureData.dtAdminTable = daa.SelectWhere(TableNames.AdminInfo, "*", " WHERE id = 1");
			if (this.nomenclaatureData.dtAdminTable == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за администрацията");
			}

			this.nomenclaatureData.dtYear = daa.SelectWhere(TableNames.Year, "*", "");
			if (this.nomenclaatureData.dtYear == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за текуща година");
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'familystatus'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура семмен статус");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrFamilyStatus.Add(dr["level"].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'reasonfired'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура основание за совобождаване");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrReasonFired.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'sciencelevel'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура научна степен");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrScienceLevel.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'sciencetitle'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура научно звание");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrScienceTitle.Add(dr[0].ToString());
				}
			}

			this.nomenclaatureData.dtMilitaryRang = daa.SelectWhere(TableNames.JoinNomenklature, "*", "WHERE descriptor = 'militaryrang'");
			if (this.nomenclaatureData.dtMilitaryRang == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура военен ранг");
			}
			this.nomenclaatureData.dtMilitaryDegree = daa.SelectWhere(TableNames.JoinNomenklature, "*", "WHERE descriptor = 'militarydegree' order by englevel, level ");
			if (this.nomenclaatureData.dtMilitaryDegree == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура военен ранг");
			}
			
			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'language'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура чужди езици");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrLanguages.Add(dr[0].ToString());
				}
			}

			this.nomenclaatureData.dtEducation = daa.SelectWhere(TableNames.Education, "*", "ORDER BY id");

			if (this.nomenclaatureData.dtEducation == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура образование");
			}
			this.nomenclaatureData.dtEducation.PrimaryKey = new DataColumn[] { this.nomenclaatureData.dtEducation.Columns["id"] };

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'Contract'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура тип договор");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrContract.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.NKP, "Code, level", "ORDER BY code");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура НКПД");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrNKPCode.Add(dr[0].ToString());
					this.nomenclaatureData.arrNKPlevel.Add(dr[1].ToString());
				}
			}


			dt = daa.SelectWhere(TableNames.NKID, "Code, level", "");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура НКИД");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrNKIDCode.Add(dr[0].ToString());
					this.nomenclaatureData.arrNKIDlevel.Add(dr[1].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.Ekda, "Code, level", "");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура ЕКДА");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrNKDSCode.Add(dr[0].ToString());
					this.nomenclaatureData.arrNKDSlevel.Add(dr[1].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'law'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура правоотношение");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrLaw.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'rang'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура ранг");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrRang.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'Experience'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура опит");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrExperience.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'YearlyAddon'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура годишни надбавки");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrYearlyAddon.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'penaltyreason'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура основания за наказание");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrPenaltyReason.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'bonusreason'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура основания за наказание");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrBonusReason.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'typepenalty'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура тип наказание");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrTypePenalty.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'typebonus'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура тип наказание");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrTypeBonus.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'nkpclass'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура клас по НКПД");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrNKPClass.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'languageknowledge'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура ниво на владеене на език");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrLanguageKnowledge.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'militaryStatus'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура военен ранг");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrMilitaryStatus.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'NatoDegree'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура военен ранг НАТО");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrNatoDegree.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "englevel", "WHERE descriptor = 'NatoDegree'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура военен ранг НАТО");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrNatoDegreeEng.Add(dr[0].ToString());
				}
			}

			dt = daa.SelectWhere(TableNames.JoinNomenklature, "level", "WHERE descriptor = 'specialskills'");
			if (dt == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура военен ранг");
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					this.nomenclaatureData.arrSpecialSkills.Add(dr[0].ToString());
				}
			}

			this.nomenclaatureData.dtWorkTime = daa.SelectWhere(TableNames.WorkTime, "*", "ORDER BY id");
			if (this.nomenclaatureData.dtWorkTime == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура работно време");
			}
			this.nomenclaatureData.dtWorkTime.PrimaryKey = new DataColumn[] { this.nomenclaatureData.dtWorkTime.Columns["id"] };

			this.nomenclaatureData.dtTreeTable = daa.SelectWhere(TableNames.NewTree2, "*", "ORDER BY id");
			if (this.nomenclaatureData.dtTreeTable == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура структура на организацията");
			}
			this.nomenclaatureData.dtTreeTable.TableName = TableNames.NewTree2;
			this.nomenclaatureData.dtPositionTable = daa.SelectWhere(TableNames.GlobalPositions, "*", "ORDER BY id");
			this.nomenclaatureData.dtPositionTable.PrimaryKey = new DataColumn[] { this.nomenclaatureData.dtPositionTable.Columns["id"] };
			if (this.nomenclaatureData.dtPositionTable == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура длъжности");
			}
			this.nomenclaatureData.dtOptions = daa.SelectWhere(TableNames.Options, "*", "ORDER BY id");
			if (this.nomenclaatureData.dtOptions == null)
			{
				ErrorLog.Add("Грешка при зареждане на данните за номенклатура настройки");
			}

			if (ErrorLog.Count > 0)
			{
				string Errors = "";
				for (int i = 0; i < ErrorLog.Count; i++)
				{
					Errors += ErrorLog[i] + "\n";
				}
				MessageBox.Show(Errors, ErrorMessages.NoConnection);
				this.Close();
			}
		}

		private void mainForm_Load(object sender, System.EventArgs e)
		{
			IsRegistried = false;
			this.CheckConfigFile();

			string connStringUsers = "";
			
			if (this.dsOptions.Tables[0].Rows[0][3].ToString() != "")
			{					
				connStringUsers = "Database=" + this.database + ";server=" +
								this.dsOptions.Tables[0].Rows[0][1]
								+ "; User Id=" + this.dbUser + ";Password=" + this.password;					
			}

			this.tablePrefix = "HR_";

			DataLayer.TableNames.Prefix = this.tablePrefix;

			this.userAction = new DataAction(connStringUsers);
			this.dtUsers = this.userAction.SelectWhere(TableNames.Users, "*", "");
			if (this.dtUsers == null)
			{
				MessageBox.Show("Таблицата с потребителските имена и пароли е повредена ИЛИ сървъра не работи или е изключен. Проверете дали сте написали правилно паролата в конфигурационния файл.", "Грешка");
				//{
				//	//this.dsOptions.Clear();
				//	//this.dsOptions.Tables[0].Rows.Add(new object[] { System.Windows.Forms.Application.StartupPath + "\\", "localhost", "root", "tess", "HRDB", "ActivationKey", "DBTypes" });
				//	//formOptions opt = new formOptions(this, false);
				//	//opt.ShowDialog(this);
				//	//goto UserCheck;
				//	//this.dsOptions.WriteXml( System.Windows.Forms.Application.StartupPath +"Config.xml", XmlWriteMode.WriteSchema );
				//}
				Application.Exit();
				return;
			}
			formLogIn log = new formLogIn(this, false);
			DialogResult dr;
			dr = log.ShowDialog();
			if (DialogResult.Cancel == dr)
			{
				Application.Exit();
			}

			if (this.dsOptions.Tables[0].Rows[0][3].ToString() != "")
			{
				this.connString = String.Format("Data Source={0};Initial Catalog= {1};uid={2};Password={3};", this.dsOptions.Tables[0].Rows[0]["host"].ToString(), this.database, this.dsOptions.Tables[0].Rows[0]["user"].ToString(), this.dsOptions.Tables[0].Rows[0]["password"].ToString());

			}

			GetConnString(out this.EntityConectionString);
			//SplashScreen splash = new SplashScreen();
			this.LoadNomenklatureInLists();
#if NIKSAN
			this.User = "e";
#else
			this.User = log.textBoxUserName.Text;
#endif
			this.statusBarPanel1.Text = " Потребител : " + this.User;
			this.statusBarPanel4.Text = " Дата : " + System.DateTime.Now.ToShortDateString();
			try
			{
				System.Net.IPHostEntry ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
				this.statusBarPanel2.Text = " Computer name: " + System.Net.Dns.GetHostName().ToString();
				this.statusBarPanel3.Text = " IP address: " + ip.AddressList[0].ToString();
			}
			catch
			{
			}

			this.menuItemSickness.Visible = true;

			HolidayPlan.AutomaticMessages.CheckForEvents(this.EntityConectionString);
		}

		internal void ErrorInDataBase(string message)
		{
			MessageBox.Show("Проверетe си SQL server'a. Несъществуваща таблица. Повредена база. Вероятен проблем за грешката: " + message);
		}

		#region Menu Kartoteka
		private void menuKartoteka_KartotekaLS_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataSet dsQery = new DataSet();
				dsQery.ReadXml(System.Windows.Forms.Application.StartupPath + @"\XMLLabels\KartotekaQuery.xml", System.Data.XmlReadMode.Auto);
				int idx = 0;
				this.action = new DataLayer.DataAction(this.connString);
				if (nomenclaatureData.dtOptions.Rows.Count > 0)
					int.TryParse(nomenclaatureData.dtOptions.Rows[0]["personorder"].ToString(), out idx);

				this.dtKartoteka = action.SelectBase(false, nomenclaatureData.arrPersonOrder[idx].ToString(), dsQery.Tables["BasicQuery"]);
				if (dtKartoteka == null)
				{
					MessageBox.Show("Грешка при зареждане на картотеката", ErrorMessages.NoConnection);
					this.Close();
					return;
				}
				//dtKartoteka.PrimaryKey = new DataColumn[] { this.dtKartoteka.Columns["ID"] };

				this.formKartoteka = new KartotekaLichenSystaw(this, this.dtKartoteka, "Картотека на всички служители", false);
				this.formKartoteka.ShowDialog(this);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void menuKartoteka_KartotekaCanceled_Click(object sender, System.EventArgs e)
		{
			try
			{
				int idx = 0;
				DataSet dsQery = new DataSet();
				dsQery.ReadXml(System.Windows.Forms.Application.StartupPath + @"\XMLLabels\KartotekaQuery.xml", System.Data.XmlReadMode.Auto);
				this.action = new DataLayer.DataAction(this.connString);
				int.TryParse(nomenclaatureData.dtOptions.Rows[0]["personorder"].ToString(), out idx);
				dtKartoteka = action.SelectBase(true, nomenclaatureData.arrPersonOrder[idx].ToString(), dsQery.Tables["FiredQuery"]);
				if (dtKartoteka == null)
				{
					MessageBox.Show("Грешка при зареждане на картотеката", ErrorMessages.NoConnection);
					return;
				}
				formKartoteka = new KartotekaLichenSystaw(this, dtKartoteka, "Картотека на прекратени договори", true);
				formKartoteka.ShowDialog(this);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void menuKartoteka_AddNewEmployee_Click_2(object sender, System.EventArgs e)
		{
			this.Close();
		}

		internal void TotalStatistics(bool IsFiredd)
		{
			formStatisticTotal stat = new formStatisticTotal(this, true, IsFiredd); //Karotekata ot tuk izwikwa obshtite sprawki
			stat.ShowDialog();
		}

		private void menuItemMessages_Click(object sender, EventArgs e)
		{
			ReviewMessages win = new ReviewMessages(this.EntityConectionString, this.User);
			win.ShowDialog();
		}

		#endregion

		#region Menu Administration
		private void menuAdministartion_GlobalPositions_Click(object sender, System.EventArgs e)
		{
			GlobalPositions formOffice = new GlobalPositions(this);
			formOffice.ShowDialog();
		}

		private void menuAdministration_Register_Click(object sender, System.EventArgs e)
		{
			formRegister reg = new formRegister(this);
			reg.ShowDialog();
		}

		private void menuAdministartion_Structure_Click(object sender, System.EventArgs e)
		{
			FormStructureNew form = new FormStructureNew(this, TableNames.FirmPersonal3, this.nomenclaatureData.dtTreeTable);
			form.ShowDialog(this);
		}
		#endregion

		#region Menu Sprawki

		private void menuSpravki_Staff_Click(object sender, System.EventArgs e)
		{
			Ex = new ExcelExpo();
			Ex.ExportOSR(this);
			GC.Collect();
		}

		private void menu_Spravki_PSR_Click(object sender, System.EventArgs e)
		{
			Ex = new ExcelExpo();
			Ex.ExportPSR(this);
			GC.Collect();

		}
		private void menuSprawki_Free_Click(object sender, System.EventArgs e)
		{
			Ex.ExtractFreeEntity(this);
			GC.Collect();
		}

		private void menuSprawki_Holiday_Click(object sender, System.EventArgs e)
		{
			Ex.ExtractHoliday(this);
			GC.Collect();
		}
		private void menuItemStatisticsLeadersHolidays_Click(object sender, EventArgs e)
		{
			Ex.ExtractImportantHoliday(this);
			GC.Collect();
		}
		private void menuItemSprawkiZZBUT_Click(object sender, System.EventArgs e)
		{
			Ex.ExportZZBUT(this);
			GC.Collect();
		}
		private void menuItemSprawkiAttestations_Click(object sender, System.EventArgs e)
		{
			Ex.ExportAttestations(this);
			GC.Collect();
		}

		private void menuItemStatistics_Click(object sender, System.EventArgs e)
		{
			try
			{
				formStatisticTotal form = new formStatisticTotal(this, false, false);
				form.ShowDialog();
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}

		private void menuItemMilitaryRangs_Click(object sender, EventArgs e)
		{
			GetDate dateForm = new GetDate();
			if (dateForm.ShowDialog() == DialogResult.OK)
			{
				Ex.ExtractRangUpdate(this, dateForm.dateTimePicker1.Value);
				GC.Collect();
			}
		}

		private void menuItemSyscosetWeekAbsences_Click(object sender, EventArgs e)
		{
			GetDate dateForm = new GetDate();
			if (dateForm.ShowDialog() == DialogResult.OK)
			{
				Ex.ExportSyscoAbsences(this, dateForm.dateTimePicker1.Value);
				GC.Collect();
			}
		}

		private void menuItemOSRNSO_Click(object sender, EventArgs e)
		{
			Ex = new ExcelExpo();
			Ex.ExportOSRNSO(this);
			GC.Collect();
		}

		#endregion

		#region Menu Nomenclature Functions
		internal void menuNomenklaturi_Education_Click(object sender, System.EventArgs e)
		{
			CommonNomenclature form = new CommonNomenclature(TableNames.Education, "Образование", this.nomenclaatureData.dtEducation, this);
			form.ShowDialog();
		}

		internal void menuNomenklaturi_ForeignLanguages_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Чужди езици", this.nomenclaatureData.arrLanguages, this, "language");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_MilitaryRang_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature2 form = new JoinNomenklature2(TableNames.JoinNomenklature, "Военен ранг", this.nomenclaatureData.dtMilitaryRang, this, "militaryrang");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_ScienceTitle_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Научно звание", this.nomenclaatureData.arrScienceTitle, this, "ScienceTitle");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_ScienceDegree_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Научнa степен", this.nomenclaatureData.arrScienceLevel, this, "ScienceLevel");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_ReasonAssignment_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature2 form = new JoinNomenklature2(TableNames.ReasonAssignment, "Oснования за назначение", this.nomenclaatureData.dtReasonAssignment, this, "");
			form.ShowDialog();
		}

		internal void menuNomenklatures_ReasonFired_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Oснования за освобождаване", this.nomenclaatureData.arrReasonFired, this, "ReasonFired");
			form.ShowDialog();
		}

		private void menuNomenklaturi_NSONato_Click(object sender, EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Звания НАТО", this.nomenclaatureData.arrNatoDegree, this, "NatoDegree");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_ProfessionClassifier_Click(object sender, System.EventArgs e)
		{
			DataLayer.DataAction da = new DataAction(this.connString);
			DataTable dt = da.SelectWhere(TableNames.NKP, "*", "");
			dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
			CommonNomenclature form = new CommonNomenclature(TableNames.NKP, "Класификатор на професии", dt, this);
			form.ShowDialog();
		}

		private void menuItemNKPDClass_Click(object sender, EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Класове по НКПД", this.nomenclaatureData.arrNKPClass, this, "NKPClass");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_ClassifierID_Click(object sender, System.EventArgs e)
		{
			DataLayer.DataAction da = new DataAction(this.connString);
			DataTable dt = da.SelectWhere(TableNames.NKID, "*", "");
			dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
			CommonNomenclature form2 = new CommonNomenclature(TableNames.NKID, "Класификатор на икономически дейности", dt, this);
			form2.ShowDialog();
		}

		internal void menuNomenklaturi_FamilyStatus_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Семейно положение", this.nomenclaatureData.arrFamilyStatus, this, "FamilyStatus");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_WorkTime_Click(object sender, System.EventArgs e)
		{
			CommonNomenclature form = new CommonNomenclature(TableNames.WorkTime, "Работно време", this.nomenclaatureData.dtWorkTime, this);
			form.ShowDialog();
		}

		internal void menuNomenklaturi_Contract_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Договори", this.nomenclaatureData.arrContract, this, "Contract");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_KlasifikatorDS_Click(object sender, System.EventArgs e)
		{
			FormEKDAView form = new FormEKDAView(this);
			form.ShowDialog();
		}

		internal void menuNomenklaturi_Law_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Правоотношения", this.nomenclaatureData.arrLaw, this, "law");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_Rang_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Рангoве", this.nomenclaatureData.arrRang, this, "rang");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_Experience_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Професионален опит", this.nomenclaatureData.arrExperience, this, "Experience");
			form.ShowDialog();
		}

		internal void menuNomenklaturiYearlyAddon_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Годишни надбавки", this.nomenclaatureData.arrYearlyAddon, this, "YearlyAddon");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_reasonPenalty_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Основания за наказание", this.nomenclaatureData.arrPenaltyReason, this, "penaltyreason");
			form.ShowDialog();
		}

		internal void menuNomenklaturi_TypePenalty_Click(object sender, System.EventArgs e)
		{
			JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Основания за наказание", this.nomenclaatureData.arrYearlyAddon, this, "typepenalty");
			form.ShowDialog();
		}

		private void menuItem_Nomenklaturi_Educations_Click(object sender, System.EventArgs e)
		{
			FormEducationNomenclature form = new FormEducationNomenclature(this);
			form.ShowDialog();
		}
		#endregion

		#region NOI Tryouts

		//private void menuItemNOI_Click(object sender, System.EventArgs e)
		//{
		//    DataTable persons = new DataTable();
		//    DataTable assignments = new DataTable();
		//    DataTable firedtable = new DataTable();
		//    DataTable admin = new DataTable();
		//    DataAction dac = new DataAction(this.connString);

		//    persons = dac.SelectWhere(TableNames.Person, "*", "ORDER BY id");
		//    assignments = dac.SelectWhere(TableNames.PersonAssignment, "*", "ORDER BY id");
		//    firedtable = dac.SelectWhere(TableNames.Fired, "*, parent as par", "");
		//    admin = dac.SelectWhere(TableNames.AdminInfo, "*", "ORDER BY id");

		//    if (persons == null || assignments == null || firedtable == null || admin == null)
		//    {
		//        MessageBox.Show("Грешка при зареждане на номенклатурети за експорт", ErrorMessages.NoConnection);
		//    }

		//    persons.PrimaryKey = new DataColumn[] { persons.Columns["id"] };

		//    DataView vueAssignments,vueFired;
		//    DataViewRowState dvrs = DataViewRowState.CurrentRows;

		//    string TodaysPath;
		//    TodaysPath = Application.StartupPath + @"\NAP\" + System.DateTime.Now.Year.ToString() + "_" + System.DateTime.Now.Month.ToString() + "_" + System.DateTime.Now.Day.ToString();
		//    if(System.IO.Directory.Exists(TodaysPath) == false)
		//    {
		//        try
		//        {
		//            System.IO.Directory.CreateDirectory(TodaysPath);
		//        }
		//        catch
		//        {
		//            MessageBox.Show("Не може да се създаде папка за експорт на данни до НАП.", "Грешка при създаване на папка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        }
		//        //					try
		//        //					{
		//        //						// За сега няма да се прави продружително писмо
		//        //						//System.IO.File.Copy(Application.StartupPath + @"\Templates\cover_letter_u62.rtf",TodaysPath + @"\cover_letter_u62.rtf", true);
		//        //					}
		//        //					catch
		//        //					{
		//        //						//MessageBox.Show("Липсва шаблонен файл за придружително писно до НАП.");
		//        //						return;
		//        //					}
		//    }

		//    StreamWriter sw = new StreamWriter(TodaysPath + @"\utd2003.txt", true, Encoding.Default);

		//    vueAssignments = new DataView(assignments, "exported = 0", "id", dvrs); //looking up for the employees that are currently assigned  //alse cancelling isactive = 1
		//    if(vueAssignments.Count > 0)
		//    {
		//        for(int i = 0; i < vueAssignments.Count; i ++)
		//        {
		//            string Line;
		//            StringBuilder sb = new StringBuilder(500);
		//            bool fired = false, isactive = false, isAdditionalAssignment = false;
		//            DateTime assigndate, fireddate, enddate;
		//            DataRow rowPer = persons.Rows.Find(vueAssignments[i]["parent"]);
		//            if(rowPer["fired"].ToString() == "1")
		//            {
		//                fired = true;
		//            }
		//            if(vueAssignments[i]["isadditionalassignment"].ToString() == "1")
		//            {
		//                isAdditionalAssignment = true;
		//            }
		//            if(vueAssignments[i]["isactive"].ToString() == "1")
		//            {
		//                isactive = true;
		//            }
		//            else if(fired == false)
		//            {
		//                continue;
		//            }
		//            //Код корекция - Коректни данни позиция 1
		//            sb.Append("0,");

		//            if(fired) //&& isactive //позиция 2
		//            {
		//                sb.Append("3,"); //Тип на документа - 3 – прекратяване след 01.01.2003 г.
		//            }
		//            else if(isAdditionalAssignment && isactive)
		//            {
		//                sb.Append("2,"); //Тип на документа - 2 –допълнително споразумение след 01.01.2003 г.
		//            }
		//            else if(isactive)
		//            {
		//                sb.Append("1,"); //Тип на документа - 1 – договор, сключен след 01.01.2003 г.
		//            }

		//            sb.Append("\"" + admin.Rows[0]["bulstat"].ToString() + "\","); //БУЛСТАТ номер позиция 3
		//            sb.Append( "\"" + rowPer["egn"].ToString() + "\","); //ЕГН позиция 4
		//            sb.Append("0,"); //Флаг за ЕГН/ЛНЧ - 0 за ЕГН позиция 5
		//            string[] names;
		//            string first, middle= "", last = "", fullname;
		//            fullname = rowPer["name"].ToString();
		//            fullname = fullname.Trim(new char[] {' '});
		//            string oldname;
		//            do
		//            {
		//                oldname = fullname;
		//                fullname = fullname.Replace("  ", " ");
		//            }while(oldname != fullname);
		//            names = fullname.Split(new char[]{' '});
		//            first = names[0];
		//            if(first.Length > 25)
		//            {
		//                MessageBox.Show("Името " + first + " е прекалено дълго (над 25 символа) и ще бъде отрязано.", "Моля коригирайте изходният файл за да не се допусне грешка.",
		//                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//                first = first.Substring(0, 25);						
		//            }

		//            if(names.Length > 2)
		//            {
		//                middle = names[1];
		//                if(middle.Length > 25)
		//                {
		//                    MessageBox.Show("Името " + middle + " е прекалено дълго (над 25 символа) и ще бъде отрязано.", "Моля коригирайте изходният файл за да не се допусне грешка.",
		//                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//                    middle = middle.Substring(0, 25);
		//                }
		//            }
		//            else if(names.Length == 2)
		//            {
		//                middle = "";
		//                last = names[1];
		//            }
		//            for(int nam = 2; nam < names.Length; nam++)
		//            {
		//                last += names[nam];
		//            }

		//            if(last.Length > 25)
		//            {
		//                MessageBox.Show("Името " + last + " е прекалено дълго (над 25 символа) и ще бъде отрязано.", "Моля коригирайте изходният файл за да не се допусне грешка.",
		//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//                last = last.Substring(0, 25);
		//            }

		//            sb.Append("\"" + first.ToUpper() + "\",\"" + middle.ToUpper() + "\",\"" + last.ToUpper() + "\","); //Трите имена рзделени (надявам се правилно) //позиция 6, 7 и 8
		//            if(vueAssignments[i]["pcontractreasoncode"].ToString() == "")
		//            {
		//                MessageBox.Show("Основанието по което е назначен служителя" + first + " " + last + " е нестандартно.", 
		//                        "Данните за слкужителя няма да бъдат експортирани.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//                continue;
		//            }
		//            sb.Append(vueAssignments[i]["pcontractreasoncode"].ToString() + ","); // Основание на договора (по номенклатурата от 1 до 15) позиция 9
		//            string contnum = vueAssignments[i]["contractnumber"].ToString();
		//            if(contnum.Length > 15)
		//            {
		//                MessageBox.Show("Номера на договора " + contnum + " е прекален дълъг (над 15 символа) и ще бъде отрязан.", "Моля коригирайте изходният файл за да не се допусне грешка.",
		//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//                contnum = contnum.Substring(0, 15);
		//            }
		//            sb.Append("\"" + contnum.ToUpper() + "\","); //Номер на договора //позиция 10
		//            assigndate = (DateTime)vueAssignments[i]["assignedat"];
		//            string aday, amonth;
		//            aday = assigndate.Day.ToString();
		//            if(aday.Length < 2)
		//            {
		//                aday = aday.PadLeft(2, '0');
		//            }
		//            sb.Append(aday + ","); //Ден //позиция 11
		//            amonth = assigndate.Month.ToString();
		//            if(amonth.Length < 2)
		//            {
		//                amonth = amonth.PadLeft(2, '0');
		//            }
		//            sb.Append(amonth + ","); //Месец //позиция 12
		//            sb.Append(assigndate.Year.ToString() + ",");  // и Година на назначението //позиция 13
		//            sb.Append(vueAssignments[i]["basesalary"] + ","); // Основно трудово възнаграждение //позиция 14
		//            if(vueAssignments[i]["Contract"].ToString().StartsWith("Срочен"))
		//            {
		//                string eday, emonth;
		//                enddate = (DateTime) vueAssignments[i]["contractexpiry"];

		//                eday = enddate.Month.ToString();
		//                if(eday.Length < 2)
		//                {
		//                    eday = eday.PadLeft(2, '0');
		//                }
		//                sb.Append(eday + ","); //Ден //позиция 15
		//                emonth = enddate.Month.ToString();
		//                if(emonth.Length < 2)
		//                {
		//                    emonth = emonth.PadLeft(2, '0');
		//                }
		//                sb.Append(emonth + ","); // месец //Позиция 16
		//                sb.Append(enddate.Year.ToString() + ","); // и година на изтучане на срока на договора //Позиция 17
		//            }
		//            else
		//            {
		//                sb.Append(",,,"); //записваме празни полета за безсрочен договор //Позиция 15, 16, 17
		//            }
		//            sb.Append("\"" + vueAssignments[i]["position"].ToString().ToUpper() + "\","); //Длъжност //Позиция 18
		//            sb.Append("\"" + vueAssignments[i]["nkpcode"].ToString() + "\","); //Код по НКПД //Позиция 19
		//            string nkid = admin.Rows[0]["NKIDLevel"].ToString();
		//            if(nkid.Length > 59)
		//            {
		//                nkid = nkid.Substring(0,59);
		//            }
		//            sb.Append("\"" + nkid.ToUpper() + "\","); //Ниво по НКИД (орязано до 60 символа) //Позиция 20
		//            nkid = admin.Rows[0]["NKIDCode"].ToString().Replace(".","");
		//            sb.Append("\"" + nkid + "\","); //Код по НКИД //Позиция 21

		//            if(fired)
		//            {
		//                vueFired = new DataView(firedtable, "par = " + vueAssignments[i]["parent"].ToString(), "id", dvrs);
		//                fireddate = (DateTime) vueFired[i]["fromdate"];
		//                string fday, fmonth;
		//                fday = fireddate.Day.ToString();
		//                if(fday.Length < 2)
		//                {
		//                    fday = fday.PadLeft(2, '0');
		//                }
		//                sb.Append(fday + ","); // ден  //Позиция 22

		//                fmonth = fireddate.Month.ToString();
		//                if(fmonth.Length < 2)
		//                {
		//                    fmonth = fmonth.PadLeft(2, '0');
		//                }
		//                sb.Append(fday + ","); // ден  //Позиция 22
		//                sb.Append(fmonth + ","); // месец //Позицияп 23
		//                sb.Append(fireddate.Year.ToString() + ","); // и година на уволнение Позиция 24
		//            }
		//            else
		//            {
		//                sb.Append(",,,"); //записваме празни ако не е уволнение  //Позиция 22, 23, 24
		//            }

		//            sb.Append("\"121714049\""); //Служебни - Флаг за източник - БУЛСТАТ на разработчика  //Позиция 25
		//            Line = sb.ToString();
		//            sw.WriteLine(Line);

		//            Dictionary<string, string> iDict = new Dictionary<string, string>();
		//            iDict.Add("exported", "1");
		//            if (this.action.UniversalUpdate(TableNames.PersonAssignment, vueAssignments[i]["id"].ToString(), iDict) == false) //updateExported
		//            {
		//                MessageBox.Show("Внимание! Данните за експортирането не са записани в базата данни. Експортирането трябва да се извърши отново!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                sw.Close();
		//                return;
		//            }
		//        }
		//        sw.Write(0x1a);
		//        sw.Close();
		//        MessageBox.Show("Експортирани са данни за " + vueAssignments.Count + " договора");
		//    }
		//    else
		//    {
		//        MessageBox.Show("Няма данни за експортиране.");
		//    }
		//}

		#endregion

		#region Menu System
		private void menuSystem_Users_Click(object sender, System.EventArgs e)
		{
			formUsers users = new formUsers(this);
			users.ShowDialog(this);
		}

		private void menuSystem_LogIn_Click(object sender, System.EventArgs e)
		{
			formLogIn log = new formLogIn(this, true);
			log.ShowDialog();
			this.statusBarPanel1.Text = "Потребител : " + this.User;
		}

		private void menuSystem_ProgramOptions_Click(object sender, System.EventArgs e)
		{
			formOptions form = new formOptions(this, true);
			form.ShowDialog();
		}

		private void menuSystem_FinishYear_Click(object sender, System.EventArgs e)
		{
			formFinishYear year = new formFinishYear(this);
			year.ShowDialog();
		}

		#endregion

		#region Toolbar Buttons
		private void buttonKartoteka_Click(object sender, System.EventArgs e)
		{
			menuKartoteka_KartotekaLS_Click(sender, e);
		}

		private void buttonStructura_Click(object sender, System.EventArgs e)
		{
			menuAdministartion_Structure_Click(sender, e);
		}

		private void buttonDlujnosti_Click(object sender, System.EventArgs e)
		{
			menuAdministartion_GlobalPositions_Click(sender, e);
		}

		private void buttonShtatnoRazpisanie_Click(object sender, System.EventArgs e)
		{
			menuSpravki_Staff_Click(sender, e);
		}

		private void buttonObshtiSprawki_Click(object sender, System.EventArgs e)
		{
			try
			{
				//menuSpravki_Total_Click( sender, e ); Old total statistics
				menuAdministration_Register_Click(sender, e);
				//menuItem Statistics_Click( sender, e );
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonUsers_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.menuSystem_ProgramOptions_Click(sender, e);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			try
			{
				menuKartoteka_AddNewEmployee_Click_2(sender, e);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		private void mainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			/*try
			{
				this.dsOptions.WriteXml( System.Windows.Forms.Application.StartupPath +"\\Config.xml", XmlWriteMode.WriteSchema );
                
			}
			catch(Exception exc )
			{
				MessageBox.Show( exc.Message );
				System.Diagnostics.Debug.Write( "\\n" + exc.Message );
			}
			*/
		}

		private void menuHelp_Author_Click(object sender, System.EventArgs e)
		{
			try
			{
				Author form = new Author();
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void menuItemBackup_Click(object sender, EventArgs e)
		{
			try
			{
				HRBackup win = new HRBackup(this.dbHost, this.database, this.connString, this.dbUser, this.password);
				win.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void menuItemRestore_Click(object sender, EventArgs e)
		{
			try
			{
				HRRestore win = new HRRestore(this.dbHost, this.database, this.connString, this.dbUser, this.password);
				win.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void menuItemSickness_Click(object sender, EventArgs e)
		{
			MainWindow win = new MainWindow(this.EntityConectionString);
			win.ShowDialog();
		}

		enum XMLPosition
		{
			Server = 1,
			UserID = 2,
			Password = 3,
			Database = 4,
			DBType = 6,
		}

		private void menuItemKartotekaHolidayPlan_Click(object sender, EventArgs e)
		{
			HolidayPlanNewWindow win = new HolidayPlanNewWindow(this.EntityConectionString);
			win.ShowDialog();
		}

		private void menuItemPaidHolidays_Click(object sender, EventArgs e)
		{
			CustomHolidays win = new CustomHolidays(this.EntityConectionString);
			win.ShowDialog();
		}

		private void menuItemSystemWorkDays_Click(object sender, EventArgs e)
		{
			YearWorkdays win = new YearWorkdays(this.EntityConectionString);
			win.ShowDialog();
		}


		/// <summary>
		/// constructs a connection string for entity for the corresponding database
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns>false if unsuccessful</returns>
		public static bool GetConnString(out string connectionString)
		{
			try
			{
				connectionString = "";
				DataSet ds = new DataSet();
				string server, userID, pass, database;

				//FileStream fsReadXML = new FileStream("config.xml", FileMode.Open);

				ds.ReadXml("config.xml", XmlReadMode.InferSchema);

				server = ds.Tables[0].Rows[0].ItemArray[(int)XMLPosition.Server].ToString();
				userID = ds.Tables[0].Rows[0].ItemArray[(int)XMLPosition.UserID].ToString();
				pass = ds.Tables[0].Rows[0].ItemArray[(int)XMLPosition.Password].ToString();
				database = ds.Tables[0].Rows[0].ItemArray[(int)XMLPosition.Database].ToString();

				//CustomEntityConnectionStingBuilder sb = new CustomEntityConnectionStingBuilder(server, userID, pass, database, );

				string model = "HREntity";
				connectionString = string.Format(@"metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=System.Data.SqlClient;provider connection string='server={1};user id={3};password={4};database={2};persist security info=True'", model, server, database, userID, pass);

				ds.Dispose();
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
				connectionString = "";
				return false;
			}
			return true;
		}

		private void menuItemOmegaExport_Click(object sender, EventArgs e)
		{
			Printing.OmegaExport win = new Printing.OmegaExport(this, false);
			win.Show();
		}

		private void menuItemOmegaExportAdditional_Click(object sender, EventArgs e)
		{
			Printing.OmegaExport win = new Printing.OmegaExport(this, true);
			win.Show();
		}

		private void menuItemCheckHolidays_Click(object sender, EventArgs e)
		{
			string connectionString;
			if (GetConnString(out connectionString) == false)
				return;
			
			//if (this.dsOptions.Tables[0].Columns.Contains("specialcustomer"))
			//{
			//	var customer = this.dsOptions.Tables[0].Rows[0]["specialcustomer"].ToString();
			//	switch(customer.ToLower())
			//	{
			//		case "shumen":
			//			CheckHolidaysShumen win = new CheckHolidaysShumen(connectionString);
			//			win.ShowDialog();
			//			break;
			//		case "nso":
			//			break;
			//	}
			//}
			//else
			//{
				CheckHolidays wib = new CheckHolidays(connectionString);
				wib.ShowDialog();
			//}
		}

		private void menuItemNKPDCheck_Click(object sender, EventArgs e)
		{
			string connectionString;
			if (GetConnString(out connectionString) == false)
				return;
			NKPDCheck wib = new NKPDCheck(connectionString);
			wib.ShowDialog();
		}

		private void menuSettingsService_Click(object sender, EventArgs e)
		{
			string connectionString;
			if (GetConnString(out connectionString) == false)
				return;
			ServiseFunctions wib = new ServiseFunctions(connectionString);
			wib.ShowDialog();
		}

		private void menuItemStructureEdit_Click(object sender, EventArgs e)
		{
			string connectionString;
			if (GetConnString(out connectionString) == false)
				return;
			OrganisationStructure win = new OrganisationStructure(connectionString);
			win.ShowDialog();
		}

		private void menuItemNSOLastPosition_Click(object sender, EventArgs e)
		{
			string connectionString;
			if (GetConnString(out connectionString) == false)
				return;
			SicknessFrame.LastPosition win = new LastPosition(connectionString);
			win.ShowDialog();
		}

        private void menuItemNSOOfficerPromotions_Click(object sender, EventArgs e)
        {
            string connectionString;
            if (GetConnString(out connectionString) == false)
                return;
            SicknessFrame.OfficerPromotion win = new OfficerPromotion(connectionString);
            win.ShowDialog();
        }		
	}

	class EntryPoint
	{
		[STAThread]
		public static void Main()
		{
			mainForm mainform = new mainForm();
			Application.Run(mainform);
		}
	}

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
			Microsoft.Win32.RegistryKey rkCompany;
			//rkCompany = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
			rkCompany = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Човешки Ресурси");
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
			try
			{
				Microsoft.Win32.RegistryKey rkSoftware;
				//RegistryKey rkCompany;
				Microsoft.Win32.RegistryKey rkApplication;

				rkSoftware = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(SOFTWARE_KEY, true);
				//rkCompany = rkSoftware.CreateSubKey(COMPANY_NAME);
				//if( rkCompany != null )
				//{
				rkApplication = rkSoftware.CreateSubKey(APPLICATION_NAME);
				if (rkApplication != null)
				{
					rkApplication.SetValue(key, stringValue);
				}
			}
			catch
			{
			}
			//}
		}
	}
}