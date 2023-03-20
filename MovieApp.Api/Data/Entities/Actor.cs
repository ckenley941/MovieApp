using System;
using System.Collections.Generic;

namespace MovieApp.Api.Data.Entities;

public partial class Actor
{
    public int ActorId { get; set; }

    public Guid ActorGuid { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime ModifiedDateTime { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MovieToActor> MovieToActors { get; } = new List<MovieToActor>();
}
