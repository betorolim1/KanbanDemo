using KanbanDemo.Core.Commands;
using KanbanDemo.Core.Domains.Cards;
using KanbanDemo.Core.Domains.Cards.Repository;
using KanbanDemo.Core.Handlers.Interfaces;
using KanbanDemo.Core.Result;
using KanbanDemo.Core.Validator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace KanbanDemo.Core.Handlers
{
    public class CardHandler : Notifiable, ICardHandler
    {
        private ICardRepository _cardRepository;

        public CardHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<List<CardResult>> InsertCardAsync(InsertCardCommand command)
        {
            var cardDomain = CardDomain.Factory.CreateCardToInsert(command.Titulo, command.Conteudo, command.Lista);
            AddNotifications(cardDomain.Notifications);

            if (!IsValid)
                return null;

            await _cardRepository.InsertCardAsync(cardDomain);

            var cards = await GetAllCardsAsync();

            return cards;
        }

        public async Task<CardResult> UpdateCardAsync(UpdateCardCommand command)
        {
            var cardDomain = CardDomain.Factory.CreateCardToUpdate(command.Id, command.Titulo, command.Conteudo, command.Lista);
            AddNotifications(cardDomain.Notifications);

            if (!IsValid)
                return null;

            var cardOld = _cardRepository.GetNotRemovedCardById(command.Id).FirstOrDefault();

            if (cardOld is null)
                return null;

            var cardNew = await _cardRepository.UpdateCardAsync(cardOld, cardDomain);

            var cardResult = new CardResult
            {
                Conteudo = cardNew.Conteudo,
                Id = cardNew.Id,
                Lista = cardNew.Lista,
                Titulo = cardNew.Titulo
            };

            return cardResult;
        }

        public async Task<List<CardResult>> DeleteCardAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                AddNotification("Id inválido.");
                return null;
            }

            var card = _cardRepository.GetNotRemovedCardById(id).FirstOrDefault();

            if (card is null)
                return null;

            await _cardRepository.DeleteCardAsync(card);

            var cards = await GetAllCardsAsync();

            return cards;
        }


        public async Task<List<CardResult>> GetCards()
        {
            var cards = await GetAllCardsAsync();

            return cards;
        }

        private async Task<List<CardResult>> GetAllCardsAsync()
        {
            var cards = _cardRepository.GetNotRemovedCards();

            var cardResultList = cards.Select(x => new CardResult
            {
                Conteudo = x.Conteudo,
                Id = x.Id,
                Lista = x.Lista,
                Titulo = x.Titulo
            });

            return await cardResultList.ToListAsync();
        }
    }
}
