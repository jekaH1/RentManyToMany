using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRent.Models
{
    public class SettingsRH
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string Value { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? Img { get; set; } 
    }
}
