using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class CoreAccountGroup
    {
        public CoreAccountGroup()
        {
            CoreControlAccount = new HashSet<CoreControlAccount>();
        }

        public Guid Id { get; set; }
        public Guid? AccountTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual CoreAccountType AccountType { get; set; }
        public virtual ICollection<CoreControlAccount> CoreControlAccount { get; set; }
    }
}
