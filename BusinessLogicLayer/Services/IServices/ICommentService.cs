using Model.Entities;
using System.Collections.Generic;


namespace BusinessLogicLayer.Services.IServices
{
    public interface ICommentService
    {
        void Newcomment(string gamekey, Comment comment);

        void Dispose();

        // void newcomment(int parentCommentId, Comment childComment);
        void Newcomment(Comment parentComment, Comment childComment, string gamekey);

        Comment Get(int commentId);

        void Ban(int commentId);

        void Ban(Comment comment);

        void Remove(int commentId);

        void Remove(Comment comment);

        IEnumerable<Comment> GameComments(string gamekey);
      
        void BanUser(BanUser userBan);

        BanUser UserBanDetected(int idUser);
    }
}
