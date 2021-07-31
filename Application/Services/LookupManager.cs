using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Domain.Models;
using System.Linq;
using Domain.Entities;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class LookupManager : ILookupManager
    {
        private readonly ILookupRepository _lookupRepository;
        private readonly IConfiguration _configuration;

        public LookupManager(ILookupRepository lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }

        public async Task<IEnumerable<LookupEntity>> GetAllLookup(string table)
        {
            IEnumerable<LookupModel> lookupModels = await this._lookupRepository.GetAllLookup(table);

            IEnumerable<LookupEntity> lookups = lookupModels.Select(x => new LookupEntity(x));

            return lookups;
        }

        public async Task<LookupEntity> Get(string table)
        {
            LookupModel lookupModels = await this._lookupRepository.Get(table);

            return new LookupEntity(lookupModels);
        }


    }
}
