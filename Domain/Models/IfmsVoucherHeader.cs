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
            IfmsVoucherDetails1 = new HashSet<IfmsVoucherDetail>();
            IfmsVoucherDetails2 = new HashSet<IfmsVoucherDetail>();
        }

        public Guid Id { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid VoucherTypeId { get; set; }
        public string ReferenceNo { get; set; }
        public string? DocumentNo { get; set; }
        public DateTime Date { get; set; }
        public Guid PeriodId { get; set; }
        public string? PayedToReceivedFrom { get; set; }
        public Guid? PurposeTemplateId { get; set; }
        public string? Purpose { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public string? TaxId { get; set; }
        public Guid? ModeOfPaymentId { get; set; }
        public string? ChequeNo { get; set; }
        public string CreatedBy { get; set; }
        public string? PostedBy { get; set; }
        public bool IsPosted { get; set; }
        public bool IsAdjustment { get; set; }
        public bool IsVoid{ get; set; }
        public string? PostedFromOperation { get; set; }
        public DateTime? AuthorizedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual CoreCostCenter CostCenter { get; set; }
        public virtual CorePeriod CorePeriod { get; set; }
        public virtual LupVoucherType VoucherType { get; set; }
        public virtual LupModeOfPayment ModeOfPayment { get; set; }
        public virtual IfmsPurposeTemplate PurposeTemplate { get; set; }
        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }
        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails1 { get; set; }
        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails2 { get; set; }


    }
}
