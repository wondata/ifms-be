using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ILookupManager
    {
        Task<IEnumerable<LookupEntity>> GetAllLookup(string table);
    }
}
