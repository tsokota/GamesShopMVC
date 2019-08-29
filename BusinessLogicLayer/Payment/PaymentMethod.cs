using Model.Payments;
using Model.Payments.Stratages;

namespace BusinessLogicLayer.Payment
{
    public abstract class PaymentMethod:IPayable
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        private IPayable _payableBehavior;   

        public decimal Price { get; set; }

        public PaymentMethod(IPayable method)
        {
            _payableBehavior = method ?? new NoPayNow();
        }

        // for dynamic set pay behavior 
        public void SetPayBehavior(IPayable newPayBehavior)
        {
            _payableBehavior = newPayBehavior;
        }

        public virtual bool Pay(PayArgs args)
        {
           return _payableBehavior.Pay(args);
        }
       
       
    }
}
