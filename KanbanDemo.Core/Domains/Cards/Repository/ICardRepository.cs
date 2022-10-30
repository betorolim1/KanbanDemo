using KanbanDemo.Model.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanDemo.Core.Domains.Cards.Repository
{
    public interface ICardRepository
    {
        Task InsertCardAsync(CardDomain cardDomain);
        IQueryable<Card> GetCards();
        Task<Card> UpdateCardAsync(Card cardOld, CardDomain cardDomainNew);
        IQueryable<Card> GetCardById(Guid id);
        Task DeleteCardAsync(Card card);
    }
}
