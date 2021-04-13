using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public partial class CoreControlAccount
    {
        public CoreControlAccount()
        {
            IfmsVoucherDetails = new HashSet<IfmsVoucherDetail>();
            IfmsVoucherHeaderHistorys = new HashSet<IfmsVoucherHeaderHistory>();
            IfmsVoucherDetailHistorys = new HashSet<IfmsVoucherDetailHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string  Code { get; set; }
        public string NoteNo { get; set; }
        public int AccountGroupId { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }
        public ICollection<IfmsVoucherHeaderHistory> IfmsVoucherHeaderHistorys { get; set; }
        public ICollection<IfmsVoucherDetailHistory> IfmsVoucherDetailHistorys { get; set; }

    }
}
