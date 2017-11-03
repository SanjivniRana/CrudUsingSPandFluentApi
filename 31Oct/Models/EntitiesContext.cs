using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _31Oct.Models
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext() : base("name=Entities")
        {

        }

        public DbSet<CState> CStates { get; set; }
        public DbSet<CCity> CCities { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomrVM>().Map(e =>
            {
                e.Properties(p => new { p.Name, p.Email });
                e.ToTable("Customrs");
            }).Map(e =>
            {
                e.Properties(p => new { p.Address });
                e.ToTable("CAddress");
            }).Map(e =>
            {
                e.Properties(p => new { p.State });
                e.ToTable("CStates");
            }).Map(e =>
            {
                e.Properties(p => new { p.City });
                e.ToTable("CCities");
            }).MapToStoredProcedures();
        }

        public System.Data.Entity.DbSet<_31Oct.Models.CustomrVM> CustomrVMs { get; set; }
    }
}