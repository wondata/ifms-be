using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsVoucherHeaderHistory
    {
        public IfmsVoucherHeaderHistory()
        {
            IfmsVoucherDetailHistorys = new HashSet<IfmsVoucherDetailHistory>();
        }

        public int Id { get; set; }
        public int VoucherTypeId { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? Date { get; set; }
        public int PeriodId { get; set; }
        public string PayedToReceivedFrom { get; set; }
        public string Purpose { get; set; }
        public float Amount { get; set; }
        public int ModeOfPaymentId { get; set; }
        public string ChequeNo { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsPosted { get; set; }
        public bool? IsAdjustment { get; set; }
        public bool? IsVoid { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? LastUpdated { get; set; }

        public virtual ICollection<IfmsVoucherDetailHistory> IfmsVoucherDetailHistorys { get; set; }
    }
}
