using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Data;
using MovieReviewer.Models;

namespace MovieReviewer.Controllers
{
    public class ActorsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _WebHostEnvironment;
        public ActorsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetIndexView(string? search)
        {
            List<Actor> actors;
            ViewBag.ActorsSearch = search;
            if(search == null)
            {
                actors = _context.Actor.ToList();
            }
            else
            {
                actors = _context.Actor.Where(act => (act.FirstName.ToLower() + act.LastName.ToLower()).Contains(search.ToLower())).ToList();
            }
            return View("Index",actors);
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Actor actor = _context.Actor.Include(d => d.MoviesIn).FirstOrDefault(x => x.Id == id);
            if (actor == null)
                return NotFound();
            else
                return View("Details", _context.Actor.FirstOrDefault(ac => ac.Id == id));
        }

        [HttpGet]
        public IActionResult GetCreateView()
        {
            ViewBag.AllMovies = _context.Movie.ToList();
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNew(Actor actor, IFormFile? ImageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFormFile != null)
                {
                    string imgExtension = Path.GetExtension(ImageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    actor.ImagePath= imgPath;
                    string imgFullPath = _WebHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    ImageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                else
                {
                    actor.ImagePath = "\\images\\No_Image.png";
                }

                _context.Actor.Add(actor);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
                return View("Create");
        }

        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Actor actor = _context.Actor.FirstOrDefault(d => d.Id == id);
            if (actor == null)
                return NotFound();
            else
                return View("Edit", actor);
        }

        [HttpPost]
        public IActionResult EditCurrent(Actor actor, IFormFile? ImageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFormFile != null)
                {
                    if (actor.ImagePath!= "\\images\\No_Image.png")
                    {
                        string oldImgPath = _WebHostEnvironment.WebRootPath + actor.ImagePath;
                        System.IO.File.Delete(oldImgPath);
                    }
                    string imgExtension = Path.GetExtension(ImageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    actor.ImagePath = imgPath;
                    string imgFullPath = _WebHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    ImageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                _context.Actor.Update(actor);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit", actor);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Actor actor = _context.Actor.Include(d => d.MoviesIn).FirstOrDefault(dep => dep.Id == id);
            return View("Delete", actor);
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Actor actor= _context.Actor.Include(a=>a.MoviesIn).FirstOrDefault(d => d.Id == id);
            actor.MoviesIn.Clear();
            if (actor.ImagePath!= "\\images\\No_Image.png")
            {
                string imgPath = _WebHostEnvironment.WebRootPath + actor.ImagePath;
                System.IO.File.Delete(imgPath);
            }

            _context.Actor.Remove(actor);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }

    }
}
