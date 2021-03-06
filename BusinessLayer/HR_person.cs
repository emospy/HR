//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class HR_person
    {
        public HR_person()
        {
            this.HR_Absence = new HashSet<HR_Absence>();
            this.HR_Educations = new HashSet<HR_Educations>();
            this.HR_Fired = new HashSet<HR_Fired>();
            this.HR_LanguageLevel = new HashSet<HR_LanguageLevel>();
            this.HR_Notes = new HashSet<HR_Notes>();
            this.HR_NotesTable = new HashSet<HR_NotesTable>();
            this.HR_Penalty = new HashSet<HR_Penalty>();
            this.HR_PersonAssignment = new HashSet<HR_PersonAssignment>();
            this.HR_PlannedHolidays = new HashSet<HR_PlannedHolidays>();
            this.HR_Year_Holiday = new HashSet<HR_Year_Holiday>();
        }
    
        public int id { get; set; }
        public string egn { get; set; }
        public string name { get; set; }
        public string bornCountry { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string town { get; set; }
        public string kwartal { get; set; }
        public string street { get; set; }
        public string numBlockHouse { get; set; }
        public string phone { get; set; }
        public string pcard { get; set; }
        public Nullable<System.DateTime> pcardPublish { get; set; }
        public string publishedBy { get; set; }
        public string familyStatus { get; set; }
        public string education { get; set; }
        public string diplomDate { get; set; }
        public string profession { get; set; }
        public string languages { get; set; }
        public string scienceTitle { get; set; }
        public string scienceLevel { get; set; }
        public string militaryRang { get; set; }
        public string militaryStatus { get; set; }
        public string category { get; set; }
        public Nullable<System.DateTime> hiredAt { get; set; }
        public string workExperience { get; set; }
        public string sex { get; set; }
        public Nullable<int> fired { get; set; }
        public string borntown { get; set; }
        public string modifiedByUser { get; set; }
        public Nullable<int> nodeID { get; set; }
        public Nullable<System.DateTime> bornDate { get; set; }
        public string Speciality { get; set; }
        public string ReceivedAddon { get; set; }
        public string Rang { get; set; }
        public string Other { get; set; }
        public Nullable<int> egnlnch { get; set; }
        public string engname { get; set; }
        public string engeducation { get; set; }
        public Nullable<int> other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string other4 { get; set; }
        public string other5 { get; set; }
        public Nullable<int> positionID { get; set; }
        public string workbook { get; set; }
        public Nullable<System.DateTime> workbookdate { get; set; }
        public Nullable<int> id_sysco { get; set; }
        public string password { get; set; }
        public int id_roletype { get; set; }
        public Nullable<System.DateTime> pcardExpiry { get; set; }
        public bool IsSecondary { get; set; }
    
        public virtual ICollection<HR_Absence> HR_Absence { get; set; }
        public virtual ICollection<HR_Educations> HR_Educations { get; set; }
        public virtual ICollection<HR_Fired> HR_Fired { get; set; }
        public virtual ICollection<HR_LanguageLevel> HR_LanguageLevel { get; set; }
        public virtual ICollection<HR_Notes> HR_Notes { get; set; }
        public virtual ICollection<HR_NotesTable> HR_NotesTable { get; set; }
        public virtual ICollection<HR_Penalty> HR_Penalty { get; set; }
        public virtual ICollection<HR_PersonAssignment> HR_PersonAssignment { get; set; }
        public virtual HR_Pictures HR_Pictures { get; set; }
        public virtual ICollection<HR_PlannedHolidays> HR_PlannedHolidays { get; set; }
        public virtual ICollection<HR_Year_Holiday> HR_Year_Holiday { get; set; }
    }
}
