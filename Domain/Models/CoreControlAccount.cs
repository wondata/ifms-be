using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class CoreControlAccount
    {
        public CoreControlAccount()
        {            
            CoreSubsidiaryAccounts = new HashSet<CoreSubsidiaryAccount>();
            IfmsVoucherDetails = new HashSet<IfmsVoucherDetail>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string NoteNo { get; set; }
        public Guid? AccountGroupId { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual CoreCompany Company { get; set; }
        public virtual ICollection<CoreSubsidiaryAccount> CoreSubsidiaryAccounts { get; set; }
        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }

    }
}
