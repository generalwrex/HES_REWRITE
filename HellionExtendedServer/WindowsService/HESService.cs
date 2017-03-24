using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;

namespace HellionExtendedServer
{
    partial class HESService : ServiceBase
    {

        public HESService(string[] args)
        {
            InitializeComponent();
            var eventSourceName = "HellionExtendedServer";
            var logName = "HellionExtendedServer Service Log";


            if (args.Length > 0)
            {
                eventSourceName = args[0];
            }
            if (args.Length > 1)
            {
                logName = args[1];
            }
            eventLog1 = new EventLog();

            if (!EventLog.SourceExists(eventSourceName))
            {
                EventLog.CreateEventSource(eventSourceName, logName);
            }
            eventLog1.Source = eventSourceName;
            eventLog1.Log = logName;
        }


        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            eventLog1.WriteEntry("In OnStart");

            var thread = new Thread(() => {
                HellionExtendedServer.Instance.Run(args);

                eventLog1.WriteEntry("HellionExtendedServer Service Running!", EventLogEntryType.Information, 1);
            });
            thread.Start();
            

            var timer = new System.Timers.Timer(60000);
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();                     
        }

        protected override void OnStop()
        {
            HellionExtendedServer.Instance.OnQuit();
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }

        private int eventId;

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }
    }
}