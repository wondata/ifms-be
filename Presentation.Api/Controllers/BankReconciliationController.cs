using Application.DTOs;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankReconciliationController: BaseController
    {
        private readonly IFinancialSetupManager _service;
        private readonly ITransactionSetupManager _transactionService;
        private readonly ILookupManager _lookupManager;

        public BankReconciliationController(IFinancialSetupManager service, ITransactionSetupManager transactionService, ILookupManager lookupManager)
        {
            _service = service;
            _transactionService = transactionService;
            _lookupManager = lookupManager;
        }


        //[HttpPost("GetPeriodRunningBalance")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<APIPagedResponse<PeriodEntity>>> GetPeriodRunningBalance(PeriodEntity period)
        //{           

        //    try
        //    {
        //        //var lookups = await _transactionService.GetVoucherDetails(id);

        //        //return Ok(new APIPagedResponse<IEnumerable<object>>(lookups, lookups.Count()));
        //        return NotImplementedException;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //        return BadRequest(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(null, 0, true, "Exception occurred, please retry!"));
        //    }

        //}

    }
}
