using System;
using WCFService.Model;

namespace WCFService.StubManagerNotifications
{
    class NotificationEventArgs:EventArgs
    {
        public TransferDto Transfer;
        public string adress;
    }
}
