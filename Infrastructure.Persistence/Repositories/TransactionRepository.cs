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
               .Include(x => x.ControlAccount).Take(300);


            return voucherDetail;
        }
    }
}
