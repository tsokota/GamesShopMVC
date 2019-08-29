using AutoMapper;
using BusinessLogicLayer.Services.IServices;
using Model.Entities;
using NLog.Interface;
using System;
using System.Collections.Generic;
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
    public class GenresController : BaseWebApiController
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService, ILogger logger):base(logger)
        {
            _genreService = genreService;
        }

        // GET api/Genres
        public IEnumerable<GenreDTO> GetGenre()
        {
            var genres = _genreService.GetAllItems();
            return Mapper.Map<IEnumerable<GenreDTO>>(genres);
        }

        // GET api/Genres/company1
        public GenreDTO GetGenre(string genreName)
        {
            Genre genre = _genreService.GetAllItems().First(x=>x.Name== genreName);
            if (genre == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Mapper.Map<GenreDTO>(genre);
        }

        // PUT api/Genres/company1
        public HttpResponseMessage PutGenre(string oldGenreName, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (!string.IsNullOrWhiteSpace(oldGenreName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                _genreService.Update(genre);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Genres
        public HttpResponseMessage PostGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _genreService.New(genre);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<GenreDTO>(genre));
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { genreName = genre.Name }));
                return response;
            }
           
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
          
        }

        // DELETE api/Genres/5
        public HttpResponseMessage DeleteGenre(int genreId)
        {
            Genre genre = _genreService.GetById(genreId);
            if (genre == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            //No delete method
            return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
        }
    }
}

