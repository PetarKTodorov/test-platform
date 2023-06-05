namespace TestPlatform.DTOs.ViewModels.Common
{
    using System;
    using TestPlatform.Common.Constants;

    public class Paging
    {
        private const int DEFAULT_PAGE = 1;
        private int currentPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="Paging"/> class.
        /// </summary>
        public Paging()
            : this(DEFAULT_PAGE, GlobalConstants.DEFAULT_PAGE_SIZE, default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Paging"/> class.
        /// </summary>
        /// <param name="currentPage">Current page.</param>
        /// <param name="totalNumberOfItems">The number of all items.</param>
        public Paging(int currentPage, int totalNumberOfItems)
            : this(currentPage, GlobalConstants.DEFAULT_PAGE_SIZE, totalNumberOfItems)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Paging"/> class.
        /// </summary>
        /// <param name="currentPage">Current page.</param>
        /// <param name="pageSize">The number of elements to show on page.</param>
        /// <param name="totalNumberOfItems">The number of all items.</param>
        public Paging(int currentPage, int pageSize, int totalNumberOfItems)
        {
            this.SiblingCount = 1;
            this.BoundaryCount = 1;

            this.PageSize = pageSize;
            this.TotalNumberOfItems = totalNumberOfItems;
            this.CurrentPage = currentPage;
        }

        public int CurrentPage
        {
            get => this.currentPage;
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                else if (value > this.CountOfPages && this.CountOfPages != 0)
                {
                    value = this.CountOfPages;
                }

                this.currentPage = value;
            }
        }

        public int PageSize { get; set; }

        public int CountOfPages
        {
            get
            {
                var pagesCount = this.TotalNumberOfItems / (double)this.PageSize;
                return (int)Math.Ceiling(pagesCount);
            }
        }

        public int BoundaryCount { get; set; }

        public int SiblingCount { get; set; }

        public int TotalNumberOfItems { get; set; }

        public int SkipCount => this.PageSize * (this.CurrentPage - 1);
    }
}
