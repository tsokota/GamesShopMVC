using System;
using NLog.Interface;

namespace WCFService.StubManagerNotifications
{
    class SmsSender
    {
        private ILogger logger;

        public SmsSender(ILogger logger)
        {
            this.logger = logger;
        }

        public void SendSmsStub(object sender, EventArgs e)
        {
            var eventArgs = (NotificationEventArgs)e;
            var phone = eventArgs.adress;

                //logger.Info(String.Format("Sms notification send to {0}", eventArgs.Address));
        }
    }
}
