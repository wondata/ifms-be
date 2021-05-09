using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class CoreSubsidiaryAccount
    {
        public CoreSubsidiaryAccount()
        {
           // IfmsVoucherTypeSettings = new HashSet<IfmsVoucherTypeSetting>();
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
     //   public virtual CoreControlAccount CoreControlAccount { get; set; }
        public virtual ICollection<IfmsVoucherTypeSetting> IfmsVoucherTypeSettings { get; set; }

     }
}
