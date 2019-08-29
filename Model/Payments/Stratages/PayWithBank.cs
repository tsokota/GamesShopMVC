using System;
using Model.Payments.Args;

namespace Model.Payments.Stratages
{
    public class PayWithBank: IPayable
    {
        public bool Pay(PayArgs args)
        {
            if (args is BankPayArgs)
            {
                var payArgs = args as BankPayArgs;
            }
            else
            {
                throw new ArgumentException("Incorrect type of PayArgs (Must be BankPayArgs)");
            }
            return true;
        }
    }
}
