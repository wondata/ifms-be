using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Contexts
{
    public class LookupContext : DbContext
    {
        public LookupContext()
        {

        }
        public LookupContext(DbContextOptions<LookupContext> options) : base(options)
        {

        }
        public virtual DbSet<LookupModel> Lookup { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LookupModel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

             //   entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdated)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

             //   entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            });
        }
    }
}
