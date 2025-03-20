namespace Movie.Models.ViewModels
{
    public class AddShowtimeViewModel
    {
        public IEnumerable<Room> Rooms { get; set; } = Enumerable.Empty<Room>();
        public int SelectedRoomId { get; set; }
        public IEnumerable<Models.Movie> Movies { get; set; } = Enumerable.Empty<Models.Movie>();
        public int SelectedMovieId { get; set; }
        public DateTime Time { get; set; }
        public int TicketPrice { get; set; }
    }
}
