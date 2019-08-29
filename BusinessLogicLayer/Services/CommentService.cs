using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities;
using BusinessLogicLayer.Services.UnitOfWorks;
using BusinessLogicLayer.Services.IServices;
using AutoMapper;
using NLog.Interface;

namespace BusinessLogicLayer.Services
{
    public class CommentService : ICommentService
    {

        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public void Newcomment(string gamekey, Comment comment)
        {
            try
            {
               var keySplit = gamekey.Split('-');
                var game = new Game();
                if (comment == null)
                {
                    _logger.Error("comment is null CommentService.cs");
                    throw new ArgumentNullException("comment");

                }
                // all games from Northwnd have key -> NorthWind-{GameId}
                var gameInmainDb = _unitOfWork.GameRepository.Get().FirstOrDefault(x=>String.Equals(gamekey,x.Key));
                if (gameInmainDb == null)
                {
                    Product product = _unitOfWork.ProductRepository.Get().FirstOrDefault(x => x.ProductID == Int32.Parse(keySplit[1]));
                    game = Mapper.Map<Game>(product);
                    game.GameProduction = DateTime.Now;                   
                    _unitOfWork.GameRepository.Insert(game);
                    _unitOfWork.Save();
                    game = _unitOfWork.GameRepository.Get(x => true).FirstOrDefault(g => g.Key == gamekey);
                }
                else
                {

                    game = _unitOfWork.GameRepository.Get(x => true).FirstOrDefault(g => g.Key == gamekey);
                }
                if (game == null)
                {
                    _logger.Error("game is null CommentService.cs");
                    throw new ArgumentException("unknown game");
                }
                comment.Game = game;
                game.Comments.Add(comment);

                _unitOfWork.CommentRepository.Insert(comment);
                _unitOfWork.Save();
            

            }
            catch (Exception ex)
            {
                _logger.Error("some error: CommentSrvice.cs" + "\n" + ex.Message);
                throw;
            }
        }

        public void Newcomment(int parentCommentId, Comment childComment)
        {
            try
            {
                var parentComment = _unitOfWork.CommentRepository.GetById(parentCommentId);

                if (childComment == null)
                {
                    _logger.Error("newcomment, comment is null, CommentService.cs");
                    throw new ArgumentNullException("childComment");
                }

                if (parentComment == null)
                {
                    _logger.Error("newcomment, parentcomment is null, CommentService.cs");
                    throw new ArgumentException("Parent comment does not exist");
                }

                parentComment.Comments.Add(childComment);
                childComment.ParentName = parentComment.AuthorName;
                _unitOfWork.CommentRepository.Update(parentComment);
                _unitOfWork.Save();
                _logger.Debug("result succsess - leave comment to comment: {0}, {1}, parentcommnet: {2}", childComment.AuthorName, childComment.Body, childComment.ParentName);
                _logger.Debug("Action  -> newcomment have worked success! ");

            }
            catch (Exception ex)
            {
                _logger.Error("some error:  newcomment(Comment comment) CommentService.cs " + "\n" + ex.Message);

            }
        }

        public void Newcomment(Comment parentComment, Comment childComment, string gamekey)
        {
            if (parentComment == null)
            {
                _logger.Error("parent comment is null, CommentService.cs");
                throw new ArgumentNullException();
            }
            if (childComment == null)
            {
                _logger.Error("child comment is null, CommentService.cs");
                throw new ArgumentNullException();
            }
            if (gamekey == string.Empty)
            {
                _logger.Error("string key is empty CommentService.cs");
                throw new ArgumentNullException();
            }
            var game = _unitOfWork.GameRepository.Get().FirstOrDefault(x => x.Key == gamekey);
            childComment.Game = game;
            parentComment.Comments.Add(childComment);
            childComment.ParentName = parentComment.AuthorName;

            _unitOfWork.CommentRepository.Insert(childComment);
            _unitOfWork.CommentRepository.Update(parentComment);
            _unitOfWork.Save();
        }

        public void BanUser(BanUser userBan)
        {

            _unitOfWork.BanUserRepository.Insert(userBan);
            _unitOfWork.Save();
        }

        public IEnumerable<Comment> GameComments(string gamekey)
        {
            try
            {
                if (gamekey == null)
                {
                    _logger.Error("gameKey is null, CommentService.cs");
                    throw new ArgumentNullException("gamekey");
                }
                var comments = _unitOfWork.CommentRepository.Get(a => true, null, "User").Where(x => x.Game.Key == gamekey).AsEnumerable();
                _logger.Info("result succsess, CommentService.cs");
                return comments.Where(x=>x.ParentName == null);

            }
            catch (Exception ex)
            {
                _logger.Error("some error:  GameComments, CommentService.cs" + "\n" + ex.Message);
                throw;

            }
        }

        public Comment Get(int commentId)
        {
            try
            {
                return _unitOfWork.CommentRepository.Get(a => true).FirstOrDefault(c => c.Id == commentId);
            }
            catch (Exception ex)
            {
                _logger.Error("some error:  Get(int commentId), CommentService.cs" + "\n" + ex.Message);
                throw;
            }
        }

        public void Ban(int commentId)
        {
            try
            {
                var comment = Get(commentId);
                comment.Body = "Удалено модератором!";

                _unitOfWork.CommentRepository.Update(comment);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error:  Ban(int commentId), CommentService.cs" + "\n" + ex.Message);
                throw;
            }
        }

        public void Ban(Comment comment)
        {
            try
            {
                if (Get(comment.Id) == null)
                {
                    throw new ArgumentException("Unknown comment");
                }

                comment.Body = "Удалено модератором!";

                _unitOfWork.CommentRepository.Update(comment);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: Ban(Comment comment), CommentService.cs" + "\n" + ex.Message);
                throw;
            }
        }

        public void Remove(int commentId)
        {
            try
            {
                _unitOfWork.CommentRepository.Delete(commentId);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: Remove(int commentId), CommentService.cs" + "\n" + ex.Message);
                throw;
            }
        }

        public void Remove(Comment comment)
        {
            try
            {
                _unitOfWork.CommentRepository.Delete(comment);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: Remove(Comment comment), CommentService.cs" + "\n" + ex.Message);
                throw;
            }
        }

        public BanUser UserBanDetected(int idUser)
        {
            try
            {
                return _unitOfWork.BanUserRepository.Get().Where(user => user.IdUser == idUser).FirstOrDefault(user => user.LastBan > DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.Error("some error: UserBanDetected(int idUser), CommentService.cs" + "\n" + ex.Message);
                throw;
            }
        }
    }
}
