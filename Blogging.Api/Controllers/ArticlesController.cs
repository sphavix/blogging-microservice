using Blogging.Api.Models.Domain;
using Blogging.Api.Models.Dtos.Article;
using Blogging.Api.Models.Dtos.Categories;
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
        private readonly ICategoryRepository _categoryRepository;

        public ArticlesController(IArticleRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        // GET: https://localhost:7026/api/articles
        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _repository.GetArticlesAsync();

            // Map domain model to Dto
            var response = new List<ArticleDto>();
            foreach (var article in articles)
            {
                response.Add(new ArticleDto
                {
                    Id = article.Id,
                    Title = article.Title,
                    UrlHandle = article.UrlHandle,
                    ShortDescription = article.ShortDescription,
                    Content = article.Content,
                    IsVisible = article.IsVisible,
                    PublishedDate = article.PublishedDate,
                    FeatureImageUrl = article.FeatureImageUrl,
                    Author = article.Author,
                    Categories = article.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }

        // GET: https://localhost:7026/api/articles/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetArticle([FromRoute] Guid id)
        {
            var article = await _repository.GetArticleAsync(id);

            if (article == null)
            {
                return NotFound();
            }

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
                Author = article.Author,
                Categories = article.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()

            };

            return Ok(response);
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
                Author = request.Author,
                Categories = new List<Category>()
            };

            foreach(var category in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetCategoryAsync(category);
                if(existingCategory is not null)
                {
                    article.Categories.Add(existingCategory);
                }
            }

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
                Author = article.Author,
                Categories = article.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // PUT: https://localhost:7026/api/articles/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] Guid id, UpdateArticleRequestDto request)
        {
            // Map from Dto to Domain model
            var article = new Article
            {
                Id = id,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                FeatureImageUrl = request.FeatureImageUrl,
                Author = request.Author,
                Categories = new List<Category>()
            };

            // Loop through the categories in the Db
            foreach(var category in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetCategoryAsync(category);

                if(existingCategory is not null)
                {
                    article.Categories.Add(existingCategory);
                }
            }

            // update the article
            var updatedArticle = await _repository.UpdateArticleAsync(article);

            if (updatedArticle is null)
            {
                return NotFound();
            }

            // Map domain model to Dto
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
                Author = article.Author,
                Categories = article.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // DELETE: https://localhost:7026/api/articles/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
            var removedArticle = await _repository.DeleteArticleAsync(id);

            if (removedArticle is null)
            {
                return NotFound();
            }

            // Map domain model to Dto for response
            //var response = new ArticleDto
            //{
            //    Id = removedArticle.Id,
            //    Title = removedArticle.Title,
            //    UrlHandle = removedArticle.UrlHandle,
            //    ShortDescription = removedArticle.ShortDescription,
            //    Content = removedArticle.Content,
            //    IsVisible = removedArticle.IsVisible,
            //    PublishedDate = removedArticle.PublishedDate,
            //    FeatureImageUrl = removedArticle.FeatureImageUrl,
            //    Author = removedArticle.Author
            //};

            return NoContent();
        }
    }
}
