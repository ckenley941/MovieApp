using System;
using System.Collections.Generic;

namespace MovieApp.Api.Data.Entities;

public partial class MovieRating
{
    public int MovieRatingId { get; set; }

    public Guid MovieRatingGuid { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime ModifiedDateTime { get; set; }

    public int MovieId { get; set; }

    public int Rating { get; set; }

    public string? RatingSource { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
