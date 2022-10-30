using AutoFixture;
using KanbanDemo.Core.Domains.Cards;
using System;
using Xunit;

namespace KanbanDemo.Core.Test.Domains.Cards
{
    public class CardDomainTest
    {
        private Fixture _fixture = new Fixture();

        // CreateCardToInsert

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateCardToInsert_Should_Notify_When_Titulo_Is_Null_Or_White_Spaces(string titulo)
        {
            var result = CardDomain.Factory.CreateCardToInsert(titulo, "ConteudoTest", "ListaTest");

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Título inválido.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateCardToInsert_Should_Notify_When_Conteudo_Is_Null_Or_White_Spaces(string conteudo)
        {
            var result = CardDomain.Factory.CreateCardToInsert("TituloTest", conteudo, "ListaTest");

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Conteudo inválido.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateCardToInsert_Should_Notify_When_Lista_Is_Null_Or_White_Spaces(string lista)
        {
            var result = CardDomain.Factory.CreateCardToInsert("TituloTest", "ConteudoTest", lista);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Lista inválida.");
        }

        [Fact]
        public void CreateCardToInsert_Should_Return_CardDomain()
        {
            var result = CardDomain.Factory.CreateCardToInsert("TituloTest", "ConteudoTest", "ListaTest");

            Assert.True(result.IsValid);
            Assert.Empty(result.Notifications);

            Assert.Equal("TituloTest", result.Titulo);
            Assert.Equal("ConteudoTest", result.Conteudo);
            Assert.Equal("ListaTest", result.Lista);
            Assert.Equal(new Guid(), result.Id);
        }

        // CreateCardToUpdate

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateCardToUpdate_Should_Notify_When_Titulo_Is_Null_Or_White_Spaces(string titulo)
        {
            var id = _fixture.Create<Guid>();

            var result = CardDomain.Factory.CreateCardToUpdate(id, titulo, "ConteudoTest", "ListaTest");

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Título inválido.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateCardToUpdate_Should_Notify_When_Conteudo_Is_Null_Or_White_Spaces(string conteudo)
        {
            var id = _fixture.Create<Guid>();

            var result = CardDomain.Factory.CreateCardToUpdate(id, "TituloTest", conteudo, "ListaTest");

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Conteudo inválido.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateCardToUpdate_Should_Notify_When_Lista_Is_Null_Or_White_Spaces(string lista)
        {
            var id = _fixture.Create<Guid>();

            var result = CardDomain.Factory.CreateCardToUpdate(id, "TituloTest", "ConteudoTest", lista);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Lista inválida.");
        }

        [Fact]
        public void CreateCardToUpdate_Should_Notify_When_Id_Is_Null_Or_White_Spaces()
        {
            var id = new Guid();

            var result = CardDomain.Factory.CreateCardToUpdate(id, "TituloTest", "ConteudoTest", "ListaTest");

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Id inválido.");
        }

        [Fact]
        public void CreateCardToUpdate_Should_Return_CardDomain()
        {
            var id = _fixture.Create<Guid>();

            var result = CardDomain.Factory.CreateCardToUpdate(id, "TituloTest", "ConteudoTest", "ListaTest");

            Assert.True(result.IsValid);
            Assert.Empty(result.Notifications);

            Assert.Equal("TituloTest", result.Titulo);
            Assert.Equal("ConteudoTest", result.Conteudo);
            Assert.Equal("ListaTest", result.Lista);
            Assert.Equal(id, result.Id);
        }
    }
}
