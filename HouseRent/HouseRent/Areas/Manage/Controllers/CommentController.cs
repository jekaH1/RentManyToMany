using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class CommentController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CommentController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult CreateComment(BlogPostComment comment)
        {
            if (!ModelState.IsValid) { return View(comment); }
            _appDbContext.Add(comment);
            _appDbContext.SaveChanges();
            return RedirectToAction("index", "home", new { Area = "" });

        }
    }
}
