using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Contexts;
using PasswordManager.Models;

namespace PasswordManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClientContext _context;

        public HomeController(ClientContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
