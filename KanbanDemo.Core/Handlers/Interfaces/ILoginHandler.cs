using KanbanDemo.Core.Requests;

namespace KanbanDemo.Core.Handlers.Interfaces
{
    public interface ILoginHandler
    {
        string Login(LoginCommand command);
    }
}
