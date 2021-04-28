using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IFinancialSetupManager
    {
        Task<IEnumerable<ChartOfAccountEntity>> GetChartOfAccount();

        Task<IEnumerable<CostCodeEntity>> GetCostCodes();

        Task<IEnumerable<CostCenterEntity>> GetCostCenters();

        Task<List<VoucherTypeSettingEntity>> GetVoucherTypeSettings();

        Task<List<VoucherTypeEntity>> GetVoucherTypes();

        Task SaveSetting(SettingEntity settingEntity);

        Task SaveFixedAssetSetting(FixedAssetSettingEntity fixedAssetSetting);

        Task<IfmsSetting> GetSetting(Guid id);

    }
}
