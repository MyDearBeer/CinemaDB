using System;
using System.Collections.Generic;

namespace CinemaDB.Models;

public partial class Position
{
    public int Id { get; set; }

    public string PName { get; set; }

    public virtual ICollection<Worker> Workers { get; } = new List<Worker>();

    //public Position(string pName)
    //{
    //    PName = pName;

    //}
}
