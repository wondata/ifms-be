using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class VoucherDetailEntity
    {
        public Guid Id { get; set; }
        public Guid VoucherHeaderId { get; set; }
        public int CostCenterId { get; set; }
        public int CostCodeId { get; set; }
        public int ControlAccountId { get; set; }
        public Guid? SubsidiaryAccountId { get; set; }
        public float DebitAmount { get; set; }
        public float CreditAmount { get; set; }
        public bool? IsInterBranchTransactionCleared { get; set; }
        public Guid IBTReferenceVoucherHeaderId { get; set; }

        public VoucherDetailEntity()
        {
        }

        public VoucherDetailEntity(IfmsVoucherDetail coreVoucher)
        {
            if (coreVoucher == null) return;
            //this.Id = coreVoucher.Id;
            //this.VoucherHeaderId = coreVoucher.VoucherHeaderId.ToString();
            //this.CostCenterId = coreVoucher.CostCenterId;
            //this.CostCodeId = coreVoucher.CostCodeId;
            //this.ControlAccountId = coreVoucher.ControlAccountId;
           // this.SubsidiaryAccountId = coreVoucher.SubsidiaryAccountId;
            this.DebitAmount = coreVoucher.DebitAmount;
            this.CreditAmount = coreVoucher.CreditAmount;
            this.IsInterBranchTransactionCleared = coreVoucher.IsInterBranchTransactionCleared;
            //this.IBTReferenceVoucherHeaderId = coreVoucher.IBTReferenceVoucherHeaderId;
        }





    }
}
