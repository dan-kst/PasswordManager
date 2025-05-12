using Microsoft.AspNetCore.Mvc;
using PasswordManager.Contexts;

namespace PasswordManager.Controllers
{
    public class AdminController : Controller
    {
        private readonly ClientContext _context;

        public AdminController(ClientContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        public IActionResult EditUser()
        {
            return View();
        }
        public IActionResult DeleteUser()
        {
            return View();
        }
        public IActionResult ViewUser()
        {
            return View();
        }
        public IActionResult SearchUser()
        {
            return View();
        }
    }
}
