using System.ComponentModel.DataAnnotations;

namespace HouseRent.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Username { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 8), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
