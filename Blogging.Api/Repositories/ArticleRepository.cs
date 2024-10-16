using Blogging.Api.Models.Domain;
using Blogging.Api.Persistance;
using Blogging.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Api.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await _context.Articles.Include(x => x.Categories).ToListAsync();
        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;
        }
    }
}
