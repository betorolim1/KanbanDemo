using KanbanDemo.Core.Validator;
using System;

namespace KanbanDemo.Core.Domains.Cards
{
    public class CardDomain : Notifiable
    {
        private CardDomain(string titulo, string conteudo, string lista)
        {
            Titulo = titulo;
            Conteudo = conteudo;
            Lista = lista;

            ValidateMainFields();
        }

        private CardDomain(Guid id, string titulo, string conteudo, string lista)
        {
            Id = id;
            Titulo = titulo;
            Conteudo = conteudo;
            Lista = lista;

            ValidateId();
            ValidateMainFields();
        }

        public Guid Id { get; }
        public string Titulo { get; }
        public string Conteudo { get; }
        public string Lista { get; }

        private void ValidateId()
        {
            if (Id == Guid.Empty)
                AddNotification("Id inválido.");
        }

        private void ValidateMainFields()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                AddNotification("Título inválido.");

            if (string.IsNullOrWhiteSpace(Conteudo))
                AddNotification("Conteudo inválido.");

            if (string.IsNullOrWhiteSpace(Lista))
                AddNotification("Lista inválida.");
        }

        public static class Factory
        {
            public static CardDomain CreateCardToInsert(string titulo, string conteudo, string lista)
                => new CardDomain(titulo, conteudo, lista);
            public static CardDomain CreateCardToUpdate(Guid id, string titulo, string conteudo, string lista)
                => new CardDomain(id, titulo, conteudo, lista);
        }
    }
}
