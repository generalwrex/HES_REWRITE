using HellionExtendedServer.Managers.Components;
using NLog;
using ZeroGravity;

namespace HellionExtendedServer.Managers.IPC
{
    public class ServerService : IServerService
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        public ServerStatus GetStatus()
        {
            var status = new ServerStatus
            {
                ServerName = Server.Instance.ServerName,
                IsRunning = Server.IsRunning,
                UpTime = Server.Instance.RunTime
            };

            return status;
        }

        public WCFMessage Save()
        {
            try
            {
                if (Server.IsRunning)
                {
                    ServerInstance.Instance.Save();
                    return new WCFMessage(false, "Server Saved");
                }
                else
                    return new WCFMessage(true, "The server is not running");
            }
            catch (System.Exception ex)
            {
                return new WCFMessage(true, "Save", ex);
            }
        }

        public WCFMessage StartServer()
        {
            try
            {
                if (!Server.IsRunning)
                {
                    ServerInstance.Instance.StartServer();
                    return new WCFMessage(false, "Server Started");
                }
                else
                    return new WCFMessage(true, "The server is already running");
            }
            catch (System.Exception ex)
            {
                return new WCFMessage(true, "StartServer", ex);
            }
        }

        public WCFMessage StopServer()
        {
            try
            {
                if (Server.IsRunning)
                {
                    ServerInstance.Instance.StopServer();
                    return new WCFMessage(false, "Server Stopped");
                }
                else
                    return new WCFMessage(true, "The server is not running");
            }
            catch (System.Exception ex)
            {
                return new WCFMessage(true, "StopServer", ex);
            }
        }
    }
}