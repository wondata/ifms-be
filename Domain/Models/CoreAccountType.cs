using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class CoreAccountType
    {
        public CoreAccountType()
        {
            CoreAccountGroup = new HashSet<CoreAccountGroup>();
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

        //public virtual CoreAccountCategory AccountCategory { get; set; }
        public virtual LupBalanceSide BalanceSide { get; set; }
        public virtual ICollection<CoreAccountGroup> CoreAccountGroup { get; set; }
    }
}
