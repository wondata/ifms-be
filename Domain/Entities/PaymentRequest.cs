using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PaymentRequest
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string PayeeName { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
        public string? Priority { get; set; }
        public Guid StoreId { get; set; }
        public string AttachedDocuments { get; set; }
        public string? Type { get; set; }
        public Guid? PRId { get; set; }
        public Guid? SupplierId { get; set; }
        public int NoOfPages { get; set; }
        public Guid RequestedById { get; set; }
        public Guid? CertifiedById { get; set; }
        public DateTime? CertifiedDate { get; set; }
        public Guid? ApprovedById { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Guid? DirectedById { get; set; }
        public string? Remark { get; set; }
        public Guid StatusId { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }

        public PaymentRequest()
        {
        }

        public PaymentRequest(PsmsPaymentRequest psmsPayment)
        {
            this.Id = psmsPayment.Id;
            this.Date = psmsPayment.Date;
            this.Amount = psmsPayment.Amount;
            this.Purpose = psmsPayment.Purpose;
            this.Priority = psmsPayment.Priority;
            this.StoreId = psmsPayment.StoreId;
            this.AttachedDocuments = psmsPayment.AttachedDocuments;
            this.Type = psmsPayment.Type;
            this.PRId = psmsPayment.PRId;
            this.SupplierId = psmsPayment.SupplierId;
            this.NoOfPages = psmsPayment.NoOfPages;
            this.RequestedById = psmsPayment.RequestedById;
            this.CertifiedById = psmsPayment.CertifiedById;
            this.CertifiedDate = psmsPayment.CertifiedDate;
            this.ApprovedById = psmsPayment.ApprovedById;
            this.ApprovedDate = psmsPayment.ApprovedDate;
            this.DirectedById = psmsPayment.DirectedById;
            this.Remark = psmsPayment.Remark;
            this.StatusId = psmsPayment.StatusId;
            this.IsDeleted = psmsPayment.IsDeleted;
        }
    }
}
