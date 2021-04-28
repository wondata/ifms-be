using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.PostModel
{
    public class FixedAssetPostModle
    {

        public int Id { get; set; }
        public int DefaultCostCenterId { get; set; }
        public int VoucherTypeId { get; set; }
        public int GainOnDisposalAccountId { get; set; }
        public int LossOnDisposalAccountId { get; set; }
        public int CashAccountId { get; set; }

        public FixedAssetPostModle()
        {
        }

        public FixedAssetSettingEntity MapToEntity()
        {

            FixedAssetSettingEntity fixedEntity = new FixedAssetSettingEntity
            {                
            };

            return fixedEntity;
        }
    }
}
