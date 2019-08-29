using Model;
using System;
using System.Collections.Generic;

namespace Yevhenii_KoliesnikTask1.WebApi.ApiDTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ShippetDate { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsClosed { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderDetailsDTO> OrderDetails { get; set; }
    }
}