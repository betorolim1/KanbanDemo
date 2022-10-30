using AutoFixture;
using KanbanDemo.Core.Commands;
using KanbanDemo.Core.Domains.Cards;
using KanbanDemo.Core.Domains.Cards.Repository;
using KanbanDemo.Core.Handlers;
using KanbanDemo.Model.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KanbanDemo.Core.Test.Handlers
{
    public class CardHandlerTest
    {
        private CardHandler _handler;

        private Mock<ICardRepository> _cardRepository = new Mock<ICardRepository>();

        private Fixture _fixture = new Fixture();

        public CardHandlerTest()
        {
            _handler = new CardHandler(_cardRepository.Object);
        }

        // InsertCardAsync

        [Fact]
        public async Task InsertCardAsync_Should_Return_Null_When_There_Are_Notifications()
        {
            var command = _fixture.Build<InsertCardCommand>()
                .Without(x => x.Titulo)
                .Create();

            var result = await _handler.InsertCardAsync(command);

            Assert.Null(result);
            Assert.False(_handler.IsValid);
            Assert.NotEmpty(_handler.Notifications);

            VerifyMocks();
        }

        [Fact]
        public async Task InsertCardAsync_Should_Insert_And_Return_All_Cards()
        {
            var command = _fixture.Create<InsertCardCommand>();

            var cards = _fixture.CreateMany<Card>(10);

            _cardRepository.Setup(x => x.InsertCardAsync(It.Is<CardDomain>(x =>
                x.Id == new Guid() &&
                x.Conteudo == command.Conteudo &&
                x.Lista == command.Lista &&
                x.Titulo == command.Titulo
            )));

            _cardRepository.Setup(x => x.GetNotRemovedCardsAsync()).ReturnsAsync(cards.ToList());

            var result = await _handler.InsertCardAsync(command);

            Assert.NotNull(result);
            Assert.True(_handler.IsValid);
            Assert.Empty(_handler.Notifications);

            Assert.Equal(cards.Count(), result.Count);
            Assert.Equal(cards.FirstOrDefault().Conteudo, result.FirstOrDefault().Conteudo);
            Assert.Equal(cards.FirstOrDefault().Id, result.FirstOrDefault().Id);
            Assert.Equal(cards.FirstOrDefault().Lista, result.FirstOrDefault().Lista);
            Assert.Equal(cards.FirstOrDefault().Titulo, result.FirstOrDefault().Titulo);

            VerifyMocks();
        }

        // UpdateCardAsync

        [Fact]
        public async Task UpdateCardAsync_Should_Return_Null_When_There_Are_Notifications()
        {
            var command = _fixture.Build<UpdateCardCommand>()
                .Without(x => x.Titulo)
                .Create();

            var result = await _handler.UpdateCardAsync(command);

            Assert.Null(result);
            Assert.False(_handler.IsValid);
            Assert.NotEmpty(_handler.Notifications);

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateCardAsync_Should_Return_Null_When_Card_Is_Not_Found()
        {
            var command = _fixture.Create<UpdateCardCommand>();

            _cardRepository.Setup(x => x.GetNotRemovedCardByIdAsync(command.Id));

            var result = await _handler.UpdateCardAsync(command);

            Assert.Null(result);
            Assert.True(_handler.IsValid);
            Assert.Empty(_handler.Notifications);

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateCardAsync_Should_Update_And_Return_Updated_Card()
        {
            var command = _fixture.Create<UpdateCardCommand>();

            var card = _fixture.Create<Card>();

            _cardRepository.Setup(x => x.GetNotRemovedCardByIdAsync(command.Id)).ReturnsAsync(card);

            _cardRepository.Setup(x => x.UpdateCardAsync(card, It.Is<CardDomain>(x =>
                x.Id == command.Id &&
                x.Conteudo == command.Conteudo &&
                x.Lista == command.Lista &&
                x.Titulo == command.Titulo
            ))).ReturnsAsync(card);

            var result = await _handler.UpdateCardAsync(command);

            Assert.NotNull(result);
            Assert.True(_handler.IsValid);
            Assert.Empty(_handler.Notifications);

            Assert.Equal(card.Conteudo, result.Conteudo);
            Assert.Equal(card.Id, result.Id);
            Assert.Equal(card.Lista, result.Lista);
            Assert.Equal(card.Titulo, result.Titulo);

            VerifyMocks();
        }

        // DeleteCardAsync

        [Fact]
        public async Task DeleteCardAsync_Should_Notify_When_Id_Is_Empty()
        {
            var result = await _handler.DeleteCardAsync(new Guid());

            Assert.Null(result);
            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "Id inválido.");

            VerifyMocks();
        }

        [Fact]
        public async Task DeleteCardAsync_Should_Return_Null_When_Card_Is_Not_Found()
        {
            var id = _fixture.Create<Guid>();

            _cardRepository.Setup(x => x.GetNotRemovedCardByIdAsync(id));

            var result = await _handler.DeleteCardAsync(id);

            Assert.Null(result);
            Assert.True(_handler.IsValid);

            VerifyMocks();
        }

        [Fact]
        public async Task DeleteCardAsync_Should_Delete_Card_And_Return_All_Cards()
        {
            var id = _fixture.Create<Guid>();

            var card = _fixture.Create<Card>();

            var cards = _fixture.CreateMany<Card>(10);

            _cardRepository.Setup(x => x.GetNotRemovedCardByIdAsync(id)).ReturnsAsync(card);

            _cardRepository.Setup(x => x.DeleteCardAsync(card));

            _cardRepository.Setup(x => x.GetNotRemovedCardsAsync()).ReturnsAsync(cards.ToList());

            var result = await _handler.DeleteCardAsync(id);

            Assert.NotNull(result);
            Assert.True(_handler.IsValid);
            Assert.Empty(_handler.Notifications);

            Assert.Equal(cards.Count(), result.Count);
            Assert.Equal(cards.FirstOrDefault().Conteudo, result.FirstOrDefault().Conteudo);
            Assert.Equal(cards.FirstOrDefault().Id, result.FirstOrDefault().Id);
            Assert.Equal(cards.FirstOrDefault().Lista, result.FirstOrDefault().Lista);
            Assert.Equal(cards.FirstOrDefault().Titulo, result.FirstOrDefault().Titulo);

            VerifyMocks();
        }

        // GetCardsAsync

        [Fact]
        public async Task GetCardsAsync_Should_Return_All_Cards()
        {
            var cards = _fixture.CreateMany<Card>(10);

            _cardRepository.Setup(x => x.GetNotRemovedCardsAsync()).ReturnsAsync(cards.ToList());

            var result = await _handler.GetCardsAsync();

            Assert.NotNull(result);
            Assert.True(_handler.IsValid);
            Assert.Empty(_handler.Notifications);

            Assert.Equal(cards.Count(), result.Count);
            Assert.Equal(cards.FirstOrDefault().Conteudo, result.FirstOrDefault().Conteudo);
            Assert.Equal(cards.FirstOrDefault().Id, result.FirstOrDefault().Id);
            Assert.Equal(cards.FirstOrDefault().Lista, result.FirstOrDefault().Lista);
            Assert.Equal(cards.FirstOrDefault().Titulo, result.FirstOrDefault().Titulo);

            VerifyMocks();
        }

        private void VerifyMocks()
        {
            _cardRepository.VerifyAll();
            _cardRepository.VerifyNoOtherCalls();
        }
    }
}
