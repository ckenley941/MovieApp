using MovieApp.Api.Data.Entities;
using MovieApp.Api.Data.Interfaces;

namespace MovieApp.Api.Data.Repositories
{
    /// <summary>
    /// Repository to Movie To Actor data
    /// </summary>
    public class MovieToActorsRepository : RepositoryBase<MovieToActor>, IMovieToActorsRepository
    {
        public MovieToActorsRepository(MovieContext repositoryContext) : base(repositoryContext)
        {

        }

        public bool Exists(int id)
        {
            return base.FindByCondition(x => x.MovieToActorId == id).Any();
        }
    }
}
