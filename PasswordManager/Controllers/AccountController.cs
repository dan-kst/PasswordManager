using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Contexts;
using PasswordManager.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace PasswordManager.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ClientContext _context;

        public AccountController(ClientContext context)
        {
            _context = context;
        }
        [Route("[controller]")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("[controller]/Login")]


        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginAccount()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAccount(ClientBase client)
        {
            // Logic to authenticate the user
            if (ModelState.IsValid && _context.Clients != null)
            {
                var foundClient = await _context.Clients
                    .Where(c => 
                        c.Name.Equals(client.Name) && 
                        c.Email.Equals(client.Email) && 
                        c.MasterPassword.Equals(client.MasterPassword)
                        )
                    .FirstOrDefaultAsync();

                if (foundClient != null)
                {
                    await Authenticate(foundClient);

                    //If successful, redirect to a different action
                    return RedirectToAction("Index", "Passwords");
                }
            }

            // If authentication fails, return to the login view with an error message
            return View(client);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterAccount()
        {
            return View();
        }
        [Route("[controller]/Register")]

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAccount(ClientBase client)
        {
            // Logic to authenticate the user
            if (ModelState.IsValid && _context.Clients != null)
            {
                var foundClient = await _context.Clients
                    .Where(c => c.Email.Equals(client.Email))
                    .FirstOrDefaultAsync();

                if (foundClient != null)
                {
                    ModelState.AddModelError("", "A client with this email already exists.");
                    return View(client);
                }

                if (client is Admin admin)
                {
                    _context.Admins.Add(admin);
                }
                else if (client is User user)
                {
                    _context.Users.Add(user);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid client type.");
                    return View(client);
                }

                await _context.SaveChangesAsync();
                await Authenticate(client);

                //If successful, redirect to a different action
                return RedirectToAction("Index", "Passwords");
            }

            // If authentication fails, return to the login view with an error message
            return View(client);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(ClientBase client)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, client.Name),
                new Claim(ClaimTypes.Email, client.Email),
                new Claim(ClaimTypes.Role, client.ClientType.ToString()),
                new Claim(ClaimTypes.NameIdentifier, client.Id.ToString())
            };
            var identity = new ClaimsIdentity(claims, "PasswordManagerAuth");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("PasswordManagerAuth", principal);
        }


        [HttpGet]
        [Route("[controller]/Edit/{id}")]
        public IActionResult EditAccount(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditAccount(ClientBase client)
        {
            // Logic to authenticate the user
            if (ModelState.IsValid)
            {
                // If successful, redirect to a different action
                return RedirectToAction("Index", "Account");
            }
            else
            {
                // If authentication fails, return to the login view with an error message
                return View(client);
            }
        }
        [HttpGet]
        [Route("[controller]/Delete/{id}")]
        public IActionResult DeleteAccount()
        {
            return View();
        }
        [HttpPost]
        [Route("[controller]/Delete/{id}/Confirm")]
        public IActionResult DeleteAccount(int id, ClientBase client)
        {
            return View();
        }
    }
}
