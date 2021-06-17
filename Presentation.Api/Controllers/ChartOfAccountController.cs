using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Wrappers;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Api.Model.PostModel;
using Presentation.Api.Model.ViewModel;
using Serilog;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartOfAccountController : BaseController
    {
        private readonly IFinancialSetupManager _service;
        private readonly ILookupManager _lookupManager;

        public ChartOfAccountController(IFinancialSetupManager service, ILookupManager lookupManager)
        {
            _service = service;
            _lookupManager = lookupManager;
        }

        [HttpPost("GetAccountGroup")]
        public async Task<IEnumerable<ChartOfAccountEntity>> GetAccountGroup()
        {
            try
            {
                IEnumerable<ChartOfAccountEntity> chartOfAccounts = await this._service.GetChartOfAccount();

                return chartOfAccounts;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        
        [HttpPost("GetAccounts")]
        public async Task<IEnumerable<SubsidiaryAccountEntity>> GetAccounts(string accountCode)
        {
            return await this._service.GetSubsidiaryAccounts(accountCode);
        }


        [HttpPost("GetControlAccountsByParam")]
        public async Task<IEnumerable<ControlAccountEntity>> GetControlAccountsByParam(string accountCode)
        {
            return await this._service.GetControlAccountsByParam(accountCode);
        }


        [HttpPost("GetFilteredCostCenters")]
        public async Task<IEnumerable<CostCenterEntity>> GetFilteredCostCenters(string filteredCostCeter)
        {
            return await this._service.GetFilteredCostCenters(filteredCostCeter);
        }

        [HttpPost("GetDefaultCostCenter")]
        public async Task<IEnumerable<DefaultCostCenterViewModle>> GetDefaultCostCenter()
        {

            IEnumerable<SettingEntity> settingEntities = await this._service.GetSettings();
            IEnumerable<DefaultCostCenterViewModle> settings = settingEntities.Select(x => new DefaultCostCenterViewModle(x));

            return settings;
        }

        

        [HttpPost("GetCostCodes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<CostCodeEntity>>> GetCostCodes()
        {
            try
            {
                var lookups = await _service.GetCostCodes();
                return Ok(new APIPagedResponse<List<CostCodeEntity>>(lookups, lookups.Count()));
            }          
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<List<CostCodeEntity>>(null, 0, true, "Exception occurred, please retry!"));

            }
        }

        [HttpPost("GetCostCenters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<CostCenterEntity>>> GetCostCenters()
        {
            try
            {
                var lookups = await _service.GetCostCenters();
                return Ok(new APIPagedResponse<IEnumerable<CostCenterEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<IEnumerable<CostCenterEntity>>(null, 0, true, "Exception occurred, please retry!"));

            }
        }

        [HttpPost("GetVoucherType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherTypeEntity>>> GetVoucherType()
        {            

            try
            {
                var lookups = await _service.GetVoucherTypes();
                return Ok(new APIPagedResponse<List<VoucherTypeEntity>>(lookups, lookups.Count()));
            }            
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<List<VoucherTypeEntity>>(null, 0, true, "Exception occurred, please retry!"));

            }

        }


        #region Voucher
        
        [HttpPost("GetAllVoucherList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherHeaderEntity>>> GetAllVoucherList()
        {           
            try
            {
                var lookups = await _service.GetAllVoucherList();
                return Ok(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(lookups, lookups.Count()));
            }           
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return BadRequest(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(null, 0, true, "Exception occurred, please retry!"));

            }

        }

        [HttpPost("GetAllVouchers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherHeaderEntity>>> GetAllVouchers(int start, int limit, string sort, string dir, string record)
        {

            try
            {
                var lookups = await this._service.GetAllVouchers(start, limit, sort, dir, record);
                return Ok(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<VoucherHeaderEntity>(null, 0, true, "Exception occurred, please retry!"));
            }

        }


        [HttpPost("GetVoucher")]
        public async Task<IEnumerable<VoucherHeaderEntity>> GetVoucher(string voucherId)
        {
            Guid id;
            Guid.TryParse(voucherId, out id);

            return await this._service.GetVoucher(id);
        }

        [HttpPost("GetVoucherDetails")]
        public async Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails(string voucherId)
        {
            Guid id;
            Guid.TryParse(voucherId, out id);

            return await this._service.GetVoucherDetails(id);
        }

        #endregion

       

        #region Transaction

        [HttpPost("GetTransactionHeaders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherHeaderEntity>>> GetTransactionHeaders()
        {           

            try
            {
                var lookups = await this._service.GetTransactionHeaders();
                return Ok(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<VoucherHeaderEntity>(null, 0, true, "Exception occurred, please retry!"));
            }

        }

        [HttpPost("GetTransactionList")]
        public async Task<IEnumerable<VoucherDetailEntity>> GetTransactionList(int start, int limit, string sort, string dir, string record)
        {

            return await this._service.GetTransactionList(start, limit, sort, dir, record);
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
                var lookups = await this._service.GetCollectionVouchers();
                return Ok(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<VoucherHeaderEntity>(null, 0, true, "Exception occurred, please retry!"));
            }

        }

        [HttpPost("GetCollectionVoucherTypes")]
        public async Task<IEnumerable<object>> GetCollectionVoucherTypes()
        {
            const string VoucherType = "lupVoucherType";
            var filtered = await this._lookupManager.GetAllLookup(VoucherType);
            filtered = filtered.Where(l => l.Name == "CRV" || l.Name == "CRSV" || l.Name == "BD");

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
                var lookups = await this._service.GetPaymentVouchers();
                return Ok(new APIPagedResponse<IEnumerable<VoucherHeaderEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<VoucherHeaderEntity>(null, 0, true, "Exception occurred, please retry!"));
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


        #region VoucherTypeSetting
        [HttpPost("GetVoucherTypeSetting")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherTypeSettingEntity>>> GetVoucherTypeSetting()
        {
            try
            {
                var lookups = await this._service.GetVoucherTypeSettings();
                return Ok(new APIPagedResponse<IEnumerable<VoucherTypeSettingEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<VoucherTypeSettingEntity>(null, 0, true, "Exception occurred, please retry!"));
            }

        }

        [HttpPost("GetVoucherTypesSettingByParam")]
        public async Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypesSettingByParam(string voucherCode)
        {
            Guid id;
            Guid.TryParse(voucherCode, out id);
            return await this._service.GetVoucherTypesSettingByParam(id);
        }


        #endregion

        #region Payment Request

        [HttpPost("GetApprovedPaymentRequests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<object>>> GetApprovedPaymentRequests()
        {                     

            try
            {
                const string paymentRequests = "psmsPaymentRequest";
                var filtered = await this._lookupManager.GetAllLookup(paymentRequests);
                //filtered = filtered.Where(l => l.Name == "CPV" || l.Name == "BPV" || l.Name == "PCPV");

                var voucherTypes = filtered.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Code,
                });


                return Ok(new APIPagedResponse<object>(voucherTypes, voucherTypes.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<object>(null, 0, true, "Exception occurred, please retry!"));
            }

        }


        #endregion


        [HttpPost("SaveGeneraltSetting")]
        public async Task<ResponseDTO> SaveGeneraltSetting(SettingEntity setting)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {               
                await this._service.SaveSetting(setting);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record saved successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Closing accounts has not been saved successfully!";
                return response;
            }
        }

        [HttpPost("SaveFixedAssetSetting")]
        public async Task<ResponseDTO> SaveFixedAssetSetting(FixedAssetSettingEntity fixedAsset)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                await this._service.SaveFixedAssetSetting(fixedAsset);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Fixed asset setting has been added successfully!";

                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Closing accounts has not been saved successfully!";
                return null;
            }
        }

        [HttpPost("GetChartOfAccount")]
        public async Task<ChartOfAccountViewModel> GetChartOfAccount()
        {
            //return Ok(await Mediator.Send(new GetAllAccountTypesQuery()));
            //  var charAccount = await this._service.GetChartOfAccount();

            IEnumerable<ChartOfAccountEntity> chartOfAccountEntities = await this._service.GetChartOfAccount();
            IEnumerable<ChartOfAccountViewModel> chartOfAccountViewModels = chartOfAccountEntities.Select(x => new ChartOfAccountViewModel(x));

            ChartOfAccountViewModel chartOfAccounts = new ChartOfAccountViewModel
            {
                Id = null,
                text = "Chart Of Accounts",
                expanded = true,
                iconCls = "x-fa fa-sitemap",
                children = chartOfAccountViewModels.ToList()
            };

            return chartOfAccounts;
            
        }


        [HttpPost("GetCashiers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<CashierEntity>>> GetCashiers()
        {            

            try
            {
                var lookups = await _service.GetCashiers();
                //Log.Information("Get all Voucher Types", logModel, ResponseStatus.Info);
                return Ok(new APIPagedResponse<IEnumerable<CashierEntity>>(lookups, lookups.Count()));
            }
           
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                // Log.Information(message, logModel, ResponseStatus.Error);

                return BadRequest(new APIPagedResponse<IEnumerable<CashierEntity>>(null, 0, true, "Exception occurred, please retry!"));

            }

        }

        //[HttpPost("SaveAccountGroup")]
        //public async Task<Response<bool>> SaveAccountGroup(AccountGroupEntity param)
        //{
        //    Response<> response = new ResponseDTO();
        //    new Response<Guid>(product.Id);
        //    try
        //    {
        //        Guid id;
        //        Guid.TryParse(param.Id, out id);

        //        CoreAccountGroup existingRecord = await this._service..Get<CoreAccountGroup>(x => x.Id == id);

        //        if (existingRecord == null)
        //        {
        //            CoreAccountGroup accountGroup = param.MapToModel<CoreAccountGroup>();
        //            await this._service.Add<CoreAccountGroup>(accountGroup);
        //        }
        //        else
        //        {
        //            CoreAccountGroup coreAccountGroup = await this._service.Get<CoreAccountGroup>(x => x.Id == id);
        //            CoreAccountGroup accountGroup = param.MapToModel<CoreAccountGroup>(coreAccountGroup);

        //            await this._service.Edit(accountGroup);

        //        }

        //        response.ResponseStatus = ResponseStatus.Success.ToString();
        //        response.Message = "Record saved successfully!";
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ResponseStatus = ResponseStatus.Error.ToString();
        //        response.Message = "Error occured, Please retry or contact the admin!";
        //        return response;
        //    }
        //}

        //[HttpPost("DeleteAccountGroup")]
        //public async Task<ResponseDTO> DeleteAccountGroup(AccountGroupEntity param)
        //{
        //    ResponseDTO response = new ResponseDTO();

        //    try
        //    {
        //        Guid id;
        //        Guid.TryParse(param.Id, out id);

        //        await this._service.Delete<CoreAccountGroup>(x=>x.Id == id);
        //        response.ResponseStatus = ResponseStatus.Success.ToString();
        //        response.Message = "Record deleted successfully";
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ResponseStatus = ResponseStatus.Error.ToString();
        //        response.Message = "Error occured, Please retry or contact the admin!";
        //        return response;
        //    }
        //}



    }
}