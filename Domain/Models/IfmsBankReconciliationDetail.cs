using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsBankReconciliationDetail
    {

        public Guid Id { get; set; }
        public Guid ReconciliationHeaderId { get; set; }
        public Guid VoucherDetailId { get; set; }
        public bool IsReconciled { get; set; }
        public bool IsDeleted { get; set; }

        public virtual IfmsVoucherDetail VoucherDetail { get; set; }
    }
}
