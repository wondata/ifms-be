using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CostCodeEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public CostCodeEntity()
        {
        }

        public CostCodeEntity(IfmsCostCode ifmsCost)
        {
            if (ifmsCost == null) return;

            this.Id = ifmsCost.Id;
            this.Name = ifmsCost.Name;
            this.Code = ifmsCost.Code;
        }

        public IfmsCostCode MapToModel()
        {
            IfmsCostCode ifmsCost = new IfmsCostCode
            {
                Id = this.Id,
                Name = this.Name,
                Code = this.Code,
             };
            return ifmsCost;
        }

        public IfmsCostCode MapToModel( IfmsCostCode ifmsCost)
        {            
                ifmsCost.Id = this.Id;
                ifmsCost.Name = this.Name;
                ifmsCost.Code = this.Code;
            
            return ifmsCost;
        }

        //public override T MapToModel<T>()
        //{
        //    Guid id;
        //    Guid.TryParse(this.Id, out id);

        //    IfmsCostCode costCode = new IfmsCostCode
        //    {
        //        Id = id,
        //        Name = this.Name,
        //        Code = this.Code
        //    };

        //    return costCode as T;
        //}

        //public override T MapToModel<T>(T t)
        //{

        //    IfmsCostCode costCode = t as IfmsCostCode;
            
        //    Id = this.Id;
        //    Name = this.Name;
        //    Code = this.Code;
            

        //    return costCode as T;
        //}
    }
}
