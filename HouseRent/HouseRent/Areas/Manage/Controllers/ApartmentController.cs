using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class ApartmentController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public ApartmentController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            var query= _appDbContext.Apartments.Include(x => x.ApartmentCategory).Include(x => x.ApartmentImages).AsQueryable();
            PaginatedList<Apartment> apartments=PaginatedList<Apartment>.Create(query, 4, page);
            return View(apartments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _appDbContext.ApartmentCategories.ToList();
            ViewBag.Features = _appDbContext.Features.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Apartment apartment)
        {
            ViewBag.Categories = _appDbContext.ApartmentCategories.ToList();
            ViewBag.Features = _appDbContext.Features.ToList();
            if (!ModelState.IsValid) { return View(apartment); }

            if (apartment.PosterImage is null)
            {
                ModelState.AddModelError("PosterImage", "Required");
                return View(apartment);
            }
            if(apartment.Images is null)
            {
                ModelState.AddModelError("Images", "Required");
                return View(apartment);
            }
            if (apartment.PosterImage.ContentType != "image/jpeg" && apartment.PosterImage.ContentType != "image/png")
            {
                ModelState.AddModelError("PosterImage", "Only PNG and JPEG files allowed");
                return View(apartment);
            }
            if (apartment.PosterImage.Length > 2097152)
            {
                ModelState.AddModelError("PosterImage", "Up to 2mb files allowed");
                return View(apartment);
            }
            ApartmentImages Images = new ApartmentImages
            {
                Img = apartment.PosterImage.SaveFile(_env.WebRootPath, "uploads/apartments"),
                IsPoster = true,
                Apartment = apartment
            };
            _appDbContext.ApartmentImages.Add(Images);

            foreach (var Image in apartment.Images)
            {
                if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
                {
                    ModelState.AddModelError("Images", "Only PNG and JPEG files allowed");
                    return View(apartment);
                }
                if (Image.Length > 2097152)
                {
                    ModelState.AddModelError("Images", "Up to 2mb files allowed");
                    return View(apartment);
                }

                ApartmentImages apartmentImages = new ApartmentImages
                {
                    Apartment = apartment,
                    Img = Image.SaveFile(_env.WebRootPath, "uploads/apartments"),
                    IsPoster = false

                };
                _appDbContext.ApartmentImages.Add(apartmentImages);
            }
            foreach (var item in apartment.ApartmentFeaturesIds)
            {
                ApartmentFeature apartmentFeature = new ApartmentFeature
                {
                    Apartment = apartment,
                    FeatureId = item,

                };
                _appDbContext.ApartmentFeatures.Add(apartmentFeature);
            }
            apartment.TotalViewCount = 0;
            apartment.IsReserved= false;
            _appDbContext.Apartments.Add(apartment);
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Apartment apartment = _appDbContext.Apartments.Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).Include(x => x.ApartmentFeatures).FirstOrDefault(x => x.Id == id);
            if (apartment is null) { return NotFound(); }

            foreach (var item in apartment.ApartmentImages)
            {
                string path = Path.Combine(_env.WebRootPath, "uploads/apartments", item.Img);
                System.IO.File.Delete(path);
            }
            _appDbContext.Remove(apartment);
            _appDbContext.SaveChanges();
            return RedirectToAction("index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Features = _appDbContext.Features.ToList();
            ViewBag.Categories = _appDbContext.ApartmentCategories.ToList();
            Apartment apartment = _appDbContext.Apartments.Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).Include(x => x.ApartmentFeatures).FirstOrDefault(x => x.Id == id);
            if (apartment is null) { return NotFound(); }
            return View(apartment);
        }

        [HttpPost]
        public IActionResult Update(Apartment newApartment)
        {
            ViewBag.Features = _appDbContext.Features.ToList();
            ViewBag.Categories = _appDbContext.ApartmentCategories.ToList();
            if (!ModelState.IsValid) { return View(newApartment); }
            Apartment exApartment = _appDbContext.Apartments.Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).Include(x => x.ApartmentFeatures).FirstOrDefault(x => x.Id == newApartment.Id);
            if (exApartment is null) { return NotFound(); }

            List<ApartmentImages> ImagesDelet = exApartment.ApartmentImages.FindAll(ai => !newApartment.ApartmentImagesIds.Contains(ai.Id) && ai.IsPoster is false);
            foreach (var item in ImagesDelet)
            {
                string path = Path.Combine(_env.WebRootPath, "uploads/apartments", item.Img);
                System.IO.File.Delete(path);
            }
            exApartment.ApartmentImages.RemoveAll(ai => !newApartment.ApartmentImagesIds.Contains(ai.Id) && ai.IsPoster is false);

            if (newApartment.Images != null)
            {
                foreach (var Image in newApartment.Images)
                {
                    if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Images", "Only PNG and JPEG files allowed");
                        return View(newApartment);
                    }
                    if (Image.Length > 2097152)
                    {
                        ModelState.AddModelError("Images", "Up to 2mb files allowed");
                        return View(newApartment);
                    }

                    ApartmentImages apartmentImages = new ApartmentImages
                    {
                        ApartmentId = newApartment.Id,
                        Img = Image.SaveFile(_env.WebRootPath, "uploads/apartments"),
                        IsPoster = false

                    };
                    _appDbContext.ApartmentImages.Add(apartmentImages);
                }
            }
            if (newApartment.PosterImage != null)
            {
                if (newApartment.PosterImage.ContentType != "image/jpeg" && newApartment.PosterImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("PosterImage", "Only PNG and JPEG files allowed");
                    return View(newApartment);
                }
                if (newApartment.PosterImage.Length > 2097152)
                {
                    ModelState.AddModelError("PosterImage", "Up to 2mb files allowed");
                    return View(newApartment);
                }

                foreach (var item in exApartment.ApartmentImages)
                {
                    if (item.IsPoster is true)
                    {
                        string path = Path.Combine(_env.WebRootPath, "uploads/apartments", item.Img);
                        System.IO.File.Delete(path);
                        _appDbContext.Remove(item);
                    }
                }

                ApartmentImages Images = new ApartmentImages
                {
                    Img = newApartment.PosterImage.SaveFile(_env.WebRootPath, "uploads/apartments"),
                    IsPoster = true,
                    ApartmentId = newApartment.Id
                };

                _appDbContext.ApartmentImages.Add(Images);

            }
            if (newApartment.ApartmentFeaturesIds is null)
            {
                exApartment.ApartmentFeaturesIds = newApartment.ApartmentFeaturesIds;
            }
            else
            {
                foreach (var item in exApartment.ApartmentFeatures)
                {
                    _appDbContext.ApartmentFeatures.Remove(item);
                }
                foreach (var item in newApartment.ApartmentFeaturesIds)
                {
                    ApartmentFeature apartmentFeature = new ApartmentFeature
                    {
                        ApartmentId = newApartment.Id,
                        FeatureId = item,
                    };
                    _appDbContext.ApartmentFeatures.Add(apartmentFeature);
                }
            }
            exApartment.Rentprice = newApartment.Rentprice;
            exApartment.ApartmentCategoryID = newApartment.ApartmentCategoryID;
            exApartment.RoomCategory = newApartment.RoomCategory;
            exApartment.AdditionalFacilities = newApartment.AdditionalFacilities;
            exApartment.AdressAndArea = newApartment.AdressAndArea;
            exApartment.BathCount = newApartment.BathCount;
            exApartment.CarSpace = newApartment.CarSpace;
            exApartment.City = newApartment.City;
            exApartment.Country = newApartment.Country;
            exApartment.Total = newApartment.Total;
            exApartment.Facilities = newApartment.Facilities;
            exApartment.FloorCount = newApartment.FloorCount;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
