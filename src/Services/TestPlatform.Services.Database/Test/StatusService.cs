namespace TestPlatform.Services.Database.Test
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Mapper;

    public class StatusService : BaseService<Status>, IStatusService
    {
        public StatusService(IBaseRepository<Status> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
        }

        public async Task<T> FindByNameAsync<T>(string name)
        {
            var status = await this.FindAllAsQueryable<Status>()
               .Where(s => s.Name == name)
               .To<T>()
               .FirstOrDefaultAsync();

            return status;
        }
    }
}
