using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PasswordManager.Contexts;
using PasswordManager.Models;
using PasswordManager.Models.Classes.Secrets;
using PasswordManager.Models.Enums;
using PasswordManager.Services;

namespace PasswordManager.Controllers
{
    [Authorize(Policy = "User", AuthenticationSchemes = "PasswordManagerAuth")]
    public class PasswordsController : Controller
    {
        private readonly ClientContext _clientContext;
        private readonly SecretService _secretServices;
        private int _clientId;

        public PasswordsController(SecretService secretServices, ClientContext clientContext)
        {
            _secretServices = secretServices;
            _clientContext = clientContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(idClaim, out var id))
                    _clientId = id;
                else
                    _clientId = -1;
            }
            else
            {
                _clientId = -1;
            }
            base.OnActionExecuting(context);
        }


        public async Task<IActionResult> Index(string filterValue)
        {
            List<SecretBase>? passwords = null;
            if (ViewData["CurrentFilter"] != null)
                passwords = await _secretServices.GetFilteredSecretsAsync((string)ViewData["CurrentFilter"]);
            else
                passwords = await _secretServices.GetSecretsAsync(_clientId);
            
            return View(passwords);
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
            if (ModelState.IsValid)
            {
                sitePassword.ClientId = _clientId;
                var foundsecret = await _secretServices.CheckSecretAsync(sitePassword);
                if(foundsecret)
                {
                    ModelState.AddModelError("", "Secret already exists.");
                    return View("Create/CreateSitePassword", sitePassword);
                }
                else
                {
                    await _secretServices.AddSecret(sitePassword);
                    return RedirectToAction("Index", "Passwords");
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
            if (ModelState.IsValid)
            {
                pincode.ClientId = _clientId;
                var foundsecret = await _secretServices.CheckSecretAsync(pincode);
                if (foundsecret)
                {
                    ModelState.AddModelError("", "Secret already exists.");
                    return View("Create/CreatePincode", pincode);
                }
                else
                {
                    await _secretServices.AddSecret(pincode);
                    return RedirectToAction("Index", "Passwords");
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
            var secret = await _secretServices.GetSecretAsync(id, _clientId);
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

        [HttpPost]
        public async Task<IActionResult> EditSitePassword(SitePassword sitePassword)
        {
            if (ModelState.IsValid)
            {
                if(await _secretServices.UpdateSecret(sitePassword))
                    return RedirectToAction("View", "Passwords", new { id = sitePassword.Id } );
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
            if (ModelState.IsValid)
            {
                if(await _secretServices.UpdateSecret(pincode))
                    return RedirectToAction("View", "Passwords", new { id = pincode.Id });
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
            var secret = await _secretServices.GetSecretAsync(id, _clientId);
            if(secret == null)
                return View("Error", new ErrorViewModel { RequestId = "DeletePassword Error" });
            else
                return View(secret);

        }
        [HttpPost]
        [Route("[controller]/Delete/Confirm")]
        public async Task<IActionResult> DeletePasswordConfirm(SecretBase secretBase)
        {
            await _secretServices.DeleteSecret(secretBase.Id, _clientId);
            return RedirectToAction("Index", "Passwords");
        }


        [Route("[controller]/View/{id}")]
        public async Task<IActionResult> ViewPassword(int id)
        {
            var secret = await _secretServices.GetSecretAsync(id, _clientId);
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
        [HttpGet]
        public IActionResult SearchPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("[controller]/SearchResult/{searchName}")]
        public IActionResult SearchPassword(string searchName)
        {
            return View();
        }
    }
}
