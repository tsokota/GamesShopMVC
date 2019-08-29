using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using WCFService.Model;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class PaymentService : IPaymentService
    {
        public PaymentService()
        {
            Console.WriteLine("Create Payment Service -> constructor");
        }

        static readonly List<UserDto> Users;

        static UserDto CurrentManager;

        static Dictionary<NotificationActions, Action<object, EventArgs>> Notifications;

        public async  Task<PaymentResult> Pay(PaymentData paymentData)
        {
            Console.WriteLine("Processing Payment");
            PaymentResult result = ValidationData(paymentData);
            if (result.PaymentStatus == PaymentStatus.Failed)
            {
                return result;
            }
            switch (paymentData.PaymentType)
            {
                case PaymentType.Visa:
                    return await VisaPay(paymentData);
                case PaymentType.MasterCard:
                    return await MasterCardPay(paymentData);
                default:
                    return await VisaPay(paymentData);
            }
        }

        private async  Task<PaymentResult> VisaPay(PaymentData paymentData)
        {
            return await  Payment(paymentData);
        }

        private async Task<PaymentResult> MasterCardPay(PaymentData paymentData)
        {
            return await Payment(paymentData);
        }

        public static event Action<object, EventArgs> OnSendNotification;

        private async Task<PaymentResult> Payment(PaymentData paymentData)
        {
            TransferTransact transfer = new TransferTransact
            {
                Date = DateTime.Now,
                MoneyAccount = paymentData.AmountOfPayment,
                NumberCard = paymentData.NumberCard,
                PurposeOfPayment = paymentData.PurposeOfPayment,
                TransferTransactId = 5
            };

            PaymentResult result = new PaymentResult();

            string[] userName = paymentData.FullName.Split(' ');
            var user = FakeDataRepository.Users.FirstOrDefault(a => a.UserName == userName[0] && a.UserSurname == userName[1]);

            result = ValidationPayment(result, user, transfer, paymentData);
            if (result.PaymentStatus != PaymentStatus.Success)
                return result;
            user.Account.MoneyAccount -= paymentData.AmountOfPayment;

            result.PaymentStatus = PaymentStatus.Success;
            transfer.PaymentStatus = PaymentStatus.Success;
            FakeDataRepository.TransferTransacts.Add(transfer);

            if (!string.IsNullOrWhiteSpace(paymentData.Email) || !string.IsNullOrWhiteSpace(user.UserEmail))
            {
                string email = paymentData.Email ?? user.UserEmail;
                EmailService emailService = new EmailService();
                emailService.SendEmail(transfer, email);
            }

            return result;
        }

        // method wich validation data and change Paymentstatus if there is error
        public PaymentResult ValidationData(PaymentData paymentData)
        {
            PaymentResult result = new PaymentResult { ErrorMessage = string.Empty, PaymentStatus = PaymentStatus.Success };

            if (paymentData == null)
            {
                throw new ArgumentNullException("paymentData");
            }
            if (string.IsNullOrWhiteSpace(paymentData.NumberCard))
            {
                result.PaymentStatus = PaymentStatus.Failed;
                result.ErrorMessage += "Card number cannot be null.";
            }
            if (string.IsNullOrWhiteSpace(paymentData.CVV))
            {
                result.PaymentStatus = PaymentStatus.Failed;
                result.ErrorMessage += "CVV number cannot be null.";
            }
            if (string.IsNullOrWhiteSpace(paymentData.FullName) || paymentData.FullName.Split(' ').Count() != 2)
            {
                result.PaymentStatus = PaymentStatus.Failed;
                result.ErrorMessage += "Full name cannot be null.";
            }
            if (string.IsNullOrWhiteSpace(paymentData.PurposeOfPayment))
            {
                result.PaymentStatus = PaymentStatus.Failed;
                result.ErrorMessage += "Purpose of payment cannot be null.";
            }
            if (paymentData.ExpirationDateYear < DateTime.Now.Year)
            {
                result.PaymentStatus = PaymentStatus.Failed;
                result.ErrorMessage += "Wrond expiration year.";
            }
            if ((paymentData.ExpirationDateYear == DateTime.Now.Year && paymentData.ExpirationDateMonth < DateTime.Now.Month) 
                || paymentData.ExpirationDateMonth >= SettingsConst.ExpirationDateMonth)
            {
                result.PaymentStatus = PaymentStatus.Failed;
                result.ErrorMessage += "Wrond expiration month.";
            }
            return result;

        }

        public PaymentResult ValidationPayment(PaymentResult result, User user, TransferTransact transfer, PaymentData paymentData)
        {
            if (!GetPhoneNotification("111"))
            {
                result.PaymentStatus = PaymentStatus.PhoneCodeInvalid;
                return result;
            }

            if (user == null)
            {
                result.PaymentStatus = PaymentStatus.UserNotFound;
                transfer.PaymentStatus = PaymentStatus.UserNotFound;
                FakeDataRepository.TransferTransacts.Add(transfer);
                return result;
            }
            transfer.UserId = user.IdUser;
            if (user.Account.AccountNumber != paymentData.NumberCard || user.Account.CVV != paymentData.CVV)
            {
                result.PaymentStatus = PaymentStatus.CardNotExist;
                transfer.PaymentStatus = PaymentStatus.CardNotExist;
                FakeDataRepository.TransferTransacts.Add(transfer);
                return result;
            }

            if (user.Account.MoneyAccount < paymentData.AmountOfPayment)
            {
                result.PaymentStatus = PaymentStatus.NotEnoughMoney;
                transfer.PaymentStatus = PaymentStatus.NotEnoughMoney;
                FakeDataRepository.TransferTransacts.Add(transfer);
                return result;
            }
      

            if (!string.IsNullOrWhiteSpace(paymentData.PhoneNumber))
            {
                PhoneService phoneService = new PhoneService();
                if (!phoneService.CodeIdentic(paymentData.PhoneNumber))
                {
                    result.PaymentStatus = PaymentStatus.PhoneCodeInvalid;
                    transfer.PaymentStatus = PaymentStatus.PhoneCodeInvalid;
                    FakeDataRepository.TransferTransacts.Add(transfer);
                    return result;
                }
            }


            var newManager = Users.First(a => a.UserId == user.IdUser);
            if (newManager != CurrentManager)
            {
                SetCurrentManager(newManager);
            }
            result.PaymentStatus = PaymentStatus.Success;
            return result;
        }


        private bool GetPhoneNotification(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                Console.WriteLine("No phone applied!");
                return true;
            }
            int code = new Random().Next(0, 10000);
            Console.WriteLine("Secret code checker for phone ({0}): {1}", phoneNumber, code);
            int enteredCode = code;
            bool isOk = code == enteredCode;
            Console.WriteLine(isOk ? "Passed!" : "Wrong code!");
            return isOk;
        }

        private void SetCurrentManager(UserDto user)
        {
            foreach (var notificationName in CurrentManager.Notifications)
            {
                OnSendNotification -= Notifications[notificationName];
            }

            foreach (var notificationName in user.Notifications)
            {
                OnSendNotification += Notifications[notificationName];
            }
            CurrentManager = user;
        }
    }
}
