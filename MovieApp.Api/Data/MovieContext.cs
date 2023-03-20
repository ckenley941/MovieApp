using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MovieApp.Api.Data.Entities;

namespace MovieApp.Api.Data;

/// <summary>
/// This class and all entities were scaffolded using the Entity Framework Database first
/// </summary>
public partial class MovieContext : DbContext
{
    public MovieContext()
    {
    }

    public MovieContext(DbContextOptions<MovieContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieRating> MovieRatings { get; set; }

    public virtual DbSet<MovieToActor> MovieToActors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseInMemoryDatabase(databaseName: "MovieAppDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actor__57B3EA4BFB492384");

            entity.ToTable("Actor");

            entity.Property(e => e.ActorGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__4BD2941AF5EC4CF4");

            entity.ToTable("Movie");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.MovieGuid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<MovieRating>(entity =>
        {
            entity.HasKey(e => e.MovieRatingId).HasName("PK__MovieRat__AB2CC87371B20E28");

            entity.ToTable("MovieRating");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.MovieRatingGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieRatings)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovieRating_MovieId");
        });

        modelBuilder.Entity<MovieToActor>(entity =>
        {
            entity.HasKey(e => e.MovieToActorId).HasName("PK__MovieToA__7531B84C946CFD37");

            entity.ToTable("MovieToActor");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.MovieToActorGuid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Actor).WithMany(p => p.MovieToActors)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovieToActor_ActorId");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieToActors)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovieToActor_MovieId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

