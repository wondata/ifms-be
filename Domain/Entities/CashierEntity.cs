using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CashierEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Guid UserId { get; set; }
        public Guid SubsidiaryAccountId { get; set; }
        public bool IsDeleted { get; set; }      
        public SubsidiaryAccountEntity SubsidiaryAccount { get; set; }
        public UserEntity User { get; set; }

        public CashierEntity()
        {
        }

        public CashierEntity(IfmsCashier ifmsCashier)
        {
            if (ifmsCashier == null) return;

            this.Id = ifmsCashier.Id;
            this.FullName = ifmsCashier.FullName;
            this.SubsidiaryAccountId = ifmsCashier.SubsidiaryAccountId;
            this.UserId = ifmsCashier.UserId;           
            this.IsDeleted = ifmsCashier.IsDeleted;
            this.SubsidiaryAccount = new SubsidiaryAccountEntity(ifmsCashier.SubsidiaryAccount);
            this.User = new UserEntity(ifmsCashier.User);
        }

        public IfmsCashier MapToModel()
        {
            IfmsCashier ifmsCashier= new IfmsCashier
            {
                Id = this.Id,
                FullName = this.FullName,
                UserId = this.UserId,
                SubsidiaryAccountId = this.SubsidiaryAccountId,
            };

           
            return ifmsCashier;
        }

        public IfmsCashier MapToModel(IfmsCashier ifmsCashier)
        {
            ifmsCashier.Id = this.Id;
            ifmsCashier.FullName = this.FullName;
            ifmsCashier.UserId = this.UserId;
            ifmsCashier.SubsidiaryAccountId = this.SubsidiaryAccountId;

            return ifmsCashier;
        }

    }
}
