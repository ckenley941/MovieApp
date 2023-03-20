namespace MovieApp.Api.Data.DTOs
{
    /// <summary>
    /// User friendly version of the Movie object
    /// </summary>
    public class MovieDto
    {
        public MovieDto()
        {
            Actors = new List<string>();
        }

        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int Rating { get; set; }
        public List<string> Actors { get; set; }
    }
}
