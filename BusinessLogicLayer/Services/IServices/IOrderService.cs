using Model.Entities;
using System;
using System.Collections.Generic;


namespace BusinessLogicLayer.Services.IServices
{
    public interface IOrderService : IGenericService<Model.Entities.Order>
    {
        void AddGame(Game game, int userId);

        List<Model.Entities.Order> GetOrdersHistory(DateTime startDate, DateTime endDate);

        List<Model.Entities.Order> GetOrderByClientId(int clientId);

        void MakeOrder(int idOrder);   
    }
}
