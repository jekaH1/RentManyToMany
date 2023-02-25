using HouseRent.Models;
using System.Net;

namespace HouseRent.ViewModels
{
    public class BlogPostViewModel
    {
        public BlogPosts? BlogPost { get; set; } 
        public BlogPostComment? BlogPostComment { get; set; }
    }
}
