using Microsoft.AspNetCore.Mvc;
using PasswordManager.Contexts;

namespace PasswordManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly ClientContext _context;

        public AccountController(ClientContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginAccount()
        {
            return View();
        }
        public IActionResult RegisterAccount()
        {
            return View();
        }
        public IActionResult EditAccount()
        {
            return View();
        }
        public IActionResult DeleteAccount()
        {
            return View();
        }
    }
}
