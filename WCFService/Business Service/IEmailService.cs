using WCFService.Model;

namespace WCFService.Business_Service
{
    public interface IEmailService
    {
        void SendEmail(TransferTransact tranfer, string userEmail);
    }
}
