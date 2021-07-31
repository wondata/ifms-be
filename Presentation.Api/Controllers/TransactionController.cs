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
        private readonly ITransactionSetupManager _transactionService;
        private readonly ILookupManager _lookupManager;

        public TransactionController(IFinancialSetupManager service, ITransactionSetupManager transactionService, ILookupManager lookupManager)
        {
            _service = service;
            _transactionService = transactionService;
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
                var lookups = await this._transactionService.GetTransactionHeaders();

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
                    item.Period,
                    item.PurposeTemplate,
                    item.CostCenter,
                    item.VoucherType,
                });

                return Ok(new APIPagedResponse<IEnumerable<object>>(transaction, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<VoucherHeaderEntity>(null, 0, true, "Exception occurred, please retry!"));
            }

        }

        [HttpPost("GetTransactionDetails")]
        public async Task<IEnumerable<VoucherDetailEntity>> GetTransactionDetails(VoucherDetailEntity voucherDetail)
        {
            Guid.TryParse(voucherDetail.Id, out Guid id);
            return await this._transactionService.GetTransactionDetails(id);
        }


        private async Task<bool> IsBalanced(string Id)
        {
            Guid id;
            Guid.TryParse(Id, out id);

            decimal debit = 0;
            decimal credit = 0;

            IEnumerable<VoucherDetailEntity> voucherDetail = await this._transactionService.GetTransactionDetails(id);

            foreach (var filter in voucherDetail)
            {
                debit += filter.DebitAmount;
                credit += filter.CreditAmount;
            }

            return debit == credit;

        }

        [HttpPost("TransactionPost")]
        public async Task<ResponseDTO> TransactionPost(IEnumerable<VoucherHeaderEntity> voucherDetail)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                await this._transactionService.TransactionUnpost(voucherDetail);
                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Transactions has been posted Successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Error occured, Please retry or contact the admin!";
                return response;
            }
        }


        [HttpPost("TransactionUnpost")]
        public async Task<ResponseDTO> TransactionUnpost(IEnumerable<VoucherHeaderEntity> voucherDetail)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {                
                await this._transactionService.TransactionUnpost(voucherDetail);
                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Transactions has been Unposted Successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Error occured, Please retry or contact the admin!";
                return response;
            }
        }

        [HttpPost("GetJvNumber")]
        public async Task<string> GetJvNumber(string voucherCode)
        {                     

            const string VoucherType = "lupVoucherType";
            var filtered = await this._lookupManager.Get(VoucherType);
            var currentNumber = this._transactionService.GetVoucherNumber(filtered.Id);
            if (filtered.Equals(filtered.Code)) {
                return filtered.Code;
            }
            else {
                return null;
            }

        }


        #endregion
    }
}
