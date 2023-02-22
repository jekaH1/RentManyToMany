using HouseRent.Models;

namespace HouseRent.ViewModels
{
    public class CheckOutViewModel
    {
        public Apartment? Apartment { get; set; }
        public int? ApartmentId { get; set; }
        public Order? Order { get; set; }
        public int? OrderId { get; set; }
    }
}
