using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MovieApp.Api.Data.DTOs;
using MovieApp.Api.Data.Entities;
using MovieApp.Api.Data.Interfaces;
using System.Linq.Expressions;

namespace MovieApp.Api.Data.Repositories
{
    /// <summary>
    /// Repository to retrieve Movie data
    /// </summary>
    public class MoviesRepository : RepositoryBase<Movie>, IMoviesRepository
    {
        public MoviesRepository(MovieContext repositoryContext) : base (repositoryContext)
        {
            
        }

        public bool Exists(int id)
        {
            return base.FindByCondition(x => x.MovieId == id).Any();
        }
    }
}
