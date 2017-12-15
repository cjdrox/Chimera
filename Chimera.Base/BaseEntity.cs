using System;

namespace Chimera.Base
{
    public abstract class BaseEntity : ISaveable, IEnumerated
    {
        public virtual long Id { get; set; }
        public Guid ObjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        protected BaseEntity()
        {
            ObjectId = Guid.NewGuid();
        }
    }
}
