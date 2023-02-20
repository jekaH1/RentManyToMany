using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRent.Models
{
    public class ApartmentImages
    {
        public int Id { get; set; }
        public int? ApartmentId { get; set; }
        public string? Img { get; set; }
        public bool IsPoster { get; set; }
        public Apartment? Apartment { get; set; }
        
    }
}
