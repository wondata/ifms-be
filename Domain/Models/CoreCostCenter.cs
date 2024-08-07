﻿using System;
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
            IfmsVoucherDetails = new HashSet<IfmsVoucherDetail>();
            IfmsSettings = new HashSet<IfmsSetting>();
            IfmsCostCenterUsers = new HashSet<IfmsCostCenterUser>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? ParentId{ get; set; }

        public virtual CoreCostCenter Parent { get; set; }
        public virtual ICollection<CoreCostCenter> InverseParent { get; set; }
        public virtual ICollection<IfmsVoucherTypeSetting> IfmsVoucherTypeSettings { get; set; }
        public virtual ICollection<IfmsVoucherHeader> IfmsVoucherHeaders { get; set; }
        public virtual ICollection<IfmsVoucherDetail> IfmsVoucherDetails { get; set; }
        public virtual ICollection<IfmsSetting> IfmsSettings { get; set; }
        public virtual ICollection<IfmsCostCenterUser> IfmsCostCenterUsers{ get; set; }


    }
}
