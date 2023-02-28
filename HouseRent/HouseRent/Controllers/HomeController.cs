//using AspNetCore;
using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using HouseRent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

namespace HouseRent.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Apartments = _appDbContext.Apartments.Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).OrderByDescending(x => x.TotalViewCount).Take(3).ToList(),
                Sliders = _appDbContext.Sliders.ToList(),
                ApartmentCategories = _appDbContext.ApartmentCategories.ToList(),
                AllForCount = _appDbContext.Apartments.ToList(),
                BlogPosts = _appDbContext.BlogPosts.Include(x => x.BlogPostComments).Take(3).ToList(),
                Orders = _appDbContext.Orders.ToList(),
                appUsers = _appDbContext.Users.ToList(),
                Categories = _appDbContext.ApartmentCategories.ToList(),
                GalleryImages = _appDbContext.GalleryImages.Take(6).ToList(),
                aboutUs=_appDbContext.AboutUs.ToList(), 
                
            };
            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Blogpostdetail(int id)
        {
            BlogPosts blogPosts = _appDbContext.BlogPosts.Include(x => x.BlogPostComments).FirstOrDefault(x => x.Id == id);
            if (blogPosts == null) { return NotFound(); }
            blogPosts.TotalViewCount++;
            BlogPostComment blogPostComment = null;
            BlogPostViewModel blogPostViewModel = new BlogPostViewModel
            {
                BlogPost = blogPosts,
                BlogPostComment = blogPostComment
            };
            _appDbContext.SaveChanges();
            return View(blogPostViewModel);
        }

        [HttpPost]
        public IActionResult Blogpostdetail(int id, BlogPostViewModel blogPostVM)
        {
            BlogPosts blogPosts = _appDbContext.BlogPosts.Include(x => x.BlogPostComments).FirstOrDefault(x => x.Id == id);
            if (blogPosts == null) { return NotFound(); }
            blogPostVM.BlogPostComment.BlogPostId = blogPosts.Id;
            blogPostVM.BlogPost = blogPosts;
            blogPostVM.BlogPost.Id = blogPosts.Id;

            BlogPostViewModel blogPostViewModel = new BlogPostViewModel
            {
                BlogPostComment = blogPostVM.BlogPostComment,
                BlogPost = blogPostVM.BlogPost,
            };

            BlogPostComment needed = blogPostVM.BlogPostComment;
            if (!ModelState.IsValid) { return View(blogPostVM); }
            if (blogPostVM.BlogPostComment.UserCommetMail is null)
            {
                ModelState.AddModelError("UserCommetMail", "Required");
                return View(blogPostVM);
            }
            if (blogPostVM.BlogPostComment.UserCommentMessage is null)
            {
                ModelState.AddModelError("UserCommentMessage", "Required");
                return View(blogPostVM);
            }
            if (blogPostVM.BlogPostComment.UserFullName is null)
            {
                ModelState.AddModelError("UserFullName", "Required");
                return View(blogPostVM);
            }

            _appDbContext.BlogPostComments.Add(needed);
            _appDbContext.SaveChanges();
            return RedirectToAction("Blogpostdetail");
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            Apartment apartment = _appDbContext.Apartments.Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).Include(x => x.ApartmentFeatures).FirstOrDefault(x => x.Id == id);
            OrderViewModel orderViewModel = new OrderViewModel
            {
                apartmentFeatures = _appDbContext.ApartmentFeatures.ToList(),
                Apartment = apartment,
                Fetures = _appDbContext.Features.ToList(),
            };
            if (apartment is null) { return NotFound(); }
            apartment.TotalViewCount++;
            _appDbContext.SaveChanges();
            return View(orderViewModel);

        }

        [HttpPost]
        public IActionResult Detail(int id, OrderViewModel orderVM)
        {
            Apartment apartment = _appDbContext.Apartments.Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).Include(x => x.ApartmentFeatures).FirstOrDefault(x => x.Id == id);
            orderVM.Apartment = apartment;
            orderVM.Apartment.City = apartment.City;
            orderVM.Apartment.Total = apartment.Total;
            orderVM.Apartment.Country = apartment.Country;
            orderVM.Apartment.Facilities = apartment.Facilities;
            orderVM.Apartment.RoomCategory = apartment.RoomCategory;
            orderVM.Apartment.FloorCount = apartment.FloorCount;
            orderVM.Apartment.AdressAndArea = apartment.AdressAndArea;
            orderVM.Fetures = _appDbContext.Features.ToList();
            orderVM.apartmentFeatures = _appDbContext.ApartmentFeatures.ToList();
            orderVM.IsCancelled = false;
            orderVM.Apartment.Id = apartment.Id;
            if (!ModelState.IsValid) { return View(orderVM); }
            int Startdate = orderVM.StartRentDate.DayOfYear;
            int Enddate = orderVM.EndRentDate.DayOfYear;
            int DayCount = Enddate - Startdate;
            int? TotalPrice = DayCount * apartment.Rentprice;
            foreach (var item in _appDbContext.Orders.Where(x => x.IsCancelled == false).Where(x => x.IsOver == false).Where(x=>x.OrderStatus != Enum.OrderStatus.Rejected).Where(x => x.ApartmentId == apartment.Id).ToList())
            {

                int date = item.StartRentDate.DayOfYear;
                int date2 = item.EndRentDate.DayOfYear;
                for (int i = date; i <= date2; i++)
                {
                    for (int j = Startdate; j <= Enddate; j++)
                    {
                        if (i == j)
                        {
                            ModelState.AddModelError("", "Already Reserverd");
                            return View(orderVM);
                        }
                    }
                }
            }
            if (orderVM.ChildCount < 0 || orderVM.FamilyMember < 0)
            {
                ModelState.AddModelError("ChildCount", "Select correct count");
                return View(orderVM);
            }
            if (orderVM.EndRentDate < orderVM.StartRentDate)
            {
                ModelState.AddModelError("EndRentDate", "Choice correct date");
                return View(orderVM);
            }
            if (orderVM.StartRentDate < DateTime.Now)
            {
                ModelState.AddModelError("StartRentDate", "Choice correct date");
                return View(orderVM);
            }
            if (orderVM.EndRentDate < DateTime.Now)
            {
                ModelState.AddModelError("EndRentDate", "Choice correct date");
                return View(orderVM);
            }
            if (DayCount > 30)
            {
                ModelState.AddModelError("EndRentDate", "Up to 1 month reservation allowed");
                return View(orderVM);
            }
            if (orderVM.StartRentDate.DayOfYear == orderVM.EndRentDate.DayOfYear)
            {
                ModelState.AddModelError("StartRentDate", "Reservation allowed from 1 up to 30 days");
                return View(orderVM);
            }

            Order order = null;
            order = new Order
            {
                EndRentDate = orderVM.EndRentDate,
                ChildCount = orderVM.ChildCount,
                FamilyMemberCount = orderVM.FamilyMember,
                StartRentDate = orderVM.StartRentDate,
                eMail = orderVM.eMail,
                Message = orderVM.Message,
                Fullname = orderVM.Fullname,
                PhoneNumber = orderVM.PhoneNumber,
                TotalPrice = TotalPrice,
                AppUserId = orderVM.AppUserId,
                ApartmentId = orderVM.Apartment.Id,
                Apartment = orderVM.Apartment,
                OneDayPrice = orderVM.RentPrice,
                IsCancelled = orderVM.IsCancelled,
            };
            _appDbContext.Orders.Add(order);
            TempData.Put<Order>("order", order);
            return RedirectToAction("CheckOut", TempData.Get<Order>("order"));
        }
        [HttpGet]
        public async Task<IActionResult> CheckOut(Order order)
        {
            CheckOutViewModel checkOutViewModel = null;
            Apartment apartment = _appDbContext.Apartments.Include(x => x.ApartmentFeatures).Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).FirstOrDefault(x => x.Id == order.ApartmentId);
            order.Apartment = apartment;
            order.Apartment.ApartmentFeatures = _appDbContext.ApartmentFeatures.Where(x => x.ApartmentId == order.ApartmentId).ToList();
            apartment.ApartmentFeatures = _appDbContext.ApartmentFeatures.Where(x => x.ApartmentId == order.ApartmentId).ToList();
            order.Apartment.ApartmentImages = _appDbContext.ApartmentImages.Where(x => x.ApartmentId == order.ApartmentId).ToList();
            List<int> month = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                month.Add(i);
            }
            List<int> days = new List<int>();
            for (int i = 1; i < 31; i++)
            {
                days.Add(i);
            }
            ViewBag.Days = days;
            ViewBag.Month = month;
            checkOutViewModel = new CheckOutViewModel
            {
                Apartment = apartment,
                ApartmentId = order.ApartmentId,
                Order = order,
                OrderId = order.Id,
            };

            return View(checkOutViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutViewModel checkOutViewModel)
        {
            Order order = null;
            Apartment apartment = _appDbContext.Apartments.Include(x => x.ApartmentFeatures).Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).FirstOrDefault(x => x.Id == checkOutViewModel.ApartmentId);
            checkOutViewModel.Apartment = apartment;
            checkOutViewModel.Order = checkOutViewModel.Order;
            List<int> month = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                month.Add(i);
            }
            List<int> days = new List<int>();
            for (int i = 1; i < 31; i++)
            {
                days.Add(i);
            }
            ViewBag.Days = days;
            ViewBag.Month = month;
            if (checkOutViewModel.CVV.Value < 100 || checkOutViewModel.CVV.Value > 999)
            {
                ModelState.AddModelError("CVV", "3 digits required");
                return View(checkOutViewModel);
            }
            if (checkOutViewModel.CardNum.Value < 10000000000000 || checkOutViewModel.CVV.Value > 99999999999999)
            {
                ModelState.AddModelError("CardNum", "14 digits required");
                return View(checkOutViewModel);
            }

            if (!ModelState.IsValid) { return View(checkOutViewModel); }

            order = new Order
            {
                EndRentDate = checkOutViewModel.Order.EndRentDate,
                ChildCount = checkOutViewModel.Order.ChildCount,
                FamilyMemberCount = checkOutViewModel.Order.FamilyMemberCount,
                StartRentDate = checkOutViewModel.Order.StartRentDate,
                eMail = checkOutViewModel.Order.eMail,
                Message = checkOutViewModel.Order.Message,
                Fullname = checkOutViewModel.Order.Fullname,
                PhoneNumber = checkOutViewModel.Order.PhoneNumber,
                TotalPrice = checkOutViewModel.Order.TotalPrice,
                AppUserId = checkOutViewModel.Order.AppUserId,
                ApartmentId = checkOutViewModel.Apartment.Id,
                Apartment = checkOutViewModel.Apartment,
                OneDayPrice = checkOutViewModel.Apartment.Rentprice,
                IsCancelled = checkOutViewModel.Order.IsCancelled,
                CardMonth = checkOutViewModel.CardMonth,
                CardNum = checkOutViewModel.CardNum,
                CardYear = checkOutViewModel.CardYear,
                CVV = checkOutViewModel.CVV,
                NameOncard = checkOutViewModel.NameOncard,
            };
            _appDbContext.Add(order);
            _appDbContext.SaveChanges();
            return RedirectToAction("index","order");
        }

        [HttpGet]
        public async Task<IActionResult> AllApartments(AllApartmentsViewModel model, string filter, DateTime? dateTimeAsent = null, DateTime? dateTimeDesent = null,
                                                        int? AsentPrice = 0, int? desent = 0, int? popularity = 0,
                                                        string? Warehouse = null, string? family = null, string? office = null, string? FemaleMess = null, int page = 1)
        {
            var apartments = _appDbContext.Apartments.Include(x => x.ApartmentImages).Include(x => x.ApartmentCategory).OrderByDescending(x => x.TotalViewCount).AsQueryable();
            var query = apartments;
            switch (filter)
            {
                case "AsentPrice":
                    query = apartments.OrderBy(x => x.Rentprice);
                    break;
                case "desent":
                    query = apartments.OrderByDescending(x => x.Rentprice);
                    break;
                case "dateTimeAsent":
                    query = apartments.OrderByDescending(x => x.ApartmentCreationDate);
                    break;
                case "dateTimeDesent":
                    query = apartments.OrderBy(x => x.ApartmentCreationDate);
                    break;
                case "Warehouse":
                    query = apartments.Where(x => x.ApartmentCategory.AppartmentCategoryName == "Warehouse");
                    break;
                case "family":
                    query = apartments.Where(x => x.ApartmentCategory.AppartmentCategoryName == "Family House");
                    break;
                case "Office":
                    query = apartments.Where(x => x.ApartmentCategory.AppartmentCategoryName == "Office");
                    break;
                case "FemaleMess":
                    query = apartments.Where(x => x.ApartmentCategory.AppartmentCategoryName == "Female Mess");
                    break;
                case "Apartment":
                    query = apartments.Where(x => x.ApartmentCategory.AppartmentCategoryName == "Apartment");
                    break;
                case "popularity":
                    query = apartments.OrderByDescending(x => x.TotalViewCount);
                    break;
                default:
                    break;
            }
            ViewBag.filters = filter;
            model = new AllApartmentsViewModel
            {
                Apartments = PaginatedList<Apartment>.Create(query, 6, page),
            };
            return View(model);
        }
    }
}