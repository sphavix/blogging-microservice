using Blogging.Api.Models.Domain;
using Blogging.Api.Models.Dtos.Picture;
using Blogging.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IPictureRepository _repository;

        public PicturesController(IPictureRepository repository)
        {
            _repository = repository;
        }

        // GET: localhost:{PORT}/api/pictures
        [HttpGet]
        public async Task<IActionResult> GetPictures()
        {
            var pictures = await _repository.GetPictures();

            // Map domain to Dto
            var response = new List<PictureDto>();
            foreach(var picture in pictures)
            {
                response.Add(new PictureDto
                {
                    Id = picture.Id,
                    Title = picture.Title,
                    FileExtension = picture.FileExtension,
                    FileName = picture.FileName,
                    DateCreated = picture.DateCreated,
                    ImageUrl = picture.ImageUrl
                });
            }

            return Ok(response);
        }

        // POST: localhost:{PORT}/api/pictures
        [HttpPost]
        public async Task<IActionResult> UploadPicture([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);

            if(ModelState.IsValid)
            {
                // Upload picture
                var picture = new Picture
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,

                };

                var blogPicture = await _repository.UploadPicture(file, picture);

                // Map domain model to Dto
                var response = new PictureDto
                {
                    Id = blogPicture.Id,
                    Title = blogPicture.Title,
                    FileExtension = blogPicture.FileExtension,
                    FileName = blogPicture.FileName,
                    DateCreated = blogPicture.DateCreated,
                    ImageUrl = blogPicture.ImageUrl
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }


        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Invalid file format! Please choose the valid file");
            }

            if(file.Length > 10485760)
            {
                ModelState.AddModelError("file", "Please choose a file less than 10MB");
            }
        }
    }
}
