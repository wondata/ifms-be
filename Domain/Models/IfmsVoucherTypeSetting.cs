using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsVoucherTypeSetting
    {
        public IfmsVoucherTypeSetting()
        {
           // CoreCostCenters_2 = new HashSet<CoreCostCenter>();          
        }

        public Guid Id { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid VoucherTypeId { get; set; }
        public Guid? DefaultAccountId { get; set; }
        public Guid? BalanceSideId { get; set; }
        public int StartingNumber { get; set; }
        public int EndingNumber { get; set; }
        public int CurrentNumber { get; set; }
        public int NumberOfDigits { get; set; }
        //public bool? IsDeleted { get; set; }
        //public DateTime? LastUpdated { get; set; }
        public virtual CoreCostCenter CoreCostCenters {get; set;}                
        public virtual ICollection<CoreCostCenter> CoreCostCenters_2 { get; set; }
        public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts { get; set; }
        public virtual LupBalanceSide LupBalanceSides{ get; set; }
        public virtual LupVoucherType LupVoucherTypes  { get; set; }

    }
}
