using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Data;
using MovieReviewer.Models;
using System.Diagnostics;

namespace MovieReviewer.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _WebHostEnvironment;
        public HomeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string? search)
        {
            ViewBag.Search = search;
            if (search == null)
            {
                ViewBag.Movies = _context.Movie.ToList();
                ViewBag.Actors = _context.Actor.ToList();
                ViewBag.Directors = _context.Director.ToList();
            }
            else
            {
                ViewBag.Movies = _context.Movie.Where(m => m.MovieName.ToLower().Contains(search.ToLower())).ToList();
                ViewBag.Actors = _context.Actor.Where(act => (act.FirstName.ToLower() + act.LastName.ToLower()).Contains(search.ToLower())).ToList();
                ViewBag.Directors = _context.Director.Where(dir => (dir.FirstName.ToLower() + dir.LastName.ToLower()).Contains(search.ToLower())).ToList();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}