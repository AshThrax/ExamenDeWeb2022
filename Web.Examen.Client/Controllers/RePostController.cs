using Microsoft.AspNetCore.Mvc;
using Web.Examen.Client.Client;
using Web.Examen.Client.Models.RePosts;

namespace Web.Examen.Client.Controllers
{
    public class RePostController : Controller
    {
        private readonly IRepostClient _repostClient;
        public RePostController(IRepostClient repostClient)
        {
            _repostClient = repostClient;
        }

        public IActionResult CreateRepost()
        {

            return View(); 
        }
        [HttpPost]
        public IActionResult CreateRepost(CreateRepost repost)
        {
            _repostClient.Create(repost);
            return RedirectToAction("Index","Post");

        }
        public IActionResult UpdateRepost()
        { 
          return View();
        }
        [HttpPost]
        public IActionResult UpdateRepost( UpdateRepost post)
        {
            _repostClient.Update(post.Id,post);
            return RedirectToAction("Index", "Post");
        }
        public IActionResult DeleteRepost(int id) { 
            _repostClient.Delete(id);

            return RedirectToAction("Index", "Post");
        }
    }
}
