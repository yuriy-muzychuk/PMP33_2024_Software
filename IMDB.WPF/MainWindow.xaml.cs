using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using DALWithEF.Profiles;
using BusinessLogic.Concrete;
using DALWithEF;
using DAL.Concrete;
using DTO;
namespace IMDB.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FilmManager _manager;

        public MainWindow()
        {
            InitializeComponent();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            string connectionString = configuration.GetConnectionString("IMDB") ?? "";

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(MovieProfile).Assembly));
            var movieDal = new MovieDalEF(config.CreateMapper(), connectionString);
            var genreDal = new GenreDal(connectionString);

            _manager = new FilmManager(movieDal, genreDal);
            UpdateList();
            UpdateGenres();
        }

        private void UpdateGenres()
        {
            var genres = _manager.GetAllGenres();
            cmbGenre.Items.Clear();
            foreach (var genre in genres)
            {
                cmbGenre.Items.Add(genre);
            }
        }

        private void UpdateList()
        {
            var movies = _manager.GetAllMovies();

            lsMovies.Items.Clear();
            foreach (var movie in movies)
            {
                lsMovies.Items.Add(movie.Title);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                var selectedGenre = (Genre)cmbGenre.SelectedItem;
                var movie = new Movie { Title = txtTitle.Text.Trim(), GenreId = selectedGenre.GenreId, ReleaseYear = DateTime.Now.Year };
                movie = _manager.AddMovie(movie);

                MessageBox.Show($"Movie {movie.Title} added successfully with ID: {movie.MovieId}!", "Movie Added!", MessageBoxButton.OK, MessageBoxImage.Information);

                UpdateList();

                txtTitle.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a movie title!", "Wrong Title!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}