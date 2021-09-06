using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepository
    {

        Task<IQueryable<IfmsVoucherHeader>> GetVoucherHeaders();
        Task<IQueryable<IfmsVoucherDetail>> GetVoucherDetails();
        Task<IQueryable<IfmsVoucherTypeSetting>> GetVoucherTypeSettings();
        Task<IQueryable<IfmsSetting>> GetSettings();

    }
}
