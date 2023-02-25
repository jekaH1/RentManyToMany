using HouseRent.Models;
using System.ComponentModel.DataAnnotations;

namespace HouseRent.ViewModels
{
    public class CheckOutViewModel
    {
        public Apartment? Apartment { get; set; }
        public int? ApartmentId { get; set; }
        public Order? Order { get; set; }
        public int? OrderId { get; set; }
        //[DataType(DataType.CreditCard)]
        public ulong? CardNum { get; set; }
        public byte? CardMonth { get; set; }
        public byte? CardYear { get; set;}
        public int? CVV { get; set;}
        //[DataType(DataType.Text)]k
        public string? NameOncard { get; set; }
    }
}
