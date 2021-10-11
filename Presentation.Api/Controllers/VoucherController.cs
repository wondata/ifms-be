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
                    item.Amount,
                    item.ModeOfPaymentId,
                    item.ChequeNo,
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
                    CostCenterCode = item.CostCenter.Code,
                    VoucherTypeName = item.VoucherType.Name,
                    modePayment = (item.ModePayment == null) ? null : item.ModePayment.Name,
                    item.VoucherTypeId,                  
                    item.ReferenceNo,
                    item.DocumentNo,
                    item.Date,                    
                    item.PayedToReceivedFrom,
                    item.PurposeTemplateId,
                    Purpose = item.PurposeTemplate != null ? item.PurposeTemplate.Name : "",
                    item.Description,
                    item.Amount,
                    item.TaxId,
                    item.ModeOfPaymentId,
                    item.ChequeNo,
                    Project = item.CostCenter != null ? item.CostCenter.Code : "",
                    ProjectId = item.CostCenterId,
                    item.PostedFromOperation,
                    item.AuthorizedDate,
                    item.CostCenter,
                    item.VoucherType,
                    
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
            Guid.TryParse(voucherHeader.Id, out Guid id);

            try
            {
                var lookups = await _transactionService.GetVoucherDetails(id);              

                return Ok(new APIPagedResponse<IEnumerable<object>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(null, 0, true, "Exception occurred, please retry!"));
            }

        }

        [HttpPost("SaveVoucher")]
        public async Task<ResponseDTO> SaveVoucher(VoucherHeaderPostModel voucherHeader)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                VoucherHeaderEntity voucher = voucherHeader.MapToEntity();
                var message = await this._transactionService.SaveVoucher(voucher, voucherHeader.voucherDetails);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = message;
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Record has not been saved successfully!";
                return response;
            }
        }

        [HttpPost("DeleteVoucherDetail")]
        public async Task<ResponseDTO> DeleteVoucherDetail(VoucherHeaderPostModel voucherHeader)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                Guid.TryParse(voucherHeader.Id, out Guid id);

                await this._transactionService.DeleteVoucherDetail(id);

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


        [HttpPost("GetVoucherNumber")]
        public async Task<string> GetVoucherNumber(VoucherHeaderEntity voucherHeader )
        {            
            try
            {
                var voucherNumber = await this._transactionService.GetVoucherNumber(voucherHeader.CostCenterId, voucherHeader.VoucherTypeId);               
                return voucherNumber;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        [HttpPost("GetDefaultAccounts")]
        public async Task<object> GetDefaultAccounts(VoucherHeaderEntity voucherHeader)
        {
            try
            {
                string defaultCostCenterIds= "";
                string defaultCostCenters= "";
                string controlAccountId = "";
                string controlAccount = "";
                string controlAccountCode = "";
                string subsidiaryAccountId = "";
                string subsidiaryAccount = "";
                string subsidiaryAccountCode = "";

                var setting = await this._transactionService.GetSettings();
                if (setting != null) {
                    if (setting.DefaultCostCenterId == null)
                    {
                        defaultCostCenterIds = "";
                        defaultCostCenters = "";
                    }
                    else {
                        defaultCostCenterIds = setting.DefaultCostCenterId.ToString() ;
                        defaultCostCenters = setting.CostCenter.Code;
                    }
                }


                var voucherType = await this._transactionService.GetDefaultAccounts(voucherHeader.VoucherTypeId);
                if (voucherType != null && voucherType.DefaultAccountId != null)
                {

                    controlAccountId = voucherType.SubsidiaryAccount.ControlAccountId.ToString();
                    controlAccount = voucherType.SubsidiaryAccount.ControlAccount.Name;
                    controlAccountCode = voucherType.SubsidiaryAccount.ControlAccount.Code;
                    subsidiaryAccountId = voucherType.DefaultAccountId.ToString();
                    subsidiaryAccount = voucherType.SubsidiaryAccount.Name;
                    subsidiaryAccountCode = voucherType.SubsidiaryAccount.Code;                                   

                }
                else {

                    controlAccountId = "";
                    controlAccount = "";
                    controlAccountCode = "";
                    subsidiaryAccountId = "";
                    subsidiaryAccount = "";
                    subsidiaryAccountCode = "";                                  
                }

                var voucher = new
                {
                    CostCenterId = defaultCostCenterIds,
                    CostCenter = defaultCostCenters,
                    ControlAccountId = controlAccountId,
                    ControlAccount = controlAccount,
                    ControlAccountCode = controlAccountCode,
                    SubsidiaryAccountId = subsidiaryAccountId,
                    SubsidiaryAccount = subsidiaryAccount,
                    SubsidiaryAccountCode = subsidiaryAccountCode
                };

                return voucher;
                
            }
            catch (System.Exception ex)
            {
                return null;
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
