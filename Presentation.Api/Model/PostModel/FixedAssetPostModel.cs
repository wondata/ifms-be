using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.PostModel
{
    public class FixedAssetPostModel
    {

        public int Id { get; set; }
        public string DefaultCostCenterId { get; set; }
        public string DefualtVoucherTypeId { get; set; }
        public string GainOnDisposalAccountId { get; set; }
        public string LossOnDisposalAccountId { get; set; }
        public string CashAccountId { get; set; }

        public FixedAssetPostModel()
        {
        }

        public FixedAssetSettingEntity MapToEntity()
        {
            Guid DefaultCostCenterIds;
            Guid.TryParse(this.DefaultCostCenterId, out DefaultCostCenterIds);
            Guid DefualtVoucherTypeIds;
            Guid.TryParse(this.DefualtVoucherTypeId, out DefualtVoucherTypeIds);
            Guid GainOnDisposalAccountIds;
            Guid.TryParse(this.GainOnDisposalAccountId, out GainOnDisposalAccountIds);
            Guid LossOnDisposalAccountIds;
            Guid.TryParse(this.LossOnDisposalAccountId, out LossOnDisposalAccountIds);
            Guid CashAccountIds;
            Guid.TryParse(this.CashAccountId, out CashAccountIds);

            FixedAssetSettingEntity fixedEntity = new FixedAssetSettingEntity
            {  
                DefaultCostCenterId = DefaultCostCenterIds,
                DefaultVoucherTypeId = DefualtVoucherTypeIds,
                GainOnDisposalAccountId = GainOnDisposalAccountIds,
                LossOnDisposalAccountId = LossOnDisposalAccountIds,
                CashAccountId = CashAccountIds
            };

            return fixedEntity;
        }
    }
}
