using Blogging.Api.Models.Domain;
using Blogging.Api.Models.Dtos.Categories;

namespace Blogging.Api.Utilities
{
    public static class CategoryMapping
    {
        public static Category MapToCategory(this CreateCategoryRequestDto request)
        {
            return new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
        }

        public static CreateCategoryRequestDto MapToResponse(this Category category)
        {
            return new CreateCategoryRequestDto
            {
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
        }

        public static ReadCategoryRequestDto MapToResponse(this IEnumerable<Category> categories)
        {
            return new ReadCategoryRequestDto
            {
                Categories = categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                })
            };
        }
    }
}
