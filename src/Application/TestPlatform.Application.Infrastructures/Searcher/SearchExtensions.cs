namespace TestPlatform.Application.Infrastructures.Searcher
{
    using System.Linq.Expressions;
    using System.Reflection;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Application.Infrastructures.Searcher.Types;
    using TestPlatform.Application.Infrastructures.Searcher.Types.Boolean;
    using TestPlatform.Application.Infrastructures.Searcher.Types.DateTime;
    using TestPlatform.Application.Infrastructures.Searcher.Types.Enums;
    using TestPlatform.Application.Infrastructures.Searcher.Types.Numeric;
    using TestPlatform.Application.Infrastructures.Searcher.Types.Text;
    using TestPlatform.Common.Extensions;

    public static class SearchExtensions
    {
        public static IQueryable<T> ApplySearchCriteria<T>(this IQueryable<T> query, IEnumerable<AbstractSearch> searchCriterias)
        {
            foreach (var criteria in searchCriterias)
            {
                query = criteria.ApplyToQuery(query);
            }

            return query;
        }

        public static ICollection<AbstractSearch> GetDefaultSearchCriteria(this Type type)
        {
            var properties = type.GetProperties()
                .Where(p => p.CanRead
                    && p.CanWrite
                    && p.GetCustomAttributes<CustomSearchFieldAttribute>().Any())
                .OrderBy(p => p.PropertyType.IsCollectionType())
                .ThenBy(p => p.Name);

            var searchCriterias = properties
                .Select(p => CreateSearchCriterion(type, p.PropertyType, p.Name))
                .Where(s => s != null)
                .ToList();

            return searchCriterias;
        }

        public static ICollection<AbstractSearch> AddCustomSearchCriterion<T>(this ICollection<AbstractSearch> searchCriterias, Expression<Func<T, object>> property)
        {
            Type propertyType = null;
            string fullPropertyPath = GetPropertyPath(property, out propertyType);

            AbstractSearch searchCriteria = CreateSearchCriterion(typeof(T), propertyType, fullPropertyPath);

            if (searchCriteria != null)
            {
                searchCriterias.Add(searchCriteria);
            }

            return searchCriterias;
        }

        private static AbstractSearch CreateSearchCriterion(Type classType, Type propertyType, string propertyName)
        {
            AbstractSearch result = null;

            if (propertyType.IsCollectionType())
            {
                propertyType = propertyType.GetGenericArguments().First();
            }

            if (propertyType.Equals(typeof(string)))
            {
                result = new TextSearch();
            }
            else if (IsNumericType(propertyType))
            {
                result = new NumericSearch();
            }
            else if (propertyType.Equals(typeof(DateTime)) || propertyType.Equals(typeof(DateTime?)))
            {
                result = new DateTimeSearch();
            }
            else if (propertyType.IsEnum)
            {
                result = new EnumSearch();
                var enumSearch = (EnumSearch)result;
                enumSearch.EnumTypeName = propertyType.AssemblyQualifiedName;
            }
            else if (propertyType.Equals(typeof(bool)))
            {
                result = new BooleanSearch();
            }

            if (result != null)
            {
                result.Property = propertyName;
                result.TargetTypeName = classType.AssemblyQualifiedName;
            }

            return result;
        }

        private static string GetPropertyPath<T>(Expression<Func<T, object>> expression, out Type targetType)
        {
            MethodCallExpression methodCallExpression = expression.Body as MethodCallExpression;

            if (methodCallExpression != null)
            {
                if (methodCallExpression.Arguments.Count == 2)
                {
                    MemberExpression memberExpression1 = methodCallExpression.Arguments[0] as MemberExpression;
                    LambdaExpression lambdaExpression = methodCallExpression.Arguments[1] as LambdaExpression;

                    if (memberExpression1 != null && lambdaExpression != null)
                    {
                        MemberExpression memberExpression2 = lambdaExpression.Body as MemberExpression;

                        if (memberExpression2 != null)
                        {
                            targetType = memberExpression2.Type;

                            string propertyPath = string.Format("{0}.{1}",
                                GetPropertyPath(memberExpression1),
                                GetPropertyPath(memberExpression2));

                            return propertyPath;
                        }
                    }
                }

                throw new ArgumentException("Please provide a lambda expression like 'n => n.Collection.Select(c => c.PropertyName)'", "expression");
            }
            else
            {
                UnaryExpression unaryExpression = expression.Body as UnaryExpression;
                MemberExpression memberExpression = null;

                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
                else
                {
                    memberExpression = expression.Body as MemberExpression;
                }

                if (memberExpression != null)
                {
                    targetType = memberExpression.Type;
                    string propertyPath = GetPropertyPath(memberExpression);

                    return propertyPath;
                }

                throw new ArgumentException("Please provide a lambda expression like 'n => n.PropertyName'", "expression");
            }
        }

        private static string GetPropertyPath(MemberExpression memberExpression)
        {
            string property = memberExpression.ToString();
            string propertyPath = property.Substring(property.IndexOf('.') + 1);

            return propertyPath;
        }

        private static bool IsNumericType(Type propertyType)
        {
            bool isNumericType = propertyType.Equals(typeof(int)) || propertyType.Equals(typeof(int?))
                || propertyType.Equals(typeof(float)) || propertyType.Equals(typeof(float?))
                || propertyType.Equals(typeof(double)) || propertyType.Equals(typeof(double?))
                || propertyType.Equals(typeof(decimal)) || propertyType.Equals(typeof(decimal?));

            return isNumericType;
        }
    }
}
