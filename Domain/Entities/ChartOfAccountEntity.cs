using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public partial class ChartOfAccountEntity: BaseEntity
    {
        public static string ACCOUNT_CATEGORY = "Account Category";
        public static string ACCOUNT_TYPE = "Account Type";
        public static string ACCOUNT_GROUP = "Account Group";
        
        public static string CHART_ACCOUNT = "Chart Account";
        public static string CONTROL_ACCOUNT = "Control Account";
        public static string SUBSIDIARY_ACCOUNT = "Subsidiary Account";


        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? CompanyId { get; set; }
        public List<ChartOfAccountEntity> Children { get; set; }

        public ChartOfAccountEntity()
        {

        }

        //public ChartOfAccountEntity(CoreAccountType accountType)
        //{
        //    if (accountType == null) return;

        //    this.Id = accountType.Id.ToString();
        //    this.Name = accountType.Name;
        //    this.Code = accountType.Code;
        //    this.Type = ACCOUNT_TYPE;

        //    this.Children =
        //        accountType.CoreAccountGroup == null ? new List<ChartOfAccountEntity>() :
        //        accountType.CoreAccountGroup.Select(x => new ChartOfAccountEntity(x)).ToList();
        //}

        //public ChartOfAccountEntity(CoreAccountGroup accountGroup)
        //{
        //    if (accountGroup == null) return;

        //    this.Id = accountGroup.Id.ToString();
        //    this.Name = accountGroup.Name;
        //    this.Code = accountGroup.Code;
        //    this.Type = ACCOUNT_GROUP;

        //    //this.Children =
        //    //    accountGroup.CoreControlAccount == null ? new List<ChartOfAccountEntity>() :
        //    //    accountGroup.CoreControlAccount.Select(x => new ChartOfAccountEntity(x)).ToList();
        //}       

        public ChartOfAccountEntity(CoreChartOfAccount chartOfAccount)
        {
            if (chartOfAccount == null) return;

            this.Id = chartOfAccount.Id.ToString();
            this.Name = chartOfAccount.Name;
            this.Code = chartOfAccount.Code;
            this.Type = CHART_ACCOUNT;

            this.Children =
                chartOfAccount.CoreControlAccounts == null ? new List<ChartOfAccountEntity>() :
                chartOfAccount.CoreControlAccounts.Select(x => new ChartOfAccountEntity(x)).ToList();
        }

        public ChartOfAccountEntity(CoreControlAccount controlAccount)
        {
            if (controlAccount == null) return;

            this.Id = controlAccount.Id.ToString();
            this.Name = controlAccount.Name;
            this.Code = controlAccount.Code;
            this.Type = CONTROL_ACCOUNT;

            this.Children =
                controlAccount.CoreSubsidiaryAccounts == null ? new List<ChartOfAccountEntity>() :
                controlAccount.CoreSubsidiaryAccounts.Select(x => new ChartOfAccountEntity(x)).ToList();
        }


        public ChartOfAccountEntity(CoreSubsidiaryAccount subsidiaryAccount)
        {
            if (subsidiaryAccount == null) return;

            this.Id = subsidiaryAccount.Id.ToString();
            this.Name = subsidiaryAccount.Name;
            this.Code = subsidiaryAccount.Code;
            this.Type = SUBSIDIARY_ACCOUNT;

            this.Children = new List<ChartOfAccountEntity>();
        }

        public override T MapToModel<T>()
        {
            throw new NotImplementedException();
        }

        public override T MapToModel<T>(T t)
        {
            throw new NotImplementedException();
        }

        public override T MapToModel<T>(T t, String type)
        {
            if (type == ChartOfAccountEntity.ACCOUNT_CATEGORY)
            {
                CoreAccountType coreAccount = t as CoreAccountType;
                Guid.TryParse(this.Id, out Guid id);

                coreAccount.Id = id;
                coreAccount.Name = this.Name;
                coreAccount.Code = this.Code;
                return coreAccount as T;

            }
            else if (type == ChartOfAccountEntity.ACCOUNT_GROUP)
            {
                CoreControlAccount coreAccount = t as CoreControlAccount;
                Guid.TryParse(this.Id, out Guid id);

                coreAccount.Id = id;
                coreAccount.Name = this.Name;
                coreAccount.Code = this.Code;
                return coreAccount as T;

            }
            else if (type == ChartOfAccountEntity.ACCOUNT_TYPE)
            {
                CoreAccountGroup coreAccount = t as CoreAccountGroup;
                Guid.TryParse(this.Id, out Guid id);

                coreAccount.Id = id;
                coreAccount.Name = this.Name;
                coreAccount.Code = this.Code;
                return coreAccount as T;

            }
            else if (type == ChartOfAccountEntity.CONTROL_ACCOUNT)
            {
                CoreSubsidiaryAccount coreAccount = t as CoreSubsidiaryAccount;
                Guid.TryParse(this.Id, out Guid id);

                coreAccount.Id = id;
                coreAccount.Name = this.Name;
                coreAccount.Code = this.Code;
                return coreAccount as T;

            }
            else if (type == ChartOfAccountEntity.CHART_ACCOUNT)
            {
                CoreChartOfAccount coreChart = t as CoreChartOfAccount;
                Guid.TryParse(this.Id, out Guid id);
                

                coreChart.Id = id;
                coreChart.Name = this.Name;
                coreChart.Code = this.Code;
                coreChart.CompanyId = this.CompanyId;
                coreChart.ParentId = this.ParentId;

                return coreChart as T;
            }

            return null;


        }

        public override T MapToModel<T>(string type)
        {

            if (type == ChartOfAccountEntity.ACCOUNT_CATEGORY)
            {
                CoreAccountType coreAccount = new CoreAccountType
                {
                  

                    Id = Guid.NewGuid(),
                    Name = this.Name,
                    Code = this.Code,
                    AccountCategoryId = this.ParentId
                };
                return coreAccount as T;

            }
            else if (type == ChartOfAccountEntity.ACCOUNT_GROUP)
            {
                CoreControlAccount coreAccount = new CoreControlAccount
                {
                    Id = Guid.NewGuid(),
                    Name = this.Name,
                    Code = this.Code,
                    //AccountGroupId = this.ParentId
                };
                return coreAccount as T;

            }
            else if (type == ChartOfAccountEntity.ACCOUNT_TYPE)
            {
                CoreAccountGroup coreAccount = new CoreAccountGroup
                {

                    Id = Guid.NewGuid(),
                    Name = this.Name,
                    Code = this.Code,
                    AccountTypeId = this.ParentId
                };
                return coreAccount as T;


            }
            else if (type == ChartOfAccountEntity.CONTROL_ACCOUNT)
            {
                CoreSubsidiaryAccount coreAccount = new CoreSubsidiaryAccount
                {
                    Id = Guid.NewGuid(),
                    Name = this.Name,
                    Code = this.Code,
                    ControlAccountId = this.ParentId.Value
                };
                return coreAccount as T;

            }
            else if (type == ChartOfAccountEntity.CHART_ACCOUNT)
            {
                CoreChartOfAccount coreChart = new CoreChartOfAccount
                {
                    Id = Guid.NewGuid(),
                    Name = this.Name,
                    Code = this.Code,
                    ParentId = this.ParentId.Value,
                    CompanyId = this.CompanyId.Value
                };
                return coreChart as T;

            }
            return null as T;

        }
    }
}
