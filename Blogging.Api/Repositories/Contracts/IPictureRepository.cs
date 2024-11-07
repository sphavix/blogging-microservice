using Blogging.Api.Models.Domain;

namespace Blogging.Api.Repositories.Contracts
{
    public interface IPictureRepository
    {
        Task<Picture> UploadPicture(IFormFile file, Picture picture);
    }
}
