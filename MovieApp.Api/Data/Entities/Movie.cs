using System;
using System.Collections.Generic;

namespace MovieApp.Api.Data.Entities;

public partial class Movie
{
    public int MovieId { get; set; }

    public Guid MovieGuid { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime ModifiedDateTime { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MovieRating> MovieRatings { get; } = new List<MovieRating>();

    public virtual ICollection<MovieToActor> MovieToActors { get; } = new List<MovieToActor>();
}
