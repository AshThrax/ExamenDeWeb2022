using Application.Film;
using Application.Repost.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RePostController : ApiController
    {
       //*
        [HttpPost]
        public async Task<ActionResult> Create(CreateRepostCommand Command) 
        {
            await Mediator.Send(Command);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,UpdateRepostCommand Command)
        {
            if (id !=Command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(Command);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id,int PostId)
        {
            await Mediator.Send(new DeleteRepostCommand {Id=id,PostId=PostId});
            return NoContent();
        }

    }
}
