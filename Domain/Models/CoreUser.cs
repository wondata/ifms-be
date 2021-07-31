using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class CoreUser
    {
        public CoreUser()
        {
            IfmsCashiers = new HashSet<IfmsCashier>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsInactive { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }

        public virtual ICollection<IfmsCashier> IfmsCashiers { get; set; }
    }
}
