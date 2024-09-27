using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DALWithEF.Models;

public partial class Imdb20241Context : DbContext
{
    public Imdb20241Context()
    {
    }

    public Imdb20241Context(DbContextOptions<Imdb20241Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TblGenre> TblGenres { get; set; }

    public virtual DbSet<TblMovie> TblMovies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("abracadabra");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblGenre>(entity =>
        {
            entity.HasKey(e => e.GenreId);

            entity.ToTable("tblGenre");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<TblMovie>(entity =>
        {
            entity.HasKey(e => e.MovieId);

            entity.ToTable("tblMovie");

            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Genre).WithMany(p => p.TblMovies)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblMovie_tblGenre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
