using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Examen.Client.Client;
using Web.Examen.Client.Models.Posts;

namespace Web.Examen.Client.Controllers
{
    [Authorize(Roles ="Admin,User,Owner")]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostClient _postClient;
        public PostController(ILogger<PostController> logger,IPostClient postClient)
        {
            _postClient = postClient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllPost()
        {
            var getAllPost=_postClient.GetAllAsync().Result;

            return View(getAllPost);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetPost(int postid)
        { 
            var getPost =_postClient.GetPost(postid);
            return View(getPost);
        }

        public IActionResult DeletePost(int Postid)
        {
            _postClient.DeletePost(Postid);
            return RedirectToAction("GetAllPost");
        }
        public IActionResult UpdatePost(int postid)
        {
            var getPost = _postClient.GetPost(postid).Result;
            return View(getPost);
        }
        [HttpPost]
        public IActionResult UpdatePost(int updt,UpdatePost post)
        {
            _postClient.UpdatePost(updt,post);
            return RedirectToAction("GetAllPost");
        }
        public IActionResult CreatePost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePost(CreatePost post)
        {
            _postClient.CreatePost(post);
            return RedirectToAction("GetAllPost","Post");
        }
    }
}
