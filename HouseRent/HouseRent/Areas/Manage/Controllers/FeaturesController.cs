using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class FeaturesController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public FeaturesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index(int page=1)
        {
            var query= _appDbContext.Features.AsQueryable();
            PaginatedList<Feature> features = PaginatedList<Feature>.Create(query, 10, page);
            return View(features);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            if(!ModelState.IsValid) { return View(feature); }
            _appDbContext.Features.Add(feature);
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Feature feature = _appDbContext.Features.FirstOrDefault(x => x.Id == id);
            if (feature == null) { return NotFound(); }
            return View(feature);
        }
        [HttpPost]
        public IActionResult Update(Feature newFeature)
        {
            if (!ModelState.IsValid) { return View(newFeature); } 
            Feature exFeature= _appDbContext.Features.FirstOrDefault(x=>x.Id == newFeature.Id);
            if (exFeature == null) { return NotFound(); }
            exFeature.FeatureName=newFeature.FeatureName;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int id) 
        {
            Feature feature= _appDbContext.Features.FirstOrDefault(x => x.Id == id);
            if(feature==null) { return NotFound(); }
            _appDbContext.Remove(feature);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
