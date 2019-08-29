using System;
using Model.Payments.Args;

namespace Model.Payments.Stratages
{
    public class PayWithIBOX:IPayable
    {
        public bool Pay(PayArgs args)
        {
            if (args is iboxPayArgs)
            {
                var payArgs = args as iboxPayArgs;
            }
            else
            {
                throw new ArgumentException("Incorrect type of PayArgs (Must be iboxPayArgs)");
            }

            return true; 
        }
    }
}
