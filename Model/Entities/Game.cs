using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.MetaEntities;


namespace Model.Entities
{
    [MetadataType(typeof(MetaGame))]
    public class Game : IGenericModel
    {

        public int Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public bool Discontinued { get; set; }

        public double Price { get; set; }

        public string Picture { get; set; }

        public DateTime GameProduction { get; set; }

        public int UnitsInStock { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Platform> Platforms { get; set; }

        public Game()
        {
            Comments = new List<Comment>();
            Genres = new List<Genre>();
            Platforms = new List<Platform>();
            GameProduction = DateTime.Now;
        }

    }
}
