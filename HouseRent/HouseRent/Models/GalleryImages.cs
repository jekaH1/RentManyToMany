using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRent.Models
{
    public class GalleryImages
    {
        public int Id { get; set; }
        public string? Img { get; set; }
        public string Href { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        
    }
}
