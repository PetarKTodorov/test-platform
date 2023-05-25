namespace TestPlatform.Application.Infrastructures.Searcher.Types.Enums
{
    using System.Linq.Expressions;

    public class EnumSearch : AbstractSearch
    {
        private const string INVALID_COMPARABLE_OPTION_ERROR_MESSAGE = "Comparable option is not supported.";

        public string SearchTerm { get; set; }

        public EnumComparableOptions ComparableOption { get; set; }

        public Type EnumType => Type.GetType(this.EnumTypeName);

        public string EnumTypeName { get; set; }

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

            var enumValue = Enum.Parse(this.EnumType, this.SearchTerm);
            var constantExpression = Expression.Constant(enumValue);

            switch (this.ComparableOption)
            {
                case EnumComparableOptions.Equal:
                    expression = Expression.Equal(property, constantExpression);
                    break;
                default:
                    throw new InvalidOperationException(INVALID_COMPARABLE_OPTION_ERROR_MESSAGE);
            }

            return expression;
        }
    }
}
