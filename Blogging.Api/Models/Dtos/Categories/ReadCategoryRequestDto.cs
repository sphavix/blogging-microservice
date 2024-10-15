namespace Blogging.Api.Models.Dtos.Categories
{
    public class ReadCategoryRequestDto
    {
        public  IEnumerable<CategoryDto> Categories { get; init; } = Enumerable.Empty<CategoryDto>();
    }
}
