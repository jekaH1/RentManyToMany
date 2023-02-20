using HouseRent.Models;
using System.Net;

namespace HouseRent.ViewModels
{

    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Apartment> Apartments { get; set; }
        public List<ApartmentCategory> ApartmentCategories { get; set; }

        public List<Apartment> AllForCount { get; set; }

        public List<BlogPosts> BlogPosts { get; set; }  
        public List<Order> Orders { get; set; }

        public List<AppUser> appUsers { get; set; } 
    }
}
