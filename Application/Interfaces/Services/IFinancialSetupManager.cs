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
        Task<IEnumerable<SubsidiaryAccountEntity>> GetSubsidiaryAccountList();
        Task<IEnumerable<ControlAccountEntity>> GetControlAccountList();
        Task<IEnumerable<ControlAccountEntity>> GetControlAccountsByParam(string accountCode);
        Task<IEnumerable<CostCenterEntity>> GetFilteredCostCenters(string filteredCostCenter);
        Task<List<CostCodeEntity>> GetCostCodes();
        Task SaveCostCode(CostCodeEntity costCode);
        Task DeleteCostCode(Guid id);
        Task SaveCashier(CashierEntity costCode);
        Task DeleteCashier(Guid id);
        Task<IEnumerable<UserEntity>> GetUserList();
        Task<IEnumerable<CostCenterEntity>> GetCostCenters();
        Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSettings();
        Task DeleteVoucherTypeSetting( Guid Id);
        Task SaveVoucherTypesSetting(VoucherTypeSettingEntity voucherTypeSetting);
        Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSettingByParam(Guid id);
        Task<List<VoucherTypeEntity>> GetVoucherTypes();
        Task<IEnumerable<VoucherTypeEntity>> GetPaymentVoucherTypes();
        Task<IEnumerable<VoucherTypeEntity>> GetCollectionVoucherTypes();

        Task SaveSetting(SettingEntity settingEntity);
        Task SaveFixedAssetSetting(FixedAssetSettingEntity fixedAssetSetting);
        Task<IEnumerable<SettingEntity>> GetSettings();
        Task<SettingEntity> GetSetting(Guid id);
        Task<IEnumerable<CashierEntity>> GetCashiers();      
        
      
        Task<IEnumerable<PurposeTemplateEntity>> GetPurposeTemplate(string searchText);
        Task<IEnumerable<PaymentRequest>> GetApprovedPaymentRequest();

        Task<IEnumerable<PurposeTemplateEntity>> GetAllPurposeTemplates();

    }
}
