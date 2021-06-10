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

        public FinancialSetupManager(IFinancialRepository financialRepository, IConfiguration configuration)
        {
            _financialRepository = financialRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ChartOfAccountEntity>> GetChartOfAccount()
        {
            IEnumerable<CoreAccountType> coreAccountCategories = await this._financialRepository.GetChartOfAccount();
            IEnumerable<ChartOfAccountEntity> chartOfAccounts = coreAccountCategories.Select(x => new ChartOfAccountEntity(x));

            return chartOfAccounts;
        }

        public async Task<List<CostCodeEntity>> GetCostCodes()
        {
            IQueryable<IfmsCostCode> ifmsCost = await this._financialRepository.GetCostCodes();
            List<CostCodeEntity> costCodes = ifmsCost.Select(x => new CostCodeEntity(x)).ToList();

            return costCodes;
        }

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
            //  string defaultCostCenter = ifmsSettings.
            //  string DefaultCostCenter;
            // string DefaultCostCenterId;
            IEnumerable<SettingEntity> settings = ifmsSettings.Select(x => new SettingEntity(x));

            //var returnValue = new LookupModel
            //{
            //    Code = settings.Select(x => x.CostCenter.Code).ToString(),
            //    Name = settings.Select(x => x.DefaultCostCenterId).ToString()
            // };
            //var DefaultCostCenter = ifmsSettings.Select(x => x.CoreCostCenter.Code);
            //var DefaultCostCenterId = ifmsSettings.Select(x => x.DefaultCostCenterId);


            return settings; 
        }
        public async Task<List<VoucherTypeEntity>> GetVoucherTypes()
        {
            IQueryable<LupVoucherType> lupVoucher = await this._financialRepository.GetAllAsync<LupVoucherType>();
            List<VoucherTypeEntity> voucherType = lupVoucher.Select(x => new VoucherTypeEntity(x)).ToList();

            return voucherType;
        }


        public async Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails()
        {
            IQueryable<IfmsVoucherDetail> ifmsVoucher = await this._financialRepository.GetVoucherDetails();
            List<VoucherDetailEntity> voucherDetail = ifmsVoucher.Select(x => new VoucherDetailEntity(x)).ToList();

            return voucherDetail;
        }


        public async Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSettings()
        {
            IEnumerable<IfmsVoucherTypeSetting> ifmsVoucher = await this._financialRepository.GetVoucherTypeSettings();
            IEnumerable<VoucherTypeSettingEntity> voucherType = ifmsVoucher.Select(x => new VoucherTypeSettingEntity(x));

            return voucherType;

        }

        public async Task SaveSetting(SettingEntity settingEntity)
        {
            Guid id;
            Guid.TryParse(settingEntity.Id.ToString(), out id);
            
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
            Guid id;
            Guid.TryParse(fixedAssetSetting.Id.ToString(), out id);

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

        public async Task<IEnumerable<VoucherHeaderEntity>> GetVoucherHeaders()
        {
            IQueryable<IfmsVoucherHeader> ifmsVoucher = await this._financialRepository.GetVoucherHeaders();
            IEnumerable<VoucherHeaderEntity> voucher = ifmsVoucher.Select(x => new VoucherHeaderEntity(x));

            return voucher;
        }

        public async Task<IEnumerable<SubsidiaryAccountEntity>> GetSubsidiaryAccounts(string accountCode)
        {            
            IQueryable<CoreSubsidiaryAccount> CoreSubsidiary = await this._financialRepository.GetSubsidiaryAccounts();
            CoreSubsidiary = CoreSubsidiary.Where(x => x.Code == accountCode);
            IEnumerable<SubsidiaryAccountEntity> subsidiaries = CoreSubsidiary.Select(x => new SubsidiaryAccountEntity(x));

            return subsidiaries;
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

     

        public async Task<IEnumerable<VoucherHeaderEntity>> GetAllHeaders(int start, int limit, string sort, string dir, string record)
        {
            //var currentUser = Session[Constants.CurrentUser] as coreUser;
            //int currentUserId = currentUser != null ? currentUser.Id : 0;

            IQueryable<IfmsVoucherHeader> ifmsVoucher = await this._financialRepository.GetVoucherHeaders();
            ifmsVoucher = ifmsVoucher.Where(x => x.VoucherType.Name != "PCPV");
            // ifmsVoucher = ifmsVoucher.Where(x => x.CostCenter.IfmsCostCenterUsers.Any(v => v.UserId == currentUserId ));
            IEnumerable<VoucherHeaderEntity> voucher = ifmsVoucher.Select(x => new VoucherHeaderEntity(x));

            return voucher;
        }

        public async Task<IEnumerable<VoucherHeaderEntity>> GetVoucher(Guid id)
        {
            IQueryable<IfmsVoucherHeader> ifmsVoucher = await this._financialRepository.GetVoucherHeaders();
            ifmsVoucher = ifmsVoucher.Where(x => x.Id == id);            
            IEnumerable<VoucherHeaderEntity> voucher = ifmsVoucher.Select(x => new VoucherHeaderEntity(x));

            return voucher;
        }

        public async Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails(Guid id)
        {
            IQueryable<IfmsVoucherDetail> ifmsVoucher = await this._financialRepository.GetVoucherDetails();
            ifmsVoucher = ifmsVoucher.Where(x => x.VoucherHeaderId == id);
            IEnumerable<VoucherDetailEntity> voucherDetail = ifmsVoucher.Select(x => new VoucherDetailEntity(x)).ToList();

            return voucherDetail;
        }

        
    }
}
