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

        Task<IQueryable<CoreChartOfAccount>> GetChartOfAccount();

        Task<IQueryable<CoreChartOfAccount>> GetChartOfAccounts();

        Task<IQueryable<CoreSubsidiaryAccount>> GetSubsidiaryAccounts();

        Task<IQueryable<CoreControlAccount>> GetControlAccounts();

        Task<IQueryable<IfmsCostCode>> GetCostCodes();

        Task<IQueryable<CoreCostCenter>> GetCostCenters();

        Task<IQueryable<IfmsCashier>> GetCashiers();

        Task<IQueryable<CoreUser>> GetUsers();

        Task<IQueryable<IfmsVoucherTypeSetting>> GetVoucherTypeSettings();

        Task<IQueryable<IfmsSetting>> GetSettings();

        Task<IQueryable<IfmsSetting>> GetSetting(Guid id);

        Task<IQueryable<LupBalanceSide>> GetBalanceSides();

        Task<IQueryable<IfmsPurposeTemplate>> GetPurposeTemplates();

        Task<IQueryable<PsmsPaymentRequest>> GetApprovedPaymentRequest();

    }
}
