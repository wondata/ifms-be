using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class AccountTypeEntity
    {
        public AccountTypeEntity()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? AccountCategoryId { get; set; }
        public Guid? BalanceSideId { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
