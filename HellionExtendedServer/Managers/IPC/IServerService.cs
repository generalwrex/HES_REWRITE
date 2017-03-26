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
    public interface IServerService
    {
        [OperationContract]
        [WebGet]
        bool StartServer();

        [OperationContract]
        [WebGet]
        bool StopServer();

        [OperationContract]
        [WebGet]
        bool Save();

    }
}
