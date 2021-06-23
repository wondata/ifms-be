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


        #region VoucherTypeSetting

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
                response.Message = "Record has not been saved successfully!";
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
                response.Message = "Record has not been saved successfully!";
                return null;
            }
        }

        


    }
}
