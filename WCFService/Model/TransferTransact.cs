using System;
namespace WCFService.Model
{
    public class TransferTransact
    {
        public int TransferTransactId { get; set; }

        public string NumberCard { get; set; }

        public int UserId { get; set; }

        public string PurposeOfPayment { get; set; }

        public decimal MoneyAccount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime Date { get; set; }
    }
}
