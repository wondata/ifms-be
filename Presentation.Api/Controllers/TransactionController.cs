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
    public class TransactionController : BaseController
    {
        private readonly IFinancialSetupManager _service;
        private readonly ILookupManager _lookupManager;

        public TransactionController(IFinancialSetupManager service, ILookupManager lookupManager)
        {
            _service = service;
            _lookupManager = lookupManager;
        }



        #region Transaction

        [HttpPost("GetTransactionHeaders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<VoucherHeaderEntity>> GetTransactionHeaders()
        {
            try
            {
                var lookups = await this._service.GetTransactionHeaders();

                var transaction =  lookups.Select( item => new {
                    item.Id,                   
                    item.ReferenceNo,
                    IsBalanced =  IsBalanced(item.Id).Result.ToString(),
                    item.Amount,
                    item.ChequeNo,
                    item.CostCenterId,
                    item.CreatedBy,
                    item.Date,
                    item.Description,
                    item.DocumentNo,
                    item.IsAdjustment,
                    item.IsDeleted,
                    item.IsPosted,
                    item.IsVoid,
                    item.ModeOfPaymentId,
                    item.PayedToReceivedFrom,
                    item.Period.Name,
                    item.PeriodId,
                    item.Purpose,
                    item.CostCenter,
                    item.Period,
                    item.VoucherType,
                    item.PurposeTemplate,
                });

                return Ok(new APIPagedResponse<IEnumerable<object>>(transaction, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<VoucherHeaderEntity>(null, 0, true, "Exception occurred, please retry!"));
            }

        }


        private async Task<bool> IsBalanced(Guid id)
        {
            decimal debit = 0;
            decimal credit = 0;

            IEnumerable<VoucherDetailEntity> voucherDetail = await this._service.GetTransactionDetails(id);
            // IEnumerable<ChartOfAccountViewModel> chartOfAccountViewModels = chartOfAccountEntities.Select(x => new ChartOfAccountViewModel(x));

            foreach (var filter in voucherDetail)
            {
                debit += filter.DebitAmount;
                credit += filter.CreditAmount;
            }

            return debit == credit;

        }


        [HttpPost("GetTransactionDetails")]
        public async Task<IEnumerable<VoucherDetailEntity>> GetTransactionDetails(VoucherDetailEntity voucherDetail)
        {           
            Guid.TryParse(voucherDetail.Id, out Guid id);
            return await this._service.GetTransactionDetails(id);
        }

        #endregion
    }
}
