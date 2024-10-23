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

        public async Task<Article?> GetArticleAsync(Guid id)
        {
            var article = await _context.Articles.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
            return article;
        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<Article?> UpdateArticleAsync(Article article)
        {
            var existingArticle = await _context.Articles.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == article.Id);

            if(existingArticle is null)
            {
                return null;
            }

            // Update articles
            _context.Entry(existingArticle).CurrentValues.SetValues(article);

            // Update categories for the article
            existingArticle.Categories = article.Categories;

            await _context.SaveChangesAsync();

            return article;
        }
    }
}
