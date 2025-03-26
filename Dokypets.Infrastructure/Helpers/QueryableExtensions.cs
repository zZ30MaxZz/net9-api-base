using Dokypets.Domain.Entities.Pagination;

namespace Dokypets.Infrastructure.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Pagination<T>(this IQueryable<T> queryable, Pagination pagination)
        {
            return queryable.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);
        }
    }
}
