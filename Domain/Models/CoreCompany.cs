using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class CoreCompany
    {

        public CoreCompany() {
            CoreControlAccounts = new HashSet<CoreControlAccount>();
            InverseParent = new HashSet<CoreCompany>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public string IconCls { get; set; }

        public virtual CoreCompany Parent { get; set; }
        public virtual ICollection<CoreCompany> InverseParent { get; set; }
        public virtual ICollection<CoreControlAccount> CoreControlAccounts { get; set; }



    }
}
