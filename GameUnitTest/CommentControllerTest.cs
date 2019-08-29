using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.UnitOfWorks;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities;
using Model.Reporting;
using Moq;
using Yevhenii_KoliesnikTask1.Controllers;

namespace GameUnitTest
{
    [TestClass]
    public class CommentControllerTest
    {
        /*
        CommentController controller;
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

            controller = new CommentController(gameService, commentService);
            controller.Url = new UrlHelper(new RequestContext());
        }

        [TestMethod]
        public void GameCommentsCommentsActionTest()
        {
            string gamekey = "001";

            var actionResult = controller.GameComments(gamekey) as ViewResult;

            Assert.AreEqual("GameComments", actionResult.ViewName);

        }

        [TestMethod]
        public void GameCommentsAction_ModelTest()
        {
            string gamekey = "001";

            var commentList = commentService.GameComments(gamekey);
            var actionResult = controller.GameComments(gamekey) as ViewResult;

            Assert.IsInstanceOfType(actionResult.Model, typeof(CommentViewModel));

            var tmpModel = (CommentViewModel)actionResult.Model;

            Assert.AreEqual(commentList.Count(), tmpModel.CommentList.Count);
        }


        [TestMethod]
        public void GameCommentsAction_NULLTest()
        {
            var actionResult = controller.GameComments(null) as ViewResult;

            Assert.AreEqual(actionResult, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void newCommentToGameAction_NULLViewModelTest()
        {
            var gamekey = "001";
            var routParams = new RouteValueDictionary(new { controller = "Game", action = "comments", gamekey = gamekey });

            var actionResult = controller.Newcomment(gamekey, new Comment(), null) as RedirectToRouteResult;

            Assert.AreEqual(routParams.ToString(), actionResult.RouteValues.ToString());
        }

        [TestMethod]
        public void newCommentToGameActionTest_NULLGameKeyTest()
        {
            var actionResult = controller.Newcomment(null, new Comment()) as RedirectToRouteResult;

            Assert.AreEqual(actionResult, null);
        }

        [TestMethod]
        public void newCommentToGameActionTest_NULLCommentTest()
        {
            var actionResult = controller.Newcomment("001", null) as RedirectToRouteResult;

            Assert.AreEqual(actionResult, null);
        }

        [TestMethod]
        public void newCommentToGameActionTest_NULLTest()
        {
            var actionResult = controller.Newcomment(null, null) as RedirectToRouteResult;

            Assert.AreEqual(actionResult, null);
        }

        [TestMethod]
        public void RemoveCommentActionTest()
        {
            int commentId = 1;

            var comment = commentService.Get(commentId);
            var actionResult = controller.Remove(commentId) as RedirectToRouteResult;
            var expectedResult = new RedirectToRouteResult("Comments", new RouteValueDictionary(new { gamekey = comment.Game.Key, action = "comments", controller = "Comment" }));

            Assert.AreEqual(expectedResult.RouteName, actionResult.RouteName);
        }

        [TestMethod]
        public void RemoveCommentAction_WrongIDTest()
        {
            var actionResult = controller.Remove(-1);

            Assert.IsInstanceOfType(actionResult, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void BanCommentAction_Test()
        {
            string gameKey = "001";

            Comment comment = commentService.Get(1);

            var actionResult = controller.Ban(comment.CommentId) as RedirectToRouteResult;

            var expectedResult = new RedirectToRouteResult("Comments", new RouteValueDictionary(new { gamekey = gameKey }));

            Assert.AreEqual(expectedResult.RouteName, actionResult.RouteName);
        }

        [TestMethod]
        public void BanCommentAction_ModelTest()
        {
            int commentId = 1;

            controller.Ban(commentId);

            Comment comment = commentService.Get(commentId);

            Assert.AreEqual(comment.Body, "Удалено модератором!");
        }

        [TestMethod]
        public void BanCommentAction_WrongIDTest()
        {
            var actionResult = controller.Ban(-1);

            Assert.IsInstanceOfType(actionResult, typeof(HttpNotFoundResult));
        }

        //[TestMethod]
        //public void newCommentToCommentActionTest()
        //{
        //    var res = new { result = "result success" };
        //    string gamekey = "001";

        //    Comment comment = commentService.GameComments(gamekey).FirstOrDefault();

        //    CommentViewModel model = new CommentViewModel();
        //    model.Comment = new Comment();
        //    model.ParentComment = comment;
        //    model.GameKey = gamekey;

        //    var actionResult = controller.Newcomment(gamekey, comment, model) as ViewResult;

        //    Assert.AreEqual(model.ToString(), actionResult.Model.ToString());
        //}

        //[TestMethod]
        //public void newCommentToCommentPostActionTest()
        //{

        //    string gamekey = "001";

        //    var routParams = new RouteValueDictionary(new { controller = "Game", action = "comments", gamekey = gamekey });

        //    var viewModel = new CommentViewModel
        //    {
        //        Comment = new Comment
        //        {
        //            AuthorName = String.Empty,
        //            Body = String.Empty
        //        },
        //        GameKey = gamekey,
        //        ParentComment = commentService.GameComments(gamekey).FirstOrDefault(),
        //        QuoteTag = ""
        //    };

        //    var actionResult = controller.Newcomment(viewModel) as RedirectToRouteResult;

        //    Assert.AreEqual(routParams.ToString(), actionResult.RouteValues.ToString());
        //}

        //[TestMethod]
        //public void newCommentToCommentPostAction_AddCommentTest()
        //{
        //    string gamekey = "001";
        //    var commentsCount = commentService.GameComments(gamekey).FirstOrDefault().Comments.Count();

        //    var viewModel = new CommentViewModel
        //    {
        //        Comment = new Comment
        //        {
        //            AuthorName = String.Empty,
        //            Body = String.Empty
        //        },
        //        GameKey = gamekey,
        //        ParentComment = commentService.GameComments(gamekey).FirstOrDefault(),
        //        QuoteTag = ""
        //    };

        //    controller.Newcomment(viewModel);

        //    Assert.AreEqual(commentsCount + 1, commentService.GameComments(gamekey).FirstOrDefault().Comments.Count());
        //}


        //[TestMethod]
        //public void newCommentToCommentPostAction_NULLModelTest()
        //{

        //    var actionResult = controller.Newcomment((CommentViewModel)null) as RedirectToRouteResult;

        //    Assert.AreEqual(null, actionResult);
        //}
         * */
    }
}
