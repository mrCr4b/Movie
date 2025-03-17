using System;
using System.Collections.Generic;

namespace Movie.Models;

public partial class Showtime
{
    public int Id { get; set; }

    public int? RoomId { get; set; }

    public int? MovieId { get; set; }

    public DateTime? Time { get; set; }

    public int? TicketPrice { get; set; }

    public virtual Room? Room { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
