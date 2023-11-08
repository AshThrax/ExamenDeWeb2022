using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using Web.Examen.Client.Client;
using Web.Examen.Client.Models;
using Web.Examen.Client.Models.FilmOmdb;

namespace Web.Examen.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOmdbClient _omdbClient;
        
        public HomeController(ILogger<HomeController> logger, IOmdbClient _omdbClient)
        {
            this._omdbClient= _omdbClient;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string accesToken = await HttpContext.GetTokenAsync("acces_token");
                DateTime accessTokenExpireAT = DateTime.Parse(  
                    await  HttpContext.GetTokenAsync("expires_at"),
                    CultureInfo.InvariantCulture,DateTimeStyles.RoundtripKind
                    );
                string idToken = await HttpContext.GetTokenAsync("id_token");
            }
            return View();
        }
        [Authorize(Roles =("Admin"))]
        public IActionResult Claims()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SearchFilm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Submit(FilmOMdb movie)
        {
            var result = await _omdbClient.GetAsync(movie.Title);
            return View("SearchFilm", result);

        }
    }
}