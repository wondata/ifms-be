using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class SubsidiaryAccountEntity 
    {
        public Guid Id { get; set; }
        public Guid ControlAccountId { get; set; }
        public Guid BalanceSideId { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? CreatedBy { get; set; }
        public decimal RunningBalance { get; set; }
        public Guid? SubsidiaryAccountTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ControlAccountEntity ControlAccount { get; set; }

        public SubsidiaryAccountEntity()
        {
        }

        public SubsidiaryAccountEntity(CoreSubsidiaryAccount coreSubsidiary)
        {
            if (coreSubsidiary == null) return;

            this.Id = coreSubsidiary.Id;
            this.ControlAccountId = coreSubsidiary.ControlAccountId;
            this.BalanceSideId = coreSubsidiary.BalanceSideId;
            this.Name = coreSubsidiary.Name;
            this.Code = coreSubsidiary.Code;
            this.value = coreSubsidiary.Id.ToString();
            this.text = string.Format("{0}-{1}", coreSubsidiary.ControlAccount.Code, coreSubsidiary.Code);
            this.RunningBalance = coreSubsidiary.RunningBalance;
            this.CreatedBy = coreSubsidiary.CreatedBy;
            this.SubsidiaryAccountTypeId = coreSubsidiary.SubsidiaryAccountTypeId;
            this.IsDeleted = coreSubsidiary.IsDeleted;
            this.CreatedAt = coreSubsidiary.CreatedAt;
            this.UpdatedAt = coreSubsidiary.UpdatedAt;
            this.ControlAccount = new ControlAccountEntity(coreSubsidiary.ControlAccount);
        }

       
    }
}
