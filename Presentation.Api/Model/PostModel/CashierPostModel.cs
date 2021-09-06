using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.PostModel
{   
    public class CashierPostModel
    {

        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string SubsidiaryAccountId { get; set; }

        public CashierPostModel()
        {
        }

        public CashierEntity MapToEntity()
        {
            Guid.TryParse(this.Id, out Guid id);

            Guid.TryParse(this.UserId, out Guid userId);

            Guid.TryParse(this.SubsidiaryAccountId, out Guid subsidiaryAccountId);

            CashierEntity cashier = new CashierEntity
            {

                Id = id,
                FullName = this.FullName,
                UserId = userId,
                SubsidiaryAccountId = subsidiaryAccountId,
            };

            return cashier;
        }
    }
}
