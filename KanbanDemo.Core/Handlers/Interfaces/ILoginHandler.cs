using KanbanDemo.Core.Requests;
using System.Threading.Tasks;

namespace KanbanDemo.Core.Handlers.Interfaces
{
    public interface ILoginHandler
    {
        Task<string> Login(LoginCommand command);
    }
}
