using System.Collections;
using System.ComponentModel;
using System.ServiceProcess;

namespace HellionExtendedServer.Services
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            string parameter = "HellionExtendedServerService\" \"HellionExtendedServer_ServiceLog"; Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" \"" + parameter + "\""; base.OnBeforeInstall(savedState);
        }
    }
}