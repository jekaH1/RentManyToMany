using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HouseRent.Models
{
    public class Apartment
    {
        public int? Id { get; set; }
        public int? FlatSize { get; set; }
        public int? Rentprice { get; set; }
        public string? Total { get; set; }  
        public string? AdressAndArea { get; set; }
        public string? FloorCount { get; set; }
        public string? RoomCategory { get; set; }
        public string? Facilities { get; set; }
        public string? AdditionalFacilities { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public byte? CarSpace { get; set; }
        public byte? BathCount { get; set; }
        public byte? BedRoomCount { get; set; }
        public int? TotalViewCount { get; set; }
        public int? ApartmentCategoryID { get; set; }
        public DateTime? ApartmentCreationDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        [IgnoreDataMember]
        public List<ApartmentImages>? ApartmentImages { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ApartmentCategory? ApartmentCategory { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public List<ApartmentFeature>? ApartmentFeatures { get; set; }
        public bool IsReserved { get; set; }
        
        [NotMapped]
        public IFormFile? PosterImage { get; set; }
        [NotMapped]
        public List<IFormFile>? Images { get; set; }
        [NotMapped]
        public List<int>? ApartmentImagesIds { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        [NotMapped]
        public List<int>? ApartmentFeaturesIds { get; set; } 
    }
}
