using System.Runtime.Serialization;


namespace WCFService.Model
{

    /// <summary>
    ///  7.	Payment method should take required parameters: card number, Name and Surname (one parameter),
    ///  CVV code, expiration date (two parameters month and year), purpose of payment, amount of payment.
    /// </summary>
    [DataContract]
    public class PaymentData
    {
        [DataMember]
        public int? PaymentId { get; set; }

        [DataMember]
        public PaymentType PaymentType { get; set; }

        [DataMember]
        public string NumberCard { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string CVV { get; set; }

        [DataMember]
        public int ExpirationDateYear { get; set; }

        [DataMember]
        public int ExpirationDateMonth { get; set; }

        [DataMember]
        public string PurposeOfPayment { get; set; }

        [DataMember]
        public decimal AmountOfPayment { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }
   

    }
}
