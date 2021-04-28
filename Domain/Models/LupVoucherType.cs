using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class LupVoucherType
    {
        public LupVoucherType()
        {
            IfmsFixedAssetSettings = new HashSet<IfmsFixedAssetSetting>();
            IfmsVoucherTypeSettings = new HashSet<IfmsVoucherTypeSetting>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        // public bool? IsFinanceVoucher { get; set; }
        // public bool? IsDeleted { get; set; }
        // public DateTime? LastUpdated { get; set; }

        public virtual ICollection<IfmsFixedAssetSetting> IfmsFixedAssetSettings { get; set; }
        public virtual ICollection<IfmsVoucherTypeSetting> IfmsVoucherTypeSettings { get; set; }


    }
}
