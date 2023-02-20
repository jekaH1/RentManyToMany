using Microsoft.AspNetCore.Identity;

namespace HouseRent.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
