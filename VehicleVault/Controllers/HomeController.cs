using Microsoft.AspNetCore.Mvc;
using VehicleVault.Data;
using VehicleVault.Models;
using System.Linq;

namespace VehicleVault.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Vehicles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vehicle v, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                v.ImagePath = "/images/" + fileName;
            }

            _context.Vehicles.Add(v);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_context.Vehicles.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Vehicle v, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                v.ImagePath = "/images/" + fileName;
            }

            _context.Vehicles.Update(v);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var v = _context.Vehicles.Find(id);
            _context.Vehicles.Remove(v);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}