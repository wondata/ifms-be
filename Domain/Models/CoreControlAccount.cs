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
         //   IfmsSettings = new HashSet<IfmsSetting>();
         //   IfmsVoucherDetailHistorys = new HashSet<IfmsVoucherDetailHistory>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? NoteNo { get; set; }
        public Guid? AccountGroupId { get; set; }
        public Guid? ChartAccountID { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual CoreChartOfAccount ChartOfAccount{ get; set; }
        public virtual ICollection<CoreSubsidiaryAccount> CoreSubsidiaryAccounts { get; set; }
        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }
       // public virtual ICollection<IfmsVoucherDetailHistory> IfmsVoucherDetailHistorys { get; set; }
     //   public virtual ICollection<IfmsSetting> IfmsSettings{ get; set; }

    }
}
