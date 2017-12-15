namespace Chimera.Base
{
    public class Role : BaseEntity, IEnumerated
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}