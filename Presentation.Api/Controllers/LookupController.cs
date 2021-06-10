using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Model.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController
    {
        private readonly ILookupManager _lookupManager;

        public LookupController(ILookupManager lookupManager)
        {
            _lookupManager = lookupManager;
        }

        [HttpPost("GetLookups")]
        public async Task<IEnumerable<LookupEntity>> GetLookups(LookupPostModel lookupPostModel)
        {
            return await this._lookupManager.GetAllLookup(lookupPostModel.LookupName);
        }
    }
}
