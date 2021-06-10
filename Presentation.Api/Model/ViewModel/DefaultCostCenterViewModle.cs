using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.ViewModel
{
    public class DefaultCostCenterViewModle
    {
        public string DefaultCostCenterId { get; set; }

        public string DefaultCostCenter { get; set; }

        public DefaultCostCenterViewModle()
        {
        }

        public DefaultCostCenterViewModle(SettingEntity setting)
        {
            if (setting == null) return ;

            this.DefaultCostCenterId = setting.DefaultCostCenterId.ToString();
            this.DefaultCostCenter = setting.CostCenter.Code.ToString();
        }
    }
}
