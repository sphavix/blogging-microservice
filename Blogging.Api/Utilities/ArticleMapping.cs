using Blogging.Api.Models.Domain;
using Blogging.Api.Models.Dtos.Article;

namespace Blogging.Api.Utilities
{
    public static class ArticleMapping
    {
        public static Article MapToArticle(this ArticleDto article)
        {
            return new Article
            {
                Id = article.Id,
                Title = article.Title,
                ShortDescription = article.ShortDescription,
                UrlHandle = article.UrlHandle,
                Content = article.Content,
                FeatureImageUrl = article.FeatureImageUrl,
                PublishedDate = article.PublishedDate,
                IsVisible = article.IsVisible,
            };
        }

        public static ArticleDto MapToArticleDto(this Article article)
        {
            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                ShortDescription = article.ShortDescription,
                UrlHandle = article.UrlHandle,
                Content = article.Content,
                FeatureImageUrl = article.FeatureImageUrl,
                PublishedDate = article.PublishedDate,
                IsVisible = article.IsVisible,
            };
        }
    }
}
