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
        public string DocumentNo { get; set; }
        public DateTime? Date { get; set; }
        public Guid PeriodId { get; set; }
        public string PayedToReceivedFrom { get; set; }
        public int PurposeTemplateId { get; set; }
        public string Purpose { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public string TaxId { get; set; }
        public Guid ModeOfPaymentId { get; set; }
        public string ChequeNo { get; set; }
        public Guid ProjectId { get; set; }
        public bool? CreatedBy { get; set; }
        public bool? IsPosted { get; set; }
        public bool? IsAdjustment { get; set; }
        public bool? IsVoid { get; set; }
        public string PostedFromOperation { get; set; }
        public DateTime? AuthorizedDate { get; set; }
        public bool? IsChecked { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? LastUpdated { get; set; }

        public VoucherHeaderEntity()
        {
        }

        public VoucherHeaderEntity(IfmsVoucherHeader coreVoucherHeader)
        {
            if (coreVoucherHeader == null) return;

          //  this.CostCenterId = coreVoucherHeader.CostCenterId;
          //  this.VoucherTypeId = coreVoucherHeader.VoucherTypeId;
            this.ReferenceNo = coreVoucherHeader.ReferenceNo;
            this.DocumentNo = coreVoucherHeader.DocumentNo;
          //  this.PeriodId = coreVoucherHeader.PeriodId;
            this.PayedToReceivedFrom = coreVoucherHeader.PayedToReceivedFrom;
            //this.PurposeTemplateId = coreVoucherHeader.PurposeTemplateId;
            this.Purpose = coreVoucherHeader.Purpose;            
            this.Description = coreVoucherHeader.Description;
            this.Amount = coreVoucherHeader.Amount;
         //   this.TaxId = coreVoucherHeader.TaxId;
          //  this.ModeOfPaymentId = coreVoucherHeader.ModeOfPaymentId;
            this.ChequeNo = coreVoucherHeader.ChequeNo;
          //  this.ProjectId = coreVoucherHeader.ProjectId;
            this.PostedFromOperation = coreVoucherHeader.PostedFromOperation;
            this.IsChecked = coreVoucherHeader.IsChecked;
            this.IsDeleted = coreVoucherHeader.IsDeleted;            


        }
    }
}
