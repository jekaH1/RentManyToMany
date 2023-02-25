using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HouseRent.Controllers
{
    public class GalleryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public GalleryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index(int page=1)
        {
            var query=_appDbContext.GalleryImages.AsQueryable();
            PaginatedList<GalleryImages> galleryImages = PaginatedList<GalleryImages>.Create(query, 8, page);
            return View(galleryImages);
        }
    }
}
