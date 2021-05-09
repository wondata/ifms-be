using Application.Interfaces.Repositories;
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

        Task<List<CostCodeEntity>> GetCostCodes();

        Task<IEnumerable<CostCenterEntity>> GetCostCenters();

        Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSettings();

        Task<List<VoucherTypeEntity>> GetVoucherTypes();

        Task SaveSetting(SettingEntity settingEntity);

        Task SaveFixedAssetSetting(FixedAssetSettingEntity fixedAssetSetting);

        Task<SettingEntity> GetSetting(Guid id);
    }
}
