﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class LupVoucherType
    {
        public LupVoucherType()
        {
            IfmsVoucherTypeSettings = new HashSet<IfmsVoucherTypeSetting>();
            IfmsVoucherHeaders = new HashSet<IfmsVoucherHeader>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }   
        
        public virtual ICollection<IfmsVoucherTypeSetting> IfmsVoucherTypeSettings { get; set; }
        public virtual ICollection<IfmsVoucherHeader> IfmsVoucherHeaders { get; set; }

    }
}
