using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRent.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public CategoryViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IViewComponentResult Invoke()
        {
            List<ApartmentCategory> categories = _appDbContext.ApartmentCategories.ToList();
            return View(categories);
        }
    }
}
