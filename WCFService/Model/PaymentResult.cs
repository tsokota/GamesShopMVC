using System.Runtime.Serialization;

namespace WCFService.Model
{
    [DataContract]
    public class PaymentResult
    {
        [DataMember]
        public PaymentStatus PaymentStatus { get; set; }

        [DataMember]
        public int? PaymentId { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
