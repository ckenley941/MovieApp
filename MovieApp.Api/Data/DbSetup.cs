using Microsoft.IdentityModel.Tokens;
using MovieApp.Api.Data.Entities;

namespace MovieApp.Api.Data
{
    /// <summary>
    /// Static class used to Seed DB Data
    /// </summary>
    public static class DbSetup
    {
        private static MovieContext _context = new MovieContext();
        public static void SeedData()
        {     
            var movieTitle = "Pulp Fiction";
            var actorsInMovie = new List<string>()
                {
                    "John Travolta",
                    "Uma Thurman",
                    "Samuel L Jackson",
                    "Bruce Willis"
                };
            AddMovieAndActorData(movieTitle, actorsInMovie, 97);          
             
            movieTitle = "The Godfather";
            actorsInMovie = new List<string>()
                {
                    "Marlon Brando",
                    "Al Pacino",
                    "James Caan",
                    "Diane Keaton"
                };
            AddMovieAndActorData(movieTitle, actorsInMovie, 95);

            movieTitle = "Shawshank Redemption";
            actorsInMovie = new List<string>()
                {
                    "Tim Robbins",
                    "Morgan Freeman"
                };
            AddMovieAndActorData(movieTitle, actorsInMovie, 91);

                movieTitle = "Batman Begins";
            actorsInMovie = new List<string>()
                {
                    "Christian Bale",
                    "Michael Caine",
                    "Morgan Freeman"
                };
            AddMovieAndActorData(movieTitle, actorsInMovie, 89);

            movieTitle = "Die Hard";
            actorsInMovie = new List<string>()
                {
                    "Bruce Willis"
                };
            AddMovieAndActorData(movieTitle, actorsInMovie, 83);
        }

        private static void AddMovieAndActorData(string movieTitle, List<string> actorsInMovie, int rating)
        {
            var movie = GetMovie(movieTitle, rating);
            var movieToActors = new List<MovieToActor>();
            foreach (var actorInMovie in actorsInMovie)
            {
                var actor = GetActor(actorInMovie);
                movieToActors.Add(new MovieToActor()
                {
                    MovieId = movie.MovieId,
                    ActorId = actor.ActorId
                });
            }
            _context.MovieToActors.AddRange(movieToActors);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets a movie based on a name and adds it if it doesn't exist
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        private static Movie GetMovie(string name, int rating)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Name == name);
            //If movie doesn't exist then add it
            if (movie == null)
            {
                movie = new Movie()
                {
                    Name = name
                };
                _context.Movies.Add(movie);

                var movieRating = new MovieRating()
                {
                    Rating = rating,
                    MovieId = movie.MovieId
                };
                _context.MovieRatings.Add(movieRating);

                _context.SaveChanges();
            }
            return movie;
        }

        /// <summary>
        /// Gets an actor based on a name and adds it if it doesn't exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static Actor GetActor(string name)
        {
            var actor = _context.Actors.FirstOrDefault(x => x.Name == name);
            //If actor doesn't exist then add it
            if (actor == null)
            {
                actor = new Actor()
                {
                    Name = name
                };
                _context.Actors.Add(actor);
                _context.SaveChanges();
            }
            return actor;
        }
    }
}
