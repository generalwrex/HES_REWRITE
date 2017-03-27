using HellionExtendedServer.Managers.IPC;
using HellionExtendedServer.Services;
using HellionExtendedServer.Wrappers;
using System;
using ZeroGravity;
using NLog;

namespace HellionExtendedServer.Managers
{
    public abstract class HesManager
    {
        protected Logger m_log;
        protected HELLION m_hellion;
        protected Server m_server;

        public virtual Logger Log => m_log;

        public virtual Server Server => m_server;


        protected HesManager(HELLION hellion, Server server)
        {
            m_hellion = hellion;
            m_server = server;

            m_log = LogManager.GetCurrentClassLogger();

        }

        public virtual void OnServerStarted()
        {

        }

        public virtual void OnServerStopped()
        {

        }
    }
}
