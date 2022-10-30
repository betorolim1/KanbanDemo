using KanbanDemo.Core.Commands;
using KanbanDemo.Core.Result;
using KanbanDemo.Core.Validator.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanDemo.Core.Handlers.Interfaces
{
    public interface ICardHandler : INotifiable
    {
        Task<List<CardResult>> InsertCardAsync(InsertCardCommand command);
        Task<CardResult> UpdateCardAsync(UpdateCardCommand command);
        Task<List<CardResult>> DeleteCardAsync(Guid id);
        Task<List<CardResult>> GetCards();
    }
}
