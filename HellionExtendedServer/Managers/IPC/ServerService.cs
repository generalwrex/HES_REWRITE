using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace HellionExtendedServer.Managers.IPC
{

    public class ServerService : IServerService
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();


        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool StartServer()
        {
            throw new NotImplementedException();
        }

        public bool StopServer()
        {
            throw new NotImplementedException();
        }
    }

}
