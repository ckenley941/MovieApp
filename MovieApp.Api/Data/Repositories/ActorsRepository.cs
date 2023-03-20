using MovieApp.Api.Data.Entities;
using MovieApp.Api.Data.Interfaces;

namespace MovieApp.Api.Data.Repositories
{
    /// <summary>
    /// Repository to retrieve Actor data
    /// </summary>
    public class ActorsRepository : RepositoryBase<Actor>, IActorsRepository
    {
        public ActorsRepository(MovieContext repositoryContext) : base(repositoryContext)
        {

        }
        public bool Exists(int id)
        {
            return base.FindByCondition(x => x.ActorId == id).Any();
        }
    }
}
