using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BusinessLogicLayer.Services
{
    public class CommentService : ICommentService
    {

        // Nlog more info https://github.com/nlog/nlog/wiki/Tutorial
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private UnitOfWork unitOfWork;

        public CommentService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public CommentService()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public CommentService(GenericRepository<Comment> rep_Com, GenericRepository<Game> rep_Game)
        {
            this.unitOfWork = new UnitOfWork();
            this.unitOfWork.CommentRepository = rep_Com;
            this.unitOfWork.GameRepository = rep_Game;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void newcomment(string gamekey, Comment comment)
        {
            try
            {
                if (comment == null)
                {
                    throw new ArgumentNullException("null comment data");
                }
                unitOfWork.CommentRepository.Insert(comment);
                unitOfWork.Save();
                logger.Debug("result succsess - leave comment: {0}, {1}", comment.AuthorName, comment.Body);
                logger.Debug("Action  -> newcomment have worked success!");
               
            }
            catch (Exception ex)
            {
                logger.Error("some error: Controller: Game, Action: newcomment" + "\n" + ex.Message);

            }
        }

        public void newcomment(int parentCommentId, Comment childComment)
        {
            try
            {
                var parentComment = unitOfWork.CommentRepository.GetByID(parentCommentId);

                if (childComment == null)
                {
                    throw new ArgumentNullException("null comment data");
                }

                if (parentComment == null)
                {
                    throw new ArgumentException("Parent comment does not exist");
                }


                parentComment.Comments.Add(childComment);
                childComment.ParentName = parentComment.AuthorName;

                unitOfWork.CommentRepository.Update(parentComment);

                //unitOfWork.CommentRepository.GetByID(childComment.ParentName).Comments.Add(childComment);

                unitOfWork.Save();
                logger.Debug("result succsess - leave comment to comment: {0}, {1}, parentcommnet: {2}", childComment.AuthorName, childComment.Body, childComment.ParentName);
                logger.Debug("Action  -> newcomment have worked success!");

            }
            catch (Exception ex)
            {
                logger.Error("some error:  newcomment(Comment comment)" + "\n" + ex.Message);

            }
        }

        public IEnumerable<Comment> GameComments(string gamekey)
        {
            try
            {
                var game = unitOfWork.GameRepository
                    .Get(a => true).Where(x => x.Key == gamekey).SingleOrDefault();

                if (game == null)
                {
                    logger.Error("game can't be found");
                    return null;
                }

                var comments = unitOfWork.CommentRepository.Get(a => true).Where(x => x.Game.Key == gamekey).AsEnumerable();
                logger.Info("result succsess");
                return comments;
                
            }
            catch (Exception ex)
            {
                logger.Error("some error:  GameComments" + "\n" + ex.Message);
                return null;
               
            }
        }
    }
}
