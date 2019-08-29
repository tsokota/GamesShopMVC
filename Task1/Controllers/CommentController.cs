using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.ViewModel;
using Model.Entities;
using NLog.Interface;
using System;
using System.Linq;
using System.Web.Mvc;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.Controllers
{
    [Authorize]
    [ExceptionControllerAttribute]
    public class CommentController : BaseController
    {
        // Nlog more info https://github.com/nlog/nlog/wiki/Tutorial


        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentServices, ILogger logger)
            : base(logger)
        {

            _commentService = commentServices;

        }
        // User can leave comment for game (POST URL: /game/{gamekey}/newcomment)
        [HttpPost]
        [ActionName("newcomment")]
        [ValidateInput(false)]
        public ActionResult Newcomment(string gamekey = "", Comment comment = null, CommentViewModel model = null)
        {

            // try again input value, for solve problem with error model
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fill field correctly");
                if (model != null)
                {
                    model.CommentList = _commentService.GameComments(gamekey).ToList();
                    return View("GameComments", model);
                }
            }

            // detect isBan user? 
            BanUser banUser = _commentService.UserBanDetected(CurrentUser.Id);
            if (banUser != null)
                return View("BanMessage", banUser);
            comment.User = CurrentUser;

            // if answer was quote 
            if (model.QuoteTag != null)
            {

                model.Comment.User = CurrentUser;
                model.Comment.Body += model.QuoteTag;
                _commentService.Newcomment(model.ParentComment, model.Comment, model.GameKey);
                return GameComments(gamekey);

            }
            // if answer was answer to some comment
            if (model.ParentComment != null)
            {
                model.Comment.User = CurrentUser;
                _commentService.Newcomment(model.ParentComment, model.Comment, model.GameKey);
            }
            else
            {

                _commentService.Newcomment(gamekey, comment);
            }

            return GameComments(gamekey);



        }

        /// <summary>
        /// User can get all comments by game key (Get URL: /game/{gamekey}/comments)
        /// </summary>
        /// <param name="gamekey"></param>
        /// <param name="parentCommentId"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpGet]     
        //[OutputCache(Duration=60, VaryByParam="None")]
        public ActionResult GameComments(string gamekey = "", int parentCommentId = 0, string answer = "")
        {

            CommentViewModel model = new CommentViewModel();
            var comments = _commentService.GameComments(gamekey).ToList();
            model.CommentList = comments.ToList();
            if (gamekey == string.Empty)
            {
                Logger.Error("GameComment(), GameController, gamekey is null");
                throw new ArgumentNullException("gamekey");
            }
            // if answer == null , it is simlpe rendering comment view with fied to new comment
            if (answer == string.Empty)
            {
                return View("GameComments", model);
            }
            // if answer != null , it is rendering comment view with fied with FIELD For Answer to AnyBody
            model.Comment = new Comment();
            Comment parentComment = _commentService.Get(parentCommentId);
            model.ParentComment = parentComment;
            model.GameKey = gamekey;
            //   if true => rendering comment  view with field for QUote to Anybody and with quote text
            if (answer == "Quote")
            {
                model.QuoteTag = string.Empty;
                model.QuoteTag += string.Format("<quote> {0}: {1} </quote>",
                parentComment.AuthorName, parentComment.Body);
                return View("GameComments", model);
            }
            return View("GameComments", model);



        }

        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public ActionResult Remove(int commentId = 0)
        {
            var comment = _commentService.Get(commentId);

            if (comment == null)
            {
                Logger.Error("CommentController, Some error in Remove(int CommentId = 0)\n");
                return new HttpNotFoundResult("Comment not found");

            }
            string gameKey = comment.Game.Key;
            _commentService.Remove(comment);
            return RedirectToRoute("Comments", new { gamekey = gameKey, action = "GameComments", controller = "Comment" });
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public ActionResult Ban(int commentId = 0, int userId = 0)
        {
            BanModelView model = new BanModelView();
            BanUser banUser = new BanUser
            {
                IdUser = userId,
                LastBan = new DateTime(),
                BeginBan = new DateTime(),
                IdComment = commentId
            };
            model.UserBan = banUser;
            return View(model);
        }

        /// <summary>
        /// It redirects to page (post) with two options:
        /// ban duration(1 hour, 1 day, 1 week, 1 month, forever) and ban accomplices
        /// (true/false, it means apply ban to all users in this branch).
        /// </summary>
        /// <param>
        ///     <name>BanModelView model</name>
        /// </param>
        /// <param name="model"></param>
        /// <param name="banVariant"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public ActionResult Ban(BanModelView model, int banVariant)
        {
            if (!ModelState.IsValid) 
                return View(model);
            var comment = _commentService.Get(model.UserBan.IdComment);
            if (comment == null)
            {
                return new HttpNotFoundResult("Comment not found");
            }
            _commentService.Ban(comment);
            model.BanVariant = banVariant;
            _commentService.BanUser(MakeBan(model));
            return RedirectToAction("GameComments");
        }

        [NonAction]
        public BanUser MakeBan(BanModelView model)
        {
            model.UserBan.BeginBan = DateTime.Now;
            model.UserBan.LastBan = DateTime.Now;
            switch (model.BanVariant)
            {
                case 1:
                    {
                        model.UserBan.LastBan.AddHours(1);
                        break;
                    }
                case 2:
                    {
                        model.UserBan.LastBan.AddDays(1);
                        break;
                    }
                case 3:
                    {
                        model.UserBan.LastBan.AddMonths(1);
                        break;
                    }
                case 4:
                    {
                        model.UserBan.LastBan.AddYears(2000);
                        break;
                    }
            }
            return model.UserBan;
        }

    }
}

