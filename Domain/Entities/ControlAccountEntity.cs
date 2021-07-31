using Domain.Common;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ControlAccountEntity
    {       
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? NoteNo { get; set; }
        public Guid? AccountGroupId { get; set; }
        public DateTime? DateCreated { get; set; }   


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
            this.DateCreated = coreControl.DateCreated;
        }
       
    }
}
