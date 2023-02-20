using System.ComponentModel.DataAnnotations;

namespace HouseRent.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 90)]
        public string? FeatureName { get; set; }
        public List<ApartmentFeature>? ApartmentFeatures { get; set; }
    }
}
