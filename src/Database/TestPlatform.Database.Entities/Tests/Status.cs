namespace TestPlatform.Database.Entities.Tests
{
    public class Status : BaseEntity
    {
        public Status()
        {
            this.Tests = new HashSet<Test>();
        }

        public string Name { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
