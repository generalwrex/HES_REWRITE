using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HellionExtendedServer.Managers.Components
{
    [DataContract]
    public class ServerStatus
    {
        [DataMember]
        public string ServerName;

        [DataMember]
        public TimeSpan UpTime;

        [DataMember]
        public bool IsRunning;
    }
}
