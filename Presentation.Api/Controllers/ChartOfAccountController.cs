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
using Serilog;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartOfAccountController : BaseController
    {
        private readonly IFinancialSetupManager _service;

        public ChartOfAccountController(IFinancialSetupManager service)
        {
            _service = service;
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


        [HttpPost("GetCostCodes")]
        public async Task<IEnumerable<CostCodeEntity>> GetCostCodes()
        {
            return await this._service.GetCostCodes();
        }

        //[HttpPost("GetVoucherType")]
        //public async Task<IEnumerable<VoucherTypeEntity>> GetVoucherType()
        //{
        //    return await this._service.GetVoucherTypes();
        //}

        [HttpPost("GetVoucherType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherTypeEntity>>> GetVoucherType()
        {
            //BusinessLogModel logModel = new BusinessLogModel
            //{
            //    ActionName = "GetBankSetupLookups",
            //    BranchName = bankSetupPostModel.Branch.BranchName,
            //    Data = bankSetupPostModel
            //};

            try
            {
                var lookups = await _service.GetVoucherTypes();
                //Log.Information("Get all bank setups", logModel, ResponseStatus.Info);
                return Ok(new APIPagedResponse<List<VoucherTypeEntity>>(lookups));
            }
            //catch (CustomException e)
            //{
            //    return BadRequest(new APIResponse<BankLookupsEntity>(null, true, e.Message));

            //}
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                // Log.Information(message, logModel, ResponseStatus.Error);

                return BadRequest(new APIPagedResponse<List<VoucherTypeEntity>>(null, true, "Exception occurred, please retry!"));

            }

        }

        //[HttpPost("GetVoucherTypeSetting")]
        //public async Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSetting()
        //{
        //    return await this._service.GetVoucherTypeSettings();
        //}

        [HttpPost("GetVoucherTypeSetting")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherTypeSettingEntity>>> GetVoucherTypeSetting()
        {
            //IfmsVoucherTypeSetting logModel = new IfmsVoucherTypeSetting
            //{
            //    ActionName = "GetVoucherTypeSetting",
            //    BranchName = bankSetupPostModel.Branch.BranchName,
            //    Data = bankSetupPostModel
            //};
           

            try
            {
                var lookups = await this._service.GetVoucherTypeSettings();


                //Log.Information("Get all bank setups", logModel, ResponseStatus.Info);
                return Ok(new APIPagedResponse<List<VoucherTypeSettingEntity>>(lookups));
            }
            //catch (CustomException e)
            //{
            //    return BadRequest(new APIResponse<BankLookupsEntity>(null, true, e.Message));

            //}
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
               // Log.Information(message, logModel, ResponseStatus.Error);

                return BadRequest(new APIPagedResponse<VoucherTypeSettingEntity>(null, true, "Exception occurred, please retry!"));
                //return BadRequest(new APIPagedResponse<VoucherTypeSettingEntity>(null, true, "Exception occurred, please retry!"));

            }

        }


        //public async Task<ActionResult<ApiPagedResponse<BankLookupsEntity>>> GetBankSetupLookups(APIPostModel<object> bankSetupPostModel)
        // {
        //    BusinessLogModel logModel = new BusinessLogModel
        //    {
        //        ActionName = "GetBankSetupLookups",
        //        BranchName = bankSetupPostModel.Branch.BranchName,
        //        Data = bankSetupPostModel
        //    };
        //
        //    try
        //    {
        //        var lookups = await _service.GetBankLookups();
        //        Log.Information("Get all bank setups", logModel, ResponseStatus.Info);
        //        return Ok(new APIResponse<BankLookupsEntity>(lookups));
        //    }
        //    catch (CustomException e)
        //    {
        //        return BadRequest(new APIResponse<BankLookupsEntity>(null, true, e.Message));

        //    }
        //    catch (System.Exception ex)
        //    {
        //        string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //        Log.Information(message, logModel, ResponseStatus.Error);

        //        return BadRequest(new APIResponse<BankLookupsEntity>(null, true, "Exception occurred, please retry!"));

        //    }

        // }

        [HttpPost("SaveGeneraltSetting")]
        public async Task SaveGeneraltSetting(SettingPostModel settingPostModel)
        {
            SettingEntity response = new SettingEntity();

            try
            {
                SettingEntity settingEntity = settingPostModel.MapToEntity();

                await this._service.SaveSetting(settingEntity);

               // return response;
            }
            catch (Exception ex)
            {
                //response.ResponseStatus = ResponseStatus.Error.ToString();
                //response.Message = "Closing accounts has not been saved successfully!";
                //return null;
            }
        }

        [HttpPost("SaveFixedAssetSetting")]
        public async Task SaveFixedAssetSetting(FixedAssetPostModle fixedAssetPost)
        {
            FixedAssetSettingEntity response = new FixedAssetSettingEntity();

            try
            {
                FixedAssetSettingEntity fixedEntity = fixedAssetPost.MapToEntity();

                 await this._service.SaveFixedAssetSetting(fixedEntity);

                //return response;
            }
            catch (Exception ex)
            {
                //response.ResponseStatus = ResponseStatus.Error.ToString();
                //response.Message = "Closing accounts has not been saved successfully!";
                //return null;
            }
        }

        [HttpPost("GetChartOfAccount")]
        public async Task<IEnumerable<AccountTypeEntity>> GetChartOfAccount()
        {
            //return Ok(await Mediator.Send(new GetAllAccountTypesQuery()));
            return null;
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