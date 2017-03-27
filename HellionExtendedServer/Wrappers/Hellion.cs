using HellionExtendedServer.Managers;
using NLog;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using ZeroGravity;

namespace HellionExtendedServer.Wrappers
{
    /// <summary>
    /// This contains the main reflection to properly start Hellion Dedicated
    /// </summary>
    public class HELLION
    {
        #region Fields

        public const String AssemblyName = "HELLION_Dedicated.exe";

        private Boolean m_isRunning;
        private MethodInfo m_closeSocketListeners;
        private Server m_server;
        private Thread serverThread;

        #endregion Fields

        #region Events

        public delegate void ServerRunningEvent();

        public event ServerRunningEvent OnServerStarted;

        public event ServerRunningEvent OnServerStopped;

        #endregion Events

        #region Properties

        private static Logger Log = LogManager.GetCurrentClassLogger();

        public Server Server { get { return m_server; } }

        #endregion Properties

        public HELLION()
        {
        }

        #region Methods

        public void Load()
        {
            Log.Info("Loading Hellion Dedicated...");

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AssemblyName);

            if (!File.Exists(path))
            {
                Log.Fatal("Hellion Dedicated executable not found in: \r\n" + path);
                return;
            }

            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFile(path);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Hellion Extended Server [ASSEMBLYLOADERROR]");
                return;
            }
            Log.Debug("Hellion Dedicated Loaded!");

            SetupReflection(assembly);

            Server.Properties = new Properties(Server.ConfigDir + "GameServer.ini");

            Dbg.OutputDir = Server.ConfigDir;
            Dbg.Initialize();

            if (InitializeServer())
            {
                var serverInstance = new ServerInstance(this);
            }
        }

        private void SetupReflection(Assembly Assembly)
        {
            try
            {
                m_closeSocketListeners = Assembly.GetType("ZeroGravity.Network.NetworkController").GetMethod("OnApplicationQuit",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
            }
            catch (ArgumentException ex)
            {
                Log.Fatal(ex, "Hellion Extended Server [REFLECTION ERROR]");
            }
        }

        private bool InitializeServer()
        {
            try
            {
                m_server = new Server();
            }
            catch (Exception ex)
            {
                if (ex.TargetSite.Name == "LoadServerSettings")
                {
                    if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\CelestialBodies.json")))
                    {
                        Log.Fatal(ex, "CelestialBodies.json not found. Cannot start the server (Does the Data folder exist?)");
                        return false;
                    }

                    
                }
                //throw;
                return false;
            }

            return true;
        }

        public void Stop()
        {
            try
            {
                if (Server.PersistenceSaveInterval > 0.0)
                {
                    //ServerInstance.Instance.Save();
                    //Log.Instance.Info(HES.Localization.Sentences["SavingUniverse"]);
                }

                m_closeSocketListeners.Invoke(Server.Instance.NetworkController, null);

                Server.IsRunning = false;

                Dbg.Destroy();
                Server.MainLoopEnded.WaitOne(5000);

                //ServerInstance.Instance.IsRunning = false;

                //Log.Instance.Info(HES.Localization.Sentences["SuccessShutdown"]);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Hellion Extended Server [SHUTDOWN ERROR]");
            }
        }

        /// <summary>
        /// Starts Hellion Dedicated in its own thread
        /// </summary>
        /// <param name="args">command line arg passthrough</param>
        /// <returns>the thread!</returns>
        public Thread Start()
        {
            //Log.Instance.Info(HES.Localization.Sentences["LoadingDedicated"]);

            serverThread = new Thread(new ThreadStart(ServerThread))
            {
                IsBackground = true,
                CurrentCulture = CultureInfo.InvariantCulture,
                CurrentUICulture = CultureInfo.InvariantCulture
            };
            try
            {
                // Start the thread!
                serverThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hellion Extended Server [SERVER THREAD ERROR] : " + ex.ToString());
                return null;
            }

            while (!Server.WorldInitialized)
                Thread.Sleep(1000);

            OnServerStarted?.Invoke();

            return serverThread;
        }

        /// <summary>
        /// Starts the server by calling MainLoop in the Server class, after
        /// setting hellions log directory and loading the properties from the config file.
        /// </summary>
        /// <param name="args"></param>
        private void ServerThread()
        {
            try
            {
                m_server.MainLoop();
            }
            catch (TypeInitializationException ex)
            {
                Log.Fatal(ex, "[REPORT THE FOLLOWING TO GITHUB ISSUES] Could Not Initialize Server! : [FATAL ERROR]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), "Hellion Extended Server [START EXCEPTION]");
            }
        }

       

        #endregion Methods
    }
}