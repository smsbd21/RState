//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RState
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tb_Properties
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tb_Properties()
        {
            this.Tb_Bookings = new HashSet<Tb_Bookings>();
            this.Tb_Pictures = new HashSet<Tb_Pictures>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int NoShare { get; set; }
        public System.DateTime ExpDate { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string Criteria { get; set; }
        public Nullable<System.DateTime> OfferStart { get; set; }
        public Nullable<System.DateTime> OfferEnd { get; set; }
        public string Notes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tb_Bookings> Tb_Bookings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tb_Pictures> Tb_Pictures { get; set; }
    }
}
