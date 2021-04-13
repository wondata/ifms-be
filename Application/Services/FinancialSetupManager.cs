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

        public async Task<IEnumerable<CostCenterEntity>> GetCostCenter()
        {
            IEnumerable<CoreCostCenter> coreCost = await this._repository.GetAllAsync<CoreCostCenter>();
            IEnumerable<CostCenterEntity> costCodes = coreCost.Select(x => new CostCenterEntity(x));

            return costCodes;
        }

        public async Task<IEnumerable<VoucherTypeEntity>> GetVoucherType()
        {
            IEnumerable<LupVoucherType> lupVoucher = await this._repository.GetAllAsync<LupVoucherType>();
            IEnumerable<VoucherTypeEntity> voucherType = lupVoucher.Select(x => new VoucherTypeEntity(x));

            return voucherType;
        }

        public async Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSetting()
        {

            IEnumerable<IfmsVoucherTypeSetting> ifmsVoucher = await this._repository.GetAllAsync<IfmsVoucherTypeSetting>();
            
            IEnumerable<VoucherTypeSettingEntity> voucherType = ifmsVoucher.Select(x => new VoucherTypeSettingEntity(x));

            return voucherType;

        }
    }
}
