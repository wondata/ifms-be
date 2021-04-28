using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsVoucherDetail
    {       
        public Guid Id { get; set; }
        public Guid VoucherHeaderId { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid CostCodeId { get; set; }
        public Guid? ControlAccountId { get; set; }
        public Guid? SubsidiaryAccountId { get; set; }
        public float DebitAmount { get; set; }
        public float CreditAmount { get; set; }
        public bool? IsInterBranchTransactionCleared { get; set; }
        public Guid IBTReferenceVoucherHeaderId { get; set; }

        public virtual IfmsCostCode CoreCostCodes { get; set; }
        public virtual CoreCostCenter CoreCostCenters { get; set; }
        public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts{ get; set; }
        public virtual CoreControlAccount CoreControlAccounts { get; set; }
        public virtual IfmsVoucherHeader IfmsVoucherHeaders { get; set; }
        public virtual IfmsVoucherHeader IfmsVoucherHeaders_2 { get; set; }


    }
}
