using Blogging.Api.Models.Domain;

namespace Blogging.Api.Repositories.Contracts
{
    public interface IPictureRepository
    {
        Task<IEnumerable<Picture>> GetPictures();
        Task<Picture> UploadPicture(IFormFile file, Picture picture);
    }
}
