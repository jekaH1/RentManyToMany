using HouseRent.Models;

namespace HouseRent.ViewModels
{
    public class CheckOutViewModel
    {
        public Apartment? Apartment { get; set; }
        public int? ApartmentId { get; set; }
        public Order? Order { get; set; }
        public int? OrderId { get; set; }
        public string? CardNum { get; set; }
        public byte? CardMonth { get; set; }
        public byte? CardYear { get; set;}
        public byte? CVV { get; set;}
        public string? NameOncard { get; set; }
    }
}
