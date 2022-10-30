using System.Collections.Generic;

namespace KanbanDemo.Core.Validator.Interfaces
{
    public interface INotifiable
    {
        bool IsValid { get; }
        List<string> Notifications { get; }
    }
}
