using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Entities
{
    public class VoucherHeaderEntity : BaseEntity
    {
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
        public string? PostedBy { get; set; }
        public bool IsPosted { get; set; }
        public bool IsAdjustment { get; set; }
        public bool IsVoid { get; set; }
        public string? PostedFromOperation { get; set; }

        public Guid? AccountId { get; set; }
        public DateTime? AuthorizedDate { get; set; }

        public CostCenterEntity CostCenter { get; set; }
        public PeriodEntity Period { get; set; }
        public VoucherTypeEntity VoucherType { get; set; }
        public PurposeTemplateEntity PurposeTemplate { get; set; }
        public LookupEntity ModePayment{ get; set; }

        public DateTime parsedDate;


        public VoucherHeaderEntity()
        {
        }

        public VoucherHeaderEntity(IfmsVoucherHeader ifmsVoucherHeader)
        {
            if (ifmsVoucherHeader == null) return;
           

            this.Id = ifmsVoucherHeader.Id.ToString();
            this.CostCenterId = ifmsVoucherHeader.CostCenterId;
            this.VoucherTypeId = ifmsVoucherHeader.VoucherTypeId;
            this.ReferenceNo = ifmsVoucherHeader.ReferenceNo;
            this.DocumentNo = ifmsVoucherHeader.DocumentNo;
            this.Date = ifmsVoucherHeader.Date;

            this.PeriodId = ifmsVoucherHeader.PeriodId;
            this.PayedToReceivedFrom = ifmsVoucherHeader.PayedToReceivedFrom;
            this.PurposeTemplateId = ifmsVoucherHeader.PurposeTemplateId;
            this.Purpose = ifmsVoucherHeader.Purpose;            
            this.Description = ifmsVoucherHeader.Description;
            this.ModeOfPaymentId = ifmsVoucherHeader.ModeOfPaymentId;
            this.Amount = ifmsVoucherHeader.Amount;
            this.TaxId = ifmsVoucherHeader.TaxId;
            this.ChequeNo = ifmsVoucherHeader.ChequeNo;
            this.CreatedBy = ifmsVoucherHeader.CreatedBy;
            this.IsDeleted = ifmsVoucherHeader.IsDeleted;
            this.PostedBy = ifmsVoucherHeader.PostedBy;
            this.IsAdjustment = ifmsVoucherHeader.IsAdjustment;
            this.IsVoid = ifmsVoucherHeader.IsVoid;
            this.PostedFromOperation = ifmsVoucherHeader.PostedFromOperation;
            this.AuthorizedDate = ifmsVoucherHeader.AuthorizedDate;
        //    this.LastUpdated = ifmsVoucherHeader.LastUpdated;
            
            this.CostCenter = new CostCenterEntity(ifmsVoucherHeader.CostCenter);
            this.Period = new PeriodEntity(ifmsVoucherHeader.CorePeriod);
            this.VoucherType = new VoucherTypeEntity(ifmsVoucherHeader.VoucherType);
            this.PurposeTemplate = new PurposeTemplateEntity(ifmsVoucherHeader.PurposeTemplate);
            this.ModePayment = ( ifmsVoucherHeader.ModeOfPayment == null) ? null : LookupEntity.Map(ifmsVoucherHeader.ModeOfPayment);
        }

        public override T MapToModel<T>()
        {
            Guid id;
            Guid.TryParse(this.Id, out id);

            IfmsVoucherHeader voucherHeader = new IfmsVoucherHeader
            {
                Id = id,
                CostCenterId = this.CostCenterId,
                VoucherTypeId = this.VoucherTypeId,
                ReferenceNo = this.ReferenceNo,
                DocumentNo = this.DocumentNo,
                Date = this.Date,
                PeriodId = this.PeriodId,
                PayedToReceivedFrom = this.PayedToReceivedFrom,
                PurposeTemplateId = this.PurposeTemplateId,
                Purpose = this.Purpose,
                Description = this.Description,
                ModeOfPaymentId = this.ModeOfPaymentId,
                Amount = this.Amount,
                TaxId = this.TaxId,
                ChequeNo = this.ChequeNo,
                CreatedBy = this.CreatedBy,
                PostedBy = this.PostedBy,
                IsDeleted = this.IsDeleted,
                IsAdjustment = this.IsAdjustment,
                IsVoid = this.IsVoid,
                PostedFromOperation = this.PostedFromOperation,
                AuthorizedDate = this.AuthorizedDate

        };

            return voucherHeader as T;
        }

        public IfmsVoucherHeader MapToModel()
        {
            Guid id;
            Guid.TryParse(this.Id, out id);

            IfmsVoucherHeader voucherHeader = new IfmsVoucherHeader
            {
                Id = id,
                CostCenterId = this.CostCenterId,
                VoucherTypeId = this.VoucherTypeId,
                ReferenceNo = this.ReferenceNo,
                DocumentNo = this.DocumentNo,
                Date = this.Date,
                PeriodId = this.PeriodId,
                PayedToReceivedFrom = this.PayedToReceivedFrom,
                PurposeTemplateId = this.PurposeTemplateId,
                Purpose = this.Purpose,
                Description = this.Description,
                ModeOfPaymentId = this.ModeOfPaymentId,
                Amount = this.Amount,
                TaxId = this.TaxId,
                ChequeNo = this.ChequeNo,
                CreatedBy = this.CreatedBy,
                PostedBy = this.PostedBy,
                IsDeleted = this.IsDeleted,
                IsAdjustment = this.IsAdjustment,
                IsVoid = this.IsVoid,
                PostedFromOperation = this.PostedFromOperation,
                AuthorizedDate = this.AuthorizedDate

            };

            return voucherHeader ;
        }

        public override T MapToModel<T>(T t)
        {
            IfmsVoucherHeader voucherHeader = t as IfmsVoucherHeader;

                Id = this.Id;
                ReferenceNo = this.ReferenceNo;
                DocumentNo = this.DocumentNo;
                Date = this.Date;
                PeriodId = this.PeriodId;
                PayedToReceivedFrom = this.PayedToReceivedFrom;
                Purpose = this.Purpose;
                Description = this.Description;
                ModeOfPaymentId = this.ModeOfPaymentId;
                Amount = this.Amount;
                ChequeNo = this.ChequeNo;
                CreatedBy = this.CreatedBy;
                IsDeleted = this.IsDeleted;
                IsAdjustment = this.IsAdjustment;
                IsVoid = this.IsVoid;
            
            return voucherHeader as T;

        }
    }
}
