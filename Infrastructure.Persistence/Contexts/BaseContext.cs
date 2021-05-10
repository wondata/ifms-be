using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Contexts
{
    public class BaseContext : DbContext
    {
        public BaseContext()
        {

        }

        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }
        public virtual DbSet<CoreAccountGroup> CoreAccountGroup { get; set; }
        public virtual DbSet<CoreAccountType> CoreAccountType { get; set; }
        public virtual DbSet<CoreSubsidiaryAccount> CoreSubsidiaryAccount { get; set; }
        public virtual DbSet<CoreControlAccount> CoreControlAccount { get; set; }
        public virtual DbSet<LupBalanceSide> LupBalanceSide { get; set; }      
        public virtual DbSet<LupVoucherType> LupVoucherType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<CoreAccountGroup>(entity =>
            {
                entity.ToTable("coreAccountGroup");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdated)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.CoreAccountGroup)
                    .HasForeignKey(d => d.AccountTypeId)
                    .HasConstraintName("FK_coreAccountGroup_coreAccountType");
            });

            modelBuilder.Entity<CoreAccountType>(entity =>
            {
                entity.ToTable("coreAccountType");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdated)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                //entity.HasOne(d => d.BalanceSide)
                //    .WithMany(p => p.CoreAccountType)
                //    .HasForeignKey(d => d.BalanceSideId)
                //    .HasConstraintName("FK_coreAccountType_lupBalanceSide");
            });

            modelBuilder.Entity<CoreControlAccount>(entity =>
            {
                entity.ToTable("coreControlAccount");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdated)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AccountGroup)
                    .WithMany(p => p.CoreControlAccount)
                    .HasForeignKey(d => d.AccountGroupId)
                    .HasConstraintName("FK_coreControlAccount_coreAccountGroup");
            });





            modelBuilder.Entity<CoreSubsidiaryAccount>(entity =>
            {
                entity.ToTable("coreSubsidiaryAccount");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.LastUpdated)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.BalanceSide)
                    .WithMany(p => p.CoreSubsidiaryAccount)
                    .HasForeignKey(d => d.BalanceSideId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_coreSubsidiaryAccount_lupBalanceSide");

                //entity.HasOne(d => d.CoreControlAccount)
                //  .WithMany(p => p.CoreSubsidiaryAccounts)
                //  .HasForeignKey(d => d.ControlAccountId)
                //  .OnDelete(DeleteBehavior.ClientSetNull)
                //  .HasConstraintName("FK_coreSubsidiaryAccount_coreControlAccount");

            });



            modelBuilder.Entity<LupBalanceSide>(entity =>
            {
                entity.ToTable("lupBalanceSide");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdated)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            });


            }

    }
}
