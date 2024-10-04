using DTO;

namespace BusinessLogic.Interface
{
    public interface IFilmManager
    {
        List<Movie> GetAllMovies();
        Movie AddMovie(Movie movie);
        List<Genre> GetAllGenres();
    }
}
