using System;

namespace Chimera.Base
{
    public class SerializeAttribute : Attribute
    {
        public long Length { get; set; }
    }

    public class NoSerializeAttribute : Attribute
    {
    }
}