using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Services;
using PasswordManager.Models.Enums;
using PasswordManager.Models;

namespace PasswordManager.Controllers
{
    [Authorize(AuthenticationSchemes = "PasswordManagerAuth")]
    public class AccountController : Controller
    {
        private readonly ClientService _clientService;
        public AccountController(ClientService clientService)
        {
            _clientService = clientService;
        }


        [Route("[controller]/View/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            if (id != -1)
            {
                var client = await _clientService.GetClientByIdAsync(id);
                if (client == null)
                {
                    return View("Error", new ErrorViewModel { RequestId = "ClientNotFound" });
                }
                else
                    switch (client.ClientType)
                    {
                        case EnumClientType.Admin:
                            return View("View/ViewAdmin", client);
                        case EnumClientType.Personal:
                            return View("View/ViewUser", client);
                        default:
                            return View("View/ViewUser", client);
                    }
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "ClientId is -1" });
            }
        }


        [Route("[controller]/Login")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginAccount()
        {
            return View(new ClientBase());
        }

        [Route("[controller]/Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAccount(ClientBase client)
        {
            if (ModelState.IsValid)
            {
                var foundClient = await _clientService.GetClientAsync(client);

                if (foundClient != null)
                {
                    await Authenticate(foundClient);
                    if(foundClient.ClientType == EnumClientType.Admin)
                        return RedirectToAction("Index", "Clients");
                    else if (foundClient.ClientType == EnumClientType.Personal)
                        return RedirectToAction("Index", "Passwords");
                    else
                        return RedirectToAction("Index", "Home");
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
            return View(new User());
        }

        [Route("[controller]/Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAccount(User client)
        {
            if (ModelState.IsValid)
            {
                var foundClient = await _clientService.GetClientByEmailAsync(client.Email);

                if (foundClient != null)
                {
                    ModelState.AddModelError("", "A client with this email already exists.");
                    return View(client);
                }
                else
                {
                    if(await _clientService.AddClientAsync(client))
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
            var principal = _clientService.AuthenticateClient(client);
            await HttpContext.SignInAsync("PasswordManagerAuth", principal);
        }


        [HttpGet]
        [Route("[controller]/Edit/{id}")]
        public async Task<IActionResult> EditAccount(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client != null)
            {
                switch (client.ClientType)
                {
                    case EnumClientType.Admin:
                        return View("Edit/EditAdmin", client as Admin);
                    case EnumClientType.Personal:
                        return View("Edit/EditUser", client as User);
                    default:
                        return View("Edit/EditUser", client as User);
                }
            }
            else
                return View("Error", new ErrorViewModel { RequestId = "EditAccount Get Error" });
        }
        [HttpPost]
        public async Task<IActionResult> EditAccountConfirm(ClientBase client)
        {
            if (ModelState.IsValid)
            {
                if (await _clientService.UpdateClientAsync(client))
                    return RedirectToAction("Index", "Account", new { id = client.Id });
                else
                    return View("Error", new ErrorViewModel { RequestId = "EditAccount Post Error" });
            }
            else
            {
                ModelState.AddModelError("", "Secret error.");
                return RedirectToActionPermanent("EditAccount", new { client.Id });
            }
        }
        [HttpGet]
        [Route("[controller]/Delete/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var secret = await _clientService.GetClientByIdAsync(id);
            if (secret == null)
                return View("Error", new ErrorViewModel { RequestId = "DeleteAccount Error" });
            else
                return View(secret);

        }

        [HttpPost]
        [Route("[controller]/Delete/Confirm")]
        public async Task<IActionResult> DeleteAccountConfirm(ClientBase client)
        {
            if(await _clientService.DeleteClientByIdAsync(client.Id))
            {
                return await Logout();
            }
            else
                return View("Error", new ErrorViewModel { RequestId = "DeleteAccount Confirm Error" });
        }
    }
}
