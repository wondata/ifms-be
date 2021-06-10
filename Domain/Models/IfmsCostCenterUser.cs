using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsCostCenterUser
    {
        public IfmsCostCenterUser()
        {
        }

        public Guid Id { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }

       public virtual CoreCostCenter CoreCostCenter { get; set; }
    }
}
