using System;
using NLog.Interface;

namespace WCFService.StubManagerNotifications
{
    public class NotificationSender
    {
        private readonly ILogger logger;

        public NotificationSender(ILogger log)
        {
           logger = log;
        }

        public void SendSms(object sender, EventArgs e)
        {
            var eventArgs = (NotificationEventArgs)e;
            logger.Info(String.Format("App notification send to {0}", eventArgs.adress));
        }

    }
}
