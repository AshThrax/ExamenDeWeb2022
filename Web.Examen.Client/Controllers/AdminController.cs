using Auth0.ManagementApi.Models;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Configuration;
using Web.Examen.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Security.Cryptography.X509Certificates;

namespace Web.Examen.Client.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
       
        public IActionResult Index()
        {

            return View();
        }

        public AdminController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        public IActionResult DisplayallUser()
        { 
            return View();
        }
        //affiche tout les utilisateur du site internet
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            /*
             
             */
            var Auth0Token = _configuration["Auth0:Token"];
            var AuthDomain = _configuration["Auth0:Domain"]+"api/v2/";
            var apiClient = new ManagementApiClient(Auth0Token,AuthDomain);
            //Get Auth0 Users
            var allUsers = await apiClient.Users.GetAllAsync(new Auth0.ManagementApi.Models.GetUsersRequest(),
                                                             new Auth0.ManagementApi.Paging.PaginationInfo());
            return View(allUsers);
        }
        public async Task<IActionResult> AccesDenied()
        { 
          return View();
        }
    }  
}
    

