using Blogging.Api.Models.Domain;

namespace Blogging.Api.Repositories.Contracts
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticlesAsync();
        Task<Article?> GetArticleAsync(Guid id);
        Task<Article> CreateArticleAsync(Article article);
        Task<Article?> UpdateArticleAsync(Article article);
    }
}
