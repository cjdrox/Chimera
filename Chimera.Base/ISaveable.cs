using System;

namespace Chimera.Base
{
    public interface ISaveable
    {
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}