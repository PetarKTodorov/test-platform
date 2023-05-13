namespace TestPlatform.DTOs.ViewModels.Common
{
    using System.Collections.Generic;
    using TestPlatform.Common.Constants;

    public class PageableResult<T>
    {
        public PageableResult()
        {
            this.Results = new List<T>();

            this.CurrentPage = Validations.ONE;
            this.BoundaryCount = Validations.ONE;
            this.SiblingCount = Validations.ONE;
            this.PageSize = Validations.DEFAULT_PAGE_SIZE;
        }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int PagesCount
        {
            get
            {
                var pagesCount = this.Results.Count() / (double)this.PageSize;
                return (int)Math.Ceiling(pagesCount);
            }
        }

        public int BoundaryCount { get; set; }

        public int SiblingCount { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}
