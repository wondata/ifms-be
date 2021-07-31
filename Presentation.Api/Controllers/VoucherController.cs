using Application.DTOs;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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
    public class VoucherController : BaseController
    {
        private readonly IFinancialSetupManager _service;
        private readonly ITransactionSetupManager _transactionService;
        private readonly ILookupManager _lookupManager;

        public VoucherController(IFinancialSetupManager service, ITransactionSetupManager transactionService, ILookupManager lookupManager)
        {
            _service = service;
            _transactionService = transactionService;
            _lookupManager = lookupManager;
        }
        

        #region Voucher

        [HttpPost("GetAllVoucherList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherHeaderEntity>>> GetAllVoucherList()
        {
            try
            {
                var lookups = await _transactionService.GetAllVoucherList();

                var voucher = lookups.Select(item => new {
                    item.Id,
                    item.CostCenterId,
                    IsBalanced = IsBalanced(item.Id).Result.ToString(),
                    item.VoucherTypeId,
                    item.DocumentNo,
                    item.Date,
                    item.PeriodId,
                    item.Description,
                    item.PayedToReceivedFrom,
                    item.PurposeTemplateId,
                    item.Purpose,
                  //  item.Description,
                    item.Amount,
                    item.ModeOfPaymentId,
                   // item.PayedToReceivedFrom ,
                    item.ChequeNo,
                   // item.PeriodId,
                    item.IsPosted,
                    item.IsAdjustment,
                    item.IsVoid,
                    item.CostCenter,
                    item.Period,
                    item.VoucherType,
                    item.PurposeTemplate,
                });

                return Ok(new APIPagedResponse<IEnumerable<object>>(voucher, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(null, 0, true, "Exception occurred, please retry!"));
            }

        }

        private async Task<bool> IsBalanced(string Id)
        {
            Guid id;
            Guid.TryParse(Id, out id);

            decimal debit = 0;
            decimal credit = 0;

            IEnumerable<VoucherDetailEntity> voucherDetail = await this._transactionService.GetVoucherDetails(id);



            foreach (var filter in voucherDetail)
            {
                debit += filter.DebitAmount;
                credit += filter.CreditAmount;
            }

            return debit == credit;

        }

        [HttpPost("GetVoucherHeaderDetails")]
        public async Task<IEnumerable<object>> GetVoucherHeaderDetails(VoucherHeaderEntity voucherHeader)
        {
            Guid id;
            Guid.TryParse(voucherHeader.Id, out id);

            try
            {
                var lookups = await this._transactionService.GetVoucherHeaderDetails(id);

                var voucher = lookups.Select(item => new {
                    item.Id,
                    item.CostCenterId,
                    CostCenter = item.CostCenter.Code,
                    voucherType = item.VoucherType.Name,
                    modePayment = (item.ModePayment == null) ? null : item.ModePayment.Name,
                    item.VoucherTypeId,
                    item.VoucherType.Name,
                    item.ReferenceNo,
                    item.DocumentNo,
                    item.Date,
                    item.PayedToReceivedFrom,
                    item.PurposeTemplateId,
                    Purpose = item.PurposeTemplate != null ? item.PurposeTemplate.Purpose : "",
                    item.Description,
                    item.Amount,
                    item.TaxId,
                    item.ModeOfPaymentId,
                    item.ChequeNo,
                    Project = item.CostCenter != null ? item.CostCenter.Code : "",
                    item.PostedFromOperation,
                    item.AuthorizedDate,
                });

                return voucher;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        [HttpPost("GetVoucherDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherDetailEntity>>> GetVoucherDetail(VoucherHeaderEntity voucherHeader)
        {
            Guid id;
            Guid.TryParse(voucherHeader.Id, out id);

            try
            {
                var lookups = await _transactionService.GetVoucherDetails(id);

                var voucher = lookups.Select(item => new {
                    item.Id,
                    item.CostCenterId,
                    CostCenter = item.CostCenters.Code,
                    item.ControlAccountId,
                    AccountId = item.SubsidiaryAccountId,
                    AccountCode = item.ControlAccount.Code + "-" + item.SubsidiaryAccount.Code,
                    AccountName = item.SubsidiaryAccount.Name,
                    item.DebitAmount,
                    item.CreditAmount,
                    item.CostCodeId,
                    CostCode = item.CostCodes == null ? "" : item.CostCodes.Code,
                    item.IsInterBranchTransactionCleared,
                    item.IBTReferenceVoucherHeaderId,
                    item.ControlAccount,
                    item.CostCodes,
                    item.SubsidiaryAccount,
                    item.CostCenters,
                });

                return Ok(new APIPagedResponse<IEnumerable<object>>(voucher, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(null, 0, true, "Exception occurred, please retry!"));
            }

        }

        [HttpPost("SaveVoucher")]
        public async Task<ResponseDTO> SaveVoucher(VoucherHeaderEntity voucherHeader)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                await this._transactionService.SaveVoucher(voucherHeader);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record saved successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Record has not been saved successfully!";
                return response;
            }
        }

        [HttpPost("DeleteVoucher")]
        public async Task<ResponseDTO> DeleteVoucher(VoucherHeaderEntity voucherHeader)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                Guid id;
                Guid.TryParse(voucherHeader.Id, out id);

                await this._transactionService.DeleteVoucher(id);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record deleted successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Error occured, Please retry or contact the admin!";
                return response;
            }
        }

        #endregion


        #region Collection Voucher

        [HttpPost("GetCollectionVouchers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherHeaderEntity>>> GetCollectionVouchers()
        {
            try
            {
                var lookups = await this._transactionService.GetCollectionVouchers();
                return Ok(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<VoucherHeaderEntity>(null, 0, true, "Exception occurred, please retry!"));
            }

        }        

        [HttpPost("SaveCollectionVoucher")]
        public async Task<ResponseDTO> SaveCollectionVoucher(PaymentVoucherPostModel voucherHeader)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                VoucherHeaderEntity voucher = voucherHeader.MapToEntity();
                await this._transactionService.SaveCollectionVoucher(voucher);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record saved successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Record has not been saved successfully!";
                return response;
            }
        }

        [HttpPost("GetCollectionVoucherTypes")]
        public async Task<IEnumerable<object>> GetCollectionVoucherTypes()
        {
            const string VoucherType = "lupVoucherType";
            var filtered = await this._lookupManager.GetAllLookup(VoucherType);
            filtered = filtered.Where(l => l.Name == "CRV" || l.Name == "CSV" || l.Name == "CRSV" || l.Name == "BD");

            var voucherTypes = filtered.Select(costCenter => new
            {
                costCenter.Id,
                costCenter.Name,
                costCenter.Code
            });


            return voucherTypes;
        }

        #endregion


        #region Payment Voucher
        [HttpPost("GetPaymentVouchers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherHeaderEntity>>> GetPaymentVouchers()
        {

            try
            {
                var lookups = await this._transactionService.GetPaymentVouchers();
                return Ok(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<VoucherHeaderEntity>(null, 0, true, "Exception occurred, please retry!"));
            }

        }
       
        [HttpPost("SavePaymentVoucher")]
        public async Task<ResponseDTO> SavePaymentVoucher(PaymentVoucherPostModel voucherHeader)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                VoucherHeaderEntity voucher = voucherHeader.MapToEntity();
                await this._transactionService.SavePaymentVoucher(voucher);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record saved successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Record has not been saved successfully!";
                return response;
            }
        }

        [HttpPost("GetPaymentVoucherTypes")]
        public async Task<IEnumerable<object>> GetPaymentVoucherTypes()
        {
            const string VoucherType = "lupVoucherType";
            var filtered = await this._lookupManager.GetAllLookup(VoucherType);
            filtered = filtered.Where(l => l.Name == "CPV" || l.Name == "BPV" || l.Name == "PCPV");

            var voucherTypes = filtered.Select(costCenter => new
            {
                costCenter.Id,
                costCenter.Name,
                costCenter.Code
            });


            return voucherTypes;
        }


        #endregion

    }
}
