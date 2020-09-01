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
    
    public partial class LENDING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LENDING()
        {
            this.OPINIONS = new HashSet<OPINION>();
        }
    
        public int LendingCode { get; set; }
        public int UserCode { get; set; }
        public int ProductCode { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<System.DateTime> LendingDate { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual USER USER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPINION> OPINIONS { get; set; }
    }
}
