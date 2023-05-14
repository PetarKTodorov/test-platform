namespace TestPlatform.DTOs.ViewModels.Common
{
    using System.Collections.Generic;

    public class PageableResult<T>
    {
        private const int DEFAULT_PAGE_SIZE = 10;

        public PageableResult()
        {
            this.Results = new List<T>();

            this.CurrentPage = 1;
            this.BoundaryCount = 1;
            this.SiblingCount = 1;
            this.PageSize = DEFAULT_PAGE_SIZE;
        }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int PagesCount
        {
            get
            {
                var pagesCount = this.AllResultsCount / (double)this.PageSize;
                return (int)Math.Ceiling(pagesCount);
            }
        }

        public int BoundaryCount { get; set; }

        public int SiblingCount { get; set; }

        public int AllResultsCount { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}
