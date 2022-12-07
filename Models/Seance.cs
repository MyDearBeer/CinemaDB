using System;
using System.Collections.Generic;

namespace CinemaDB.Models;

public partial class Seance
{
    public int Id { get; set; }

    public int? FilmId { get; set; }

    public int? HallNum { get; set; }

    public string TypeOf3D { get; set; }

    public DateTime? SDate { get; set; }

    public short Price { get; set; }

    public virtual Film Film { get; set; }

    public virtual Hall HallNumNavigation { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
