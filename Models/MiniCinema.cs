using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaDB.Models
{
   // [Table("MiniCinemas")]
    public partial class MiniCinema: Cinema
    {
        public int? Kids { get; set; }
    }
}
