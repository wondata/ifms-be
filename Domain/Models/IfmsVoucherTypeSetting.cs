using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public partial class IfmsVoucherTypeSetting
    {
        public IfmsVoucherTypeSetting()
        {            
        }   
        
        public Guid Id { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid VoucherTypeId { get; set; }
        public Guid? DefaultAccountId { get; set; }
        public Guid? BalanceSideId { get; set; }
        public int? StartingNumber { get; set; }
        public int? EndingNumber { get; set; }
        public int? CurrentNumber { get; set; }
        public int NumberOfDigits { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
      

        public virtual CoreCostCenter CoreCostCenter {get; set;}                
        public virtual CoreSubsidiaryAccount CoreSubsidiaryAccount { get; set; }
        public virtual LupBalanceSide LupBalanceSide { get; set; }
        public virtual LupVoucherType LupVoucherType  { get; set; }

        
    }
}
