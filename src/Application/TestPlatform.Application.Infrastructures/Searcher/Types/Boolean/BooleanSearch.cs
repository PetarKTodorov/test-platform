namespace TestPlatform.Application.Infrastructures.Searcher.Types.Boolean
{
    using System.Linq.Expressions;

    public class BooleanSearch : AbstractSearch
    {
        private const string INVALID_COMPARABLE_OPTION_ERROR_MESSAGE = "Comparable option is not supported.";

        public bool? SearchTerm { get; set; }

        public BooleanComparableOptions ComparableOption { get; set; }

        protected override Expression BuildFilterExpression(Expression property)
        {
            Expression searchExpression = null;

            if (this.SearchTerm.HasValue)
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
                case BooleanComparableOptions.Equal:
                    expression = Expression.Equal(property, constantExpression);
                    break;
                default:
                    throw new InvalidOperationException(INVALID_COMPARABLE_OPTION_ERROR_MESSAGE);
            }

            return expression;
        }
    }
}
