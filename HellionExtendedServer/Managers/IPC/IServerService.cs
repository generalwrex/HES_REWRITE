using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using NLog;
using HellionExtendedServer.Managers;
using HellionExtendedServer.Managers.Components;

namespace HellionExtendedServer.Managers.IPC
{
    [ServiceContract]
    public interface IServerService
    {
        [OperationContract]
        [WebGet]
        WCFMessage StartServer();

        [OperationContract]
        [WebGet]
        WCFMessage StopServer();

        [OperationContract]
        [WebGet]
        WCFMessage Save();

        [OperationContract]
        [WebGet]
        ServerStatus GetStatus();

    }
}
