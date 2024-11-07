namespace Blogging.Api.Models.Dtos.Picture
{
    public class PictureDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
