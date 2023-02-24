using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using HouseRent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRent.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index(BlogViewModel blogVM,string filter,string? recent=null,string? popular=null,string? treding=null,int page=1)
        {
            var query = _appDbContext.BlogPosts.Include(x => x.BlogPostComments).AsQueryable();
            switch (filter)
            {
                case "recent":
                    query = query.OrderByDescending(x => x.BlogPostDate);
                    break;
                case "popular":
                    query = query.OrderByDescending(x => x.TotalViewCount);
                    break;
                case "treding":
                    query = query.OrderByDescending(x => x.BlogPostComments.Count());
                    break;
                default:
                    break;
            }
            ViewBag.filters = filter;
            BlogViewModel blogViewModel = new BlogViewModel
            {
                BlogPosts = PaginatedList<BlogPosts>.Create(query,4,page),
                BlogPosts4 = _appDbContext.BlogPosts.Include(x => x.BlogPostComments).Take(4).ToList(),
            };
            return View(blogViewModel);
        }
    }
}
