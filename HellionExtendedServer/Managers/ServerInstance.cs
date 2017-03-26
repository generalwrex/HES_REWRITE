using HellionExtendedServer.Managers.IPC;
using HellionExtendedServer.Services;
using HellionExtendedServer.Wrappers;
using System;
using ZeroGravity;
using NLog;

namespace HellionExtendedServer.Managers
{
    internal class ServerInstance
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        private static HELLION m_hellion;

        public ServerInstance(HELLION hellion)
        {
            m_hellion = hellion;

            hellion.OnServerStarted += Hellion_OnServerStarted;
            hellion.OnServerStopped += Hellion_OnServerStopped;

            //WCFService.CreateServiceHost(typeof(ServerService), typeof(IServerService), "Server", "");

            StartServer();
        }

        private void Hellion_OnServerStarted(Server server)
        {
            // TEST PASSED!!!
            Log.Warn("DA SERVER STARTED?!?>$#@$%GFDSG");
            Log.Warn(Server.Instance.ServerName);

            foreach(var vessel in Server.Instance.AllVessels)
            {
                Log.Warn(vessel.Position.ToString());
            }
            
        }

        private void Hellion_OnServerStopped(Server server)
        {
            throw new NotImplementedException();
        }

        public void StartServer()
        {
            m_hellion.Start();
        }
    }
}