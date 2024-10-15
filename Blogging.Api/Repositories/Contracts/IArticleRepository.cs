using Blogging.Api.Models.Domain;

namespace Blogging.Api.Repositories.Contracts
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticlesAsync();
        //Task<Article> GetArticleAsync(int id);
        Task<Article> CreateArticleAsync(Article article);
    }
}
