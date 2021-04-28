using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.PostModel
{
    public class SettingPostModel
    {
        public Guid CurrentFiscalYearId { get; set; }
        public Guid CurrentPeriodId { get; set; }
        public Guid IncomeSummaryAccountId { get; set; }
        public Guid ClosingCapitalAccountId { get; set; }
        public Guid DefaultCostCenterId { get; set; }
        public Guid CompanyTaxId { get; set; }
        public Guid InterBranchControlAccountId { get; set; }


        public SettingEntity MapToEntity()
        {
            SettingEntity settingEntity = new SettingEntity
            {
                CurrentFiscalYearId = this.CurrentFiscalYearId,
                CurrentPeriodId = this.CurrentPeriodId,
                IncomeSummaryAccountId = this.IncomeSummaryAccountId,
                ClosingCapitalAccountId = this.ClosingCapitalAccountId,
                DefaultCostCenterId = this.DefaultCostCenterId,
                CompanyTaxId = this.CompanyTaxId,
                InterBranchControlAccountId = this.InterBranchControlAccountId
            };

            return settingEntity;
        }
    }
}
