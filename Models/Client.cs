using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaDB.Models;

public partial class Client
{

    public int Id { get; set; }

    public string CName { get; set; }

    public string CSurname { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
