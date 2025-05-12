using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Contexts;
using PasswordManager.Models.Classes;

namespace PasswordManager.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ClientsController : Controller
    {
        private readonly ClientContext _context;

        public ClientsController(ClientContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Admin/Create")]
        public IActionResult CreateClient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateClient(ClientBase client)
        {
            // Logic to authenticate the user
            if (ModelState.IsValid)
            {
                // If successful, redirect to a different action
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // If authentication fails, return to the login view with an error message
                return View(client);
            }
        }
        [HttpGet]
        [Route("Admin/Edit/{id}")]
        public IActionResult EditClient(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditClient(ClientBase client)
        {
            // Logic to authenticate the user
            if (ModelState.IsValid)
            {
                // If successful, redirect to a different action
                return RedirectToAction("ViewUser", "Admin");
            }
            else
            {
                // If authentication fails, return to the login view with an error message
                return View(client);
            }
        }
        [HttpGet]
        [Route("Admin/Delete/{id}")]
        public IActionResult DeleteClient(int id)
        {
            return View();
        }
        [HttpPost]
        [Route("Admin/Delete/{id}/Confirm")]
        public IActionResult DeleteClient(int id, ClientBase client)
        {
            return View();
        }
        [HttpGet]
        [Route("Admin/View/{id}")]
        public IActionResult ViewClient()
        {
            return View();
        }
        [HttpGet]
        [Route("Admin/SearchResults")]
        public IActionResult SearchClient()
        {
            return View();
        }
        [HttpPost]
        [Route("Admin/SearchResults/{searchName}")]
        public IActionResult SearchClient(string searchName)
        {
            return View();
        }
    }
}
