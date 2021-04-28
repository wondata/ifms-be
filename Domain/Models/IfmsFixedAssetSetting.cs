using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsFixedAssetSetting
    {
        public IfmsFixedAssetSetting()
        {            

        }

        public Guid Id { get; set; }
        public Guid DefaultCostCenterId { get; set; }
        public Guid VoucherTypeId { get; set; }
        public Guid GainOnDisposalAccountId { get; set; }
        public Guid LossOnDisposalAccountId { get; set; }
        public Guid CashAccountId { get; set; }      

        public virtual CoreCostCenter CoreCostCenters { get; set; }
        public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts  { get; set; }
        public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts_1 { get; set; }
        public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts_2 { get; set; }

        public virtual LupVoucherType LupVoucherTypes { get; set; }
    }
}
