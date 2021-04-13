using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public class LookupEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid value { get; set; }
        public string text { get; set; }

        public LookupEntity()
        {

        }

        public LookupEntity(LookupModel lookup)
        {
            if (lookup == null) return;

            this.Id = lookup.Id;
            this.Name = lookup.Name;
            this.Code = lookup.Code;
            this.value = lookup.Id;
            this.text = lookup.Name;
        }


        public static LookupEntity Map<T>(T t)
        {
            LookupEntity lookup = new LookupEntity();
            try
            {
                lookup.Id = (Guid)t.GetType().GetProperty("Id").GetValue(t,null);
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
