using Chimera.Base;

namespace Chimera.Entities
{
    public class Region : BaseEntity, IEnumerated
    {
        public string Name { get; set; }
        public CountryTerritory Territory { get; set; }
    }
}