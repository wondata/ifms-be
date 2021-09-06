using Application.Interfaces.Repositories;
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
    public class TransactionRepository : GenericRepository, ITransactionRepository
    {

        public TransactionRepository(FinancialContext dbContext)
           : base(dbContext)
        {
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

        public async Task<IQueryable<IfmsVoucherDetail>> GetVoucherDetails()
        {
            var voucherDetail = (await this.GetAllAsync<IfmsVoucherDetail>())
               .Include(x => x.CostCenter)
               .Include(x => x.CostCode)               
               .Include(x => x.SubsidiaryAccount).ThenInclude(x => x.ControlAccount)
               .Include(x => x.ControlAccount);


            return voucherDetail;
        }

        public async Task<IQueryable<IfmsVoucherTypeSetting>> GetVoucherTypeSettings()
        {
            var voucherTypeSettings = (await this.GetAllAsync<IfmsVoucherTypeSetting>())
               .Include(x => x.CoreCostCenter)
               .Include(x => x.CoreSubsidiaryAccount).ThenInclude(x => x.ControlAccount);


            return voucherTypeSettings;
        }

        public async Task<IQueryable<IfmsSetting>> GetSettings()
        {
            var settings = (await this.GetAllAsync<IfmsSetting>())
               .Include(x => x.CoreCostCenter);              


            return settings;
        }
    }
}
