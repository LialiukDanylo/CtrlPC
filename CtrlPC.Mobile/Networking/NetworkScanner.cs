using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace CtrlPC.Mobile.Networking
{
    public class NetworkScanner : INetworkScanner
    {
        public async Task<List<Models.Device>> ScanNetworkAsync(string broadcastIp, int port)
        {
            var ipList = new List<Models.Device>();

            try
            {
                using var udpClient = new UdpClient();
                udpClient.EnableBroadcast = true;

                var message = Encoding.UTF8.GetBytes("Ping");
                await udpClient.SendAsync(message, message.Length, broadcastIp, port);

                using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));

                while (true)
                {
                    var result = await udpClient.ReceiveAsync(cancellationTokenSource.Token);
                    var ip = result.RemoteEndPoint.Address.ToString();

                    if (!ipList.Any(d => d.IpAddress == ip))
                        ipList.Add(new Models.Device { IpAddress = ip });
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return ipList;
        }
    }
}