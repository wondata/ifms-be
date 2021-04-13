using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsVoucherDetail
    {       
        public int Id { get; set; }

        public int VoucherHeaderId { get; set; }

        public int CostCenterId { get; set; }

        public int CostCodeId { get; set; }

        public int ControlAccountId { get; set; }

        public Guid? SubsidiaryAccountId { get; set; }

        public float DebitAmount { get; set; }

        public float CreditAmount { get; set; }

        public bool? IsInterBranchTransactionCleared { get; set; }

        public int IBTReferenceVoucherHeaderId { get; set; }

        public virtual IfmsCostCode CoreCostCodes { get; set; }
        public virtual CoreCostCenter CoreCostCenters { get; set; }
        public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts{ get; set; }
        public virtual CoreControlAccount CoreControlAccounts { get; set; }
        public virtual IfmsVoucherHeader IfmsVoucherHeaders { get; set; }

        public virtual IfmsVoucherHeader IfmsVoucherHeaders_2 { get; set; }


    }
}
