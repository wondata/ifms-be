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
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;

        public FinancialSetupManager(IRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ChartOfAccountEntity>> GetChartOfAccount()
        {
            IEnumerable<CoreAccountType> coreAccountCategories = await this._repository.GetAllAsync<CoreAccountType>();
            IEnumerable<ChartOfAccountEntity> chartOfAccounts = coreAccountCategories.Select(x => new ChartOfAccountEntity(x));

            return chartOfAccounts;
        }

        public async Task<IEnumerable<CostCodeEntity>> GetCostCodes()
        {
            IEnumerable<IfmsCostCode> ifmsCost = await this._repository.GetAllAsync<IfmsCostCode>();
            IEnumerable<CostCodeEntity> costCodes = ifmsCost.Select(x => new CostCodeEntity(x));

            return costCodes;
        }

        public async Task<IEnumerable<CostCenterEntity>> GetCostCenters()
        {
            IEnumerable<CoreCostCenter> coreCost = await this._repository.GetAllAsync<CoreCostCenter>();
            IEnumerable<CostCenterEntity> costCodes = coreCost.Select(x => new CostCenterEntity(x));

            return costCodes;
        }

        public async Task<List<VoucherTypeEntity>> GetVoucherTypes()
        {
            IQueryable<LupVoucherType> lupVoucher = await this._repository.GetAllAsync<LupVoucherType>();
            List<VoucherTypeEntity> voucherType = lupVoucher.Select(x => new VoucherTypeEntity(x)).ToList();

            return voucherType;
        }
     

        public async Task<List<VoucherTypeSettingEntity>> GetVoucherTypeSettings()
        {

            IQueryable<IfmsVoucherTypeSetting> ifmsVoucher = await this._repository.GetAllAsync<IfmsVoucherTypeSetting>();

            List<VoucherTypeSettingEntity> voucherType = ifmsVoucher.Select(x => new VoucherTypeSettingEntity(x)).ToList();

            return voucherType;

        }

        public async Task SaveSetting(SettingEntity settingEntity)
        {
            Guid id;
            Guid.TryParse(settingEntity.Id.ToString(), out id);
            
            IfmsSetting existingRecord = await this._repository.GetAsync<IfmsSetting>(x => x.Id == id);

            if (existingRecord == null)
            {
                IfmsSetting ifmssetting = settingEntity.MapToModel();

                await this._repository.AddAsync(ifmssetting);
                await this._repository.UnitOfWork.SaveChanges();
              
            }
            else
            {
                IfmsSetting ifmssetting = await this.GetSetting(id);
                IfmsSetting setting = settingEntity.MapToModel(ifmssetting);

                await this._repository.UpdateAsync(setting);
                await this._repository.UnitOfWork.SaveChanges();

                             
            }
        }


        public async Task SaveFixedAssetSetting(FixedAssetSettingEntity fixedAssetSetting)
        {
            Guid id;
            Guid.TryParse(fixedAssetSetting.Id.ToString(), out id);

            IfmsFixedAssetSetting existingRecord = await this._repository.GetAsync<IfmsFixedAssetSetting>(x => x.Id == id);

            if (existingRecord == null)
            {
                IfmsFixedAssetSetting setting = fixedAssetSetting.MapToModel();

                await this._repository.AddAsync(setting);
                await this._repository.UnitOfWork.SaveChanges();

            }
            else
            {
                IfmsFixedAssetSetting ifmsfixed = await this.GetFixedAssetSetting(id);
                IfmsFixedAssetSetting setting = fixedAssetSetting.MapToModel(ifmsfixed);

                await this._repository.UpdateAsync(setting);
                await this._repository.UnitOfWork.SaveChanges();

            }
        }

        public async Task<IfmsSetting> GetSetting(Guid id)
        {
            return (await this._repository.GetAsync<IfmsSetting>(x => x.Id == id));

        }

        public async Task<IfmsFixedAssetSetting> GetFixedAssetSetting(Guid id)
        {
            return (await this._repository.GetAsync<IfmsFixedAssetSetting>(x => x.Id == id));

        }
    }
}
