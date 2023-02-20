using Microsoft.Build.Execution;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRent.Models
{
    public class AboutUS
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? AvatarImg { get; set; }
        [NotMapped]
        public IFormFile? AvatarImage { get; set; }
        public string CeoName { get; set; }
        public string Position { get; set; }
        public string Quote { get; set; }
        public string? SignImg { get; set; }
        [NotMapped]
        public IFormFile? SignImage { get; set; }
        public string FaceUrl { get; set; }
        public string GplusUrl { get; set; }
        public string PintereseUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string SmthUrl { get; set; }

    }
}
