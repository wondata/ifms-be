using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class SubsidiaryAccountEntity
    {
        public int Id { get; set; }
        public int ControlAccountId { get; set; }
        public int BalanceSideId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

       
    }
}
