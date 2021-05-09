using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public partial class VoucherTypeSettingEntity 
    {
        public VoucherTypeSettingEntity()
        {
        }
        public Guid Id { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid VoucherTypeId { get; set; }
        public Guid? DefaultAccountId { get; set; }
        public Guid? BalanceSideId { get; set; }
        public int StartingNumber { get; set; }
        public int EndingNumber { get; set; }
        public int CurrentNumber { get; set; }
        public int NumberOfDigits { get; set; }
        public CostCenterEntity CostCenter { get; set; }
        public SubsidiaryAccountEntity SubsidiaryAccount { get; set; }
        public VoucherTypeEntity VoucherType { get; set; }


        public VoucherTypeSettingEntity(IfmsVoucherTypeSetting ifmsVoucherType)
        {
            if (ifmsVoucherType == null) return;

            this.Id = ifmsVoucherType.Id;               
            this.StartingNumber = ifmsVoucherType.StartingNumber;
            this.EndingNumber   = ifmsVoucherType.EndingNumber;
            this.CurrentNumber  = ifmsVoucherType.CurrentNumber;          
            this.CostCenter = new CostCenterEntity(ifmsVoucherType.CoreCostCenter);
            this.SubsidiaryAccount = new SubsidiaryAccountEntity(ifmsVoucherType.CoreSubsidiaryAccount);
            this.VoucherType = new VoucherTypeEntity(ifmsVoucherType.LupVoucherType);
        }
        
    }
}
