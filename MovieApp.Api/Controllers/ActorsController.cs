using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MovieApp.Api.Common;
using MovieApp.Api.Data.DTOs;
using MovieApp.Api.Data.Entities;
using MovieApp.Api.Data.Interfaces;
using MovieApp.Api.Data.Repositories;

namespace MovieApp.Api.Controllers
{
    /// <summary>
    /// Actors Controller with CRUD operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorsRepository _actorsRepo;
        public ActorsController(IActorsRepository actorsRepo)
        {
            _actorsRepo = actorsRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ActorDto>> Get()
        {
            var actors = _actorsRepo.FindAll();
            var data = ConvertToDto(actors);
            return Ok(data);
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<ActorDto>> Get(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return BadRequest(AppConstants.Messages.NameFilterRequired);
            }
            //Using ToLower() to ignore case
            var actors = _actorsRepo.FindByCondition(x => x.Name.ToLower().Contains(name.ToLower()));
            var data = ConvertToDto(actors);
            return Ok(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Movie> Post([FromBody] Actor actor)
        {
            if (actor is null)
            {
                return BadRequest(AppConstants.Messages.NoDataProvided);
            }
            _actorsRepo.Create(actor);
            return Ok(actor);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] Actor actor)
        {
            if (id != actor.ActorId)
            {
                return BadRequest(AppConstants.Messages.IdColumnsNoMatch);
            }

            if (!_actorsRepo.Exists(id))
            {
                return BadRequest(AppConstants.Messages.NoRecordsFound);
            }

            _actorsRepo.Update(actor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Movie> Delete(int id)
        {
            var movie = _actorsRepo.FindByCondition(x => x.ActorId == id).FirstOrDefault();
            if (movie == null)
            {
                return BadRequest(AppConstants.Messages.NoRecordsFound);
            }
            _actorsRepo.Delete(movie);
            return Ok();
        }

        private IQueryable<ActorDto> ConvertToDto(IQueryable<Actor> actorsQuery)
        {
            actorsQuery = actorsQuery.Include("MovieToActors.Movie");
            var actorDto = actorsQuery.Select(x => new ActorDto()
            {
                ActorId = x.ActorId,
                ActorName = x.Name,
                Movies = x.MovieToActors.Select(x => x.Movie.Name).ToList()
            });

            return actorDto;
        }
    }
}
