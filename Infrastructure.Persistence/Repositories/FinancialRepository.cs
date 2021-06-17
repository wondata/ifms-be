using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class FinancialRepository : GenericRepository, IFinancialRepository
    {
        public FinancialRepository(FinancialContext dbContext)
           : base(dbContext)
        {
        }

        public async Task<IQueryable<LupBalanceSide>> GetBalanceSides()
        {
            return (await this.GetAllAsync<LupBalanceSide>());
        }

        public async Task<IQueryable<IfmsCashier>> GetCashiers()
        {
            return (await this.GetAllAsync<IfmsCashier>());
              //   .Include(x => x.SubsidiaryAccount).ThenInclude(x => x.CoreControlAccount);
        }

        public async Task<IQueryable<CoreAccountType>> GetChartOfAccount()
        {
            var chartOfAccount = (await this.GetAllAsync<CoreAccountType>())
               .Include(x => x.CoreAccountGroup);
               //.ThenInclude(y => y.CoreControlAccount)
               //.ThenInclude(x => x.CoreSubsidiaryAccount);

            return chartOfAccount;
        }

        public async Task<IQueryable<CoreControlAccount>> GetControlAccounts()
        {
            return (await this.GetAllAsync<CoreControlAccount>());
        }

        public async Task<IQueryable<CoreCostCenter>> GetCostCenters()
        {
            return (await this.GetAllAsync<CoreCostCenter>())
                .Include(x => x.InverseParent);
           
        }

        public async Task<IQueryable<IfmsCostCode>> GetCostCodes()
        {
            return (await this.GetAllAsync<IfmsCostCode>());
              
        }

        public async Task<IQueryable<IfmsSetting>> GetSetting(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<IfmsSetting>> GetSettings()
        {
            return (await this.GetAllAsync<IfmsSetting>()).Include(x => x.CoreCostCenter);
        }

        public async Task<IQueryable<CoreSubsidiaryAccount>> GetSubsidiaryAccount()
        {
            return (await this.GetAllAsync<CoreSubsidiaryAccount>());
        }

        public Task<IQueryable<CoreSubsidiaryAccount>> GetSubsidiaryAccounts()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<IfmsVoucherDetail>> GetVoucherDetails()
        {
            var voucherDetail = (await this.GetAllAsync<IfmsVoucherDetail>())
               .Include(x => x.CoreCostCenter)
               .Include(x => x.IfmsCostCode)           
               //.Include(x => x.CoreSubsidiaryAccount).Take(300);
               .Include(x => x.CoreControlAccount).Take(300);


            return voucherDetail;
        }

        public async Task<IQueryable<IfmsVoucherHeader>> GetVoucherHeaders()
        {
            var voucherHeader = (await this.GetAllAsync<IfmsVoucherHeader>())
               .Include(x => x.CostCenter)
               .Include(x => x.CorePeriod)
               .Include(x => x.VoucherType)
               .Include(x => x.PurposeTemplate)
               .Include(x => x.ModeOfPayment).Take(300);

            return voucherHeader;
        }

        public async Task<IQueryable<IfmsVoucherTypeSetting>> GetVoucherTypeSettings()
        {
            var voucherType = (await this.GetAllAsync<IfmsVoucherTypeSetting>())
               .Include(x => x.CoreCostCenter)
               .Include(x => x.CoreSubsidiaryAccount)
               .Include(x => x.LupBalanceSide)
               .Include(x => x.LupVoucherType);

            return voucherType;
        }
    }
}
