﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.AcknowledgmentModels
{
    public class AcknowledgmentContext : DbContext
    {
        public AcknowledgmentContext()
        {
            //Database.SetInitializer<AcknowledgmentContext>(new CreateDatabaseIfNotExists<AcknowledgmentContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AcknowledgmentContext>());
        }

        public DbSet<Acknowledgment> Acknowledgments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Acknowledgment");

            modelBuilder.Entity<AckEnvelope>()
                .HasRequired(x => x.Acknowledgment)
                .WithRequiredPrincipal(x => x.AckEnvelope);

            base.OnModelCreating(modelBuilder);
        }
    }
}
