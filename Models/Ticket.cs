using System;
using System.Collections.Generic;

namespace CinemaDB.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? SeanceId { get; set; }

    public byte NumOfSeat { get; set; }

    public string Booking { get; set; }

    public DateTime? DateOfPay { get; set; }

    public virtual Order Order { get; set; }

    public virtual Seance Seance { get; set; }
}
