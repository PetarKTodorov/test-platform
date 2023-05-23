namespace TestPlatform.Application.Controllers
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Application.Infrastructures.Searcher;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.DTOs.ViewModels.Common;

    public class TestController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index(ICollection<AbstractSearch> searchCriteria, int page = 1)
        {
            var data = new Person[]
            {
                new Person() { isDeleted = true, Name = "Pesho", Age = 10, Date = new DateTime(2023, 1, 15), TestEnum = TestEnum.First, CollectionString = new List<string>() { "aaa", "bbbb", "ba" }, NestedClass = new SomeNestedClass() { NestedAge = 10, NestedText = "Test1" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 1, NestedText = "Collection1" }, new SomeNestedClass() { NestedAge = 2, NestedText = "Collection2" } } },
                new Person() { isDeleted = false, Name = "PeGosho", Age = 20, Date = new DateTime(2023, 2, 15), TestEnum = TestEnum.First, CollectionString = new List<string>() { "cccc", "dddd", "bd" }, NestedClass = new SomeNestedClass() { NestedAge = 20, NestedText = "Test2" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 3, NestedText = "Collection3" }, new SomeNestedClass() { NestedAge = 22, NestedText = "Collection22" } }  },
                new Person() { isDeleted = false, Name = "Ivan", Age = 30, Date = new DateTime(2023, 3, 15), TestEnum = TestEnum.Second, CollectionString = new List<string>() { "a", "basa", "bk" }, NestedClass = new SomeNestedClass() { NestedAge = 30, NestedText = "Test3" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 4, NestedText = "Collection4" }, new SomeNestedClass() { NestedAge = 23, NestedText = "Collection23" } }  },
                new Person() { isDeleted = false, Name = "Kiro", Age = 40, Date = new DateTime(2023, 4, 15), TestEnum = TestEnum.Third, CollectionString = new List<string>() { "gggg", "hh", "t" }, NestedClass = new SomeNestedClass() { NestedAge = 40, NestedText = "Test4" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 1, NestedText = "Collection111" }, new SomeNestedClass() { NestedAge = 24, NestedText = "Collection24" } }  },
            };

            if (searchCriteria == null || searchCriteria.Count == 0)
            {
                searchCriteria = typeof(Person).GetDefaultSearchCriteria()
                    .AddCustomSearchCriterion<Person>(p => p.NestedClass.NestedText)
                    .AddCustomSearchCriterion<Person>(s => s.CollectionSomeNestedClass.Select(c => c.NestedText));
            }

            var filteredData = data.AsQueryable().ApplySearchCriteria(searchCriteria).ToArray();

            var result = new PageableResult<Person>();
            result.Results = filteredData.Skip(1 * (page - 1)).Take(1);
            result.AllResultsCount = filteredData.Count();
            result.PageSize = 1;
            result.CurrentPage = page;

            var model = new SearchFilterVM<Person>()
            {
                Data = result,
                SearchCriteria = searchCriteria
            };

            return this.View(model);
        }
    }

    public class Person
    {
        public string Name { get; set; }

        [CustomSearchField]
        public int? Age { get; set; }

        public DateTime? Date { get; set; }

        [CustomSearchField]
        public bool isDeleted { get; set; }

        [CustomSearchField]
        public TestEnum TestEnum { get; set; }

        public SomeNestedClass NestedClass { get; set; }

        [CustomSearchField]
        public ICollection<string> CollectionString { get; set; }

        public ICollection<SomeNestedClass> CollectionSomeNestedClass { get; set; }
    }

    public class SomeNestedClass
    {
        public string NestedText { get; set; }

        public int NestedAge { get; set; }
    }

    public enum TestEnum
    {
        [Display(Name = "First entry")]
        First,

        [Display(Name = "Second entry")]
        Second,

        [Display(Name = "Third entry")]
        Third
    }
}
