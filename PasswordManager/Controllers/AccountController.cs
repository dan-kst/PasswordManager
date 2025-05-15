using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Services;

namespace PasswordManager.Controllers
{
    [Authorize(AuthenticationSchemes = "PasswordManagerAuth")]
    public class AccountController : Controller
    {
        private readonly ClientService _clientServices;

        public AccountController(ClientService clientService)
        {
            _clientServices = clientService;
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
            if (ModelState.IsValid)
            {
                var foundClient = await _clientServices.GetClientAsync(client);

                if (foundClient != null)
                {
                    await Authenticate(foundClient);
                    return RedirectToAction("Index", "Passwords");
                }
                else
                    ModelState.AddModelError("", "Wrong email or password.");
            }

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
        public async Task<IActionResult> RegisterAccount(User client)
        {
            if (ModelState.IsValid)
            {
                var foundClient = await _clientServices.GetClientByEmailAsync(client.Email);

                if (foundClient != null)
                {
                    ModelState.AddModelError("", "A client with this email already exists.");
                    return View(client);
                }
                else
                {
                    if(await _clientServices.AddClientAsync(client))
                        await Authenticate(client);
                    else
                    {
                        ModelState.AddModelError("", "Register Error.");
                        return View(client);
                    }
                }


                return RedirectToAction("Index", "Passwords");
            }

            return View(client);
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
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Account");
            }
            else
            {
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
