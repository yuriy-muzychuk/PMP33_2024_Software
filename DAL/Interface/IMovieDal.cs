using DTO;

namespace DAL.Interface
{
    public interface IMovieDal
    {
        List<Movie> GetAllMovies();
        Movie AddMovie(Movie movie);
        void RemoveMovie(int movieId);
    }
}
