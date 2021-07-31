using Application.DTOs;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Model.PostModel;
using Presentation.Api.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IfmsController : BaseController
    {
        private readonly IFinancialSetupManager _service;
        private readonly ILookupManager _lookupManager;

        public IfmsController(IFinancialSetupManager service, ILookupManager lookupManager)
        {
            _service = service;
            _lookupManager = lookupManager;
        }

        [HttpPost("GetAccounts")]
        public async Task<IEnumerable<SubsidiaryAccountEntity>> GetAccounts(LookupPostModel accounts)
        {
            return await this._service.GetSubsidiaryAccounts(accounts.Name);
        }

        [HttpPost("GetAccountList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<SubsidiaryAccountEntity>>> GetAccountList()
        {
            try
            {
                var lookups = await _service.GetSubsidiaryAccountList();
                return Ok(new APIPagedResponse<IEnumerable<SubsidiaryAccountEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<IEnumerable<SubsidiaryAccountEntity>>(null, 0, true, "Exception occurred, please retry!"));

            }
        }

        [HttpPost("GetControlAccountList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<ControlAccountEntity>>> GetControlAccountList()
        {
            try
            {
                var lookups = await _service.GetControlAccountList();
                return Ok(new APIPagedResponse<IEnumerable<ControlAccountEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<IEnumerable<ControlAccountEntity>>(null, 0, true, "Exception occurred, please retry!"));

            }
        }

        [HttpPost("GetControlAccountsByParam")]
        public async Task<IEnumerable<ControlAccountEntity>> GetControlAccountsByParam(LookupPostModel accountCode)
        {
            return await this._service.GetControlAccountsByParam(accountCode.Name);
        }
        

        [HttpPost("GetDefaultCostCenter")]
        public async Task<IEnumerable<DefaultCostCenterViewModle>> GetDefaultCostCenter()
        {

            IEnumerable<SettingEntity> settingEntities = await this._service.GetSettings();
            IEnumerable<DefaultCostCenterViewModle> settings = settingEntities.Select(x => new DefaultCostCenterViewModle(x));

            return settings;
        }

        [HttpPost("GetFilteredCostCenters")]
        public async Task<IEnumerable<CostCenterEntity>> GetFilteredCostCenters(string filteredCostCeter)
        {
            return await this._service.GetFilteredCostCenters(filteredCostCeter);
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

        [HttpPost("SaveCostCode")]
        public async Task<ResponseDTO> SaveCostCode(CostCodePostModel costCode)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                CostCodeEntity cost = costCode.MapToEntity();
                await this._service.SaveCostCode(cost);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record saved successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Error occured, Please retry or contact the admin!";
                return response;
            }
        }

        [HttpPost("DeleteCostCode")]
        public async Task<ResponseDTO> DeleteCostCode(CostCodePostModel costCode)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                Guid id;
                Guid.TryParse(costCode.Id, out id);

                await this._service.DeleteCostCode(id);
                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Error occured, Please retry or contact the admin!";
                return response;
            }
        }

        [HttpPost("GetModePayment")]
        public async Task<ActionResult<APIPagedResponse<object>>> GetModePayment()
        {            
            try
            {           
                const string LupBalance = "lupModeOfPayment";
                var filtered = await this._lookupManager.GetAllLookup(LupBalance);
                return Ok(new APIPagedResponse<IEnumerable<object>>(filtered, filtered.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<List<CostCodeEntity>>(null, 0, true, "Exception occurred, please retry!"));

            }
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

        [HttpPost("SaveCashier")]
        public async Task<ResponseDTO> SaveCashier(CashierPostModel cashier)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                CashierEntity cashiers = cashier.MapToEntity();
                await this._service.SaveCashier(cashiers);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record saved successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Error occured, Please retry or contact the admin!";
                return response;
            }
        }

        [HttpPost("DeleteCashier")]
        public async Task<ResponseDTO> DeleteCashier(CashierPostModel cashier)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                Guid id;
                Guid.TryParse(cashier.Id, out id);

                await this._service.DeleteCashier(id);
                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Error occured, Please retry or contact the admin!";
                return response;
            }
        }

        [HttpPost("GetUserList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<UserEntity>>> GetUserList()
        {

            try
            {
                var lookups = await _service.GetUserList();
                return Ok(new APIPagedResponse<IEnumerable<UserEntity>>(lookups, lookups.Count()));
            }

            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return BadRequest(new APIPagedResponse<IEnumerable<UserEntity>>(null, 0, true, "Exception occurred, please retry!"));

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

        [HttpPost("GetBalanceSides")]
        public async Task<IEnumerable<object>> GetBalanceSides()
        {
            const string LupBalance = "lupBalanceSide";
            var filtered = await this._lookupManager.GetAllLookup(LupBalance);

            var BalanceSide = filtered.Select(balanceSide => new
            {
                balanceSide.Id,
                balanceSide.Name,
                balanceSide.Code
            });


            return BalanceSide;
        }

       


        [HttpPost("GetPurposeTemplates")]
        public async Task<IEnumerable<PurposeTemplateEntity>> GetPurposeTemplates(string searchText)
        {            
            return await this._service.GetPurposeTemplate(searchText);
        }

      

        [HttpPost("GetAllPurposeTemplates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<PurposeTemplateEntity>>> GetAllPurposeTemplates()
        {
            try
            {
                var lookups = await _service.GetAllPurposeTemplates();
                return Ok(new APIPagedResponse<IEnumerable<PurposeTemplateEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<IEnumerable<PurposeTemplateEntity>>(null, 0, true, "Exception occurred, please retry!"));
            }

        }


        #region VoucherTypeSetting

        [HttpPost("GetVoucherTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherTypeEntity>>> GetVoucherTypes()
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

        [HttpPost("GetPaymentVoucherTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherTypeEntity>>> GetPaymentVoucherTypes()
        {
            try
            {
                var lookups = await _service.GetPaymentVoucherTypes();
                return Ok(new APIPagedResponse<IEnumerable<VoucherTypeEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<IEnumerable<VoucherTypeEntity>>(null, 0, true, "Exception occurred, please retry!"));
            }

        }

        [HttpPost("GetCollectionVoucherTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<VoucherTypeEntity>>> GetCollectionVoucherTypes()
        {
            try
            {
                var lookups = await _service.GetCollectionVoucherTypes();
                return Ok(new APIPagedResponse<IEnumerable<VoucherTypeEntity>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<IEnumerable<VoucherTypeEntity>>(null, 0, true, "Exception occurred, please retry!"));
            }

        }

        [HttpPost("GetVoucherTypeList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IEnumerable<object>> GetVoucherTypeList()
        {
            try
            {
                var lookups = await _service.GetVoucherTypes();
                var voucherType = lookups.Select(VoucherTypeEntity => new
                {
                    VoucherTypeEntity.Id,
                    Name = VoucherTypeEntity.Name,
                    text = VoucherTypeEntity.Name,
                    value = VoucherTypeEntity.Id,
                    VoucherType = VoucherTypeEntity.Name,
                    VoucherTypeEntity.Code,
                    VoucherTypeEntity.Description,
                });
                return voucherType.ToList();
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }

           

        }

        [HttpPost("GetVoucherTypeSetting")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<APIPagedResponse<object>>> GetVoucherTypeSetting()
        {
            try
            {
                var lookups = await this._service.GetVoucherTypeSettings();

                //var voucherTypeSettings = lookups.Select(voucherTypeSetting => new
                //{
                //    voucherTypeSetting.Id,
                //    CostCenter = voucherTypeSetting.CostCenter.Code,
                //    VoucherType = voucherTypeSetting.VoucherType.Name,
                //    DefaultAccount = voucherTypeSetting.DefaultAccountId == null ? "" : string.Format("{0}-{1}", voucherTypeSetting.SubsidiaryAccount.ControlAccount.Code, voucherTypeSetting.SubsidiaryAccount.Code),
                //    AccountTitle = voucherTypeSetting.DefaultAccountId == null ? "" : voucherTypeSetting.SubsidiaryAccount.Name,
                //    BalanceSide = voucherTypeSetting.BalanceSideId == null ? "" : voucherTypeSetting.BalanceSide.Name,
                //    voucherTypeSetting.StartingNumber,
                //    voucherTypeSetting.EndingNumber,
                //    voucherTypeSetting.CurrentNumber,
                //    voucherTypeSetting.NumberOfDigits
                //});
                return Ok(new APIPagedResponse<IEnumerable<object>>(lookups, lookups.Count()));
            }
            catch (System.Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new APIPagedResponse<object>(null, 0, true, "Exception occurred, please retry!"));
            }

        }



        [HttpPost("GetVoucherTypeSettingByParam")]
        public async Task<IEnumerable<VoucherTypeSettingEntity>> GetVoucherTypeSettingByParam(string voucherCode)
        {
            Guid id;
            Guid.TryParse(voucherCode, out id);
            return await this._service.GetVoucherTypeSettingByParam(id);
        }

        [HttpPost("SaveVoucherTypesSetting")]
        public async Task<ResponseDTO> SaveVoucherTypesSetting(VoucherTypeSettingEntity voucherTypeSetting)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                await this._service.SaveVoucherTypesSetting(voucherTypeSetting);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record saved successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Error occured, Please retry or contact the admin!";
                return response;
            }
        }


        [HttpPost("DeleteVoucherTypesSetting")]
        public async Task<ResponseDTO> DeleteVoucherTypeSetting(VoucherTypeSettingEntity voucherTypeSetting)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                Guid id;
                Guid.TryParse(voucherTypeSetting.Id, out id);

                await this._service.DeleteVoucherTypeSetting(id);
                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Record deleted successfully";
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

        [HttpPost("SaveGeneraltSetting")]
        public async Task<ResponseDTO> SaveGeneraltSetting(SettingPostModel setting)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                SettingEntity settingEntity = setting.MapToEntity();
                await this._service.SaveSetting(settingEntity);

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

        [HttpPost("SaveFixedAssetSetting")]
        public async Task<ResponseDTO> SaveFixedAssetSetting(FixedAssetPostModel fixedAsset)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                FixedAssetSettingEntity settingEntity = fixedAsset.MapToEntity();
                await this._service.SaveFixedAssetSetting(settingEntity);

                response.ResponseStatus = ResponseStatusEnum.Success.ToString();
                response.Message = "Fixed asset setting has been added successfully!";

                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusEnum.Error.ToString();
                response.Message = "Record has not been saved successfully!";
                return null;
            }
        }

        


    }
}
