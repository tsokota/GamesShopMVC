using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using Model.Reporting;

namespace Model
{
    public class GameStoreContext: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<EntityView> EntityViews { get; set; }

        public GameStoreContext()
            : base("GameContext")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasMany(c => c.Genres)
                .WithMany(s => s.Games)
                .Map(t => t.MapLeftKey("GameId")
                .MapRightKey("GenreId")
                .ToTable("GameGenre"));


            modelBuilder.Entity<Game>().HasMany(c => c.Platforms)
                .WithMany(s => s.Games)
                .Map(t => t.MapLeftKey("GameId")
                .MapRightKey("PlatformId")
                .ToTable("GamePlatform"));
        }
        
    }
}
