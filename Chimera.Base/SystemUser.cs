namespace Chimera.Base
{
    public class SystemUser : BaseEntity, IEnumerated
    {
        public int Id { get; set; }

        [Serialize(Length = 50)]
        public string Username { get; set; }
        public string Password { get; set; }

        [Serialize(Length = 10)]
        public string PUK { get; set; }
    }
}