using System.Runtime.InteropServices;
using Chimera.Base;

namespace Chimera.Entities
{
    public class Subject : BaseEntity, IEnumerated
    {
        public int Id { get; set; }
        public string CodeName { get; set; }
        public BirthPlace BirthPlace { get; set; }
        public Nationality Nationality { get; set; }
        public BioProfile BioProfile { get; set; }
    }
}
