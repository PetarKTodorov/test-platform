namespace TestPlatform.Database.Entities.Authorization
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;

    public class Role : BaseEntity
    {
        public Role()
        {
            this.Users = new HashSet<UserRoleMap>();
        }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Name { get; set; }

        public virtual ICollection<UserRoleMap> Users { get; set; }
    }
}
