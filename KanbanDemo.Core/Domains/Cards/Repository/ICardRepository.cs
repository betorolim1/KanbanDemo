using KanbanDemo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanDemo.Core.Domains.Cards.Repository
{
    public interface ICardRepository
    {
        Task InsertCardAsync(CardDomain cardDomain);
        Task<List<Card>> GetNotRemovedCardsAsync();
        Task<Card> UpdateCardAsync(Card cardOld, CardDomain cardDomainNew);
        Task<Card> GetNotRemovedCardByIdAsync(Guid id);
        Card GetNotRemovedCardById(Guid id);
        Task DeleteCardAsync(Card card);
        IQueryable<Card> GetCardById(Guid id);
    }
}
