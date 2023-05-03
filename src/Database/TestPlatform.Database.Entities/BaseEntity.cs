namespace TestPlatform.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            this.CreatedDate = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
