using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PasswordManager.Contexts;
using PasswordManager.Models;
using PasswordManager.Models.Classes.Secrets;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Models.Enums;
using PasswordManager.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace PasswordManager.Controllers
{
    [Authorize(Policy = "User", AuthenticationSchemes = "PasswordManagerAuth")]
    public class PasswordsController : Controller
    {
        private readonly SecretService _secretService;
        private readonly ClientService _clientService;
        private User? user;

        public PasswordsController(ClientService clientService, SecretService secretServices, ClientContext clientContext)
        {
            _secretService = secretServices;
            _clientService = clientService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(idClaim, out var id))
                {
                    user = _clientService.GetClientByIdAsync(id).Result as User;
                }
            }
            base.OnActionExecuting(context);
        }

        public async Task<IActionResult> Index()
        {
            if(user != null)
            {
                return View(await _secretService.GetSecretsAsync(user.Id));
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "User Error" });
            }
        }


        [HttpGet]
        public IActionResult GetType(SecretBase secret)
        {
            switch (secret.SecretType)
            {
                case EnumSecretType.Pincode:
                    return RedirectToAction("CreatePincode");
                case EnumSecretType.SitePassword:
                    return RedirectToAction("CreateSitePassword");
                default:
                    return RedirectToAction("CreatePincode");
            }
        }



        [HttpGet]
        [Route("[controller]/Create/SitePassword")]
        public  IActionResult CreateSitePassword()
        {
            return View("Create/CreateSitePassword", new SitePassword());
        }

        [HttpPost]
        [Route("[controller]/Create/SitePassword")]
        public async Task<IActionResult> CreateSitePassword(SitePassword sitePassword)
        {
            if (ModelState.IsValid && user != null)
            {
                sitePassword.ClientId = user.Id;
                var foundsecret = await _secretService.CheckSecretAsync(sitePassword);
                if(foundsecret)
                {
                    ModelState.AddModelError("", "Secret already exists.");
                    return View("Create/CreateSitePassword", sitePassword);
                }
                else
                {
                    if (await _secretService.AddSecretAsync(sitePassword))
                    {
                        user.PasswordsCreated++;
                        await _clientService.UpdateClientAsync(user);
                        return RedirectToAction("Index", "Passwords");
                    }
                    else
                        return View("Error", new ErrorViewModel { RequestId = "CreateSitePassword Error" });
                }
            }
            else
            {
                return View("Create/CreateSitePassword", sitePassword);
            }
        }



        [HttpGet]
        [Route("[controller]/Create/Pincode")]
        public IActionResult CreatePincode()
        {
            return View("Create/CreatePincode", new Pincode());
        }

        [HttpPost]
        [Route("[controller]/Create/Pincode")]
        public async Task<IActionResult> CreatePincode(Pincode pincode)
        {
            if (ModelState.IsValid && user != null)
            {
                pincode.ClientId = user.Id;
                var foundsecret = await _secretService.CheckSecretAsync(pincode);
                if (foundsecret)
                {
                    ModelState.AddModelError("", "Secret already exists.");
                    return View("Create/CreatePincode", pincode);
                }
                else
                {
                    if (await _secretService.AddSecretAsync(pincode))
                    {
                        user.PasswordsCreated++;
                        await _clientService.UpdateClientAsync(user);
                        return RedirectToAction("Index", "Passwords");
                    }
                    else
                        return View("Error", new ErrorViewModel { RequestId = "CreatePincode Error" });
                }
            }
            else
            {
                return View("Create/CreatePincode", pincode);
            }
        }



        [HttpGet]
        [Route("[controller]/Edit/{id}")] 
        public async Task<IActionResult> EditPassword(int id)
        {
            if (user != null)
            {
                var secret = await _secretService.GetSecretAsync(id, user.Id);
                if (secret == null)
                {
                    return View("Error", new ErrorViewModel { RequestId = "EditPassword Error" });
                }
                else
                    switch (secret.SecretType)
                    {
                        case EnumSecretType.Pincode:
                            return View("Edit/EditPincode", secret as Pincode);
                        case EnumSecretType.SitePassword:
                            return View("Edit/EditSitePassword", secret as SitePassword);
                        default:
                            return View("Edit/EditPincode", secret as Pincode);
                    }
            }
            else
                return View("Error", new ErrorViewModel { RequestId = "User Error" });
        }

        [HttpPost]
        public async Task<IActionResult> EditSitePassword(SitePassword sitePassword)
        {
            if (ModelState.IsValid && user != null)
            {
                if(await _secretService.UpdateSecretAsync(sitePassword))
                {
                    user.PasswordsUpdated++;
                    await _clientService.UpdateClientAsync(user);
                    return RedirectToAction("View", "Passwords", new { id = sitePassword.Id } );
                }
                else
                    return View("Error", new ErrorViewModel { RequestId = "EditSitePassword Error" });
            }
            else
            {
                ModelState.AddModelError("", "Secret error.");
                return RedirectToAction("EditPassword", "Passwords", sitePassword.Id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditPincode(Pincode pincode)
        {
            if (ModelState.IsValid && user != null)
            {
                if(await _secretService.UpdateSecretAsync(pincode))
                {
                    user.PasswordsUpdated++;
                    await _clientService.UpdateClientAsync(user);
                    return RedirectToAction("View", "Passwords", new { id = pincode.Id });
                }
                else
                    return View("Error", new ErrorViewModel { RequestId = "EditPincode Error" });
            }
            else
            {
                ModelState.AddModelError("", "Secret error.");
                return RedirectToAction("EditPassword", "Passwords", pincode.Id);
            }
        }



        [HttpGet]
        [Route("[controller]/Delete/{id}")]
        public async Task<IActionResult> DeletePassword(int id)
        {
            if (user != null)
            {
                var secret = await _secretService.GetSecretAsync(id, user.Id);
                if(secret == null)
                    return View("Error", new ErrorViewModel { RequestId = "DeletePassword Error" });
                else
                    return View(secret);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "User Error" });
            }

        }
        [HttpPost]
        [Route("[controller]/Delete/Confirm")]
        public async Task<IActionResult> DeletePasswordConfirm(SecretBase secretBase)
        {
            if(user != null)
            {
                if(await _secretService.DeleteSecretAsync(secretBase.Id, user.Id))
                {
                    user.PasswordsDeleted++;
                    await _clientService.UpdateClientAsync(user);
                    return RedirectToAction("Index", "Passwords");
                }
                else
                    return View("Error", new ErrorViewModel { RequestId = "DeletePassword Confirm Error" });
            }
            else
                return View("Error", new ErrorViewModel { RequestId = "User Error" });
        }



        [Route("[controller]/View/{id}")]
        public async Task<IActionResult> ViewPassword(int id)
        {
            if (user != null)
            {
                var secret = await _secretService.GetSecretAsync(id, user.Id);
                if (secret == null)
                {
                    return View("Error", new ErrorViewModel { RequestId = "SecretNotFound" });
                }
                else
                    switch (secret.SecretType)
                    {
                        case EnumSecretType.Pincode:
                            return View("View/ViewPincode", secret);
                        case EnumSecretType.SitePassword:
                            return View("View/ViewSitePassword", secret);
                        default:
                            return View("View/ViewPincode", secret);
                    }
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "User Error" });
            }
        }
    }
}
