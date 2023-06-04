namespace TestPlatform.Services.Database.Comments
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Comments;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Comments.Interfaces;

    public class TestCommentService : BaseService<TestComment>, ITestCommentService
    {
        public TestCommentService(IBaseRepository<TestComment> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {
        }
    }
}
