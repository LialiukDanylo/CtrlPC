using System.Collections.Generic;
using System.Threading.Tasks;
using CtrlPC.Desktop.Commands;
using CtrlPC.Desktop.Networking;

namespace CtrlPC.Desktop
{
    class Program
    {
        static async Task Main()
        {
            // Register available command handlers
            var handlers = new List<ICommandHandler>
            {
                new PingCommand(),
                new SleepCommand(),
                new ShutdownCommand(),
                new RestartCommand()
            };

            // Start listening for UDP commands
            var listener = new UdpListener(handlers);
            await listener.StartAsync();
        }
    }
}