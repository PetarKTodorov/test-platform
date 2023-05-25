namespace TestPlatform.Services.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using TestPlatform.Application.Infrastructures.Searcher;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Common.Constants;
    using TestPlatform.DTOs.ViewModels.Common;
    using TestPlatform.Services.Managers.Interfaces;

    public class SearchPageableMananager : ISearchPageableMananager
    {
        public PageableResult<T> CreatePageableResult<T>(IQueryable<T> dataQuery, int page, int pageSize = GlobalConstants.DEFAULT_PAGE_SIZE)
        {
            var dataCount = dataQuery.Count();
            var paging = new Paging(page, pageSize, dataCount);

            var pagedData = dataQuery
                .Skip(paging.SkipCount)
                .Take(paging.PageSize)
                .ToArray();

            var pageableData = new PageableResult<T>(pagedData, paging);

            return pageableData;
        }

        public SearchFilterVM<T> CreateSearchFilterModelWithPaging<T>(IQueryable<T> dataQuery, IEnumerable<AbstractSearch> searchCriteria, int page, int pageSize = GlobalConstants.DEFAULT_PAGE_SIZE)
        {
            if (searchCriteria == null || searchCriteria.Count() == 0)
            {
                searchCriteria = typeof(T).GetDefaultSearchCriteria();
            }

            var filteredDataQuery = dataQuery
                .ApplySearchCriteria(searchCriteria);

            var pageableData = this.CreatePageableResult<T>(filteredDataQuery, page, pageSize);

            var model = new SearchFilterVM<T>()
            {
                Data = pageableData,
                SearchCriteria = searchCriteria
            };

            return model;
        }
    }
}
