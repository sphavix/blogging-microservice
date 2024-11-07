namespace Blogging.Api.Models.Domain
{
    public class Picture
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
