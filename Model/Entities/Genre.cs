using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.MetaEntities;


namespace Model.Entities
{
    [MetadataType(typeof(MetaGenre))]
    public class Genre : IGenericModel
    {
      
        public int Id { get; set; }
       
        public string Name { get; set; }

        public Genre ParentGenre { get; set; }
       
        public bool IsDeleted { get; set; }

        public int NorthWindId { get; set; }
   

        public virtual ICollection<Genre> SubGenres { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public Genre()
        {
            SubGenres = new List<Genre>();
            Games = new List<Game>();
        }
    }
}
