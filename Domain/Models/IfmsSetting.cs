using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class IfmsSetting
    {
        public Guid Id { get; set; }
        public Guid? CurrentFiscalYearId { get; set; }
        public Guid? CurrentPeriodId { get; set; }
        public Guid? IncomeSummaryAccountId { get; set; }
        public Guid? ClosingCapitalAccountId { get; set; }
        public Guid? DefaultCostCenterId { get; set; }
        public Guid? CompanyTaxId { get; set; }
        public Guid? InterBranchControlAccountId { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual CoreControlAccount CoreControlAccounts {get; set;}
        public virtual CoreCostCenter CoreCostCenter { get; set; }
        //public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts { get; set; }
        //public virtual CoreSubsidiaryAccount CoreSubsidiaryAccounts_2 { get; set; }
    }
}
