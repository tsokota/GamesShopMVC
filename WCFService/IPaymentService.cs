using System.ServiceModel;
using System.Threading.Tasks;
using WCFService.Model;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPaymentService
    {   
            [OperationContract]
            Task<PaymentResult> Pay(PaymentData paymentData);
    }

  
}
