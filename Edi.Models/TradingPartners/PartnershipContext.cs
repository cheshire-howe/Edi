using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.TradingPartners
{
    public class PartnershipContext : DbContext
    {
        public PartnershipContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PartnershipContext>());
        }

        public DbSet<Partnership> Partnerships { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TradingPartners");

            modelBuilder.Entity<Partnership>()
                .HasRequired<Vendor>(x => x.Vendor)
                .WithMany(x => x.Partnerships)
                .HasForeignKey(x => x.VendorID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
