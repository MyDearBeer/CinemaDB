using System;
using System.Collections.Generic;

namespace CinemaDB.Models;

public partial class Film
{
    public int Id { get; set; }

    public string FName { get; set; }

    public string Genre { get; set; }

    public string Company { get; set; }

    public TimeSpan? FTime { get; set; }

    public string AgeRate { get; set; }

    public decimal? Stars { get; set; }

    public virtual ICollection<Seance> Seances { get; } = new List<Seance>();
}
