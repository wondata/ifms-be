using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class PsmsPaymentRequest
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
        public Guid? SupplierId  { get; set; }
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

    }
}
