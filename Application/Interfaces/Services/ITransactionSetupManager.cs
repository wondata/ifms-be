using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ITransactionSetupManager
    {
        Task<IEnumerable<VoucherHeaderEntity>> GetAllVoucherList();
        Task<IEnumerable<VoucherHeaderEntity>> GetTransactionHeaders();
        Task<string> SaveVoucher(VoucherHeaderEntity voucherHeader, List<VoucherDetailEntity> voucherDetails);
        Task DeleteVoucher(Guid Id);
        Task DeleteVoucherDetail(Guid Id);
        Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails();
        Task<VoucherTypeSettingEntity> GetDefaultAccounts(Guid VoucherTypeId);
        Task<string> GetVoucherNumber(Guid CostCenterId, Guid VoucherTypeId);
        Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails(Guid id);
        Task<IEnumerable<VoucherHeaderEntity>> GetVoucherHeaderDetails(Guid id);
        Task<SettingEntity> GetSettings();
        Task<IEnumerable<VoucherDetailEntity>> GetTransactionDetails(List<Guid> id);
        Task<IEnumerable<VoucherDetailEntity>> GetTransactionDetails(Guid id);

        Task TransactionPost(List<VoucherHeaderEntity> voucherHeaders);
        Task TransactionUnpost(List<VoucherHeaderEntity> voucherHeaders);
        Task TransactionVoid(List<VoucherHeaderEntity> voucherHeaders);
        Task TransactionAdjust(List<VoucherHeaderEntity> voucherHeaders);
        Task TransactionDelete(List<VoucherHeaderEntity> voucherHeaders);
        Task<IEnumerable<VoucherHeaderEntity>> GetCollectionVouchers();
        Task SaveCollectionVoucher(VoucherHeaderEntity voucherHeader);
        Task<IEnumerable<VoucherHeaderEntity>> GetPaymentVouchers();
        Task SavePaymentVoucher(VoucherHeaderEntity voucherHeader);

        Task<string> GetVoucherNumber(Guid? id);

    }
}
