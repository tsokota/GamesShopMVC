using Model.MetaEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [MetadataType(typeof(MetaOrder))]
    public class Order : IGenericModel
    {
        public int Id { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public int CustomerId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime? ShippedDate { get; set; }

        public virtual User User { get; set; }
    }
}
