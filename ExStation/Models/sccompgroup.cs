//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExStation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sccompgroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sccompgroup()
        {
            this.scacclv = new HashSet<scacclv>();
            this.sccomp = new HashSet<sccomp>();
        }
    
        public int id { get; set; }
        public string groupname { get; set; }
        public System.DateTime credate { get; set; }
        public Nullable<System.DateTime> chgdate { get; set; }
        public int member_id { get; set; }
    
        public virtual member member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<scacclv> scacclv { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sccomp> sccomp { get; set; }
    }
}