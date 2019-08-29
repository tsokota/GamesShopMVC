using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.IO;
using Yevhenii_KoliesnikTask1.Filters;
using Model.Reporting;
using BusinessLogicLayer.Services;

namespace Yevhenii_KoliesnikTask1.Controllers
{
    [MyAttributeFilter]
    public class GameController : Controller
    {
        // Nlog more info https://github.com/nlog/nlog/wiki/Tutorial
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger(); 
        protected IGameService gameService;
        protected ICommentService commentService;

       public GameController(IGameService gameServices, ICommentService commentServices)
       {
          gameService = gameServices;
          commentService = commentServices;
       }



        // User can create game (POST URL: /games/new). 
        [HttpPost]
        [ActionName("new")]
        public ActionResult New(Game game)
        {
            try
            {
                gameService.New(game);            
                return Json(new { result = "result success" });
            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Game, Action: New" + "\n"+ex.Message);
                return Json(new { result = "Error! Sorry ... " });
            }
           
     
        }

        // User can edit game (POST URL: /games/update)
        [HttpPost]
        [ActionName("update")]
        public ActionResult Update(Game game)
        {
            try
            {
      
                gameService.Update(game);        
                return Json(new { result = "result success" });
            }
            catch (Exception)
            {
               
                return Json(new { result = "Error! Sorry ... " });
            }
        }

        // User can delete game (POST URL: /games/remove)
        [HttpPost]
        [ActionName("remove")]
        public ActionResult DeleteGame(string gamekey)
        {
            try
            {
                gameService.DeleteGame(gamekey);
                return Json(new { result = "result success" });
            }
            catch(Exception)
            {
                return Json(new { result = "Error! Sorry ... " });
            }
        }

        // User can get all games (GET URL: /games)
        [HttpGet]
        [OutputCache(Duration = 60, VaryByParam = "None")]
        public ActionResult AllGames()
        {
            try
            {
               
                return Json(gameService.AllGames(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
              
                return Json(new { result = "Error! Sorry ... " }, JsonRequestBehavior.AllowGet);
            }
        }


        // User can get game details by key (GET URL: /game/{key})
        [HttpGet]
        [OutputCache(Duration = 60, VaryByParam = "None")]
        public ActionResult GameDetails(string gamekey)
        {
            try
            {

                var game = gameService.GameDetails(gamekey);
                return Json(game, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Game, Action: GameDetails" + "\n" + ex.Message);
                return Json(new { result = "Error! Sorry ... " });
            }
        }


        // User can leave comment for game (POST URL: /game/{gamekey}/newcomment)
        [HttpPost]
        public ActionResult newcomment(string gamekey, Comment comment)
        {
            try
            {
                commentService.newcomment(gamekey, comment);
                return Json(new { result = "result success" });
            }
            catch (Exception)
            {
               
                return Json(new { result = "Error! Sorry ... " });
            }
        }


        // User can leave comment for another comment (POST URL: /game/{gamekey}/newcomment) 
        [HttpPost]
        public ActionResult newcomment(Comment parentComment, Comment childComment)
        {
            try
            {
                commentService.newcomment(parentComment.CommentId, childComment);
                return Json(new { result = "result success" });
            }
            catch (Exception)
            {
               
                return Json(new { result = "Error! Sorry ... " });
            }
        }


        // User can get all comments by game key (Get URL: /game/{gamekey}/comments)
        [HttpGet]
        [ActionName("comments")]
        [OutputCache(Duration=60, VaryByParam="None")]
        public ActionResult GameComments(string gamekey)
        {
            try
            {

                var comments =  commentService.GameComments(gamekey.ToString());
                return Json(comments, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                
                return Json(new { result = "Error! Sorry ... " });
            }

        }


        // User can download game (just return any binary file as response) (GET URL: /game/{gamekey}/download)
        [HttpGet]
        public FileInfo downloads(string gamekey)
        {
           return gameService.downloads(gamekey);
        }

        // Disposable pattern
        protected override void Dispose(bool disposing)
        {
            gameService.Dispose();
        }
    }

}

