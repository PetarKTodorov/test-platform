namespace TestPlatform.Application.Infrastructures.Searcher.Types.Text
{
    using System;
    using System.Linq.Expressions;

    public class TextSearch : AbstractSearch
    {
        private const string INVALID_COMPARABLE_OPTION_ERROR_MESSAGE = "Comparable option is not supported.";

        public string SearchTerm { get; set; }

        public TextComparableOptions ComparableOption { get; set; }

        protected override Expression BuildFilterExpression(Expression property)
        {
            Expression searchExpression = null;

            if (this.SearchTerm != null)
            {
                searchExpression = this.CreateFilterExpression(property);
            }

            return searchExpression;
        }

        private Expression CreateFilterExpression(Expression property)
        {
            Expression expression = null;
            var constantExpression = Expression.Constant(this.SearchTerm);

            switch (this.ComparableOption)
            {
                case TextComparableOptions.Contains:
                    var methodInfo = typeof(string).GetMethod(this.ComparableOption.ToString(), new[] { typeof(string) });

                    expression = Expression.Call(property, methodInfo, constantExpression);
                    break;
                case TextComparableOptions.Equal:
                    expression = Expression.Equal(property, constantExpression);
                    break;
                default:
                    throw new InvalidOperationException(INVALID_COMPARABLE_OPTION_ERROR_MESSAGE);
            }

            return expression;
        }
    }
}
