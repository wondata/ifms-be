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
        Task SaveVoucher(VoucherHeaderEntity voucherHeader);
        Task DeleteVoucher(Guid Id);
        Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails();
        Task<IEnumerable<VoucherDetailEntity>> GetVoucherDetails(Guid id);
        Task<IEnumerable<VoucherHeaderEntity>> GetVoucherHeaderDetails(Guid id);

        Task<IEnumerable<VoucherDetailEntity>> GetTransactionDetails(Guid id);
        Task TransactionUnpost(IEnumerable<VoucherHeaderEntity> voucherDetails);
       
        Task<IEnumerable<VoucherHeaderEntity>> GetCollectionVouchers();
        Task SaveCollectionVoucher(VoucherHeaderEntity voucherHeader);
        Task<IEnumerable<VoucherHeaderEntity>> GetPaymentVouchers();
        Task SavePaymentVoucher(VoucherHeaderEntity voucherHeader);

        Task<string> GetVoucherNumber(Guid? id);

    }
}
