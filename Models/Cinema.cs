using System;
using System.Collections.Generic;

namespace CinemaDB.Models;

public partial class Cinema
{
    public int Id { get; set; }

    public string CName { get; set; }

    public string CAddress { get; set; }

    public byte Halls { get; set; }

    public short WorkersCount { get; set; }

    public short? Clients { get; set; }

    public string AdminPhone { get; set; }

   // public string? Discriminator { get; set; }
    // public short? Color { get; set; }

    //  public Worker worker { get; set; }
    public virtual ICollection<Worker> WorkersNavigation { get; } = new List<Worker>();
}
