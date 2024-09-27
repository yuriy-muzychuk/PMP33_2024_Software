using DAL.Concrete;
using DTO;
using DALWithEF;
using AutoMapper;
using DALWithEF.Profiles;
using Microsoft.Extensions.Configuration;


IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .Build();

//string connectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("IMDB") ?? "";
//string connectionString = configuration.GetSection("ConnectionStrings")["IMDB"] ?? "";
string connectionString = configuration.GetConnectionString("IMDB") ?? "";

Console.WriteLine("Welcome to IMDB!");
char option = 's';

MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(MovieProfile).Assembly));


while (true)
{
    Console.WriteLine("Please enter\n'1' to get all Genres\n'2' to get Genre by ID\n'3' to get all Movies\n'4' to add movie\nQ to quit.\n");
    string entryLine = Console.ReadLine()??"";
    if(string.IsNullOrWhiteSpace(entryLine) || entryLine.Length > 1)
    {
        Console.WriteLine("Incorrect option!");
        continue;
    }
    option = Convert.ToChar(entryLine.ToLower());

    switch (option)
    {
        case '1':
            GetAllGenres();
            break;
        case '2':
            GetGenreById();
            break;
        case '3':
            GetAllMovies();
            break;
        case '4':
            AddMovie();
            break;
        case 'q':
            return;
        case '5':
            TestEntityFramework();
            break;
        case '6':
            AddMovieEF();
            break;
        default:
            Console.WriteLine("Incorrect option!");
            break;
    }
}

void TestEntityFramework()
{
    var dal = new MovieDalEF(config.CreateMapper(), connectionString);
    var movies = dal.GetAllMovies();
    foreach (var movie in movies)
    {
        Console.WriteLine($"{movie.MovieId} - {movie.Title} - {movie.GenreName}");
    }

}
void AddMovieEF()
{
    Console.WriteLine("Please enter movie title:");
    string title = Console.ReadLine();

    Console.WriteLine("Please enter movie release year:");
    int releaseYear = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Please enter movie genre Id:");
    int genreId = Convert.ToInt32(Console.ReadLine());

    var movie = new Movie
    {
        Title = title,
        ReleaseYear = releaseYear,
        GenreId = genreId
    };

    var dal = new MovieDalEF(config.CreateMapper(), connectionString);

    movie = dal.AddMovie(movie);
    Console.WriteLine($"Added movie. Id = {movie.MovieId}");
}
//void TestEF()
//{
//    MapperConfiguration conf = new MapperConfiguration(cfg => cfg.AddMaps(typeof(MovieProfile).Assembly));


//    var dal = new MovieDalEF(
//        new DBConfiguration(configuration.GetConnectionString("IMDB")??""), 
//        conf.CreateMapper());
//    List<Movie> movies = dal.GetAllMovies();

//    foreach (var item in movies)
//    {
//        Console.WriteLine($"{item.MovieId}. {item.Title} - {item.GenreName}");
//    }
//}

void AddMovie()
{
    Console.WriteLine("Please enter movie title:");
    string title = Console.ReadLine();

    Console.WriteLine("Please enter movie release year:");
    int releaseYear = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Please enter movie genre Id:");
    int genreId = Convert.ToInt32(Console.ReadLine());

    var movie = new Movie
    {
        Title = title,
        ReleaseYear = releaseYear,
        GenreId = genreId
    };

    var dal = new MovieDal(connectionString);

    movie = dal.AddMovie(movie);
    Console.WriteLine($"Added movie. Id = {movie.MovieId}");
}

void GetAllMovies()
{
    var dal = new MovieDal(connectionString);
    List<Movie> movies = dal.GetAllMovies();

    foreach (var item in movies)
    {
        Console.WriteLine($"{item.MovieId}. {item.Title}");
    }
}

void GetGenreById()
{
    Console.WriteLine("Please enter Genre Id");
    int id = Convert.ToInt32(Console.ReadLine());

    var dal = new GenreDal(connectionString);
    Genre genre = dal.GetGenreById(id);
    if (genre != null)
    {
        Console.WriteLine($"{genre.GenreId}. {genre.Name}");
    }
    else
    { 
        Console.WriteLine("Genre Id not found!");
    }
}

void GetAllGenres()
{
    var dal = new GenreDal(connectionString);
    List<Genre> genres = dal.GetAllGenres();

    foreach (var item in genres)
    {
        Console.WriteLine($"{item.GenreId}. {item.Name}");
    }
}

























