using DAL.Interface;
using DTO;
using System.Data.SqlClient;

namespace DAL.Concrete
{
    public class MovieDal : IMovieDal
    {
        private readonly string _connectionString;

        public MovieDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Movie AddMovie(Movie movie)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into tblMovie (Title, ReleaseYear, GenreId) output inserted.MovieId values (@title, @year, @genre)";
                
                command.Parameters.AddWithValue("title", movie.Title);
                command.Parameters.AddWithValue("year", movie.ReleaseYear);
                command.Parameters.AddWithValue("genre", movie.GenreId);

                connection.Open();
                var newMovieId = Convert.ToInt32(command.ExecuteScalar());
                movie.MovieId = newMovieId;

                return movie;
            }
        }

        public List<Movie> GetAllMovies()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from tblMovie";

                connection.Open();
                var reader = command.ExecuteReader();

                List<Movie> movies = new List<Movie>();
                while (reader.Read())
                {
                    movies.Add(new Movie
                    {
                        Title = reader["Title"].ToString(),
                        GenreId = Convert.ToInt32(reader["GenreId"]),
                        MovieId = Convert.ToInt32(reader["MovieId"]),
                        ReleaseYear = Convert.ToInt32(reader["ReleaseYear"])
                    });
                }
                return movies;
            }
        }

        public void RemoveMovie(int movieId)
        {
            throw new NotImplementedException();
        }
    }
}
