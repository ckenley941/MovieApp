using MovieApp.Api.Data.Entities;
using MovieApp.Api.Data.Interfaces;

namespace MovieApp.Api.Data.Repositories
{
    /// <summary>
    /// Repository to retrieve Movie Ratings data
    /// </summary>
    public class MovieRatingsRepository : RepositoryBase<MovieRating>, IMovieRatingsRepository
    {
        public MovieRatingsRepository(MovieContext repositoryContext) : base(repositoryContext)
        {

        }

        public bool Exists(int id)
        {
            return base.FindByCondition(x => x.MovieRatingId == id).Any();
        }
    }
}
