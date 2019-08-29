using Model;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BusinessLogicLayer.ViewModel
{
    public class OrderViewModel
    {


        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CustomerId { get; set; }

        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsDeletable { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PaymentMethod { get; set; }

        [Display(Name = "Sum for paying")]
        public decimal PayingSum { get; set; }

        [Display(Name = "Status")]
        public OrderStatus OrderStatus { get; set; }

        public List<String> ListOrderStatus { get; set; }

        [Display(Name = "Shipped Date")]
        public Nullable<DateTime> ShippedDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


        public OrderViewModel()
        {
            ListOrderStatus = new List<string>();
            var statuses = Enum.GetValues(typeof(OrderStatus));

            foreach (var status in statuses)
            {
                OrderStatus s = (OrderStatus)status;
                
                this.ListOrderStatus.Add(s.ToString());
            }
        }
    }
}
