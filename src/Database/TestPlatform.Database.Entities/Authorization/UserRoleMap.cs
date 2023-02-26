namespace TestPlatform.Database.Entities.Authorization
{
    using System;

    public class UserRoleMap : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
