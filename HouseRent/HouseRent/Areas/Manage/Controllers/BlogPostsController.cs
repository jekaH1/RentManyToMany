using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class BlogPostsController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public BlogPostsController(AppDbContext appDbContext,IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<BlogPosts> posts = _appDbContext.BlogPosts.ToList(); 
            return View(posts);
        }
        public IActionResult CommentIndex(int id)
        {
            List <BlogPostComment> comments=_appDbContext.BlogPostComments.Include(x=>x.BlogPost).Where(x=>x.BlogPostId==id).ToList();
            return View(comments);
        }

        public IActionResult DeleteComment(int id)
        {
            BlogPostComment blogPostComment = _appDbContext.BlogPostComments.Include(x=>x.BlogPost).FirstOrDefault(y=>y.Id==id);
            if(blogPostComment is null) { return NotFound(); }
            _appDbContext.Remove(blogPostComment);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogPosts post)
        {
            if(!ModelState.IsValid) { return View(post); }
            if (post.Image is null)
            {
                ModelState.AddModelError("Image", "Required");
                return View(post);
            }
            if (post.Image.ContentType != "image/jpeg" && post.Image.ContentType != "image/png")
            {
                ModelState.AddModelError("Image", "Only PNG and JPEG files allowed");
                return View(post);
            }
            if (post.Image.Length > 2097152)
            {
                ModelState.AddModelError("Image", "Up to 2mb files allowed");
                return View(post);
            }
            post.Img = post.Image.SaveFile(_env.WebRootPath, "uploads/blogposts");
            _appDbContext.Add(post);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            BlogPosts post = _appDbContext.BlogPosts.FirstOrDefault(x => x.Id == id);
            if (post is null) { return NotFound(); }
            string path = Path.Combine(_env.WebRootPath, "uploads/blogposts", post.Img);
            System.IO.File.Delete(path);
            _appDbContext.Remove(post);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            BlogPosts post = _appDbContext.BlogPosts.FirstOrDefault(x => x.Id == id);
            if (post is null) { return NotFound(); }
            return View(post);
        }

        [HttpPost]
        public IActionResult Update(BlogPosts newPost)
        {
            if (!ModelState.IsValid) { return View(newPost); }
            BlogPosts exPost = _appDbContext.BlogPosts.FirstOrDefault(x => x.Id == newPost.Id);
            if (exPost is null) { return NotFound(); }

            if (newPost.Image != null)
            {
                if (newPost.Image.ContentType != "image/jpeg" && newPost.Image.ContentType != "image/png")
                {
                    ModelState.AddModelError("Image", "Only PNG and JPEG files allowed");
                    return View(newPost);
                }
                if (newPost.Image.Length > 2097152)
                {
                    ModelState.AddModelError("Image", "Up to 2mb files allowed");
                    return View(newPost);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/blogposts", exPost.Img);
                if (System.IO.File.Exists(path))
                {

                    System.IO.File.Delete(path);
                }
                exPost.Img = newPost.Image.SaveFile(_env.WebRootPath, "uploads/blogposts");
            }
            exPost.Title = newPost.Title;
            exPost.TwitterUrl = newPost.TwitterUrl;
            exPost.Quote=newPost.Quote;
            exPost.Quote2=newPost.Quote2;
            exPost.FaceUrl= newPost.FaceUrl;
            exPost.Category= newPost.Category;
            exPost.HeartUrl= newPost.HeartUrl;
            exPost.IgUrl= newPost.IgUrl;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
