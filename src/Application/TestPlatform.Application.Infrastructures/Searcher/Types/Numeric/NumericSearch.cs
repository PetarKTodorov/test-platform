namespace TestPlatform.Application.Infrastructures.Searcher.Types.Numeric
{
    using System;
    using System.Linq.Expressions;

    public class NumericSearch : AbstractSearch
    {
        private const string INVALID_COMPARABLE_OPTION_ERROR_MESSAGE = "Comparable option is not supported.";

        public decimal? SearchTerm { get; set; }

        public decimal? OtherSearchTerm { get; set; }

        public NumericComparableOptions ComparableOption { get; set; }

        protected override Expression BuildFilterExpression(Expression property)
        {
            Expression searchExpression = null;

            if (this.SearchTerm.HasValue || this.OtherSearchTerm.HasValue)
            {
                searchExpression = this.CreateFilterExpression(property);
            }

            return searchExpression;
        }

        private Expression CreateFilterExpression(Expression property)
        {
            Expression expression = null;

            try
            {
                var covertedSearchTerm = Convert.ChangeType(this.SearchTerm, property.Type);

                var constantExpression = Expression.Constant(covertedSearchTerm);

                switch (this.ComparableOption)
                {
                    case NumericComparableOptions.Less:
                        expression = Expression.LessThan(property, constantExpression);
                        break;
                    case NumericComparableOptions.LessOrEqual:
                        expression = Expression.LessThanOrEqual(property, constantExpression);
                        break;
                    case NumericComparableOptions.Equal:
                        expression = Expression.Equal(property, constantExpression);
                        break;
                    case NumericComparableOptions.GreaterOrEqual:
                        expression = Expression.GreaterThanOrEqual(property, constantExpression);
                        break;
                    case NumericComparableOptions.Greater:
                        expression = Expression.GreaterThan(property, constantExpression);
                        break;
                    case NumericComparableOptions.InRange:
                        Expression searchExpression1 = null;
                        Expression searchExpression2 = null;

                        if (this.SearchTerm.HasValue)
                        {
                            searchExpression1 = Expression.GreaterThanOrEqual(property, constantExpression);
                        }

                        if (this.OtherSearchTerm.HasValue)
                        {
                            var covertedSearchTerm2 = Convert.ChangeType(this.OtherSearchTerm, property.Type);
                            var constantExpression2 = Expression.Constant(covertedSearchTerm2);

                            searchExpression2 = Expression.LessThanOrEqual(property, constantExpression2);
                        }

                        if (searchExpression1 != null && searchExpression2 != null)
                        {
                            expression = Expression.AndAlso(searchExpression1, searchExpression2);
                        }
                        else if (searchExpression1 != null)
                        {
                            expression = searchExpression1;
                        }
                        else if (searchExpression2 != null)
                        {
                            expression = searchExpression2;
                        }
                        break;
                    default:
                        throw new InvalidOperationException(INVALID_COMPARABLE_OPTION_ERROR_MESSAGE);
                }
            }
            catch (Exception)
            {
            }

            return expression;
        }
    }
}
