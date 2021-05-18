using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CorePeriod
    {
        public CorePeriod()
        {
            IfmsVoucherHeader = new HashSet<IfmsVoucherHeader>();
        }

        public Guid Id { get; set; }
        public Guid FiscalYearId { get; set; }
        public int? OrderNumber { get; set; }
        public int? QuarterNumber { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<IfmsVoucherHeader> IfmsVoucherHeader { get; set; }
    }
}
