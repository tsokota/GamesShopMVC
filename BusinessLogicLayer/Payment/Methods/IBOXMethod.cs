using Model.Payments.Stratages;

namespace BusinessLogicLayer.Payment.Methods
{
    public class IBOXMethod: PaymentMethod
    {
        public IBOXMethod()
            : base(new PayWithIBOX())
        {
            Name = "IBOX";
            Description = "Pay IBOX";
            ImagePath = "~/Images/ibox.jpg";
        }
    }
}
