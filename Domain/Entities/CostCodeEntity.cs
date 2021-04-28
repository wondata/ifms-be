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
             
        public CostCodeEntity(IfmsCostCode ifmsCost)
        {

            if (ifmsCost == null) return;
          //  this.Id = ifmsCost.Id;
            this.Name = ifmsCost.Name;
            this.Code = ifmsCost.Code;
        }



    }
}
