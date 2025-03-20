namespace Movie.Models.ViewModels
{
    public class ReserveSeatsViewModel
    {
        public int ShowtimeId { get; set; }
        public string Place { get; set; }
        public List<string> SelectedPlaces { get; set; } = new List<string>();
        //public string SelectedPlaces { get; set; }
        public string State { get; set; }

        public string UserId { get; set; }
    }
}
