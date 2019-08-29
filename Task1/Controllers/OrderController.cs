using BusinessLogicLayer.Payment.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Model.Entities;
using BusinessLogicLayer.ViewModel;
using BusinessLogicLayer.Services.IServices;
using Model.Filtering;
using Model.Payments.Args;
using AutoMapper;
using NLog.Interface;
using Yevhenii_KoliesnikTask1.Filters;


namespace Yevhenii_KoliesnikTask1.Controllers
{
    [ExceptionControllerAttribute]
    public class OrderController : BaseController
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderServices, ILogger logger)
            : base(logger)
        {
            _orderService = orderServices;
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {

            var orders = _orderService.GetAllItems().ToList();
            return View(orders);

        }

        [HttpGet]
        [Authorize]
        public ActionResult AddGameToBucket(Game game)
        {

            int userId = CurrentUser.Id;
            _orderService.AddGame(game, userId);
            return RedirectToAction("GoToBusket");
        }

        [HttpGet]
        [Authorize]  // only authorization user
        public ActionResult GoToBusket()
        {
            int userId = CurrentUser.Id;
            Order order = _orderService.GetOrderByClientId(userId).FirstOrDefault();
            return View(order);
        }

        [HttpGet]
        public ActionResult MakeOrder(int orderId = 0)
        {


            var orderDetails = new PaymentOrderViewModel
            {
                Order = _orderService.GetById(orderId),

                PaymentMethods = new List<BusinessLogicLayer.Payment.PaymentMethod> {
                    new VisaMethod(),
                    new IBOXMethod(),
                    new BankMethod()
                   
                }

            };
            return View(orderDetails);


        }

        [HttpPost]
        public ActionResult MakeOrder(string payment, int orderId)
        {
            return RedirectToRoute("PaymentsRoute", new { action = payment, id = orderId });
        }

        [HttpGet]
        public ActionResult Visa(int id = 0)
        {

            var order = _orderService.GetById(id);
            var model = new VisaViewModel { OrderId = order.Id, Price = order.OrderDetails.Sum(o => o.Price), UserId = 1 };
            return View("Payments/Visa", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Visa(VisaViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var order = _orderService.GetById(model.OrderId);
                model.OrderId = order.Id;
                return View("Payments/Visa", model);
            }
            VisaMethod payment = new VisaMethod();
            var result = payment.Pay(new VisaPayArgs
             {
                 OrderId = model.OrderId,
                 UserId = model.UserId,
                 CardHoldersName = model.UserName,
                 CarNumber = model.CardNumber,
                 ExpiryDate = model.DateToExpire,
                 CVV2_CVC2 = model.CVV2
             });
            _orderService.MakeOrder(model.OrderId);
            return View("FinishPayment");
        }

        public ActionResult Bank()
        {
            int userId = CurrentUser.Id;
            var order = _orderService.GetById(userId);
            return File(new byte[] { }, "application/txt", "invoice.txt");
        }

        [HttpGet]
        public ActionResult IBOX()
        {

            var order = _orderService.GetById(CurrentUser.Id);
            var model = new IBOXViewModel
            {
                OrderId = order.Id,
                UserId = CurrentUser.Id,
                PriceOrder = order.OrderDetails.Sum(o => o.Price)
            };
            return View("Payments/IBOX", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IBOX(IBOXViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View("Payments/IBOX", model);
            }
            IBOXMethod payment = new IBOXMethod();

            payment.Pay(new iboxPayArgs
            {
                InvoiceNumber = model.OrderId.ToString(),
                AccountNumber = model.UserId.ToString(),
                Sum = model.PriceOrder,
                UserId = model.UserId
            });

            return RedirectToRoute("GameActions");

        }



        /// <summary>
        ///  GET: ~/orders/history
        /// GET URL:/Orders/history should display history of orders between selected dates. 
        /// List of orders should contain orders from both databases.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpGet, ActionName("history")]
        public ActionResult GetHistory()
        {

            OrderHistoryViewModel ohvm = new OrderHistoryViewModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                OrderList = _orderService.GetOrdersHistory(DateTime.Now, DateTime.Now).ToList()
            };

            return View("OrderHistory", ohvm);


        }

        [HttpPost, ActionName("history")]
        public ActionResult GetHistory(OrderHistoryViewModel model)
        {

            model.OrderList = _orderService.GetOrdersHistory(model.StartDate, model.EndDate).ToList();
            return View("OrderHistory", model);

        }



        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id = 0)
        {
            var order = _orderService.GetById(id);
            OrderViewModel model = Mapper.Map(order, typeof(Order), typeof(OrderViewModel)) as OrderViewModel;
            return View(model);

        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(OrderViewModel order)
        {

            if (ModelState.IsValid)
            {
                var editingOrder = Mapper.Map<Order>(order);
                _orderService.Update(editingOrder);
                return RedirectToAction("Index", "Order");
            }
            return View(order);

        }


        public PartialViewResult BusketItem()
        {
            int items=0;
            double sum=0;
            if (CurrentUser == null)
            {
                return PartialView("_BusketItem", new BasketViewModel());
            }
            Order firstOrDefault = _orderService.GetOrderByClientId(CurrentUser.Id).FirstOrDefault();
            if (firstOrDefault != null)
            {
                items = firstOrDefault.OrderDetails.Count;
                sum = firstOrDefault.OrderDetails.Sum(x => x.Price);
            }

            return PartialView("_BusketItem", new BasketViewModel { TotalItemsCount = items, TotalSum = sum });
        }

    }
}
