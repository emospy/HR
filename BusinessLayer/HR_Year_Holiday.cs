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
    
    public partial class HR_Year_Holiday
    {
        public Nullable<int> parent { get; set; }
        public Nullable<int> year { get; set; }
        public Nullable<int> leftover { get; set; }
        public Nullable<int> total { get; set; }
        public int id { get; set; }
        public Nullable<int> telk { get; set; }
        public Nullable<int> Unpayed { get; set; }
        public Nullable<int> Education { get; set; }
        public Nullable<int> Additional { get; set; }
    
        public virtual HR_person HR_person { get; set; }
    }
}