using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PurposeTemplateEntity 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Purpose { get; set; }
        public string value { get; set; }
        public string text { get; set; }
        public bool IsDeleted { get; set; }

        public PurposeTemplateEntity()
        {
        }

        public PurposeTemplateEntity(IfmsPurposeTemplate ifmsPurpose)
        {
            this.Id = ifmsPurpose.Id;
            this.Name = ifmsPurpose.Purpose;
            this.Code = ifmsPurpose.Code;
            this.Purpose = ifmsPurpose.Code;
            this.value = ifmsPurpose.Id.ToString();
            this.text = ifmsPurpose.Purpose;
            this.IsDeleted = ifmsPurpose.IsDeleted;
        }
    }
}
