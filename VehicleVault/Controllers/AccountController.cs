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

        // 🔹 LOGIN GET
        public IActionResult Login()
        {
            return View();
        }

        // 🔹 LOGIN POST
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(x =>
                    x.Username.Trim().ToLower() == username.Trim().ToLower() &&
                    x.Password.Trim() == password.Trim());

            if (user != null)
            {
                HttpContext.Session.SetString("user", user.Username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid login";
            return View();
        }

        // 🔹 REGISTER GET
        public IActionResult Register()
        {
            return View();
        }

        // 🔹 REGISTER POST
        [HttpPost]
        public IActionResult Register(User u)
        {
            _context.Users.Add(u);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // 🔹 LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}