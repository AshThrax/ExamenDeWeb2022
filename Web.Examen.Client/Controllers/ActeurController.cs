using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Web.Examen.Client.Client;
using Web.Examen.Client.Models;
using Web.Examen.Client.Models.Acteur;

namespace Web.Examen.Client.Controllers
{
    [Authorize]
    public class ActeurController : Controller
    {
        private readonly ILogger<ActeurController> logger;
        private readonly IActeurClient acteurClient;
        public ActeurController(ILogger<ActeurController> logger ,IActeurClient acteurClient)
        {
            this.logger = logger;
            this.acteurClient = acteurClient;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult DeleteActeur(int filmId, int acteurId)
        {
            var showActeur = acteurClient.GetAsync(filmId, acteurId).Result;
            return View(showActeur);
        }
        [HttpDelete]
        public IActionResult Delete(int Filmid,int acteurid)
        {
            acteurClient.DeleteAsync(Filmid,acteurid);

            return RedirectToAction("Index", "Film");
        }

       
        public IActionResult CreateActeur()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult CreateActeur(CreateActeur acteur)
        {
            acteurClient.PostAsync(acteur);
            return RedirectToAction("Index", "Film");

        }
        
       
        public IActionResult EditActeur(int filmId ,int acteurId)
        {
            var showActeur = acteurClient.GetAsync(filmId,acteurId).Result;
            return View(showActeur);
        }
        [HttpPost]
        public IActionResult EditActeur(ActeurViewModel saveActeur)
        {
            var updtActeur = new SaveActeur();
            updtActeur.Id = saveActeur.Id;
            updtActeur.Name = saveActeur.Name;
            updtActeur.Roles=saveActeur.Roles;
            updtActeur.FilmId = saveActeur.Filmid;
            updtActeur.Rolesdescription = saveActeur.RolesDesCription;

            acteurClient.PutAsync(updtActeur.Id,updtActeur);
            return RedirectToAction("Index","Film");
        }
    }
}
