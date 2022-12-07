using System;
using System.Collections.Generic;

namespace CinemaDB.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public int? WorkerId { get; set; }

    public byte Tickets { get; set; }

    public DateTime? ODate { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<Ticket> TicketsNavigation { get; } = new List<Ticket>();

    public virtual Worker Worker { get; set; }
}
