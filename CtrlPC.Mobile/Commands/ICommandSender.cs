namespace CtrlPC.Mobile.Commands
{
    public interface ICommandSender
    {
        void SendCommand(string ipAddress, int port, string command);
    }
}
