namespace CtrlPC.Mobile.Networking
{
    public interface INetworkScanner
    {
        Task<List<Models.Device>> ScanNetworkAsync(string broadcastIp, int port);
    }
}