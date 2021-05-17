using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsCashier
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Guid UserId { get; set; }
        public Guid SubsidiaryAccountId { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }

        //public virtual CoreUser CoreUser{get; set;}

        public virtual CoreSubsidiaryAccount SubsidiaryAccount { get; set; }
    }
}
