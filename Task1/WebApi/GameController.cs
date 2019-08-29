using AutoMapper;
using BusinessLogicLayer.Services.IServices;
using Model.Entities;
using Model.Filtering;
using NLog.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yevhenii_KoliesnikTask1.Filters;
using Yevhenii_KoliesnikTask1.WebApi.ApiDTO;

namespace Yevhenii_KoliesnikTask1.WebApi
{
    [Authorize]
    [ExceptionControllerAttribute]
    public class GamesController : BaseWebApiController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService, ILogger logger)
            : base(logger)
        {
            _gameService = gameService;
        }

  

        // GET api/Games/001
        public GameDTO GetGame(string gamekey)
        {
            Game game = _gameService.GetByKey(gamekey, CurrentLangCode);

            if (game == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Mapper.Map<GameDTO>(game);
        }

        // PUT api/Games/001
        [Authorize(Roles = "Manager")]       
        public HttpResponseMessage PutGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _gameService.Update(game);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Authorize(Roles = "Manager")]
        // POST api/Games
        public HttpResponseMessage PostGame(Game game)
        {
            if (ModelState.IsValid)
            {
                _gameService.New(game);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<GameDTO>(game));
                return response;
            }


            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

        }

        [Authorize(Roles = "Manager")]
        // DELETE api/Games/warcraft
        public HttpResponseMessage DeleteGame(string gamekey)
        {
            try
            {
                _gameService.Delete(_gameService.GetByKey(gamekey, CurrentLangCode));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route("api/{lang}/games/{id}/genres")]
        [HttpGet]
        public HttpResponseMessage GenresOfGame(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _gameService.GetById(id).Genres.ToList());
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }


        // GET api/Games
        public IEnumerable<GameDTO> GetGames([FromUri] FilterArgs filters, [FromUri] PaginationArgs paginations)
        {
            if (filters == null)
            {
                filters = new FilterArgs();
            }
            if (paginations == null)
            {
                paginations = new PaginationArgs();
            }
            var games = _gameService.Get(filters, CurrentLangCode);

            paginations.TotalItems = games.Count();
            paginations.TotalPages = (paginations.TotalItems / paginations.CountPerPage) + 1;
            games = games.Skip((paginations.PageNumber - 1) * paginations.CountPerPage).Take(paginations.CountPerPage);
            return Mapper.Map<IEnumerable<GameDTO>>(games);
        }
    }
}
