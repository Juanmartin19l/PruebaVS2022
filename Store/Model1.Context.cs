﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Store
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database1Entities : DbContext
    {
        public Database1Entities()
            : base("name=Database1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<brands> brands { get; set; }
        public virtual DbSet<categories> categories { get; set; }
        public virtual DbSet<products> products { get; set; }
        public virtual DbSet<stocks> stocks { get; set; }
        public virtual DbSet<customers> customers { get; set; }
        public virtual DbSet<order_items> order_items { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<staffs> staffs { get; set; }
        public virtual DbSet<stores> stores { get; set; }
    }
}
