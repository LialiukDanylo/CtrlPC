using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CtrlPC.Desktop.Commands
{
    public class PingCommand : ICommandHandler
    {
        public string CommandName => "Ping";

        // Used to confirm listener is active
        public async Task HandleAsync(IPEndPoint remoteEndPoint)
        {
            byte[] response = Encoding.UTF8.GetBytes("Pong");

            using (var client = new UdpClient())
            {
                await client.SendAsync(response, response.Length, remoteEndPoint);
            }
        }
    }
}
