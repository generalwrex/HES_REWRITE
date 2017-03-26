using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using NLog;
using HellionExtendedServer.Managers.IPC;

namespace HellionExtendedServer.Services
{
    public static class WCFService
    {      
        private static List<WebServiceHost> m_serviceHosts;

        private static Logger Log = LogManager.GetCurrentClassLogger();


        public static void Start()
        {
            foreach (var host in m_serviceHosts)
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

            
          
        }

        public static void CreateServiceHost(Type serviceType, Type contractType, string urlExtension, string name)
        {
            try
            {
                var baseAddress = new Uri("http://localhost:1337/HES/" + urlExtension);
                var selfHost = new WebServiceHost(serviceType, baseAddress);

                selfHost.AddServiceEndpoint(contractType, new WebHttpBinding(), name);

                selfHost.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });
                    
                m_serviceHosts.Add(selfHost);

                Log.Debug("Created service at '" + baseAddress.ToString() + "'");
            }
            catch (CommunicationException ex)
            {
                Log.Error(ex, "An Communication exception occurred");
            }
        }


    }
}
