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
    
    public partial class HR_MessageInstances
    {
        public HR_MessageInstances()
        {
            this.HR_Messages = new HashSet<HR_Messages>();
        }
    
        public int id_messageInstance { get; set; }
        public int id_messageType { get; set; }
        public Nullable<System.DateTime> FixedDate { get; set; }
        public Nullable<int> WarningDays { get; set; }
        public Nullable<int> AlarmDays { get; set; }
        public Nullable<int> id_department { get; set; }
    
        public virtual HR_MessageTypes HR_MessageTypes { get; set; }
        public virtual ICollection<HR_Messages> HR_Messages { get; set; }
    }
}
