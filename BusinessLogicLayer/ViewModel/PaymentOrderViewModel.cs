using BusinessLogicLayer.Payment;
using Model.Entities;
using System.Collections.Generic;

namespace BusinessLogicLayer.ViewModel
{
    public class PaymentOrderViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
    }
}
