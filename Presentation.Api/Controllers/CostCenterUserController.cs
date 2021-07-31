using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    public class CostCenterUserController : BaseController
    {
        private readonly IFinancialSetupManager _service;
        private readonly ILookupManager _lookupManager;

        public CostCenterUserController(IFinancialSetupManager service, ILookupManager lookupManager)
        {
            _service = service;
            _lookupManager = lookupManager;
        }


        //[HttpPost("GetAllCostCenterUser")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<APIPagedResponse<CostCodeEntity>>> GetAllCostCenterUser()
        //{
        //    try
        //    {
        //        var lookups = await _service.GetAllCostCenterUser();
        //        return Ok(new APIPagedResponse<List<CostCodeEntity>>(lookups, lookups.Count()));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //        return BadRequest(new APIPagedResponse<List<CostCodeEntity>>(null, 0, true, "Exception occurred, please retry!"));

        //    }
        //}

    }
}
