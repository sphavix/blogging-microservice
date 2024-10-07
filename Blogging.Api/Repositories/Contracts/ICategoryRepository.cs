using Blogging.Api.Models.Domain;

namespace Blogging.Api.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);
    }
}
