using System;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public byte[] LastUpdated { get; set; }

        public abstract T MapToModel<T>() where T : class;
        public abstract T MapToModel<T>(T t) where T : class;
        public abstract T MapToModel<T>(String Type) where T : class;
        public abstract T MapToModel<T>(T t, String tyoe) where T : class;
    }
}
