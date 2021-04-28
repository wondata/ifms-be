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
            IfmsSettings = new HashSet<IfmsSetting>();
            CoreSubsidiaryAccounts = new HashSet<CoreSubsidiaryAccount>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string  Code { get; set; }
        public string NoteNo { get; set; }
        public int AccountGroupId { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }
        public virtual ICollection<IfmsVoucherHeaderHistory> IfmsVoucherHeaderHistorys { get; set; }
        public virtual ICollection<IfmsVoucherDetailHistory> IfmsVoucherDetailHistorys { get; set; }
        public virtual ICollection<IfmsSetting> IfmsSettings { get; set; }
        public virtual ICollection<CoreSubsidiaryAccount> CoreSubsidiaryAccounts { get; set; }

    }
}
