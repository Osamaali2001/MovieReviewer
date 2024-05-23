using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Data;
using MovieReviewer.Models;

namespace MovieReviewer.Controllers
{
    public class DirectorsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _WebHostEnvironment;
        public DirectorsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetIndexView(string? search)
        {
            List<Director> directors;
            ViewBag.DirectorsSearch = search;
            if (search == null)
            {
                directors = _context.Director.ToList();
            }
            else
            {
                directors = _context.Director.Where(dir => (dir.FirstName.ToLower() + dir.LastName.ToLower()).Contains(search.ToLower())).ToList();
            }
            return View("Index", directors);
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Director director = _context.Director.Include(d => d.MoviesDirected).FirstOrDefault(x => x.Id == id);
            return View("Details", director);
        }

        [HttpGet]
        public IActionResult GetCreateView()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNew(Director director, IFormFile? ImageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFormFile != null)
                {
                    string imgExtension = Path.GetExtension(ImageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    director.ImagePath = imgPath;
                    string imgFullPath = _WebHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    ImageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                else
                {
                    director.ImagePath = "\\images\\No_Image.png";
                }

                _context.Director.Add(director);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
                return View("Create");
        }

        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Director director = _context.Director.FirstOrDefault(d => d.Id == id);
            return View("Edit", director);
        }

        [HttpPost]
        public IActionResult EditCurrent(Director director, IFormFile? ImageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFormFile != null)
                {
                    if (director.ImagePath != "\\images\\No_Image.png")
                    {
                        string oldImgPath = _WebHostEnvironment.WebRootPath + director.ImagePath;
                        System.IO.File.Delete(oldImgPath);
                    }
                    string imgExtension = Path.GetExtension(ImageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    director.ImagePath = imgPath;
                    string imgFullPath = _WebHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    ImageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                _context.Director.Update(director);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit", director);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Director director = _context.Director.Include(d => d.MoviesDirected).FirstOrDefault(dep => dep.Id == id);
            return View("Delete", director);
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Director director = _context.Director.Include(d => d.MoviesDirected).ThenInclude(m => m.ActtorsIn).FirstOrDefault(d => d.Id == id);
            foreach (Movie movie in director.MoviesDirected)
            {
                if (movie.ImagePath != "\\images\\No_Image.png")
                {
                    string imgPath = _WebHostEnvironment.WebRootPath + movie.ImagePath;
                    System.IO.File.Delete(imgPath);
                }
                movie.ActtorsIn.Clear();
                _context.Movie.Remove(movie);
            }
            if (director.ImagePath != "\\images\\No_Image.png")
            {
                string imgPath = _WebHostEnvironment.WebRootPath + director.ImagePath;
                System.IO.File.Delete(imgPath);
            }

            _context.Director.Remove(director);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }
    }
}
