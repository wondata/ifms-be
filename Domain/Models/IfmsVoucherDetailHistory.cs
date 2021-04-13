using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class IfmsVoucherDetailHistory
    {
        public IfmsVoucherDetailHistory()
        {
          
        }

        public int Id { get; set; }
        public int VoucherHeaderId { get; set; }
        public int CostCenterId { get; set; }
        public int ControlAccountId { get; set; }
        public Guid? SubsidiaryAccountId { get; set; }
        public float DebitAmount { get; set; }
        public float CreditAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual CoreControlAccount CoreControlAccounts { get; set;}
        public virtual CoreCostCenter CoreCostCenters { get; set; }
        public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts{ get; set; }
        public virtual IfmsVoucherHeaderHistory IfmsVoucherHeaderHistorys { get; set; }

    } 
}
