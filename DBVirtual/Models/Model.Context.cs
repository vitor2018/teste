﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBVirtual.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LocalizaAmigosEntities : DbContext
    {
        public LocalizaAmigosEntities()
            : base("name=LocalizaAmigosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Amigos> Amigos { get; set; }
        public virtual DbSet<CalculoHistoricoLog> CalculoHistoricoLog { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
