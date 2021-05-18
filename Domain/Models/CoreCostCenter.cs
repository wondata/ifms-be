using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class CoreCostCenter
    {
        public CoreCostCenter()
        {           
            InverseParent = new HashSet<CoreCostCenter>();
            IfmsVoucherTypeSettings = new HashSet<IfmsVoucherTypeSetting>();
            IfmsVoucherHeaders = new HashSet<IfmsVoucherHeader>();
           
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? ParentId{ get; set; }

        public virtual CoreCostCenter Parent { get; set; }
        public virtual ICollection<CoreCostCenter> InverseParent { get; set; }
        public virtual ICollection<IfmsVoucherTypeSetting> IfmsVoucherTypeSettings { get; set; }
        public virtual ICollection<IfmsVoucherHeader> IfmsVoucherHeaders { get; set; }


    }
}
