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