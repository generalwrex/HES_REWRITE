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

        private static List<WebServiceHost> m_httpServiceHosts;

        private static Logger Log = LogManager.GetCurrentClassLogger();

        private static bool m_useHTTP;

        public static void Start()
        {
            if(m_useHTTP)
                foreach (var host in m_httpServiceHosts)
                {
                    try
                    {
                        host.Open();
                    }
                    catch (AddressAccessDeniedException)
                    {
                        Log.Error("Hellion Extended Server: You must run HES as an Administrator to use the WEB API!");
                    }
                }         
                    
                

            foreach (var host in m_serviceHosts)
            {
                try
                {
                    host.Open();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "WCF Exception!");
                }
                
            }
        }

        public static void Stop()
        {
            if (m_useHTTP)
                foreach (var host in m_httpServiceHosts)
                    host.Close();

            foreach (var host in m_serviceHosts)
            {
                host.Close();
            }
        }

        public static void Init(bool useHTTP = false)
        {
            m_useHTTP = useHTTP;

            m_serviceHosts = new List<WebServiceHost>();
            m_httpServiceHosts = new List<WebServiceHost>();

        }

        public static void CreateServiceHost(Type serviceType, Type contractType, string urlExtension, string name)
        {
            if (m_useHTTP)
                CreateHTTPServiceHost(serviceType, contractType, urlExtension, name);

            try
            {
                var baseAddress = new Uri("net.tcp://localhost:8000/HES/" + urlExtension);
                var selfHost = new WebServiceHost(serviceType, baseAddress);

                selfHost.AddServiceEndpoint(contractType, new NetTcpBinding(), name);

                selfHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                    
                m_serviceHosts.Add(selfHost);

                Log.Debug("Created service at '" + baseAddress.ToString() + "'");
            }
            catch (CommunicationException ex)
            {
                Log.Error(ex, "An Communication exception occurred");
            }
        }

        private static void CreateHTTPServiceHost(Type serviceType, Type contractType, string urlExtension, string name)
        {
            try
            {
                var baseAddress = new Uri("http://localhost:1337/HES/" + urlExtension);
                var selfHost = new WebServiceHost(serviceType, baseAddress);

                selfHost.AddServiceEndpoint(contractType, new WebHttpBinding(), name);

                selfHost.Description.Behaviors.Add(new ServiceMetadataBehavior {
                    HttpGetEnabled = true
                });

                selfHost.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
                selfHost.Description.Behaviors.Add(new ServiceDebugBehavior {  IncludeExceptionDetailInFaults = true });

                m_httpServiceHosts.Add(selfHost);

                Log.Debug("Created service at '" + baseAddress.ToString() + "'");
            }
            catch (CommunicationException ex)
            {
                Log.Error(ex, "An HTTPCommunication exception occurred");
            }
        }

    }
}
