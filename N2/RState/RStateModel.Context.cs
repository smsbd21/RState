﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DbCon : DbContext
    {
        public DbCon()
            : base("name=DbCon")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tb_Pictures> Tb_Pictures { get; set; }
        public virtual DbSet<Tb_Prop_Pictures> Tb_Prop_Pictures { get; set; }
        public virtual DbSet<Tb_Properties> Tb_Properties { get; set; }
        public virtual DbSet<Tb_User_Pictures> Tb_User_Pictures { get; set; }
        public virtual DbSet<Tb_Users_Info> Tb_Users_Info { get; set; }
        public virtual DbSet<Tb_Bookings> Tb_Bookings { get; set; }
    
        public virtual ObjectResult<sp_getLogin_Result> sp_getLogin(string usr, string pwd)
        {
            var usrParameter = usr != null ?
                new ObjectParameter("usr", usr) :
                new ObjectParameter("usr", typeof(string));
    
            var pwdParameter = pwd != null ?
                new ObjectParameter("pwd", pwd) :
                new ObjectParameter("pwd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getLogin_Result>("sp_getLogin", usrParameter, pwdParameter);
        }
    }
}