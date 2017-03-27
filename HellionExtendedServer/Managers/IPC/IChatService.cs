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
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        [WebGet]
        bool Broadcast();



    }
}
