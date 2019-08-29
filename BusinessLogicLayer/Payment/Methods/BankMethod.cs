using Model.Payments.Stratages;

namespace BusinessLogicLayer.Payment.Methods
{
    public class BankMethod : PaymentMethod
    {
        public BankMethod() : base(new PayWithBank())
        {
            Name = "Bank";
            Description = "Bank Pay, generate invoice file";
            ImagePath = "~/Images/bank.gif";
        }
    }
}
