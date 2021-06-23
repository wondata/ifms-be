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
    public class VoucherController : BaseController
    {
        private readonly IFinancialSetupManager _service;
        private readonly ILookupManager _lookupManager;

        public VoucherController(IFinancialSetupManager service, ILookupManager lookupManager)
        {
            _service = service;
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


        //[HttpPost("GetVoucher")]
        //public async Task<IEnumerable<VoucherHeaderEntity>> GetVoucher(string voucherId)
        //{
        //    Guid id;
        //    Guid.TryParse(voucherId, out id);

        //    return await this._service.GetVoucher(id);
        //}

        [HttpPost("GetVoucherDetails")]
        public async Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails(string voucherId)
        {
            Guid id;
            Guid.TryParse(voucherId, out id);

            return await this._service.GetVoucherDetails(id);
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




    }
}
