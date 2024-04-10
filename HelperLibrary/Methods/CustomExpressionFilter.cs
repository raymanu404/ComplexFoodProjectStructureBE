using HelperLibrary.Classes;
using System;
using System.Linq.Expressions;
using HelperLibrary.Methods;

public static class CustomExpressionFilter<T> where T : class
{
    public class ExpressionFilter
    {
        public string ColumnName { get; set; }
        public string Value { get; set; }
    }
   
    public static Expression<Func<T, bool>> CustomFilter(List<ColumnFilter> columnFilters, string className)
    {
        Expression<Func<T, bool>> filters = null;
        try
        {
            var expressionFilters = new List<ExpressionFilter>();
            foreach (var item in columnFilters)
            {
                expressionFilters.Add(new ExpressionFilter() { ColumnName = item.Id, Value = item.Value });
            }
            // Create the parameter expression for the input data
            var parameter = Expression.Parameter(typeof(T), className);

            // Build the filter expression dynamically
            Expression filterExpression = null;
            foreach (var filter in expressionFilters)
            {
                var property = Expression.Property(parameter, filter.ColumnName);
                var comparison = GetComparision(property, filter.Value);

                filterExpression = filterExpression == null
                    ? comparison
                    : Expression.And(filterExpression, comparison);
            }

            // Create the lambda expression with the parameter and the filter expression
            filters =  Expression.Lambda<Func<T, bool>>(filterExpression, parameter);

        }
        catch (Exception)
        {
            filters = null;
        }
        return filters;
    }

    public static Expression<Func<T, TKey>> CreateOrderByFunc<T, TKey>(string orderBy, string className)
    {
        // Define parameter expression
        var parameter = Expression.Parameter(typeof(T), className);

        // Get property by name
        var property = typeof(T).GetProperty(orderBy.ToUpperFirstChar());

        if (property == null)
        {
            throw new ArgumentException($"Property '{orderBy}' not found in type {typeof(T).FullName}");
        }
        // Create property access expression
        var propertyAccess = Expression.Property(parameter, property);

        // Create lambda expression
        var lambda = Expression.Lambda<Func<T, TKey >>(propertyAccess, parameter);

        // Compile lambda expression to create the Func<T, TKey>
        return lambda;
    }

    private static Expression GetComparision(MemberExpression property, string value)
    {
        Expression comparison;
        if (property.Type == typeof(string))
        {
            var constant = Expression.Constant(value);
            comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
        }
        else if (property.Type == typeof(double))
        {
            var constant = Expression.Constant(Convert.ToDouble(value));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(Guid))
        {
            var constant = Expression.Constant(Guid.Parse(value));
            comparison = Expression.Equal(property, constant);
        }
        else
        {
            var constant = Expression.Constant(Convert.ToInt32(value));
            comparison = Expression.Equal(property, constant);
        }

        return comparison;
    }

}
