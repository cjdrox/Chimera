namespace Chimera.Base
{
    public class AccountProfile : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public SystemUser SystemUser { get; set; }
    }
}
