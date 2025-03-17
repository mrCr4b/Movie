namespace Movie.Models.ViewModels
{
    public class AddMovieViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genres { get; set; }
        public IFormFile Poster { get; set; }
    }
}
