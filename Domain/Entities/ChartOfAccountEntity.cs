﻿using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public partial class ChartOfAccountEntity: BaseEntity
    {
        public static string ACCOUNT_TYPE = "Account Type";
        public static string ACCOUNT_GROUP = "Account Group";
        public static string SUBSIDIARY_ACCOUNT = "Subsidiary Account";

        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }

        public List<ChartOfAccountEntity> Children { get; set; }

        public ChartOfAccountEntity()
        {

        }

        public ChartOfAccountEntity(CoreAccountType accountType)
        {
            if (accountType == null) return;

            this.Id = accountType.Id.ToString();
            this.Name = accountType.Name;
            this.Code = accountType.Code;
            this.Type = ACCOUNT_TYPE;

            this.Children =
                accountType.CoreAccountGroup == null ? new List<ChartOfAccountEntity>() :
                accountType.CoreAccountGroup.Select(x => new ChartOfAccountEntity(x)).ToList();
        }

        public ChartOfAccountEntity(CoreAccountGroup accountGroup)
        {
            if (accountGroup == null) return;

            this.Id = accountGroup.Id.ToString();
            this.Name = accountGroup.Name;
            this.Code = accountGroup.Code;
            this.Type = ACCOUNT_GROUP;

            //this.Children =
            //    accountGroup.CoreControlAccount == null ? new List<ChartOfAccountEntity>() :
            //    accountGroup.CoreControlAccount.Select(x => new ChartOfAccountEntity(x)).ToList();
        }


        //public ChartOfAccountEntity(CoreSubsidiaryAccount subsidiaryAccount)
        //{
        //    if (subsidiaryAccount == null) return;

        //    this.Id = subsidiaryAccount.Id.ToString();
        //    this.Name = subsidiaryAccount.Name;
        //    this.Code = subsidiaryAccount.Code;
        //    this.Type = SUBSIDIARY_ACCOUNT;

        //    this.Children = new List<ChartOfAccountEntity>();
        //}

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
