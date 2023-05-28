namespace TestPlatform.Database.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Repositories.Interfaces;

    public class QuestionAnswerMapRepository : BaseRepository<QuestionAnswerMap>, IQuestionAnswerMapRepository
    {
        public QuestionAnswerMapRepository(TestPlatformDbContext dbContext)
            : base(dbContext)
        {
        }

        public override void DetachLocal(QuestionAnswerMap entity)
        {
            var local = this.DbSet
                .Local
                .FirstOrDefault(entry => entry.AnswerId.Equals(entry.AnswerId));
            if (local != null)
            {
                this.DbContext.Entry(local).State = EntityState.Detached;
            }
            this.DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
