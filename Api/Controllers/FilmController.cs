using Application.Film;
using Application.Film.Command;
using Application.Film.Query;
using Application.Post.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController :ApiController
    {
        [HttpGet("pagenumber,pageSize")]
        public async Task<ActionResult<PagedList<FilmDto>>> GetByPage([FromQuery] GetFilmByPage query)
        {
            var getFilmPage = await Mediator.Send(query);
            return Ok(getFilmPage);
        }
        [HttpGet]
        public async Task<ActionResult<FilmVm>> Get()
        {
            var getFilm= await Mediator.Send(new GetAllFilmQuery());
            return Ok (getFilm.Lists);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmDto>> GetById(int id)
        {
            return await Mediator.Send(new GetFilmByIdQuery{ Id=id});
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateFilmCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateFilmCommand command)
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
            await Mediator.Send(new DeleteFilmCommand {Id=id });
            return NoContent();
        }
    }
}
