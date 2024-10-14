using Blogging.Api.Models.Domain;

namespace Blogging.Api.Repositories.Contracts
{
    public interface IArticleRepository
    {
        Task<Article> CreateArticleAsync(Article article);
    }
}
