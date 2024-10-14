using Blogging.Api.Models.Domain;
using Blogging.Api.Models.Dtos.Article;
using Blogging.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _repository;

        public ArticlesController(IArticleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleRequestDto request)
        {
            // Map dto to domain model
            var article = new Article
            {
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                FeatureImageUrl = request.FeatureImageUrl,
                Author = request.Author
            };

            article = await _repository.CreateArticleAsync(article);


            // Map domain model bact to Dto
            var response = new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                UrlHandle = article.UrlHandle,
                ShortDescription = article.ShortDescription,
                Content = article.Content,
                IsVisible = article.IsVisible,
                PublishedDate = article.PublishedDate,
                FeatureImageUrl = article.FeatureImageUrl,
                Author = article.Author
            };

            return Ok(response);


        }
    }
}
