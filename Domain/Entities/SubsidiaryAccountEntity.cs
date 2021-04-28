using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class SubsidiaryAccountEntity
    {
        public Guid? Id { get; set; }
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

       
    }
}
