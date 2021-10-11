using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CoreChartOfAccount
    {
        public CoreChartOfAccount()
        {
            InverseParent = new HashSet<CoreChartOfAccount>();
            CoreControlAccounts = new HashSet<CoreControlAccount>();
        }

        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? ParentId { get; set; }

        public virtual CoreChartOfAccount Parent { get; set; }
        public virtual CoreCompany Company{ get; set; }
        public virtual ICollection<CoreChartOfAccount> InverseParent { get; set; }
        public virtual ICollection<CoreControlAccount> CoreControlAccounts { get; set; }
    }
}
