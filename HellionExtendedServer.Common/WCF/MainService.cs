using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace HellionExtendedServer.Common
{
    public partial class MainService : IMainService
    {
        public string Echo(string s)
        {
            
            return "You said " + s;
        }

    }

    [ServiceContract]
    public interface IMainService
    {
        [OperationContract]
        [WebGet( ResponseFormat = WebMessageFormat.Json)]
        string Echo(string s);
    }
}
