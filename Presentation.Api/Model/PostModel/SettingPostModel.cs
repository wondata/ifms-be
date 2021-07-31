using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.PostModel
{
    public class SettingPostModel
    {
        public string CurrentFiscalYearId { get; set; }
        public string CurrentPeriodId { get; set; }
        public string IncomeSummaryAccountId { get; set; }
        public string ClosingCapitalAccountId { get; set; }
        public string DefaultCostCenterId { get; set; }
        public string CompanyTaxId { get; set; }
        public string InterBranchControlAccountId { get; set; }


        public SettingEntity MapToEntity()
        {
            Guid CurrentFiscalYearIds;
            Guid.TryParse(this.CurrentFiscalYearId, out CurrentFiscalYearIds);
            Guid ClosingCapitalAccountIds;
            Guid.TryParse(this.ClosingCapitalAccountId, out ClosingCapitalAccountIds);
            Guid DefaultCostCenterIds;
            Guid.TryParse(this.DefaultCostCenterId, out DefaultCostCenterIds);
            Guid InterBranchControlAccountIds;
            Guid.TryParse(this.InterBranchControlAccountId, out InterBranchControlAccountIds);
            
            SettingEntity settingEntity = new SettingEntity
            {
                CurrentFiscalYearId = CurrentFiscalYearIds,       
                ClosingCapitalAccountId = ClosingCapitalAccountIds,
                DefaultCostCenterId = DefaultCostCenterIds,
                InterBranchControlAccountId = InterBranchControlAccountIds
            };

            return settingEntity;
        }
    }
}
