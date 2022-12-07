using System;
using System.Collections.Generic;

namespace CinemaDB.Models;

public partial class Hall
{
    public int Number { get; set; }

    public string Technology { get; set; }

    public byte HSeats { get; set; }

    public byte HColumns { get; set; }

    public string SeatsType { get; set; }

    public virtual ICollection<Seance> Seances { get; } = new List<Seance>();
}
