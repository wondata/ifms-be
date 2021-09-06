﻿using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TransactionSetupManager : ITransactionSetupManager
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConfiguration _configuration;
        private readonly ILookupManager _lookupManager;

        public TransactionSetupManager(ITransactionRepository transactionRepository, IConfiguration configuration, ILookupManager lookupManager)
        {
            _transactionRepository = transactionRepository;
            _configuration = configuration;
            _lookupManager = lookupManager;
        }

        public async Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails()
        {
            IQueryable<IfmsVoucherDetail> ifmsVoucher = await this._transactionRepository.GetVoucherDetails();
            List<VoucherDetailEntity> voucherDetail = ifmsVoucher.Select(x => new VoucherDetailEntity(x)).ToList();

            return voucherDetail;
        }

        public async Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails(Guid id)
        {
            IQueryable<IfmsVoucherDetail> ifmsVoucher = await this._transactionRepository.GetVoucherDetails();
            ifmsVoucher = ifmsVoucher.Where(x => x.VoucherHeaderId == id);
            IEnumerable<VoucherDetailEntity> voucherDetail = ifmsVoucher.Select(x => new VoucherDetailEntity(x));

            return voucherDetail;
        }

        public async Task<IEnumerable<VoucherHeaderEntity>> GetTransactionHeaders()
        {
            IQueryable<IfmsVoucherHeader> ifmsVoucher = await this._transactionRepository.GetVoucherHeaders();
            ifmsVoucher = ifmsVoucher.Where( c => c.VoucherType.Name != "PCPV");
            IEnumerable<VoucherHeaderEntity> voucher = ifmsVoucher.Select(x => new VoucherHeaderEntity(x));

            return voucher;
        }

        public async Task<IEnumerable<VoucherHeaderEntity>> GetAllVoucherList()
        {

            IQueryable<IfmsVoucherHeader> ifmsVoucher = await this._transactionRepository.GetVoucherHeaders();
            ifmsVoucher = ifmsVoucher.Where(x => x.VoucherType.Name != "PCPV");
            IEnumerable<VoucherHeaderEntity> voucher = ifmsVoucher.Select(x => new VoucherHeaderEntity(x));

            return voucher;
        }

        public async Task<IEnumerable<VoucherHeaderEntity>> GetVoucherHeaderDetails(Guid id)
        {
            IQueryable<IfmsVoucherHeader> ifmsVoucher = await this._transactionRepository.GetVoucherHeaders();
            ifmsVoucher = ifmsVoucher.Where(x => x.Id == id);
            IEnumerable<VoucherHeaderEntity> voucherDetail = ifmsVoucher.Select(x => new VoucherHeaderEntity(x)).ToList();

            return voucherDetail;
        }

        public async Task<string> SaveVoucher(VoucherHeaderEntity voucherHeader, List<VoucherDetailEntity> voucherDetails)
        {
            Guid.TryParse(voucherHeader.Id.ToString(), out Guid id);
            var isNew = false;
            // get Current Period
            var period = await this.GetPeriod(voucherHeader.Date);
            if (period != null)
            {
                voucherHeader.PeriodId = period.Id;
            }
            else
            {
                return "Period is not defined for the selected transaction date";
            }

            IfmsVoucherHeader existingRecord = await this._transactionRepository.GetAsync<IfmsVoucherHeader>(x => x.Id == id
                && x.CostCenterId == voucherHeader.CostCenterId
                && x.ReferenceNo == voucherHeader.ReferenceNo
                && x.VoucherTypeId == voucherHeader.VoucherTypeId);

            if (existingRecord != null)
            {
                return "Voucher Header has already been registered!";
            }

            if (id == null) 
            {                
                id = Guid.NewGuid();
                isNew = true;

                IfmsVoucherHeader voucher = voucherHeader.MapToModel();

                await this._transactionRepository.AddAsync(voucher);
                await this._transactionRepository.UnitOfWork.SaveChanges();               
            }else
            {
                IfmsVoucherHeader voucher = await this._transactionRepository.GetAsync<IfmsVoucherHeader>(x => x.Id == id);
                IfmsVoucherHeader header = voucherHeader.MapToModel(voucher);

                await this._transactionRepository.UpdateAsync(header);
                await this._transactionRepository.UnitOfWork.SaveChanges();
            }

            await this.SaveVoucherDetail(id, voucherDetails);

            if (isNew)
            {
                await this.UpdateVoucherNumber(voucherHeader.CostCenterId, voucherHeader.VoucherTypeId);
            }
            else {              
                return "Voucher has been added successfully";
            }
            return "";
        }

        
        public async Task SaveVoucherDetail( Guid voucherHeaderId , List<VoucherDetailEntity> voucherDetails) {
            if (voucherDetails.Count > 0) {
                foreach (var item in voucherDetails) {

                    Guid.TryParse(item.Id , out Guid ids);
                    if (ids != Guid.Empty)
                    {
                        var detailVoucher = await this._transactionRepository.GetAsync<IfmsVoucherDetail>(x => x.Id == ids);
                        if (detailVoucher != null)
                        {
                            detailVoucher.VoucherHeaderId = voucherHeaderId;
                            detailVoucher.CostCenterId = item.CostCenterId;
                            detailVoucher.ControlAccountId = item.ControlAccountId;
                            detailVoucher.SubsidiaryAccountId = item.SubsidiaryAccountId;
                            detailVoucher.DebitAmount = item.DebitAmount;
                            detailVoucher.CreditAmount = item.CreditAmount;
                            detailVoucher.Remark = item.Remark;
                            detailVoucher.IsInterBranchTransactionCleared = item.IsInterBranchTransactionCleared;

                            if (item.CostCodeId != Guid.Empty)
                            {
                                detailVoucher.CostCodeId = item.CostCodeId;
                            }
                            if (item.ReferenceVoucherHeaderId != Guid.Empty)
                            {
                                detailVoucher.ReferenceVoucherHeaderId = item.ReferenceVoucherHeaderId;
                            }
                            //if (item.StaffCode != Guid.Empty)
                            //{
                            //    detailVoucher.StaffCode = item.StaffCode;
                            //}
                            //if (item.customerId != Guid.Empty)
                            //{
                            //    detailVoucher.customerId = item.customerId;
                            //}
                            //if (item.supplierId != Guid.Empty)
                            //{
                            //    detailVoucher.supplierId = item.supplierId;
                            //}
                            if (item.CaseId != Guid.Empty)
                            {
                                detailVoucher.CaseId = item.CaseId;
                            }
                            if (item.ProjectTaskId != Guid.Empty)
                            {
                                detailVoucher.ProjectTaskId = item.ProjectTaskId;
                            }
                            if (item.IBTReferenceVoucherHeaderId != Guid.Empty)
                            {
                                detailVoucher.IBTReferenceVoucherHeaderId = item.IBTReferenceVoucherHeaderId;
                            }
                        }
                    }
                    else {
                        var detailVoucher = new IfmsVoucherDetail
                        {
                            Id = Guid.NewGuid(),
                            VoucherHeaderId = voucherHeaderId,
                            CostCenterId = item.CostCenterId,
                            ControlAccountId = item.ControlAccountId,
                            SubsidiaryAccountId = item.SubsidiaryAccountId,
                            DebitAmount = item.DebitAmount,
                            CreditAmount = item.CreditAmount
                        };

                        if (item.CostCodeId != Guid.Empty)
                        {
                            detailVoucher.CostCodeId = item.CostCodeId;
                        }
                        if (item.ReferenceVoucherHeaderId != Guid.Empty)
                        {
                            detailVoucher.ReferenceVoucherHeaderId = item.ReferenceVoucherHeaderId;
                        }
                        //if (item.CustomerId != Guid.Empty)
                        //{
                        //    detailVoucher.CustomerId = item.CustomerId;
                        //}
                        //if (item.SupplierId != Guid.Empty)
                        //{
                        //    detailVoucher.SupplierId = item.SupplierId;
                        //}
                        //if (item.CaseNoId != Guid.Empty)
                        //{
                        //    detailVoucher.CaseId = item.CaseNoId;
                        //}
                        //if (item.StaffCode != Guid.Empty)
                        //{
                        //    detailVoucher.StaffId = item.StaffCode;
                        //}
                        if (item.ProjectTaskId != Guid.Empty)
                        {
                            detailVoucher.ProjectTaskId = item.ProjectTaskId;
                        }
                        if (item.IBTReferenceVoucherHeaderId != Guid.Empty)
                        {
                            detailVoucher.IBTReferenceVoucherHeaderId = item.IBTReferenceVoucherHeaderId;
                        }
                        detailVoucher.IsInterBranchTransactionCleared = item.IsInterBranchTransactionCleared;
                        detailVoucher.Remark = item.Remark;


                        await this._transactionRepository.AddAsync(detailVoucher);
                        await this._transactionRepository.UnitOfWork.SaveChanges();

                    }
                }
            }
        }



        public async Task DeleteVoucher(Guid id)
        {
            await this._transactionRepository.DeleteAsync<IfmsBankReconciliationDetail>(x => x.VoucherDetailId == id);
            await this._transactionRepository.UnitOfWork.SaveChanges();

            await this._transactionRepository.DeleteAsync<IfmsVoucherDetail>(x => x.Id == id);
            await this._transactionRepository.UnitOfWork.SaveChanges();

        }

        public async Task<IEnumerable<VoucherDetailEntity>> GetTransactionDetails(Guid id)
        {
            IQueryable<IfmsVoucherDetail> ifmsVoucher = await this._transactionRepository.GetVoucherDetails();
            ifmsVoucher = ifmsVoucher.Where(x => x.VoucherHeaderId == id);
            IEnumerable<VoucherDetailEntity> voucherDetail = ifmsVoucher.Select(x => new VoucherDetailEntity(x)).ToList();

            return voucherDetail;
        }

        public async Task<IEnumerable<VoucherDetailEntity>> GetTransactionDetails(List<Guid> voucherIds)
        {
            //List<VoucherDetailEntity> voucherDetails = null;
            //foreach (var item in id)
            //{
            //    Guid.TryParse(item, out Guid Id);

            //}

            IQueryable<IfmsVoucherDetail> ifmsVoucher = (await this._transactionRepository.GetVoucherDetails())
                .Where(x => voucherIds.Contains(x.VoucherHeaderId));
            //ifmsVoucher = ifmsVoucher.Where(x => x.VoucherHeaderId == Id);

            IEnumerable<VoucherDetailEntity> voucherDetail = ifmsVoucher.Select(x => new VoucherDetailEntity(x));

            return voucherDetail;
        }

        public async Task<IEnumerable<VoucherHeaderEntity>> GetCollectionVouchers()
        {
            IQueryable<IfmsVoucherHeader> ifmsVoucher = await this._transactionRepository.GetVoucherHeaders();
           //ifmsVoucher = ifmsVoucher.Where(x => x.IsPosted == false && x.Description == "Collection Voucher" && (x.VoucherType.Name == "CRV" || x.VoucherType.Name == "BD") );
            IEnumerable<VoucherHeaderEntity> voucher = ifmsVoucher.Select(x => new VoucherHeaderEntity(x));

            return voucher;
        }

        public async Task<CorePeriod> GetPeriod(DateTime? date)
        {

            var period = await this._transactionRepository.GetAsync<CorePeriod>(x => x.StartDate <= date && x.EndDate >= date);
            return period;
        }

        public async Task SaveCollectionVoucher(VoucherHeaderEntity voucherEntity)
        {
            Guid.TryParse(voucherEntity.Id, out Guid id);

            // get Current Period
            var period = await this.GetPeriod(voucherEntity.Date);
            if (period != null)
            {
                voucherEntity.PeriodId = period.Id;
            }
            else
            {
                return;
            }

            //Get Owner Cost Center      
            if (voucherEntity.CostCenterId == null)
            {
                return;
            }

            var referenceNo = await this.GetVoucherNumber(voucherEntity.CostCenterId, voucherEntity.VoucherTypeId);
            if (referenceNo == String.Empty)
            {
                return;
            }

            ////Get Default Account
            //IfmsCashier objCashier = await this._transactionRepository.GetAsync<IfmsCashier>(c => c.UserId == currentUser.Id).FirstOrDefault();
            //if (objCashier == null)
            //{
            //    return;
            //}


            IfmsVoucherHeader existingRecord = await this._transactionRepository.GetAsync<IfmsVoucherHeader>(x => x.Id == id
                && x.CostCenterId == voucherEntity.CostCenterId
                && x.ReferenceNo == voucherEntity.ReferenceNo
                && x.VoucherTypeId == voucherEntity.VoucherTypeId);

            if (existingRecord == null)
            {

                voucherEntity.ReferenceNo = referenceNo;
                voucherEntity.DocumentNo = referenceNo;
                voucherEntity.CreatedBy = "CyberERP";
                voucherEntity.IsPosted = true;
                voucherEntity.IsAdjustment = false;
                voucherEntity.IsVoid = false;
                voucherEntity.IsDeleted = false;
                voucherEntity.PostedBy = "admin";
                voucherEntity.CreatedAt = DateTime.Now;
                voucherEntity.UpdatedAt = DateTime.Now;

                IfmsVoucherHeader voucher = voucherEntity.MapToModel();

                await this._transactionRepository.AddAsync(voucher);
                await this._transactionRepository.UnitOfWork.SaveChanges();

                await this.UpdateVoucherNumber(voucherEntity.CostCenterId, voucherEntity.VoucherTypeId);

            }
            else
            {
                voucherEntity.DocumentNo = voucherEntity.ReferenceNo;

                IfmsVoucherHeader voucher = await this._transactionRepository.GetAsync<IfmsVoucherHeader>(x => x.Id == id);
                IfmsVoucherHeader voucherHeader = voucherEntity.MapToModel(voucher);

                await this._transactionRepository.UpdateAsync(voucherHeader);
                await this._transactionRepository.UnitOfWork.SaveChanges();

            }

            //Save Bank Detail for Bank Deposit voucher type
            var voucherType = await this._transactionRepository.GetAsync<LupVoucherType>(x => x.Id == voucherEntity.VoucherTypeId);
            if (voucherType != null && voucherType.Name == "BD")
            {
                //Get Default Account
                var subAccount = await this._transactionRepository.GetAsync<CoreSubsidiaryAccount>(x => x.Id == voucherEntity.AccountId);
                if (subAccount == null)
                {
                    // "Account is not selected.
                    return;
                }            

                var cashierVoucherDetail = new IfmsVoucherDetail
                {
                    VoucherHeaderId = id,
                    CostCenterId = voucherEntity.CostCenterId,
                    //ControlAccountId = objCashier.coreSubsidiaryAccount.ControlAccountId,
                    //SubsidiaryAccountId = objCashier.SubsidiaryAccountId,
                    DebitAmount = 0,
                    CreditAmount = voucherEntity.Amount == null ? 0 : (decimal)voucherEntity.Amount,
                    IsDeleted = false
                };

                var bankVoucherDetail = new IfmsVoucherDetail
                {
                    VoucherHeaderId = id,
                    CostCenterId = voucherEntity.CostCenterId,
                    ControlAccountId = subAccount.ControlAccountId,
                    SubsidiaryAccountId = subAccount.Id,
                    DebitAmount = voucherEntity.Amount == null ? 0 : (decimal)voucherEntity.Amount,
                    CreditAmount = 0,
                    IsDeleted = false
                };

                await this.DeleteVoucherDetail(id);

                await this._transactionRepository.AddAsync(bankVoucherDetail);
                await this._transactionRepository.AddAsync(cashierVoucherDetail);

                await this._transactionRepository.UnitOfWork.SaveChanges();
            }
            else
            {

                var crvVoucherDetail = new IfmsVoucherDetail
                {
                    VoucherHeaderId = id,
                    CostCenterId = voucherEntity.CostCenterId,
                    //ControlAccountId = objCashier.coreSubsidiaryAccount.ControlAccountId,
                    //SubsidiaryAccountId = objCashier.SubsidiaryAccountId,
                    DebitAmount = voucherEntity.Amount == null ? 0 : (decimal)voucherEntity.Amount,
                    CreditAmount = 0,
                    IsDeleted = false
                };

                await this.DeleteVoucherDetail(id);

                await this._transactionRepository.AddAsync(crvVoucherDetail);
                await this._transactionRepository.UnitOfWork.SaveChanges();

            }

        }

        public async Task DeleteVoucherDetail(Guid id)
        {
            await this._transactionRepository.DeleteAsync<IfmsVoucherDetail>(x => x.VoucherHeaderId == id);
            await this._transactionRepository.UnitOfWork.SaveChanges();
        }

        public async Task DeleteVoucherHeader(Guid id)
        {
            await this._transactionRepository.DeleteAsync<IfmsVoucherHeader>(x => x.Id == id);
            await this._transactionRepository.UnitOfWork.SaveChanges();
        }

        public async Task UpdateVoucherNumber(Guid costCenterId, Guid voucherTypeId)
        {
            var typeSetting = await this._transactionRepository.GetAsync<IfmsVoucherTypeSetting>(s => s.VoucherTypeId == voucherTypeId && s.CostCenterId == costCenterId);
            if (typeSetting != null)
            {
                typeSetting.CurrentNumber += 1;
            }
            await this._transactionRepository.UnitOfWork.SaveChanges();
        }

        public async Task<Guid> GetDefaultCostCenterId()
        {
            IfmsSetting objSetting = await this._transactionRepository.GetAsync<IfmsSetting>(x => x.DefaultCostCenterId != null);
            return objSetting != null ? (Guid)objSetting.DefaultCostCenterId : (Guid)objSetting.DefaultCostCenterId;
        }

        public async Task<string> GetVoucherNumber(Guid costCenterId, Guid voucherTypeId)
        {

            var voucherNumber = await this._transactionRepository.GetAsync<IfmsVoucherTypeSetting>(x => x.VoucherTypeId == voucherTypeId && x.CostCenterId == costCenterId);
            var format = voucherNumber == null ? null : this.GetVoucherFormat(voucherNumber.NumberOfDigits);
            var currentNumber = voucherNumber == null ? null : string.Format(format, voucherNumber.CurrentNumber);

            return currentNumber;
        }

        public async Task<string> GetVoucherNumber(Guid? voucherTypeId)
        {
            Guid defaultCostCenterId = await this.GetDefaultCostCenterId();

            var voucherNumber = await this._transactionRepository.GetAsync<IfmsVoucherTypeSetting>(x => x.VoucherTypeId == voucherTypeId && x.CostCenterId == defaultCostCenterId);
            if (voucherNumber != null)
            {
                var format = this.GetVoucherFormat(voucherNumber.NumberOfDigits);
                var currentNumber = string.Format(format, voucherNumber.CurrentNumber);

                return currentNumber;
            }
            else {
                return null;
            }
            
        }

        public string GetVoucherFormat(int numberOfDigits)
        {
            var format = "{0:";
            for (var i = 0; i < numberOfDigits; i++)
            {
                format += "0";
            }
            format += "}";
            return format;
        }

        public async Task<IEnumerable<VoucherHeaderEntity>> GetPaymentVouchers()
        {
            IQueryable<IfmsVoucherHeader> ifmsVoucher = await this._transactionRepository.GetVoucherHeaders();
            //ifmsVoucher = ifmsVoucher.Where(x => x.IsPosted == false && x.PostedFromOperation == "Payment Voucher" && (x.VoucherType.Name == "BPV" || x.VoucherType.Name == "PCPV")  );
            IEnumerable<VoucherHeaderEntity> voucher = ifmsVoucher.Select(x => new VoucherHeaderEntity(x));

            return voucher;
        }


        public async Task SavePaymentVoucher(VoucherHeaderEntity voucherEntity)
        {
            Guid.TryParse(voucherEntity.Id, out Guid id);

            IfmsVoucherHeader existingRecord = await this._transactionRepository.GetAsync<IfmsVoucherHeader>(x => x.Id == id
                && x.CostCenterId == voucherEntity.CostCenterId
                && x.ReferenceNo == voucherEntity.ReferenceNo
                && x.VoucherTypeId == voucherEntity.VoucherTypeId);

            // get Current Period
            var period = await this.GetPeriod(voucherEntity.Date);
            if (period != null)
            {
                voucherEntity.PeriodId = period.Id;
            }
            else
            {
                return;
            }

            //Get Owner Cost Center      
            if (voucherEntity.CostCenterId == null)
            {
                return;
            }

            var referenceNo = await this.GetVoucherNumber(voucherEntity.CostCenterId, voucherEntity.VoucherTypeId);
            if (referenceNo == String.Empty)
            {
                return;
            }

            //Get Default Account
            var subAccount = await this._transactionRepository.GetAsync<CoreSubsidiaryAccount>(x => x.Id == voucherEntity.AccountId);
            if (subAccount == null)
            {
                return;
            }


            //var subAccount = await this._transactionRepository.GetAsync<IfmsVoucherDetail>(x => x.Id == existingRecord.Id);
            //if (subAccount == null)
            //{
            //    return;
            //}

          


            if (existingRecord == null)
            {
                voucherEntity.ReferenceNo = referenceNo;
                voucherEntity.DocumentNo = referenceNo;
                voucherEntity.CreatedBy = "CyberERP";
                voucherEntity.IsPosted = true;
                voucherEntity.IsAdjustment = false;
                voucherEntity.IsVoid = false;
                voucherEntity.IsDeleted = false;
                voucherEntity.PostedBy = "admin";
                voucherEntity.CreatedAt = DateTime.Now;
                voucherEntity.UpdatedAt = DateTime.Now;

                IfmsVoucherHeader voucher = voucherEntity.MapToModel();

               

                await this._transactionRepository.AddAsync(voucher);
                await this._transactionRepository.UnitOfWork.SaveChanges();

                await this.UpdateVoucherNumber(voucherEntity.CostCenterId, voucherEntity.VoucherTypeId);

            }
            else
            {
                voucherEntity.DocumentNo = voucherEntity.ReferenceNo;

                IfmsVoucherHeader voucher = await this._transactionRepository.GetAsync<IfmsVoucherHeader>(x => x.Id == id);
                IfmsVoucherHeader voucherHeader = voucherEntity.MapToModel(voucher);

                await this._transactionRepository.UpdateAsync(voucherHeader);
                await this._transactionRepository.UnitOfWork.SaveChanges();

            }

            //Save Voucher Detail
            var paymentVoucherDetail = new IfmsVoucherDetail
            {
                VoucherHeaderId = id,
                CostCenterId = voucherEntity.CostCenterId,
                ControlAccountId = subAccount.ControlAccountId,
                SubsidiaryAccountId = subAccount.Id,
                DebitAmount = 0,
                CreditAmount = voucherEntity.Amount == null ? 0 : (decimal)voucherEntity.Amount,
                IsDeleted = false
            };

            await this.DeleteVoucherDetail(id);

            await this._transactionRepository.UpdateAsync(paymentVoucherDetail);
            await this._transactionRepository.UnitOfWork.SaveChanges();

        }

        public async Task TransactionPost(List<VoucherHeaderEntity> voucherHeader)
        {
            foreach (var details in voucherHeader)
            {
                Guid id;
                Guid.TryParse(details.Id, out id);

                IfmsVoucherHeader voucher = await this._transactionRepository.GetAsync<IfmsVoucherHeader>(x => x.Id == id);
                voucher.IsPosted = true;

                await this._transactionRepository.UpdateAsync(voucher);
                await this._transactionRepository.UnitOfWork.SaveChanges();

            }
        }

        public async Task TransactionUnpost(List<VoucherHeaderEntity> voucherHeader)
        {
            foreach (var details in voucherHeader)
            {
                Guid id;
                Guid.TryParse(details.Id, out id);


                IfmsVoucherHeader voucher = await this._transactionRepository.GetAsync<IfmsVoucherHeader>(x => x.Id == id);
                voucher.IsPosted = false;

                await this._transactionRepository.UpdateAsync(voucher);
                await this._transactionRepository.UnitOfWork.SaveChanges();
            }

        }

        public async Task TransactionVoid(List<VoucherHeaderEntity> voucherHeader)
        {
            foreach (var headers in voucherHeader)
            {
                Guid id;
                Guid.TryParse(headers.Id, out id);

                IfmsVoucherHeader voucher = await this._transactionRepository.GetAsync<IfmsVoucherHeader>(x => x.Id == id);

                if (voucher != null && voucher.IsPosted == true && voucher.IsVoid == false)
                {
                    voucher.IsVoid = true;
                    await this._transactionRepository.UpdateAsync(voucher);
                    await this._transactionRepository.UnitOfWork.SaveChanges();

                    IEnumerable<IfmsVoucherDetail> details = await this._transactionRepository.FindAsync<IfmsVoucherDetail>(x => x.VoucherHeaderId == id);

                    foreach (var detail in details)
                    {
                        var debitAmount = detail.DebitAmount;
                        var creditAmount = detail.CreditAmount;

                        if (debitAmount == 0)
                        {
                            debitAmount = creditAmount;
                            creditAmount = 0;
                        }
                        else
                        {
                            creditAmount = debitAmount;
                            debitAmount = 0;
                        }

                        var newVoucherDetail = new IfmsVoucherDetail
                        {
                            VoucherHeaderId = voucher.Id,
                            CostCenterId = voucher.CostCenterId,
                            ControlAccountId = detail.ControlAccountId,
                            SubsidiaryAccountId = detail.SubsidiaryAccountId,
                            DebitAmount = debitAmount,
                            CreditAmount = creditAmount,
                            IsInterBranchTransactionCleared = detail.IsInterBranchTransactionCleared,
                            IBTReferenceVoucherHeaderId = detail.IBTReferenceVoucherHeaderId,
                            IsDeleted = false
                        };

                        await this._transactionRepository.UpdateAsync(newVoucherDetail);
                        await this._transactionRepository.UnitOfWork.SaveChanges();

                    }

                }

            }

            

        }

        public Task TransactionAdjust(List<VoucherHeaderEntity> voucherHeader)
        {
            throw new NotImplementedException();

        }

        public async Task TransactionDelete(List<VoucherHeaderEntity> voucherHeader)
        {
            foreach (var headers in voucherHeader)
            {
                Guid id;
                Guid.TryParse(headers.Id, out id);

                await this._transactionRepository.DeleteAsync<IfmsVoucherHeader>(x => x.Id == id);
                await this._transactionRepository.UnitOfWork.SaveChanges();
            }
                           
        }

        public async Task<VoucherTypeSettingEntity> GetDefaultAccounts(Guid VoucherTypeId)
        {
            IQueryable<IfmsVoucherTypeSetting> typeSetting = await this._transactionRepository.GetVoucherTypeSettings();
            typeSetting = typeSetting.Where(x => x.VoucherTypeId == VoucherTypeId);
            VoucherTypeSettingEntity voucherType = typeSetting.Select(x => new VoucherTypeSettingEntity(x)).FirstOrDefault();

            return voucherType;            
           
        }

        public async Task<SettingEntity> GetSettings()
        {

            IQueryable<IfmsSetting> ifmsSetting = await this._transactionRepository.GetSettings();
            SettingEntity settngs = ifmsSetting.Select(x => new SettingEntity(x)).FirstOrDefault();

            return settngs;

        }
    }
}
