using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public partial class CostCenterEntity 
    {   
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? ParentId { get; set; }   
        public List<CostCenterEntity> Children { get; set; }

        public CostCenterEntity()
        {
        }

        public CostCenterEntity(CoreCostCenter coreCost)
        {
            this.Id = coreCost.Id;
            this.Name = coreCost.Name;
            this.Code = coreCost.Code;
            this.ParentId = coreCost.ParentId;          
            this.Children =
               coreCost.InverseParent == null ? new List<CostCenterEntity>() :
               coreCost.InverseParent.Select(x => new CostCenterEntity(x)).ToList();

        }

       
    }
}
