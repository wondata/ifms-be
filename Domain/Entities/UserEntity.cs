using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserEntity
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public bool IsInactive { get; set; }

        public UserEntity()
        {

        }

        public UserEntity(CoreUser user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.UserName = user.UserName;
            this.value = user.Id.ToString();
            this.text = user.UserName;
            this.IsInactive = user.IsInactive;

        }



        public CoreUser MapToModel()
        {
            CoreUser coreUser = new CoreUser
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                UserName = this.UserName,
                IsInactive = this.IsInactive,
                EntryDate = DateTime.Now,
                UpdatedDate = DateTime.Now,

            };

            return coreUser;
        }

        public CoreUser MapToModel(CoreUser coreUser)
        {
            coreUser.FirstName = this.FirstName;
            coreUser.LastName = this.LastName;
            coreUser.UserName = this.UserName;
            coreUser.IsInactive = this.IsInactive;
            coreUser.UpdatedDate = DateTime.Now;

            return coreUser;
        }
    }
}
