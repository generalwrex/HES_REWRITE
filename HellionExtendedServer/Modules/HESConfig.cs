using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Policy;

namespace HellionExtendedServer.Modules
{
    [DataContract]
    public class HESConfig
    {
        private static string serviceName;
        private static string wcfUrl;
        private static string webWcfUrl;
        private static ushort gamePort;
        private static ushort statusPort;
        private static string serverDir;
        private static string configDir;
        private static string logDir;

        [DataMember]
        public string ServiceName { get => serviceName; set => serviceName = value; }

        [DataMember]
        public string WCFUrl { get => wcfUrl; set => wcfUrl = value; }

        [DataMember]
        public string WebWCFUrl { get => webWcfUrl; set => webWcfUrl = value; }

        [DataMember]
        public ushort GamePort { get => gamePort; set => gamePort = value; }

        [DataMember]
        public ushort StatusPort { get => statusPort; set => statusPort = value; }

        [DataMember]
        public string ServerDir { get => serverDir; set => serverDir = value; }

        [DataMember]
        public string ConfigDir { get => configDir; set => configDir = value; }

        [DataMember]
        public string LogDir { get => logDir; set => logDir = value; }

        public HESConfig()
        {
            serviceName = "HellionExtendedServer";
            wcfUrl = "localhost:8000";
            webWcfUrl = "localhost:8000";
            gamePort = 1337;
            statusPort = 1338;
            serverDir = Directory.GetCurrentDirectory();
            configDir = "";
            logDir = "logs";
        }

        public static void ProcessArgs(string[] args)
        {
            var numArgs = args.GetLength(0);
            var i = 0;
            while (numArgs != i)
            {
                var currentArg = args[i].ToLower();
                switch (currentArg)
                {
                    case "-servicename":
                        {
                            if (i + 1 != numArgs)
                            {
                                var value = args[i + 1];

                                serviceName = value;

                                i++;
                            }
                            break;
                        }
                    case "-wcfurl":
                        {
                            if (i + 1 != numArgs)
                            {
                                var value = args[i + 1];

                                i++;
                            }
                            break;
                        }
                    case "-webwcfurl":
                        {
                            if (i + 1 != numArgs)
                            {
                                var value = args[i + 1];

                                i++;
                            }
                            break;
                        }
                    case "-gameport":
                        {
                            if (i + 1 != numArgs)
                            {
                                var value = args[i + 1];

                                i++;
                            }
                            break;
                        }
                    case "-statusport":
                        {
                            if (i + 1 != numArgs)
                            {
                                var value = args[i + 1];

                                i++;
                            }
                            break;
                        }
                    case "-serverdir":
                        {
                            if (i + 1 != numArgs)
                            {
                                var value = args[i + 1];

                                i++;
                            }
                            break;
                        }
                    case "-configdir":
                        {
                            if (i + 1 != numArgs)
                            {
                                var value = args[i + 1];

                                i++;
                            }
                            break;
                        }
                    case "-logdir":
                        {
                            if (i + 1 != numArgs)
                            {
                                var value = args[i + 1];

                                i++;
                            }
                            break;
                        }
                }
                i++;
            }
        }
    }
}