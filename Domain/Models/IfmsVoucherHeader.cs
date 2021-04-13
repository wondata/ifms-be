using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsVoucherHeader
    {
        public IfmsVoucherHeader()
        {
            IfmsVoucherDetails = new HashSet<IfmsVoucherDetail>();
            IfmsVoucherDetails_2 = new HashSet<IfmsVoucherDetail>();
        }

        public int Id { get; set; }
        public int CostCenterId { get; set; }
        public int VoucherTypeId { get; set; }
        public string ReferenceNo { get; set; }
        public string DocumentNo { get; set; }
        public DateTime? Date { get; set; }
        public int PeriodId { get; set; }
        public string PayedToReceivedFrom { get; set; }
        public int PurposeTemplateId { get; set; }
        public string Purpose { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public string TaxId { get; set; }
        public int ModeOfPaymentId { get; set; }
        public string ChequeNo { get; set; }
        public int ProjectId { get; set; }
        public bool? CreatedBy { get; set; }
        public bool? IsPosted { get; set; }
        public bool? IsAdjustment { get; set; }
        public bool? IsVoid{ get; set; }
        public string PostedFromOperation { get; set; }
        public DateTime? AuthorizedDate { get; set; }
        public bool? IsChecked { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? LastUpdated { get; set; }

        public ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }

        public ICollection<IfmsVoucherDetail> IfmsVoucherDetails_2 { get; set; }

        public virtual CoreCostCenter CoreCostCenter { get; set; }

    }
}
