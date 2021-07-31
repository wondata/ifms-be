﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public partial class VoucherTypeEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public string value { get; set; }
        public string text { get; set; }
        public string? Description { get; set; }     

        public VoucherTypeEntity()
        {
        }

        public VoucherTypeEntity(LupVoucherType  lupVoucher)
        {
            this.Id = lupVoucher.Id;
            this.Name = lupVoucher.Name;
            this.Code = lupVoucher.Code;
            this.value = lupVoucher.Id.ToString();
            this.text = lupVoucher.Name;
            this.Description = lupVoucher.Description;

        }
    }
}
