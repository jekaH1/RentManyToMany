namespace HouseRent.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OndDayPrice { get; set; }
        public int DayCount { get; set; }
        public int? TotalPrice { get; set; }
        public int? ApartmentId { get; set; }
        public int? OrderId { get; set; }    
        public Order? Order { get; set; }
        public Apartment? Apartment { get; set; }
    }
}
