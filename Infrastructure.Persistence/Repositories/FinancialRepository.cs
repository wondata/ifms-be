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

        public async Task<IQueryable<CoreAccountType>> GetChartOfAccount()
        {
            var chartOfAccount = (await this.GetAllAsync<CoreAccountType>())
               .Include(x => x.CoreAccountGroup);
               //.ThenInclude(y => y.CoreControlAccount)
               //.ThenInclude(x => x.CoreSubsidiaryAccount);

            return chartOfAccount;
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
