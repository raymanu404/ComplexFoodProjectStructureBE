using HelperLibrary.Classes;
using System;
using System.Linq.Expressions;
using Domain.Models.Enums;
using Domain.ValueObjects;
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
            filters = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
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
        var lambda = Expression.Lambda<Func<T, TKey>>(propertyAccess, parameter);

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
        else if (property.Type == typeof(Price))
        {
            var constant = Expression.Constant(new Price(Convert.ToDouble(value)));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(PhoneNumber))
        {
            var constant = Expression.Constant(new PhoneNumber(value));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(Password))
        {
            var constant = Expression.Constant(new Password(value));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(Name))
        {
            var constant = Expression.Constant(new Name(value));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(MatrNumber))
        {
            var constant = Expression.Constant(new MatrNumber(value));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(Gender))
        {
            var constant = Expression.Constant(new Gender(value));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(Email))
        {
            var constant = Expression.Constant(new Email(value));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(Discount))
        {
            var constant = Expression.Constant(new Discount(Convert.ToInt32(value)));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(Cantity))
        {
            var constant = Expression.Constant(new Cantity(Convert.ToInt32(value)));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(Balance))
        {
            var constant = Expression.Constant(new Balance(Convert.ToDouble(value)));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(AcademicYear))
        {
            var constant = Expression.Constant(new AcademicYear(Convert.ToInt32(value)));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(UniqueCode))
        {
            var constant = Expression.Constant(new UniqueCode(value));
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(OrderStatus))
        {
            var convertToEnum = Convert.ToInt32(value).ConvertToEnum(OrderStatus.Placed);
            var constant = Expression.Constant(convertToEnum);
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(AcademicDegree))
        {
            var convertToEnum = Convert.ToInt32(value).ConvertToEnum(AcademicDegree.Bachelor);
            var constant = Expression.Constant(convertToEnum);
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(Categories))
        {
            var convertToEnum = Convert.ToInt32(value).ConvertToEnum(Categories.Soup);
            var constant = Expression.Constant(convertToEnum);
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(RoleNameEnum))
        {
            var convertToEnum = Convert.ToInt32(value).ConvertToEnum(RoleNameEnum.User);
            var constant = Expression.Constant(convertToEnum);
            comparison = Expression.Equal(property, constant);
        }
        else if (property.Type == typeof(TypeCoupons))
        {
            var convertToEnum = Convert.ToInt32(value).ConvertToEnum(TypeCoupons.TenProcent);
            var constant = Expression.Constant(convertToEnum);
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