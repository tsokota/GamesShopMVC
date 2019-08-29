using System;
using System.Collections.Generic;
using BusinessLogicLayer.Services;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System.Linq;
using Yevhenii_KoliesnikTask1.Controllers;
using System.Web.Helpers;
using System.Web.Mvc;
using Model.Reporting;
using System.IO;

namespace GameUnitTest
{
    [TestClass]
    public class GameControllerTest
    {
        GameController controller;
        UnitOfWork uof = new UnitOfWork();
        GameService gameService;
        CommentService commentService;

        [TestInitialize]
        public void Init()
        {
            var comments = new List<Comment>();
            var games = new List<Game>();

            #region InitGames
            Game game = new Game();

            game.Key = "001";
            game.Name = "Warcraft III: Reign of Chaos";
            game.Description = "Warcraft III: Reign of Chaos is a high fantasy real-time strategy video game released " +
                "by Blizzard Entertainment on July 3, 2002 in the U.S. It is the second sequel to Warcraft: Orcs & Humans, " +
                "and it is the third game set in the Warcraft fictional Universe. An expansion pack, The Frozen Throne, " +
                "was released on July 1, 2003.";

            games.Add(game);

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
            games.Add(game);

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
            games.Add(game);

            comments.Add(new Comment
            {
                AuthorName = "Koleso",
                Body = "It's good game. I want to buy it!",
                CommentId = 1,
                ParentName = null,
                Comments = new List<Comment>(),
                Game = games.FirstOrDefault()
            });
            comments.Add(new Comment
            {
                AuthorName = "Vanka Vetrov",
                Body = "Really hard core",
                CommentId = 2,
                ParentName = null,
                Comments = new List<Comment>(),
                Game = games.FirstOrDefault()
            });
            comments.FirstOrDefault().Comments.Add(new Comment
            {
                AuthorName = "Kovalchyk Petr",
                Body = "FFFFFFFFFFFFFFFFFUUUUUUUUUUUUUUUU!",
                CommentId = 3,
                ParentName = "Koleso",
                Comments = null,
                Game = games.FirstOrDefault()
            });
            comments.FirstOrDefault().Comments.Add(new Comment
            {
                AuthorName = "Kovalchyk Petr",
                Body = "FFFFFFFFFFFFFFFFFUUUUUUUUUUUUUUUU!",
                CommentId = 4,
                ParentName = "Koleso",
                Comments = null,
                Game = games.FirstOrDefault()
            });
            comments.FirstOrDefault().Comments.Add(new Comment
            {
                AuthorName = "Nekiy User",
                Body = "GOOOOOOOOOOOOOD!!!!",
                CommentId = 5,
                ParentName = "Koleso",
                Comments = null,
                Game = games.FirstOrDefault()
            });


            #endregion

            var gameRepo = new Mock<GenericRepository<Game>>();
            var commentRepo = new Mock<GenericRepository<Comment>>();
            commentRepo.Setup(x => x.Get(a => true, null, "")).Returns(comments.AsQueryable);
            gameRepo.Setup(x => x.Get(a => true, null, "")).Returns(games.AsQueryable);

            
            uof.GameRepository = gameRepo.Object;
            uof.CommentRepository = commentRepo.Object;
            uof.ViewRepository = new Mock<GenericRepository<EntityView>>().Object;

            gameService = new GameService(uof);
            commentService = new CommentService(uof);

            controller = new GameController(gameService, commentService);
        }


        [TestMethod]
        public void NewGameActionTest()
        {
            var res = new { result = "result success" };

            var actionResult = controller.New(new Game
            {
                Name = "testName",
                Description = "asdasda"
            }) as JsonResult;

            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());
        }

        [TestMethod]
        public void UpdateGameActionTest()
        {
            var res = new { result = "result success" };

            Game game = uof.GameRepository.Get(a => true, null, "").Where(g => g.Key == "001").FirstOrDefault();
            game.Name = "NameNameNewName";

            var actionResult = controller.Update(game) as JsonResult;

            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());
        }

        [TestMethod]
        public void DeleteGameActionTest()
        {
            var res = new { result = "result success" };

            var actionResult = controller.DeleteGame("001") as JsonResult;

            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());
        }

        [TestMethod]
        public void AllGamesActionTest()
        {
            var res = new { result = "result success" };
            List<Game> list = uof.GameRepository.Get(a => true, null, "").ToList<Game>();

            var actionResult = controller.AllGames() as JsonResult;

            Assert.AreEqual(list.ToString(), actionResult.Data.ToString());
        }

        [TestMethod]
        public void GameDetailsActionTest()
        {
            Game game = uof.GameRepository.Get(a => true, null, "").Where(g => g.Key == "001").FirstOrDefault();

            var actionResult = controller.GameDetails(game.Key) as JsonResult;

            Assert.AreEqual(game.ToString(), actionResult.Data.ToString());
        }

        [TestMethod]
        public void AllCommentsActionTest()
        {
            var list = commentService.GameComments("001");

            var actionResult = controller.GameComments("001") as JsonResult;

            Assert.AreEqual(list.ToString(), actionResult.Data.ToString());
        }

        [TestMethod]
        public void newCommentToGameActionTest()
        {
            var res = new { result = "result success" };

            var actionResult = controller.newcomment("001", new Comment()) as JsonResult;

            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());
        }

        [TestMethod]
        public void newCommentToCommentActionTest()
        {
            var res = new { result = "result success" };

            Comment comment = commentService.GameComments("001").FirstOrDefault();
            var actionResult = controller.newcomment(comment, new Comment()) as JsonResult;

            Assert.AreEqual(res.ToString(), actionResult.Data.ToString());
        }

        [TestMethod]
        public void downloadGameActionTest()
        {
            var result = controller.downloads("001");

            Assert.AreEqual(typeof(FileInfo), result.GetType());
        }
    }
}
