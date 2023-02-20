using HouseRent.Enum;
using System.ComponentModel.DataAnnotations;

namespace HouseRent.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string Fullname { get; set; }
        [Required]
        [StringLength(maximumLength: 30), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(maximumLength: 100), DataType(DataType.Password)]
        public string eMail { get; set; }
        [Required]
        public byte FamilyMemberCount { get; set; }
        [Required]
        public byte ChildCount { get; set; }

        [StringLength(maximumLength: 200)]
        public string? Message { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartRentDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndRentDate { get; set; }

        //public DateTime TotalRentDay { get => StartRentDate.; }
        
        [DataType(DataType.DateTime)]
        public DateTime? OrderDay { get; set; } = DateTime.Now;
        public string? AppUserId { get; set; }
        public AppUser? User { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public int? TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public Apartment? Apartment { get; set; }
        public int? ApartmentId { get; set; }
        public int OneDayPrice { get; set; }
        public bool IsOver { get; set; }    
        public bool? IsCancelled { get; set; }
        public string? DeleteMessage { get; set; }  
    }
}