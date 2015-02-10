using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.PurchaseOrderModels
{
    public class PurchaseOrderContext : DbContext
    {
        public PurchaseOrderContext()
        {
            //Database.SetInitializer<PurchaseOrderContext>(new CreateDatabaseIfNotExists<PurchaseOrderContext>());
            Database.SetInitializer<PurchaseOrderContext>(new DropCreateDatabaseIfModelChanges<PurchaseOrderContext>());
            //Database.SetInitializer<PurchaseOrderContext>(new DropCreateDatabaseAlways<PurchaseOrderContext>());
            //Database.SetInitializer<PurchaseOrderContext>(new DBInitializer());
        }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PurchaseOrders");

            modelBuilder.Entity<PoEnvelope>()
                .HasKey(x => x.PurchaseOrderID);

            modelBuilder.Entity<PurchaseOrder>()
                .HasRequired(x => x.PoEnvelope)
                .WithRequiredPrincipal(x => x.PurchaseOrder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
