using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace CtrlPC.Desktop.Commands
{
    public class RestartCommand : ICommandHandler
    {
        public string CommandName => "RESTART";

        public Task HandleAsync(IPEndPoint remoteEndPoint)
        {
            Process.Start("shutdown", "/r /f /t 0");
            return Task.CompletedTask;
        }
    }
}
