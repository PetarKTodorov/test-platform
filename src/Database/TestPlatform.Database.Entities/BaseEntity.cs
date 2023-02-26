namespace TestPlatform.Database.Entities
{
    using System;

    public class BaseEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            this.CreatedDate = DateTime.Now;
            this.IsDeleted = false;
        }

        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}