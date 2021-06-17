using System;

namespace Domain.Models
{
    public partial class LookupModel
    {
        public LookupModel()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] LastUpdated { get; set; }
      //  public DateTime CreatedAt { get; set; }
      //  public DateTime UpdatedAt { get; set; }

    }
}
