using System.ComponentModel.DataAnnotations;

namespace HouseRent.Models
{
    public class BlogPostComment
    {
        public int? Id { get; set; }
        [StringLength(maximumLength: 40, MinimumLength = 5)]
        public string? UserFullName { get; set; }
        [StringLength(maximumLength: 300, MinimumLength = 5)]
        public string? UserCommentMessage { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 5), DataType(DataType.EmailAddress)]
        public string? UserCommetMail { get; set; }
        public DateTime UserMessageTime { get; set; } = DateTime.Now;
        public string? UserImg { get; set; }
        public int? BlogPostId { get; set; }
        public BlogPosts? BlogPost { get; set; }
    }
}
