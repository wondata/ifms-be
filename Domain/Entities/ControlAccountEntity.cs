using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public partial class ControlAccountEntity
    {       

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? NoteNo { get; set; }
        public Guid? AccountGroupId { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ControlAccountEntity()
        {
        }

        public ControlAccountEntity(CoreControlAccount coreControl)
        {
            this.Id = coreControl.Id;
            this.Name = coreControl.Name;
            this.Code = coreControl.Code;
            this.NoteNo = coreControl.NoteNo;
            this.AccountGroupId = coreControl.AccountGroupId;
            this.CompanyId = coreControl.CompanyId;
            this.DateCreated = coreControl.DateCreated;
        }
    }
}
