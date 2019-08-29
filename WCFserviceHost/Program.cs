using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Model;
using NLog;
using WCFService;

namespace WCFserviceHost
{
    class Program
    {
        public static Logger logger;
        static void Main(string[] args)
        {
           
            var baseAddress = new Uri(SettingsConst.BaseAdress);

            using (var host = new ServiceHost(typeof(PaymentService), baseAddress))
            {
                host.Closed += HostOnClosed;
                host.Closing += HostOnClosing;
                host.UnknownMessageReceived += HostOnUnknownMessageReceived;
                host.Faulted += HostOnFaulted;

                var smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                };

                host.Description.Behaviors.Add(smb);
                ((ServiceDebugBehavior)host.Description.Behaviors.First(b => b is ServiceDebugBehavior)).IncludeExceptionDetailInFaults = true;

                host.Open();

                Console.WriteLine("The service is ready to  work. It is at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();
               
                host.Close();
            }
        }

        private static void HostOnFaulted(object sender, EventArgs eventArgs)
        {
            logger.Debug(" HostOnFaulted");
        }

        private static void HostOnUnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs unknownMessageReceivedEventArgs)
        {
            logger.Debug("HostOnUnknownMessageReceived");
        }

        private static void HostOnClosing(object sender, EventArgs eventArgs)
        {
            logger.Debug("HostOnClosing");
        }

        private static void HostOnClosed(object sender, EventArgs eventArgs)
        {
            logger.Debug("HostOnClosed");
        }
    }
}
