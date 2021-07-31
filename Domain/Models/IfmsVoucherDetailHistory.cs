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

        public Guid Id { get; set; }
        public Guid VoucherHeaderId { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid ControlAccountId { get; set; }
        public Guid? SubsidiaryAccountId { get; set; }
        public float DebitAmount { get; set; }
        public float CreditAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime LastUpdated { get; set; }
        
     //   public virtual CoreControlAccount ControlAccounts { get; set;}
        //public virtual CoreCostCenter CoreCostCenters { get; set; }
        //public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts{ get; set; }
        //public virtual IfmsVoucherHeaderHistory IfmsVoucherHeaderHistorys { get; set; }

    } 
}
