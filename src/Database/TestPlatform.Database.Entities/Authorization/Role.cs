namespace TestPlatform.Database.Entities.Authorization
{
    using System.Collections.Generic;

    public class Role : BaseEntity
    {
        public Role()
        {
            this.Users = new HashSet<UserRoleMap>();
        }

        public string Name { get; set; }

        public virtual ICollection<UserRoleMap> Users { get; set; }
    }
}
