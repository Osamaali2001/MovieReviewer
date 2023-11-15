using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Data;
using MovieReviewer.Models;

namespace MovieReviewer.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _WebHostEnvironment;
        public UsersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            User user = _context.User.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            return View(user);
        }

        [HttpGet]
        public IActionResult GetEditView(int id) 
        {
            User user = _context.User.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            return View("Edit",user);
        }

        [HttpPost]
        public IActionResult EditProfile(User user, IFormFile? ImageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFormFile != null)
                {
                    if (user.ImagePath != "\\images\\No_Image.png")
                    {
                        string oldImgPath = _WebHostEnvironment.WebRootPath + user.ImagePath;
                        System.IO.File.Delete(oldImgPath);
                    }
                    string imgExtension = Path.GetExtension(ImageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    user.ImagePath = imgPath;
                    string imgFullPath = _WebHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    ImageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                _context.User.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Profile");
            }
            else
            {
                return View("Edit", user);
            }
        }

        [HttpGet]
        public IActionResult FavMovie()
        {
            User user = _context.User.Include(u => u.PreferredMovies).FirstOrDefault(u => u.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            return View(user);
        }

        [HttpGet]
        public IActionResult FavDirector()
        {
            User user = _context.User.Include(u => u.PreferredDirectors).FirstOrDefault(u => u.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            return View(user);
        }

        [HttpGet]
        public IActionResult FavActor()
        {
            User user = _context.User.Include(u => u.PreferredActors).FirstOrDefault(u => u.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            return View(user);
        }

        [HttpPost]
        public IActionResult AddFavMovieToDB(int Id)
        {
            User user = _context.User.Include(u => u.PreferredMovies).FirstOrDefault(u=>u.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            user.PreferredMovies.Add(_context.Movie.FirstOrDefault(m => m.MovieId == Id));
            _context.SaveChanges();
            return RedirectToAction("GetDetailsView","Movies", new { id = Id });
        }

        [HttpPost]
        public IActionResult AddFavDirectorToDB(int Id)
        {
            User user = _context.User.Include(u => u.PreferredDirectors).FirstOrDefault(u => u.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            user.PreferredDirectors.Add(_context.Director.FirstOrDefault(d => d.Id == Id));
            _context.SaveChanges();
            return RedirectToAction("GetDetailsView", "Directors", new { id = Id });
        }

        [HttpPost]
        public IActionResult AddFavActorToDb(int Id)
        {
            User user = _context.User.Include(u => u.PreferredActors).FirstOrDefault(u => u.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            user.PreferredActors.Add(_context.Actor.FirstOrDefault(a => a.Id== Id));
            _context.SaveChanges();
            return RedirectToAction("GetDetailsView", "Actors", new { id = Id });
        }

        [HttpPost]
        public IActionResult Like(int id)
        {
            User user = _context.User.Include(u => u.LikedMovies).Include(u => u.DislikedMovies).FirstOrDefault(u => u.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            Movie movie = _context.Movie.FirstOrDefault(u => u.MovieId == id);
            if (user.LikedMovies.Contains(movie))
            {
                user.LikedMovies.Remove(movie);
            }
            else
            {
                if (user.DislikedMovies.Contains(movie))
                {
                    user.DislikedMovies.Remove(movie);
                }
                user.LikedMovies.Add(movie);
            }
            _context.SaveChanges();
            return RedirectToAction("GetDetailsView", "Movies", new { Id = id });
        }

        [HttpPost]
        public IActionResult DisLike(int id)
        {
            User user = _context.User.Include(u => u.LikedMovies).Include(u => u.DislikedMovies).FirstOrDefault(u => u.Id == int.Parse(HttpContext.Session.GetString("UserId")));
            Movie movie = _context.Movie.FirstOrDefault(u => u.MovieId == id);
            if (user.DislikedMovies.Contains(movie))
            {
                user.DislikedMovies.Remove(movie);
            }
            else
            {
                if (user.LikedMovies.Contains(movie))
                {
                    user.LikedMovies.Remove(movie);
                }
                user.DislikedMovies.Add(movie);
            }
            _context.SaveChanges();
            return RedirectToAction("GetDetailsView", "Movies", new { Id = id });
        }
    }
}
