using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
   public interface IFinancialRepository : IRepository
    {

        Task<IQueryable<CoreAccountType>> GetChartOfAccount();

        Task<IQueryable<IfmsCostCode>> GetCostCodes();

        Task<IQueryable<CoreCostCenter>> GetCostCenters();

        Task<IQueryable<IfmsCashier>> GetCashiers();

        Task<IQueryable<IfmsVoucherTypeSetting>> GetVoucherTypeSettings();

     // Task<List<IfmsVouc>> GetVoucherTypes();

        Task<IQueryable<IfmsSetting>> GetSetting(Guid id);

        Task<IQueryable<LupBalanceSide>> GetBalanceSides();

        Task<IQueryable<IfmsVoucherHeader>> GetVoucherHeaders();

        Task<IQueryable<IfmsVoucherDetail>> GetVoucherDetails();
    }
}
