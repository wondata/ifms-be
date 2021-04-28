using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class LupBalanceSide
    {
        public LupBalanceSide()
        {
            //Initialize related objects
            //CoreAccountType = new HashSet<CoreAccountType>();
            //CoreSubsidiaryAccount = new HashSet<CoreSubsidiaryAccount>();
            IfmsVoucherTypeSettings = new HashSet<IfmsVoucherTypeSetting>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //Related objects definitions
        public virtual ICollection<CoreAccountType> CoreAccountType { get; set; }
        public virtual ICollection<CoreSubsidiaryAccount> CoreSubsidiaryAccount { get; set; }
        public virtual ICollection<IfmsVoucherTypeSetting> IfmsVoucherTypeSettings { get; set; }
    }
}
