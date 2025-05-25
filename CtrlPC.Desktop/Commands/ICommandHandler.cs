using System.Net;
using System.Threading.Tasks;

namespace CtrlPC.Desktop.Commands
{
    public interface ICommandHandler
    {
        string CommandName { get; }
        Task HandleAsync(IPEndPoint remoteEndPoint);
    }
}