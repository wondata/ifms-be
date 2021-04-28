using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SettingEntity 
    {
        public Guid Id { get; set; }
        public Guid CurrentFiscalYearId { get; set; }
        public Guid CurrentPeriodId { get; set; }
        public Guid IncomeSummaryAccountId { get; set; }
        public Guid ClosingCapitalAccountId { get; set; }
        public Guid DefaultCostCenterId { get; set; }
        public Guid CompanyTaxId { get; set; }
        public Guid InterBranchControlAccountId { get; set; }

        public SettingEntity()
        {
        }

        public SettingEntity(IfmsSetting ifmsSetting)
        {
            if (ifmsSetting == null) return;
            //this.Id = ifmsSetting.Id;
            //this.CurrentFiscalYearId = ifmsSetting.CurrentFiscalYearId;
            //this.CurrentPeriodId = ifmsSetting.CurrentPeriodId;
            //this.IncomeSummaryAccountId = ifmsSetting.IncomeSummaryAccountId;
            //this.ClosingCapitalAccountId = ifmsSetting.ClosingCapitalAccountId;
            //this.DefaultCostCenterId = ifmsSetting.DefaultCostCenterId;
           // this.CompanyTaxId = ifmsSetting.CompanyTaxId;
            //this.InterBranchControlAccountId = ifmsSetting.InterBranchControlAccountId;
        }

        public IfmsSetting MapToModel()
        {

            IfmsSetting ifmsSetting = new IfmsSetting
            {
                //Id = this.Id,
                CurrentFiscalYearId = this.CurrentFiscalYearId,
                CurrentPeriodId = this.CurrentPeriodId,
                IncomeSummaryAccountId = this.IncomeSummaryAccountId,
                ClosingCapitalAccountId = this.ClosingCapitalAccountId,
                DefaultCostCenterId = this.DefaultCostCenterId,
                CompanyTaxId = this.CompanyTaxId,
                InterBranchControlAccountId = this.InterBranchControlAccountId
            };

            return ifmsSetting;
        }

        public IfmsSetting MapToModel(IfmsSetting ifmsSetting)
        {
            //Id = this.Id,
            ifmsSetting.CurrentFiscalYearId = CurrentFiscalYearId;
            ifmsSetting.CurrentPeriodId = CurrentPeriodId;
            ifmsSetting.IncomeSummaryAccountId = IncomeSummaryAccountId;
            ifmsSetting.ClosingCapitalAccountId = ClosingCapitalAccountId;
            ifmsSetting.DefaultCostCenterId = DefaultCostCenterId;
            ifmsSetting.CompanyTaxId = CompanyTaxId;
            ifmsSetting.InterBranchControlAccountId = InterBranchControlAccountId;

            return ifmsSetting;
        }
    }
}
