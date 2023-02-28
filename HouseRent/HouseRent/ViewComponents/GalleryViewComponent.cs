using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRent.ViewComponents
{
    public class GalleryViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public GalleryViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IViewComponentResult Invoke()
        {
            List<GalleryImages> galleryImages = _appDbContext.GalleryImages.Take(6).ToList();
            return View(galleryImages);
        }
    }
}
