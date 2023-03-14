using Category.API.Model;
using Category.API.Parameters;
using Category.API.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Category.API.Repository
{
    public class CategoryRepositoryImpl : ICategoryRepository
    {
        private readonly CategoryDbContext _context;
        public CategoryRepositoryImpl(CategoryDbContext context)
        {
            _context = context;
        }

        public void CreateCategory(CategoryItem category)
        {
            _context.Categories.Add(category);
            _context.SaveChangesAsync();
        }

        public void DeleteCategory(CategoryItem category)
        {
            _context.Categories.Remove(category);
            _context.SaveChangesAsync();
        }

        public async Task<PagedList<CategoryItem>> GetAllCategoriesAsync(CategoryParameters categoryParameters)
        {

            var categories = await _context.Categories
                .Search(categoryParameters.SearchTerm)
                .Sort(categoryParameters.OrderBy)
                .ToListAsync();

            return PagedList<CategoryItem>.ToPagedList(categories, categoryParameters.PageNumber, categoryParameters.PageSize);

        }

        public async Task<CategoryItem> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public void UpdateCategory(CategoryItem category)
        {
            _context.Categories.Update(category);
            _context.SaveChangesAsync();
        }
    }
}