using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class LookupEntity
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid value { get; set; }
        public string text { get; set; }
        public bool IsDeleted { get; set; }

        public LookupEntity()
        {
        }

        public LookupEntity(LookupModel lookupModel)
        {
            if (lookupModel == null) return;

            this.Id = lookupModel.Id;
            this.Name = lookupModel.Name;
            this.Code = lookupModel.Code;
            this.value = lookupModel.Id;
            this.text = lookupModel.Name;
            this.IsDeleted = lookupModel.IsDeleted;

        }

        public static LookupEntity Map<T>(T t)
        {
            LookupEntity lookup = new LookupEntity();
            try
            {
                lookup.Id = (Guid)t.GetType().GetProperty("Id").GetValue(t, null);
                lookup.Name = (string)t.GetType().GetProperty("Name").GetValue(t, null);
                lookup.Code = (string)t.GetType().GetProperty("Code").GetValue(t, null);

            }
            catch (Exception)
            {
            }

            return lookup;
        }
    }
}
