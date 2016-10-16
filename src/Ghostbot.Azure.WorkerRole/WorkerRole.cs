using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Ghostbot.Azure.WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        readonly ManualResetEvent _runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("Ghostbot WorkerRoles is running");

            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    Trace.TraceInformation("Working");

                    var ghostbotClient = new GhostbotClient();

                    ghostbotClient.Start();
                }
            }
            finally
            {
                _runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            var result = base.OnStart();

            Trace.TraceInformation("Ghostbot WorkerRoles has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("Ghostbot WorkerRoles is stopping");

            _cancellationTokenSource.Cancel();
            _runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("Ghostbot WorkerRoles has stopped");
        }
    }
}
