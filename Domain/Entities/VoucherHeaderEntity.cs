using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class VoucherHeaderEntity
    {
        public Guid Id { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid VoucherTypeId { get; set; }
        public string ReferenceNo { get; set; }
        public string? DocumentNo { get; set; }
        public DateTime? Date { get; set; }
        public Guid PeriodId { get; set; }
        public string? PayedToReceivedFrom { get; set; }
        public Guid? PurposeTemplateId { get; set; }
        public string? Purpose { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public Guid? ModeOfPaymentId { get; set; }
        public string? ChequeNo { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsPosted { get; set; }
        public bool? IsAdjustment { get; set; }
        public bool? IsVoid { get; set; }
        public bool? IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public CostCenterEntity CostCenter { get; set; }
        public PeriodEntity Period { get; set; }
        public VoucherTypeEntity VoucherType { get; set; }

        public VoucherHeaderEntity()
        {
        }

        public VoucherHeaderEntity(IfmsVoucherHeader ifmsVoucherHeader)
        {
            if (ifmsVoucherHeader == null) return;
       
            this.ReferenceNo = ifmsVoucherHeader.ReferenceNo;
            this.DocumentNo = ifmsVoucherHeader.DocumentNo;
            this.Date = ifmsVoucherHeader.Date;
            this.PayedToReceivedFrom = ifmsVoucherHeader.PayedToReceivedFrom;        
            this.Purpose = ifmsVoucherHeader.Purpose;            
            this.Description = ifmsVoucherHeader.Description;
            this.Amount = ifmsVoucherHeader.Amount;         
            this.ChequeNo = ifmsVoucherHeader.ChequeNo;
            this.CreatedBy = ifmsVoucherHeader.CreatedBy;
            this.IsDeleted = ifmsVoucherHeader.IsDeleted;
            this.IsAdjustment = ifmsVoucherHeader.IsAdjustment;
            this.IsVoid = ifmsVoucherHeader.IsVoid;
            this.IsDeleted = ifmsVoucherHeader.IsDeleted;
            
            this.CostCenter = new CostCenterEntity(ifmsVoucherHeader.CostCenter);
            this.Period = new PeriodEntity(ifmsVoucherHeader.CorePeriod);
            this.VoucherType = new VoucherTypeEntity(ifmsVoucherHeader.VoucherType);
        }

    }
}
