using DTO;

namespace DAL.Interface
{
    public interface IGenreDal
    {
        List<Genre> GetAllGenres();
        Genre GetGenreById(int id);
    }
}
