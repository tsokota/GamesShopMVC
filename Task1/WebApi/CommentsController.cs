using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using Model.Entities;
using BusinessLogicLayer.Services.IServices;
using Yevhenii_KoliesnikTask1.WebApi.ApiDTO;
using AutoMapper;
using NLog.Interface;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.WebApi
{

    [Authorize]
    [ExceptionControllerAttribute]
    public class CommentsController : BaseWebApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService, ILogger logger)
            : base(logger)
        {
            _commentService = commentService;
        }
        [HttpGet]
        // GET api/games/001/Comments
        public IEnumerable<CommentDTO> GetComments(string gamekey)
        {
            var comments = _commentService.GameComments(gamekey);
            return Mapper.Map<IEnumerable<CommentDTO>>(comments);
        }
        [HttpGet]
        // GET api/games/001/Comments/5
        public CommentDTO GetComment(string gamekey, int id)
        {
            Comment comment = _commentService.Get(id);
            if (comment == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return Mapper.Map<CommentDTO>(comment);
        }

        // POST api/games/001/Comments
        [HttpPost]
        public HttpResponseMessage PostComment(string gamekey, Comment comment)
        {
            //var Comment = new Comment {Body = body, User = CurrentUser, AuthorName = authorName, ParentId = parentId};
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (comment.ParentId == 0)
                _commentService.Newcomment(gamekey, comment);
            else
                _commentService.Newcomment(_commentService.Get(comment.ParentId), comment, gamekey);

            return Request.CreateResponse(HttpStatusCode.Created);
        }



        // DELETE api/games/001/Comments/5

        [HttpDelete]
        public HttpResponseMessage DeleteComment(string gamekey, int id, bool cascadeDelete)
        {
            try
            {
                _commentService.Remove(id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}