using AutoMapper;
using DAL.Interface;
using DALWithEF.Context;
using DALWithEF.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DALWithEF
{
    public class MovieDalEF : IMovieDal
    {
        private readonly IMapper _mapper;
        private readonly string _connectionString;

        public MovieDalEF(IMapper mapper, string connectionString)
        {
            _mapper = mapper;
            _connectionString = connectionString;
        }

        public Movie AddMovie(Movie movie)
        {
            using (var context = new IMDBContext(_connectionString))
            {
                var tblMovie = _mapper.Map<TblMovie>(movie);
                context.TblMovies.Add(tblMovie);
                context.SaveChanges();

                movie.MovieId = tblMovie.MovieId;

                return movie;
            }
        }

        public List<Movie> GetAllMovies()
        {
            using (var context = new IMDBContext(_connectionString))
            {
                var movies = context.TblMovies.Include(m => m.Genre).ToList();
                return _mapper.Map<List<Movie>>(movies);
            }
        }

        public void RemoveMovie(int movieId)
        {
            using (var context = new IMDBContext(_connectionString))
            {
                var m = context.TblMovies.Where(m => m.MovieId == movieId).FirstOrDefault();
                if (m == null)
                {
                    return;
                }
                context.TblMovies.Remove(m);
                context.SaveChanges();
            }
        }
    }
}
