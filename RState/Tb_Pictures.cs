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
    
    public partial class Tb_Pictures
    {
        public int Id { get; set; }
        public int PropId { get; set; }
        public string PicUrl { get; set; }
    
        public virtual Tb_Properties Tb_Properties { get; set; }
    }
}
