using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class LupBalanceSide
    {
        public LupBalanceSide()
        {            
            IfmsVoucherTypeSettings = new HashSet<IfmsVoucherTypeSetting>();
            CoreSubsidiaryAccount = new HashSet<CoreSubsidiaryAccount>();
            CoreAccountTypes = new HashSet<CoreAccountType>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
       
        public virtual ICollection<IfmsVoucherTypeSetting> IfmsVoucherTypeSettings { get; set; }
        public virtual ICollection<CoreSubsidiaryAccount> CoreSubsidiaryAccount { get; set; }
        public virtual ICollection<CoreAccountType> CoreAccountTypes { get; set; }

    }
}
