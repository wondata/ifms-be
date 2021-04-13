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
        }

        public Guid Id { get; set; }
        public Guid ControlAccountId { get; set; }
        public Guid BalanceSideId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual LupBalanceSide BalanceSide { get; set; }
        //public virtual CoreControlAccount ControlAccount { get; set; }
        public ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }
        public virtual ICollection<IfmsVoucherDetailHistory> IfmsVoucherDetailHistorys { get; set; }

    }
}
