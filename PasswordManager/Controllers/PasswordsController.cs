using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.Controllers
{
    public class PasswordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreatePassword()
        {
            return View();
        }
        public IActionResult EditPassword()
        {
            return View();
        }
        public IActionResult DeletePassword()
        {
            return View();
        }
        public IActionResult ViewPassword()
        {
            return View();
        }
        public IActionResult SearchPassword()
        {
            return View();
        }
    }
}
