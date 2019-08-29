using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.MetaEntities
{
    class MetaOrder
    {
        public int Id { get; set; }
        [Display(Name = "OrderDate", ResourceType = typeof(ModelRes))]
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [Display(Name = "IsRemove", ResourceType = typeof(ModelRes))]
        [Required(ErrorMessageResourceType = typeof(ModelRes),
        ErrorMessageResourceName = "IsDeletedRequired")]
        public bool IsDeleted { get; set; }


        [Display(Name = "Status")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "ShippedDate")]
        public DateTime? ShippedDate { get; set; }

        [Display(Name = "User")]
        public virtual User User { get; set; }
    }
}
