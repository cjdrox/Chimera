namespace Chimera.Base
{
    public class UserRoles : IEnumerated
    {
        public int Id { get; set; }
        public SystemUser SystemUser { get; set; }
        public Role Role { get; set; }
    }
}