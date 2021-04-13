using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsVoucherTypeSetting
    {
        public IfmsVoucherTypeSetting()
        {
            CoreCostCenters_2 = new HashSet<CoreCostCenter>();

        }

        public int Id { get; set; }
        public int CostCenterId { get; set; }
        public int VoucherTypeId { get; set; }
        public int? DefaultAccountId { get; set; }
        public int? BalanceSideId { get; set; }
        public int StartingNumber { get; set; }
        public int EndingNumber { get; set; }
        public int CurrentNumber { get; set; }
        public int NumberOfDigits { get; set; }
        //public bool? IsDeleted { get; set; }
        //public DateTime? LastUpdated { get; set; }
        public virtual CoreCostCenter CoreCostCenters {get; set;}
        public virtual ICollection<CoreCostCenter> CoreCostCenters_2 { get; set; }

    }
}
