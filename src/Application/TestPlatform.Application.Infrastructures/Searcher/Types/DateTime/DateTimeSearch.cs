namespace TestPlatform.Application.Infrastructures.Searcher.Types.DateTime
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    public class DateTimeSearch : AbstractSearch
    {
        private const string INVALID_COMPARABLE_OPTION_ERROR_MESSAGE = "Comparable option is not supported.";

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? SearchTerm { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? OtherSearchTerm { get; set; }

        public DateTimeComparableOptions ComparableOption { get; set; }

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
            var constantExpression = Expression.Constant(this.SearchTerm);

            switch (this.ComparableOption)
            {
                case DateTimeComparableOptions.Less:
                    expression = Expression.LessThan(property, constantExpression);
                    break;
                case DateTimeComparableOptions.LessOrEqual:
                    expression = Expression.LessThanOrEqual(property, constantExpression);
                    break;
                case DateTimeComparableOptions.Equal:
                    expression = Expression.Equal(property, constantExpression);
                    break;
                case DateTimeComparableOptions.GreaterOrEqual:
                    expression = Expression.GreaterThanOrEqual(property, constantExpression);
                    break;
                case DateTimeComparableOptions.Greater:
                    expression = Expression.GreaterThan(property, constantExpression);
                    break;
                case DateTimeComparableOptions.InRange:
                    Expression searchExpression1 = null;
                    Expression searchExpression2 = null;

                    if (this.SearchTerm.HasValue)
                    {
                        searchExpression1 = Expression.GreaterThanOrEqual(property, constantExpression);
                    }

                    if (this.OtherSearchTerm.HasValue)
                    {
                        var constantExpression2 = Expression.Constant(this.OtherSearchTerm);
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

            return expression;
        }
    }
}
