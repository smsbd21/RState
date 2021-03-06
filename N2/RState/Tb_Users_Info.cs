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
    using System.ComponentModel.DataAnnotations;

    public partial class Tb_Users_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tb_Users_Info()
        {
            this.Tb_User_Pictures = new HashSet<Tb_User_Pictures>();
            this.Tb_Bookings = new HashSet<Tb_Bookings>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string CellNo { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public decimal Share { get; set; }
        public bool Status { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
        [DataType(DataType.Password)]
        public string HashPwd { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tb_User_Pictures> Tb_User_Pictures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tb_Bookings> Tb_Bookings { get; set; }
    }
}
