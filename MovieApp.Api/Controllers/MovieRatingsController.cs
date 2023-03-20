using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    /// Movie Ratings Controller with CRUD operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRatingsController : ControllerBase
    {
        private readonly IMovieRatingsRepository _movieRatingsRepo;
        private readonly IMoviesRepository _moviesRepo;
        public MovieRatingsController(IMovieRatingsRepository movieRatingsRepo, IMoviesRepository moviesRepo)
        {
            _movieRatingsRepo = movieRatingsRepo;
            _moviesRepo = moviesRepo;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<MovieRatingDto>> Get()
        {
            var actors = _movieRatingsRepo.FindAll();
            var data = ConvertToDto(actors);
            return Ok(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Movie> Post([FromBody] MovieRating movingRating)
        {
            if (movingRating is null)
            {
                return BadRequest(AppConstants.Messages.NoDataProvided);
            }

            if (!_moviesRepo.Exists(movingRating.MovieId))
            {
                return BadRequest(AppConstants.Messages.NoRecordsFound);
            }
            _movieRatingsRepo.Create(movingRating);
            return Ok(movingRating);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] MovieRating movieRating)
        {
            if (id != movieRating.MovieRatingId)
            {
                return BadRequest(AppConstants.Messages.IdColumnsNoMatch);
            }

            if (!_movieRatingsRepo.Exists(id))
            {
                return BadRequest(AppConstants.Messages.NoRecordsFound);
            }
            _movieRatingsRepo.Update(movieRating);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Movie> Delete(int id)
        {
            var movie = _movieRatingsRepo.FindByCondition(x => x.MovieRatingId == id).FirstOrDefault();
            if (movie == null)
            {
                return BadRequest(AppConstants.Messages.NoRecordsFound);
            }
            _movieRatingsRepo.Delete(movie);
            return Ok();
        }

        private IQueryable<MovieRatingDto> ConvertToDto(IQueryable<MovieRating> movieRatingQuery)
        {
            movieRatingQuery = movieRatingQuery.Include(x => x.Movie);
            var actorDto = movieRatingQuery.Select(x => new MovieRatingDto()
            {
                MovieRatingId = x.MovieRatingId,
                Rating = x.Rating,
                RatingSource = x.RatingSource,
                MovieName = x.Movie.Name
            });

            return actorDto;
        }
    }
}
