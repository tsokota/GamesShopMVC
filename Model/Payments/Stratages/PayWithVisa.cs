using System;
using Model.Payments.Args;

namespace Model.Payments.Stratages
{
    public class PayWithVisa:IPayable
    {
        public bool Pay(PayArgs args)
        {
            if (args is VisaPayArgs)
            {
                var payArgs = args as VisaPayArgs;
            }
            else
            {
                throw new ArgumentException("Incorrect type of PayArgs (Must be VisaPayArgs)");
            }

            return true;
        }
    }
}
