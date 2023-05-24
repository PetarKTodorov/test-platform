namespace TestPlatform.DTOs.ViewModels.Common
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class PageableResult<T> : IEnumerable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageableResult&lt;T&gt;"/> class.
        /// </summary>
        public PageableResult()
        {
            this.Items = new List<T>();
            this.Paging = new Paging();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageableResult&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="totalNumberOfItems">The total number of items.</param>
        /// <param name="paging">The paging.</param>
        public PageableResult(IEnumerable<T> items, Paging paging)
        {
            this.Items = items;
            this.Paging = paging;
        }

        public IEnumerable<T> Items { get; set; }

        public Paging Paging { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }
    }
}
