using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GameStoreContextDbInitializer : DropCreateDatabaseAlways<GameStoreContext>
    {
        protected override void Seed(GameStoreContext context)
        {
            // Platforms
            Platform platformDesktop = new Platform { Name = "Desktop" };
            Platform platformConsole = new Platform { Name = "Console" };

            context.Platforms.Add(new Platform { Name = "Mobile" });
            context.Platforms.Add(new Platform { Name = "Browser" });
            context.Platforms.Add(platformDesktop);
            context.Platforms.Add(platformConsole);

            context.SaveChanges();

            // Genres
            Genre genre = new Genre();
            genre.Name = "Strategy";
            genre.SubGenres = new List<Genre> 
            { 
                new Genre { Name = "RTS", ParentGenre = genre }, 
                new Genre { Name = "TBS", ParentGenre = genre } 
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Action";
            genre.SubGenres = new List<Genre> 
            {
                new Genre { Name = "FPS", ParentGenre = genre },
                new Genre { Name = "TPS", ParentGenre = genre }
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "RPG";
            genre.SubGenres = new List<Genre>
            {
                new Genre { Name = "MMORPG", ParentGenre = genre },
                new Genre { Name = "Action RPG", ParentGenre = genre },
                new Genre { Name = "Tactical RPG", ParentGenre = genre } 
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Sports";
            genre.SubGenres = new List<Genre>
            {
                new Genre { Name = "Football", ParentGenre = genre },
                new Genre { Name = "Golf", ParentGenre = genre }
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Races";
            genre.SubGenres = new List<Genre>
            {
                new Genre { Name = "Rally", ParentGenre = genre },
                new Genre { Name = "Arcade", ParentGenre = genre },
                new Genre { Name = "Formula", ParentGenre = genre },
                new Genre { Name = "Off-road", ParentGenre = genre }
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Adventure";
            genre.SubGenres = new List<Genre>();
            context.Genres.Add(genre);

            context.SaveChanges();

            // Games
            Game game = new Game();
            game.Key = "001";
            game.Name = "Warcraft III: Reign of Chaos";
            game.Description = "Warcraft III: Reign of Chaos is a high fantasy real-time strategy video game released " +
                "by Blizzard Entertainment on July 3, 2002 in the U.S. It is the second sequel to Warcraft: Orcs & Humans, " +
                "and it is the third game set in the Warcraft fictional Universe. An expansion pack, The Frozen Throne, " +
                "was released on July 1, 2003.";
            game.RegisterGenre(context.Genres.Find("Strategy"));
            game.RegisterGenre(context.Genres.Find("RTS"));
            game.RegisterPlatform(platformDesktop);
            context.Games.Add(game);

            game = new Game();
            game.Key = "002";
            game.Name = "Fallout 3";
            game.Description = "Fallout 3 takes place in the year 2277, 36 years after the setting of " +
                "Fallout 2 and 200 years after the nuclear apocalypse that devastated the game's world " +
                "in a future where international conflicts between the United States and China culminated " +
                "in a Sino-American war in 2077, due to the scarcity of petroleum reserves that ran the economies " +
                "of both countries. The player character is an inhabitant of Vault 101, a survival shelter designed " +
                "to protect up to 1,000 humans from the nuclear fallout. When the player character's father disappears " +
                "under mysterious circumstances, the Overseer, or the leader of the vault, initiates martial law, " +
                "and sends security forces after the player, who is forced to escape from the Vault and journey into " +
                "the ruins of Washington, D.C. to track him down. Along the way the player is assisted by a number of " +
                "human survivors and must battle a myriad of enemies that inhabit the area now known as the \"Capital Wasteland\".";
            game.RegisterGenre(context.Genres.Find("Action"));
            game.RegisterGenre(context.Genres.Find("TPS"));
            game.RegisterGenre(context.Genres.Find("FPS"));
            game.RegisterGenre(context.Genres.Find("Action RPG"));
            game.RegisterPlatform(platformDesktop);
            game.RegisterPlatform(platformConsole);
            context.Games.Add(game);

            game = new Game();
            game.Key = "003";
            game.Name = "Uncharted 3: Drake's Deception";
            game.Description = "Uncharted 3: Drake's Deception is a 2011 action-adventure third-person shooter " +
                "platform video game, the third game in the Uncharted series, developed by Naughty Dog, " +
                "with a story written by script-writer Amy Hennig. It is the sequel to one of the most critically " +
                "acclaimed video games of 2009, Uncharted 2: Among Thieves.[1] The game was released by Sony Computer" +
                "Entertainment for the PlayStation 3, in North America on November 1, 2011, Europe on November 2, 2011, " +
                "and Australia on November 3, 2011. A Game of the Year Edition, containing all additional content that was " +
                "a part of the Fortune Hunters' Club deal, was released on September 19, 2012, for Europe.";
            game.RegisterGenre(context.Genres.Find("Adventure"));
            game.RegisterGenre(context.Genres.Find("TPS"));
            game.RegisterGenre(context.Genres.Find("Action"));
            game.RegisterPlatform(platformConsole);
            context.Games.Add(game);

            game = new Game();
            game.Key = "004";
            game.Name = "Carmageddon";
            game.Description = "Carmageddon is a graphically violent vehicular combat 1997 PC video game. " +
                "It was later ported to other platforms, and spawned a series of follow-up titles. " +
                "It was inspired by the 1975 cult classic movie Death Race 2000. The game was produced by Stainless Games, " +
                "published by Interplay and SCi.";
            game.RegisterGenre(context.Genres.Find("Races"));
            game.RegisterGenre(context.Genres.Find("Arcade"));
            game.RegisterPlatform(platformDesktop);
            game.RegisterPlatform(platformConsole);
            context.Games.Add(game);

            context.SaveChanges();

            context.Comments.Add(new Comment { AuthorName = "Koleso", Body = "It's good game. I want to buy it!", CommentId = 1, ParentName = null, Game = context.Games.FirstOrDefault() });
            context.Comments.Add(new Comment { AuthorName = "Vanka Vetrov", Body = "Really hard core", CommentId = 2, ParentName = null, Game = context.Games.FirstOrDefault() });
            context.SaveChanges();
            context.Comments.FirstOrDefault().Comments.Add(new Comment { AuthorName = "Kovalchyk Petr", Body = "FFFFFFFFFFFFFFFFFUUUUUUUUUUUUUUUU!", CommentId = 3, ParentName = "Koleso", Comments = null, Game = context.Games.FirstOrDefault() });
            context.Comments.FirstOrDefault().Comments.Add(new Comment { AuthorName = "Nekiy User", Body = "GOOOOOOOOOOOOOD!!!!", CommentId = 4, ParentName = "Koleso", Comments = null, Game = context.Games.FirstOrDefault() });
            context.SaveChanges();
            base.Seed(context);

            context.SaveChanges();
        }
    }
}
