using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Game
    {
        [Key]
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Platform> Platforms { get; set; }

        public Game()
        {
            Comments = new List<Comment>();
            Genres = new List<Genre>();
            Platforms = new List<Platform>();
        }

        public void RegisterGenre(Genre genre)
        {
            this.Genres.Add(genre);
            genre.Games.Add(this);
        }

        public void RegisterPlatform(Platform platform)
        {
            this.Platforms.Add(platform);
            platform.Games.Add(this);
        }

        public override string ToString()
        {
            return String.Format("Key = {0}, Name = {1}", this.Key, this.Name);
        }
    }
}
