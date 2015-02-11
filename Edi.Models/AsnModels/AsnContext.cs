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
                .HasKey(x => x.AsnID);

            modelBuilder.Entity<Asn>()
                .HasRequired(x => x.AsnEnvelope)
                .WithRequiredPrincipal(x => x.Asn);

            base.OnModelCreating(modelBuilder);
        }
    }
}
