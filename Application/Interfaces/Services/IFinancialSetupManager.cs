using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IFinancialSetupManager
    {
        Task<IEnumerable<ChartOfAccountEntity>> GetChartOfAccount();

        Task<IEnumerable<CostCodeEntity>> GetCostCodes();

        Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSetting();

        Task<IEnumerable<VoucherTypeEntity>> GetVoucherType();

    }
}
