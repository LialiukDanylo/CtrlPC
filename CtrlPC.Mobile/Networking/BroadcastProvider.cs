using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace CtrlPC.Networking
{
    public class BroadcastProvider : IBroadcastProvider
    {
        public string GetBroadcastAddress()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i => i.OperationalStatus == OperationalStatus.Up && i.NetworkInterfaceType != NetworkInterfaceType.Loopback);

            foreach (var ni in networkInterfaces)
            {
                var properties = ni.GetIPProperties();
                var ipv4 = properties.UnicastAddresses.FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork);
                if (ipv4 != null)
                {
                    byte[] ipBytes = ipv4.Address.GetAddressBytes();
                    byte[] maskBytes = ipv4.IPv4Mask.GetAddressBytes();
                    byte[] broadcastBytes = new byte[4];
                    for (int i = 0; i < 4; i++)
                        broadcastBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]);

                    return new IPAddress(broadcastBytes).ToString();
                }
            }
            return "255.255.255.255";
        }
    }
}
