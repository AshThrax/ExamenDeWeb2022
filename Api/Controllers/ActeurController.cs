using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using Application.Film;
using Application.Film.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Application.Acteur.Command;

namespace Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ActeurController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ActeurDto>>> Get(int filmid)
        {
            return Ok(await Mediator.Send(new GetAllActeurbyFilmQuery{ filmId=filmid}));//
        }
        [HttpGet("{filmId}/{acteurId}")]
        public async Task<ActionResult<ActeurDto>> Get(int filmId,int acteurId)
        {
            var acteur= await Mediator.Send(new GetActeurByIdFromFilmQuery { filmId = filmId, ActeurId = acteurId });
            return Ok(acteur);//
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult<int>> Create(CreateActeurCommand Command)
        {
          
            return Ok(await Mediator.Send(Command));
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update(int id ,UpdateActeurCommand command)
        {

            if (id !=command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteActeurCommand { Id = id });

            return NoContent();
        }
    }
}
