using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.PostModel
{
    public class CostCodePostModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public CostCodePostModel()
        {
        }

        public CostCodeEntity MapToEntity()
        {
            Guid id;
            Guid.TryParse(this.Id, out id);

            CostCodeEntity costCode = new CostCodeEntity
            {

                Id = id,
                Name = this.Name,
                Code = this.Code
            };

            return costCode;
        }
    }
}
