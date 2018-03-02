using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using DataLayer;

namespace HR
{
	/// <summary>
	/// Summary description for formRegister.
	/// </summary>
	public class formRegister : System.Windows.Forms.Form
	{
		#region Controls
		private System.Windows.Forms.TextBox textBoxFirmName;
        private System.Windows.Forms.Label labelFirmName;
		private System.Windows.Forms.Label labelType;
		private System.Windows.Forms.Label labelCind;
		private System.Windows.Forms.Label labelTown;
		private System.Windows.Forms.Label labelRegion;
		private BugBox.BugBox bugBoxPostalCode;
		private System.Windows.Forms.Label labelPostalCode;
		private System.Windows.Forms.Label labelAddressData;
		private System.Windows.Forms.TextBox textBoxAddressData;
		private System.Windows.Forms.Label labelNominalEmployes;
		private BugBox.BugBox bugBoxNominalEmployes;
		private BugBox.BugBox bugBoxSecureNumber;
		private System.Windows.Forms.Label labelSecNumber;
		private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonCacncel;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label2;
		private BugBox.BugBox bugBoxTelephone;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxDirectorName;
		private System.Windows.Forms.TextBox textBoxDirectorLSys;
		private System.Windows.Forms.TextBox textBoxTRZ;
		private System.Windows.Forms.TextBox textBoxMainConsult;
		private BugBox.BugBox bugBoxDirectorEGN;
		private System.Windows.Forms.TextBox textBoxMainAccountant;
		private System.Windows.Forms.Label labelMainConsult;
		private System.Windows.Forms.Label labelMainAccountant;
		private System.Windows.Forms.Label labelBossName;
		private System.Windows.Forms.Label labelTRZ;
		private System.Windows.Forms.Label label5;
		private BugBox.BugBox bugBoxEGNDirectorLSys;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelLichenSystaw;
		private System.Windows.Forms.Label labelEGNBoss;
		private BugBox.BugBox bugBoxEGNMainAccountant;
		private System.Windows.Forms.GroupBox groupBox4;
		private BugBox.BugBox bugBoxBankCode;
		private System.Windows.Forms.Label labelBankCode;
		private System.Windows.Forms.Label labelBankName;
		private BugBox.BugBox bugBoxBankAccount;
		private System.Windows.Forms.Label labelBankAccount;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private BugBox.BugBox bugBoxEGNMainConsult;
		private BugBox.BugBox bugBoxEGNTRZ;
		private System.Windows.Forms.TextBox textBoxBankName;
		private System.Windows.Forms.TextBox textBoxAdditionalInfo;
		mainForm formmain;
		#endregion// Kontroli po formata
		private System.Windows.Forms.TextBox textBoxEmail;
		private System.Windows.Forms.TextBox textBoxTaxNum;
		private System.Windows.Forms.TextBox textBoxBulstat;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox comboBoxNKIDCode;
		private System.Windows.Forms.Button buttonSelectPosition;
		private System.Windows.Forms.ComboBox comboBoxNKIDLevel;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxTown;
		private System.Windows.Forms.TextBox textBoxRegion;
        private TextBox textBoxKind;
        private TextBox textBoxType;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formRegister( mainForm main)
		{
            this.formmain = main;
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formRegister));
            this.textBoxFirmName = new System.Windows.Forms.TextBox();
            this.labelFirmName = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.labelCind = new System.Windows.Forms.Label();
            this.labelTown = new System.Windows.Forms.Label();
            this.labelRegion = new System.Windows.Forms.Label();
            this.bugBoxPostalCode = new BugBox.BugBox();
            this.labelPostalCode = new System.Windows.Forms.Label();
            this.labelAddressData = new System.Windows.Forms.Label();
            this.textBoxAddressData = new System.Windows.Forms.TextBox();
            this.labelNominalEmployes = new System.Windows.Forms.Label();
            this.bugBoxNominalEmployes = new BugBox.BugBox();
            this.bugBoxSecureNumber = new BugBox.BugBox();
            this.labelSecNumber = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxRegion = new System.Windows.Forms.TextBox();
            this.textBoxTown = new System.Windows.Forms.TextBox();
            this.buttonSelectPosition = new System.Windows.Forms.Button();
            this.comboBoxNKIDCode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bugBoxTelephone = new BugBox.BugBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.comboBoxNKIDLevel = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxAdditionalInfo = new System.Windows.Forms.TextBox();
            this.buttonCacncel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelBossName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bugBoxEGNTRZ = new BugBox.BugBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bugBoxEGNMainConsult = new BugBox.BugBox();
            this.textBoxDirectorLSys = new System.Windows.Forms.TextBox();
            this.textBoxTRZ = new System.Windows.Forms.TextBox();
            this.textBoxMainConsult = new System.Windows.Forms.TextBox();
            this.bugBoxDirectorEGN = new BugBox.BugBox();
            this.textBoxMainAccountant = new System.Windows.Forms.TextBox();
            this.labelMainConsult = new System.Windows.Forms.Label();
            this.labelMainAccountant = new System.Windows.Forms.Label();
            this.labelTRZ = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bugBoxEGNDirectorLSys = new BugBox.BugBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelLichenSystaw = new System.Windows.Forms.Label();
            this.labelEGNBoss = new System.Windows.Forms.Label();
            this.bugBoxEGNMainAccountant = new BugBox.BugBox();
            this.textBoxDirectorName = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxTaxNum = new System.Windows.Forms.TextBox();
            this.bugBoxBankCode = new BugBox.BugBox();
            this.labelBankCode = new System.Windows.Forms.Label();
            this.labelBankName = new System.Windows.Forms.Label();
            this.textBoxBankName = new System.Windows.Forms.TextBox();
            this.bugBoxBankAccount = new BugBox.BugBox();
            this.labelBankAccount = new System.Windows.Forms.Label();
            this.textBoxBulstat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.textBoxKind = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFirmName
            // 
            this.textBoxFirmName.Location = new System.Drawing.Point(8, 32);
            this.textBoxFirmName.Name = "textBoxFirmName";
            this.textBoxFirmName.Size = new System.Drawing.Size(400, 20);
            this.textBoxFirmName.TabIndex = 0;
            // 
            // labelFirmName
            // 
            this.labelFirmName.Location = new System.Drawing.Point(8, 16);
            this.labelFirmName.Name = "labelFirmName";
            this.labelFirmName.Size = new System.Drawing.Size(192, 16);
            this.labelFirmName.TabIndex = 1;
            this.labelFirmName.Text = "Наименование на организацията :";
            // 
            // labelType
            // 
            this.labelType.Location = new System.Drawing.Point(424, 16);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(48, 16);
            this.labelType.TabIndex = 3;
            this.labelType.Text = "Тип :";
            // 
            // labelCind
            // 
            this.labelCind.Location = new System.Drawing.Point(576, 16);
            this.labelCind.Name = "labelCind";
            this.labelCind.Size = new System.Drawing.Size(64, 16);
            this.labelCind.TabIndex = 5;
            this.labelCind.Text = "Вид :";
            // 
            // labelTown
            // 
            this.labelTown.Location = new System.Drawing.Point(144, 56);
            this.labelTown.Name = "labelTown";
            this.labelTown.Size = new System.Drawing.Size(144, 16);
            this.labelTown.TabIndex = 9;
            this.labelTown.Text = "Населено място :";
            // 
            // labelRegion
            // 
            this.labelRegion.Location = new System.Drawing.Point(8, 56);
            this.labelRegion.Name = "labelRegion";
            this.labelRegion.Size = new System.Drawing.Size(112, 16);
            this.labelRegion.TabIndex = 7;
            this.labelRegion.Text = "Област";
            // 
            // bugBoxPostalCode
            // 
            this.bugBoxPostalCode.Location = new System.Drawing.Point(304, 72);
            this.bugBoxPostalCode.Name = "bugBoxPostalCode";
            this.bugBoxPostalCode.OnlyInteger = true;
            this.bugBoxPostalCode.OnlyPositive = true;
            this.bugBoxPostalCode.Size = new System.Drawing.Size(102, 20);
            this.bugBoxPostalCode.TabIndex = 10;
            // 
            // labelPostalCode
            // 
            this.labelPostalCode.Location = new System.Drawing.Point(304, 56);
            this.labelPostalCode.Name = "labelPostalCode";
            this.labelPostalCode.Size = new System.Drawing.Size(100, 16);
            this.labelPostalCode.TabIndex = 11;
            this.labelPostalCode.Text = "Пощенкси код :";
            // 
            // labelAddressData
            // 
            this.labelAddressData.Location = new System.Drawing.Point(424, 56);
            this.labelAddressData.Name = "labelAddressData";
            this.labelAddressData.Size = new System.Drawing.Size(184, 16);
            this.labelAddressData.TabIndex = 13;
            this.labelAddressData.Text = "Адресни данни :";
            // 
            // textBoxAddressData
            // 
            this.textBoxAddressData.Location = new System.Drawing.Point(424, 72);
            this.textBoxAddressData.Multiline = true;
            this.textBoxAddressData.Name = "textBoxAddressData";
            this.textBoxAddressData.Size = new System.Drawing.Size(296, 62);
            this.textBoxAddressData.TabIndex = 12;
            // 
            // labelNominalEmployes
            // 
            this.labelNominalEmployes.Location = new System.Drawing.Point(424, 144);
            this.labelNominalEmployes.Name = "labelNominalEmployes";
            this.labelNominalEmployes.Size = new System.Drawing.Size(104, 16);
            this.labelNominalEmployes.TabIndex = 37;
            this.labelNominalEmployes.Text = "Брой служители :";
            // 
            // bugBoxNominalEmployes
            // 
            this.bugBoxNominalEmployes.Location = new System.Drawing.Point(424, 160);
            this.bugBoxNominalEmployes.Name = "bugBoxNominalEmployes";
            this.bugBoxNominalEmployes.OnlyInteger = true;
            this.bugBoxNominalEmployes.OnlyPositive = true;
            this.bugBoxNominalEmployes.Size = new System.Drawing.Size(136, 20);
            this.bugBoxNominalEmployes.TabIndex = 38;
            // 
            // bugBoxSecureNumber
            // 
            this.bugBoxSecureNumber.Location = new System.Drawing.Point(576, 160);
            this.bugBoxSecureNumber.Name = "bugBoxSecureNumber";
            this.bugBoxSecureNumber.OnlyInteger = true;
            this.bugBoxSecureNumber.OnlyPositive = true;
            this.bugBoxSecureNumber.Size = new System.Drawing.Size(144, 20);
            this.bugBoxSecureNumber.TabIndex = 40;
            // 
            // labelSecNumber
            // 
            this.labelSecNumber.Location = new System.Drawing.Point(576, 144);
            this.labelSecNumber.Name = "labelSecNumber";
            this.labelSecNumber.Size = new System.Drawing.Size(128, 16);
            this.labelSecNumber.TabIndex = 39;
            this.labelSecNumber.Text = "Осигурителен номер :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxKind);
            this.groupBox2.Controls.Add(this.textBoxType);
            this.groupBox2.Controls.Add(this.textBoxRegion);
            this.groupBox2.Controls.Add(this.textBoxTown);
            this.groupBox2.Controls.Add(this.buttonSelectPosition);
            this.groupBox2.Controls.Add(this.comboBoxNKIDCode);
            this.groupBox2.Controls.Add(this.bugBoxSecureNumber);
            this.groupBox2.Controls.Add(this.bugBoxNominalEmployes);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.bugBoxTelephone);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxEmail);
            this.groupBox2.Controls.Add(this.labelTown);
            this.groupBox2.Controls.Add(this.labelNominalEmployes);
            this.groupBox2.Controls.Add(this.textBoxAddressData);
            this.groupBox2.Controls.Add(this.labelAddressData);
            this.groupBox2.Controls.Add(this.labelRegion);
            this.groupBox2.Controls.Add(this.textBoxFirmName);
            this.groupBox2.Controls.Add(this.labelSecNumber);
            this.groupBox2.Controls.Add(this.bugBoxPostalCode);
            this.groupBox2.Controls.Add(this.labelFirmName);
            this.groupBox2.Controls.Add(this.labelType);
            this.groupBox2.Controls.Add(this.labelCind);
            this.groupBox2.Controls.Add(this.labelPostalCode);
            this.groupBox2.Controls.Add(this.comboBoxNKIDLevel);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(8, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(728, 200);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Организацията - основни данни";
            // 
            // textBoxRegion
            // 
            this.textBoxRegion.Location = new System.Drawing.Point(8, 72);
            this.textBoxRegion.Name = "textBoxRegion";
            this.textBoxRegion.Size = new System.Drawing.Size(120, 20);
            this.textBoxRegion.TabIndex = 92;
            // 
            // textBoxTown
            // 
            this.textBoxTown.Location = new System.Drawing.Point(144, 72);
            this.textBoxTown.Name = "textBoxTown";
            this.textBoxTown.Size = new System.Drawing.Size(144, 20);
            this.textBoxTown.TabIndex = 91;
            // 
            // buttonSelectPosition
            // 
            this.buttonSelectPosition.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelectPosition.Image")));
            this.buttonSelectPosition.Location = new System.Drawing.Point(392, 160);
            this.buttonSelectPosition.Name = "buttonSelectPosition";
            this.buttonSelectPosition.Size = new System.Drawing.Size(21, 21);
            this.buttonSelectPosition.TabIndex = 90;
            // 
            // comboBoxNKIDCode
            // 
            this.comboBoxNKIDCode.Location = new System.Drawing.Point(272, 160);
            this.comboBoxNKIDCode.Name = "comboBoxNKIDCode";
            this.comboBoxNKIDCode.Size = new System.Drawing.Size(120, 21);
            this.comboBoxNKIDCode.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(144, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 47;
            this.label2.Text = "E-mail :";
            // 
            // bugBoxTelephone
            // 
            this.bugBoxTelephone.Location = new System.Drawing.Point(8, 112);
            this.bugBoxTelephone.Name = "bugBoxTelephone";
            this.bugBoxTelephone.OnlyInteger = true;
            this.bugBoxTelephone.OnlyPositive = true;
            this.bugBoxTelephone.Size = new System.Drawing.Size(120, 20);
            this.bugBoxTelephone.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 44;
            this.label1.Text = "Телефон :";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(144, 112);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(264, 20);
            this.textBoxEmail.TabIndex = 45;
            // 
            // comboBoxNKIDLevel
            // 
            this.comboBoxNKIDLevel.Location = new System.Drawing.Point(8, 160);
            this.comboBoxNKIDLevel.Name = "comboBoxNKIDLevel";
            this.comboBoxNKIDLevel.Size = new System.Drawing.Size(264, 21);
            this.comboBoxNKIDLevel.TabIndex = 48;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(192, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "Икономическа дейност по НКИД :";
            // 
            // buttonOk
            // 
            this.buttonOk.Image = ((System.Drawing.Image)(resources.GetObject("buttonOk.Image")));
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(240, 496);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(90, 23);
            this.buttonOk.TabIndex = 43;
            this.buttonOk.Text = "Запис";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxAdditionalInfo);
            this.groupBox1.Location = new System.Drawing.Point(8, 424);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(728, 64);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Допълнителна информация";
            // 
            // textBoxAdditionalInfo
            // 
            this.textBoxAdditionalInfo.Location = new System.Drawing.Point(8, 16);
            this.textBoxAdditionalInfo.Multiline = true;
            this.textBoxAdditionalInfo.Name = "textBoxAdditionalInfo";
            this.textBoxAdditionalInfo.Size = new System.Drawing.Size(712, 40);
            this.textBoxAdditionalInfo.TabIndex = 0;
            // 
            // buttonCacncel
            // 
            this.buttonCacncel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCacncel.Image")));
            this.buttonCacncel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCacncel.Location = new System.Drawing.Point(416, 496);
            this.buttonCacncel.Name = "buttonCacncel";
            this.buttonCacncel.Size = new System.Drawing.Size(90, 23);
            this.buttonCacncel.TabIndex = 52;
            this.buttonCacncel.Text = "Откажи";
            this.buttonCacncel.Click += new System.EventHandler(this.buttonCacncel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelBossName);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.bugBoxEGNTRZ);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.bugBoxEGNMainConsult);
            this.groupBox3.Controls.Add(this.textBoxDirectorLSys);
            this.groupBox3.Controls.Add(this.textBoxTRZ);
            this.groupBox3.Controls.Add(this.textBoxMainConsult);
            this.groupBox3.Controls.Add(this.bugBoxDirectorEGN);
            this.groupBox3.Controls.Add(this.textBoxMainAccountant);
            this.groupBox3.Controls.Add(this.labelMainConsult);
            this.groupBox3.Controls.Add(this.labelMainAccountant);
            this.groupBox3.Controls.Add(this.labelTRZ);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.bugBoxEGNDirectorLSys);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.labelLichenSystaw);
            this.groupBox3.Controls.Add(this.labelEGNBoss);
            this.groupBox3.Controls.Add(this.bugBoxEGNMainAccountant);
            this.groupBox3.Controls.Add(this.textBoxDirectorName);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(8, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(432, 224);
            this.groupBox3.TabIndex = 69;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Основни длъжности в администрацията";
            // 
            // labelBossName
            // 
            this.labelBossName.Location = new System.Drawing.Point(30, 16);
            this.labelBossName.Name = "labelBossName";
            this.labelBossName.Size = new System.Drawing.Size(184, 16);
            this.labelBossName.TabIndex = 70;
            this.labelBossName.Text = "Ръководител :";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(312, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 16);
            this.label6.TabIndex = 88;
            this.label6.Text = "ЕГН";
            // 
            // bugBoxEGNTRZ
            // 
            this.bugBoxEGNTRZ.Location = new System.Drawing.Point(312, 192);
            this.bugBoxEGNTRZ.Name = "bugBoxEGNTRZ";
            this.bugBoxEGNTRZ.OnlyInteger = true;
            this.bugBoxEGNTRZ.OnlyPositive = true;
            this.bugBoxEGNTRZ.Size = new System.Drawing.Size(100, 20);
            this.bugBoxEGNTRZ.TabIndex = 87;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(312, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 86;
            this.label3.Text = "ЕГН";
            // 
            // bugBoxEGNMainConsult
            // 
            this.bugBoxEGNMainConsult.Location = new System.Drawing.Point(312, 152);
            this.bugBoxEGNMainConsult.Name = "bugBoxEGNMainConsult";
            this.bugBoxEGNMainConsult.OnlyInteger = true;
            this.bugBoxEGNMainConsult.OnlyPositive = true;
            this.bugBoxEGNMainConsult.Size = new System.Drawing.Size(100, 20);
            this.bugBoxEGNMainConsult.TabIndex = 85;
            // 
            // textBoxDirectorLSys
            // 
            this.textBoxDirectorLSys.Location = new System.Drawing.Point(8, 110);
            this.textBoxDirectorLSys.Name = "textBoxDirectorLSys";
            this.textBoxDirectorLSys.Size = new System.Drawing.Size(288, 20);
            this.textBoxDirectorLSys.TabIndex = 71;
            // 
            // textBoxTRZ
            // 
            this.textBoxTRZ.Location = new System.Drawing.Point(8, 192);
            this.textBoxTRZ.Name = "textBoxTRZ";
            this.textBoxTRZ.Size = new System.Drawing.Size(288, 20);
            this.textBoxTRZ.TabIndex = 83;
            // 
            // textBoxMainConsult
            // 
            this.textBoxMainConsult.Location = new System.Drawing.Point(8, 150);
            this.textBoxMainConsult.Name = "textBoxMainConsult";
            this.textBoxMainConsult.Size = new System.Drawing.Size(288, 20);
            this.textBoxMainConsult.TabIndex = 81;
            // 
            // bugBoxDirectorEGN
            // 
            this.bugBoxDirectorEGN.Location = new System.Drawing.Point(312, 30);
            this.bugBoxDirectorEGN.Name = "bugBoxDirectorEGN";
            this.bugBoxDirectorEGN.OnlyInteger = true;
            this.bugBoxDirectorEGN.OnlyPositive = true;
            this.bugBoxDirectorEGN.Size = new System.Drawing.Size(100, 20);
            this.bugBoxDirectorEGN.TabIndex = 75;
            // 
            // textBoxMainAccountant
            // 
            this.textBoxMainAccountant.Location = new System.Drawing.Point(8, 70);
            this.textBoxMainAccountant.Name = "textBoxMainAccountant";
            this.textBoxMainAccountant.Size = new System.Drawing.Size(288, 20);
            this.textBoxMainAccountant.TabIndex = 73;
            // 
            // labelMainConsult
            // 
            this.labelMainConsult.Location = new System.Drawing.Point(30, 134);
            this.labelMainConsult.Name = "labelMainConsult";
            this.labelMainConsult.Size = new System.Drawing.Size(184, 16);
            this.labelMainConsult.TabIndex = 82;
            this.labelMainConsult.Text = "Главен юрист консулт :";
            // 
            // labelMainAccountant
            // 
            this.labelMainAccountant.Location = new System.Drawing.Point(30, 54);
            this.labelMainAccountant.Name = "labelMainAccountant";
            this.labelMainAccountant.Size = new System.Drawing.Size(184, 16);
            this.labelMainAccountant.TabIndex = 74;
            this.labelMainAccountant.Text = "Главен счетоводител :";
            // 
            // labelTRZ
            // 
            this.labelTRZ.Location = new System.Drawing.Point(30, 176);
            this.labelTRZ.Name = "labelTRZ";
            this.labelTRZ.Size = new System.Drawing.Size(184, 16);
            this.labelTRZ.TabIndex = 84;
            this.labelTRZ.Text = "Ръководител ТРЗ :";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(312, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 80;
            this.label5.Text = "ЕГН";
            // 
            // bugBoxEGNDirectorLSys
            // 
            this.bugBoxEGNDirectorLSys.Location = new System.Drawing.Point(312, 110);
            this.bugBoxEGNDirectorLSys.Name = "bugBoxEGNDirectorLSys";
            this.bugBoxEGNDirectorLSys.OnlyInteger = true;
            this.bugBoxEGNDirectorLSys.OnlyPositive = true;
            this.bugBoxEGNDirectorLSys.Size = new System.Drawing.Size(100, 20);
            this.bugBoxEGNDirectorLSys.TabIndex = 79;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(312, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 78;
            this.label4.Text = "ЕГН";
            // 
            // labelLichenSystaw
            // 
            this.labelLichenSystaw.Location = new System.Drawing.Point(30, 94);
            this.labelLichenSystaw.Name = "labelLichenSystaw";
            this.labelLichenSystaw.Size = new System.Drawing.Size(232, 16);
            this.labelLichenSystaw.TabIndex = 72;
            this.labelLichenSystaw.Text = "Завеждащ \"Личен състав\"  :";
            // 
            // labelEGNBoss
            // 
            this.labelEGNBoss.Location = new System.Drawing.Point(312, 14);
            this.labelEGNBoss.Name = "labelEGNBoss";
            this.labelEGNBoss.Size = new System.Drawing.Size(100, 16);
            this.labelEGNBoss.TabIndex = 76;
            this.labelEGNBoss.Text = "ЕГН";
            // 
            // bugBoxEGNMainAccountant
            // 
            this.bugBoxEGNMainAccountant.Location = new System.Drawing.Point(312, 70);
            this.bugBoxEGNMainAccountant.Name = "bugBoxEGNMainAccountant";
            this.bugBoxEGNMainAccountant.OnlyInteger = true;
            this.bugBoxEGNMainAccountant.OnlyPositive = true;
            this.bugBoxEGNMainAccountant.Size = new System.Drawing.Size(100, 20);
            this.bugBoxEGNMainAccountant.TabIndex = 77;
            // 
            // textBoxDirectorName
            // 
            this.textBoxDirectorName.Location = new System.Drawing.Point(8, 32);
            this.textBoxDirectorName.Name = "textBoxDirectorName";
            this.textBoxDirectorName.Size = new System.Drawing.Size(288, 20);
            this.textBoxDirectorName.TabIndex = 69;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxTaxNum);
            this.groupBox4.Controls.Add(this.bugBoxBankCode);
            this.groupBox4.Controls.Add(this.labelBankCode);
            this.groupBox4.Controls.Add(this.labelBankName);
            this.groupBox4.Controls.Add(this.textBoxBankName);
            this.groupBox4.Controls.Add(this.bugBoxBankAccount);
            this.groupBox4.Controls.Add(this.labelBankAccount);
            this.groupBox4.Controls.Add(this.textBoxBulstat);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(448, 200);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(288, 224);
            this.groupBox4.TabIndex = 70;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Банкова информация";
            // 
            // textBoxTaxNum
            // 
            this.textBoxTaxNum.Location = new System.Drawing.Point(16, 72);
            this.textBoxTaxNum.Name = "textBoxTaxNum";
            this.textBoxTaxNum.Size = new System.Drawing.Size(256, 20);
            this.textBoxTaxNum.TabIndex = 57;
            // 
            // bugBoxBankCode
            // 
            this.bugBoxBankCode.Location = new System.Drawing.Point(16, 192);
            this.bugBoxBankCode.Name = "bugBoxBankCode";
            this.bugBoxBankCode.OnlyInteger = true;
            this.bugBoxBankCode.OnlyPositive = true;
            this.bugBoxBankCode.Size = new System.Drawing.Size(256, 20);
            this.bugBoxBankCode.TabIndex = 53;
            // 
            // labelBankCode
            // 
            this.labelBankCode.Location = new System.Drawing.Point(16, 176);
            this.labelBankCode.Name = "labelBankCode";
            this.labelBankCode.Size = new System.Drawing.Size(100, 16);
            this.labelBankCode.TabIndex = 54;
            this.labelBankCode.Text = "Банков код :";
            // 
            // labelBankName
            // 
            this.labelBankName.Location = new System.Drawing.Point(16, 96);
            this.labelBankName.Name = "labelBankName";
            this.labelBankName.Size = new System.Drawing.Size(184, 16);
            this.labelBankName.TabIndex = 52;
            this.labelBankName.Text = "Банка :";
            // 
            // textBoxBankName
            // 
            this.textBoxBankName.Location = new System.Drawing.Point(16, 112);
            this.textBoxBankName.Name = "textBoxBankName";
            this.textBoxBankName.Size = new System.Drawing.Size(256, 20);
            this.textBoxBankName.TabIndex = 51;
            // 
            // bugBoxBankAccount
            // 
            this.bugBoxBankAccount.Location = new System.Drawing.Point(16, 152);
            this.bugBoxBankAccount.Name = "bugBoxBankAccount";
            this.bugBoxBankAccount.OnlyInteger = true;
            this.bugBoxBankAccount.OnlyPositive = true;
            this.bugBoxBankAccount.Size = new System.Drawing.Size(256, 20);
            this.bugBoxBankAccount.TabIndex = 55;
            // 
            // labelBankAccount
            // 
            this.labelBankAccount.Location = new System.Drawing.Point(16, 136);
            this.labelBankAccount.Name = "labelBankAccount";
            this.labelBankAccount.Size = new System.Drawing.Size(100, 16);
            this.labelBankAccount.TabIndex = 56;
            this.labelBankAccount.Text = "Банкова сметка :";
            // 
            // textBoxBulstat
            // 
            this.textBoxBulstat.Location = new System.Drawing.Point(16, 32);
            this.textBoxBulstat.Name = "textBoxBulstat";
            this.textBoxBulstat.Size = new System.Drawing.Size(256, 20);
            this.textBoxBulstat.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 16);
            this.label7.TabIndex = 52;
            this.label7.Text = "Данъчен номер :";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(184, 16);
            this.label8.TabIndex = 52;
            this.label8.Text = "БУЛСТАТ :";
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(424, 32);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(136, 20);
            this.textBoxType.TabIndex = 93;
            // 
            // textBoxKind
            // 
            this.textBoxKind.Location = new System.Drawing.Point(576, 32);
            this.textBoxKind.Name = "textBoxKind";
            this.textBoxKind.Size = new System.Drawing.Size(144, 20);
            this.textBoxKind.TabIndex = 94;
            // 
            // formRegister
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(752, 525);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCacncel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1280, 1024);
            this.MinimumSize = new System.Drawing.Size(700, 480);
            this.Name = "formRegister";
            this.ShowInTaskbar = false;
            this.Text = "Регистрация";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
            Dictionary<string, object> Dict = new Dictionary<string,object>();
			DataAction action = new DataLayer.DataAction(this.formmain.connString  );
			this.ValidateAdminRegistration( Dict );
			if (action.UniversalUpdateParam(TableNames.AdminInfo, "id", Dict, 1.ToString(), TransactionComnmand.NO_TRANSACTION) == false) //Especially here the table has only one row and it will have always be with id 1
			{
				MessageBox.Show("Грешка при редакция на данни", ErrorMessages.NoConnection);
			}
			else
			{
				this.Close();
			}
		}

		private void RegisterForm_Load(object sender, System.EventArgs e)
		{
            try
            {
                DataTable dtAdminInfo, dtNKID;
                int index = 0;

                DataAction daa = new DataLayer.DataAction(this.formmain.connString);
                dtAdminInfo = daa.SelectWhere(TableNames.AdminInfo, "*", " WHERE id = 1"); //select only the first row
                dtNKID = daa.SelectWhere(TableNames.NKID, "*", "ORDER BY id");
                if ((dtAdminInfo == null) || (dtNKID == null))
                {
                    MessageBox.Show("Грешка при зареждане на данните за администрацията", ErrorMessages.NoConnection);
                    this.Close();
                }
                try
                {
                    if (dtAdminInfo.Rows.Count < 1)
                    {
                        MessageBox.Show("Няма данни за организацията все още!");
                        return;
                    }
                    this.textBoxFirmName.Text = (string)dtAdminInfo.Rows[0]["firmname"];
                    this.textBoxAddressData.Text = (string)dtAdminInfo.Rows[0]["addressdata"];
                    this.textBoxEmail.Text = (string)dtAdminInfo.Rows[0]["email"];
                    this.textBoxAdditionalInfo.Text = (string)dtAdminInfo.Rows[0]["additionalinfo"];
                    this.textBoxBankName.Text = (string)dtAdminInfo.Rows[0]["bankname"];
                    this.textBoxDirectorLSys.Text = (string)dtAdminInfo.Rows[0]["directorlsys"];
                    this.textBoxDirectorName.Text = (string)dtAdminInfo.Rows[0]["directorname"];
                    this.textBoxMainAccountant.Text = (string)dtAdminInfo.Rows[0]["mainaccountantname"];
                    this.textBoxMainConsult.Text = (string)dtAdminInfo.Rows[0]["mainconsult"];
                    this.textBoxTRZ.Text = (string)dtAdminInfo.Rows[0]["trz"];
                    this.textBoxRegion.Text = (string)dtAdminInfo.Rows[0]["region"];
                    this.textBoxTown.Text = (string)dtAdminInfo.Rows[0]["town"];

                    this.bugBoxBankAccount.Text = (string)dtAdminInfo.Rows[0]["bankaccount"];
                    this.bugBoxBankCode.Text = (string)dtAdminInfo.Rows[0]["bankcode"];
                    this.bugBoxDirectorEGN.Text = (string)dtAdminInfo.Rows[0]["egndirector"];
                    this.bugBoxEGNDirectorLSys.Text = (string)dtAdminInfo.Rows[0]["egndirectorlsys"];
                    this.bugBoxEGNMainAccountant.Text = (string)dtAdminInfo.Rows[0]["egnmainaccountant"];
                    this.bugBoxEGNMainConsult.Text = (string)dtAdminInfo.Rows[0]["egnmainconsult"];
                    this.bugBoxEGNTRZ.Text = (string)dtAdminInfo.Rows[0]["egntrz"];
                    this.bugBoxNominalEmployes.Text = (string)dtAdminInfo.Rows[0]["nominalemployees"].ToString();
                    this.bugBoxPostalCode.Text = (string)dtAdminInfo.Rows[0]["postalcode"];
                    this.bugBoxSecureNumber.Text = (string)dtAdminInfo.Rows[0]["securenumber"];
                    this.bugBoxTelephone.Text = (string)dtAdminInfo.Rows[0]["phone"];
                    this.textBoxTaxNum.Text = (string)dtAdminInfo.Rows[0]["taxNum"];
                    this.textBoxBulstat.Text = (string)dtAdminInfo.Rows[0]["bulstat"];

                    this.comboBoxNKIDCode.DataSource = dtNKID;
                    this.comboBoxNKIDCode.DisplayMember = "code";
                    this.comboBoxNKIDLevel.DataSource = dtNKID;
                    this.comboBoxNKIDLevel.DisplayMember = "level";

                    this.textBoxType.Text = dtAdminInfo.Rows[0]["type"].ToString();
                    this.textBoxKind.Text = dtAdminInfo.Rows[0]["kind"].ToString();

                    index = 0;

                    index = this.comboBoxNKIDCode.FindStringExact(dtAdminInfo.Rows[0]["NKIDCode"].ToString());
                    if (index > -1)
                    {
                        this.comboBoxNKIDCode.SelectedIndex = index;
                    }
                    index = 0;

                    index = this.comboBoxNKIDLevel.FindStringExact(dtAdminInfo.Rows[0]["NKIDLevel"].ToString());
                    if (index > -1)
                    {
                        this.comboBoxNKIDLevel.SelectedIndex = index;
                    }
                    index = 0;
                }
                catch (System.IndexOutOfRangeException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public void ValidateAdminRegistration(Dictionary <string, object> Dict)
		{
			Dict.Add("firmName", this.textBoxFirmName.Text);
			Dict.Add("Type", this.textBoxType.Text);
            Dict.Add("Kind", this.textBoxKind.Text);
            Dict.Add("Region", this.textBoxRegion.Text);
            Dict.Add("Town", this.textBoxTown.Text);
            Dict.Add("PostalCode", this.bugBoxPostalCode.Text); 
            Dict.Add("AddressData", this.textBoxAddressData.Text); 
			Dict.Add("Email", this.textBoxEmail.Text);   //е-маил
			Dict.Add("Phone", this.bugBoxTelephone.Text);  //или телефон
            Dict.Add("SecureNumber", this.bugBoxSecureNumber.Text); 
            if( this.bugBoxNominalEmployes.Text == "" )     // Брой работници Контрол на числото
			{
				Dict.Add("NominalEmployees", 0.ToString());
			}
			else
			{
				Dict.Add("NominalEmployees", this.bugBoxNominalEmployes.Text);
			}
            Dict.Add("BankName", this.textBoxBankName.Text);  
			Dict.Add("BankAccount", this.bugBoxBankAccount.Text); 
            Dict.Add("BankCode", this.bugBoxBankCode.Text);  
            Dict.Add("DirectorName", this.textBoxDirectorName.Text); 
			Dict.Add("DirectorLSys", this.textBoxDirectorLSys.Text);
			Dict.Add("MainAccountantName", this.textBoxMainAccountant.Text);
			Dict.Add("MainConsult", this.textBoxMainConsult.Text);
			Dict.Add("TRZ", this.textBoxTRZ.Text);
			Dict.Add("EGNDirector", this.bugBoxDirectorEGN.Text);
			Dict.Add("EGNDirectorLSys", this.bugBoxEGNDirectorLSys.Text);
			Dict.Add("EGNMainAccountant", this.bugBoxEGNMainAccountant.Text);
			Dict.Add("EGNMainConsult", this.bugBoxEGNMainConsult.Text);
			Dict.Add("EGNTRZ", this.bugBoxEGNTRZ.Text);
            Dict.Add("AdditionalInfo", this.textBoxAdditionalInfo.Text);
            Dict.Add("ModifiedByUser", this.formmain.User);
			Dict.Add("Bulstat", this.textBoxBulstat.Text);
			Dict.Add("TaxNum", this.textBoxTaxNum.Text);
			
			if( this.comboBoxNKIDCode.SelectedIndex == -1 )
			{
				Dict.Add("NKIDCode", "not selected");
			}
			else
			{
				DataRowView rowv = (DataRowView) this.comboBoxNKIDCode.SelectedItem;
				Dict.Add("NKIDCode", rowv.Row["code"].ToString());
			}

			if( this.comboBoxNKIDLevel.SelectedIndex == -1 )    //Firm Town
			{
				Dict.Add("NKIDLevel", "not selected");
			}
			else
			{
				DataRowView rowv = (DataRowView) this.comboBoxNKIDLevel.SelectedItem;
				Dict.Add("NKIDLevel", rowv.Row["level"].ToString());
			}
		}

		private void buttonCacncel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

//		private void comboBoxRegion_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			this.comboBoxTown.Items.Clear();
//			foreach( City city in this.formmain.nomenclaatureData.arrCity)
//			{
//				if(city.code == this.comboBoxRegion.SelectedIndex)
//				{
//					this.comboBoxTown.Items.Add(city.Name);					
//				}
//			}		
//		}

	}
}
