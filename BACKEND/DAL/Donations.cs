//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Donations
    {
        public int donationCode { get; set; }
        public string donationName { get; set; }
        public int Category { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string donorName { get; set; }
        public string donorEmail { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public Nullable<int> ProductCode { get; set; }
    
        public virtual CategoryGMH CategoryGMH { get; set; }
        public virtual Products Products { get; set; }
    }
}
