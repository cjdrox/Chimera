namespace Chimera.Base
{
    public class UserRoles : IEnumerated
    {
        public SystemUser SystemUser { get; set; }
        public Role Role { get; set; }
        public long Id { get; set; }
    }
}