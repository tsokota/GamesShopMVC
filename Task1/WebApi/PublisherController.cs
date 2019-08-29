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
    public class PublishersController : BaseWebApiController
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService, ILogger logger):base(logger)
        {
            _publisherService = publisherService;
        }

        // GET api/Publishers
        public IEnumerable<PublisherDTO> GetPublishers()
        {
            var publishers = _publisherService.GetAllItems();
            return Mapper.Map<IEnumerable<PublisherDTO>>(publishers);
        }

        // GET api/Publishers/company1
        public PublisherDTO GetPublisher(string companyName)
        {
            Publisher publisher = _publisherService.GetAllItems().FirstOrDefault(x=>x.CompanyName==companyName);
            if (publisher == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Mapper.Map<PublisherDTO>(publisher);
        }

        // PUT api/Publishers/company1
        public HttpResponseMessage PutPublisher( Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }     

            try
            {
                _publisherService.Update( publisher);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Publishers
        public HttpResponseMessage PostPublisher(Publisher publisher)
        {
            if(ModelState.IsValid)
            {
                _publisherService.New(publisher);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<PublisherDTO>(publisher));
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { companyName = publisher.CompanyName }));
                return response;
            }         
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        // DELETE api/Publishers/5
        public HttpResponseMessage DeletePublisher(string companyName)
        {
            Publisher publisher = _publisherService.GetAllItems().FirstOrDefault(x=>x.CompanyName == companyName);
            if (publisher == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            //No delete method

            return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
        }
    }
}