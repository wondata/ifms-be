using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ILookupRepository 
    {
        Task<LookupModel> Get(Guid id, string table);

        Task<List<LookupModel>> GetAllLookup(string table);

        Task<LookupModel> Get(string table);

    }
}
