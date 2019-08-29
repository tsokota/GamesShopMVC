using System.Globalization;
using AutoMapper;
using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.Services.UnitOfWorks;
using Model;
using Model.Entities;
using NLog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public OrderService(IUnitOfWork unitOfWork, ILogger logger)
        {
           _unitOfWork = unitOfWork;
           _logger = logger;
        }
    
        public Order GetById(int idOrder)
        {
            try
            {

                var order = _unitOfWork.OrderRepository.Get(a => true, null, "OrderDetails").FirstOrDefault(o => o.Id == idOrder);
                return order;
            }
            catch (Exception)
            {
                _logger.Error("some error, OrderService.cs");
                throw;
            }
        }

        public List<Order> GetOrderByClientId(int clientId)
        {
            try
            {
                var orders = _unitOfWork.OrderRepository.Get(a => true, null, "OrderDetails").Where(x=>x.User.Id == clientId  && x.OrderStatus!=OrderStatus.Shipped).ToList();
                return orders;
            }
            catch(Exception)
            {
                _logger.Error("some error, GetOrderByClientId(int clientId), OrderService.cs");
                throw;
            }
        }

        public void AddGame(Game game, int userId)
        {
            try
            {
              
                var currentGame = _unitOfWork.GameRepository.Get().FirstOrDefault(x=>x.Key == game.Key);
                if (currentGame == null)
                {
                    string id = game.Key.Split('-')[1];                  
                    game = Mapper.Map<Game>(_unitOfWork.ProductRepository.Get().
                             FirstOrDefault(a => a.ProductID.ToString() == id));
                    game.GameProduction = DateTime.Now;
                    _unitOfWork.GameRepository.Insert(game);
                    _unitOfWork.Save();
                    
                }
               
                var order = _unitOfWork.OrderRepository.Get().FirstOrDefault(x => x.User.Id == userId);
                if (order == null)
                {
                    order = new Order
                    {
                        OrderDetails = new List<OrderDetail>(),
                        CustomerId = userId,
                        OrderDate = DateTime.Now,
                        User = _unitOfWork.UserRepository.GetById(userId)
                    };

                    _unitOfWork.OrderRepository.Insert(order);
                }
                order.OrderStatus = OrderStatus.InProgress;
                
                var orderDetail = order.OrderDetails.FirstOrDefault(x => x.ProuctId == game.Id.ToString(CultureInfo.InvariantCulture));

                Game proxygame = _unitOfWork.GameRepository.GetById(game.Id);
                if (orderDetail == null)
                {
                    order.OrderDetails.Add(new OrderDetail 
                    {  Order = order , Price = game.Price, OrderType = OrderType.Game, Product = proxygame, Quantity = 1, ProuctId = proxygame.Id.ToString()});
                }
                else
                {
                    order.OrderDetails.FirstOrDefault(x => x.ProuctId == game.Id.ToString()).Quantity++;
                }

                _unitOfWork.Save();
            }
            catch (Exception)
            {
                _logger.Error("some error, OrderService.cs");
                throw;

            }
        }

        public List<Order> GetAllItems()
        {
            try
            {
              return _unitOfWork.OrderRepository.Get(a => true, null, "OrderDetails").ToList();   
            }
            catch (Exception)
            {
                _logger.Error("some error: GenreService.cs");
                throw;
            }
        }

        public void New(Order item)
        {
            try
            {
                if (item == null)
                {
                    _logger.Error("item is null: OrderService.cs New(Order item)");
                    throw new ArgumentNullException("item");
                }
                _unitOfWork.OrderRepository.Insert(item);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: OrderService.cs New(Genre genre)" + ex.Message);
                throw;
            }
        }

        public void Update(Order item)
        {
            try
            {
                if (item == null)
                {
                    _logger.Error("item is null: OrderService.cs Update(Model.Entities.Order item)");
                    throw new ArgumentNullException("item");
                }
               
             
                    _unitOfWork.OrderRepository.Update(item);
                    _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs Update(Genre genre)" + ex.Message);
                throw;
            }
        }

        public bool Delete(Order item)
        {
            try
            {
                if (item == null)
                {
                    _logger.Error("item is null: OrderService.cs Delete(Model.Entities.Order item)");
                    throw new ArgumentNullException("item");
                }
                item.IsDeleted = true;
                Update(item);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("some error: GenreService.cs Delete(Model.Entities.Order item" + ex.Message);
                throw;
            }
        }

        public List<System.Web.Mvc.SelectListItem> GetAllItemsAsSelectListItems()
        {
            throw new NotImplementedException();
        }

        public bool CheckOnItem(Order item)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersHistory(DateTime startDate, DateTime endDate)
        {
            return GetAllItems().Where(o => (o.OrderDate >= startDate && o.OrderDate <= endDate)).ToList();
        }

        public void MakeOrder(int idOrder)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(idOrder);
                order.OrderStatus = OrderStatus.Shipped;
                order.ShippedDate = DateTime.Now;
                _unitOfWork.OrderRepository.Update(order);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + "Some error MakeOrder(int idOrder)");
                throw;
            }
        }

        IEnumerable<Order> IGenericService<Order>.GetAllItems()
        {
            try
            {
                return _unitOfWork.OrderRepository.Get(a => true, null, "OrderDetails, User, Product").ToList();
            }
            catch (Exception)
            {
                _logger.Error("some error: GenreService.cs");
                throw;
            }
        }
    }
}
