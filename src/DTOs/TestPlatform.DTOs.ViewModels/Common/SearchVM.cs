namespace TestPlatform.DTOs.ViewModels.Common
{
    using System.Collections.Generic;

    using TestPlatform.Application.Infrastructures.Searcher.Types;

    public class SearchFilterVM<T>
    {
        public IEnumerable<AbstractSearch> SearchCriteria { get; set; } = new List<AbstractSearch>();

        public PageableResult<T> Data { get; set; }
    }
}
