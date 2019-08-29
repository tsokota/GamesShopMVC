using AutoMapper;
using BusinessLogicLayer.Services.IServices;
using Model.Entities;
using NLog.Interface;
using System;
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
    public class OrdersController : BaseWebApiController
    {
        private IOrderService orderService;

        public OrdersController(IOrderService orderService, ILogger logger):base(logger)
        {
            this.orderService = orderService;
        }

        // GET api/Orders
        public OrderDTO GetOrder()
        {
            Order order = orderService.GetOrderByClientId(CurrentUser.Id).FirstOrDefault();
            if (order == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Mapper.Map<OrderDTO>(order);
        }

        // POST api/Orders/5
        public HttpResponseMessage PostOrder(int orderId, Order order)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (orderId != order.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                orderService.Update(order);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Orders
        public HttpResponseMessage PostOrder(Order order)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            try
            {
                orderService.New(order);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, order);
            return response;
        }
    }
}