using HellionExtendedServer.Managers.IPC;
using HellionExtendedServer.Services;
using HellionExtendedServer.Wrappers;
using System;
using ZeroGravity;
using NLog;
using System.Collections.Generic;

namespace HellionExtendedServer.Managers
{
    internal class ServerInstance
    {
        private List<HesManager> m_managers;

        private static ServerInstance m_instance;

        private HELLION m_hellion;

        public static ServerInstance Instance => m_instance;

        private static Logger Log => LogManager.GetCurrentClassLogger();

        private Server Server => Server.Instance;

        public ServerInstance(HELLION hellion)
        {
            WCFService.Init(true);
            

            m_managers = new List<HesManager>();

            m_instance = this;

            m_hellion = hellion;

            hellion.OnServerStarted += OnServerStarted;
            hellion.OnServerStopped += OnServerStopped;

            WCFService.CreateServiceHost(typeof(ServerService), typeof(IServerService), "Server", "");

            CreateManagers();

            WCFService.Start();

            //TODO: this is temp! will be controlled via the manager or
            StartServer();
        }

        public void CreateManagers()
        {
            m_managers.Add(new ChatManager(m_hellion, Server));
        }

        public void StartServer()
        {
            m_hellion.Start();           
        }

        public void StopServer()
        {
            m_hellion.Stop();
        }

        public void Save()
        {
            Log.Warn("Attempting to save");
            throw new NotImplementedException();
        }

        private void OnServerStarted()
        {
            foreach (var manager in m_managers)
                manager.OnServerStarted();

            // TEST PASSED!!!
            Log.Warn("Server running!");
          
        }

        private  void OnServerStopped()
        {
            foreach (var manager in m_managers)
                manager.OnServerStopped();
        }


    }
}