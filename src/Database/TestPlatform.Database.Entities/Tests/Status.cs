namespace TestPlatform.Database.Entities.Tests
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;

    public class Status : BaseEntity
    {
        public Status()
        {
            this.Tests = new HashSet<Test>();
        }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Name { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
