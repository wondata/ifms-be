using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public partial class VoucherTypeSettingEntity
    {
        public int Id { get; set; }
        public int CostCenterId { get; set; }
        public int VoucherTypeId { get; set; }
        public int? DefaultAccountId { get; set; }
        public int? BalanceSideId { get; set; }
        public int StartingNumber { get; set; }
        public int EndingNumber { get; set; }
        public int CurrentNumber { get; set; }
        public int NumberOfDigits { get; set; }
        // public bool? IsDeleted { get; set; }
        //public DateTime? LastUpdated { get; set; }
        public IEnumerable<CostCenterEntity> CostCenters{ get; set; }



        public VoucherTypeSettingEntity()
        {
        }

        public VoucherTypeSettingEntity(IfmsVoucherTypeSetting ifmsVoucherType)
        {
            if (ifmsVoucherType == null) return;

            this.Id = ifmsVoucherType.Id;            
            this.CostCenterId = ifmsVoucherType.CostCenterId;
            this.VoucherTypeId = ifmsVoucherType.VoucherTypeId;
            this.DefaultAccountId = ifmsVoucherType.DefaultAccountId;
            this.BalanceSideId = ifmsVoucherType.BalanceSideId;
            this.StartingNumber = ifmsVoucherType.StartingNumber;
            this.EndingNumber = ifmsVoucherType.EndingNumber;
            this.CurrentNumber = ifmsVoucherType.CurrentNumber;
            this.CostCenters = ifmsVoucherType.CoreCostCenters_2.Select(x => new CostCenterEntity(x.Parent));

        }
    }
}
