using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Contexts
{
    public class FinancialContext : DbContext
    {

        public FinancialContext()
        {

        }

        public FinancialContext(DbContextOptions<FinancialContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<IfmsCostCode> IfmsCostCode { get; set; }
        public virtual DbSet<IfmsVoucherDetail> IfmsVoucherDetail { get; set; }
        public virtual DbSet<IfmsVoucherDetailHistory> IfmsVoucherDetailHistory { get; set; }
        public virtual DbSet<IfmsVoucherHeader> IfmsVoucherHeader { get; set; }
        public virtual DbSet<IfmsVoucherHeaderHistory> IfmsVoucherHeaderHistory { get; set; }
        public virtual DbSet<IfmsVoucherTypeSetting> IfmsVoucherTypeSetting { get; set; }
        public virtual DbSet<IfmsSetting> IfmsSetting { get; set; }
        public virtual DbSet<IfmsFixedAssetSetting> IfmsFixedAssetSetting { get; set; }
        public virtual DbSet<IfmsCashier> IfmsCashier { get; set; }     
        public virtual DbSet<IfmsCostCenterUser> IfmsCostCenterUser { get; set; }
        public virtual DbSet<IfmsPurposeTemplate> IfmsPurposeTemplate { get; set; }
        public virtual DbSet<LupModeOfPayment> LupModeOfPayment { get; set; }
        public virtual DbSet<CoreAccountType> CoreAccountType { get; set; }
        public virtual DbSet<CoreCostCenter> CoreCostCenter { get; set; }
        public virtual DbSet<CoreControlAccount> CoreControlAccount { get; set; }
        public virtual DbSet<PsmsPaymentRequest> PsmPaymentRequest { get; set; }
        public virtual DbSet<IfmsBankReconciliationDetail> IfmsBankReconciliationDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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

                entity.HasOne(d => d.BalanceSide)
                    .WithMany(p => p.CoreAccountTypes)
                    .HasForeignKey(d => d.BalanceSideId)
                    .HasConstraintName("FK_coreAccountType_lupBalanceSide");
            });

            modelBuilder.Entity<CoreCostCenter>(entity =>
            {
                entity.ToTable("coreCostCenter");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name);

                entity.Property(e => e.Code);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_coreCostCenter_coreCostCenter");
            });

            modelBuilder.Entity<CoreControlAccount>(entity =>
            {
                entity.ToTable("coreControlAccount");

                //  entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");


                entity.HasOne(d => d.ChartOfAccount)
                    .WithMany(p => p.CoreControlAccounts)
                    .HasForeignKey(d => d.ChartAccountID)
                    .HasConstraintName("FK_coreControlAccount_coreChartOfAccount");

            });

            modelBuilder.Entity<IfmsCostCode>(entity =>
            {
                entity.ToTable("ifmsCostCode");

                entity.Property(e => e.Name);

                entity.Property(e => e.Code);

            });

            modelBuilder.Entity<IfmsCostCenterUser>(entity =>
            {
                entity.ToTable("ifmsCostCenterUser");

                entity.HasOne(d => d.CoreCostCenter)
                      .WithMany(p => p.IfmsCostCenterUsers)
                      .HasForeignKey(d => d.CostCenterId)
                      .HasConstraintName("FK_ifmsCostCenterUser_coreCostCenter");

            });

            modelBuilder.Entity<IfmsCashier>(entity =>
            {
                entity.ToTable("ifmsCashier");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FullName);

                entity.HasOne(d => d.User)
                   .WithMany(p => p.IfmsCashiers)
                   .HasForeignKey(d => d.UserId)
                   .HasConstraintName("FK_ifmsCashier_coreUser");

                entity.HasOne(d => d.SubsidiaryAccount)
                    .WithMany(p => p.IfmsCashiers)
                    .HasForeignKey(d => d.SubsidiaryAccountId)
                    .HasConstraintName("FK_ifmsCashier_coreSubsidiaryAccount");
            });

            modelBuilder.Entity<IfmsBankReconciliationDetail>(entity =>
            {
                entity.ToTable("ifmsBankReconciliationDetail");             

                entity.HasOne(d => d.VoucherDetail)
                    .WithMany(p => p.IfmsBankReconciliationDetails)
                    .HasForeignKey(d => d.VoucherDetailId)
                    .HasConstraintName("FK_ifmsBankReconciliationDetail_ifmsVoucherDetail");
            });


            modelBuilder.Entity<IfmsVoucherHeader>(entity =>
            {
                entity.ToTable("ifmsVoucherHeader");

                entity.Property(e => e.ReferenceNo);

                entity.Property(e => e.DocumentNo);

                entity.Property(e => e.PayedToReceivedFrom);

                entity.Property(e => e.Purpose);

                entity.Property(e => e.Description);

                entity.Property(e => e.Amount);           

                entity.Property(e => e.ChequeNo);

                entity.HasOne(d => d.CostCenter)
                   .WithMany(p => p.IfmsVoucherHeaders)
                   .HasForeignKey(d => d.CostCenterId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_ifmsVoucherHeader_coreCostCenter");

                entity.HasOne(d => d.CorePeriod)
                    .WithMany(p => p.IfmsVoucherHeader)
                    .HasForeignKey(d => d.PeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ifmsVoucherHeader_corePeriod");

                entity.HasOne(d => d.VoucherType)
                    .WithMany(p => p.IfmsVoucherHeaders)
                    .HasForeignKey(d => d.VoucherTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ifmsVoucherHeader_lupVoucherType");

                entity.HasOne(d => d.ModeOfPayment)
                   .WithMany(p => p.IfmsVoucherHeaders)
                   .HasForeignKey(d => d.ModeOfPaymentId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_ifmsVoucherHeader_lupModeOfPayment");

                entity.HasOne(d => d.PurposeTemplate)
                  .WithMany(p => p.IfmsVoucherHeaders)
                  .HasForeignKey(d => d.PurposeTemplateId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ifmsVoucherHeader_ifmsPurposeTemplate");
         

            });


            modelBuilder.Entity<IfmsVoucherHeaderHistory>(entity =>
            {
                entity.ToTable("ifmsVoucherHeaderHistory");

                entity.Property(e => e.ReferenceNo);

                entity.Property(e => e.PayedToReceivedFrom);

                entity.Property(e => e.Purpose);

                entity.Property(e => e.Amount);

                entity.Property(e => e.ChequeNo);

                entity.Property(e => e.CreatedBy);


            });

            modelBuilder.Entity<IfmsVoucherDetail>(entity =>
            {
                entity.ToTable("ifmsVoucherDetail");

                entity.Property(e => e.DebitAmount);

                entity.Property(e => e.CreditAmount);

                entity.HasOne(d => d.ControlAccount)
                    .WithMany(p => p.IfmsVoucherDetails)
                    .HasForeignKey(d => d.ControlAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ifmsVoucherDetail_coreControlAccount");

                entity.HasOne(d => d.CostCenter)
                     .WithMany(p => p.IfmsVoucherDetails)
                     .HasForeignKey(d => d.CostCenterId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_ifmsVoucherDetail_coreCostCenter");

                entity.HasOne(d => d.SubsidiaryAccount)
                  .WithMany(p => p.IfmsVoucherDetails)
                  .HasForeignKey(d => d.SubsidiaryAccountId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ifmsVoucherDetail_coreSubsidiaryAccount");

                entity.HasOne(d => d.CostCode)
                     .WithMany(p => p.IfmsVoucherDetails)
                     .HasForeignKey(d => d.CostCodeId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_ifmsVoucherDetail_ifmsCostCode");

                entity.HasOne(d => d.VoucherHeaders)
                     .WithMany(p => p.IfmsVoucherDetails)
                     .HasForeignKey(d => d.VoucherHeaderId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_ifmsVoucherDetail_ifmsVoucherHeader");

                entity.HasOne(d => d.VoucherHeaders1)
                    .WithMany(p => p.IfmsVoucherDetails1)
                    .HasForeignKey(d => d.IBTReferenceVoucherHeaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ifmsVoucherDetail_ifmsVoucherHeader1");

                entity.HasOne(d => d.VoucherHeaders2)
                    .WithMany(p => p.IfmsVoucherDetails2)
                    .HasForeignKey(d => d.ReferenceVoucherHeaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ifmsVoucherDetail_ifmsVoucherHeader2");

            });

            modelBuilder.Entity<IfmsVoucherDetailHistory>(entity =>
            {
                entity.ToTable("ifmsVoucherDetailHistory");

                entity.Property(e => e.VoucherHeaderId);

                entity.Property(e => e.CostCenterId);

                entity.Property(e => e.SubsidiaryAccountId);

                entity.Property(e => e.DebitAmount);

                entity.Property(e => e.CreditAmount);

                //entity.HasOne(d => d.ControlAccounts)
                //   .WithMany(p => p.IfmsVoucherDetailHistorys)
                //   .HasForeignKey(d => d.ControlAccountId)
                //   .OnDelete(DeleteBehavior.ClientSetNull)
                //   .HasConstraintName("FK_ifmsVoucherDetailHistory_coreControlAccount");

                //entity.HasOne(d => d.CoreCostCenters)
                //  .WithMany(p => p.IfmsVoucherDetailHistorys)
                //  .HasForeignKey(d => d.CostCenterId)
                //  .OnDelete(DeleteBehavior.ClientSetNull)
                //  .HasConstraintName("FK_ifmsVoucherDetailHistory_coreCostCenter");

                //entity.HasOne(d => d.CoreSubsidiaryAccounts)
                // .WithMany(p => p.IfmsVoucherDetailHistorys)
                // .HasForeignKey(d => d.SubsidiaryAccountId)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                // .HasConstraintName("FK_ifmsVoucherDetailHistory_coreSubsidiaryAccount");

                //entity.HasOne(d => d.IfmsVoucherHeaderHistorys)
                //    .WithMany(p => p.IfmsVoucherDetailHistorys)
                //    .HasForeignKey(d => d.VoucherHeaderId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_ifmsVoucherDetailHistory_ifmsVoucherHeaderHistory");

            });


            modelBuilder.Entity<IfmsVoucherTypeSetting>(entity =>
            {
                entity.ToTable("ifmsVoucherTypeSetting");               

                entity.Property(e => e.StartingNumber);

                entity.Property(e => e.EndingNumber);

                entity.Property(e => e.CurrentNumber);

                entity.Property(e => e.NumberOfDigits);

                entity.HasOne(d => d.CoreCostCenter)
                  .WithMany(p => p.IfmsVoucherTypeSettings)
                  .HasForeignKey(d => d.CostCenterId)
                  .HasConstraintName("FK_ifmsVoucherTypeSetting_coreCostCenter");

                entity.HasOne(d => d.CoreSubsidiaryAccount)
                  .WithMany(p => p.IfmsVoucherTypeSettings)
                  .HasForeignKey(d => d.DefaultAccountId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_ifmsVoucherTypeSetting_coreSubsidiaryAccount");

                entity.HasOne(d => d.LupBalanceSide)
                    .WithMany(p => p.IfmsVoucherTypeSettings)
                    .HasForeignKey(d => d.BalanceSideId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ifmsVoucherTypeSetting_lupBalanceSide");

                entity.HasOne(d => d.LupVoucherType)
                   .WithMany(p => p.IfmsVoucherTypeSettings)
                   .HasForeignKey(d => d.VoucherTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_ifmsVoucherTypeSetting_lupVoucherType");

            });

            modelBuilder.Entity<IfmsSetting>(entity =>
            {
                entity.ToTable("ifmsSetting");

                entity.Property(e => e.CurrentFiscalYearId);

                entity.Property(e => e.CurrentPeriodId);

                entity.Property(e => e.CompanyTaxId);

                //entity.HasOne(d => d.ControlAccount)
                //  .WithMany(p => p.IfmsSettings)
                //  .HasForeignKey(d => d.InterBranchControlAccountId)
                //  .HasConstraintName("FK_ifmsSetting_coreControlAccount");

                entity.HasOne(d => d.CoreCostCenter)
                  .WithMany(p => p.IfmsSettings)
                  .HasForeignKey(d => d.DefaultCostCenterId)
                  .HasConstraintName("FK_ifmsSetting_coreCostCenter");

                //entity.HasOne(d => d.CoreSubsidiaryAccounts)
                // .WithMany(p => p.IfmsSettings)
                // .HasForeignKey(d => d.IncomeSummaryAccountId)
                // .HasConstraintName("FK_ifmsSetting_coreSubsidiaryAccount");

                //entity.HasOne(d => d.CoreSubsidiaryAccounts_2)
                // .WithMany(p => p.IfmsSettings_2)
                // .HasForeignKey(d => d.ClosingCapitalAccountId)
                // .HasConstraintName("FK_ifmsSetting_coreSubsidiaryAccount1");

            });

            modelBuilder.Entity<IfmsFixedAssetSetting>(entity =>
            {
                entity.ToTable("ifmsFixedAssetSetting");

                //entity.HasOne(d => d.CoreCostCenters)
                //  .WithMany(p => p.IfmsFixedAssetSettings)
                //  .HasForeignKey(d => d.DefaultCostCenterId)
                //  .HasConstraintName("FK_ifmsFixedAssetSetting_coreCostCenter");

                //entity.HasOne(d => d.CoreSubsidiaryAccounts)
                //  .WithMany(p => p.IfmsFixedAssetSettings)
                //  .HasForeignKey(d => d.GainOnDisposalAccountId)
                //  .HasConstraintName("FK_ifmsFixedAssetSetting_coreSubsidiaryAccount");

                //entity.HasOne(d => d.CoreSubsidiaryAccounts_1)
                // .WithMany(p => p.IfmsFixedAssetSettings_1)
                // .HasForeignKey(d => d.LossOnDisposalAccountId)
                // .HasConstraintName("FK_ifmsFixedAssetSetting_coreSubsidiaryAccount1");

                //entity.HasOne(d => d.CoreSubsidiaryAccounts_2)
                // .WithMany(p => p.IfmsFixedAssetSettings_2)
                // .HasForeignKey(d => d.CashAccountId)
                // .HasConstraintName("FK_ifmsFixedAssetSetting_coreSubsidiaryAccount2");

                //entity.HasOne(d => d.LupVoucherTypes)
                // .WithMany(p => p.IfmsFixedAssetSettings)
                // .HasForeignKey(d => d.VoucherTypeId)
                // .HasConstraintName("FK_ifmsFixedAssetSetting_lupVoucherType");



            });

            modelBuilder.Entity<IfmsPurposeTemplate>(entity =>
            {
                entity.ToTable("ifmsPurposeTemplate");

                entity.Property(e => e.Code);

                entity.Property(e => e.Purpose);

            });

            modelBuilder.Entity<PsmsPaymentRequest>(entity =>
            {
                entity.ToTable("psmsPaymentRequest");

                entity.Property(e => e.PayeeName);

                entity.Property(e => e.Priority);
                entity.Property(e => e.AttachedDocuments);
                entity.Property(e => e.Type);
                entity.Property(e => e.Remark);

            });


            modelBuilder.Entity<LupVoucherType>(entity =>
            {
                entity.ToTable("lupVoucherType");

                entity.Property(e => e.Name);

                entity.Property(e => e.Code);

                entity.Property(e => e.Description);
     

            });

            modelBuilder.Entity<LupModeOfPayment>(entity =>
            {
                entity.ToTable("lupModeOfPayment");

                entity.Property(e => e.Name);

                entity.Property(e => e.Code);                                   

            });
        }
        
    }
}
