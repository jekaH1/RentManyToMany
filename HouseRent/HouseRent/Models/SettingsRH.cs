using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRent.Models
{
    public class SettingsRH
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? Img { get; set; }
        public bool IsImage { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }



    }
}
