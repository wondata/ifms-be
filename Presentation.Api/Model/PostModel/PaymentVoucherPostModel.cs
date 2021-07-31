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
            Guid AccountIds;
            Guid.TryParse(this.AccountId, out AccountIds);
            Guid ModeOfPaymentIds;
            Guid.TryParse(this.ModeOfPaymentId, out ModeOfPaymentIds);
            Guid Projects;
            Guid.TryParse(this.Project, out Projects);
            Guid PurposeTemplateIds;
            Guid.TryParse(this.PurposeTemplateId, out PurposeTemplateIds);
            Guid VoucherTypeIds;
            Guid.TryParse(this.VoucherTypeId, out VoucherTypeIds);


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
                VoucherTypeId = VoucherTypeIds

            };

            return voucher;

        }

    }
}
