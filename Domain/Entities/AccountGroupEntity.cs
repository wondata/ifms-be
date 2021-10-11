using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class AccountGroupEntity: BaseEntity
    {
        public AccountGroupEntity()
        {
        }

        public Guid? AccountTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public AccountGroupEntity(CoreAccountGroup accountGroup)
        {
            if (accountGroup == null) return;

            this.Id = accountGroup.Id.ToString();
            this.AccountTypeId = accountGroup.AccountTypeId;
            this.Name = accountGroup.Name;
            this.Code = accountGroup.Code;
        }

        public override T MapToModel<T>()
        {
            Guid id;
            Guid.TryParse(this.Id.ToString(), out id);

            CoreAccountGroup coreAccountGroup = new CoreAccountGroup
            {
                Id = Guid.NewGuid(),
                AccountTypeId = this.AccountTypeId,
                Name = this.Name,
                Code = this.Code
            };

            return coreAccountGroup as T;
        }

        public override T MapToModel<T>(T t)
        {
            CoreAccountGroup coreAccountGroup = t as CoreAccountGroup;

            coreAccountGroup.AccountTypeId = this.AccountTypeId;
            coreAccountGroup.Name = this.Name;
            coreAccountGroup.Code = this.Code;
            coreAccountGroup.UpdatedAt = DateTime.Now;

            return coreAccountGroup as T;
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
