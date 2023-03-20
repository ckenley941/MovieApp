using System;
using System.Collections.Generic;

namespace MovieApp.Api.Data.Entities;

public partial class MovieToActor
{
    public int MovieToActorId { get; set; }

    public Guid MovieToActorGuid { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public int MovieId { get; set; }

    public int ActorId { get; set; }

    public virtual Actor Actor { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
