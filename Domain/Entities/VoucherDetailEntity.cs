using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class VoucherDetailEntity : BaseEntity
    {
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

        public CostCenterEntity CostCenter{ get; set; }
        public CostCodeEntity CostCode { get; set; }
        public SubsidiaryAccountEntity Subsidiary { get; set; }
        public ControlAccountEntity Account{ get; set; }

        public VoucherDetailEntity()
        {
        }

        public VoucherDetailEntity(IfmsVoucherDetail ifmsVoucher)
        {
            if (ifmsVoucher == null) return;
            this.Id = ifmsVoucher.Id.ToString();
            this.DebitAmount = ifmsVoucher.DebitAmount;
            this.CreditAmount = ifmsVoucher.CreditAmount;
            this.IsInterBranchTransactionCleared = ifmsVoucher.IsInterBranchTransactionCleared;
            this.IBTReferenceVoucherHeaderId = ifmsVoucher.IBTReferenceVoucherHeaderId;
            this.CostCenter = new CostCenterEntity(ifmsVoucher.CoreCostCenter);
            this.CostCode = new CostCodeEntity(ifmsVoucher.IfmsCostCode);
            this.Subsidiary = new SubsidiaryAccountEntity(ifmsVoucher.CoreSubsidiaryAccount);
            this.Account = new ControlAccountEntity(ifmsVoucher.CoreControlAccount);
        }

        public override T MapToModel<T>()
        {
            throw new NotImplementedException();
        }

        public override T MapToModel<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}
