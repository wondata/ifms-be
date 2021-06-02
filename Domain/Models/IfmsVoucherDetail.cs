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
        public int SNo { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid? CaseId { get; set; }
        public Guid? CostCodeId { get; set; }
        public Guid ControlAccountId { get; set; }
        public Guid SubsidiaryAccountId { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Remark { get; set; }
        public bool IsReconciled { get; set; }
        public Guid? ReferenceVoucherHeaderId { get; set; }
        public Guid? BankReconciliationId { get; set; }
        public bool IsInterBranchTransactionCleared { get; set; }
        public Guid? IBTReferenceVoucherHeaderId { get; set; }
        public Guid? ProjectTaskId { get; set; }

        public virtual IfmsCostCode IfmsCostCode { get; set; }
        public virtual CoreCostCenter CoreCostCenter { get; set; }
        public virtual CoreSubsidiaryAccount CoreSubsidiaryAccount { get; set; }
        public virtual CoreControlAccount CoreControlAccount { get; set; }
    //public virtual IfmsVoucherHeader IfmsVoucherHeaders { get; set; }
    //public virtual IfmsVoucherHeader IfmsVoucherHeaders_2 { get; set; }


}
}
