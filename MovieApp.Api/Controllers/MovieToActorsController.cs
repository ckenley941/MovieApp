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
    /// MovieToActors Controller to tie together movies and actors
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MovieToActorsController : ControllerBase
    {
        private readonly IActorsRepository _actorsRepo;
        private readonly IMoviesRepository _moviesRepo;
        private readonly IMovieToActorsRepository _movieToActorsRepository;
        public MovieToActorsController(IActorsRepository actorsRepo, IMoviesRepository moviesRepo,
            IMovieToActorsRepository movieToActorsRepository)
        {
            _actorsRepo = actorsRepo;
            _moviesRepo = moviesRepo;
            _movieToActorsRepository = movieToActorsRepository;
        }


        [HttpPost("TieActorAndMovie")]
        [Authorize]
        public ActionResult<MovieToActorDto> TieActorAndMovie([FromBody] MovieToActorDto movieToActor)
        {
            var movie = _moviesRepo.FindByCondition(x => x.Name == movieToActor.MovieName).FirstOrDefault();
            if (movie is null)
            {
                movie = new Movie()
                {
                    Name = movieToActor.MovieName
                };
                _moviesRepo.Create(movie);
            }

            var actor = _actorsRepo.FindByCondition(x => x.Name == movieToActor.ActorName).FirstOrDefault();
            if (actor is null)
            {
                actor = new Actor()
                {
                    Name = movieToActor.ActorName
                };
                _actorsRepo.Create(actor);
            }

            _movieToActorsRepository.Create(new MovieToActor()
            {
                MovieId = movie.MovieId,
                ActorId = actor.ActorId
            });
            return Ok(movieToActor);
        }    
    }
}
