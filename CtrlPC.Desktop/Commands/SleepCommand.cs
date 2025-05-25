using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace CtrlPC.Desktop.Commands
{
    public class SleepCommand : ICommandHandler
    {
        public string CommandName => "SLEEP";

        public Task HandleAsync(IPEndPoint remoteEndPoint)
        {
            Process.Start("shutdown", "/h");
            return Task.CompletedTask;
        }
    }
}
