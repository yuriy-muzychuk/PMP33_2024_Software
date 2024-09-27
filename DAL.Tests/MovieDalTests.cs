using AutoMapper;
using DALWithEF;
using DALWithEF.Profiles;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL.Tests
{
    public class MovieDalTests
    {
        private readonly MovieDalEF _dal;
        private List<Movie> _moviesInDb = new List<Movie>();
        public MovieDalTests() 
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            string connectionString = configuration.GetConnectionString("IMDB") ?? "";

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(MovieProfile).Assembly));

            _dal = new MovieDalEF(config.CreateMapper(), connectionString);
        }  


        [SetUp]
        public void Setup()
        {
            var m = new Movie { Title = "1111", GenreId = 1, ReleaseYear = 2024 };
            _moviesInDb.Add(_dal.AddMovie(m));

            m = new Movie { Title = "2222", GenreId = 2, ReleaseYear = 2024 };
            _moviesInDb.Add(_dal.AddMovie(m));

            m = new Movie { Title = "3333", GenreId = 3, ReleaseYear = 2024 };
            _moviesInDb.Add(_dal.AddMovie(m));
        }

        [TearDown]
        public void TearDown() 
        {
            foreach (var m in _moviesInDb)
            {
                _dal.RemoveMovie(m.MovieId);
            }
        }

        [Test]
        public void GetAllTest()
        {
            var movies = _dal.GetAllMovies();

            Assert.That(movies.Count, Is.EqualTo(_moviesInDb.Count));
            foreach (var movie in _moviesInDb)
            {
                Assert.IsNotNull(movies.Where(m => m.Title == movie.Title).FirstOrDefault());
                Assert.IsTrue(movies.Any(m => m.Title == movie.Title));

                Assert.IsTrue(movies.Any(m => m.MovieId == movie.MovieId));
            }
        }
    }
}