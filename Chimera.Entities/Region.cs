using Chimera.Base;

namespace Chimera.Entities
{
    public class Region : BaseEntity, IEnumerated
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryTerritory Territory { get; set; }
    }
}