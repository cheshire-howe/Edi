using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.AsnModels
{
    public class AsnContext : DbContext
    {
        public AsnContext()
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<AsnContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AsnContext>());
        }

        public DbSet<Asn> Asns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Asn");

            modelBuilder.Entity<AsnEnvelope>()
                .HasRequired(x => x.Asn)
                .WithRequiredPrincipal(x => x.AsnEnvelope);

            modelBuilder.Entity<Asn>()
                .HasRequired(x => x.Shipment)
                .WithRequiredPrincipal(x => x.Asn);

            base.OnModelCreating(modelBuilder);
        }
    }
}
