using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public partial class VoucherTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
       // public bool? IsFinanceVoucher { get; set; }
     //   public bool? IsDeleted { get; set; }
     //   public DateTime? LastUpdated { get; set; }

        public VoucherTypeEntity()
        {

        }

        public VoucherTypeEntity(LupVoucherType  lupVoucher)
        {
            this.Id = lupVoucher.Id;
            this.Name = lupVoucher.Name;
            this.Code = lupVoucher.Code;
            this.Description = lupVoucher.Description;

        }
    }
}
