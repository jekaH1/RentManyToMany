using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class FeaturesController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public FeaturesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            List<Feature> features = _appDbContext.Features.ToList();
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
