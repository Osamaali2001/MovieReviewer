using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Data;
using MovieReviewer.Models;

namespace MovieReviewer.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _WebHostEnvironment;
        public MoviesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetIndexView(string? search)
        {
            List<Movie> movies;
            ViewBag.MoviesSearch = search;
            if (search == null)
            {
                movies = _context.Movie.Include(d => d.Director).ToList();
            }
            else
            {
                movies = _context.Movie.Where(act => act.MovieName.ToLower().Contains(search.ToLower())).Include(d => d.Director).ToList();
            }
            return View("Index", movies);
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Movie movie = _context.Movie
                .Include(d => d.Director)
                .Include(d => d.ActtorsIn)
                .Include(d => d.LikedByUsers)
                .Include(d => d.DislikedByUsers)
                .FirstOrDefault(x => x.MovieId == id);
            ViewBag.LikesCount = movie.LikedByUsers.Count();
            ViewBag.DisLikesCount = movie.DislikedByUsers.Count();
            return View("Details", movie);
        }

        [HttpGet]
        public IActionResult GetCreateView()
        {
            ViewBag.AllActors = _context.Actor.ToList();
            ViewBag.AllDirectors = _context.Director.ToList();
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNew(Movie movie, IFormFile? ImageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFormFile != null)
                {
                    string imgExtension = Path.GetExtension(ImageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    movie.ImagePath = imgPath;
                    string imgFullPath = _WebHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    ImageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                else
                {
                    movie.ImagePath = "\\images\\No_Image.png";
                }
                _context.Movie.Add(movie);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
                return View("Create");
        }

        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Movie movie = _context.Movie.Include(d => d.Director).Include(a => a.ActtorsIn).FirstOrDefault(m => m.MovieId == id);
            ViewBag.AllDirectors = _context.Director.ToList();
            return View("Edit", movie);
        }

        [HttpPost]
        public IActionResult EditCurrent(Movie movie, IFormFile? ImageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFormFile != null)
                {
                    if (movie.ImagePath != "\\images\\No_Image.png")
                    {
                        string oldImgPath = _WebHostEnvironment.WebRootPath + movie.ImagePath;
                        System.IO.File.Delete(oldImgPath);
                    }
                    string imgExtension = Path.GetExtension(ImageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    movie.ImagePath = imgPath;
                    string imgFullPath = _WebHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    ImageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                _context.Movie.Update(movie);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit", movie);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Movie movie = _context.Movie.Include(m => m.ActtorsIn).Include(m => m.Director).FirstOrDefault(m => m.MovieId == id);
            return View("Delete", movie);
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Movie movie = _context.Movie.Include(m => m.ActtorsIn).FirstOrDefault(m => m.MovieId == id);
            movie.ActtorsIn.Clear();
            if (movie.ImagePath != "\\images\\No_Image.png")
            {
                string imgPath = _WebHostEnvironment.WebRootPath + movie.ImagePath;
                System.IO.File.Delete(imgPath);
            }

            _context.Movie.Remove(movie);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }

        [HttpGet]
        public IActionResult AddActorsToMovie(int id)
        {
            Movie movie = _context.Movie.Include(m => m.ActtorsIn).FirstOrDefault(m => m.MovieId == id);
            ViewBag.Actors = _context.Actor.ToList().Except(movie.ActtorsIn);
            return View("AddActors", movie);
        }

        [HttpPost]
        public IActionResult SaveMovieActors(Movie mv, int[] Actorsid)
        {
            Movie movie = _context.Movie.Include(m => m.ActtorsIn).FirstOrDefault(m => m.MovieId == mv.MovieId);
            foreach (int id in Actorsid)
            {
                movie.ActtorsIn.Add(_context.Actor.FirstOrDefault(a => a.Id == id));
            }
            _context.SaveChanges();
            return RedirectToAction("GetEditView", new { id = movie.MovieId });
        }

        [HttpGet]
        public IActionResult DeleteActorsToMovie(int MvId, int AcId)
        {
            Movie movie = _context.Movie.Include(m => m.ActtorsIn).FirstOrDefault(m => m.MovieId == MvId);
            Actor actor = _context.Actor.FirstOrDefault(a => a.Id == AcId);
            movie.ActtorsIn.Remove(actor);
            _context.SaveChanges();
            return RedirectToAction("GetEditView", new { id = MvId });
        }
    }
}
