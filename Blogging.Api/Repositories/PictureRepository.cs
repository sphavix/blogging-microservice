using Blogging.Api.Models.Domain;
using Blogging.Api.Persistance;
using Blogging.Api.Repositories.Contracts;

namespace Blogging.Api.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationDbContext _context;
        public PictureRepository(IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContext, ApplicationDbContext context)
        {
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment)); 
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Picture> UploadPicture(IFormFile file, Picture picture)
        {
            // Upload picture
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "images", $"{picture.FileName}{picture.FileExtension}");

            using(var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Save to database
            var httpRequest = _httpContext.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/images/{picture.FileName}{picture.FileExtension}";

            picture.ImageUrl = urlPath;

            await _context.Pictures.AddAsync(picture);
            await _context.SaveChangesAsync();

            return picture;

        }
    }
}
