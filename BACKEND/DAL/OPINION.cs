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
    
    public partial class OPINION
    {
        public int OpinionCode { get; set; }
        public int LandingCode { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Comment { get; set; }
    
        public virtual LENDING LENDING { get; set; }
    }
}
