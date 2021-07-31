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
            return (await this.GetAllAsync<IfmsCashier>())
                 .Include(x => x.SubsidiaryAccount).ThenInclude(x => x.ControlAccount)
                 .Include(x => x.User);
        }
        public async Task<IQueryable<CoreUser>> GetUsers()
        {
            return (await this.GetAllAsync<CoreUser>());
        }
        

        public async Task<IQueryable<CoreChartOfAccount>> GetChartOfAccount()
        {
            var chartOfAccount = (await this.GetAllAsync<CoreChartOfAccount>())
               .Include(x => x.CoreControlAccounts)
               .ThenInclude(x => x.CoreSubsidiaryAccounts);

            //.ThenInclude(y => y.CoreControlAccount)
            //

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
            return (await this.GetAllAsync<IfmsSetting>())
                .Include(x => x.CoreCostCenter);
              //  .Include(x => x.ControlAccount);
        }

        public async Task<IQueryable<CoreSubsidiaryAccount>> GetSubsidiaryAccount()
        {
            return (await this.GetAllAsync<CoreSubsidiaryAccount>());
        }

        public async Task<IQueryable<CoreSubsidiaryAccount>> GetSubsidiaryAccounts()
        {
            var subSidiary =  (await this.GetAllAsync<CoreSubsidiaryAccount>())
                           .Include(x => x.ControlAccount) ;
            return subSidiary;
        }

        public async Task<IQueryable<IfmsVoucherTypeSetting>> GetVoucherTypeSettings()
        {
            var voucherType = (await this.GetAllAsync<IfmsVoucherTypeSetting>())
               .Include(x => x.CoreCostCenter)
               .Include(x => x.CoreSubsidiaryAccount).ThenInclude( y => y.ControlAccount)
               .Include(x => x.LupBalanceSide)
               .Include(x => x.LupVoucherType);

            return voucherType;
        }

        public async Task<IQueryable<IfmsPurposeTemplate>> GetPurposeTemplates()
        {
            var voucherType = (await this.GetAllAsync<IfmsPurposeTemplate>());
               

            return voucherType;
        }

        public async Task<IQueryable<PsmsPaymentRequest>> GetApprovedPaymentRequest()
        {
            var payment = (await this.GetAllAsync<PsmsPaymentRequest>());


            return payment;
        }
    }
}
