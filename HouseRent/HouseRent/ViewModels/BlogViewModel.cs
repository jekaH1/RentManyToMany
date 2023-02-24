using HouseRent.Helper;
using HouseRent.Models;

namespace HouseRent.ViewModels
{
    public class BlogViewModel
    {
        public PaginatedList<BlogPosts> BlogPosts { get; set; }  
        public List<BlogPosts> BlogPosts4 { get; set; } 
    }
}
