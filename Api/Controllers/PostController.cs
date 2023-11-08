using Application.Film;
using Application.Post.Command;
using Application.Post.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ApiController
    {
        [HttpGet("{Pagezise}/{PageNumber}")]
        public async Task<ActionResult<PagedList<PostDto>>> GetPage([FromQuery] GetPaginatedPostQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet]
        public async Task<ActionResult<PostVm>> GetAll()
        { 
            return Ok(await Mediator.Send(new GetAllPostQuery()));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPostQuery { Id=id}));
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePostCommand command)
        {
           return Ok(await Mediator.Send(command));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(int id,UpdatePostCommand command) 
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePostCommand { Id = id });
            return NoContent();
        }
    }
}
