using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public partial class VoucherHeaderHistoryEntity
    {
        public Guid Id { get; set; }
        public Guid VoucherTypeId { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? Date { get; set; }
        public Guid PeriodId { get; set; }
        public string PayedToReceivedFrom { get; set; }
        public string Purpose { get; set; }
        public float Amount { get; set; }
        public Guid ModeOfPaymentId { get; set; }
        public string ChequeNo { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsPosted { get; set; }
        public bool? IsAdjustment { get; set; }
        public bool? IsVoid { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
