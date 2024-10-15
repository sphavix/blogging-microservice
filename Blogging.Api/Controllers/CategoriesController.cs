using Blogging.Api.Models.Domain;
using Blogging.Api.Models.Dtos.Categories;
using Blogging.Api.Persistance;
using Blogging.Api.Repositories.Contracts;
using Blogging.Api.Utilities;
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


            //var response = categories.MapToResponse(); // Manual mapping method also works, but it is also still cumbersome because it
                                                         // needs us to have multiple Dtos for request/response. Clean code & S/C is still maintained.
                                                         // Mappings are under Utilities folder.

            // Map domain model to Dto
            var response = new List<CategoryDto>();
            foreach (var category in categories)
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

        // GET: https://localhost:7026/api/categories/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategory([FromRoute]Guid id)
        {
            var category = await _repository.GetCategoryAsync(id);
            if(category is null)
            {
                return NotFound();
            }

            // Map domain to Dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }


        // POST: https://localhost:7026/api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {

            //This can be also be done using the 3rd party package AutoMapper but we are just using the below method.
            //var category = request.MapToCategory();   // Manual mapping method also works, but it is also still cumbersome because it
                                                        // needs us to have multiple Dtos for request/response. Clean code & S/C is still maintained.
                                                        // Mappings are under Utilities folder.

            // Map Dto to domain model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await _repository.CreateCategoryAsync(category);


            //This can be also be done using the 3rd party package AutoMapper but we are just using the below method.
            //var response = category.MapToResponse();  // Manual mapping method also works, but it is also still cumbersome because it
                                                        // needs us to have multiple Dtos for request/response. Clean code & S/C is still maintained.
                                                        // Mappings are under Utilities folder.

            // Map domain model back to Dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);

        }

        // PUT: https://localhost:7026/api/categories/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]Guid id, UpdateCategoryRequestDto request)
        {
            // Map Dto to domain model
            //This can be also be done using the 3rd party package AutoMapper but we are just using the below method.
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            category = await _repository.UpdateCategoryAsync(category);

            if(category is null)
            {
                return NotFound();
            }

            // Map domain model back to Dto
            //This can be also be done using the 3rd party package AutoMapper but we are just using the below method.
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        // DELETE: https://localhost:7026/api/categories/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await _repository.DeleteCategoryAsync(id);
            if(category is null)
            {
                return NotFound();
            }

            // Map domain model to Dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return NoContent();
        }


    }
}
