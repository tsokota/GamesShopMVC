using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Genre
    {
        [Key]
        public string Name { get; set; }
        public Genre ParentGenre { get; set; }

        public virtual ICollection<Genre> SubGenres { get; set; }
        public virtual ICollection<Game> Games { get; set; }

        public Genre()
        {
            SubGenres = new List<Genre>();
            Games = new List<Game>();
        }
    }
}
