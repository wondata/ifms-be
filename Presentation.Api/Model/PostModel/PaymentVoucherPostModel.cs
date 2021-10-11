using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.PostModel
{
    public class PaymentVoucherPostModel
    {

        public string AccountId { get; set; }
        public string Amount { get; set; }
        public string? AuthorizedDate { get; set; }
        public string ChequeNo { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string ModeOfPaymentId { get; set; }
        public string PayedToReceivedFrom { get; set; }
        public string Project { get; set; }
        public string PurposeTemplateId { get; set; }
        public string VoucherTypeId { get; set; }

        public VoucherHeaderEntity MapToEntity()
        {            
            Guid.TryParse(this.AccountId, out Guid AccountIds);
            Guid.TryParse(this.ModeOfPaymentId, out Guid ModeOfPaymentIds);
            Guid.TryParse(this.Project, out Guid Projects);
            Guid.TryParse(this.PurposeTemplateId, out Guid PurposeTemplateIds);
            Guid.TryParse(this.VoucherTypeId, out Guid VoucherTypeIds);        

            VoucherHeaderEntity voucher = new VoucherHeaderEntity
            {
                AccountId = AccountIds,
                Amount = decimal.Parse(this.Amount),
                AuthorizedDate = this.AuthorizedDate == null ? DateTime.Now : DateTime.Parse(this.AuthorizedDate, null),
                ChequeNo = this.ChequeNo,
                Date = DateTime.Parse(this.Date, null),
                Description = this.Description,
                ModeOfPaymentId = ModeOfPaymentIds,
                PayedToReceivedFrom = this.PayedToReceivedFrom,
                CostCenterId = Projects,
                PurposeTemplateId = PurposeTemplateIds,
                VoucherTypeId = VoucherTypeIds,
                Id = this.Id

            };

            return voucher;

        }

    }
}
