namespace TestPlatform.Database.Entities.Questions
{
    using System;

    using TestPlatform.Database.Entities.Tests;

    public class QuestionTestMap : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public virtual QuestionCopy Question { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }

        public int Points { get; set; }
    }
}
