using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PasswordManager.Models;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Models.Enums;
using PasswordManager.Models.Validation;
using PasswordManager.Services;

namespace PasswordManager.Controllers
{
    [Authorize(Policy = "Admin", AuthenticationSchemes = "PasswordManagerAuth")]
    public class ClientsController : Controller
    {
        private readonly ClientService _clientService;
        private Admin? _admin;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(idClaim, out var id))
                {
                    _admin = _clientService.GetClientByIdAsync(id).Result as Admin;
                }
            }
            base.OnActionExecuting(context);
        }

        public async Task<IActionResult> Index()
        {
            if (_admin != null)
            {
                return View(await _clientService.GetClientsAsync(_admin.Id));
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "Admin Error" });
            }
        }


        [HttpGet]
        [Route("[controller]/Create/Admin")]
        [AllowAnonymous]
        public IActionResult CreateAdmin([FromQuery] string? key)
        {
            if ((!string.IsNullOrEmpty(key) && key.Equals(AdminValidation.ADMIN_KEY)) || User.IsInRole("Admin"))
            {
                return View("Create/CreateAdmin", new Admin());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Route("[controller]/Create/User")]
        public IActionResult CreateUser()
        {
            return View("Create/CreateUser", new User());
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("[controller]/Create/Admin")]
        public async Task<IActionResult> CreateAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var foundsecret = await _clientService.CheckClientAsync(admin);
                if (foundsecret)
                {
                    ModelState.AddModelError("", "This admin already exists.");
                    return View("Create/CreateAdmin", admin);
                }
                else
                {
                    if (await _clientService.AddClientAsync(admin))
                    {
                        return RedirectToAction("Index", "Clients");
                    }
                    else
                        return View("Error", new ErrorViewModel { RequestId = "DeletePassword Confirm Error" });
                }
            }
            else
            {
                return View("Create/CreateAdmin", admin);
            }
        }

        [HttpPost]
        [Route("[controller]/Create/User")]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                var foundsecret = await _clientService.CheckClientAsync(user);
                if (foundsecret)
                {
                    ModelState.AddModelError("", "This user already exists.");
                    return View("Create/CreateUser", user);
                }
                else
                {
                    if (await _clientService.AddClientAsync(user))
                    {
                        _admin.UsersCreated++;
                        await _clientService.UpdateClientAsync(user);
                        return RedirectToAction("Index", "Clients");
                    }
                    else
                        return View("Error", new ErrorViewModel { RequestId = "DeletePassword Confirm Error" });
                }
            }
            else
            {
                return View("Create/CreateUser", user);
            }
        }



        [HttpGet]
        [Route("[controller]/Edit/{id}")]
        public IActionResult EditClient(int id)
        {
            return View("../Account/EditAccount", new { id });
        }
        [HttpPost]
        public async Task<IActionResult> EditClient(ClientBase client)
        {
            if (ModelState.IsValid && _admin != null)
            {
                switch (client.ClientType)
                {
                    case EnumClientType.Admin:
                        if (!await _clientService.UpdateClientAsync(client as Admin))
                            return View("Error", new ErrorViewModel { RequestId = "EditClient Admin Error" });
                        break;
                    case EnumClientType.Personal:
                        if (await _clientService.UpdateClientAsync(client as User))
                        {
                            _admin.UsersUpdated++;
                            await _clientService.UpdateClientAsync(_admin);
                        }
                        else
                            return View("Error", new ErrorViewModel { RequestId = "EditClient User Error" });
                        break;
                    default:
                        return View("Error", new ErrorViewModel { RequestId = "DeletePassword None Error" });
                }

                return RedirectToAction("ViewClient", "Clients", new { client.Id });
            }
            else
            {
                return View(client);
            }
        }



        [HttpGet]
        [Route("[controller]/Delete/{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            return View(await _clientService.GetClientByIdAsync(id));
        }
        [HttpPost]
        [Route("[controller]/Delete/Confirm")]
        public async Task<IActionResult> DeleteClientConfirm(ClientBase client)
        {
            if(await _clientService.DeleteClientByIdAsync(client.Id))
            {
                _admin.UsersDeleted++;
                await _clientService.UpdateClientAsync(_admin);
                return RedirectToAction("Index", "Clients");
            }
            else
                return View("Error", new ErrorViewModel { RequestId = "EditClient User Error" });
        }



        [HttpGet]
        [Route("[controller]/View/{id}")]
        public IActionResult ViewClient(int id)
        {
            return RedirectToActionPermanent("Index", "Account", new { id });
        }
    }
}
