using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRent.Models
{
    public class ApartmentCategory
    {
        public int Id { get; set; }
        [StringLength(maximumLength:90)]
        public string? AppartmentCategoryName { get; set; }
        public string Action { get; set; }  
        public string? ImgApart { get; set; }
        [NotMapped]
        public IFormFile? ImageApart { get; set; }    
        public List<Apartment>? Apartments { get; set; }

    }
}
