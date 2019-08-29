using System.Data.Entity;
using Model.Reporting;
using Model;
using Model.Entities;

namespace DAL
{
    public class GameStoreContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<EntityView> EntityViews { get; set; }

        public DbSet<Model.Entities.Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Publisher> Publisher { get; set; }

        public IDbSet<Role> Roles { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Language> Language { get; set; }

        public IDbSet<GameLang> GameLang { get; set; }

        public IDbSet<BanUser> BanUser { get; set; }
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


            modelBuilder.Entity<User>()
                .HasMany(q => q.UserRoles)
                .WithMany(x => x.UsersInRole)
                .Map(t => t.MapLeftKey("IdUser")
                        .MapRightKey("IdRole")
                        .ToTable("UsersRoles"));
        }
    }
}
