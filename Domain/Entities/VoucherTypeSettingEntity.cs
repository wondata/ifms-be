using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public partial class VoucherTypeSettingEntity : BaseEntity
    {
        public VoucherTypeSettingEntity()
        {
        }
        
        public Guid CostCenterId { get; set; }
        public Guid VoucherTypeId { get; set; }
        public Guid? DefaultAccountId { get; set; }
        public Guid? BalanceSideId { get; set; }
        public int? StartingNumber { get; set; }
        public int? EndingNumber { get; set; }
        public int? CurrentNumber { get; set; }
        public int NumberOfDigits { get; set; }
   
        public CostCenterEntity CostCenter { get; set; }
        public SubsidiaryAccountEntity SubsidiaryAccount { get; set; }
        public VoucherTypeEntity VoucherType { get; set; }
        public LookupEntity BalanceSide { get; set; }

        public VoucherTypeSettingEntity(IfmsVoucherTypeSetting ifmsVoucherType)
        {
            if (ifmsVoucherType == null) return;

            this.Id = ifmsVoucherType.Id.ToString();
            this.CostCenterId  = ifmsVoucherType.CostCenterId;
            this.VoucherTypeId = ifmsVoucherType.VoucherTypeId;
            this.DefaultAccountId = ifmsVoucherType.DefaultAccountId;
            this.BalanceSideId  = ifmsVoucherType.BalanceSideId;
            this.StartingNumber = ifmsVoucherType.StartingNumber;
            this.EndingNumber   = ifmsVoucherType.EndingNumber;
            this.CurrentNumber  = ifmsVoucherType.CurrentNumber;          
            this.CostCenter = new CostCenterEntity(ifmsVoucherType.CoreCostCenter);
            this.SubsidiaryAccount = new SubsidiaryAccountEntity(ifmsVoucherType.CoreSubsidiaryAccount);
            this.VoucherType = new VoucherTypeEntity(ifmsVoucherType.LupVoucherType);

            this.BalanceSide = LookupEntity.Map(ifmsVoucherType.LupBalanceSide);


        }



        public override T MapToModel<T>()
        {
            Guid id;
            Guid.TryParse(this.Id, out id);

            IfmsVoucherTypeSetting voucherType = new IfmsVoucherTypeSetting
            {
                Id = id,
                CostCenterId = CostCenterId,
                VoucherTypeId = VoucherTypeId,
                DefaultAccountId = DefaultAccountId,
                BalanceSideId = BalanceSideId,
                StartingNumber = this.StartingNumber,
                EndingNumber = this.EndingNumber,
                CurrentNumber = this.CurrentNumber,
                NumberOfDigits = this.NumberOfDigits,
                IsDeleted = this.IsDeleted,
            };

            return voucherType as T;
        }

        public override T MapToModel<T>(T t)
        {
            IfmsVoucherTypeSetting voucherType = t as IfmsVoucherTypeSetting;
            
                Id = this.Id;
                CostCenterId = CostCenterId;
                VoucherTypeId = VoucherTypeId;
                DefaultAccountId = DefaultAccountId;
                BalanceSideId = BalanceSideId;
                StartingNumber = this.StartingNumber;
                EndingNumber = this.EndingNumber;
                CurrentNumber = this.CurrentNumber;
                NumberOfDigits = this.NumberOfDigits;
                IsDeleted = this.IsDeleted;

            return voucherType as T;
        }

        public override T MapToModel<T>(string Type)
        {
            throw new NotImplementedException();
        }

        public override T MapToModel<T>(T t, string tyoe)
        {
            throw new NotImplementedException();
        }
    }
}
