using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.ServiceModel.Web;
using HellionExtendedServer.Common;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace HellionExtendedServer
{
    /// <summary>
    /// This class is the main class for the program, It's called as a service or console app
    /// </summary>
    public class HellionExtendedServer
    {

        private static HellionExtendedServer m_instance;
        private static string[] m_args;

        private Logger Log = LogManager.GetCurrentClassLogger();

        public static Version Version => System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
        public static String BuildBranch => "Dev Branch";
        public static String VersionString => Version.ToString(3) + " " + BuildBranch;

        public bool RunningAsService => !Environment.UserInteractive;

        public static HellionExtendedServer Instance => m_instance;

        public HellionExtendedServer(string[] args)
        {
            Log.Warn("Initializing HellionExtendedServer v" + HellionExtendedServer.VersionString);

            WCFService.Init();


            m_args = args;

            m_instance = this;

            
        }

        internal void Run(string[] args)
        {
            m_args = args;

            WCFService.Start();

            if (this.RunningAsService)
            {
                // things to do only when hes is being ran as a service

            }
            else
            {
                Log.Info("It's working!");

                // temp, replace with command reader
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }


        }
        
        internal void OnQuit()
        {
            WCFService.Stop();
        }


    }
}
