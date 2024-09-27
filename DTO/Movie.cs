namespace DTO
{
    public class Movie
    {
        public Movie()
        {
            Title = string.Empty;
            GenreName = string.Empty;
        }
        public Movie(string title, int releaseYear, int genreId)
        {
            Title = title;
            ReleaseYear = releaseYear;
            GenreId = genreId;
            GenreName = string.Empty;   
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear {  get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set;}
    }
}
