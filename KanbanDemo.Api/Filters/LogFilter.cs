using KanbanDemo.Core.Domains.Cards.Repository;
using KanbanDemo.Model.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace KanbanDemo.API.Filters
{
    public class LogFilter : IActionFilter
    {
        private const string UPDATE_CARD = "UpdateCard";
        private const string ACTION_ALTERAR = "Alterar";

        private const string DELETE_CARD = "DeleteCard";
        private const string ACTION_REMOVER = "Remover";

        private ICardRepository _cardRepository;

        public LogFilter(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var path = context.HttpContext.Request.Path.Value;

            var id = path.Split('/').LastOrDefault();

            if (id is null)
            {
                Console.WriteLine($"{GetDateTimeNowFormated()} - Id not found - Card {id}");

                return;
            }

            var action = context.ActionDescriptor.RouteValues["controller"] + "/" + context.ActionDescriptor.RouteValues["action"];

            var actionName = GetAction(action);

            Card card = null;

            if (actionName == ACTION_ALTERAR)
                card = _cardRepository.GetNotRemovedCardById(new Guid(id));

            if (actionName == ACTION_REMOVER)
                card = _cardRepository.GetCardById(new Guid(id)).FirstOrDefault();

            if (card is null)
            {
                Console.WriteLine($"{GetDateTimeNowFormated()} - Card not found - Id {id}");

                return;
            }

            Console.WriteLine($"{GetDateTimeNowFormated()} - Card {id} - {card.Titulo} - {actionName}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        private string GetAction(string actionName)
        {
            if (actionName.Contains(UPDATE_CARD))
                return ACTION_ALTERAR;

            if (actionName.Contains(DELETE_CARD))
                return ACTION_REMOVER;

            return string.Empty;
        }

        private string GetDateTimeNowFormated() => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
