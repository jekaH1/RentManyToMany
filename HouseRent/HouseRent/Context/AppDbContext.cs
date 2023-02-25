using HouseRent.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseRent.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentCategory> ApartmentCategories { get; set; }   
        public DbSet<ApartmentImages> ApartmentImages { get; set; } 
        public DbSet<SettingsRH> SettingsRH { get; set; }
        public DbSet<BlogPosts> BlogPosts { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<AboutUS> AboutUs { get; set; }
        public DbSet<ApartmentFeature> ApartmentFeatures { get; set; }
        public DbSet<Feature> Features { get; set; }   

        public DbSet<toDoList> ToDoListOrders { get; set; } 
        public DbSet<AdminMessage> AdminMessages { get; set; }
        public DbSet<GalleryImages> GalleryImages { get; set; } 
    }
}
