using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Model.ViewModel
{
    public class ChartOfAccountViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
       // public string ParentId { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public bool leaf { get; set; }
        public bool expanded { get; set; }
        public List<ChartOfAccountViewModel> children { get; set; }

        public ChartOfAccountViewModel()
        {

        }

        public ChartOfAccountViewModel(ChartOfAccountEntity chartOfAccountEntity)
        {
            if (chartOfAccountEntity == null) return;

            this.Id = chartOfAccountEntity.Id.ToString();
            this.Name = chartOfAccountEntity.Name;
            this.Code = chartOfAccountEntity.Code;
            this.Type = chartOfAccountEntity.Type;
         //   this.ParentId = chartOfAccountEntity.ParentId?.ToString();

            this.text = chartOfAccountEntity.Name;
            this.leaf = (chartOfAccountEntity.Children.Count() > 0 && chartOfAccountEntity.Type != ChartOfAccountEntity.ACCOUNT_TYPE) ? false : true;
            this.expanded = chartOfAccountEntity.Type == "Account Category" ? true : false;
            this.iconCls = this.leaf ? "x-fa fa-circle" : "";
            this.children =
                (chartOfAccountEntity.Children == null || chartOfAccountEntity.Type == ChartOfAccountEntity.ACCOUNT_TYPE) ?
                new List<ChartOfAccountViewModel>() :
                chartOfAccountEntity.Children.Select(x => new ChartOfAccountViewModel(x)).ToList();
        }
    }
}
