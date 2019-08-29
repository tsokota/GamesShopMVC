using Model;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class GameStoreContextDbInitializer : DropCreateDatabaseAlways<GameStoreContext>
    {
        protected override void Seed(GameStoreContext context)
        {

            context.Database.CreateIfNotExists();
            context.Database.ExecuteSqlCommand("ALTER TABLE Games ADD CONSTRAINT uqc_Key UNIQUE ([Key])");
            context.Database.ExecuteSqlCommand("ALTER TABLE Genres ADD CONSTRAINT uqc_Name UNIQUE ([Name])");
            context.Database.ExecuteSqlCommand("ALTER TABLE Platforms ADD CONSTRAINT uqc_Type UNIQUE ([Name])");
            context.Database.ExecuteSqlCommand("ALTER TABLE Publishers ADD CONSTRAINT uqc_CompanyName UNIQUE ([CompanyName])");
            context.Database.ExecuteSqlCommand("ALTER TABLE Users ADD CONSTRAINT uqc_Login UNIQUE ([Login])");
            context.Database.ExecuteSqlCommand("ALTER TABLE Users ADD CONSTRAINT uqc_Email UNIQUE ([Email])");
            context.Database.ExecuteSqlCommand("ALTER TABLE Roles ADD CONSTRAINT uqc_RoleName UNIQUE ([NameRole])");

            #region Platforms
            Platform platformDesktop = new Platform { Name = "Desktop", IsDeleted=false};
            Platform platformConsole = new Platform { Name = "Console" , IsDeleted=false};

            context.Platforms.Add(new Platform { Name = "Mobile", IsDeleted = false });
            context.Platforms.Add(new Platform { Name = "Browser", IsDeleted = false });
            context.Platforms.Add(platformDesktop);
            context.Platforms.Add(platformConsole);
            #endregion

            context.SaveChanges();

            #region Publishers
            Publisher publisher = new Publisher();
            publisher.CompanyName = "NoPublisher";
            publisher.Description = "Game have no publishers";
            publisher.HomePage = "none";
            publisher.Id = 1;
            publisher.IsDeleted = false;
            context.Publisher.Add(publisher);
            #endregion

            context.SaveChanges();

            #region Genres
            Genre genre = new Genre();
            genre.Name = "Strategy";
            genre.SubGenres = new List<Genre> 
            { 
                new Genre { Name = "RTS", ParentGenre = genre , IsDeleted= false}, 
                new Genre { Name = "TBS", ParentGenre = genre, IsDeleted=false } 
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Action";
            genre.SubGenres = new List<Genre> 
            {
                new Genre { Name = "FPS", ParentGenre = genre , IsDeleted= false},
                new Genre { Name = "TPS", ParentGenre = genre , IsDeleted =false}
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "RPG";
            genre.SubGenres = new List<Genre>
            {
                new Genre { Name = "MMORPG", ParentGenre = genre, IsDeleted= false },
                new Genre { Name = "Action RPG", ParentGenre = genre, IsDeleted = false },
                new Genre { Name = "Tactical RPG", ParentGenre = genre, IsDeleted = false } 
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Sports";
            genre.SubGenres = new List<Genre>
            {
                new Genre { Name = "Football", ParentGenre = genre , IsDeleted = false},
                new Genre { Name = "Golf", ParentGenre = genre, IsDeleted = false }
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Races";
            genre.SubGenres = new List<Genre>
            {
                new Genre { Name = "Rally", ParentGenre = genre, IsDeleted = false },
                new Genre { Name = "Arcade", ParentGenre = genre, IsDeleted= false },
                new Genre { Name = "Formula", ParentGenre = genre, IsDeleted= false },
                new Genre { Name = "Off-road", ParentGenre = genre , IsDeleted =false}
            };
            context.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Adventure";
            genre.SubGenres = new List<Genre>();
            genre.IsDeleted = false;
            context.Genres.Add(genre);
            #endregion

            context.SaveChanges();



            #region Games
            Game game = new Game();

            game.Key = "001";
            game.Name = "Warcraft III: Reign of Chaos";
            game.Description = "Warcraft III: Reign of Chaos is a high fantasy real-time strategy video game released " +
                "by Blizzard Entertainment on July 3, 2002 in the U.S. It is the second sequel to Warcraft: Orcs & Humans, " +
                "and it is the third game set in the Warcraft fictional Universe. An expansion pack, The Frozen Throne, " +
                "was released on July 1, 2003.";
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "Strategy"));
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "RTS"));
            game.RegisterPlatformToGame(platformDesktop);
            game.RegisterPublisherToGame(publisher);
            game.IsDeleted = false;
            game.Price = 10.3;
            game.UnitsInStock = 30;
            game.Discontinued = false;
            game.GameProduction = new DateTime(2000, 6, 20);
            game.Picture = "100x100.gif";
            context.Games.Add(game);
            context.SaveChanges();

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
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "Action"));
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "TPS"));
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "FPS"));
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "Action RPG"));
            game.RegisterPlatformToGame(platformDesktop);
            game.RegisterPlatformToGame(platformConsole);
            game.RegisterPublisherToGame(publisher);
            game.IsDeleted = false;
            game.Discontinued = false;
            game.Price = 7.7;
            game.UnitsInStock = 50;
            game.GameProduction = new DateTime(2003, 4, 10);
            game.Picture = "100x100.gif";
            context.Games.Add(game);
            context.SaveChanges();

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
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "Adventure"));
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "TPS"));
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "Action"));
            game.RegisterPlatformToGame(platformConsole);
            game.RegisterPublisherToGame(publisher);
            game.IsDeleted = false;
            game.Price = 10.7;
            game.UnitsInStock = 36;
            game.Discontinued = false;
            game.GameProduction = new DateTime(2005, 3, 09);
            game.Picture = "100x100.gif";
            context.Games.Add(game);
            context.SaveChanges();

            game = new Game();
            game.Key = "004";
            game.Name = "Carmageddon";
            game.Description = "Carmageddon is a graphically violent vehicular combat 1997 PC video game. " +
                "It was later ported to other platforms, and spawned a series of follow-up titles. " +
                "It was inspired by the 1975 cult classic movie Death Race 2000. The game was produced by Stainless Games, " +
                "published by Interplay and SCi.";
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "Races"));
            game.RegisterGenreToGame(context.Genres.FirstOrDefault(g => g.Name == "Arcade"));
            game.RegisterPlatformToGame(platformDesktop);
            game.RegisterPlatformToGame(platformConsole);
            game.RegisterPublisherToGame(publisher);
            game.IsDeleted = false;
            game.Price = 77.7;
            game.UnitsInStock = 33;
            game.Discontinued = false;
            game.GameProduction = new DateTime(2004, 4, 12);
            game.Picture = "100x100.gif";
            context.Games.Add(game);
            #endregion

            context.SaveChanges();


          

            context.SaveChanges();

            #region Role
            Role r1 = new Role
            {
               NameRole = "Administrator"
            };
            Role r2 = new Role
            {
                NameRole = "Manager"
            };
            Role r3 = new Role
            {
                NameRole = "Moderator"
            };
            Role r4 = new Role
            {
                NameRole = "User"
            };
            Role r5 = new Role
            {
                NameRole = "Guest"
            };
            Role r6 = new Role
            {
                NameRole = "Publisher"
            };


            context.Roles.Add(r1);
            context.Roles.Add(r2);
            context.Roles.Add(r3);
            context.Roles.Add(r4);
            context.Roles.Add(r5);
            context.Roles.Add(r6);
            #endregion
            context.SaveChanges();


            #region User
            User u1 = new User
            {
                Login = "admin",
                Email = "admin@gmail.com",
                UserRoles = new List<Role> { r1 },
                Password = "password",
                Birthdate = new DateTime(1993, 07, 26)
                
            };
            User u2 = new User
            {
                Login = "manager",
                Email = "manager@gmail.com",
                UserRoles = new List<Role> { r2 },
                Password = "password",
                Birthdate = new DateTime(1993,07,26)
            };
            User u3 = new User
            {
                Login = "moderator",
                Email = "moderator@gmail.com",
                UserRoles = new List<Role> { r3 },
                Password = "password",
                Birthdate = new DateTime(1993, 07, 26)
            };
            User u4 = new User
            {
                Login = "user",
                Email = "user@gmail.com",
                UserRoles = new List<Role> { r4 },
                Password = "password",
                Birthdate = new DateTime(1993, 07, 26)
            };
            User u6 = new User
            {
                Login = "publisher",
                Email = "publisher@gmail.com",
                UserRoles = new List<Role> { r6 },
                Password = "password",
                Birthdate = new DateTime(1993, 07, 26)
            };


            User u7 = new User
            {
                Login = "user2",
                Email = "user2@gmail.com",
                UserRoles = new List<Role> { r4 },
                Password = "password",
                Birthdate = new DateTime(1993, 07, 26)
            };

            User u8 = new User
            {
                Login = "user3",
                Email = "user3@gmail.com",
                UserRoles = new List<Role> { r4 },
                Password = "password",
                Birthdate = new DateTime(1993, 07, 26)
            };

            User u9 = new User
            {
                Login = "user4",
                Email = "user4@gmail.com",
                UserRoles = new List<Role> { r4 },
                Password = "password",
                Birthdate = new DateTime(1993, 07, 26)
            };

            context.Users.Add(u1);
            context.Users.Add(u2);
            context.Users.Add(u3);
            context.Users.Add(u4);
            context.Users.Add(u6);
            context.Users.Add(u7);
            context.Users.Add(u8);
            context.Users.Add(u9);
            base.Seed(context);
            #endregion


            
            #region Comments
            Comment comment = new Comment
            {
                AuthorName = "Koleso",
                Body = "It's good game. I want to buy it!",
                Id = 1,
                ParentName = null,
                Game = context.Games.FirstOrDefault(), 
                User = u4
            };

            context.Comments.Add(comment);
            comment = new Comment
            {
                AuthorName = "Vanka Vetrov",
                Body = "Really hard core",
                Id = 2,
                ParentName = null,
                Game = context.Games.FirstOrDefault(),
                User = u7
            };
            context.Comments.Add(comment);
            context.SaveChanges();

            comment = new Comment
            {
                AuthorName = "Kovalchyk Petr",
                Body = "FFFFFFFFFFFFFFFFFUUUUUUUUUUUUUUUU!",
                Id = 3,
                ParentName = "Koleso",
                Comments = null,
                Game = context.Games.FirstOrDefault(),
                User = u8
            };
            context.Comments.FirstOrDefault().Comments.Add(comment);

            comment = new Comment
            {
                AuthorName = "Nekiy User",
                Body = "GOOOOOOOOOOOOOD!!!!",
                Id = 4,
                ParentName = "Koleso",
                Comments = null,
                Game = context.Games.FirstOrDefault(),
                User = u9
            };
            context.Comments.FirstOrDefault().Comments.Add(comment);
            #endregion

            context.SaveChanges();
            #region Orders
            OrderDetail orderDetail = new OrderDetail { Id = 0, ProuctId = game.Id.ToString(), Price = game.Price, Quantity = 1, Discount = 0, OrderType = OrderType.Game, Product = game };
            context.OrderDetails.Add(orderDetail);
          
           

            Model.Entities.Order order = new Model.Entities.Order { User = u2, OrderDate = DateTime.Now, OrderDetails = new List<OrderDetail> { orderDetail }, OrderStatus = OrderStatus.InProgress, ShippedDate = null };
            context.Orders.Add(order);

            orderDetail = new OrderDetail { Id = 1, ProuctId = game.Id.ToString(), Price = game.Price, Quantity = 1, Discount = 0, OrderType = OrderType.Game, Product = game };
            context.OrderDetails.Add(orderDetail);
            order = new Model.Entities.Order { User = u2, OrderDate = new DateTime(2012, 7, 25), OrderDetails = new List<OrderDetail> { orderDetail }, OrderStatus = OrderStatus.InProgress, ShippedDate = null };
            context.Orders.Add(order);

            orderDetail = new OrderDetail { Id = 2, ProuctId = game.Id.ToString(), Price = game.Price, Quantity = 1, Discount = 0, OrderType = OrderType.Game, Product = game };
            context.OrderDetails.Add(orderDetail);
         
            order = new Model.Entities.Order { User = u4, OrderDate = new DateTime(2013, 6, 13), OrderDetails = new List<OrderDetail> { orderDetail }, OrderStatus = OrderStatus.InProgress, ShippedDate = null };
            context.Orders.Add(order);
            #endregion

            context.SaveChanges();

            #region localization
            var enLanguage = new Language() { Id = 1, Code = "en", Name = "English" };
            var ruLanguage = new Language() { Id = 2, Code = "ru", Name = "Russian" };
            context.Language.Add(enLanguage);
            context.Language.Add(ruLanguage);
            var dota2GameLang = new GameLang() { Id = 1, LanguageId = 2, Description = "компьютерная многопользовательская командная игра жанра MOBA, реализация известной карты DotA для игры Warcraft III в отдельном клиенте. Осенью 2009 года компания Valve приняла на работу основного разработчика DotA — IceFrog" };
            context.GameLang.Add(dota2GameLang);
            #endregion


            BanUser banUser = new BanUser();
            banUser.IdUser = 8;
            banUser.ReasonBan = "test ban";
            banUser.BeginBan = DateTime.Now;
            banUser.LastBan = DateTime.Now.AddMonths(1);
            context.BanUser.Add(banUser);
            context.SaveChanges();



            base.Seed(context);
            context.SaveChanges();


        }
    }
}
