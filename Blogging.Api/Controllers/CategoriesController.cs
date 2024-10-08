using Blogging.Api.Models.Domain;
using Blogging.Api.Models.Dtos.Categories;
using Blogging.Api.Persistance;
using Blogging.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET: https://localhost:7026/api/categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repository.GetCategoriesAsync();

            // Map domain model to Dto
            var response = new List<CategoryDto>();
            foreach(var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }
            return Ok(response);
        }


        // POST: https://localhost:7026/api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Map Dto to domain model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await _repository.CreateCategoryAsync(category);

            // Map domain model back to Dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            // The above code is quite a lengthy method which can be refactored by using 3rd party packages (AutoMapper)
            return Ok(response);

        }
    }
}
