namespace HouseRent.Models
{
    public class ApartmentFeature
    {
        public int Id { get; set; }
        public int? FeatureId { get; set; }
        public int? ApartmentId { get; set; }
        public Feature? Feature { get; set; }
        public Apartment? Apartment { get; set; }
    }
}
