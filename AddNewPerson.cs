using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace LichenSystaw2004
{
	/// <summary>
	/// Summary description for AddNewPerson.
	/// </summary>
	public class AddNewPersonForm : System.Windows.Forms.Form
	{
		#region Items
		private System.Windows.Forms.Button buttonОК;
		private System.Windows.Forms.Button buttonCancel;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label labelSex;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.Label labelPublishedByy;
		private BugBox.NumBox numBoxPcCard;
		private System.Windows.Forms.Label labelPublishedBy;
		private System.Windows.Forms.TextBox textBoxPublishedFrom;
		private System.Windows.Forms.Label labelJKkwartal;
		private BugBox.NumBox numBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelNumBlock;
		private System.Windows.Forms.TextBox textBoxNumBlock;
		private System.Windows.Forms.Label labelStreet;
		private System.Windows.Forms.TextBox textBoxStreet;
		private System.Windows.Forms.Label labelKwartal;
		private System.Windows.Forms.TextBox textBoxKwartal;
		private BugBox.BugBox bugBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxNames;
		private System.Windows.Forms.Label labelNames;
		private System.Windows.Forms.ComboBox comboBoxCountry;
		private System.Windows.Forms.Label labelCountry;
		private System.Windows.Forms.ComboBox comboBoxLive;
		private System.Windows.Forms.Label labelLive;
		private ComboBoxIntelisense.InteliCombo comboBoxNaselenoMqsto;
		private System.Windows.Forms.Label labelRegion;
		private System.Windows.Forms.ComboBox comboBoxRegion;
		private System.Windows.Forms.Label labelNaselenoMqsto;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ComboBox comboBoxFamilyStatus;
		private System.Windows.Forms.ComboBox comboBoxEducation;
		private System.Windows.Forms.TextBox textBoxDiplom;
		private System.Windows.Forms.Label labelFamilyStatus;
		private System.Windows.Forms.Label labelEducation;
		private System.Windows.Forms.Label labelDiplom;
		private System.Windows.Forms.ComboBox comboBoxProfesion;
		private System.Windows.Forms.Label labelProfesion;
		private System.Windows.Forms.CheckedListBox checkedListBoxLanguage;
		private System.Windows.Forms.Label labelLanguage;
		private System.Windows.Forms.Label labelScience;
		private System.Windows.Forms.ComboBox comboBoxScience;
		private System.Windows.Forms.Label labelScienceLevel;
		private System.Windows.Forms.Label labelMilitaryRang;
		private System.Windows.Forms.ComboBox comboBoxMilitaryRang;
		private System.Windows.Forms.ComboBox comboBoxScienceLevel;
		private System.Windows.Forms.Label labelMilitaryStatus;
		private System.Windows.Forms.Label labelEmployeStatus;
		private System.Windows.Forms.Label labelCategory;
		private System.Windows.Forms.ComboBox comboBoxMilitaryStatus;
		private System.Windows.Forms.ComboBox comboBoxEmployeStatus;
		private System.Windows.Forms.ComboBox comboBoxCategory;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label labelHiredAt;
		private System.Windows.Forms.Label labelWorkExperiance;
		private System.Windows.Forms.TextBox textBoxWorkExperience;
		private System.Windows.Forms.ToolTip toolTip1;
		private mainForm mainform;
		private bool IsPressedOKButton = false;
		#endregion

		public AddNewPersonForm( mainForm form )
		{
			this.mainform = form;
			InitializeComponent();
			this.comboBoxLive.DropDownStyle = ComboBoxStyle.DropDownList; 
			this.buttonОК.Click += new EventHandler(buttonОК_Click);
			this.buttonCancel.Click += new EventHandler(buttonCancel_Click);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.buttonОК = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelSex = new System.Windows.Forms.Label();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.labelPublishedByy = new System.Windows.Forms.Label();
			this.numBoxPcCard = new BugBox.NumBox();
			this.labelPublishedBy = new System.Windows.Forms.Label();
			this.textBoxPublishedFrom = new System.Windows.Forms.TextBox();
			this.labelJKkwartal = new System.Windows.Forms.Label();
			this.numBox1 = new BugBox.NumBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelNumBlock = new System.Windows.Forms.Label();
			this.textBoxNumBlock = new System.Windows.Forms.TextBox();
			this.labelStreet = new System.Windows.Forms.Label();
			this.textBoxStreet = new System.Windows.Forms.TextBox();
			this.labelKwartal = new System.Windows.Forms.Label();
			this.textBoxKwartal = new System.Windows.Forms.TextBox();
			this.bugBox1 = new BugBox.BugBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxNames = new System.Windows.Forms.TextBox();
			this.labelNames = new System.Windows.Forms.Label();
			this.comboBoxCountry = new System.Windows.Forms.ComboBox();
			this.labelCountry = new System.Windows.Forms.Label();
			this.comboBoxLive = new System.Windows.Forms.ComboBox();
			this.labelLive = new System.Windows.Forms.Label();
			this.comboBoxNaselenoMqsto = new ComboBoxIntelisense.InteliCombo();
			this.labelRegion = new System.Windows.Forms.Label();
			this.comboBoxRegion = new System.Windows.Forms.ComboBox();
			this.labelNaselenoMqsto = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.comboBoxFamilyStatus = new System.Windows.Forms.ComboBox();
			this.comboBoxEducation = new System.Windows.Forms.ComboBox();
			this.textBoxDiplom = new System.Windows.Forms.TextBox();
			this.textBoxWorkExperience = new System.Windows.Forms.TextBox();
			this.labelFamilyStatus = new System.Windows.Forms.Label();
			this.labelEducation = new System.Windows.Forms.Label();
			this.labelDiplom = new System.Windows.Forms.Label();
			this.comboBoxProfesion = new System.Windows.Forms.ComboBox();
			this.labelProfesion = new System.Windows.Forms.Label();
			this.checkedListBoxLanguage = new System.Windows.Forms.CheckedListBox();
			this.labelLanguage = new System.Windows.Forms.Label();
			this.labelScience = new System.Windows.Forms.Label();
			this.comboBoxScience = new System.Windows.Forms.ComboBox();
			this.labelScienceLevel = new System.Windows.Forms.Label();
			this.labelMilitaryRang = new System.Windows.Forms.Label();
			this.comboBoxMilitaryRang = new System.Windows.Forms.ComboBox();
			this.comboBoxScienceLevel = new System.Windows.Forms.ComboBox();
			this.labelMilitaryStatus = new System.Windows.Forms.Label();
			this.labelEmployeStatus = new System.Windows.Forms.Label();
			this.labelCategory = new System.Windows.Forms.Label();
			this.comboBoxMilitaryStatus = new System.Windows.Forms.ComboBox();
			this.comboBoxEmployeStatus = new System.Windows.Forms.ComboBox();
			this.comboBoxCategory = new System.Windows.Forms.ComboBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.labelHiredAt = new System.Windows.Forms.Label();
			this.labelWorkExperiance = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabPage1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonОК
			// 
			this.buttonОК.Location = new System.Drawing.Point(192, 304);
			this.buttonОК.Name = "buttonОК";
			this.buttonОК.TabIndex = 6;
			this.buttonОК.Text = "ОК";
			this.buttonОК.Click += new System.EventHandler(this.buttonОК_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(280, 304);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "Откажи";
			// 
			// labelSex
			// 
			this.labelSex.Location = new System.Drawing.Point(408, 184);
			this.labelSex.Name = "labelSex";
			this.labelSex.Size = new System.Drawing.Size(100, 24);
			this.labelSex.TabIndex = 16;
			this.labelSex.Text = "Пол:";
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.dateTimePicker2);
			this.tabPage1.Controls.Add(this.labelPublishedByy);
			this.tabPage1.Controls.Add(this.numBoxPcCard);
			this.tabPage1.Controls.Add(this.labelPublishedBy);
			this.tabPage1.Controls.Add(this.textBoxPublishedFrom);
			this.tabPage1.Controls.Add(this.labelJKkwartal);
			this.tabPage1.Controls.Add(this.numBox1);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.labelNumBlock);
			this.tabPage1.Controls.Add(this.textBoxNumBlock);
			this.tabPage1.Controls.Add(this.labelStreet);
			this.tabPage1.Controls.Add(this.textBoxStreet);
			this.tabPage1.Controls.Add(this.labelKwartal);
			this.tabPage1.Controls.Add(this.textBoxKwartal);
			this.tabPage1.Controls.Add(this.bugBox1);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.textBoxNames);
			this.tabPage1.Controls.Add(this.labelNames);
			this.tabPage1.Controls.Add(this.comboBoxCountry);
			this.tabPage1.Controls.Add(this.labelCountry);
			this.tabPage1.Controls.Add(this.comboBoxLive);
			this.tabPage1.Controls.Add(this.labelLive);
			this.tabPage1.Controls.Add(this.comboBoxNaselenoMqsto);
			this.tabPage1.Controls.Add(this.labelRegion);
			this.tabPage1.Controls.Add(this.comboBoxRegion);
			this.tabPage1.Controls.Add(this.labelNaselenoMqsto);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(872, 262);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Задължителни";
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(96, 184);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(176, 20);
			this.dateTimePicker2.TabIndex = 62;
			this.dateTimePicker2.Value = new System.DateTime(2004, 10, 30, 8, 49, 35, 468);
			// 
			// labelPublishedByy
			// 
			this.labelPublishedByy.Location = new System.Drawing.Point(96, 168);
			this.labelPublishedByy.Name = "labelPublishedByy";
			this.labelPublishedByy.Size = new System.Drawing.Size(112, 16);
			this.labelPublishedByy.TabIndex = 61;
			this.labelPublishedByy.Text = "Издаден на";
			// 
			// numBoxPcCard
			// 
			this.numBoxPcCard.Location = new System.Drawing.Point(8, 184);
			this.numBoxPcCard.Name = "numBoxPcCard";
			this.numBoxPcCard.Size = new System.Drawing.Size(88, 20);
			this.numBoxPcCard.TabIndex = 60;
			this.numBoxPcCard.Text = "";
			// 
			// labelPublishedBy
			// 
			this.labelPublishedBy.Location = new System.Drawing.Point(272, 168);
			this.labelPublishedBy.Name = "labelPublishedBy";
			this.labelPublishedBy.Size = new System.Drawing.Size(128, 16);
			this.labelPublishedBy.TabIndex = 59;
			this.labelPublishedBy.Text = "Издаден от";
			// 
			// textBoxPublishedFrom
			// 
			this.textBoxPublishedFrom.Location = new System.Drawing.Point(272, 184);
			this.textBoxPublishedFrom.Name = "textBoxPublishedFrom";
			this.textBoxPublishedFrom.Size = new System.Drawing.Size(128, 20);
			this.textBoxPublishedFrom.TabIndex = 58;
			this.textBoxPublishedFrom.Text = "";
			// 
			// labelJKkwartal
			// 
			this.labelJKkwartal.Location = new System.Drawing.Point(8, 168);
			this.labelJKkwartal.Name = "labelJKkwartal";
			this.labelJKkwartal.Size = new System.Drawing.Size(72, 16);
			this.labelJKkwartal.TabIndex = 57;
			this.labelJKkwartal.Text = "Л.К. Номер";
			// 
			// numBox1
			// 
			this.numBox1.Location = new System.Drawing.Point(320, 144);
			this.numBox1.Name = "numBox1";
			this.numBox1.Size = new System.Drawing.Size(88, 20);
			this.numBox1.TabIndex = 55;
			this.numBox1.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(320, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 54;
			this.label3.Text = "Телефон";
			// 
			// labelNumBlock
			// 
			this.labelNumBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelNumBlock.Location = new System.Drawing.Point(216, 128);
			this.labelNumBlock.Name = "labelNumBlock";
			this.labelNumBlock.Size = new System.Drawing.Size(104, 16);
			this.labelNumBlock.TabIndex = 52;
			this.labelNumBlock.Text = "N:, Бл., вх., ет., ап.";
			// 
			// textBoxNumBlock
			// 
			this.textBoxNumBlock.Location = new System.Drawing.Point(216, 144);
			this.textBoxNumBlock.Name = "textBoxNumBlock";
			this.textBoxNumBlock.Size = new System.Drawing.Size(104, 20);
			this.textBoxNumBlock.TabIndex = 51;
			this.textBoxNumBlock.Text = "";
			// 
			// labelStreet
			// 
			this.labelStreet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelStreet.Location = new System.Drawing.Point(112, 128);
			this.labelStreet.Name = "labelStreet";
			this.labelStreet.Size = new System.Drawing.Size(96, 16);
			this.labelStreet.TabIndex = 50;
			this.labelStreet.Text = "Улица/Булевард";
			// 
			// textBoxStreet
			// 
			this.textBoxStreet.Location = new System.Drawing.Point(112, 144);
			this.textBoxStreet.Name = "textBoxStreet";
			this.textBoxStreet.Size = new System.Drawing.Size(104, 20);
			this.textBoxStreet.TabIndex = 49;
			this.textBoxStreet.Text = "";
			// 
			// labelKwartal
			// 
			this.labelKwartal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelKwartal.Location = new System.Drawing.Point(8, 128);
			this.labelKwartal.Name = "labelKwartal";
			this.labelKwartal.Size = new System.Drawing.Size(104, 16);
			this.labelKwartal.TabIndex = 48;
			this.labelKwartal.Text = "Ж.К./Квартал";
			// 
			// textBoxKwartal
			// 
			this.textBoxKwartal.Location = new System.Drawing.Point(8, 144);
			this.textBoxKwartal.Name = "textBoxKwartal";
			this.textBoxKwartal.Size = new System.Drawing.Size(104, 20);
			this.textBoxKwartal.TabIndex = 47;
			this.textBoxKwartal.Text = "";
			// 
			// bugBox1
			// 
			this.bugBox1.Location = new System.Drawing.Point(8, 24);
			this.bugBox1.Name = "bugBox1";
			this.bugBox1.OnlyInteger = true;
			this.bugBox1.OnlyPositive = true;
			this.bugBox1.Size = new System.Drawing.Size(96, 20);
			this.bugBox1.TabIndex = 46;
			this.bugBox1.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "ЕГН";
			// 
			// textBoxNames
			// 
			this.textBoxNames.Location = new System.Drawing.Point(112, 24);
			this.textBoxNames.Name = "textBoxNames";
			this.textBoxNames.Size = new System.Drawing.Size(216, 20);
			this.textBoxNames.TabIndex = 2;
			this.textBoxNames.Text = "";
			// 
			// labelNames
			// 
			this.labelNames.Location = new System.Drawing.Point(120, 8);
			this.labelNames.Name = "labelNames";
			this.labelNames.Size = new System.Drawing.Size(136, 23);
			this.labelNames.TabIndex = 3;
			this.labelNames.Text = "Трите имена на лицето";
			// 
			// comboBoxCountry
			// 
			this.comboBoxCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCountry.DropDownWidth = 504;
			this.comboBoxCountry.ItemHeight = 13;
			this.comboBoxCountry.Location = new System.Drawing.Point(152, 64);
			this.comboBoxCountry.Name = "comboBoxCountry";
			this.comboBoxCountry.Size = new System.Drawing.Size(176, 21);
			this.comboBoxCountry.TabIndex = 8;
			// 
			// labelCountry
			// 
			this.labelCountry.Location = new System.Drawing.Point(152, 48);
			this.labelCountry.Name = "labelCountry";
			this.labelCountry.Size = new System.Drawing.Size(88, 16);
			this.labelCountry.TabIndex = 9;
			this.labelCountry.Text = "Националност";
			// 
			// comboBoxLive
			// 
			this.comboBoxLive.DropDownWidth = 496;
			this.comboBoxLive.ItemHeight = 13;
			this.comboBoxLive.Location = new System.Drawing.Point(8, 64);
			this.comboBoxLive.Name = "comboBoxLive";
			this.comboBoxLive.Size = new System.Drawing.Size(136, 21);
			this.comboBoxLive.TabIndex = 4;
			// 
			// labelLive
			// 
			this.labelLive.Location = new System.Drawing.Point(8, 48);
			this.labelLive.Name = "labelLive";
			this.labelLive.Size = new System.Drawing.Size(152, 23);
			this.labelLive.TabIndex = 5;
			this.labelLive.Text = "Месторождение Държава";
			// 
			// comboBoxNaselenoMqsto
			// 
			this.comboBoxNaselenoMqsto.DropDownWidth = 504;
			this.comboBoxNaselenoMqsto.ItemHeight = 13;
			this.comboBoxNaselenoMqsto.Location = new System.Drawing.Point(152, 104);
			this.comboBoxNaselenoMqsto.Name = "comboBoxNaselenoMqsto";
			this.comboBoxNaselenoMqsto.Size = new System.Drawing.Size(144, 21);
			this.comboBoxNaselenoMqsto.TabIndex = 45;
			// 
			// labelRegion
			// 
			this.labelRegion.Location = new System.Drawing.Point(8, 88);
			this.labelRegion.Name = "labelRegion";
			this.labelRegion.Size = new System.Drawing.Size(128, 16);
			this.labelRegion.TabIndex = 11;
			this.labelRegion.Text = "Област";
			// 
			// comboBoxRegion
			// 
			this.comboBoxRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRegion.DropDownWidth = 496;
			this.comboBoxRegion.ItemHeight = 13;
			this.comboBoxRegion.Location = new System.Drawing.Point(8, 104);
			this.comboBoxRegion.Name = "comboBoxRegion";
			this.comboBoxRegion.Size = new System.Drawing.Size(128, 21);
			this.comboBoxRegion.TabIndex = 10;
			this.comboBoxRegion.SelectedIndexChanged += new System.EventHandler(this.comboBoxRegion_SelectedIndexChanged);
			// 
			// labelNaselenoMqsto
			// 
			this.labelNaselenoMqsto.Location = new System.Drawing.Point(160, 88);
			this.labelNaselenoMqsto.Name = "labelNaselenoMqsto";
			this.labelNaselenoMqsto.Size = new System.Drawing.Size(136, 16);
			this.labelNaselenoMqsto.TabIndex = 13;
			this.labelNaselenoMqsto.Text = "Населено място";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.ItemSize = new System.Drawing.Size(87, 18);
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(880, 288);
			this.tabControl1.TabIndex = 47;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.comboBoxFamilyStatus);
			this.tabPage2.Controls.Add(this.comboBoxEducation);
			this.tabPage2.Controls.Add(this.textBoxDiplom);
			this.tabPage2.Controls.Add(this.textBoxWorkExperience);
			this.tabPage2.Controls.Add(this.labelFamilyStatus);
			this.tabPage2.Controls.Add(this.labelEducation);
			this.tabPage2.Controls.Add(this.labelDiplom);
			this.tabPage2.Controls.Add(this.comboBoxProfesion);
			this.tabPage2.Controls.Add(this.labelProfesion);
			this.tabPage2.Controls.Add(this.checkedListBoxLanguage);
			this.tabPage2.Controls.Add(this.labelLanguage);
			this.tabPage2.Controls.Add(this.labelScience);
			this.tabPage2.Controls.Add(this.comboBoxScience);
			this.tabPage2.Controls.Add(this.labelScienceLevel);
			this.tabPage2.Controls.Add(this.labelMilitaryRang);
			this.tabPage2.Controls.Add(this.comboBoxMilitaryRang);
			this.tabPage2.Controls.Add(this.comboBoxScienceLevel);
			this.tabPage2.Controls.Add(this.labelMilitaryStatus);
			this.tabPage2.Controls.Add(this.labelEmployeStatus);
			this.tabPage2.Controls.Add(this.labelCategory);
			this.tabPage2.Controls.Add(this.comboBoxMilitaryStatus);
			this.tabPage2.Controls.Add(this.comboBoxEmployeStatus);
			this.tabPage2.Controls.Add(this.comboBoxCategory);
			this.tabPage2.Controls.Add(this.dateTimePicker1);
			this.tabPage2.Controls.Add(this.labelHiredAt);
			this.tabPage2.Controls.Add(this.labelWorkExperiance);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(872, 262);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Допълнителни особенности";
			// 
			// comboBoxFamilyStatus
			// 
			this.comboBoxFamilyStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFamilyStatus.ItemHeight = 13;
			this.comboBoxFamilyStatus.Items.AddRange(new object[] {
																	  "Семеен",
																	  "Несеен",
																	  "Разведен",
																	  "Вдовец",
																	  "Непоказано"});
			this.comboBoxFamilyStatus.Location = new System.Drawing.Point(8, 32);
			this.comboBoxFamilyStatus.Name = "comboBoxFamilyStatus";
			this.comboBoxFamilyStatus.Size = new System.Drawing.Size(121, 21);
			this.comboBoxFamilyStatus.TabIndex = 17;
			// 
			// comboBoxEducation
			// 
			this.comboBoxEducation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEducation.ItemHeight = 13;
			this.comboBoxEducation.Items.AddRange(new object[] {
																   "Висше",
																   "Начално",
																   "Непоказано",
																   "Основно",
																   "Полувисше",
																   "Средно",
																   "Средноспециално",
																   "Среднотехническо"});
			this.comboBoxEducation.Location = new System.Drawing.Point(128, 32);
			this.comboBoxEducation.Name = "comboBoxEducation";
			this.comboBoxEducation.Size = new System.Drawing.Size(121, 21);
			this.comboBoxEducation.Sorted = true;
			this.comboBoxEducation.TabIndex = 19;
			// 
			// textBoxDiplom
			// 
			this.textBoxDiplom.Location = new System.Drawing.Point(248, 32);
			this.textBoxDiplom.Name = "textBoxDiplom";
			this.textBoxDiplom.TabIndex = 21;
			this.textBoxDiplom.Text = "";
			// 
			// textBoxWorkExperience
			// 
			this.textBoxWorkExperience.Location = new System.Drawing.Point(152, 200);
			this.textBoxWorkExperience.Name = "textBoxWorkExperience";
			this.textBoxWorkExperience.TabIndex = 44;
			this.textBoxWorkExperience.Text = "";
			// 
			// labelFamilyStatus
			// 
			this.labelFamilyStatus.Location = new System.Drawing.Point(8, 16);
			this.labelFamilyStatus.Name = "labelFamilyStatus";
			this.labelFamilyStatus.Size = new System.Drawing.Size(120, 16);
			this.labelFamilyStatus.TabIndex = 18;
			this.labelFamilyStatus.Text = "Семейно Положение";
			// 
			// labelEducation
			// 
			this.labelEducation.Location = new System.Drawing.Point(128, 16);
			this.labelEducation.Name = "labelEducation";
			this.labelEducation.Size = new System.Drawing.Size(120, 16);
			this.labelEducation.TabIndex = 20;
			this.labelEducation.Text = "Образование";
			// 
			// labelDiplom
			// 
			this.labelDiplom.Location = new System.Drawing.Point(248, 16);
			this.labelDiplom.Name = "labelDiplom";
			this.labelDiplom.Size = new System.Drawing.Size(100, 16);
			this.labelDiplom.TabIndex = 22;
			this.labelDiplom.Text = "Диплома данни";
			// 
			// comboBoxProfesion
			// 
			this.comboBoxProfesion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProfesion.ItemHeight = 13;
			this.comboBoxProfesion.Location = new System.Drawing.Point(8, 72);
			this.comboBoxProfesion.Name = "comboBoxProfesion";
			this.comboBoxProfesion.Size = new System.Drawing.Size(121, 21);
			this.comboBoxProfesion.TabIndex = 23;
			// 
			// labelProfesion
			// 
			this.labelProfesion.Location = new System.Drawing.Point(8, 56);
			this.labelProfesion.Name = "labelProfesion";
			this.labelProfesion.Size = new System.Drawing.Size(120, 16);
			this.labelProfesion.TabIndex = 24;
			this.labelProfesion.Text = "Професия";
			// 
			// checkedListBoxLanguage
			// 
			this.checkedListBoxLanguage.Items.AddRange(new object[] {
																		"Английски",
																		"Немски ",
																		"Френски",
																		"Руски",
																		"Испански",
																		"Португалски",
																		"Италиански",
																		"Гръцки",
																		"Сръбски",
																		"Румънски",
																		"Турски",
																		"(Цигански)",
																		""});
			this.checkedListBoxLanguage.Location = new System.Drawing.Point(264, 72);
			this.checkedListBoxLanguage.Name = "checkedListBoxLanguage";
			this.checkedListBoxLanguage.Size = new System.Drawing.Size(120, 64);
			this.checkedListBoxLanguage.TabIndex = 25;
			// 
			// labelLanguage
			// 
			this.labelLanguage.Location = new System.Drawing.Point(264, 56);
			this.labelLanguage.Name = "labelLanguage";
			this.labelLanguage.Size = new System.Drawing.Size(100, 16);
			this.labelLanguage.TabIndex = 26;
			this.labelLanguage.Text = "Езици";
			// 
			// labelScience
			// 
			this.labelScience.Location = new System.Drawing.Point(136, 56);
			this.labelScience.Name = "labelScience";
			this.labelScience.Size = new System.Drawing.Size(120, 16);
			this.labelScience.TabIndex = 30;
			this.labelScience.Text = "Научно звание";
			// 
			// comboBoxScience
			// 
			this.comboBoxScience.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScience.ItemHeight = 13;
			this.comboBoxScience.Location = new System.Drawing.Point(136, 72);
			this.comboBoxScience.Name = "comboBoxScience";
			this.comboBoxScience.Size = new System.Drawing.Size(121, 21);
			this.comboBoxScience.TabIndex = 29;
			// 
			// labelScienceLevel
			// 
			this.labelScienceLevel.Location = new System.Drawing.Point(8, 96);
			this.labelScienceLevel.Name = "labelScienceLevel";
			this.labelScienceLevel.Size = new System.Drawing.Size(120, 16);
			this.labelScienceLevel.TabIndex = 32;
			this.labelScienceLevel.Text = "Научна степен";
			// 
			// labelMilitaryRang
			// 
			this.labelMilitaryRang.Location = new System.Drawing.Point(136, 96);
			this.labelMilitaryRang.Name = "labelMilitaryRang";
			this.labelMilitaryRang.Size = new System.Drawing.Size(120, 16);
			this.labelMilitaryRang.TabIndex = 34;
			this.labelMilitaryRang.Text = "Военен ранг";
			// 
			// comboBoxMilitaryRang
			// 
			this.comboBoxMilitaryRang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMilitaryRang.ItemHeight = 13;
			this.comboBoxMilitaryRang.Location = new System.Drawing.Point(136, 112);
			this.comboBoxMilitaryRang.Name = "comboBoxMilitaryRang";
			this.comboBoxMilitaryRang.Size = new System.Drawing.Size(121, 21);
			this.comboBoxMilitaryRang.TabIndex = 33;
			// 
			// comboBoxScienceLevel
			// 
			this.comboBoxScienceLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScienceLevel.ItemHeight = 13;
			this.comboBoxScienceLevel.Location = new System.Drawing.Point(8, 112);
			this.comboBoxScienceLevel.Name = "comboBoxScienceLevel";
			this.comboBoxScienceLevel.Size = new System.Drawing.Size(121, 21);
			this.comboBoxScienceLevel.TabIndex = 31;
			// 
			// labelMilitaryStatus
			// 
			this.labelMilitaryStatus.Location = new System.Drawing.Point(8, 144);
			this.labelMilitaryStatus.Name = "labelMilitaryStatus";
			this.labelMilitaryStatus.Size = new System.Drawing.Size(120, 16);
			this.labelMilitaryStatus.TabIndex = 36;
			this.labelMilitaryStatus.Text = "Воененна отчетност";
			// 
			// labelEmployeStatus
			// 
			this.labelEmployeStatus.Location = new System.Drawing.Point(136, 144);
			this.labelEmployeStatus.Name = "labelEmployeStatus";
			this.labelEmployeStatus.Size = new System.Drawing.Size(120, 16);
			this.labelEmployeStatus.TabIndex = 38;
			this.labelEmployeStatus.Text = "Работен статус";
			// 
			// labelCategory
			// 
			this.labelCategory.Location = new System.Drawing.Point(264, 144);
			this.labelCategory.Name = "labelCategory";
			this.labelCategory.Size = new System.Drawing.Size(120, 16);
			this.labelCategory.TabIndex = 40;
			this.labelCategory.Text = "Категория";
			// 
			// comboBoxMilitaryStatus
			// 
			this.comboBoxMilitaryStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMilitaryStatus.ItemHeight = 13;
			this.comboBoxMilitaryStatus.Items.AddRange(new object[] {
																		"Отслужил",
																		"Неотслужил"});
			this.comboBoxMilitaryStatus.Location = new System.Drawing.Point(8, 160);
			this.comboBoxMilitaryStatus.Name = "comboBoxMilitaryStatus";
			this.comboBoxMilitaryStatus.Size = new System.Drawing.Size(121, 21);
			this.comboBoxMilitaryStatus.TabIndex = 35;
			// 
			// comboBoxEmployeStatus
			// 
			this.comboBoxEmployeStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEmployeStatus.ItemHeight = 13;
			this.comboBoxEmployeStatus.Items.AddRange(new object[] {
																	   "Работи",
																	   "Не работи"});
			this.comboBoxEmployeStatus.Location = new System.Drawing.Point(136, 160);
			this.comboBoxEmployeStatus.Name = "comboBoxEmployeStatus";
			this.comboBoxEmployeStatus.Size = new System.Drawing.Size(121, 21);
			this.comboBoxEmployeStatus.TabIndex = 37;
			// 
			// comboBoxCategory
			// 
			this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCategory.ItemHeight = 13;
			this.comboBoxCategory.Location = new System.Drawing.Point(264, 160);
			this.comboBoxCategory.Name = "comboBoxCategory";
			this.comboBoxCategory.Size = new System.Drawing.Size(121, 21);
			this.comboBoxCategory.TabIndex = 39;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(8, 200);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(136, 20);
			this.dateTimePicker1.TabIndex = 41;
			this.dateTimePicker1.Value = new System.DateTime(2004, 10, 30, 8, 46, 33, 359);
			// 
			// labelHiredAt
			// 
			this.labelHiredAt.Location = new System.Drawing.Point(8, 184);
			this.labelHiredAt.Name = "labelHiredAt";
			this.labelHiredAt.Size = new System.Drawing.Size(120, 16);
			this.labelHiredAt.TabIndex = 42;
			this.labelHiredAt.Text = "Постъпил на:";
			// 
			// labelWorkExperiance
			// 
			this.labelWorkExperiance.Location = new System.Drawing.Point(144, 184);
			this.labelWorkExperiance.Name = "labelWorkExperiance";
			this.labelWorkExperiance.Size = new System.Drawing.Size(88, 16);
			this.labelWorkExperiance.TabIndex = 43;
			this.labelWorkExperiance.Text = "Трудов Стаж";
			// 
			// AddNewPersonForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(728, 334);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.labelSex);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonОК);
			this.Name = "AddNewPersonForm";
			this.Text = "Нов служител";
			this.Load += new System.EventHandler(this.AddNewPersonForm_Load);
			this.tabPage1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonОК_Click(object sender, EventArgs e)
		{
			if( !this.IsPressedOKButton )
			{
				//Тази проверка е сложена защото когато се натисне един път
				//ок бутона тази процедура се извиква два пъти и така прави два 
				//записа в базата
				DataLayer.DataPackage package = new DataLayer.DataPackage();
				DataLayer.DataAction action = new DataLayer.DataAction( "person3", this.mainform.connString );
				this.ValidateAddPersonResult( package );
				action.InsertPerson( package );
				this.IsPressedOKButton = true;
				this.Close();
			}
			this.Close();
		}
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void AddNewPersonForm_Load(object sender, System.EventArgs e)
		{
			this.comboBoxCategory.DataSource = this.mainform.nomenclaatureData.arrCategory;
			this.comboBoxEducation.DataSource = this.mainform.nomenclaatureData.arrEducation;
            this.comboBoxMilitaryRang.DataSource = this.mainform.nomenclaatureData.arrMilitaryRang;
			this.comboBoxProfesion.DataSource = this.mainform.nomenclaatureData.arrProfession;
			this.comboBoxScience.DataSource =  this.mainform.nomenclaatureData.arrScienceTitle;
			this.comboBoxScienceLevel.DataSource = this.mainform.nomenclaatureData.arrScienceLevel;
//			this.comboBoxNaselenoMqsto = this
			this.comboBoxCountry.DataSource = this.mainform.nomenclaatureData.arrCountrys;


//			DataSet ds = new DataSet();
//			//			ds.ReadXml( "C:\\downloads" );
//			//			DataRow dr = ds.Tables[0].Rows[0];
//			DataLayer.DataAction daa = new DataLayer.DataAction("", this.mainform.connString);
//			ds = daa.SelectFromTable( "profession", "level" );
//			foreach( DataRow dr in ds.Tables[0].Rows)
//			{
//				this.comboBoxProfesion.Items.Add(dr[0].ToString());
//			}
//			ds.Tables.Remove( ds.Tables[0] );
//
//			ds = daa.SelectFromTable( "sciencelevel", "level" );
//			foreach( DataRow dr in ds.Tables[0].Rows)
//			{
//				this.comboBoxScienceLevel.Items.Add(dr[0].ToString());
//			}
//			ds.Tables.Remove( ds.Tables[0] );
//
//			ds = daa.SelectFromTable( "sciencetitle", "level" );
//			foreach( DataRow dr in ds.Tables[0].Rows)
//			{
//				this.comboBoxScience.Items.Add(dr[0].ToString());
//			}
//			ds.Tables.Remove( ds.Tables[0] );
//
//			ds = daa.SelectFromTable( "militaryrang", "level" );
//			foreach( DataRow dr in ds.Tables[0].Rows)
//			{
//				this.comboBoxMilitaryRang.Items.Add(dr[0].ToString());
//			}
//			ds.Tables.Remove( ds.Tables[0] );
//
//			ds = daa.SelectFromTable( "languages", "level" );
//			foreach( DataRow dr in ds.Tables[0].Rows)
//			{
//				this.checkedListBoxLanguage.Items.Add(dr[0].ToString());
//			}
//			ds.Tables.Remove( ds.Tables[0] );
//
//			ds = daa.SelectFromTable( "education", "level" );
//			foreach( DataRow dr in ds.Tables[0].Rows)
//			{
//				this.comboBoxEducation.Items.Add(dr[0].ToString());
//			}
//			ds.Tables.Remove( ds.Tables[0] );
//			ds.Tables.Remove( ds.Tables[0] );
//
//			ds = daa.SelectFromTable( "category", "level" );
//			foreach( DataRow dr in ds.Tables[0].Rows)
//			{
//				this.comboBoxCategory.Items.Add(dr[0].ToString());
//			}
//			ds.Tables.Remove( ds.Tables[0] );
//
//			ds = daa.SelectFromTable( "Region", "level" );
//			foreach( DataRow dr in ds.Tables[0].Rows)
//			{
//				this.comboBoxRegion.Items.Add( dr[0].ToString());
//			}
//			ds.Tables.Remove( ds.Tables[0] );
//
//			ds = daa.SelectColumnFromTable( "strani1", new string[]{"Code","CountryName"} );
		}

		public void ValidateAddPersonResult(DataLayer.DataPackage package)
		{
			package.ID = this.mainform.GenerateUniqueID();
			package.FName = this.textBoxNames.Text;
			if( this.bugBox1.Text == "" )
			{
				package.Egn = "Непоказана";
			}
			else
			{
				package.Egn = this.bugBox1.Text;
			}

//			if( this.comboBox.SelectedIndex == -1 )
//			{
//				package.BornTown = "Непоказана";
//			}
//			else
//			{
//				package.BornTown = this.comboBoxCountry.SelectedItem.ToString();
//			}

			if( this.comboBoxCategory.SelectedIndex == -1 )
			{
				package.Category = "Непоказана";
			}
			else
			{
				package.Category = this.comboBoxCategory.SelectedItem.ToString();
			}	

			if( this.comboBoxCountry.SelectedIndex == -1 )
			{
				package.Country = "Непоказана";
			}
			else
			{
				package.Country = this.comboBoxCountry.SelectedItem.ToString();
			}

			package.DiplomaData = this.textBoxDiplom.Text;  

			if( this.comboBoxEducation.SelectedIndex == -1 )
			{
				package.Education = "Непоказана";
			}
			else
			{
				package.Education = this.comboBoxEducation.SelectedItem.ToString();
			}

			if( this.comboBoxFamilyStatus.SelectedIndex == -1 )
			{
				package.FamilyStatus = "Непоказана";
			}
			else
			{
				package.FamilyStatus = this.comboBoxFamilyStatus.SelectedItem.ToString();
			}

			package.HiredAt = this.dateTimePicker1.Value;

			package.Kwartal = this.textBoxKwartal.Text;
			if( this.checkedListBoxLanguage.SelectedIndex == -1 )
			{
				package.Languages = "Непоказана";
			}
			else
			{
				package.Languages = this.checkedListBoxLanguage.SelectedItem.ToString();
			}

			if( this.comboBoxMilitaryRang.SelectedIndex == -1 )
			{
				package.MilitaryRang = "Непоказана";
			}
			else
			{
				package.MilitaryRang = this.comboBoxMilitaryRang.SelectedItem.ToString();
			}

			if( this.comboBoxMilitaryStatus.SelectedIndex == 0 )
			{
				package.MilitaryStatus = false;
			}
			else
			{
				package.MilitaryStatus = true;
			}

			package.NumBlockHouse = this.textBoxNumBlock.Text;

			if( this.numBoxPcCard.Text  == "" )
			{
				package.PCard = 0;
			}
			else
			{
				package.PCard = Int32.Parse(this.numBoxPcCard.Text);
			}

			package.PCardPublish = this.dateTimePicker2.Value;

			package.PublishedBy = this.textBoxPublishedFrom.Text;

			if( this.comboBoxRegion.SelectedIndex == -1 )
			{
				package.Region = "Непоказано";
			}
			else
			{
				package.Region = this.comboBoxRegion.SelectedItem.ToString();
			}

			if( this.comboBoxScienceLevel.SelectedIndex == -1 )
			{
				package.ScienceLevel = "Непоказано";
			}
			else
			{
				package.ScienceLevel = this.comboBoxScienceLevel.SelectedItem.ToString();
			}

			if( this.comboBoxScience.SelectedIndex == -1 )
			{
				package.ScienceTitle = "Непоказано";
			}
			else
			{
				package.ScienceTitle = this.comboBoxScience.SelectedItem.ToString();
			}

			package.Street = this.textBoxStreet.Text;

			if( this.numBox1.Text  == "" )
			{
				package.Telephone = 0;
			}
			else
			{
				package.Telephone = Int32.Parse(this.numBox1.Text);
			}

			if( this.comboBoxNaselenoMqsto.SelectedIndex == -1 )
			{
				package.Town = "Непоказана";
			}
			else
			{
				package.Town = this.comboBoxNaselenoMqsto.SelectedItem.ToString();
			}

			if( this.textBoxWorkExperience.Text  == "" )
			{
				package.WorkExperience = 0;
			}
			else
			{
				package.WorkExperience = Int32.Parse( this.textBoxWorkExperience.Text);
			}

			if( this.comboBoxEmployeStatus.SelectedIndex == -1 )
			{
				package.WorkStatus = "Непоказана";
			}
			else
			{
				package.WorkStatus = this.comboBoxEmployeStatus.SelectedItem.ToString();
			}

			if( this.comboBoxProfesion.SelectedIndex == -1 )
			{
				package.Proffesion = "Непоказана";
			}
			else
			{
				package.Proffesion = this.comboBoxProfesion.SelectedItem.ToString();
			}
		}

		private void comboBoxRegion_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.comboBoxNaselenoMqsto.Items.Clear();
			DataSet ds = new DataSet();
			DataLayer.DataAction daa = new DataLayer.DataAction("", this.mainform.connString);
			ds = daa.SelectTownsFromRegion( "towns", new string[]{"Name","Prefix"}, this.comboBoxRegion.SelectedIndex );
			foreach( DataRow dr in ds.Tables[0].Rows)
			{
				this.comboBoxNaselenoMqsto.Items.Add( dr[1].ToString()+ " "+ dr[0].ToString());
			}
			ds.Dispose();
		}

	}
}
