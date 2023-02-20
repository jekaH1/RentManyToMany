using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRent.ViewComponents
{
    public class ApartmentViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public ApartmentViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IViewComponentResult Invoke()
        {
            List<Apartment> apartments = _appDbContext.Apartments.Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).OrderByDescending(x => x.TotalViewCount).Take(3).ToList();
            return View(apartments);
        }
    }
}
