using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.PostModel
{
    public class VoucherHeaderPostModel
    {
        public string Id { get; set; }
        public string CostCenterId { get; set; }
        public string VoucherTypeId { get; set; }
        public string ReferenceNo { get; set; }
        public string? DocumentNo { get; set; }
        public DateTime Date { get; set; }
        public string PeriodId { get; set; }
        public string? PayedToReceivedFrom { get; set; }
        public string? PurposeTemplateId { get; set; }
        public string? Purpose { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public string? TaxId { get; set; }
        public string? ModeOfPaymentId { get; set; }
        public string? ChequeNo { get; set; }
        public string? PostedBy { get; set; }
        public bool IsPosted { get; set; }
        public bool IsAdjustment { get; set; }
        public bool IsVoid { get; set; }
        public string? PostedFromOperation { get; set; }

        public DateTime parsedDate;
        public string? AccountId { get; set; }
        public DateTime? AuthorizedDate { get; set; }

        public List<VoucherDetailEntity> voucherDetails { get; set; }

        public VoucherHeaderPostModel()
        {
        }

        public VoucherHeaderEntity MapToEntity()
        {
            Guid.TryParse(this.Id, out Guid id);
            Guid.TryParse(this.CostCenterId, out Guid CostCenterIds);
            Guid.TryParse(this.VoucherTypeId, out Guid VoucherTypeIds);
            Guid.TryParse(this.PeriodId, out Guid PeriodIds);
            Guid.TryParse(this.PurposeTemplateId, out Guid PurposeTemplateIds);
            Guid.TryParse(this.ModeOfPaymentId, out Guid ModeOfPaymentIds);
            Guid.TryParse(this.AccountId, out Guid AccountIds);

            VoucherHeaderEntity voucherHeader = new VoucherHeaderEntity {
                Id = this.Id,
                CostCenterId = CostCenterIds,
                VoucherTypeId = VoucherTypeIds,
                ReferenceNo = this.ReferenceNo,
                DocumentNo = this.DocumentNo,
                Date = this.Date,
                PeriodId = PeriodIds,
                PayedToReceivedFrom = this.PayedToReceivedFrom,
                PurposeTemplateId = PurposeTemplateIds,
                Purpose = this.Purpose,
                Description = this.Description,
                ModeOfPaymentId = ModeOfPaymentIds,
                Amount = this.Amount,
                TaxId = this.TaxId,
                ChequeNo = this.ChequeNo,
                PostedBy = this.PostedBy,
                IsPosted = this.IsPosted,
                IsAdjustment = this.IsAdjustment,
                IsVoid = this.IsVoid,
                PostedFromOperation = this.PostedFromOperation,
                AuthorizedDate = this.AuthorizedDate,
                AccountId = AccountIds
            };

            return voucherHeader;
        }
        

    }
}
