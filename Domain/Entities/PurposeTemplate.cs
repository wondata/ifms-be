using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PurposeTemplate 
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Purpose { get; set; }
        public bool IsDeleted { get; set; }

        public PurposeTemplate()
        {
        }

        public PurposeTemplate(IfmsPurposeTemplate ifmsPurpose)
        {
            this.Id = ifmsPurpose.Id;
            this.Code = ifmsPurpose.Code;
            this.Purpose = ifmsPurpose.Code;
            this.IsDeleted = ifmsPurpose.IsDeleted;
        }
    }
}
