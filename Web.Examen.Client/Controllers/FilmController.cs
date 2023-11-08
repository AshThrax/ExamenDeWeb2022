using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Examen.Client.Client;
using Web.Examen.Client.Models.Film;

namespace Web.Examen.Client.Controllers
{

    public class FilmController : Controller
    {
        private readonly ILogger<FilmController> logger;
        private readonly IFilmClient FilmClient;
        public const string Filmid = "Film_Id";
        public FilmController(ILogger<FilmController> logger, IFilmClient FilmClient)
        {
            this.logger = logger;
            this.FilmClient = FilmClient;
        }
        //view side
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllFilm()
        {
            var allForOne = FilmClient.GetallAsync().Result;
            return View(allForOne);
        }
        public IActionResult Detail(int id)
        {
            var OneForAll = FilmClient.GetAsync(id).Result;
            return View();
        }
        public IActionResult CreateFilm()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var filmEdit = FilmClient.GetAsync(id).Result;
            return View(filmEdit);
        }
        public IActionResult Details(int id)
        {
            var filmEdit = FilmClient.GetAsync(id).Result;
            return View(filmEdit);
        }
        public IActionResult Delete(int id)
        {
            var filmEdit = FilmClient.GetAsync(id).Result;
            return View(filmEdit);
        }
        //Action side
        [HttpPost]
        public IActionResult CreateFilm(CreateFilm createFilm)
        {

            FilmClient.PostAsync(createFilm);
            return RedirectToAction("Index", "Film");
        }
        [HttpPost]
        public IActionResult Edit(FilmViewModel saveFilm)
        {
            var UpdtFilm = new SaveFilm();
            UpdtFilm.Titre = saveFilm.Titre;
            UpdtFilm.Date = saveFilm.Date;
            UpdtFilm.Description = saveFilm.Description;
            UpdtFilm.Id = saveFilm.Id;
            UpdtFilm.Genre = saveFilm.Genre;

            FilmClient.PutAsync(saveFilm.Id, UpdtFilm);
            return RedirectToAction("Index", "Film");
        }
        [HttpGet]
        public IActionResult GetFilm()
        {
            FilmClient.GetallAsync();
            return NoContent();//will return a list with all the movie
        }
    
        public IActionResult DeleteFilm(FilmViewModel deletefilm)
        {
            FilmClient.DeleteAsync(deletefilm.Id);
            return RedirectToAction("Index","Film");
        }
    }
}
