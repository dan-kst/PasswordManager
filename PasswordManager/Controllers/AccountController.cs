using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Services;

namespace PasswordManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly ClientContext _context;
        private readonly ClientServices _clientServices;

        public AccountController(ClientContext context)
        {
            _context = context;
            _clientServices = new ClientServices(context);
        }
        [Route("[controller]")]
        public IActionResult Index()
        {
            //ClaimsPrincipal user = HttpContext.User;
            return View();
        }


        [Route("[controller]/Login")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginAccount()
        {
            return View();
        }

        [Route("[controller]/Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAccount(ClientBase client)
        {
            // Logic to authenticate the user
            if (ModelState.IsValid)
            {
                var foundClient = await _clientServices.GetClientAsync(client);

                if (foundClient != null)
                {
                    await Authenticate(foundClient);
                    Console.WriteLine($"\n\n{foundClient.Name} {foundClient.Email} {foundClient.MasterPassword} {foundClient.ClientType}\n\n");
                    //If successful, redirect to a different action
                    return RedirectToAction("Index", "Passwords");
                }
                else
                    ModelState.AddModelError("", "Wrong email or password.");
            }
            // If authentication fails, return to the login view with an error message
            return View(client);
        }


        [Route("[controller]/Register")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterAccount()
        {
            return View();
        }

        [Route("[controller]/Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAccount(User user)
        {
            if (ModelState.IsValid)
            {
                var foundClient = await _clientServices.GetUserAsync(user);

                if (foundClient != null)
                {
                    ModelState.AddModelError("", "A client with this email already exists.");
                    return View(user);
                }
                else
                {
                    _context.Users.Add(user);
                }

                await _context.SaveChangesAsync();
                await Authenticate(user);

                //If successful, redirect to a different action
                return RedirectToAction("Index", "Passwords");
            }

            // If authentication fails, return to the login view with an error message
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("PasswordManagerAuth");
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(ClientBase client)
        {
            var principal = _clientServices.AuthenticateClient(client);
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
