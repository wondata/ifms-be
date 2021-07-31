using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsVoucherDetail
    {
        public IfmsVoucherDetail()
        {
            IfmsBankReconciliationDetails = new HashSet<IfmsBankReconciliationDetail>();
        }

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
        public bool IsDeleted { get; set; }

        public virtual IfmsCostCode CostCode { get; set; }
        public virtual CoreCostCenter CostCenter { get; set; }
        public virtual CoreSubsidiaryAccount SubsidiaryAccount { get; set; }
        public virtual CoreControlAccount ControlAccount { get; set; }
        public virtual ICollection<IfmsBankReconciliationDetail> IfmsBankReconciliationDetails { get; set; }
    //public virtual IfmsVoucherHeader IfmsVoucherHeaders { get; set; }
    //public virtual IfmsVoucherHeader IfmsVoucherHeaders_2 { get; set; }


}
}
