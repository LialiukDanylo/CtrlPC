using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace CtrlPC.Desktop.Commands
{
    public class ShutdownCommand : ICommandHandler
    {
        public string CommandName => "SHUTDOWN";

        public Task HandleAsync(IPEndPoint remoteEndPoint)
        {
            Process.Start("shutdown", "/s /f /t 0");
            return Task.CompletedTask;
        }
    }
}
