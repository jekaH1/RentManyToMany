using System.ComponentModel.DataAnnotations;

namespace HouseRent.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Username { get; set; }
        [Required]
        [StringLength(maximumLength: 60), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 8), DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string CheckPass { get; set; }
        [StringLength(25),DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
