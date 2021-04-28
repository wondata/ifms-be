using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public partial class CostCenterEntity : BaseEntity
    {

    //    public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid ParentId { get; set; }
      //  public byte[] IsDeleted { get; set; }
      //  public DateTime? LastUpdated { get; set; }
      //  public DateTime? CreatedAt { get; set; }
      //  public DateTime? UpdatedAt { get; set; }

        public CostCenterEntity()
        {
        }

        public CostCenterEntity(CoreCostCenter coreCost)
        {
            this.Id = coreCost.Id.ToString();
            this.Name = coreCost.Name;
            this.Code = coreCost.Code;
            this.ParentId = coreCost.ParentId;
            
        }

        public override T MapToModel<T>()
        {
            throw new NotImplementedException();
        }

        public override T MapToModel<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}
