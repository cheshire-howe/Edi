using System.Data.Entity;
using Edi.Models.Shared;

namespace Edi.Models.InvoiceModels
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext()
        {
            //Database.SetInitializer<InvoiceContext>(new CreateDatabaseIfNotExists<InvoiceContext>());
            Database.SetInitializer<InvoiceContext>(new DropCreateDatabaseIfModelChanges<InvoiceContext>());
            //Database.SetInitializer<InvoiceContext>(new DropCreateDatabaseAlways<InvoiceContext>());
            //Database.SetInitializer<InvoiceContext>(new SchoolDBInitializer());
        }

        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Invoice");

            modelBuilder.Entity<InvoiceName>()
                .ToTable("Names");

            modelBuilder.Entity<InvoiceRef>()
                .ToTable("Refs");

            modelBuilder.Entity<InvoiceItem>()
                .ToTable("Items");

            modelBuilder.Entity<InvoiceNote>()
                .ToTable("Notes");

            modelBuilder.Entity<Invoice>()
                .HasRequired(x => x.InvoiceEnvelope)
                .WithRequiredPrincipal(x => x.Invoice);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.TDS01_Amount)
                .HasColumnType("Money");

            modelBuilder.Entity<InvoiceItem>()
                .Property(i => i.IT104_UnitPrice)
                .HasColumnType("Money");

            base.OnModelCreating(modelBuilder);
        }
    }
}
