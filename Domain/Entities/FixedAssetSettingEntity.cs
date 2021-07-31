using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FixedAssetSettingEntity
    {
        public Guid Id { get; set; }
        public Guid DefaultCostCenterId { get; set; }
        public Guid DefaultVoucherTypeId { get; set; }
        public Guid GainOnDisposalAccountId { get; set; }
        public Guid LossOnDisposalAccountId { get; set; }
        public Guid CashAccountId { get; set; }

        public FixedAssetSettingEntity()
        {
        }

        public FixedAssetSettingEntity(IfmsFixedAssetSetting ifmsFixedAsset)
        {
            if (ifmsFixedAsset == null) return;
                Id = ifmsFixedAsset.Id;
                DefaultCostCenterId = ifmsFixedAsset.DefaultCostCenterId;
                DefaultVoucherTypeId = ifmsFixedAsset.VoucherTypeId;
                GainOnDisposalAccountId = ifmsFixedAsset.GainOnDisposalAccountId;
                LossOnDisposalAccountId = ifmsFixedAsset.LossOnDisposalAccountId;
                CashAccountId = ifmsFixedAsset.CashAccountId;

        }

        public IfmsFixedAssetSetting MapToModel()
        {
            IfmsFixedAssetSetting ifmsfixed = new IfmsFixedAssetSetting
            {
                //Id = this.Id,
                DefaultCostCenterId = this.DefaultCostCenterId,
                VoucherTypeId = this.DefaultVoucherTypeId,
                GainOnDisposalAccountId = this.GainOnDisposalAccountId,
                LossOnDisposalAccountId = this.LossOnDisposalAccountId,
                CashAccountId = this.CashAccountId,               
            };

            return ifmsfixed;
        }

        public IfmsFixedAssetSetting MapToModel(IfmsFixedAssetSetting ifmsFixed)
        {
            //Id = this.Id,
            ifmsFixed.DefaultCostCenterId = DefaultCostCenterId;
            ifmsFixed.VoucherTypeId = DefaultVoucherTypeId;
            ifmsFixed.GainOnDisposalAccountId = GainOnDisposalAccountId;
            ifmsFixed.LossOnDisposalAccountId = LossOnDisposalAccountId;
            ifmsFixed.DefaultCostCenterId = DefaultCostCenterId;
            ifmsFixed.CashAccountId = CashAccountId;            

            return ifmsFixed;
        }
    }
}
