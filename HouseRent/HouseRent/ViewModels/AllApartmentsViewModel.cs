using HouseRent.Helper;
using HouseRent.Models;

namespace HouseRent.ViewModels
{
    public class AllApartmentsViewModel
    {
        public PaginatedList<Apartment> Apartments { get; set; }
    }
}
