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

        // 🔒 SESSION CHECK METHOD
        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("user") != null;
        }

        // HOME PAGE
        public IActionResult Index(string search, string fuel)
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var vehicles = _context.Vehicles.AsQueryable();

            // 🔍 SEARCH (by name)
            if (!string.IsNullOrEmpty(search))
            {
                vehicles = vehicles.Where(v => v.Name.Contains(search));
            }

            // 🔽 FILTER (by fuel)
            if (!string.IsNullOrEmpty(fuel))
            {
                vehicles = vehicles.Where(v => v.Fuel == fuel);
            }

            return View(vehicles.ToList());
        }

        // CREATE PAGE
        public IActionResult Create()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(Vehicle v)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            _context.Vehicles.Add(v);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var v = _context.Vehicles.Find(id);
            if (v != null)
            {
                _context.Vehicles.Remove(v);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var v = _context.Vehicles.Find(id);
            return View(v);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Vehicle v)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            _context.Vehicles.Update(v);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // COMPARE PAGE
        public IActionResult Compare()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var vehicles = _context.Vehicles.ToList();
            return View(vehicles);
        }

        // DASHBOARD
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var vehicles = _context.Vehicles.ToList();

            ViewBag.Total = vehicles.Count;

            ViewBag.AvgPrice = vehicles.Count > 0 ? vehicles.Average(v => v.Price) : 0;

            var bestMileage = vehicles.OrderByDescending(v => v.Mileage).FirstOrDefault();
            ViewBag.BestMileage = bestMileage != null ? bestMileage.Name : "N/A";

            ViewBag.ElectricCount = vehicles.Count(v => v.Fuel == "Electric");
            ViewBag.PetrolCount = vehicles.Count(v => v.Fuel == "Petrol");

            return View();
        }
        // COMPARE RESULT
        [HttpPost]
        public IActionResult Compare(int id1, int id2)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var v1 = _context.Vehicles.Find(id1);
            var v2 = _context.Vehicles.Find(id2);

            ViewBag.V1 = v1;
            ViewBag.V2 = v2;

            return View("CompareResult");
        }
    }
}