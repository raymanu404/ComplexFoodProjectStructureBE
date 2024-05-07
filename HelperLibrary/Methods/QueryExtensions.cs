using System.Linq;
using System.Linq.Expressions;

public static class QueryExtensions
{
    public static IQueryable<T> CustomQuery<T>(this IQueryable<T> query, Expression<Func<T, bool>>? filter = null) where T : class
    {
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query;
    }

    public static IQueryable<T> CustomOrderBy<T, TKey>(this IQueryable<T> orderByQuery, Expression<Func<T, TKey>>? keySelector, bool asc) where T : class
    {
        if (keySelector != null)
        {
            orderByQuery = asc ? orderByQuery.OrderBy(keySelector) : orderByQuery.OrderByDescending(keySelector);
        }

        return orderByQuery;
    }

    public static IQueryable<T> CustomPagination<T>(this IQueryable<T> query, int? page = 0, int? pageSize = null)
    {
        if (page != null && page != 0)
        {
            query = query.Skip(((int)page - 1) * (int)pageSize);
        }

        if (pageSize != null && pageSize != 0)
        {
            query = query.Take((int)pageSize);
        }

        return query;
    }
}
