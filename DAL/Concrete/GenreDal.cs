using DAL.Interface;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Concrete
{
    public class GenreDal : IGenreDal
    {
        private readonly string _connectionString;

        public GenreDal(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public List<Genre> GetAllGenres()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from tblGenre";

                connection.Open();
                var reader = command.ExecuteReader();

                List<Genre> genres = new List<Genre>();
                while (reader.Read())
                {
                    genres.Add(new Genre
                    {
                        Name = reader["Name"].ToString(),
                        GenreId = Convert.ToInt32(reader["GenreId"])
                    });
                }
                return genres;
            }
        }

        public Genre GetGenreById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from tblGenre where GenreId = @id";

                command.Parameters.Add("id", SqlDbType.Int);
                command.Parameters["id"].Value = id;

                connection.Open();
                var reader = command.ExecuteReader();

                if(reader.Read())
                {
                    return new Genre
                    {
                        Name = reader["Name"].ToString(),
                        GenreId = Convert.ToInt32(reader["GenreId"])
                    };
                }

                return null;
            }
        }
    }
}
