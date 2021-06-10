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
        Task<IEnumerable<SubsidiaryAccountEntity>> GetSubsidiaryAccounts(string accountCode);
        Task<IEnumerable<ControlAccountEntity>> GetControlAccountsByParam(string accountCode);
        Task<IEnumerable<CostCenterEntity>> GetFilteredCostCenters(string filteredCostCenter);
        Task<List<CostCodeEntity>> GetCostCodes();
        Task<IEnumerable<CostCenterEntity>> GetCostCenters();
        Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSettings();
        Task<List<VoucherTypeEntity>> GetVoucherTypes();
        Task SaveSetting(SettingEntity settingEntity);
        Task SaveFixedAssetSetting(FixedAssetSettingEntity fixedAssetSetting);
        Task<IEnumerable<SettingEntity>> GetSettings();
        Task<SettingEntity> GetSetting(Guid id);
        Task<IEnumerable<CashierEntity>> GetCashiers();      
        Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails();
        Task<IEnumerable<VoucherHeaderEntity>> GetAllHeaders(int start, int limit, string sort, string dir, string record);
        Task<IEnumerable<VoucherHeaderEntity>> GetVoucher(Guid id);
        Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails(Guid id);
        Task<IEnumerable<VoucherHeaderEntity>> GetVoucherHeaders();
    }
}
