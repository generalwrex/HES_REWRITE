using HellionExtendedServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace HellionExtendedServer
{
    public static class WCFService
    {
        private static List<WebServiceHost> m_serviceHosts;

        private static Logger Log = LogManager.GetCurrentClassLogger();


        public static void Start()
        {
            foreach(var host in m_serviceHosts)
            {
                host.Open();
            }
        }

        public static void Stop()
        {
            foreach (var host in m_serviceHosts)
            {
                host.Close();
            }
        }

        public static void Init()
        {
            m_serviceHosts = new List<WebServiceHost>();

            m_serviceHosts.Add(CreateServiceHost(typeof(MainService), typeof(IMainService), "Internal", "Main"));
        }

        public static WebServiceHost CreateServiceHost(Type serviceType, Type contractType, string urlExtension, string name)
        {
            try
            {
                var baseAddress = new Uri("http://localhost:1337/HellionExtendedServer/" + urlExtension);
                var selfHost = new WebServiceHost(serviceType, baseAddress);

                selfHost.AddServiceEndpoint(contractType, new WebHttpBinding(), name);

                var smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);      
                Log.Info("Created WCF service at '" + baseAddress.ToString() + "'");
                
                return selfHost;
            }
            catch (CommunicationException ex)
            {
                Log.Error(ex, "An exception occurred");
                return null;
            }
        }

    }
}
