﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public partial class VoucherDetailHistoryEntity
    {
        public Guid Id { get; set; }
        public int VoucherHeaderId { get; set; }
        public int CostCenterId { get; set; }
        public int ControlAccountId { get; set; }
        public Guid? SubsidiaryAccountId { get; set; }
        public float DebitAmount { get; set; } 
        public float CreditAmount { get; set; }
        public byte[] IsDeleted { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
