using MovieApp.Api.Data;
using MovieApp.Api.Data.Entities;
using MovieApp.Api.Data.Interfaces;
using MovieApp.Api.Data.Repositories;

namespace MovieApp.Tests
{
    /// <summary>
    /// Unit Tests for the Movies and Actors Repositories
    /// </summary>
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        ///This test ensures that the CRUD operations in the MoviesRepository work properly
        public void PerformMovieCRUD()
        {
            var movie = new Movie()
            {
                Name = "Reservoir Dogs"
            };
            var movieContext = new MovieContext();
            var movieRepo = new MoviesRepository(movieContext);

            //Ensure the movie doesn't already exist
            if (movieRepo.FindByCondition(x => x.Name == movie.Name).Any())
            {
                throw new Exception("Movie already exists");
            }

            //Create the movie and query that it exists
            movieRepo.Create(movie);
            Assert.IsTrue(movieRepo.FindByCondition(x => x.Name == movie.Name).Any());

            //Update the movie and query that it exists
            movie.Name = "Spiderman";
            movieRepo.Update(movie);
            Assert.IsTrue(movieRepo.FindByCondition(x => x.Name == movie.Name).Any());

            //Delete the movie and query that it doesn't exist
            movieRepo.Delete(movie);
            Assert.IsTrue(!movieRepo.FindByCondition(x => x.Name == movie.Name).Any());
        }

        [TestMethod]
        ///This test ensures that the CRUD operations in the ActorsRepository work properly
        public void PerformActorCRUD()
        {
            var actor = new Actor()
            {
                Name = "Johnny Depp"
            };
            var movieContext = new MovieContext();
            var actorRepo = new ActorsRepository(movieContext);

            //Ensure the movie doesn't already exist
            if (actorRepo.FindByCondition(x => x.Name == actor.Name).Any())
            {
                throw new Exception("Actor already exists");
            }

            //Create the actor and query that it exists
            actorRepo.Create(actor);
            Assert.IsTrue(actorRepo.FindByCondition(x => x.Name == actor.Name).Any());

            //Update the actor and query that it exists
            actor.Name = "Leo DiCaprio";
            actorRepo.Update(actor);
            Assert.IsTrue(actorRepo.FindByCondition(x => x.Name == actor.Name).Any());

            //Delete the actor and query that it doesn't exist
            actorRepo.Delete(actor);
            Assert.IsTrue(!actorRepo.FindByCondition(x => x.Name == actor.Name).Any());
        }
    }
}