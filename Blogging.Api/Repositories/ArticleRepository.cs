using Blogging.Api.Models.Domain;
using Blogging.Api.Persistance;
using Blogging.Api.Repositories.Contracts;

namespace Blogging.Api.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;
        }
    }
}
