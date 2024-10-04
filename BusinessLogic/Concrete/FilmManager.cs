using BusinessLogic.Interface;
using DAL.Interface;
using DTO;

namespace BusinessLogic.Concrete
{
    public class FilmManager : IFilmManager
    {
        private readonly IMovieDal _movieDal;
        private readonly IGenreDal _genreDal;

        public FilmManager(IMovieDal movieDal, IGenreDal genreDal)
        {
            _movieDal = movieDal;
            _genreDal = genreDal;
        }

        public Movie AddMovie(Movie movie)
        {
            return _movieDal.AddMovie(movie);
        }

        public List<Movie> GetAllMovies()
        {
            return _movieDal.GetAllMovies();
        }

        public List<Genre> GetAllGenres()
        {
            return _genreDal.GetAllGenres();
        }
    }
}
