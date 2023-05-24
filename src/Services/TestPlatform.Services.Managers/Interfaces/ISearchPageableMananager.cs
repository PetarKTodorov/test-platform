namespace TestPlatform.Services.Managers.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Common.Constants;
    using TestPlatform.DTOs.ViewModels.Common;

    public interface ISearchPageableMananager
    {
        PageableResult<T> CreatePageableResult<T>(IQueryable<T> dataQuery, int page, int pageSize = GlobalConstants.DEFAULT_PAGE_SIZE);

        SearchFilterVM<T> CreateSearchFilterModelWithPaging<T>(IQueryable<T> dataQuery, IEnumerable<AbstractSearch> searchCriteria, int page, int pageSize = GlobalConstants.DEFAULT_PAGE_SIZE);
    }
}
