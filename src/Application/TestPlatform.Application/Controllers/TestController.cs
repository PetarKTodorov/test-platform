﻿namespace TestPlatform.Application.Controllers
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Application.Infrastructures.Searcher;

    public class TestController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var data = new Person[]
            {
                new Person() { Name = "Pesho", Age = 10, Date = new DateTime(2023, 1, 15), TestEnum = TestEnum.First, CollectionString = new List<string>() { "aaa", "bbbb", "ba" }, NestedClass = new SomeNestedClass() { NestedAge = 10, NestedText = "Test1" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 1, NestedText = "Collection1" }, new SomeNestedClass() { NestedAge = 2, NestedText = "Collection2" } } },
                new Person() { Name = "PeGosho", Age = 20, Date = new DateTime(2023, 2, 15), TestEnum = TestEnum.First, CollectionString = new List<string>() { "cccc", "dddd", "bd" }, NestedClass = new SomeNestedClass() { NestedAge = 20, NestedText = "Test2" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 3, NestedText = "Collection3" }, new SomeNestedClass() { NestedAge = 22, NestedText = "Collection22" } }  },
                new Person() { Name = "Ivan", Age = 30, Date = new DateTime(2023, 3, 15), TestEnum = TestEnum.Second, CollectionString = new List<string>() { "a", "basa", "bk" }, NestedClass = new SomeNestedClass() { NestedAge = 30, NestedText = "Test3" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 4, NestedText = "Collection4" }, new SomeNestedClass() { NestedAge = 23, NestedText = "Collection23" } }  },
                new Person() { Name = "Kiro", Age = 40, Date = new DateTime(2023, 4, 15), TestEnum = TestEnum.Third, CollectionString = new List<string>() { "gggg", "hh", "t" }, NestedClass = new SomeNestedClass() { NestedAge = 40, NestedText = "Test4" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 1, NestedText = "Collection111" }, new SomeNestedClass() { NestedAge = 24, NestedText = "Collection24" } }  },
            };

            var model = new SearchViewModel()
            {
                Data = data,
                SearchCriteria = typeof(Person).GetDefaultSearchCriteria()
                    .AddCustomSearchCriterion<Person>(p => p.NestedClass.NestedText)
                    .AddCustomSearchCriterion<Person>(s => s.CollectionSomeNestedClass.Select(c => c.NestedText))
            };

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Index(ICollection<AbstractSearch> searchCriteria)
        {
            var data = new Person[]
            {
                new Person() { Name = "Pesho", Age = 10, Date = new DateTime(2023, 1, 15), TestEnum = TestEnum.First, CollectionString = new List<string>() { "aaa", "bbbb", "ba" }, NestedClass = new SomeNestedClass() { NestedAge = 10, NestedText = "Test1" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 1, NestedText = "Collection1" }, new SomeNestedClass() { NestedAge = 2, NestedText = "Collection2" } } },
                new Person() { Name = "PeGosho", Age = 20, Date = new DateTime(2023, 2, 15), TestEnum = TestEnum.First, CollectionString = new List<string>() { "cccc", "dddd", "bd" }, NestedClass = new SomeNestedClass() { NestedAge = 20, NestedText = "Test2" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 3, NestedText = "Collection3" }, new SomeNestedClass() { NestedAge = 22, NestedText = "Collection22" } }  },
                new Person() { Name = "Ivan", Age = 30, Date = new DateTime(2023, 3, 15), TestEnum = TestEnum.Second, CollectionString = new List<string>() { "a", "basa", "bk" }, NestedClass = new SomeNestedClass() { NestedAge = 30, NestedText = "Test3" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 4, NestedText = "Collection4" }, new SomeNestedClass() { NestedAge = 23, NestedText = "Collection23" } }  },
                new Person() { Name = "Kiro", Age = 40, Date = new DateTime(2023, 4, 15), TestEnum = TestEnum.Third, CollectionString = new List<string>() { "gggg", "hh", "t" }, NestedClass = new SomeNestedClass() { NestedAge = 40, NestedText = "Test4" }, CollectionSomeNestedClass = new List<SomeNestedClass>() { new SomeNestedClass() { NestedAge = 1, NestedText = "Collection111" }, new SomeNestedClass() { NestedAge = 24, NestedText = "Collection24" } }  },
            };

            data = data.AsQueryable().ApplySearchCriteria(searchCriteria).ToArray();

            var model = new SearchViewModel()
            {
                Data = data,
                SearchCriteria = searchCriteria
            };

            return this.View(model);
        }
    }

    public class SearchViewModel
    {
        public IEnumerable<Person> Data { get; set; } = new List<Person>();

        public IEnumerable<AbstractSearch> SearchCriteria { get; set; } = new List<AbstractSearch>();
    }

    public class Person
    {
        public string Name { get; set; }

        public int? Age { get; set; }

        public DateTime? Date { get; set; }

        public TestEnum TestEnum { get; set; }

        public SomeNestedClass NestedClass { get; set; }

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
