using System;
using System.Data;
using System.Collections;
using DataLayer;
namespace HR
{
	/// <summary>
	/// Този клас се записват всички номенклатурно данни
	/// </summary>
	public class NomeclatureData
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrScienceTitle;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrScienceLevel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrLanguages;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrReasonFired;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrBaseReason;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrDirection;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrTeam;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrFamilyStatus;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrSex;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrContract;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrNKPCode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrNKPlevel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrNKIDCode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrNKIDlevel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrNKDSCode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrNKDSlevel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrLaw;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrRang;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrExperience;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrYearlyAddon;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrPenaltyReason;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrBonusReason;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrTypePenalty;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrTypeBonus;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrNKPClass;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrLanguageKnowledge;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrMilitaryStatus;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrEKDAType;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public ArrayList arrPersonOrder;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public ArrayList arrSpecialSkills;
		/// <summary>
		/// 
		/// </summary>
		public ArrayList arrNatoDegree;
		/// <summary>
		/// 
		/// </summary>
		public ArrayList arrNatoDegreeEng;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dtReasonAssignment;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		//public string[] FirmStructure;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dtAdminTable;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dtTreeTable;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dtPositionTable;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dtWorkTime;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dtOptions;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public DataTable dtEducation;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dtMilitaryRang;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public DataTable dtMilitaryDegree;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        public DataTable dtYear;
		//ctor
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public NomeclatureData()
		{
			dtAdminTable = new DataTable();
            dtYear = new DataTable();
			arrScienceTitle = new ArrayList();
			arrScienceTitle.Add("");
			arrScienceLevel = new ArrayList();
			arrScienceLevel.Add("");			
			arrLanguages = new ArrayList();
			arrLanguages.Add("");
			arrReasonFired = new ArrayList();
			arrReasonFired.Add("");
			arrBaseReason = new ArrayList();
			arrBaseReason.Add("");
			arrDirection = new ArrayList();
			arrControl = new ArrayList();
			arrTeam = new ArrayList();
			arrFamilyStatus = new ArrayList();
			arrFamilyStatus.Add("");
			arrSex = new ArrayList();
			arrSex.Add("");
			arrContract = new ArrayList();
			arrContract.Add("");
			arrNKDSCode = new ArrayList();
			arrNKDSCode.Add("");
			arrNKDSlevel = new ArrayList();
			arrNKDSlevel.Add("");
			arrNKIDCode = new ArrayList();
			arrNKIDCode.Add("");
			arrNKIDlevel = new ArrayList();
			arrNKIDlevel.Add("");
			arrNKPCode = new ArrayList();
			arrNKPCode.Add("");
			arrNKPlevel = new ArrayList();
			arrNKPlevel.Add("");
			arrExperience = new ArrayList();
			arrExperience.Add("");
			arrLaw = new ArrayList();
			arrLaw.Add("");
			arrRang = new ArrayList();
			arrRang.Add("");
			arrYearlyAddon = new ArrayList();
			arrYearlyAddon.Add("");
			arrPenaltyReason = new ArrayList();
			arrPenaltyReason.Add("");
			arrBonusReason = new ArrayList();
			arrBonusReason.Add("");
			arrTypePenalty = new ArrayList();
			arrTypePenalty.Add("");
			arrTypeBonus = new ArrayList();
			arrTypeBonus.Add("");
			arrNKPClass = new ArrayList();
			arrNKPClass.Add("");
			arrLanguageKnowledge = new ArrayList();
			arrLanguageKnowledge.Add("");
			arrMilitaryStatus = new ArrayList();
			arrMilitaryStatus.Add("");
			arrSpecialSkills = new ArrayList();
			arrSpecialSkills.Add("");
			arrNatoDegree = new ArrayList();
			arrNatoDegree.Add("");
			arrNatoDegreeEng = new ArrayList();
			arrNatoDegreeEng.Add("");

			arrEKDAType = new ArrayList();
			arrEKDAType.Add("");
			arrEKDAType.Add("А - Ръководни длъжности");
			arrEKDAType.Add("Б - Експертни длъжности с аналитични и/или контролни функции");
			arrEKDAType.Add("В - Експертни длъжности със спомагателни функции");
			arrEKDAType.Add("Г - Технически длъжности");

            arrPersonOrder = new ArrayList();
            arrPersonOrder.Add("");
            arrPersonOrder.Add("name");
            arrPersonOrder.Add("egn");

			arrContract = new ArrayList();
			//arrContract.Add("Безсрочен");
			//arrContract.Add("Безсрочен със срок на изпитване");
			//arrContract.Add("Срочен");
			//arrContract.Add("Срочен със срок на изпитване");

            arrSex.Add("Мъж");
            arrSex.Add("Жена");			
		}
	}

	/// <summary>
	/// Required designer variable.
	/// </summary>
	public struct MappingFormData
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string HeaderText;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string MappingName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string ColumnText;
	}


	/// <summary>
	/// Required designer variable.
	/// </summary>
	public struct City
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string Name;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string Prefix;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public int code;
	}

//	public struct Countrys
//	{
//		public string Code;
//		public string CountryName;
//	}
	/// <summary>
	/// Required designer variable.
	/// </summary>
	public struct Nodes
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string ID;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string Parent;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string NodeName;
	}
	/// <summary>
	/// Required designer variable.
	/// </summary>
	public struct NodeIDs
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string ID;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public string Parent;
	}
}

	

