namespace TestPlatform.DTOs.ViewModels.Common
{
    using System.Collections.Generic;

    using TestPlatform.Application.Infrastructures.Searcher.Types;

    public class SearchFilterVM<T>
    {
        public SearchFilterVM()
        {
            this.SearchCriteria = new List<AbstractSearch>();
        }

        public IEnumerable<AbstractSearch> SearchCriteria { get; set; }

        public PageableResult<T> Data { get; set; }
    }
}
