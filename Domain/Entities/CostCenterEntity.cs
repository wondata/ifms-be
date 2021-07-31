using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public partial class CostCenterEntity : BaseEntity
    {   
     //   public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public string text { get; set; }
        public string value { get; set; }
        public Guid? ParentId { get; set; }   
        public List<CostCenterEntity> Children { get; set; }

        public CostCenterEntity()
        {
        }

        public CostCenterEntity(CoreCostCenter coreCost)
        {
            this.Id = coreCost.Id.ToString();
            this.Name = coreCost.Name;
            this.Code = coreCost.Code;
            this.value = coreCost.Id.ToString();
            this.text = coreCost.Name;
            this.ParentId = coreCost.ParentId;          
            this.Children =
               coreCost.InverseParent == null ? new List<CostCenterEntity>() :
               coreCost.InverseParent.Select(x => new CostCenterEntity(x)).ToList();

        }

        public override T MapToModel<T>()
        {
            Guid id;
            Guid.TryParse(this.Id, out id);

            CoreCostCenter coreCostCenter = new CoreCostCenter
            {
                Id = id,
                Name = this.Name,
                Code = this.Code
            };

            return coreCostCenter as T;
        }

        public override T MapToModel<T>(T t)
        {
            CoreCostCenter coreCostCenter = t as CoreCostCenter;

            coreCostCenter.Name = this.Name;
            coreCostCenter.Code = this.Code;

            return coreCostCenter as T;
        }
      


    }
}
