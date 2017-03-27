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

    public class ChatService : IChatService
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();


        public bool Broadcast()
        {
            throw new NotImplementedException();
        }

    }

}
