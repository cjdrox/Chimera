using Chimera.Base;

namespace Chimera.Entities
{
    public class BirthPlace : BaseEntity, IEnumerated
    {
        public string Name { get; set; }
        public Region Region { get; set; }
    }
}