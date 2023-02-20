using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRent.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Description { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsDeleted { get; set; }
        public string? Img { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
