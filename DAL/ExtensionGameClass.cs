using Model.Entities;
using System.Collections.Generic;


namespace DAL
{
    public static class ExtensionGameClass
    {    //this method need for Game class, corect initializer it, and POCO class - Game hav'nt this methods in its structure
        public static void RegisterGenreToGame(this Game game, Genre genre)
        {
            if(game.Genres ==null)
                game.Genres = new List<Genre>();

            game.Genres.Add(genre);

            if (genre.Games == null)
            {
                genre.Games = new List<Game>();
            }

            genre.Games.Add(game);
        }

        public static void UnregisterGenresFromGame(this Game game)
        {
            game.Genres = new List<Genre>();
            foreach(var g in game.Genres)
            {
                g.Games.Remove(game);
            }

         
        }

        public static void RegisterPlatformToGame(this Game game, Platform platform)
        {
            game.Platforms = new List<Platform>();
            game.Platforms.Add(platform);

            if (platform.Games == null)
            {
                platform.Games = new List<Game>();
            }

            platform.Games.Add(game);
        }

        public static void UnregisterPlatformFromGame(this Game game)
        {
            game.Platforms = new List<Platform>();
            foreach(var p in game.Platforms)
            {
                p.Games.Remove(game);
            }

           
        }

        public static void RegisterPublisherToGame(this Game game, Publisher publisher)
        {
            
            game.Publisher = publisher;

            if (publisher.Games == null)
            {
                publisher.Games = new List<Game>();
            }

            publisher.Games.Add(game);
        }


    }
}
