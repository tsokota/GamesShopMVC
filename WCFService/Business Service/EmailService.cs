using System.Net;
using System.Net.Mail;
using Model;
using WCFService.Business_Service;
using WCFService.Model;

namespace WCFService
{
    //Implement email sending.
    public class EmailService : IEmailService
    {
        public void SendEmail(TransferTransact tranfer, string userEmail)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = SettingsConst.Host;

            smtp.Credentials = new NetworkCredential(SettingsConst.FromAdress, SettingsConst.PasswordMail);
            MailMessage message = new MailMessage();
            smtp.EnableSsl = true;
            message.From = new MailAddress(SettingsConst.FromAdress);
            message.To.Add(new MailAddress("Dn260793kev@gmail.com"));
            message.Subject = "GameStore Payment";
            string text = "Transfer ID: " + tranfer.TransferTransactId
                          + "\n User ID " + tranfer.UserId +
                          "\n Payment Status: " + tranfer.PaymentStatus
                          + "\n Card: " + tranfer.NumberCard
                          + "\n Money Account " + tranfer.MoneyAccount +
                          "\n Date: " + tranfer.Date + "\n";
            if (tranfer.PaymentStatus != PaymentStatus.Success)
            {
                text += tranfer.ErrorMessage;
            }
            message.Body = text;

            try
            {
                smtp.Send(message);
            }
            catch (SmtpException ex)
            {
                throw new SmtpException(ex.Message);
            }
        }
    }
}
