using KanbanDemo.Core.Domains.Cards;
using KanbanDemo.Core.Domains.Cards.Repository;
using KanbanDemo.Data.Context;
using KanbanDemo.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanDemo.Data.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly DatabaseContext _databaseContext;

        public CardRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Card>> GetNotRemovedCardsAsync()
        {
            return await _databaseContext.Cards.Where(x => !x.IsRemoved).ToListAsync();
        }

        public async Task<Card> GetNotRemovedCardByIdAsync(Guid id)
        {
            return await _databaseContext.Cards.Where(x => x.Id == id && !x.IsRemoved).FirstOrDefaultAsync();
        }

        public IQueryable<Card> GetCardById(Guid id)
        {
            return _databaseContext.Cards.Where(x => x.Id == id);
        }

        public async Task InsertCardAsync(CardDomain cardDomain)
        {
            var card = new Card
            {
                Conteudo = cardDomain.Conteudo,
                Id = new Guid(),
                Lista = cardDomain.Lista,
                Titulo = cardDomain.Titulo
            };

            await _databaseContext.AddAsync(card);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Card> UpdateCardAsync(Card cardOld, CardDomain cardDomainNew)
        {
            cardOld.Conteudo = cardDomainNew.Conteudo;
            cardOld.Lista = cardDomainNew.Lista;
            cardOld.Titulo = cardDomainNew.Titulo;

            _databaseContext.Update(cardOld);
            await _databaseContext.SaveChangesAsync();

            return cardOld;
        }

        public async Task DeleteCardAsync(Card card)
        {
            card.IsRemoved = true;
            _databaseContext.Update(card);

            //_databaseContext.Remove(card); -- Caso fosse remover da base, mas estamos removendo logicamente.
            await _databaseContext.SaveChangesAsync();
        }
    }
}
