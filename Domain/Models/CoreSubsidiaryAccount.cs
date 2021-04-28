using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class CoreSubsidiaryAccount
    {
        public CoreSubsidiaryAccount()
        {
            IfmsVoucherDetails = new HashSet<IfmsVoucherDetail>();
            IfmsVoucherDetailHistorys = new HashSet<IfmsVoucherDetailHistory>();
            IfmsSettings = new HashSet<IfmsSetting>();
            IfmsSettings_2 = new HashSet<IfmsSetting>();
            IfmsFixedAssetSettings = new HashSet<IfmsFixedAssetSetting>();
            IfmsFixedAssetSettings_1 = new HashSet<IfmsFixedAssetSetting>();
            IfmsFixedAssetSettings_2 = new HashSet<IfmsFixedAssetSetting>();

            IfmsVoucherTypeSettings = new HashSet<IfmsVoucherTypeSetting>();
        }

        public Guid Id { get; set; }
        public Guid ControlAccountId { get; set; }
        public Guid BalanceSideId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CreatedBy { get; set; }
        public double RunningBalance { get; set; }
        public Guid SubsidiaryAccountTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual LupBalanceSide BalanceSide { get; set; }
        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }
        public virtual ICollection<IfmsVoucherDetailHistory> IfmsVoucherDetailHistorys { get; set; }
        public virtual ICollection<IfmsSetting> IfmsSettings { get; set; }
        public virtual ICollection<IfmsSetting> IfmsSettings_2 { get; set; }
        public virtual ICollection<IfmsFixedAssetSetting> IfmsFixedAssetSettings { get; set; }
        public virtual ICollection<IfmsFixedAssetSetting> IfmsFixedAssetSettings_1 { get; set; }
        public virtual ICollection<IfmsFixedAssetSetting> IfmsFixedAssetSettings_2 { get; set; }
        public virtual CoreControlAccount CoreControlAccounts { get; set; }
        public virtual ICollection<IfmsVoucherTypeSetting> IfmsVoucherTypeSettings { get; set; }
    }
}
