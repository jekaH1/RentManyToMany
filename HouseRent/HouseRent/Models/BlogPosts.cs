using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace HouseRent.Models
{
    public class BlogPosts
    {
        public int Id { get; set; }
        public DateTime BlogPostDate { get; set; } = DateTime.Now;
        [Required]
        [StringLength(maximumLength:60)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Quote { get; set; }
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Quote2 { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        public string Category { get; set; }
        public string? Img { get; set; }
        public string FaceUrl { get; set; }
        public string IgUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string HeartUrl { get; set; }
        public int TotalViewCount { get; set; }
        public List<BlogPostComment>? BlogPostComments { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }

    }
}
