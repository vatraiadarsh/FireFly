using Category.API.Model;
using Utils;
using System.Linq.Dynamic.Core;

namespace Category.API.Repository.Extensions
{
    public static class RepositoryCategoryExtensions
    {
        public static IQueryable<CategoryItem> Search(this IQueryable<CategoryItem> categories, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return categories;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return categories.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<CategoryItem> Sort(this IQueryable<CategoryItem> categories, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return categories.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<CategoryItem>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return categories.OrderBy(e => e.Name);

            return categories.OrderBy(orderQuery);
        }


    }
}
