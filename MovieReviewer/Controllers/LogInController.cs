using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Data;
using MovieReviewer.Models;

namespace MovieReviewer.Controllers
{
    public class LogInController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _WebHostEnvironment;
        public LogInController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }
        public ActionResult GetLoginView()
        {
            return View("Login");
        }

        // GET: Login 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Password)
        {
            User user = _context.User.FirstOrDefault(a => a.Email.Equals(Email) && a.Password.Equals(Password));
            if (user != null)
            {
                HttpContext.Session.SetString("Role",user.IsAdmin.ToString());
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
                return View("Login");
        }

        public IActionResult LogOut() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult GetCreateAccountView()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(User user, IFormFile? ImageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFormFile != null)
                {
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
                else
                {
                    user.ImagePath = "\\images\\No_Image.png";
                }
                user.IsAdmin = false;
                _context.User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("GetLoginView", "Login");
            }
            return View("Create");
        }
    }
}
