using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Web.Examen.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Web.Examen.Client.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Web.Examen.Client.Controllers
{
   
    public class AccountController : Controller
    {
        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() {RedirectUri=returnUrl });
        }
        [Authorize]

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index","Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View(new UserViewModel
            {
               
                UserName = User.Identity.Name,
                EMailAdress= User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                UserRoles=User.IsInRole("Admin"),
                pictures= User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }
        //redireige l'utilisateur vers cette page si il n'a pas l'authorization
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
