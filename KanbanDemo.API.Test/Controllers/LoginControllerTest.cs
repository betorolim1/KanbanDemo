using AutoFixture;
using KanbanDemo.API.Controllers;
using KanbanDemo.Core.Handlers.Interfaces;
using KanbanDemo.Core.Requests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace KanbanDemo.API.Test.Controllers
{
    public class LoginControllerTest
    {
        private LoginController _controller;

        private Mock<ILoginHandler> _loginHandler = new Mock<ILoginHandler>();

        private Fixture _fixture = new Fixture();

        public LoginControllerTest()
        {
            _controller = new LoginController(_loginHandler.Object);
        }

        [Fact]
        public void GetCardsAsync_Should_Return_Unauthorized_When_Token_Is_Null()
        {
            var command = _fixture.Create<LoginCommand>();

            _loginHandler.Setup(x => x.Login(command));

            var result = _controller.Login(command) as UnauthorizedResult;

            Assert.NotNull(result);

            VerifyMocks();
        }

        [Fact]
        public void GetCardsAsync_Should_Return_OkObjectResult()
        {
            var tokenResult = _fixture.Create<string>();

            var command = _fixture.Create<LoginCommand>();

            _loginHandler.Setup(x => x.Login(command)).Returns(tokenResult);

            var result = _controller.Login(command) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(tokenResult, result.Value);

            VerifyMocks();
        }

        private void VerifyMocks()
        {
            _loginHandler.VerifyAll();
            _loginHandler.VerifyNoOtherCalls();
        }
    }
}
