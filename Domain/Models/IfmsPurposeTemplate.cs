using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
   public partial class IfmsPurposeTemplate
    {
        public IfmsPurposeTemplate()
        {
            IfmsVoucherHeaders = new HashSet<IfmsVoucherHeader>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Purpose { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<IfmsVoucherHeader> IfmsVoucherHeaders { get; set; }

    }
}
