using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class CompanyEntity
    {
        public CompanyEntity()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string IconCls { get; set; }
        public Guid? ParentId { get; set; }
        public List<CompanyEntity> Children { get; set; }


        public CompanyEntity(CoreCompany coreCompany)
        {
            this.Id = coreCompany.Id;
            this.Name = coreCompany.Name;
            this.Code = coreCompany.Code;
            this.Description = coreCompany.Description;
            this.IconCls = coreCompany.IconCls;
            this.ParentId = coreCompany.ParentId;

            //this.Children =
            //   coreCompany.CoreControlAccounts == null ? new List<CompanyEntity>() :
            //   coreCompany.CoreControlAccounts.Select(x => new CompanyEntity(x)).ToList();
        }


        //public CompanyEntity(CoreControlAccount coreControl)
        //{
        //    this.Id = coreControl.Id;
        //    this.Name = coreControl.Name;
        //    this.Code = coreControl.Code;
        //    this.ParentId = coreControl.CompanyId;
        //    this.Children =
        //       coreControl.CoreSubsidiaryAccounts == null ? new List<CompanyEntity>() :
        //       coreControl.CoreSubsidiaryAccounts.Select(x => new CompanyEntity(x)).ToList();
        //}

        //public CompanyEntity(CoreSubsidiaryAccount coreSubsidiary)
        //{
        //    this.Id = coreSubsidiary.Id;
        //    this.Name = coreSubsidiary.Name;
        //    this.Code = coreSubsidiary.Code;
        //    this.ParentId = coreSubsidiary.ControlAccountId;
        //    this.Children = new List<CompanyEntity>();

        //}





    }
}
