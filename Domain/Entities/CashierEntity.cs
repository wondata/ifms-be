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
        public byte[] LastUpdated { get; set; }

        public SubsidiaryAccountEntity SubsidiaryAccount { get; set; }

        public CashierEntity()
        {
        }

        public CashierEntity(IfmsCashier ifmsCashier)
        {
            this.Id = ifmsCashier.Id;
            this.FullName = ifmsCashier.FullName;
            this.SubsidiaryAccountId = ifmsCashier.SubsidiaryAccountId;
            this.UserId = ifmsCashier.UserId;
            this.IsDeleted = ifmsCashier.IsDeleted;
            this.LastUpdated = ifmsCashier.LastUpdated;
            this.SubsidiaryAccount = new SubsidiaryAccountEntity(ifmsCashier.SubsidiaryAccount);
        }
    }
}
