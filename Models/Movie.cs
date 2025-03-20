using System;
using System.Collections.Generic;

namespace Movie.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Poster { get; set; }

    public string? State { get; set; }

    public int? Revenue { get; set; }

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
