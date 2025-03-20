namespace Movie.Models.ViewModels
{
    public class AddMovieViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Poster { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = Enumerable.Empty<Genre>();
        public List<int> SelectedGenres { get; set; } = new List<int>();

    }
}
