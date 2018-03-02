using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Data.Common;

namespace DataLayer
{
	public enum TableEnum
	{
		eAbsence = 1,
		eAdminInfo,
		eAttestations,
		eEducation,
		eEducationNomenklature,
		eEducations,
		eEkda,
		eFired,
		eFirmPersonal3,
		eFirmStructure,
		eGlobalPositions,
		eJoinNomenklature,
		eLanguageLevel,
		eMilitaryRangs,
		eNewTree2,
		eNKID,
		eNKP,
		eNotesTable,
		eOptions,
		ePenalty,
		ePerson,
		ePictures,
		ePersonAssignment,
		eReasonAssignment,
		eStructureHistory,
		eUsers,
		eWorkTime,
		eYear,
		eYearHoliday,
		eCards,
		eEmpty,
	};

    public class TableNames
    {
		private static string prefix;
		public static string Prefix
		{
			set
			{
				prefix = value;
			}
			get
			{
				return prefix;
			}
		}

		public static string absence = "absence";
		public static string Absence
		{
			get
			{
				return prefix + absence;
			}
		}
		public static string adminInfo = "admininfo";
		public static string AdminInfo
		{
			get
			{
				return prefix + adminInfo;
			}
		}
		public static string attachedDocuments = "attachedDocs";
		public static string AttachedDocuments
		{
			get
			{
				return prefix + attachedDocuments;
			}
		}
		public static string attestations = "attestations";
		public static string Attestations
		{
			get
			{
				return prefix + attestations;
			}
		}
		public static string education = "education";
		public static string Education
		{
			get
			{
				return prefix + education;
			}
		}
		public static string educationNomenklature = "educationnomenklature";
		public static string EducationNomenklature
		{
			get
			{
				return prefix + educationNomenklature;
			}
		}
		public static string educations = "educations";
		public static string Educations
		{
			get
			{
				return prefix + educations;
			}
		}
		public static string ekda = "ekda";
		public static string Ekda
		{
			get
			{
				return prefix + ekda;
			}
		}
		public static string ekdapaylevels = "ekdapaylevels";
		public static string EkdaPayLevels
		{
			get
			{
				return prefix + ekdapaylevels;
			}
		}
		public static string fired = "fired";
		public static string Fired
		{
			get
			{
				return prefix + fired;
			}
		}
		public static string firmPersonal3 = "firmpersonal3";
		public static string FirmPersonal3
		{
			get
			{
				return prefix + firmPersonal3;
			}
		}
		public static string firmStructure = "firmstructure";
		public static string FirmStructure
		{
			get
			{
				return prefix + firmStructure;
			}
		}

		public static string cards = "cards";
		public static string Cards
		{
			get
			{
				return prefix + cards;
			}
		}

		public static string globalPositions = "globalpositions";
		public static string GlobalPositions
		{
			get
			{
				return prefix + globalPositions;
			}
		}
		public static string joinNomenklature = "joinnomenklature";
		public static string JoinNomenklature
		{
			get
			{
				return prefix + joinNomenklature;
			}
		}
		public static string languageLevel = "languagelevel";
		public static string LanguageLevel
		{
			get
			{
				return prefix + languageLevel;
			}
		}
		public static string newTree2 = "newtree2";
		public static string NewTree2
		{
			get
			{
				return prefix + newTree2;
			}
		}
		public static string nKID = "nkid";
		public static string NKID
		{
			get
			{
				return prefix + nKID;
			}
		}
		public static string nKP = "nkp";
		public static string NKP
		{
			get
			{
				return prefix + nKP;
			}
		}
		public static string notesTable = "notestable";
		public static string NotesTable
		{
			get
			{
				return prefix + notesTable;
			}
		}
		public static string options = "options";
		public static string Options
		{
			get
			{
				return prefix + options;
			}
		}
		public static string penalty = "penalty";
		public static string Penalty
		{
			get
			{
				return prefix + penalty;
			}
		}
		public static string person = "person";
		public static string Person
		{
			get
			{
				return prefix + person;
			}
		}
		public static string pictures = "pictures";
		public static string Pictures
		{
			get
			{
				return prefix + pictures;
			}
		}
		public static string personAssignment = "personassignment";
		public static string PersonAssignment
		{
			get
			{
				return prefix + personAssignment;
			}
		}
		public static string reasonAssignment = "reasonassignment";
		public static string ReasonAssignment
		{
			get
			{
				return prefix + reasonAssignment;
			}
		}
		public static string structureHistory = "structurehistory";
		public static string StructureHistory
		{
			get
			{
				return prefix + structureHistory;
			}
		}
		public static string users = "users";
		public static string Users
		{
			get
			{
				return prefix + users;
			}
		}
		public static string workTime = "worktime";
		public static string WorkTime
		{
			get
			{
				return prefix + workTime;
			}
		}
		public static string year = "year";
		public static string Year
		{
			get
			{
				return prefix + year;
			}
		}
		public static string yearHoliday = "year_holiday";
		public static string YearHoliday
		{
			get
			{
				return prefix + yearHoliday;
			}
		}

		public static string militaryRang = "militaryrangs";
		public static string MilitaryRang
		{
			get
			{
				return prefix + militaryRang;
			}
		}

		public static TableEnum Compare(string TableName)
		{
			TableEnum e;
			TableName = TableName.Remove(0, prefix.Length); //remove the prefix
			switch (TableName)
			{
				case "absence":
					e = TableEnum.eAbsence;
					break;
				case "admininfo":
					e = TableEnum.eAdminInfo;
					break;
				case "attestations":
					e = TableEnum.eAttestations;
					break;
				case "education":
					e = TableEnum.eEducation;
					break;
				case "educationnomenklature":
					e = TableEnum.eEducationNomenklature;
					break;
				case "educations":
					e = TableEnum.eEducations;
					break;
				case "ekda":
					e = TableEnum.eEkda;
					break;
				case "fired":
					e = TableEnum.eFired;
					break;
				case "firmpersonal3":
					e = TableEnum.eFirmPersonal3;
					break;
				case "firmstructure":
					e = TableEnum.eFirmStructure;
					break;
				case "globalpositions":
					e = TableEnum.eGlobalPositions;
					break;
				case "joinnomenklature":
					e = TableEnum.eJoinNomenklature;
					break;
				case "languagelevel":
					e = TableEnum.eLanguageLevel;
					break;
				case "newtree2":
					e = TableEnum.eNewTree2;
					break;
				case "nkid":
					e = TableEnum.eNKID;
					break;
				case "nkp":
					e = TableEnum.eNKP;
					break;
				case "notestable":
					e = TableEnum.eNotesTable;
					break;
				case "options":
					e = TableEnum.eOptions;
					break;
				case "penalty":
					e = TableEnum.ePenalty;
					break;
				case "person":
					e = TableEnum.ePerson;
					break;
				case "pictures":
					e = TableEnum.ePictures;
					break;
				case "personassignment":
					e = TableEnum.ePersonAssignment;
					break;
				case "reasonassignment":
					e = TableEnum.eReasonAssignment;
					break;
				case "structurehistory":
					e = TableEnum.eStructureHistory;
					break;
				case "users":
					e = TableEnum.eUsers;
					break;
				case "worktime":
					e = TableEnum.eWorkTime;
					break;
				case "year":
					e = TableEnum.eYear;
					break;
				case "year_holiday":
					e = TableEnum.eYearHoliday;
					break;
				case "militaryrangs":
					e = TableEnum.eMilitaryRangs;
					break;
				case "cards":
					e = TableEnum.eCards;
					break;
				default:
					e = TableEnum.eEmpty;
					break;
			}
			return e;
		}

		
		//public const string Absence = "absence";
		//public const string AdminInfo = "admininfo";
		//public const string Attestations = "attestations";
		//public const string Education = "education";
		//public const string EducationNomenklature = "educationnomenklature";
		//public const string Educations = "educations";
		//public const string Ekda = "ekda";
		//public const string Fired = "fired";
		//public const string FirmPersonal3 = "firmpersonal3";
		//public const string FirmStructure = "firmstructure";
		//public const string GlobalPositions = "globalpositions";
		//public const string JoinNomenklature = "joinnomenklature";
		//public const string LanguageLevel = "languagelevel";
		//public const string NewTree2 = "newtree2";
		//public const string NKID = "nkid";
		//public const string NKP = "nkp";
		//public const string NotesTable = "notestable";
		//public const string Options = "options";
		//public const string Penalty = "penalty";
		//public const string Person = "person";
		//public const string Pictures = "pictures";
		//public const string PersonAssignment = "personassignment";
		//public const string ReasonAssignment = "reasonassignment";
		//public const string StructureHistory = "structurehistory";
		//public const string Users = "users";
		//public const string WorkTime = "worktime";
		//public const string Year = "year";
		//public const string YearHoliday = "year_holiday";
    }
}
