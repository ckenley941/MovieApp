namespace MovieApp.Api.Data.DTOs
{
    /// <summary>
    /// User friendly version of the Movie Rating object
    /// </summary>
    public class MovieRatingDto
    {
        public int MovieRatingId { get; set; }
        public int Rating { get; set; }
        public string? RatingSource { get; set; }
        public string MovieName { get; set; }
    }
}
