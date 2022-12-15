using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaDB.Models
{
    public class Actor
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string SurName { get; set; }

        // public string FilmId { get; set; }

        public virtual ICollection<Film> Films { get; } = new List<Film>();


    }
}
