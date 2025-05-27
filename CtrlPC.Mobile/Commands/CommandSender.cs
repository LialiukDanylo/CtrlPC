using System.Net.Sockets;
using System.Text;

namespace CtrlPC.Mobile.Commands
{
    public class CommandSender : ICommandSender
    {
        public void SendCommand(string ipAddress, int port, string command)
        {
            using var udpClient = new UdpClient();

            var message = Encoding.UTF8.GetBytes(command);
            udpClient.Send(message, message.Length, ipAddress, port);
        }
    }
}
