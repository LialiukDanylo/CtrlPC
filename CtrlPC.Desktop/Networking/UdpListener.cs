using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CtrlPC.Desktop.Commands;

namespace CtrlPC.Desktop.Networking
{
    public class UdpListener
    {
        private readonly Dictionary<string, ICommandHandler> _handlers;

        public UdpListener(IEnumerable<ICommandHandler> handlers)
        {
            _handlers = handlers.ToDictionary(h => h.CommandName, StringComparer.OrdinalIgnoreCase);
        }

        public async Task StartAsync()
        {
            // Listen on UDP port 5151
            using (var udpClient = new UdpClient(5151))
            {
                var endPoint = new IPEndPoint(IPAddress.Any, 5151);

                while (true)
                {
                    try
                    {
                        var result = await udpClient.ReceiveAsync();
                        string command = Encoding.UTF8.GetString(result.Buffer);

                        if (_handlers.TryGetValue(command, out var handler))
                        {
                            await handler.HandleAsync(result.RemoteEndPoint);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
            }
        }
    }
}