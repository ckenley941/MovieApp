using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Api.Common;
using MovieApp.Api.Data.DTOs;
using MovieApp.Api.Data.Entities;
using MovieApp.Api.Data.Interfaces;
using MovieApp.Api.Data.Repositories;

namespace MovieApp.Api.Controllers
{
    /// <summary>
    /// Movie Controller with CRUD operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepo;
        public MoviesController(IMoviesRepository moviesRepo) 
        {
            _moviesRepo = moviesRepo;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<MovieDto>> Get()
        {
            var movies = _moviesRepo.FindAll();
            var data = ConvertToDto(movies);
            return Ok(data);
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<MovieDto>> Get(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return BadRequest(AppConstants.Messages.NameFilterRequired);
            }
            //Using ToLower() to ignore case
            var movies = _moviesRepo.FindByCondition(x => x.Name.ToLower().Contains(name.ToLower()));
            var data = ConvertToDto(movies);
            return Ok(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Movie> Post([FromBody] Movie movie)
        {
            if (movie is null)
            {
                return BadRequest(AppConstants.Messages.NoDataProvided);
            }

            _moviesRepo.Create(movie);
            return Ok(movie);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest(AppConstants.Messages.IdColumnsNoMatch);
            }

            if (!_moviesRepo.Exists(id))
            {
                return BadRequest(AppConstants.Messages.NoRecordsFound);
            }
            
            _moviesRepo.Update(movie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Movie> Delete(int id)
        {
            var movie = _moviesRepo.FindByCondition(x => x.MovieId == id).FirstOrDefault();
            if (movie == null)
            {
                return BadRequest(AppConstants.Messages.NoRecordsFound);
            }
            _moviesRepo.Delete(movie);
            return Ok();
        }

        private IQueryable<MovieDto> ConvertToDto(IQueryable<Movie> moviesQuery)
        {
            moviesQuery = moviesQuery.Include(x => x.MovieRatings).Include("MovieToActors.Actor");
            var movieDto = moviesQuery.Select(x => new MovieDto()
            {
                MovieId = x.MovieId,
                MovieName = x.Name,
                Rating = x.MovieRatings.Any() ? x.MovieRatings.Max(x => x.Rating) : 0, //If movie has multiple rating then taking the highest rating 
                Actors = x.MovieToActors.Select(x => x.Actor.Name).ToList()
            });

            return movieDto;
        }
    }
}
