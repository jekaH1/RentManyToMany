using HouseRent.Models;
using System.ComponentModel.DataAnnotations;

namespace HouseRent.ViewModels
{
    public class OrderViewModel
    {
        public Apartment? Apartment { get; set; }
        [StringLength(maximumLength: 40)]
        public string? Fullname { get; set; }
        [StringLength(maximumLength: 30), DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [StringLength(maximumLength: 100), DataType(DataType.EmailAddress)]
        public string? eMail { get; set; }
        public byte FamilyMember { get; set; }
        public byte ChildCount { get; set; }

        [StringLength(maximumLength: 200)]
        public string? Message { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartRentDate { get; set; } 
        [DataType(DataType.Date)]
        public DateTime EndRentDate { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int RentPrice { get; set; }
        public bool IsCancelled { get; set; }
        public List<ApartmentFeature>? apartmentFeatures { get; set; }
        public List<Feature>? Fetures { get; set; }
    }
}
