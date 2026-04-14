using Microsoft.AspNetCore.Mvc;
using VehicleVault.Data;
using VehicleVault.Models;
using System.Linq;

namespace VehicleVault.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 LIST
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("user") == null)
                return RedirectToAction("Login", "Account");

            var vehicles = _context.Vehicles.ToList();
            return View(vehicles);
        }

        // 🔹 CREATE GET
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("user") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // 🔹 CREATE POST
        [HttpPost]
        public IActionResult Create(Vehicle v)
        {
            if (HttpContext.Session.GetString("user") == null)
                return RedirectToAction("Login", "Account");

            _context.Vehicles.Add(v);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 🔹 DELETE
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("user") == null)
                return RedirectToAction("Login", "Account");

            var vehicle = _context.Vehicles.Find(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // 🔹 EDIT GET
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("user") == null)
                return RedirectToAction("Login", "Account");

            var vehicle = _context.Vehicles.Find(id);
            return View(vehicle);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("user", user.Username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid login";
            return View();
        }

        // COMPARE PAGE
        public IActionResult Compare()
        {
            var vehicles = _context.Vehicles.ToList();
            return View(vehicles);
        }

        // COMPARE RESULT
        [HttpPost]
        public IActionResult Compare(int id1, int id2)
        {
            var v1 = _context.Vehicles.Find(id1);
            var v2 = _context.Vehicles.Find(id2);

            ViewBag.V1 = v1;
            ViewBag.V2 = v2;

            return View("CompareResult");
        }
        // 🔹 EDIT POST
        [HttpPost]
        public IActionResult Edit(Vehicle v)
        {
            if (HttpContext.Session.GetString("user") == null)
                return RedirectToAction("Login", "Account");

            _context.Vehicles.Update(v);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}