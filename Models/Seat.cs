using System;
using System.Collections.Generic;

namespace Movie.Models;

public partial class Seat
{
    public int ShowtimeId { get; set; }

    public string Place { get; set; } = null!;

    public string? State { get; set; }

    public string? UserId { get; set; }

    public virtual Showtime Showtime { get; set; } = null!;
}
