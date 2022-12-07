using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDB.Models;

public partial class Worker
{
  
    public int Id { get; set; }
   
    public int? CinemaId { get; set; }

    public string WName { get; set; }

    public string WSurname { get; set; }

    public int Passport { get; set; }

    public int? PositionId { get; set; }

    public string Phone { get; set; }

    public int Salary { get; set; }

    public virtual Cinema Cinema { get; set; }

   // public virtual ICollection<Cinema> Orders { get; } = new List<Cinema>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Position Position { get; set; }

    //public Worker( int? cinemaId, string wName, string wSurname, int passport, int? positionId, string phone, int salary)
    //{
    //    CinemaId = cinemaId;
    //    WName = wName;
    //    WSurname = wSurname;
    //    Passport = passport;
    //    PositionId = positionId;
    //    Phone = phone;
    //    Salary = salary;
    //}
}
