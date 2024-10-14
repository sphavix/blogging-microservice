using Blogging.Api.Models.Domain;

namespace Blogging.Api.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category?> GetCategoryAsync(Guid id);

        Task<Category?> UpdateCategoryAsync(Category category);
        Task<Category?> DeleteCategoryAsync(Guid id); 
    }
}
