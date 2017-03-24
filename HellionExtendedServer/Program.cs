using NLog;
using System;
using System.Configuration.Install;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;

namespace HellionExtendedServer
{
    /// <summary>
    /// This is just for the initialization of the HES program, place code in the HellionExtendedServer class
    /// </summary>
    internal static class Program
    {
        private static EventHandler _handler;

        private static Logger Log = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            var hes = new HellionExtendedServer(args);

            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);

            if (Environment.UserInteractive)
            {
                Console.Title = "HellionExtendedServer v" + HellionExtendedServer.VersionString;

                if (args.Length > 0)
                {
                    try
                    {
                        switch (args[0])
                        {
                            case "-install":
                                {
                                    if (!IsAdministrator())
                                    {
                                        Log.Error("Hellion Extended Server: You must run HES as an Administrator to install it as a service!");
                                        break;
                                    }
                                    Log.Warn("Installing Hellion Extended Server as a service.");
                                    ManagedInstallerClass.InstallHelper(new string[] { System.Reflection.Assembly.GetExecutingAssembly().Location });
                                    Log.Warn("Hellion Extended Server: Installed as a service.");
                                    break;
                                }
                            case "-uninstall":
                                {
                                    if (!IsAdministrator())
                                    {
                                        Log.Error("Hellion Extended Server: You must run HES as an Administrator to uninstall the service!");
                                        break;
                                    }
                                    Log.Warn("Uninstalling Hellion Extended Server Service.");
                                    ManagedInstallerClass.InstallHelper(new string[] { "/u", System.Reflection.Assembly.GetExecutingAssembly().Location });
                                    Log.Warn("Hellion Extended Server: Service has been Uninstalled.");
                                    break;
                                }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Hellion Extended Server: Error Installing or Uninstalling Service");
                    }

                    Log.Info("Press any key to Close.");
                    Console.ReadKey();
                    return;
                }

                hes.Run(args);
            }
            else
                ServiceBase.Run(new ServiceBase[] { new HESService(args) });
        }

        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        #region ConsoleHandler

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);

        private enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6,
        }

        private static bool Handler(CtrlType sig)
        {
            if (sig == CtrlType.CTRL_C_EVENT || sig == CtrlType.CTRL_BREAK_EVENT || (sig == CtrlType.CTRL_LOGOFF_EVENT || sig == CtrlType.CTRL_SHUTDOWN_EVENT) || sig == CtrlType.CTRL_CLOSE_EVENT)
            {
                Log.Warn("Quitting HellionExtendedServer...");
                if (HellionExtendedServer.Instance != null)
                    HellionExtendedServer.Instance.OnQuit();
            }
            return false;
        }

        #endregion ConsoleHandler
    }
}