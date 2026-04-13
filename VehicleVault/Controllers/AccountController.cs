using Microsoft.AspNetCore.Mvc;
using VehicleVault.Data;
using VehicleVault.Models;
using System.Linq;

namespace VehicleVault.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User u)
        {
            _context.Users.Add(u);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User u)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Username == u.Username && x.Password == u.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("user", user.Username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid login";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}