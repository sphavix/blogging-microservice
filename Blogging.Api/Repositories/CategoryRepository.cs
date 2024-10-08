using Blogging.Api.Models.Domain;
using Blogging.Api.Persistance;
using Blogging.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
