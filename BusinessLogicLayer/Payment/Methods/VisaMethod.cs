using System;
using Model.Payments.Args;
using Model.Payments.Stratages;
using Model.PaymentService;

namespace BusinessLogicLayer.Payment.Methods
{
    public class VisaMethod : PaymentMethod
    {
        public VisaMethod() :
            base(new PayWithVisa())
        {
            Name = "Visa";
            Description = "If you have visa card -> You can pay with help it";
            ImagePath = "~/Images/visa.jpg";
        }

        public virtual bool Pay(VisaPayArgs args)
        {



            using (var service = new Model.PaymentService.PaymentServiceClient())
            {
                PaymentData data = new PaymentData
                {
                    FullName = args.CardHoldersName,
                    NumberCard = args.CarNumber,
                    CVV = args.CVV2_CVC2,
                    Email = "dp260793kev@yandex.ru",
                    ExpirationDateMonth = args.ExpiryDate.Month,
                    ExpirationDateYear = args.ExpiryDate.Year,
                    PaymentType = PaymentType.Visa,
                    PhoneNumber = "+380994285786",
                    PurposeOfPayment = "want to by current game",
                    AmountOfPayment = Price,
                    PaymentId = args.OrderId,
                };

                var result = service.Pay(data);

                if (result.PaymentStatus != PaymentStatus.Success)
                {
                    throw new Exception("Payment  Error: " + result.PaymentStatus);
                }
                return result.PaymentStatus == PaymentStatus.Success;
            }
        }

    }
}
