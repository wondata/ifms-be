using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsCostCode
    {
        public IfmsCostCode()
        {
            IfmsVoucherDetails = new HashSet<IfmsVoucherDetail>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }

    }
}
