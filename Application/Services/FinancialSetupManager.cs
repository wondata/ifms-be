using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CyberErp.CoreSetting.Core.Service
{
    public class FinancialSetupManager : IFinancialSetupManager
    {
        private readonly IFinancialRepository _financialRepository;
        private readonly IConfiguration _configuration;
        private readonly ILookupManager _lookupManager;

        public FinancialSetupManager(IFinancialRepository financialRepository, IConfiguration configuration, ILookupManager lookupManager)
        {
            _financialRepository = financialRepository;
            _configuration = configuration;
            _lookupManager = lookupManager;
        }

        public async Task<IEnumerable<ChartOfAccountEntity>> GetChartOfAccount()
        {
            IEnumerable<CoreChartOfAccount> coreChart = await this._financialRepository.GetChartOfAccount();          
            IEnumerable<ChartOfAccountEntity> chartOfAccounts = coreChart.Select(x => new ChartOfAccountEntity(x));

            return chartOfAccounts;
        }


        public async Task<List<ChartOfAccountEntity>> GetChildChartsOfAccount(string parentNodeId, string type)
        {
            List<ChartOfAccountEntity> chartOfAccounts = new List<ChartOfAccountEntity>();
            Guid parentId;
            Guid.TryParse(parentNodeId, out parentId);

            //if (type == ChartOfAccountEntity.ACCOUNT_CATEGORY)
            //{
            //    var accountTypes = await this._financialRepository.GetAccountTypes();
            //    accountTypes = accountTypes.Where(x => x.AccountCategoryId == parentId);
            //    chartOfAccounts = accountTypes.Select(x => new ChartOfAccountEntity(x)).ToList();
            //} 
            //else if (type == ChartOfAccountEntity.ACCOUNT_TYPE)
            //{
            //    var accountGroups = await this._financialRepository.GetAccountGroups();
            //    accountGroups = accountGroups.Where(x => x.AccountTypeId == parentId);
            //    chartOfAccounts = accountGroups.Select(x => new ChartOfAccountEntity(x)).ToList();
            //}
            if (type == ChartOfAccountEntity.CHART_ACCOUNT)
            {
                var chartCompnay = await this._financialRepository.GetChartOfAccounts();
                chartCompnay = chartCompnay.Where(x => x.Id == parentId);
                chartOfAccounts = chartCompnay.Select(x => new ChartOfAccountEntity(x)).ToList();
            }
            else if (type == ChartOfAccountEntity.ACCOUNT_GROUP)
            {
                var controlAccounts = await this._financialRepository.GetControlAccounts();
                controlAccounts = controlAccounts.Where(x => x.ChartAccountID == parentId);
                chartOfAccounts = controlAccounts.Select(x => new ChartOfAccountEntity(x)).ToList();
            }
            else if (type == ChartOfAccountEntity.CONTROL_ACCOUNT)
            {
                var subsidiaryAccounts = await this._financialRepository.GetSubsidiaryAccounts();
                subsidiaryAccounts = subsidiaryAccounts.Where(x => x.ControlAccountId == parentId);
                chartOfAccounts = subsidiaryAccounts.Select(x => new ChartOfAccountEntity(x)).ToList();
            }

            return chartOfAccounts;
        }

        public async Task SaveChildChartAccount(ChartOfAccountEntity chartOfAccount)
        {
            Guid id;
            Guid.TryParse(chartOfAccount.Id, out id);

            if (chartOfAccount.Type == ChartOfAccountEntity.ACCOUNT_CATEGORY)
            {

                CoreAccountType existingRecord = await this._financialRepository.GetAsync<CoreAccountType>(x => x.Id == id);

                if (existingRecord == null)
                {
                    CoreAccountType accountType = chartOfAccount.MapToModel<CoreAccountType>(chartOfAccount.Type);

                    await this._financialRepository.AddAsync(accountType);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }
                else
                {
                    CoreAccountType controlAccounts = await this._financialRepository.GetAsync<CoreAccountType>(x => x.Id == id);
                    CoreAccountType controlModel = chartOfAccount.MapToModel(controlAccounts, chartOfAccount.Type);

                    await this._financialRepository.UpdateAsync(controlModel);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }
            }
            else if (chartOfAccount.Type == ChartOfAccountEntity.ACCOUNT_TYPE)
            {
                CoreAccountGroup existingRecord = await this._financialRepository.GetAsync<CoreAccountGroup>(x => x.Id == id);

                if (existingRecord == null)
                {
                    CoreAccountGroup coreGroup = chartOfAccount.MapToModel<CoreAccountGroup>(chartOfAccount.Type);

                    await this._financialRepository.AddAsync(coreGroup);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }
                else
                {
                    CoreAccountGroup controlAccounts = await this._financialRepository.GetAsync<CoreAccountGroup>(x => x.Id == id);
                    CoreAccountGroup controlModel = chartOfAccount.MapToModel(controlAccounts, chartOfAccount.Type);

                    await this._financialRepository.UpdateAsync(controlModel);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }

            }
            else if (chartOfAccount.Type == ChartOfAccountEntity.ACCOUNT_GROUP)
            {

                CoreControlAccount existingRecord = await this._financialRepository.GetAsync<CoreControlAccount>(x => x.Id == id);

                if (existingRecord == null)
                {
                    CoreControlAccount controlAccount = chartOfAccount.MapToModel<CoreControlAccount>(chartOfAccount.Type);

                    await this._financialRepository.AddAsync(controlAccount);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }
                else
                {
                    CoreControlAccount controlAccounts = await this._financialRepository.GetAsync<CoreControlAccount>(x => x.Id == id);
                    CoreControlAccount controlModel = chartOfAccount.MapToModel(controlAccounts, chartOfAccount.Type);

                    await this._financialRepository.UpdateAsync(controlModel);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }

            }

            else if (chartOfAccount.Type == ChartOfAccountEntity.CONTROL_ACCOUNT)
            {
                CoreSubsidiaryAccount existingRecord = await this._financialRepository.GetAsync<CoreSubsidiaryAccount>(x => x.Id == id);

                if (existingRecord == null)
                {
                    
                    CoreSubsidiaryAccount coreSubsidiary = chartOfAccount.MapToModel<CoreSubsidiaryAccount>(chartOfAccount.Type);
                    
                    await this._financialRepository.AddAsync(coreSubsidiary);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }
                else
                {
                    CoreSubsidiaryAccount controlAccounts = await this._financialRepository.GetAsync<CoreSubsidiaryAccount>(x => x.Id == id);
                    CoreSubsidiaryAccount controlModel = chartOfAccount.MapToModel(controlAccounts, chartOfAccount.Type);
                    await this._financialRepository.UpdateAsync(controlModel);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }
            }
            else if (chartOfAccount.Type == ChartOfAccountEntity.CHART_ACCOUNT)
            {
                CoreChartOfAccount existingRecord = await this._financialRepository.GetAsync<CoreChartOfAccount>(x => x.Id == id);

                if (existingRecord == null)
                {
                    CoreChartOfAccount coreChart = chartOfAccount.MapToModel<CoreChartOfAccount>(chartOfAccount.Type);

                    await this._financialRepository.AddAsync(coreChart);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }
                else
                {
                    CoreChartOfAccount accountChart = await this._financialRepository.GetAsync<CoreChartOfAccount>(x => x.Id == id);
                    CoreChartOfAccount coreChart = chartOfAccount.MapToModel(accountChart, chartOfAccount.Type);
                    await this._financialRepository.UpdateAsync(coreChart);
                    await this._financialRepository.UnitOfWork.SaveChanges();
                }
            }
        }

        public async Task DeleteChildChartAccount(Guid id, string type)
        {

            //if (type == ChartOfAccountEntity.ACCOUNT_CATEGORY)
            //{
            //    await this._financialRepository.DeleteAsync<CoreAccountCategory>(x => x.Id == id);
            //    await this._financialRepository.UnitOfWork.SaveChanges();
            //}
             
            if (type == ChartOfAccountEntity.ACCOUNT_TYPE)
            {
                await this._financialRepository.DeleteAsync<CoreAccountType>(x => x.Id == id);
                await this._financialRepository.UnitOfWork.SaveChanges();

            }
            else if (type == ChartOfAccountEntity.ACCOUNT_GROUP)
            {
                await this._financialRepository.DeleteAsync<CoreAccountGroup>(x => x.Id == id);
                await this._financialRepository.UnitOfWork.SaveChanges();

            }
            else if (type == ChartOfAccountEntity.CONTROL_ACCOUNT)
            {
                await this._financialRepository.DeleteAsync<CoreControlAccount>(x => x.Id == id);
                await this._financialRepository.UnitOfWork.SaveChanges();

            }
            else if (type == ChartOfAccountEntity.SUBSIDIARY_ACCOUNT)
            {
                await this._financialRepository.DeleteAsync<CoreSubsidiaryAccount>(x => x.Id == id);
                await this._financialRepository.UnitOfWork.SaveChanges();

            }

        }


        public async Task<List<CostCodeEntity>> GetCostCodes()
        {
            IQueryable<IfmsCostCode> ifmsCost = await this._financialRepository.GetCostCodes();
            List<CostCodeEntity> costCodes = ifmsCost.Select(x => new CostCodeEntity(x)).ToList();

            return costCodes;
        }

        public async Task SaveCostCode(CostCodeEntity costCode)
        {
            IfmsCostCode existingRecord = await this._financialRepository.GetAsync<IfmsCostCode>(x => x.Id == costCode.Id);

            if (existingRecord == null)
            {
                IfmsCostCode costEntity = costCode.MapToModel();

                await this._financialRepository.AddAsync(costEntity);
                await this._financialRepository.UnitOfWork.SaveChanges();
            }
            else
            {
                IfmsCostCode ifmsCost = await this._financialRepository.GetAsync<IfmsCostCode>(x => x.Id == costCode.Id);
                IfmsCostCode ifmsCode = costCode.MapToModel(ifmsCost);

                await this._financialRepository.UpdateAsync(ifmsCode);
                await this._financialRepository.UnitOfWork.SaveChanges();
            }

        }

        public async Task DeleteCostCode(Guid id)
        {
            await this._financialRepository.DeleteAsync<IfmsCostCode>(x => x.Id == id);
            await this._financialRepository.UnitOfWork.SaveChanges();
        }

#region Cashier 
        public async Task SaveCashier(CashierEntity cashier)
        {
            IfmsCashier existingRecord = await this._financialRepository.GetAsync<IfmsCashier>(x => x.Id == cashier.Id && x.UserId == cashier.UserId);

            if (existingRecord == null)
            {
                IfmsCashier ifmsCashier = cashier.MapToModel();

                await this._financialRepository.AddAsync(ifmsCashier);
                await this._financialRepository.UnitOfWork.SaveChanges();
            }
            else
            {
                IfmsCashier ifmsCashier = await this._financialRepository.GetAsync<IfmsCashier>(x => x.Id == cashier.Id);
                IfmsCashier cashierIfms = cashier.MapToModel(ifmsCashier);

                await this._financialRepository.UpdateAsync(cashierIfms);
                await this._financialRepository.UnitOfWork.SaveChanges();
            }

        }

        public async Task DeleteCashier(Guid id)
        {
            await this._financialRepository.DeleteAsync<IfmsCashier>(x => x.Id == id);
            await this._financialRepository.UnitOfWork.SaveChanges();
        }

#endregion

        public async Task<IEnumerable<CostCenterEntity>> GetCostCenters()
        {
            IEnumerable<CoreCostCenter> coreCost = await this._financialRepository.GetCostCenters();
            // coreCost = coreCost.Where(x => x.ParentId == null).ToList();
            IEnumerable<CostCenterEntity> costCenter = coreCost.Select(x => new CostCenterEntity(x));

            return costCenter;
        }
        public async Task<IEnumerable<SettingEntity>> GetSettings()
        {
            IQueryable<IfmsSetting> ifmsSettings = await this._financialRepository.GetSettings();        
            IEnumerable<SettingEntity> settings = ifmsSettings.Select(x => new SettingEntity(x));

            return settings; 
        }
        public async Task<List<VoucherTypeEntity>> GetVoucherTypes()
        {
            IQueryable<LupVoucherType> lupVoucher = await this._financialRepository.GetAllAsync<LupVoucherType>();
            List<VoucherTypeEntity> voucherType = lupVoucher.Select(x => new VoucherTypeEntity(x)).ToList();

            return voucherType;
        }

        public async Task<IEnumerable<VoucherTypeEntity>> GetPaymentVoucherTypes()
        {
            IQueryable<LupVoucherType> lupVoucher = await this._financialRepository.GetAllAsync<LupVoucherType>();
            lupVoucher = lupVoucher.Where(x => x.Name == "CRV" || x.Name == "CSV" || x.Name == "CRSV" || x.Name == "BD");
            IEnumerable<VoucherTypeEntity> voucherType = lupVoucher.Select(x => new VoucherTypeEntity(x));

            return voucherType;
        }

        public async Task<IEnumerable<VoucherTypeEntity>> GetCollectionVoucherTypes()
        {
            IQueryable<LupVoucherType> lupVoucher = await this._financialRepository.GetAllAsync<LupVoucherType>();
            lupVoucher = lupVoucher.Where(x => x.Name == "CPV" || x.Name == "BPV" || x.Name == "PCPV");
            IEnumerable<VoucherTypeEntity> voucherType = lupVoucher.Select(x => new VoucherTypeEntity(x));

            return voucherType;
        }



        public async Task SaveVoucherTypesSetting(VoucherTypeSettingEntity voucherTypeSetting)
        {
            Guid.TryParse(voucherTypeSetting.Id, out Guid id);

            IfmsVoucherTypeSetting existingRecord = await this._financialRepository.GetAsync<IfmsVoucherTypeSetting>(x => x.Id == id);

            if (existingRecord == null )
            {
                IfmsVoucherTypeSetting voucherType = voucherTypeSetting.MapToModel<IfmsVoucherTypeSetting>();

                await this._financialRepository.AddAsync(voucherType);
                await this._financialRepository.UnitOfWork.SaveChanges();
            }
            else
            {
                IfmsVoucherTypeSetting ifmsVoucher = await this._financialRepository.GetAsync<IfmsVoucherTypeSetting>(x => x.Id == id);
                IfmsVoucherTypeSetting voucherType = voucherTypeSetting.MapToModel(ifmsVoucher);

                await this._financialRepository.UpdateAsync(voucherType);
                await this._financialRepository.UnitOfWork.SaveChanges();
            }

        }

        public async Task DeleteVoucherTypeSetting(Guid id)
        {
            await this._financialRepository.DeleteAsync<IfmsVoucherTypeSetting>(x => x.Id == id);
            await this._financialRepository.UnitOfWork.SaveChanges();
        }      


        public async Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSettings()
        {
            IEnumerable<IfmsVoucherTypeSetting> ifmsVoucher = await this._financialRepository.GetVoucherTypeSettings();
            IEnumerable<VoucherTypeSettingEntity> voucherType = ifmsVoucher.Select(x => new VoucherTypeSettingEntity(x));

            return voucherType;

        }

        public async Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSettingByParam(Guid id)
        {
            IQueryable<IfmsVoucherTypeSetting> ifmsVoucherTypes = await this._financialRepository.GetVoucherTypeSettings();
            ifmsVoucherTypes = ifmsVoucherTypes.Where(x => x.Id == id);
            IEnumerable<VoucherTypeSettingEntity> VoucherTypeEntity = ifmsVoucherTypes.Select(x => new VoucherTypeSettingEntity(x));

            return VoucherTypeEntity;
        }

        public async Task SaveSetting(SettingEntity settingEntity)
        {
            Guid.TryParse(settingEntity.Id.ToString(), out Guid id);

            IfmsSetting existingRecord = await this._financialRepository.GetAsync<IfmsSetting>(x => x.Id == id);

            if (existingRecord == null)
            {
                IfmsSetting ifmssetting = settingEntity.MapToModel();

                await this._financialRepository.AddAsync(ifmssetting);
                await this._financialRepository.UnitOfWork.SaveChanges();
              
            }
            else
            {
                IfmsSetting ifmssetting = await this.GetSetting(id);
                IfmsSetting setting = settingEntity.MapToModel(ifmssetting);

                await this._financialRepository.UpdateAsync(setting);
                await this._financialRepository.UnitOfWork.SaveChanges();
                             
            }
        }

        public async Task SaveFixedAssetSetting(FixedAssetSettingEntity fixedAssetSetting)
        {
            Guid.TryParse(fixedAssetSetting.Id.ToString(), out Guid id);

            IfmsFixedAssetSetting existingRecord = await this._financialRepository.GetAsync<IfmsFixedAssetSetting>(x => x.Id == id);

            if (existingRecord == null)
            {
                IfmsFixedAssetSetting setting = fixedAssetSetting.MapToModel();

                await this._financialRepository.AddAsync(setting);
                await this._financialRepository.UnitOfWork.SaveChanges();

            }
            else
            {
                IfmsFixedAssetSetting ifmsfixed = await this.GetFixedAssetSetting(id);
                IfmsFixedAssetSetting setting = fixedAssetSetting.MapToModel(ifmsfixed);

                await this._financialRepository.UpdateAsync(setting);
                await this._financialRepository.UnitOfWork.SaveChanges();

            }
        }

        public async Task<IfmsSetting> GetSetting(Guid id)
        {
            return (await this._financialRepository.GetAsync<IfmsSetting>(x => x.Id == id));

        }

        public async Task<IfmsFixedAssetSetting> GetFixedAssetSetting(Guid id)
        {
            return (await this._financialRepository.GetAsync<IfmsFixedAssetSetting>(x => x.Id == id));

        }

        Task<SettingEntity> IFinancialSetupManager.GetSetting(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CashierEntity>> GetCashiers()
        {
            IQueryable<IfmsCashier> ifmsCashier = await this._financialRepository.GetCashiers();
            IEnumerable<CashierEntity> cashiers = ifmsCashier.Select(x => new CashierEntity(x));

            return cashiers;
        }
        public async Task<IEnumerable<UserEntity>> GetUserList()
        {
            IQueryable<CoreUser> coreUser = await this._financialRepository.GetUsers();
            IEnumerable<UserEntity> users = coreUser.Select(x => new UserEntity(x));

            return users;
        }

        public async Task<IEnumerable<SubsidiaryAccountEntity>> GetSubsidiaryAccounts(string accountCode)
        {            
            IQueryable<CoreSubsidiaryAccount> CoreSubsidiary = await this._financialRepository.GetSubsidiaryAccounts();
            CoreSubsidiary = CoreSubsidiary.Where(x => (x.ControlAccount.Code + "-" + x.Code).StartsWith(accountCode) || x.Name.StartsWith(accountCode));
            IEnumerable<SubsidiaryAccountEntity> subsidiaries = CoreSubsidiary.Select(x => new SubsidiaryAccountEntity(x));

            return subsidiaries;
        }

        public async Task<IEnumerable<SubsidiaryAccountEntity>> GetSubsidiaryAccountList()
        {
            IQueryable<CoreSubsidiaryAccount> CoreSubsidiary = await this._financialRepository.GetSubsidiaryAccounts();
            IEnumerable<SubsidiaryAccountEntity> subsidiaries = CoreSubsidiary.Select(x => new SubsidiaryAccountEntity(x));

            return subsidiaries;
        }
        public async Task<IEnumerable<ControlAccountEntity>> GetControlAccountList()
        {
            IQueryable<CoreControlAccount> CoreControl = await this._financialRepository.GetControlAccounts();
            IEnumerable<ControlAccountEntity> control = CoreControl.Select(x => new ControlAccountEntity(x));

            return control;
        }

        public async Task<IEnumerable<ControlAccountEntity>> GetControlAccountsByParam(string accountCode)
        {
            IQueryable<CoreControlAccount> CoreControl = await this._financialRepository.GetControlAccounts();
            CoreControl = CoreControl.Where(x => x.Code == accountCode);
            IEnumerable<ControlAccountEntity> control = CoreControl.Select(x => new ControlAccountEntity(x));

            return control;
        }

        public async Task<IEnumerable<CostCenterEntity>> GetFilteredCostCenters(string accountCode)
        {
            IQueryable<CoreCostCenter> CoreCost = await this._financialRepository.GetCostCenters();
            CoreCost = CoreCost.Where(x => x.Code == accountCode);
            IEnumerable<CostCenterEntity> cost = CoreCost.Select(x => new CostCenterEntity(x));

            return cost;
        }

        public async Task<IEnumerable<PurposeTemplateEntity>> GetPurposeTemplate(string searchText)
        {
            IQueryable<IfmsPurposeTemplate> ifmsPurpose = await this._financialRepository.GetPurposeTemplates();
            ifmsPurpose = ifmsPurpose.Where(x => x.Purpose.ToUpper().Contains(searchText.ToUpper()) || x.Code.ToUpper().Contains(searchText.ToUpper()));
            IEnumerable<PurposeTemplateEntity> voucher = ifmsPurpose.Select(x => new PurposeTemplateEntity(x));

            return voucher;
        }

        public async Task<IEnumerable<PurposeTemplateEntity>> GetAllPurposeTemplates()
        {
            IQueryable<IfmsPurposeTemplate> ifmsPurpose = await this._financialRepository.GetPurposeTemplates();
            IEnumerable<PurposeTemplateEntity> voucher = ifmsPurpose.Select(x => new PurposeTemplateEntity(x));

            return voucher;
        }

        public async Task<IEnumerable<PaymentRequest>> GetApprovedPaymentRequest()
        {
            IQueryable<PsmsPaymentRequest> ifmsPayment = await this._financialRepository.GetApprovedPaymentRequest();
            IEnumerable<PaymentRequest> payment = ifmsPayment.Select(x => new PaymentRequest(x));

            return payment;
        }
     

       
    }
}
