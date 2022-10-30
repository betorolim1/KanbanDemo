using AutoFixture;
using KanbanDemo.API.Controllers;
using KanbanDemo.Core.Commands;
using KanbanDemo.Core.Handlers.Interfaces;
using KanbanDemo.Core.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KanbanDemo.API.Test.Controllers
{
    public class CardControllerTest
    {
        private CardController _controller;

        private Mock<ICardHandler> _cardHandler = new Mock<ICardHandler>();

        private Fixture _fixture = new Fixture();

        public CardControllerTest()
        {
            _controller = new CardController(_cardHandler.Object);
        }

        //InsertCardAsync

        [Fact]
        public async Task InsertCardAsync_Should_Return_BadRequest_When_Command_Is_Null()
        {
            var result = await _controller.InsertCardAsync(null) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Command não pode ser nula", result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task InsertCardAsync_Should_Return_BadRequest_When_There_Are_Notifications()
        {
            var command = _fixture.Create<InsertCardCommand>();

            var notificationList = _fixture.CreateMany<string>().ToList();

            _cardHandler.Setup(x => x.IsValid).Returns(false);
            _cardHandler.Setup(x => x.Notifications).Returns(notificationList);

            _cardHandler.Setup(x => x.InsertCardAsync(command));

            var result = await _controller.InsertCardAsync(command) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(notificationList, result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task InsertCardAsync_Should_Return_OkObjectResult()
        {
            var command = _fixture.Create<InsertCardCommand>();

            var cardResultList = _fixture.CreateMany<CardResult>().ToList();

            _cardHandler.Setup(x => x.IsValid).Returns(true);

            _cardHandler.Setup(x => x.InsertCardAsync(command)).ReturnsAsync(cardResultList);

            var result = await _controller.InsertCardAsync(command) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(cardResultList, result.Value);

            VerifyMocks();
        }

        //UpdateCardAsync

        [Fact]
        public async Task UpdateCardAsync_Should_Return_BadRequest_When_Command_Is_Null()
        {
            var id = _fixture.Create<Guid>();

            var result = await _controller.UpdateCardAsync(id, null) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Command não pode ser nula", result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateCardAsync_Should_Return_BadRequest_When_There_Are_Notifications()
        {
            var id = _fixture.Create<Guid>();

            var command = _fixture.Create<UpdateCardCommand>();

            var notificationList = _fixture.CreateMany<string>().ToList();

            _cardHandler.Setup(x => x.IsValid).Returns(false);
            _cardHandler.Setup(x => x.Notifications).Returns(notificationList);

            _cardHandler.Setup(x => x.UpdateCardAsync(command));

            var result = await _controller.UpdateCardAsync(id, command) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(notificationList, result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateCardAsync_Should_Return_NotFound_When_There_Is_No_Card()
        {
            var id = _fixture.Create<Guid>();

            var command = _fixture.Create<UpdateCardCommand>();

            _cardHandler.Setup(x => x.IsValid).Returns(true);

            _cardHandler.Setup(x => x.UpdateCardAsync(command));

            var result = await _controller.UpdateCardAsync(id, command) as NotFoundResult;

            Assert.NotNull(result);

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateCardAsync_Should_Return_OkObjectResult()
        {
            var id = _fixture.Create<Guid>();

            var command = _fixture.Create<UpdateCardCommand>();

            var cardResult = _fixture.Create<CardResult>();

            _cardHandler.Setup(x => x.IsValid).Returns(true);

            _cardHandler.Setup(x => x.UpdateCardAsync(It.Is<UpdateCardCommand>(x =>
                x.Id == id &&
                x.Lista == command.Lista &&
                x.Titulo == command.Titulo &&
                x.Conteudo == command.Conteudo
            ))).ReturnsAsync(cardResult);

            var result = await _controller.UpdateCardAsync(id, command) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(cardResult, result.Value);

            VerifyMocks();
        }

        //DeleteCardAsync

        [Fact]
        public async Task DeleteCardAsync_Should_Return_BadRequest_When_There_Are_Notifications()
        {
            var id = _fixture.Create<Guid>();

            var notificationList = _fixture.CreateMany<string>().ToList();

            _cardHandler.Setup(x => x.IsValid).Returns(false);
            _cardHandler.Setup(x => x.Notifications).Returns(notificationList);

            _cardHandler.Setup(x => x.DeleteCardAsync(id));

            var result = await _controller.DeleteCardAsync(id) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(notificationList, result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task DeleteCardAsync_Should_Return_NotFound_When_There_Is_No_Card()
        {
            var id = _fixture.Create<Guid>();

            _cardHandler.Setup(x => x.IsValid).Returns(true);

            _cardHandler.Setup(x => x.DeleteCardAsync(id));

            var result = await _controller.DeleteCardAsync(id) as NotFoundResult;

            Assert.NotNull(result);

            VerifyMocks();
        }

        [Fact]
        public async Task DeleteCardAsync_Should_Return_OkObjectResult()
        {
            var id = _fixture.Create<Guid>();

            var cardResultList = _fixture.CreateMany<CardResult>().ToList();

            _cardHandler.Setup(x => x.IsValid).Returns(true);

            _cardHandler.Setup(x => x.DeleteCardAsync(id)).ReturnsAsync(cardResultList);

            var result = await _controller.DeleteCardAsync(id) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(cardResultList, result.Value);

            VerifyMocks();
        }

        // GetCardsAsync

        [Fact]
        public async Task GetCardsAsync_Should_Return_OkObjectResult()
        {
            var cardResultList = _fixture.CreateMany<CardResult>().ToList();

            _cardHandler.Setup(x => x.GetCardsAsync()).ReturnsAsync(cardResultList);

            var result = await _controller.GetCardsAsync() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(cardResultList, result.Value);

            VerifyMocks();
        }

        private void VerifyMocks()
        {
            _cardHandler.VerifyAll();
            _cardHandler.VerifyNoOtherCalls();
        }
    }
}
