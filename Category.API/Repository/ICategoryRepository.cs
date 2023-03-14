using Category.API.Model;
using Category.API.Parameters;
using Utils;

namespace Category.API.Repository
{
    public interface ICategoryRepository
    {
        Task<PagedList<CategoryItem>> GetAllCategoriesAsync(CategoryParameters categoryParameters);
        Task<CategoryItem> GetCategoryByIdAsync(Guid categoryId);
        void CreateCategory(CategoryItem category);
        void UpdateCategory(CategoryItem category);
        void DeleteCategory(CategoryItem category);
    }
}