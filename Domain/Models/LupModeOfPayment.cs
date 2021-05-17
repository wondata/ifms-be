using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class LupModeOfPayment
    {
        public LupModeOfPayment()
        {
            IfmsVoucherHeaders = new HashSet<IfmsVoucherHeader>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<IfmsVoucherHeader> IfmsVoucherHeaders { get; set; }

    }
}
