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

        public  CostCenterEntity CostCenters { get; set; }
        public  CostCodeEntity CostCodes { get; set; }
        public  SubsidiaryAccountEntity SubsidiaryAccount { get; set; }
        public  ControlAccountEntity ControlAccount{ get; set; }


        public VoucherDetailEntity()
        {
        }

        public VoucherDetailEntity(IfmsVoucherDetail ifmsVoucher)
        {
            if (ifmsVoucher == null) return;

            this.Id = ifmsVoucher.Id.ToString();
            this.VoucherHeaderId = ifmsVoucher.VoucherHeaderId;
            this.SNo = ifmsVoucher.SNo;
            this.CostCenterId = ifmsVoucher.CostCenterId;
            this.CaseId = ifmsVoucher.CaseId;
            this.ControlAccountId = ifmsVoucher.ControlAccountId;
            this.SubsidiaryAccountId = ifmsVoucher.SubsidiaryAccountId;

            this.DebitAmount = ifmsVoucher.DebitAmount;
            this.CreditAmount = ifmsVoucher.CreditAmount;
            this.Remark = ifmsVoucher.Remark;
            this.IsReconciled = ifmsVoucher.IsReconciled;
            this.IsInterBranchTransactionCleared = ifmsVoucher.IsInterBranchTransactionCleared;
            this.IBTReferenceVoucherHeaderId = ifmsVoucher.IBTReferenceVoucherHeaderId;
            this.ProjectTaskId = ifmsVoucher.ProjectTaskId;

            this.IsDeleted = ifmsVoucher.IsDeleted;

            this.CostCenters = new CostCenterEntity(ifmsVoucher.CostCenter);
            this.CostCodes = new CostCodeEntity(ifmsVoucher.CostCode);
            this.SubsidiaryAccount = new SubsidiaryAccountEntity(ifmsVoucher.SubsidiaryAccount);
            this.ControlAccount = new ControlAccountEntity(ifmsVoucher.ControlAccount);
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
